// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.Epidemic
{
    partial class Primary : StochasticTimeTransformer
    {
        private bool m_ModelHistoricalDeaths;

        public override void Configure()
        {
            base.Configure();
            this.ConfigureRunControl();
        }

        public override void Initialize()
        {
            base.Initialize();

            this.InitializeRunControl();
            this.CreateDistributionProvider();
            this.FillModelCollections();
            this.ExpandForUserDistributions();
            this.InitializeDistributionValues();
            this.CreateCollectionMaps();
            this.InitializeOutputTables();
        }

        public override void Transform()
        {
            try
            {
                JurisdictionCollection Jurisdictions = this.GetRuntimeJurisdictions();

                int TotalIterations = (this.MaximumIteration - this.MinimumIteration + 1);
                int TotalTimesteps = (this.MaximumTimestep - this.MinimumTimestep + 1);
                int TotalTasks = (TotalIterations * TotalTimesteps * Jurisdictions.Count);

                this.BeginProgress(TotalTasks);
                this.Simulate();
            }
            finally
            {
                this.SetStatusMessage(null);
                this.CompleteProgress();
            }
        }
    }
}
