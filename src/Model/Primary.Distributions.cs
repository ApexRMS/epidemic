// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Diagnostics;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.StochasticTime;

namespace SyncroSim.Epidemic
{
    partial class Primary
    {
        private RandomGenerator m_RandomGenerator = new RandomGenerator();
        private EPDistributionProvider m_DistributionProvider;

        private void CreateDistributionProvider()
        {
            Debug.Assert(this.m_DistributionProvider == null);

            this.m_DistributionProvider = new EPDistributionProvider(
                this.ResultScenario, 
                this.m_RandomGenerator);
        }

        private void InitializeDistributionValues()
        {
            this.m_DistributionProvider.InitializeExternalVariableValues();
            this.m_DistributionProvider.InitializeDistributionValues();
            this.InitializeGrowthRateDistributions();
            this.InitializeFatalityRateDistributions();
            this.InitializeIncubationPeriodDistributions();
            this.InitializeSymptomPeriodDistributions();
            this.InitializeAttackRateDistributions();
        }

        private void ResampleForIteration(int iteration)
        {
            this.ResampleExternalVariableValues(iteration, this.MinimumTimestep, DistributionFrequency.Iteration);
            this.ResampleDistributionValues(iteration, this.MinimumTimestep, DistributionFrequency.Iteration);
            this.ResampleGrowthRates(iteration, this.MinimumTimestep, DistributionFrequency.Iteration);
            this.ResampleFatalityRates(iteration, this.MinimumTimestep, DistributionFrequency.Iteration);
            this.ResampleIncubationPeriods(iteration, this.MinimumTimestep, DistributionFrequency.Iteration);
            this.ResampleSymptomPeriods(iteration, this.MinimumTimestep, DistributionFrequency.Iteration);
            this.ResampleAttackRates(iteration, this.MinimumTimestep, DistributionFrequency.Iteration);
        }

        private void ResampleForTimestep(int iteration, int timestep)
        {
            this.ResampleExternalVariableValues(iteration, timestep, DistributionFrequency.Timestep);
            this.ResampleDistributionValues(iteration, timestep, DistributionFrequency.Timestep);
            this.ResampleGrowthRates(iteration, timestep, DistributionFrequency.Timestep);
            this.ResampleFatalityRates(iteration, timestep, DistributionFrequency.Timestep);
            this.ResampleIncubationPeriods(iteration, timestep, DistributionFrequency.Timestep);
            this.ResampleSymptomPeriods(iteration, timestep, DistributionFrequency.Timestep);
            this.ResampleAttackRates(iteration, timestep, DistributionFrequency.Timestep);
        }

        private void ExpandForUserDistributions()
        {
            if (this.m_DistributionProvider.Values.Count > 0)
            {
                EPDistributionBaseExpander Expander = 
                    new EPDistributionBaseExpander(this.m_DistributionProvider);

                this.ExpandGrowthRates(Expander);
                this.ExpandFatalityRates(Expander);
                this.ExpandIncubationPeriods(Expander);
                this.ExpandSymptomPeriods(Expander);
                this.ExpandAttackRates(Expander);
            }
        }

        private void InitializeGrowthRateDistributions()
        {
            try
            {
                foreach (GrowthRate t in this.m_GrowthRates)
                {
                    if (!t.IsDisabled)
                    {
                        t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("Growth Rates" + " -> " + ex.Message);
            }
        }

        private void InitializeFatalityRateDistributions()
        {
            try
            {
                foreach (FatalityRate t in this.m_FatalityRates)
                {
                    if (!t.IsDisabled)
                    {
                        t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("Fatality Rates" + " -> " + ex.Message);
            }
        }

        private void InitializeIncubationPeriodDistributions()
        {
            try
            {
                foreach (IncubationPeriod t in this.m_IncubationPeriods)
                {
                    if (!t.IsDisabled)
                    {
                        t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("Incubation Periods" + " -> " + ex.Message);
            }
        }

        private void InitializeSymptomPeriodDistributions()
        {
            try
            {
                foreach (SymptomPeriod t in this.m_SymptomPeriods)
                {
                    if (!t.IsDisabled)
                    {
                        t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("Symptom Periods" + " -> " + ex.Message);
            }
        }

        private void InitializeAttackRateDistributions()
        {
            try
            {
                foreach (AttackRate t in this.m_AttackRates)
                {
                    if (!t.IsDisabled)
                    {
                        t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("Attack Rates" + " -> " + ex.Message);
            }
        }

        private void ResampleExternalVariableValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                this.m_DistributionProvider.SampleExternalVariableValues(iteration, timestep, frequency);
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("External Variable Values" + " -> " + ex.Message);
            }
        }

        private void ResampleDistributionValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                this.m_DistributionProvider.SampleDistributionValues(iteration, timestep, frequency);
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("Distribution Values" + " -> " + ex.Message);
            }
        }

        private void ResampleGrowthRates(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (GrowthRate t in this.m_GrowthRates)
                {
                    if (!t.IsDisabled)
                    {
                        t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("Growth Rates" + " -> " + ex.Message);
            }
        }

        private void ResampleFatalityRates(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (FatalityRate t in this.m_FatalityRates)
                {
                    if (!t.IsDisabled)
                    {
                        t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("Fatality Rates" + " -> " + ex.Message);
            }
        }

        private void ResampleIncubationPeriods(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (IncubationPeriod t in this.m_IncubationPeriods)
                {
                    if (!t.IsDisabled)
                    {
                        t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("Incubation Periods" + " -> " + ex.Message);
            }
        }

        private void ResampleSymptomPeriods(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (SymptomPeriod t in this.m_SymptomPeriods)
                {
                    if (!t.IsDisabled)
                    {
                        t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("Symptom Periods" + " -> " + ex.Message);
            }
        }

        private void ResampleAttackRates(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (AttackRate t in this.m_AttackRates)
                {
                    if (!t.IsDisabled)
                    {
                        t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                    }
                }
            }
            catch (Exception ex)
            {
                Shared.ThrowEpidemicException("Attack Rates" + " -> " + ex.Message);
            }
        }

        private void ExpandGrowthRates(EPDistributionBaseExpander expander)
        {
            if (this.m_GrowthRates.Count > 0)
            {
                IEnumerable<EPDistributionBase> NewItems = expander.Expand(this.m_GrowthRates);

                this.m_GrowthRates.Clear();

                foreach (GrowthRate t in NewItems)
                {
                    this.m_GrowthRates.Add(t);
                }
            }
        }

        private void ExpandFatalityRates(EPDistributionBaseExpander expander)
        {
            if (this.m_FatalityRates.Count > 0)
            {
                IEnumerable<EPDistributionBase> NewItems = expander.Expand(this.m_FatalityRates);

                this.m_FatalityRates.Clear();

                foreach (FatalityRate t in NewItems)
                {
                    this.m_FatalityRates.Add(t);
                }
            }
        }

        private void ExpandIncubationPeriods(EPDistributionBaseExpander expander)
        {
            if (this.m_IncubationPeriods.Count > 0)
            {
                IEnumerable<EPDistributionBase> NewItems = expander.Expand(this.m_IncubationPeriods);

                this.m_IncubationPeriods.Clear();

                foreach (IncubationPeriod t in NewItems)
                {
                    this.m_IncubationPeriods.Add(t);
                }
            }
        }

        private void ExpandSymptomPeriods(EPDistributionBaseExpander expander)
        {
            if (this.m_SymptomPeriods.Count > 0)
            {
                IEnumerable<EPDistributionBase> NewItems = expander.Expand(this.m_SymptomPeriods);

                this.m_SymptomPeriods.Clear();

                foreach (SymptomPeriod t in NewItems)
                {
                    this.m_SymptomPeriods.Add(t);
                }
            }
        }

        private void ExpandAttackRates(EPDistributionBaseExpander expander)
        {
            if (this.m_AttackRates.Count > 0)
            {
                IEnumerable<EPDistributionBase> NewItems = expander.Expand(this.m_AttackRates);

                this.m_AttackRates.Clear();

                foreach (AttackRate t in NewItems)
                {
                    this.m_AttackRates.Add(t);
                }
            }
        }
    }
}
