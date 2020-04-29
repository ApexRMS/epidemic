// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using SyncroSim.Common;

namespace SyncroSim.Epidemic
{
    class ActualDeathMap : MapBase
    {
        MultiLevelKeyMap3<ActualDeath> m_Map = new MultiLevelKeyMap3<ActualDeath>();

        public ActualDeathMap(Scenario scenario, ActualDeathCollection items) : base(scenario)
        {
            foreach (ActualDeath Item in items)
            {
                this.TryAddItem(Item);
            }
        }

        public ActualDeath GetActualDeath(int jurisdictionId, int iteration, int timestep)
        {
            if (!this.HasItems)
            {
                return null;
            }
            else
            {
                return this.m_Map.GetItem(jurisdictionId, iteration, timestep);
            }
        }

        public double GetActualDeathValue(int jurisdictionId, int iteration, int timestep)
        {
            ActualDeath Item = this.GetActualDeath(jurisdictionId, iteration, timestep);

            if (Item != null)
            {
                return Item.CurrentValue.Value;
            }
            else
            {
                return 1.0;
            }
        }

        private void TryAddItem(ActualDeath item)
        {
            if (this.m_Map.GetItemExact(item.JurisdictionId, item.Iteration, item.Timestep) != null)
            {
                string template =
                    "A duplicate death was detected: More information:" +
                    Environment.NewLine +
                    "Jurisdiction={0}, Iteration={1}, Timestep={2}";

                Shared.ThrowEpidemicException(template,
                    this.GetJurisdictionName(item.JurisdictionId),
                    MapBase.FormatValue(item.Iteration),
                    MapBase.FormatValue(item.Timestep));
            }

            this.m_Map.AddItem(item.JurisdictionId, item.Iteration, item.Timestep, item);
            this.SetHasItems();
        }
    }
}
