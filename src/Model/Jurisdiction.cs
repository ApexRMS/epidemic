// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.Epidemic
{
    class Jurisdiction
    {
        private int m_id;
        private string m_Name;

        public Jurisdiction(int id, string name)
        {
            this.m_id = id;
            this.m_Name = name;
        }

        public int Id
        {
            get
            {
                return this.m_id;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
        }

        public override string ToString()
        {
            return this.m_Name;
        }
    }
}
