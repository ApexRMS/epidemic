// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.Epidemic
{
    class ModelState
    {
        private int m_JurisdictionId;
        private int m_Iteration;
        private int m_Timestep;
        private double m_GrowthRate;
        private double m_FatalityRate;
        private double m_AttackRate;
        private double m_IncubationPeriod;
        private double m_SymptomPeriod;
        private double m_InfectedPeriod;
        private double m_Infected;
        private double m_Deaths;
        private double m_CumulativeInfected;
        private double m_CumulativeDeaths;

        public ModelState(
            int jurisdictionId, 
            int iteration, 
            int timestep,
            double growthRate,
            double fatalityRate,
            double attackRate,
            double incubationPeriod,
            double symptomPeriod,
            double infectedPeriod)
        {
            this.m_JurisdictionId = jurisdictionId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_GrowthRate = growthRate;
            this.m_FatalityRate = fatalityRate;
            this.m_AttackRate = attackRate;
            this.m_IncubationPeriod = incubationPeriod;
            this.m_SymptomPeriod = symptomPeriod;
            this.m_InfectedPeriod = infectedPeriod;
        }

        public int JurisdictionId
        {
            get
            {
                return this.m_JurisdictionId;
            }
        }

        public int Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        public int Timestep
        {
            get
            {
                return this.m_Timestep;
            }
        }

        public double GrowthRate
        {
            get
            {
                return m_GrowthRate;
            }
        }

        public double FatalityRate
        {
            get
            {
                return m_FatalityRate;
            }
        }

        public double AttackRate
        {
            get
            {
                return m_AttackRate;
            }
        }

        public double IncubationPeriod
        {
            get
            {
                return m_IncubationPeriod;
            }
        }

        public double SymptomPeriod
        {
            get
            {
                return m_SymptomPeriod;
            }
        }

        public double InfectedPeriod
        {
            get
            {
                return m_InfectedPeriod;
            }
        }

        public double Infected
        {
            get
            {
                return this.m_Infected;
            }
            set
            {
                this.m_Infected = value;
            }
        }

        public double Deaths
        {
            get
            {
                return this.m_Deaths;
            }
            set
            {
                this.m_Deaths = value;
            }
        }

        public double CumulativeInfected
        {
            get
            {
                return m_CumulativeInfected;
            }
            set
            {
                this.m_CumulativeInfected = value;
            }
        }

        public double CumulativeDeaths
        {
            get
            {
                return m_CumulativeDeaths;
            }
            set
            {
                this.m_CumulativeDeaths = value;
            }
        }
    }
}

