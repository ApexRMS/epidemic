// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using SyncroSim.Common;

namespace SyncroSim.Epidemic
{
    class PopulationMap : MapBase
    {
        private MultiLevelKeyMap1<Population> m_Map = new MultiLevelKeyMap1<Population>();

        public PopulationMap(Scenario scenario, PopulationCollection items) : base(scenario)
        {
            foreach (Population Item in items)
            {
                this.TryAddItem(Item);
            }
        }

        public Population GetPopulation(int jurisdictionId)
        {
            if (!this.HasItems)
            {
                return null;
            }
            else
            {
                return this.m_Map.GetItem(jurisdictionId);
            }
        }

        private void TryAddItem(Population item)
        {
            if (this.m_Map.GetItemExact(item.JurisdictionId) != null)
            {
                string template =
                    "A duplicate population was detected: More information:" +
                    Environment.NewLine +
                    "Jurisdiction={0}";

                Shared.ThrowEpidemicException(template,
                    this.GetJurisdictionName(item.JurisdictionId));
            }

            this.m_Map.AddItem(item.JurisdictionId, item);
            this.SetHasItems();
        }
    }
}
