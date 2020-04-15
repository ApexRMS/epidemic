// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using SyncroSim.StochasticTime;

namespace SyncroSim.Epidemic
{
    class DistributionValueMap
    {
        private MultiLevelKeyMap2<SortedKeyMap2<DistributionValueCollection>> m_Map = 
            new MultiLevelKeyMap2<SortedKeyMap2<DistributionValueCollection>>();

        public void AddValue(EPDistributionValue value)
        {
            SortedKeyMap2<DistributionValueCollection> m = this.m_Map.GetItemExact(
                value.JurisdictionId, value.DistributionTypeId);

            if (m == null)
            {
                m = new SortedKeyMap2<DistributionValueCollection>(SearchMode.ExactPrevNext);
                this.m_Map.AddItem(value.JurisdictionId, value.DistributionTypeId, m);
            }

            DistributionValueCollection c = m.GetItemExact(value.Iteration, value.Timestep);

            if (c == null)
            {
                c = new DistributionValueCollection();
                m.AddItem(value.Iteration, value.Timestep, c);
            }

            c.Add(value);
        }

        public DistributionValueCollection GetValues(int distributionTypeId, int iteration, int timestep, int? jurisdictionId)
        {
            SortedKeyMap2<DistributionValueCollection> m = this.m_Map.GetItem(jurisdictionId, distributionTypeId);

            if (m == null)
            {
                return null;
            }

            DistributionValueCollection c = m.GetItem(iteration, timestep);

            if (c == null)
            {
                return null;
            }

            return c;
        }
    }
}
