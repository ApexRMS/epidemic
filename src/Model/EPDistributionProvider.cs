// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using SyncroSim.Core;
using SyncroSim.StochasticTime;

namespace SyncroSim.Epidemic
{
    sealed class EPDistributionProvider : DistributionProvider
    {
        private DistributionValueCollection m_DistributionValues = new DistributionValueCollection();
        private DistributionValueMap m_DistributionValueMap;

        public EPDistributionProvider(Scenario scenario, RandomGenerator randomGenerator) : base(scenario, randomGenerator)
        {
            this.FillDistributionValueCollection();
            this.CreateDistributionValueMap();
        }

        public DistributionValueCollection Values
        {
            get
            {
                return this.m_DistributionValues;
            }
        }

        public void EPInitializeDistributionValues()
        {
            foreach (EPDistributionValue Value in this.m_DistributionValues)
            {
                Value.Initialize(this);
            }
        }

        public double EPSample(int distributionTypeId, double? distributionMean, double? distributionSD, double? distributionMinimum, double? distributionMaximum, int iteration, int timestep, int? stratumId)
        {
            if (this.IsKnownDistributionTypeId(distributionTypeId))
            {
                return base.Sample(distributionTypeId, distributionMean, distributionSD, distributionMinimum, distributionMaximum, iteration, timestep);
            }
            else
            {
                return this.EPGetUserDistribution(distributionTypeId, iteration, timestep, stratumId);
            }
        }

        private void FillDistributionValueCollection()
        {
            Debug.Assert(this.m_DistributionValues.Count == 0);
            DataSheet ds = this.Scenario.GetDataSheet(Shared.DISTRIBUTION_VALUE_DATASHEET_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                try
                {
                    DistributionFrequency? ValueDistributionFrequency = null;

                    if (dr[Shared.DISTRIBUTION_VALUE_VALUE_DIST_FREQUENCY_COLUMN_NAME] != DBNull.Value)
                    {
                        ValueDistributionFrequency = (DistributionFrequency)(long)dr[Shared.DISTRIBUTION_VALUE_VALUE_DIST_FREQUENCY_COLUMN_NAME];
                    }

                    EPDistributionValue Item = new EPDistributionValue(
                        Shared.GetNullableInt(dr, Shared.ITERATION_COLUMN_NAME),
                        Shared.GetNullableInt(dr, Shared.TIMESTEP_COLUMN_NAME),
                        Shared.GetNullableInt(dr, Shared.JURISDICTION_COLUMN_NAME),
                        Convert.ToInt32(dr[Shared.DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture),
                        Shared.GetNullableInt(dr, Shared.DISTRIBUTION_VALUE_EXTVAR_TYPE_ID_COLUMN_NAME),
                        Shared.GetNullableDouble(dr, Shared.DISTRIBUTION_VALUE_EXTVAR_MIN_COLUMN_NAME),
                        Shared.GetNullableDouble(dr, Shared.DISTRIBUTION_VALUE_EXTVAR_MAX_COLUMN_NAME),
                        Shared.GetNullableDouble(dr, Shared.DISTRIBUTION_VALUE_DIST_VALUE_COLUMN_NAME),
                        Shared.GetNullableInt(dr, Shared.DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME), ValueDistributionFrequency,
                        Shared.GetNullableDouble(dr, Shared.DISTRIBUTION_VALUE_VALUE_DIST_SD_COLUMN_NAME),
                        Shared.GetNullableDouble(dr, Shared.DISTRIBUTION_VALUE_VALUE_DIST_MIN_COLUMN_NAME),
                        Shared.GetNullableDouble(dr, Shared.DISTRIBUTION_VALUE_VALUE_DIST_MAX_COLUMN_NAME),
                        Shared.GetNullableDouble(dr, Shared.DISTRIBUTION_VALUE_VALUE_DIST_RELATIVE_FREQUENCY_COLUMN_NAME));

                    this.Validate(Item.ValueDistributionTypeId, Item.Value, Item.ValueDistributionSD, Item.ValueDistributionMin, Item.ValueDistributionMax);
                    this.m_DistributionValues.Add(Item);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ds.DisplayName + " -> " + ex.Message);
                }
            }
        }

        private void CreateDistributionValueMap()
        {
            Debug.Assert(this.m_DistributionValueMap == null);
            this.m_DistributionValueMap = new DistributionValueMap();

            foreach (EPDistributionValue Value in this.m_DistributionValues)
            {
                this.m_DistributionValueMap.AddValue(Value);
            }
        }

        private double EPGetUserDistribution(int distributionTypeId, int iteration, int timestep, int? jurisdictionId)
        {
            const string SAMPLE_ERROR = "Attempted to sample from a distribution that has no corresponding distribution values in the scenario.  More information:" + "\r\n" + "Type={0}, Iteration={1}, Timestep={2}, Stratum={3}, SecondaryStratum={4}";

            DistributionValueCollection Values = this.m_DistributionValueMap.GetValues(distributionTypeId, iteration, timestep, jurisdictionId);

            if (Values == null)
            {
                this.ThrowNoValuesException(SAMPLE_ERROR, distributionTypeId, iteration, timestep, jurisdictionId);
            }

            return base.SampleUserDistribution(Values, distributionTypeId, iteration, timestep);
        }

        private string GetProjectItemName(string dataSheetName, int id)
        {
            DataSheet ds = this.Scenario.Project.GetDataSheet(dataSheetName);
            return ds.ValidationTable.GetDisplayName(id);
        }

        private void ThrowNoValuesException(string message, int distributionTypeId, int iteration, int timestep, int? jurisdictionId)
        {
            string JurisdictionName = "NULL";

            if (jurisdictionId.HasValue)
            {
                JurisdictionName = this.GetProjectItemName(
                    Shared.DATASHEET_JURISDICTION_NAME, 
                    jurisdictionId.Value);
            }

            Shared.ThrowEpidemicException(
                message, 
                this.GetProjectItemName(Shared.DISTRIBUTION_TYPE_DATASHEET_NAME, distributionTypeId), 
                iteration, 
                timestep, 
                JurisdictionName);
        }
    }
}
