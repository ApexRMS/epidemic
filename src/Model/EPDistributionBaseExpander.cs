// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Common;

namespace SyncroSim.Epidemic
{
    class EPDistributionBaseExpander
    {
        private EPDistributionProvider m_Provider;
        private MultiLevelKeyMap1<Dictionary<string, EPDistributionValue>> m_1 = new MultiLevelKeyMap1<Dictionary<string, EPDistributionValue>>();
        private MultiLevelKeyMap2<Dictionary<string, EPDistributionValue>> m_2 = new MultiLevelKeyMap2<Dictionary<string, EPDistributionValue>>();

        public EPDistributionBaseExpander(EPDistributionProvider provider)
        {
            this.m_Provider = provider;
            this.FillUserDistributionMaps();
        }

        public IEnumerable<EPDistributionBase> Expand(IEnumerable<EPDistributionBase> items)
        {
            return this.InternalExpand(items);
        }

        private IEnumerable<EPDistributionBase> InternalExpand(IEnumerable<EPDistributionBase> items)
        {
            Debug.Assert(items.Count() > 0);
            Debug.Assert(this.m_Provider.Values.Count > 0);

            if (this.m_Provider.Values.Count == 0 || items.Count() == 0)
            {
                return items;
            }

            List<EPDistributionBase> Expanded = new List<EPDistributionBase>();

            foreach (EPDistributionBase t in items)
            {
                if (!ExpansionRequired(t))
                {
                    Expanded.Add(t);
                    continue;
                }

                Dictionary<string, EPDistributionValue> l = this.GetValueDictionary(t);

                if (l == null)
                {
                    Expanded.Add(t);
                    continue;
                }

                foreach (EPDistributionValue v in l.Values)
                {
                    EPDistributionBase n = t.Clone();
                    n.JurisdictionId = v.JurisdictionId;

                    Expanded.Add(n);
                }
            }

            Debug.Assert(Expanded.Count() >= items.Count());
            return Expanded;
        }

        private static string CreateDistBaseKey(int? jurisdictionId)
        {
            string s1 = "NULL";

            if (jurisdictionId.HasValue)
            {
                s1 = jurisdictionId.Value.ToString(CultureInfo.InvariantCulture);
            }

            return s1;
        }

        private static bool ExpansionRequired(EPDistributionBase t)
        {
            if (!t.DistributionTypeId.HasValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private Dictionary<string, EPDistributionValue> GetValueDictionary(EPDistributionBase t)
        {
            Dictionary<string, EPDistributionValue> l = null;

            if (!t.JurisdictionId.HasValue)
            {
                l = this.m_1.GetItemExact(t.DistributionTypeId);
            }
            else
            {
                l = this.m_2.GetItemExact(t.DistributionTypeId, t.JurisdictionId.Value);
            }

#if DEBUG
            if (l != null)
            {
                Debug.Assert(l.Count > 0);
            }
#endif

            return l;
        }

        private void FillUserDistributionMaps()
        {
            foreach (EPDistributionValue v in this.m_Provider.Values)
            {
                Dictionary<string, EPDistributionValue> d = this.m_1.GetItemExact(v.DistributionTypeId);

                if (d == null)
                {
                    d = new Dictionary<string, EPDistributionValue>();
                    this.m_1.AddItem(v.DistributionTypeId, d);
                }

                string k = CreateDistBaseKey(v.JurisdictionId);

                if (!d.ContainsKey(k))
                {
                    d.Add(k, v);
                }
            }

            foreach (EPDistributionValue v in this.m_Provider.Values)
            {
                if (v.JurisdictionId.HasValue)
                {
                    Dictionary<string, EPDistributionValue> d = this.m_2.GetItemExact(v.DistributionTypeId, v.JurisdictionId.Value);

                    if (d == null)
                    {
                        d = new Dictionary<string, EPDistributionValue>();
                        this.m_2.AddItem(v.DistributionTypeId, v.JurisdictionId.Value, d);
                    }

                    string k = CreateDistBaseKey(v.JurisdictionId);

                    if (!d.ContainsKey(k))
                    {
                        d.Add(k, v);
                    }
                }
            }
        }
    }
}
