// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Windows.Forms;
using SyncroSim.Core;
using SyncroSim.Core.Forms;

namespace SyncroSim.Epidemic
{
    partial class RunControlDataFeedView : DataFeedView
    {
        private bool m_IsLoaded;

        public RunControlDataFeedView()
        {
            InitializeComponent();
        }

        protected override void InitializeView()
        {
            base.InitializeView();

            DataFeedView v = this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario);
            this.PanelJurisdictions.Controls.Add(v);
        }

        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetTextBoxBinding(
                this.TextBoxMaxIterations, 
                Shared.DATASHEET_RUN_CONTROL_NAME,
                Shared.DATASHEET_RUN_CONTROL_MAX_ITERATION_COLUMN_NAME);

            MultiRowDataFeedView v = (MultiRowDataFeedView)this.PanelJurisdictions.Controls[0];
            v.LoadDataFeed(dataFeed, Shared.DATASHEET_RUNTIME_JURISDICTION_NAME);

            this.RefreshDateTimeControls();
            this.m_IsLoaded = true;
        }

        public override void EnableView(bool enable)
        {
            base.EnableView(enable);

            MultiRowDataFeedView v = (MultiRowDataFeedView)this.PanelJurisdictions.Controls[0];
            v.GridControl.IsReadOnly = (!enable);
        }

        protected override void OnBoundTextBoxValidated(TextBox textBox, string columnName, string newValue)
        {
            base.OnBoundTextBoxValidated(textBox, columnName, newValue);
            this.SaveTimestepData();
        }

        private void RefreshDateTimeControls()
        {
            DataSheet ds = this.Scenario.GetDataSheet(Shared.DATASHEET_RUN_CONTROL_NAME);
            DataRow dr = ds.GetDataRow();

            if (dr != null && dr[Shared.DATASHEET_RUN_CONTROL_START_DATE_COLUMN_NAME] != DBNull.Value)
            {
                this.DateTimeStart.Value = (DateTime)dr[Shared.DATASHEET_RUN_CONTROL_START_DATE_COLUMN_NAME];
            }

            if (dr != null && dr[Shared.DATASHEET_RUN_CONTROL_END_DATE_COLUMN_NAME] != DBNull.Value)
            {
                this.DateTimeEnd.Value = (DateTime)dr[Shared.DATASHEET_RUN_CONTROL_END_DATE_COLUMN_NAME];
            }
        }

        private void SaveTimestepData()
        {
            DataSheet ds = this.Scenario.GetDataSheet(Shared.DATASHEET_RUN_CONTROL_NAME);

            ds.SetSingleRowData(Shared.DATASHEET_RUN_CONTROL_START_DATE_COLUMN_NAME, this.DateTimeStart.Value);
            ds.SetSingleRowData(Shared.DATASHEET_RUN_CONTROL_END_DATE_COLUMN_NAME, this.DateTimeEnd.Value);
        }

        private void DateTimeStart_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.m_IsLoaded)
            {
                this.SaveTimestepData();
            }
        }

        private void DateTimeEnd_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.m_IsLoaded)
            {
                this.SaveTimestepData();
            }
        }
    }
}
