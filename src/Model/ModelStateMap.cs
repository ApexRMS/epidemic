// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using SyncroSim.Common;

namespace SyncroSim.Epidemic
{
    class ModelStateMap : MultiLevelKeyMap1<ModelState>
    {
        public void RecordState(ModelState state)
        {
            Debug.Assert(this.GetItemExact(state.Timestep) == null);
            this.AddItem(state.Timestep, state);
        }

        public ModelState GetState(int timestep)
        {
            return this.GetItemExact(timestep);
        }
    }
}
