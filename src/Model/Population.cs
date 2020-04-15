// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.Epidemic
{
    class Population
    {
        private int? m_Iteration;
        private int m_JurisdictionId;
        private double m_TotalSize;

        public Population(
            int? iteration,
            int jurisdictionId,
            double totalSize)
        {
            this.m_Iteration = iteration;
            this.m_JurisdictionId = jurisdictionId;
            this.m_TotalSize = totalSize;
        }

        public int? Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        public int JurisdictionId
        {
            get
            {
                return this.m_JurisdictionId;
            }
        }

        public double TotalSize
        {
            get
            {
                return this.m_TotalSize;
            }
        }
    }
}
