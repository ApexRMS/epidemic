// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.Epidemic
{
    class PopulationMap : MapBaseMK1SK1<Population>
    {
        public PopulationMap(Scenario scenario, PopulationCollection items) : base(scenario)
        {
            foreach (Population Item in items)
            {
                this.TryAddItem(Item);
            }
        }

        public Population GetPopulation(int jurisdictionId, int iteration)
        {
            return base.GetItem(jurisdictionId, iteration);
        }

        private void TryAddItem(Population item)
        {
            try
            {
                this.AddItem(item.JurisdictionId, item.Iteration, item);
            }
            catch (MapDuplicateItemException)
            {
                string template =
                    "A duplicate population was detected: More information:" +
                    Environment.NewLine +
                    "Jurisdiction={0}, Iteration={1}";

                Shared.ThrowEpidemicException(template,
                    this.GetJurisdictionName(item.JurisdictionId),
                    MapBase.FormatValue(item.Iteration));
            }
        }
    }
}
