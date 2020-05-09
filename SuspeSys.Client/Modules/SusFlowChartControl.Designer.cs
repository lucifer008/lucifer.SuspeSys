namespace SuspeSys.Client.Modules
{
    partial class SusFlowChartControl
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
            this.coboFlowChart = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.coboFlowChart.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // coboFlowChart
            // 
            this.coboFlowChart.Location = new System.Drawing.Point(74, 3);
            this.coboFlowChart.Name = "coboFlowChart";
            this.coboFlowChart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.coboFlowChart.Size = new System.Drawing.Size(211, 20);
            this.coboFlowChart.TabIndex = 0;
            this.coboFlowChart.SelectedValueChanged += new System.EventHandler(this.coboFlowChart_SelectedValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "工艺图:";
            // 
            // SusFlowChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.coboFlowChart);
            this.Name = "SusFlowChartControl";
            this.Size = new System.Drawing.Size(368, 26);
            this.Load += new System.EventHandler(this.SusFlowChartControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.coboFlowChart.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit coboFlowChart;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
