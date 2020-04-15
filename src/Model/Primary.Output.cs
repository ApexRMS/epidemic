// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;

namespace SyncroSim.Epidemic
{
    partial class Primary
    {
        private DataTable m_OutputGrowthRate;
        private DataTable m_OutputFatalityRate;
        private DataTable m_OutputAttackRate;
        private DataTable m_OutputIncubationPeriod;
        private DataTable m_OutputSymptomPeriod;
        private DataTable m_OutputInfectedPeriod;
        private DataTable m_OutputInfected;
        private DataTable m_OutputDeath;

        private void InitializeOutputTables()
        {
            Debug.Assert(this.m_OutputGrowthRate == null);

            this.m_OutputGrowthRate = this.ResultScenario.GetDataSheet(Shared.DATASHEET_OUTPUT_GROWTH_RATE_NAME).GetData();
            this.m_OutputFatalityRate = this.ResultScenario.GetDataSheet(Shared.DATASHEET_OUTPUT_FATALITY_RATE_NAME).GetData();
            this.m_OutputAttackRate = this.ResultScenario.GetDataSheet(Shared.DATASHEET_OUTPUT_ATTACK_RATE_NAME).GetData();
            this.m_OutputIncubationPeriod = this.ResultScenario.GetDataSheet(Shared.DATASHEET_OUTPUT_INCUBATION_PERIOD_NAME).GetData();
            this.m_OutputSymptomPeriod = this.ResultScenario.GetDataSheet(Shared.DATASHEET_OUTPUT_SYMPTOM_PERIOD_NAME).GetData();
            this.m_OutputInfectedPeriod = this.ResultScenario.GetDataSheet(Shared.DATASHEET_OUTPUT_INFECTED_PERIOD_NAME).GetData();
            this.m_OutputInfected = this.ResultScenario.GetDataSheet(Shared.DATASHEET_OUTPUT_INFECTED_NAME).GetData();
            this.m_OutputDeath = this.ResultScenario.GetDataSheet(Shared.DATASHEET_OUTPUT_DEATH_NAME).GetData();
        }

        private void RecordOutput(ModelState state)
        {
            DateTime Date = this.DateTimeFromTimestep(
                state.Timestep, 
                this.GetStartDateTime(), 
                this.GetEndDateTime());

            this.WriteGrowthRateOutput(state, Date);
            this.WriteFatalityRateOutput(state, Date);
            this.WriteAttackRateOutput(state, Date);
            this.WriteIncubationPeriodOutput(state, Date);
            this.WriteSymptomPeriodOutput(state, Date);
            this.WriteInfectedPeriodOutput(state, Date);
            this.WriteInfectedOutput(state, Date); 
            this.WriteDeathOutput(state, Date);
        }

        private void WriteGrowthRateOutput(ModelState state, DateTime date)
        {
            DataRow dr = this.m_OutputGrowthRate.NewRow();

            dr[Shared.ITERATION_COLUMN_NAME] = state.Iteration;
            dr[Shared.TIMESTEP_COLUMN_NAME] = state.Timestep;
            dr[Shared.DATE_COLUMN_NAME] = date;
            dr[Shared.JURISDICTION_COLUMN_NAME] = state.JurisdictionId;
            dr[Shared.VALUE_COLUMN_NAME] = state.GrowthRate;

            this.m_OutputGrowthRate.Rows.Add(dr);
        }

        private void WriteFatalityRateOutput(ModelState state, DateTime date)
        {
            DataRow dr = this.m_OutputFatalityRate.NewRow();

            dr[Shared.ITERATION_COLUMN_NAME] = state.Iteration;
            dr[Shared.TIMESTEP_COLUMN_NAME] = state.Timestep;
            dr[Shared.DATE_COLUMN_NAME] = date;
            dr[Shared.JURISDICTION_COLUMN_NAME] = state.JurisdictionId;
            dr[Shared.VALUE_COLUMN_NAME] = state.FatalityRate;

            this.m_OutputFatalityRate.Rows.Add(dr);
        }

        private void WriteAttackRateOutput(ModelState state, DateTime date)
        {
            DataRow dr = this.m_OutputAttackRate.NewRow();

            dr[Shared.ITERATION_COLUMN_NAME] = state.Iteration;
            dr[Shared.TIMESTEP_COLUMN_NAME] = state.Timestep;
            dr[Shared.DATE_COLUMN_NAME] = date;
            dr[Shared.JURISDICTION_COLUMN_NAME] = state.JurisdictionId;
            dr[Shared.VALUE_COLUMN_NAME] = state.AttackRate;

            this.m_OutputAttackRate.Rows.Add(dr);
        }

        private void WriteIncubationPeriodOutput(ModelState state, DateTime date)
        {
            DataRow dr = this.m_OutputIncubationPeriod.NewRow();

            dr[Shared.ITERATION_COLUMN_NAME] = state.Iteration;
            dr[Shared.TIMESTEP_COLUMN_NAME] = state.Timestep;
            dr[Shared.DATE_COLUMN_NAME] = date;
            dr[Shared.JURISDICTION_COLUMN_NAME] = state.JurisdictionId;
            dr[Shared.VALUE_COLUMN_NAME] = state.IncubationPeriod;

            this.m_OutputIncubationPeriod.Rows.Add(dr);
        }

        private void WriteSymptomPeriodOutput(ModelState state, DateTime date)
        {
            DataRow dr = this.m_OutputSymptomPeriod.NewRow();

            dr[Shared.ITERATION_COLUMN_NAME] = state.Iteration;
            dr[Shared.TIMESTEP_COLUMN_NAME] = state.Timestep;
            dr[Shared.DATE_COLUMN_NAME] = date;
            dr[Shared.JURISDICTION_COLUMN_NAME] = state.JurisdictionId;
            dr[Shared.VALUE_COLUMN_NAME] = state.SymptomPeriod;

            this.m_OutputSymptomPeriod.Rows.Add(dr);
        }

        private void WriteInfectedPeriodOutput(ModelState state, DateTime date)
        {
            DataRow dr = this.m_OutputInfectedPeriod.NewRow();

            dr[Shared.ITERATION_COLUMN_NAME] = state.Iteration;
            dr[Shared.TIMESTEP_COLUMN_NAME] = state.Timestep;
            dr[Shared.DATE_COLUMN_NAME] = date;
            dr[Shared.JURISDICTION_COLUMN_NAME] = state.JurisdictionId;
            dr[Shared.VALUE_COLUMN_NAME] = state.InfectedPeriod;

            this.m_OutputInfectedPeriod.Rows.Add(dr);
        }

        private void WriteInfectedOutput(ModelState state, DateTime date)
        {
            if (state.CumulativeInfected <= 0.0)
            {
                return;
            }

            DataRow dr = this.m_OutputInfected.NewRow();

            dr[Shared.ITERATION_COLUMN_NAME] = state.Iteration;
            dr[Shared.TIMESTEP_COLUMN_NAME] = state.Timestep;
            dr[Shared.DATE_COLUMN_NAME] = date;
            dr[Shared.JURISDICTION_COLUMN_NAME] = state.JurisdictionId;
            dr[Shared.VALUE_COLUMN_NAME] = state.Infected;
            dr[Shared.CUMULATIVE_VALUE_COLUMN_NAME] = state.CumulativeInfected;

            this.m_OutputInfected.Rows.Add(dr);
        }

        private void WriteDeathOutput(ModelState state, DateTime date)
        {
            if (state.CumulativeDeaths <= 0.0)
            {
                return;
            }

            DataRow dr = this.m_OutputDeath.NewRow();

            dr[Shared.ITERATION_COLUMN_NAME] = state.Iteration;
            dr[Shared.TIMESTEP_COLUMN_NAME] = state.Timestep;
            dr[Shared.DATE_COLUMN_NAME] = date;
            dr[Shared.JURISDICTION_COLUMN_NAME] = state.JurisdictionId;
            dr[Shared.VALUE_COLUMN_NAME] = state.Deaths;
            dr[Shared.CUMULATIVE_VALUE_COLUMN_NAME] = state.CumulativeDeaths;

            this.m_OutputDeath.Rows.Add(dr);
        }
    }
}
