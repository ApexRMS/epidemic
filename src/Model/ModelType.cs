// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.Epidemic
{
    class ModelType
    {
        private int? m_Timestep;
        private int? m_JurisdictionId;
        private EpidemicModelType m_EpidemicModel = EpidemicModelType.Logistic;

        public ModelType(
            int? timestep,
            int? jurisdictionId,
            EpidemicModelType modelType)
        {
            this.m_Timestep = timestep;
            this.m_JurisdictionId = jurisdictionId;
            this.m_EpidemicModel = modelType;
        }

        public int? Timestep
        {
            get
            {
                return this.m_Timestep;
            }
        }

        public int? JurisdictionId
        {
            get
            {
                return this.m_JurisdictionId;
            }
        }

        internal EpidemicModelType EpidemicModel
        {
            get
            {
                return m_EpidemicModel;
            }
        }
    }
}
