// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.Epidemic
{
    class SymptomPeriodMap : MapBaseMK1SK2<SymptomPeriod>
    {
        public SymptomPeriodMap(Scenario scenario, SymptomPeriodCollection items) : base(scenario)
        {
            foreach (SymptomPeriod Item in items)
            {
                this.TryAddItem(Item);
            }
        }

        public SymptomPeriod GetSymptomPeriod(int jurisdictionId, int iteration, int timestep)
        {
            return base.GetItem(jurisdictionId, iteration, timestep);
        }

        public double GetSymptomPeriodValue(int jurisdictionId, int iteration, int timestep)
        {
            SymptomPeriod Item = this.GetSymptomPeriod(jurisdictionId, iteration, timestep);

            if (Item != null)
            {
                return Item.CurrentValue.Value;
            }
            else
            {
                return 17;
            }
        }

        private void TryAddItem(SymptomPeriod item)
        {
            try
            {
                this.AddItem(item.JurisdictionId, item.Iteration, item.Timestep, item);
            }
            catch (MapDuplicateItemException)
            {
                string template =
                    "A duplicate symptom period was detected: More information:" +
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
