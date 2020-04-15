// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.Epidemic
{
    class AttackRateMap : MapBaseMK1SK2<AttackRate>
    {
        public AttackRateMap(Scenario scenario, AttackRateCollection items) : base(scenario)
        {
            foreach (AttackRate Item in items)
            {
                this.TryAddItem(Item);
            }
        }

        public AttackRate GetAttackRate(int jurisdictionId, int iteration, int timestep)
        {
            return base.GetItem(jurisdictionId, iteration, timestep);
        }

        public double GetAttackRateValue(int jurisdictionId, int iteration, int timestep)
        {
            AttackRate Item = this.GetAttackRate(jurisdictionId, iteration, timestep);

            if (Item != null)
            {
                return Item.CurrentValue.Value;
            }
            else
            {
                return 1.0;
            }
        }

        private void TryAddItem(AttackRate item)
        {
            try
            {
                this.AddItem(item.JurisdictionId, item.Iteration, item.Timestep, item);
            }
            catch (MapDuplicateItemException)
            {
                string template =
                    "A duplicate attack rate was detected: More information:" +
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
