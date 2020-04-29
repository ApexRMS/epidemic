// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using SyncroSim.Core;

namespace SyncroSim.Epidemic
{
    partial class Primary
    {
        private void SetLoopStatus(int iteration, Jurisdiction jurisdiction)
        {
            this.SetStatusMessage(string.Format(CultureInfo.InvariantCulture,
                "Simulating iteration {0}, jurisdiction {1}",
                iteration,
                jurisdiction.Name));
        }

        private JurisdictionCollection GetRuntimeJurisdictions()
        {
            JurisdictionCollection Jurisdictions = new JurisdictionCollection();
            DataTable dt = this.ResultScenario.GetDataSheet(Shared.DATASHEET_RUNTIME_JURISDICTION_NAME).GetData();;

            if (dt.DefaultView.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int Id = Convert.ToInt32(dr[Shared.JURISDICTION_COLUMN_NAME]);
                    Jurisdictions.Add(this.m_Jurisdictions[Id]);
                }
            }
            else
            {
                Jurisdictions = this.m_Jurisdictions;
            }

            Debug.Assert(Jurisdictions.Count > 0);
            return Jurisdictions;
        }

        private double GetMaxInfections(ModelState modelState)
        {
            Population Pop = this.m_PopulationMap.GetPopulation(modelState.JurisdictionId, modelState.Iteration);

            if (Pop == null)
            {
                DataSheet ds = this.Project.GetDataSheet(Shared.DATASHEET_JURISDICTION_NAME);

                Shared.ThrowEpidemicException("No population data defined for jurisdiction '{0}', iteration '{1}'.",
                    ds.ValidationTable.GetDisplayName(modelState.JurisdictionId), modelState.Iteration);
            }

            return Pop.TotalSize * modelState.AttackRate;
        }

        private ModelState CreateTimestepModelState(int jurisdictionId, int iteration, int timestep)
        {
            double GrowthRate = this.m_GrowthRateMap.GetGrowthRateValue(jurisdictionId, iteration, timestep);
            double GrowthRateMultiplier = this.m_GrowthRateMultiplierMap.GetGrowthRateMultiplierValue(jurisdictionId, iteration, timestep);
            double FatalityRate = this.m_FatalityRateMap.GetFatalityRateValue(jurisdictionId, iteration, timestep);
            double AttackRate = this.m_AttackRateMap.GetAttackRateValue(jurisdictionId, iteration, timestep);
            double IncubationPeriod = this.m_IncubationPeriodMap.GetIncubationPeriodValue(jurisdictionId, iteration, timestep);
            double SymptomPeriod = this.m_SymptomPeriodMap.GetSymptomPeriodValue(jurisdictionId, iteration, timestep);
            double InfectedPeriod = IncubationPeriod + SymptomPeriod;

            GrowthRate *= GrowthRateMultiplier;

            return new ModelState(
                jurisdictionId,
                iteration,
                timestep,
                GrowthRate,
                FatalityRate,
                AttackRate,
                IncubationPeriod,
                SymptomPeriod,
                InfectedPeriod);
        }

        private void ConfigureRunControl()
        {
            DataSheet ds = this.ResultScenario.GetDataSheet(Shared.DATASHEET_RUN_CONTROL_NAME);
            DataRow dr = ds.GetDataRow();

            if (dr == null)
            {
                Shared.ThrowEpidemicException("There is no run control data.");
            }

            if (dr[Shared.DATASHEET_RUN_CONTROL_MIN_ITERATION_COLUMN_NAME] == DBNull.Value)
            {
                Shared.ThrowEpidemicException("The run control minimum iterations is missing.");
            }

            if (dr[Shared.DATASHEET_RUN_CONTROL_MAX_ITERATION_COLUMN_NAME] == DBNull.Value)
            {
                Shared.ThrowEpidemicException("The run control total iterations is missing.");
            }

            if (dr[Shared.DATASHEET_RUN_CONTROL_START_DATE_COLUMN_NAME] == DBNull.Value)
            {
                Shared.ThrowEpidemicException("The run control start date is missing.");
            }

            if (dr[Shared.DATASHEET_RUN_CONTROL_END_DATE_COLUMN_NAME] == DBNull.Value)
            {
                Shared.ThrowEpidemicException("The run control end date is missing.");
            }

            DateTime Start = (DateTime)dr[Shared.DATASHEET_RUN_CONTROL_START_DATE_COLUMN_NAME];
            DateTime End = (DateTime)dr[Shared.DATASHEET_RUN_CONTROL_END_DATE_COLUMN_NAME];

            Start = new DateTime(Start.Year, Start.Month, Start.Day, 0, 0, 0);
            End = new DateTime(End.Year, End.Month, End.Day, 0, 0, 0);
            int TotalDays = (End - Start).Days + 1;

            if (TotalDays <= 0)
            {
                Shared.ThrowEpidemicException("The run control start date cannot be greater than the end date.");
            }

            dr[Shared.DATASHEET_RUN_CONTROL_MIN_TIMESTEP_COLUMN_NAME] = 1;
            dr[Shared.DATASHEET_RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME] = TotalDays;
        }

        private void InitializeRunControl()
        {
            DataRow dr = this.ResultScenario.GetDataSheet(Shared.DATASHEET_RUN_CONTROL_NAME).GetDataRow();
            DateTime Start = (DateTime)dr[Shared.DATASHEET_RUN_CONTROL_START_DATE_COLUMN_NAME];
            DateTime End = (DateTime)dr[Shared.DATASHEET_RUN_CONTROL_END_DATE_COLUMN_NAME];

            Start = new DateTime(Start.Year, Start.Month, Start.Day, 0, 0, 0);
            End = new DateTime(End.Year, End.Month, End.Day, 0, 0, 0);
            int TotalDays = (End - Start).Days + 1;

            //Timesteps
            this.MinimumTimestep = 1;
            this.MaximumTimestep = TotalDays;

            //Iterations
            this.MinimumIteration = Convert.ToInt32(dr[Shared.DATASHEET_RUN_CONTROL_MIN_ITERATION_COLUMN_NAME]);
            this.MaximumIteration = Convert.ToInt32(dr[Shared.DATASHEET_RUN_CONTROL_MAX_ITERATION_COLUMN_NAME]);

            //Historical deaths
            this.m_ModelHistoricalDeaths = Booleans.BoolFromValue(
                dr[Shared.DATASHEET_RUN_CONTROL_MODEL_HISTORICAL_DEATHS_COLUMN_NAME]);
        }

        private DateTime GetStartDateTime()
        {
            DataRow dr = this.ResultScenario.GetDataSheet(Shared.DATASHEET_RUN_CONTROL_NAME).GetDataRow();
            DateTime Start = (DateTime)dr[Shared.DATASHEET_RUN_CONTROL_START_DATE_COLUMN_NAME];

            return new DateTime(Start.Year, Start.Month, Start.Day, 0, 0, 0);
        }

        private DateTime GetEndDateTime()
        {
            DataRow dr = this.ResultScenario.GetDataSheet(Shared.DATASHEET_RUN_CONTROL_NAME).GetDataRow();
            DateTime Start = (DateTime)dr[Shared.DATASHEET_RUN_CONTROL_END_DATE_COLUMN_NAME];

            return new DateTime(Start.Year, Start.Month, Start.Day, 0, 0, 0);
        }

        private bool TimestepFromDateTime(DataRow dr, DateTime startDate, out int? timestep)
        {
            if (dr[Shared.TIMESTEP_COLUMN_NAME] == DBNull.Value)
            {
                timestep = null;
                return true;
            }

            DateTime d = (DateTime)dr[Shared.TIMESTEP_COLUMN_NAME];
            d = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
            int v = (d - startDate).Days + 1;

            if (v < this.MinimumTimestep)
            {
                timestep = null;
                return false;
            }
            else if (v > this.MaximumTimestep)
            {
                timestep = null;
                return false;
            }

            timestep = v;
            return true;
        }

        private DateTime DateTimeFromTimestep(int timestep, DateTime dts, DateTime dte)
        {
            DateTime dt = new DateTime(dts.Year, dts.Month, dts.Day, 0, 0, 0);
            dt = dt.AddDays(timestep - 1);

            if (dt < dts)
            {
                Shared.ThrowEpidemicException(
                    "The timestep '{0}' ({1}) is less than the minimum date in run control.",
                    timestep, dt.ToString("yyyy-MM-dd"));
            }
            else if (dt > dte)
            {
                Shared.ThrowEpidemicException(
                    "The timestep '{0}'  ({1}) is greater than the maximum date in run control.",
                    timestep, dt.ToString("yyyy-MM-dd"));
            }

            return dt;
        }
    }
}
