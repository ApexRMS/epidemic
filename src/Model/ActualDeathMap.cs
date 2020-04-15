// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Diagnostics;

namespace SyncroSim.Epidemic
{
    class ActualDeathMap : MultiLevelKeyMap2<ActualDeath>
    {
        public ActualDeathMap(ActualDeathCollection actualDeaths)
        {
            foreach (ActualDeath Item in actualDeaths)
            {
                Debug.Assert(this.GetItemExact(
                    Item.JurisdictionId,
                    Item.Timestep) == null);

                this.AddItem(
                    Item.JurisdictionId,
                    Item.Timestep,
                    Item);                
            }
        }

        public ActualDeath GetActualDeath(int jurisdictionId, int timestep)
        {
            return this.GetItemExact(jurisdictionId, timestep);
        }
    }
}
