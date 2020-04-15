// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.Epidemic
{
    class IncubationPeriodMap : MapBaseMK1SK2<IncubationPeriod>
    {
        public IncubationPeriodMap(Scenario scenario, IncubationPeriodCollection items) : base(scenario)
        {
            foreach (IncubationPeriod Item in items)
            {
                this.TryAddItem(Item);
            }
        }

        public IncubationPeriod GetIncubationPeriod(int jurisdictionId, int iteration, int timestep)
        {
            return base.GetItem(jurisdictionId, iteration, timestep);
        }

        public double GetIncubationPeriodValue(int jurisdictionId, int iteration, int timestep)
        {
            IncubationPeriod Item = this.GetIncubationPeriod(jurisdictionId, iteration, timestep);

            if (Item != null)
            {
                return Item.CurrentValue.Value;
            }
            else
            {
                return 5;
            }
        }

        private void TryAddItem(IncubationPeriod item)
        {
            try
            {
                this.AddItem(item.JurisdictionId, item.Iteration, item.Timestep, item);
            }
            catch (MapDuplicateItemException)
            {
                string template =
                    "A duplicate incubation period was detected: More information:" +
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
