// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using SyncroSim.Core;
using SyncroSim.StochasticTime;

namespace SyncroSim.Epidemic
{
    partial class Primary
    {
        private JurisdictionCollection m_Jurisdictions = new JurisdictionCollection();
        private PopulationCollection m_Populations = new PopulationCollection();
        private ActualDeathCollection m_ActualDeaths = new ActualDeathCollection();
        private GrowthRateCollection m_GrowthRates = new GrowthRateCollection();
        private FatalityRateCollection m_FatalityRates = new FatalityRateCollection();
        private AttackRateCollection m_AttackRates = new AttackRateCollection();
        private ModelTypeCollection m_ModelTypes = new ModelTypeCollection();
        private IncubationPeriodCollection m_IncubationPeriods = new IncubationPeriodCollection();
        private SymptomPeriodCollection m_SymptomPeriods = new SymptomPeriodCollection();

        private void FillModelCollections()
        {
            DateTime RunControlStartDate = this.GetStartDateTime();

            this.FillJurisdictionCollection();
            this.FillPopulationCollection();
            this.FillActualDeathCollection(RunControlStartDate);
            this.FillGrowthRateCollection(RunControlStartDate);
            this.FillFatalityRateCollection(RunControlStartDate);
            this.FillAttackRateCollection(RunControlStartDate);
            this.FillModelTypeCollection(RunControlStartDate);
            this.FillIncubationPeriodCollection(RunControlStartDate);
            this.FillSymptomPeriodCollection(RunControlStartDate);
        }

        private void FillJurisdictionCollection()
        {
            Debug.Assert(this.m_Jurisdictions.Count == 0);
            DataSheet ds = this.Project.GetDataSheet(Shared.DATASHEET_JURISDICTION_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                Jurisdiction Item = new Jurisdiction(
                    Convert.ToInt32(dr[Shared.DATASHEET_JURISDICTION_ID_COLUMN_NAME]),
                    Convert.ToString(dr[Shared.DATASHEET_NAME_COLUMN_NAME]));

                this.m_Jurisdictions.Add(Item);
            }

            if (this.m_Jurisdictions.Count == 0)
            {
                Shared.ThrowEpidemicException("At least one jurisdiction is required.");
            }
        }

        private void FillPopulationCollection()
        {
            Debug.Assert(this.m_Populations.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Shared.DATASHEET_POPULATION_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                this.m_Populations.Add(new Population(
                    Shared.GetNullableInt(dr, Shared.ITERATION_COLUMN_NAME),
                    Convert.ToInt32(dr[Shared.JURISDICTION_COLUMN_NAME]),
                    Convert.ToDouble(dr[Shared.DATASHEET_POPULATION_TOTAL_SIZE_COLUMN_NAME])));
            }
        }

        private void FillActualDeathCollection(DateTime startingDateTime)
        {
            Debug.Assert(this.m_ActualDeaths.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Shared.DATASHEET_ACTUAL_DEATH_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                ActualDeath Item = new ActualDeath(
                    this.TimestepFromDateTime(dr, startingDateTime, ds.DisplayName).Value,
                    Convert.ToInt32(dr[Shared.JURISDICTION_COLUMN_NAME]),
                    Convert.ToDouble(dr[Shared.VALUE_COLUMN_NAME]));

                this.m_ActualDeaths.Add(Item);
            }
        }

        private void FillGrowthRateCollection(DateTime startingDateTime)
        {
            Debug.Assert(this.m_GrowthRates.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Shared.DATASHEET_GROWTH_RATE_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                DistributionFrequency? df = null;

                if (dr[Shared.DISTRIBUTION_FREQUENCY_COLUMN_NAME] != DBNull.Value)
                {
                    df = (DistributionFrequency)(long)dr[Shared.DISTRIBUTION_FREQUENCY_COLUMN_NAME];
                }

                GrowthRate Item = new GrowthRate(
                    Shared.GetNullableInt(dr, Shared.ITERATION_COLUMN_NAME),
                    this.TimestepFromDateTime(dr, startingDateTime, ds.DisplayName),
                    Shared.GetNullableInt(dr, Shared.JURISDICTION_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.VALUE_COLUMN_NAME),
                    Shared.GetNullableInt(dr, Shared.DISTRIBUTION_TYPE_COLUMN_NAME),
                    df,
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONSD_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONMIN_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONMAX_COLUMN_NAME));

                try
                {
                    this.m_DistributionProvider.Validate(
                        Item.DistributionTypeId, 
                        Item.DistributionValue, 
                        Item.DistributionSD, 
                        Item.DistributionMin, 
                        Item.DistributionMax);

                    this.m_GrowthRates.Add(Item);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ds.DisplayName + " -> " + ex.Message);
                }
            }
        }

        private void FillFatalityRateCollection(DateTime startingDateTime)
        {
            Debug.Assert(this.m_FatalityRates.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Shared.DATASHEET_FATALITY_RATE_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                DistributionFrequency? df = null;

                if (dr[Shared.DISTRIBUTION_FREQUENCY_COLUMN_NAME] != DBNull.Value)
                {
                    df = (DistributionFrequency)(long)dr[Shared.DISTRIBUTION_FREQUENCY_COLUMN_NAME];
                }

                FatalityRate Item = new FatalityRate(
                    Shared.GetNullableInt(dr, Shared.ITERATION_COLUMN_NAME),
                    this.TimestepFromDateTime(dr, startingDateTime, ds.DisplayName),
                    Shared.GetNullableInt(dr, Shared.JURISDICTION_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.VALUE_COLUMN_NAME),
                    Shared.GetNullableInt(dr, Shared.DISTRIBUTION_TYPE_COLUMN_NAME),
                    df,
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONSD_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONMIN_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONMAX_COLUMN_NAME));

                try
                {
                    this.m_DistributionProvider.Validate(
                        Item.DistributionTypeId,
                        Item.DistributionValue,
                        Item.DistributionSD,
                        Item.DistributionMin,
                        Item.DistributionMax);

                    this.m_FatalityRates.Add(Item);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ds.DisplayName + " -> " + ex.Message);
                }
            }
        }

        private void FillAttackRateCollection(DateTime startingDateTime)
        {
            Debug.Assert(this.m_AttackRates.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Shared.DATASHEET_ATTACK_RATE_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                DistributionFrequency? df = null;

                if (dr[Shared.DISTRIBUTION_FREQUENCY_COLUMN_NAME] != DBNull.Value)
                {
                    df = (DistributionFrequency)(long)dr[Shared.DISTRIBUTION_FREQUENCY_COLUMN_NAME];
                }

                double Value = 1.0;

                if (dr[Shared.VALUE_COLUMN_NAME] != DBNull.Value)
                {
                    Value = Convert.ToDouble(dr[Shared.VALUE_COLUMN_NAME]);
                }

                AttackRate Item = new AttackRate(
                    Shared.GetNullableInt(dr, Shared.ITERATION_COLUMN_NAME),
                    this.TimestepFromDateTime(dr, startingDateTime, ds.DisplayName),
                    Shared.GetNullableInt(dr, Shared.JURISDICTION_COLUMN_NAME),
                    Value,
                    Shared.GetNullableInt(dr, Shared.DISTRIBUTION_TYPE_COLUMN_NAME),
                    df,
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONSD_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONMIN_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONMAX_COLUMN_NAME));

                try
                {
                    this.m_DistributionProvider.Validate(
                        Item.DistributionTypeId,
                        Item.DistributionValue,
                        Item.DistributionSD,
                        Item.DistributionMin,
                        Item.DistributionMax);

                    this.m_AttackRates.Add(Item);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ds.DisplayName + " -> " + ex.Message);
                }
            }
        }

        private void FillModelTypeCollection(DateTime startingDateTime)
        {
            Debug.Assert(this.m_ModelTypes.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Shared.DATASHEET_MODEL_TYPE_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                this.m_ModelTypes.Add(new ModelType(
                    this.TimestepFromDateTime(dr, startingDateTime, ds.DisplayName),
                    Shared.GetNullableInt(dr, Shared.JURISDICTION_COLUMN_NAME),
                    (EpidemicModelType)(long)dr[Shared.DATASHEET_MODEL_TYPE_COLUMN_NAME]));
            }
        }

        private void FillIncubationPeriodCollection(DateTime startingDateTime)
        {
            Debug.Assert(this.m_IncubationPeriods.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Shared.DATASHEET_INCUBATION_PERIOD_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                DistributionFrequency? df = null;

                if (dr[Shared.DISTRIBUTION_FREQUENCY_COLUMN_NAME] != DBNull.Value)
                {
                    df = (DistributionFrequency)(long)dr[Shared.DISTRIBUTION_FREQUENCY_COLUMN_NAME];
                }

                IncubationPeriod Item = new IncubationPeriod(
                    Shared.GetNullableInt(dr, Shared.ITERATION_COLUMN_NAME),
                    this.TimestepFromDateTime(dr, startingDateTime, ds.DisplayName),
                    Shared.GetNullableInt(dr, Shared.JURISDICTION_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.VALUE_COLUMN_NAME),
                    Shared.GetNullableInt(dr, Shared.DISTRIBUTION_TYPE_COLUMN_NAME),
                    df,
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONSD_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONMIN_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONMAX_COLUMN_NAME));

                try
                {
                    this.m_DistributionProvider.Validate(
                        Item.DistributionTypeId,
                        Item.DistributionValue,
                        Item.DistributionSD,
                        Item.DistributionMin,
                        Item.DistributionMax);

                    this.m_IncubationPeriods.Add(Item);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ds.DisplayName + " -> " + ex.Message);
                }
            }
        }

        private void FillSymptomPeriodCollection(DateTime startingDateTime)
        {
            Debug.Assert(this.m_SymptomPeriods.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Shared.DATASHEET_SYMPTOM_PERIOD_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                DistributionFrequency? df = null;

                if (dr[Shared.DISTRIBUTION_FREQUENCY_COLUMN_NAME] != DBNull.Value)
                {
                    df = (DistributionFrequency)(long)dr[Shared.DISTRIBUTION_FREQUENCY_COLUMN_NAME];
                }

                SymptomPeriod Item = new SymptomPeriod(
                    Shared.GetNullableInt(dr, Shared.ITERATION_COLUMN_NAME),
                    this.TimestepFromDateTime(dr, startingDateTime, ds.DisplayName),
                    Shared.GetNullableInt(dr, Shared.JURISDICTION_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.VALUE_COLUMN_NAME),
                    Shared.GetNullableInt(dr, Shared.DISTRIBUTION_TYPE_COLUMN_NAME),
                    df,
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONSD_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONMIN_COLUMN_NAME),
                    Shared.GetNullableDouble(dr, Shared.DISTRIBUTIONMAX_COLUMN_NAME));

                try
                {
                    this.m_DistributionProvider.Validate(
                        Item.DistributionTypeId,
                        Item.DistributionValue,
                        Item.DistributionSD,
                        Item.DistributionMin,
                        Item.DistributionMax);

                    this.m_SymptomPeriods.Add(Item);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ds.DisplayName + " -> " + ex.Message);
                }
            }
        }
    }
}
