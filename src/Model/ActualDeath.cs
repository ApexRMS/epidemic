// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.Epidemic
{
    class ActualDeath
    {
        private int m_Timestep;
        private int m_JurisdictionId;
        private double m_Value;

        public ActualDeath(
            int timestep,
            int jurisdictionId,
            double value)
        {
            this.m_Timestep = timestep;
            this.m_JurisdictionId = jurisdictionId;
            this.m_Value = value;
        }

        public int Timestep
        {
            get
            {
                return this.m_Timestep;
            }
        }

        public int JurisdictionId
        {
            get
            {
                return this.m_JurisdictionId;
            }
        }

        public double Value
        {
            get
            {
                return this.m_Value;
            }
        }
    }
}
