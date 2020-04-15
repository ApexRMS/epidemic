// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Common;

namespace SyncroSim.Epidemic
{
    abstract class MapBaseMK2SK1<T> : MapBase
    {
        private MultiLevelKeyMap2<SortedKeyMap1<T>> m_map = new MultiLevelKeyMap2<SortedKeyMap1<T>>();

        protected MapBaseMK2SK1(Scenario scenario) : base(scenario)
        {
        }

        protected void AddItem(int? k1, int? k2, int? k3, T item)
        {
            SortedKeyMap1<T> m = this.m_map.GetItemExact(k1, k2);

            if (m == null)
            {
                m = new SortedKeyMap1<T>(SearchMode.ExactPrev);
                this.m_map.AddItem(k1, k2, m);
            }

            T v = m.GetItemExact(k3);

            if (v != null)
            {
                ThrowDuplicateItemException();
            }

            m.AddItem(k3, item);
            this.SetHasItems();
        }

        protected T GetItem(int? k1, int? k2, int? k3)
        {
            if (!this.HasItems)
            {
                return default(T);
            }

            SortedKeyMap1<T> p = this.m_map.GetItem(k1, k2);

            if (p == null)
            {
                return default(T);
            }

            return p.GetItem(k3);
        }
    }
}
