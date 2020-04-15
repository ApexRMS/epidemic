// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.Epidemic
{
    partial class Primary
    {
        private PopulationMap m_PopulationMap;
        private ActualDeathMap m_ActualDeathMap;
        private GrowthRateMap m_GrowthRateMap;
        private FatalityRateMap m_FatalityRateMap;
        private AttackRateMap m_AttackRateMap;
        private ModelTypeMap m_ModelTypeMap;
        private IncubationPeriodMap m_IncubationPeriodMap;
        private SymptomPeriodMap m_SymptomPeriodMap;

        private void CreateCollectionMaps()
        {
            this.m_PopulationMap = new PopulationMap(this.ResultScenario, this.m_Populations);
            this.m_ActualDeathMap = new ActualDeathMap(this.m_ActualDeaths);
            this.m_GrowthRateMap = new GrowthRateMap(this.ResultScenario, this.m_GrowthRates);
            this.m_FatalityRateMap = new FatalityRateMap(this.ResultScenario, this.m_FatalityRates);
            this.m_AttackRateMap = new AttackRateMap(this.ResultScenario, this.m_AttackRates);
            this.m_ModelTypeMap = new ModelTypeMap(this.ResultScenario, this.m_ModelTypes);
            this.m_IncubationPeriodMap = new IncubationPeriodMap(this.ResultScenario, this.m_IncubationPeriods);
            this.m_SymptomPeriodMap = new SymptomPeriodMap(this.ResultScenario, this.m_SymptomPeriods);
        }
    }
}
