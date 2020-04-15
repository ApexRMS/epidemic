// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Diagnostics;
using SyncroSim.StochasticTime;

namespace SyncroSim.Epidemic
{
    abstract class EPDistributionBase
    {
        private int? m_Iteration;
        private int? m_Timestep;
        private int? m_JurisdictionId;
        private double? m_DistributionValue;
        private int? m_DistributionTypeId;
        private DistributionFrequency m_DistributionFrequency = DistributionFrequency.Timestep;
        private double? m_DistributionSD;
        private double? m_DistributionMin;
        private double? m_DistributionMax;
        private double? m_CurrentValue;
        private bool m_IsDisabled;

        protected EPDistributionBase
            (int? iteration, int? timestep, int? jurisdictionId,
            double? distributionValue, int? distributionTypeId, DistributionFrequency? distributionFrequency,
            double? distributionSD, double? distributionMin, double? distributionMax)
        {
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_JurisdictionId = jurisdictionId;
            this.m_DistributionValue = distributionValue;
            this.m_DistributionTypeId = distributionTypeId;
            this.m_DistributionSD = distributionSD;
            this.m_DistributionMin = distributionMin;
            this.m_DistributionMax = distributionMax;

            if (distributionFrequency.HasValue)
            {
                this.m_DistributionFrequency = distributionFrequency.Value;
            }
        }

        public int? Iteration
        {
            get
            {
                return this.m_Iteration;
            }
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
            set
            {
                this.m_JurisdictionId = value;
            }
        }

        public double? DistributionValue
        {
            get
            {
                return this.m_DistributionValue;
            }
        }

        public int? DistributionTypeId
        {
            get
            {
                return this.m_DistributionTypeId;
            }
        }

        public DistributionFrequency DistributionFrequency
        {
            get
            {
                return this.m_DistributionFrequency;
            }
        }

        public double? DistributionSD
        {
            get
            {
                return this.m_DistributionSD;
            }
        }

        public double? DistributionMin
        {
            get
            {
                return this.m_DistributionMin;
            }
        }

        public double? DistributionMax
        {
            get
            {
                return this.m_DistributionMax;
            }
        }

        public double? CurrentValue
        {
            get
            {
                this.CheckDisabled();
                return this.m_CurrentValue;
            }
        }

        public bool IsDisabled
        {
            get
            {
                return m_IsDisabled;
            }

            set
            {
                m_IsDisabled = value;
            }
        }

        public void Initialize(int iteration, int timestep, EPDistributionProvider provider)
        {
            this.InternalInitialize(iteration, timestep, provider);
        }

        public double Sample(int iteration, int timestep, EPDistributionProvider provider, DistributionFrequency frequency)
        {
            return this.InternalSample(iteration, timestep, provider, frequency);
        }

        public abstract EPDistributionBase Clone();

        private void InternalInitialize(int iteration, int timestep, EPDistributionProvider provider)
        {
            this.CheckDisabled();

            if (this.m_DistributionTypeId.HasValue)
            {
                int IterationToUse = iteration;
                int TimestepToUse = timestep;

                if (this.m_Iteration.HasValue)
                {
                    IterationToUse = this.m_Iteration.Value;
                }

                if (this.m_Timestep.HasValue)
                {
                    TimestepToUse = this.m_Timestep.Value;
                }

                this.m_CurrentValue = provider.EPSample(
                    this.m_DistributionTypeId.Value, this.m_DistributionValue, 
                    this.m_DistributionSD, this.m_DistributionMin, this.m_DistributionMax, 
                    IterationToUse, TimestepToUse, this.m_JurisdictionId);
            }
            else
            {
                Debug.Assert(this.m_DistributionValue.HasValue);
                this.m_CurrentValue = this.m_DistributionValue.Value;
            }

            Debug.Assert(this.m_CurrentValue.HasValue);
        }

        private double InternalSample(int iteration, int timestep, EPDistributionProvider provider, DistributionFrequency frequency)
        {
            this.CheckDisabled();

            if (this.m_DistributionTypeId.HasValue)
            {
                if (this.m_DistributionFrequency == frequency || this.m_DistributionFrequency == DistributionFrequency.Always)
                {
                    this.m_CurrentValue = provider.EPSample(
                        this.m_DistributionTypeId.Value, this.m_DistributionValue, 
                        this.m_DistributionSD, this.m_DistributionMin, this.m_DistributionMax, 
                        iteration, timestep, this.m_JurisdictionId);
                }
            }

            Debug.Assert(this.m_CurrentValue.HasValue);
            return this.m_CurrentValue.Value;
        }

        protected void CheckDisabled()
        {
            if (this.m_IsDisabled)
            {
                throw new InvalidOperationException("The item is disabled.");
            }
        }
    }
}
