﻿namespace SyncroSim.Epidemic
{
    partial class RunControlDataFeedView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.DateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBoxMaxIterations = new System.Windows.Forms.TextBox();
            this.PanelJurisdictions = new SyncroSim.Core.Forms.BasePanel();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DateTimeStart
            // 
            this.DateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimeStart.Location = new System.Drawing.Point(110, 21);
            this.DateTimeStart.Name = "DateTimeStart";
            this.DateTimeStart.Size = new System.Drawing.Size(116, 22);
            this.DateTimeStart.TabIndex = 1;
            this.DateTimeStart.ValueChanged += new System.EventHandler(this.DateTimeStart_ValueChanged);
            // 
            // DateTimeEnd
            // 
            this.DateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimeEnd.Location = new System.Drawing.Point(110, 53);
            this.DateTimeEnd.Name = "DateTimeEnd";
            this.DateTimeEnd.Size = new System.Drawing.Size(116, 22);
            this.DateTimeEnd.TabIndex = 3;
            this.DateTimeEnd.ValueChanged += new System.EventHandler(this.DateTimeEnd_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "End date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Realizations:";
            // 
            // TextBoxMaxIterations
            // 
            this.TextBoxMaxIterations.Location = new System.Drawing.Point(110, 85);
            this.TextBoxMaxIterations.Name = "TextBoxMaxIterations";
            this.TextBoxMaxIterations.Size = new System.Drawing.Size(116, 22);
            this.TextBoxMaxIterations.TabIndex = 5;
            // 
            // PanelJurisdictions
            // 
            this.PanelJurisdictions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelJurisdictions.BorderColor = System.Drawing.Color.Gray;
            this.PanelJurisdictions.Location = new System.Drawing.Point(110, 117);
            this.PanelJurisdictions.Name = "PanelJurisdictions";
            this.PanelJurisdictions.PaintBottomBorder = true;
            this.PanelJurisdictions.PaintLeftBorder = true;
            this.PanelJurisdictions.PaintRightBorder = true;
            this.PanelJurisdictions.PaintTopBorder = true;
            this.PanelJurisdictions.ShowBorder = true;
            this.PanelJurisdictions.Size = new System.Drawing.Size(699, 365);
            this.PanelJurisdictions.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Jurisdictions:";
            // 
            // RunControlDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PanelJurisdictions);
            this.Controls.Add(this.TextBoxMaxIterations);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DateTimeEnd);
            this.Controls.Add(this.DateTimeStart);
            this.Name = "RunControlDataFeedView";
            this.Size = new System.Drawing.Size(812, 485);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DateTimeStart;
        private System.Windows.Forms.DateTimePicker DateTimeEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBoxMaxIterations;
        private Core.Forms.BasePanel PanelJurisdictions;
        private System.Windows.Forms.Label label4;
    }
}
