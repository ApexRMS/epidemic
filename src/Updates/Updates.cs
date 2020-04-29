// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.Epidemic
{
    class Updates : UpdateProvider
    {
        public override void PerformUpdate(DataStore store, int currentSchemaVersion)
        {
            if (currentSchemaVersion < 1)
            {
                EPI00001(store);
            }
        }

        private void EPI00001(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE epidemic_ActualDeath RENAME TO TEMP_TABLE");

            store.ExecuteNonQuery(@"CREATE TABLE epidemic_ActualDeath ( 
                ActualDeathID           INTEGER PRIMARY KEY AUTOINCREMENT,
                ScenarioID              INTEGER,
                Iteration               INTEGER,
                Timestep                DATE,
                Jurisdiction            INTEGER,
                Value                   DOUBLE,
                DistributionType        INTEGER,
                DistributionFrequencyID INTEGER,
                DistributionSD          DOUBLE,
                DistributionMin         DOUBLE,
                DistributionMax         DOUBLE)");

            store.ExecuteNonQuery(
                @"INSERT INTO epidemic_ActualDeath(ScenarioID, Timestep, Jurisdiction, Value) 
                SELECT ScenarioID, Timestep, Jurisdiction, Value FROM TEMP_TABLE");

            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
        }
    }
}
