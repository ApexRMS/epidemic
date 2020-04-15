// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Globalization;
using SyncroSim.Core;

namespace SyncroSim.Epidemic
{
    abstract class MapBase
    {
        private Scenario m_Scenario;
        private string m_JurisdictionLabel;
        private bool m_HasItems;

        protected MapBase(Scenario scenario)
        {
            this.m_Scenario = scenario;
            DataSheet ds = scenario.Project.GetDataSheet(Shared.DATASHEET_TERMINOLOGY_NAME);
            this.m_JurisdictionLabel = Shared.GetJurisdictionLabel(ds);
        }

        protected string JurisdictionLabel
        {
            get
            {
                return this.m_JurisdictionLabel;
            }
        }

        protected void SetHasItems()
        {
            this.m_HasItems = true;
        }

        public bool HasItems
        {
            get
            {
                return this.m_HasItems;
            }
        }

        protected static void ThrowDuplicateItemException()
        {
            throw new MapDuplicateItemException(
                "An item with the same keys has already been added.");
        }

        protected static string FormatValue(int? value)
        {
            if (!value.HasValue)
            {
                return "NULL";
            }
            else
            {
                return Convert.ToString(value, CultureInfo.InvariantCulture);
            }
        }

        protected string GetJurisdictionName(int? jurisdictionId)
        {
            return this.GetProjectItemName(
                Shared.DATASHEET_JURISDICTION_NAME, 
                jurisdictionId);
        }

        protected string GetProjectItemName(string dataSheetName, int? id)
        {
            if (!id.HasValue)
            {
                return "NULL";
            }
            else
            {
                DataSheet ds = this.m_Scenario.Project.GetDataSheet(dataSheetName);
                return ds.ValidationTable.GetDisplayName(id.Value);
            }
        }
    }
}
