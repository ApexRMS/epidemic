// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.Epidemic
{
    class GrowthRateMultiplierMap : MapBaseMK1SK2<GrowthRateMultiplier>
    {
        public GrowthRateMultiplierMap(Scenario scenario, GrowthRateMultiplierCollection items) : base(scenario)
        {
            foreach (GrowthRateMultiplier Item in items)
            {
                this.TryAddItem(Item);
            }
        }

        public GrowthRateMultiplier GetGrowthRateMultiplier(int jurisdictionId, int iteration, int timestep)
        {
            return base.GetItem(jurisdictionId, iteration, timestep);
        }

        public double GetGrowthRateMultiplierValue(int jurisdictionId, int iteration, int timestep)
        {
            GrowthRateMultiplier Item = this.GetGrowthRateMultiplier(jurisdictionId, iteration, timestep);

            if (Item != null)
            {
                return Item.CurrentValue.Value;
            }
            else
            {
                return 1.0;
            }
        }

        private void TryAddItem(GrowthRateMultiplier item)
        {
            try
            {
                this.AddItem(item.JurisdictionId, item.Iteration, item.Timestep, item);
            }
            catch (MapDuplicateItemException)
            {
                string template =
                    "A duplicate growth rate multiplier was detected: More information:" +
                    Environment.NewLine +
                    "Jurisdiction={0}, Iteration={1}, Timestep={2}";

                Shared.ThrowEpidemicException(template,
                    this.GetJurisdictionName(item.JurisdictionId),
                    MapBase.FormatValue(item.Iteration),
                    MapBase.FormatValue(item.Timestep));
            }
        }
    }
}
