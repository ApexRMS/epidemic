// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.Epidemic
{
    class AttackRate : EPDistributionBase
    {
        public AttackRate(
            int? iteration,
            int? timestep,
            int? jurisdictionId,
            double? value,
            int? distributionTypeId,
            DistributionFrequency? distributionFrequency,
            double? distributionSD,
            double? distributionMin,
            double? distributionMax) :
                base(iteration, timestep, jurisdictionId, value, distributionTypeId,
                    distributionFrequency, distributionSD, distributionMin, distributionMax)
        {
        }

        public override EPDistributionBase Clone()
        {
            return new AttackRate(
                this.Iteration,
                this.Timestep,
                this.JurisdictionId,
                this.DistributionValue,
                this.DistributionTypeId,
                this.DistributionFrequency,
                this.DistributionSD,
                this.DistributionMin,
                this.DistributionMax);
        }
    }
}
