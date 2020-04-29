// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.Epidemic
{
    partial class Primary
    {
        private void Simulate()
        {
            for (int Iteration = this.MinimumIteration; Iteration <= this.MaximumIteration; Iteration++)
            {
                this.ResampleForIteration(Iteration);

                foreach (Jurisdiction Juris in this.GetRuntimeJurisdictions())
                {
                    this.SetLoopStatus(Iteration, Juris);

                    ModelStateMap State = new ModelStateMap();

                    for (int Timestep = this.MinimumTimestep; Timestep <= this.MaximumTimestep; Timestep++)
                    {
                        this.CheckForCancel();
                        this.ResampleForTimestep(Iteration, Timestep);
                        ModelState ThisTimestep = this.CreateTimestepModelState(Juris.Id, Iteration, Timestep);
                        double MaxInfections = this.GetMaxInfections(ThisTimestep);

                        if (Timestep != this.MinimumTimestep)
                        {
                            ModelState PrevTimestep = State.GetItem(Timestep - 1);
                            int TimestepAdjusted = Timestep + (int)ThisTimestep.InfectedPeriod;
                            ActualDeath DeathRec = this.m_ActualDeathMap.GetActualDeath(Juris.Id, Iteration, TimestepAdjusted);

                            if (DeathRec != null)
                            {
                                //Calculate infections based on later deaths
                                ThisTimestep.Infected = DeathRec.CurrentValue.Value / ThisTimestep.FatalityRate;
                                ThisTimestep.CumulativeInfected = PrevTimestep.CumulativeInfected + ThisTimestep.Infected;
                            }
                            else
                            {
                                ModelType mt = this.m_ModelTypeMap.GetModelType(Juris.Id, Timestep);

                                //Because the attack rate can change the previous timestep may exceed MaxInfections
                                if(PrevTimestep.CumulativeInfected >= MaxInfections)
                                {
                                    ThisTimestep.CumulativeInfected = PrevTimestep.CumulativeInfected;
                                }
                                else if (mt != null && mt.EpidemicModel == EpidemicModelType.Exponential)
                                {
                                    //Use exponential curve to calculate cumulative infections
                                    ThisTimestep.CumulativeInfected = PrevTimestep.CumulativeInfected * (1 + ThisTimestep.GrowthRate);

                                    if (ThisTimestep.CumulativeInfected > MaxInfections)
                                    {                                        
                                        ThisTimestep.CumulativeInfected = MaxInfections;
                                    }
                                }
                                else
                                {
                                    //Use logistic curve to calculate cumulative infections
                                    ThisTimestep.CumulativeInfected =
                                        PrevTimestep.CumulativeInfected *
                                        ((ThisTimestep.GrowthRate) * (1 - PrevTimestep.CumulativeInfected / MaxInfections) + 1);

                                    if (ThisTimestep.CumulativeInfected > MaxInfections)
                                    {
                                        ThisTimestep.CumulativeInfected = MaxInfections;
                                    }
                                }

                                ThisTimestep.Infected = ThisTimestep.CumulativeInfected - PrevTimestep.CumulativeInfected;
                            }

                            DeathRec = this.m_ActualDeathMap.GetActualDeath(Juris.Id, Iteration, Timestep);

                            if (DeathRec != null && DeathRec.CurrentValue.Value > 0 && !this.m_ModelHistoricalDeaths)
                            {
                                //Set deaths to actual values if they exist
                                ThisTimestep.Deaths = DeathRec.CurrentValue.Value;
                            }
                            else
                            {
                                //Calculate deaths based on earlier infections
                                if ((Timestep - ThisTimestep.InfectedPeriod) > 0)
                                {
                                    ModelState t = State.GetState(Timestep - (int)ThisTimestep.InfectedPeriod);

                                    if (t != null)
                                    {
                                        ThisTimestep.Deaths = t.Infected * t.FatalityRate;
                                    }
                                    else
                                    {
                                        ThisTimestep.Deaths = 0;
                                    }
                                }
                                else
                                {
                                    ThisTimestep.Deaths = 0;
                                }
                            }

                            ThisTimestep.CumulativeDeaths = PrevTimestep.CumulativeDeaths + ThisTimestep.Deaths;
                        }

                        State.RecordState(ThisTimestep);
                        this.RecordOutput(ThisTimestep);
                        this.StepProgress();
                    }
                }
            }
        }
    }
}
