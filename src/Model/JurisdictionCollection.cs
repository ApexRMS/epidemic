﻿// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.Epidemic
{
    class JurisdictionCollection : KeyedCollection<int, Jurisdiction>
    {
        protected override int GetKeyForItem(Jurisdiction item)
        {
            return item.Id;
        }
    }
}