// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Common;

namespace SyncroSim.Epidemic
{
    abstract class MapBaseMK1SK2<T> : MapBase
    {
        private MultiLevelKeyMap1<SortedKeyMap2<T>> m_map = new MultiLevelKeyMap1<SortedKeyMap2<T>>();

        protected MapBaseMK1SK2(Scenario scenario) : base(scenario)
        {
        }

        protected void AddItem(int? k1, int? k2, int? k3, T item)
        {
            SortedKeyMap2<T> m = this.m_map.GetItemExact(k1);

            if (m == null)
            {
                m = new SortedKeyMap2<T>(SearchMode.ExactPrev);
                this.m_map.AddItem(k1, m);
            }

            T v = m.GetItemExact(k2, k3);

            if (v != null)
            {
                ThrowDuplicateItemException();
            }

            m.AddItem(k2, k3, item);
            this.SetHasItems();
        }

        protected T GetItem(int? k1, int? k2, int? k3)
        {
            if (!this.HasItems)
            {
                return default(T);
            }

            SortedKeyMap2<T> p = this.m_map.GetItem(k1);

            if (p == null)
            {
                return default(T);
            }

            return p.GetItem(k2, k3);
        }
    }
}
