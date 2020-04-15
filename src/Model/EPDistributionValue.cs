// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.Epidemic
{
    class EPDistributionValue : DistributionValue
    {
        private int? m_JurisdictionId;

        public EPDistributionValue(
            int? iteration, int? timestep, int? jurisdictionId, int distributionTypeId,
            int? externalVariableTypeId, double? externalVariableMin, double? externalVariableMax, double? value,
            int? valueDistributionTypeId, DistributionFrequency? valueDistributionFrequency, double? valueDistributionSD,
            double? valueDistributionMin, double? valueDistributionMax, double? valueDistributionRelativeFrequency) : 
            base(iteration, timestep, distributionTypeId, externalVariableTypeId, externalVariableMin, 
                externalVariableMax, value, valueDistributionTypeId, valueDistributionFrequency, valueDistributionSD, 
                valueDistributionMin, valueDistributionMax, valueDistributionRelativeFrequency)
        {
            this.m_JurisdictionId = jurisdictionId;
        }

        public int? JurisdictionId
        {
            get
            {
                return this.m_JurisdictionId;
            }
        }
    }
}
