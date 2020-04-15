// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Common;

namespace SyncroSim.Epidemic
{
    abstract class MapBaseMK1SK1<T> : MapBase
    {
        private MultiLevelKeyMap1<SortedKeyMap1<T>> m_map = new MultiLevelKeyMap1<SortedKeyMap1<T>>();

        protected MapBaseMK1SK1(Scenario scenario) : base(scenario)
        {
        }

        protected void AddItem(int? k1, int? k2, T item)
        {
            SortedKeyMap1<T> m = this.m_map.GetItemExact(k1);

            if (m == null)
            {
                m = new SortedKeyMap1<T>(SearchMode.ExactPrev);
                this.m_map.AddItem(k1, m);
            }

            T v = m.GetItemExact(k2);

            if (v != null)
            {
                ThrowDuplicateItemException();
            }

            m.AddItem(k2, item);
            this.SetHasItems();
        }

        protected T GetItem(int? k1, int? k2)
        {
            if (!this.HasItems)
            {
                return default(T);
            }

            SortedKeyMap1<T> p = this.m_map.GetItem(k1);

            if (p == null)
            {
                return default(T);
            }

            return p.GetItem(k2);
        }
    }
}
