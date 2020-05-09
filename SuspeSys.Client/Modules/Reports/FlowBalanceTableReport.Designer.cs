namespace SuspeSys.Client.Modules.Reports
{
    partial class FlowBalanceTableReport
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
            this.susToolBar1 = new SuspeSys.Client.Modules.SusToolBar();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.susGrid1 = new SuspeSys.Client.Modules.SusGrid();
            this.susQueryControl1 = new SuspeSys.Client.Modules.SusQueryControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
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
            this.susToolBar1.ShowMaxButton = true;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowQueryButton = true;
            this.susToolBar1.ShowRefreshButton = true;
            this.susToolBar1.ShowSaveAndAddButton = false;
            this.susToolBar1.ShowSaveAndCloseButton = false;
            this.susToolBar1.ShowSaveButton = false;
            this.susToolBar1.Size = new System.Drawing.Size(1325, 45);
            this.susToolBar1.TabIndex = 2;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.susGrid1);
            this.panelControl1.Controls.Add(this.susQueryControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 45);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1325, 596);
            this.panelControl1.TabIndex = 3;
            // 
            // susGrid1
            // 
            this.susGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.susGrid1.Location = new System.Drawing.Point(2, 60);
            this.susGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.susGrid1.Name = "susGrid1";
            this.susGrid1.Size = new System.Drawing.Size(1321, 534);
            this.susGrid1.TabIndex = 5;
            // 
            // susQueryControl1
            // 
            this.susQueryControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.susQueryControl1.Location = new System.Drawing.Point(2, 2);
            this.susQueryControl1.Name = "susQueryControl1";
            this.susQueryControl1.Size = new System.Drawing.Size(1321, 58);
            this.susQueryControl1.TabIndex = 6;
            // 
            // FlowBalanceTableReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.susToolBar1);
            this.Name = "FlowBalanceTableReport";
            this.Size = new System.Drawing.Size(1325, 641);
            this.Load += new System.EventHandler(this.FlowBalanceTableReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SusToolBar susToolBar1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private SusGrid susGrid1;
        private SusQueryControl susQueryControl1;
    }
}
