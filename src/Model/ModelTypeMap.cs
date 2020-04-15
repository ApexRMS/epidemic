// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.Epidemic
{
    class ModelTypeMap : MapBaseMK1SK1<ModelType>
    {
        public ModelTypeMap(Scenario scenario, ModelTypeCollection items) : base(scenario)
        {
            foreach (ModelType Item in items)
            {
                this.TryAddItem(Item);
            }
        }

        public ModelType GetModelType(int jurisdictionId, int timestep)
        {
            return base.GetItem(jurisdictionId, timestep);
        }

        private void TryAddItem(ModelType item)
        {
            try
            {
                this.AddItem(item.JurisdictionId, item.Timestep, item);
            }
            catch (MapDuplicateItemException)
            {
                string template =
                    "A duplicate model type was detected: More information:" +
                    Environment.NewLine +
                    "Jurisdiction={0}, Timestep={1}";

                Shared.ThrowEpidemicException(template,
                    this.GetJurisdictionName(item.JurisdictionId),
                    MapBase.FormatValue(item.Timestep));
            }
        }
    }
}
