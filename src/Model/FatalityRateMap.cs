// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.Epidemic
{
    class FatalityRateMap : MapBaseMK1SK2<FatalityRate>
    {
        public FatalityRateMap(Scenario scenario, FatalityRateCollection items) : base(scenario)
        {
            foreach (FatalityRate Item in items)
            {
                this.TryAddItem(Item);
            }
        }

        public FatalityRate GetFatalityRate(int jurisdictionId, int iteration, int timestep)
        {
            return base.GetItem(jurisdictionId, iteration, timestep);
        }

        public double GetFatalityRateValue(int jurisdictionId, int iteration, int timestep)
        {
            FatalityRate Item = this.GetFatalityRate(jurisdictionId, iteration, timestep);

            if (Item != null)
            {
                return Item.CurrentValue.Value;
            }
            else
            {
                string template =
                    "A fatality rate was not found for:" +
                    Environment.NewLine +
                    "Jurisdiction={0}, Iteration={1}, Timestep={2}";

                Shared.ThrowEpidemicException(template,
                    this.GetJurisdictionName(jurisdictionId),
                    MapBase.FormatValue(iteration),
                    MapBase.FormatValue(timestep));

                return 0.0;
            }
        }

        private void TryAddItem(FatalityRate item)
        {
            try
            {
                this.AddItem(item.JurisdictionId, item.Iteration, item.Timestep, item);
            }
            catch (MapDuplicateItemException)
            {
                string template =
                    "A duplicate fatality rate was detected: More information:" +
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
