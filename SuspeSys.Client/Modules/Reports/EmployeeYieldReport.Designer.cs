namespace SuspeSys.Client.Modules.Reports
{
    partial class EmployeeYieldReport
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
            this.susQueryControl1 = new SuspeSys.Client.Modules.SusQueryControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.susToolBar1 = new SuspeSys.Client.Modules.SusToolBar();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.susGrid1 = new SuspeSys.Client.Modules.SusGrid();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.susToolBar1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1116, 45);
            this.panelControl1.TabIndex = 10;
            // 
            // susToolBar1
            // 
            this.susToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.susToolBar1.Location = new System.Drawing.Point(0, 0);
            this.susToolBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.susToolBar1.Name = "susToolBar1";
            this.susToolBar1.ShowAddButton = false;
            this.susToolBar1.ShowCancelButton = false;
            this.susToolBar1.ShowDeleteButton = false;
            this.susToolBar1.ShowExportButton = true;
            this.susToolBar1.ShowFixButton = true;
            this.susToolBar1.ShowMaxButton = true;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowPrintButton = true;
            this.susToolBar1.ShowQueryButton = true;
            this.susToolBar1.ShowRefreshButton = true;
            this.susToolBar1.ShowSaveAndAddButton = false;
            this.susToolBar1.ShowSaveAndCloseButton = false;
            this.susToolBar1.ShowSaveButton = false;
            this.susToolBar1.Size = new System.Drawing.Size(1116, 40);
            this.susToolBar1.TabIndex = 1;
            this.susToolBar1.Load += new System.EventHandler(this.susToolBar1_Load);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.xtraTabControl1);
            this.panelControl3.Controls.Add(this.susQueryControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 45);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1116, 505);
            this.panelControl3.TabIndex = 5;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1112, 501);
            this.xtraTabControl1.TabIndex = 5;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.susGrid1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1106, 472);
            this.xtraTabPage1.Text = "表格形式";
            // 
            // susGrid1
            // 
            this.susGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.susGrid1.Location = new System.Drawing.Point(0, 0);
            this.susGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.susGrid1.Name = "susGrid1";
            this.susGrid1.Size = new System.Drawing.Size(1106, 472);
            this.susGrid1.TabIndex = 4;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.pivotGridControl1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1106, 472);
            this.xtraTabPage2.Text = "透视图";
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGridControl1.Location = new System.Drawing.Point(0, 0);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.Size = new System.Drawing.Size(1106, 472);
            this.pivotGridControl1.TabIndex = 0;

            this.susQueryControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.susQueryControl1.Location = new System.Drawing.Point(2, 2);
            this.susQueryControl1.Name = "susQueryControl1";
            this.susQueryControl1.Size = new System.Drawing.Size(1112, 58);
            // 
            // EmployeeYieldReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "EmployeeYieldReport";
            this.Size = new System.Drawing.Size(1116, 550);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SusGrid susGrid1;
        private SusToolBar susToolBar1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private SusQueryControl susQueryControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        
        private DevExpress.XtraPivotGrid.PivotGridField pgfPO;
        private DevExpress.XtraPivotGrid.PivotGridField pgfDate;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFlowSection;
        private DevExpress.XtraPivotGrid.PivotGridField pgfStanardHours;
        private DevExpress.XtraPivotGrid.PivotGridField pgfProcessOrderNo;
        private DevExpress.XtraPivotGrid.PivotGridField pgfCardNo;
        private DevExpress.XtraPivotGrid.PivotGridField pgfEmployeeName;
        private DevExpress.XtraPivotGrid.PivotGridField pgffProcessFlowName;
        private DevExpress.XtraPivotGrid.PivotGridField pgfYieldCount;
        private DevExpress.XtraPivotGrid.PivotGridField pgfIncome;
        private DevExpress.XtraPivotGrid.PivotGridField pgfStyleNo;
        private DevExpress.XtraPivotGrid.PivotGridField pgfColor;
        private DevExpress.XtraPivotGrid.PivotGridField pgfSize;
        private DevExpress.XtraPivotGrid.PivotGridField pgfStatingNo;
        private DevExpress.XtraPivotGrid.PivotGridField pgfProcessFlowName;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFlowIndex;
        private DevExpress.XtraPivotGrid.PivotGridField pgfRealyWorkMin;
        private DevExpress.XtraPivotGrid.PivotGridField pgfSeamsRate;
        private DevExpress.XtraPivotGrid.PivotGridField pgfReworkRate;
        private DevExpress.XtraPivotGrid.PivotGridField pgfReworkCount;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField2;
        private DevExpress.XtraPivotGrid.PivotGridField RealyWorkMin;
        private DevExpress.XtraPivotGrid.PivotGridField pgfProcessFlowCode;
        private DevExpress.XtraPivotGrid.PivotGridField pgfFlowNo;
        private DevExpress.XtraPivotGrid.PivotGridField pgfStandardPrice;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField5;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField6;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField7;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField3;
    }
}
