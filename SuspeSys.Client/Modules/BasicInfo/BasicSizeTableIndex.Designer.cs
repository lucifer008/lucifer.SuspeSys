namespace SuspeSys.Client.Modules.BasicInfo
{
    partial class BasicSizeTableIndex
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
            this.pnlGridMain = new DevExpress.XtraEditors.PanelControl();
            this.susGrid1 = new SuspeSys.Client.Modules.SusGrid();
            this.pnlToolButtonMain = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.susToolBar1 = new SuspeSys.Client.Modules.SusToolBar();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridMain)).BeginInit();
            this.pnlGridMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolButtonMain)).BeginInit();
            this.pnlToolButtonMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGridMain
            // 
            this.pnlGridMain.Controls.Add(this.susGrid1);
            this.pnlGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridMain.Location = new System.Drawing.Point(0, 96);
            this.pnlGridMain.Name = "pnlGridMain";
            this.pnlGridMain.Size = new System.Drawing.Size(1457, 609);
            this.pnlGridMain.TabIndex = 9;
            // 
            // susGrid1
            // 
            this.susGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.susGrid1.Location = new System.Drawing.Point(2, 2);
            this.susGrid1.Name = "susGrid1";
            this.susGrid1.Size = new System.Drawing.Size(1453, 605);
            this.susGrid1.TabIndex = 5;
            this.susGrid1.Load += new System.EventHandler(this.susGrid1_Load);
            // 
            // pnlToolButtonMain
            // 
            this.pnlToolButtonMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlToolButtonMain.Controls.Add(this.panelControl2);
            this.pnlToolButtonMain.Controls.Add(this.panelControl1);
            this.pnlToolButtonMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolButtonMain.Location = new System.Drawing.Point(0, 0);
            this.pnlToolButtonMain.Name = "pnlToolButtonMain";
            this.pnlToolButtonMain.Size = new System.Drawing.Size(1457, 96);
            this.pnlToolButtonMain.TabIndex = 10;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.susToolBar1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1457, 57);
            this.panelControl2.TabIndex = 0;
            // 
            // susToolBar1
            // 
            this.susToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.susToolBar1.Location = new System.Drawing.Point(0, 0);
            this.susToolBar1.Name = "susToolBar1";
            this.susToolBar1.ShowAddButton = true;
            this.susToolBar1.ShowCancelButton = false;
            this.susToolBar1.ShowDeleteButton = true;
            this.susToolBar1.ShowExportButton = true;
            this.susToolBar1.ShowMaxButton = true;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowRefreshButton = true;
            this.susToolBar1.ShowSaveAndAddButton = false;
            this.susToolBar1.ShowSaveAndCloseButton = false;
            this.susToolBar1.ShowSaveButton = true;
            this.susToolBar1.Size = new System.Drawing.Size(1457, 55);
            this.susToolBar1.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.searchControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1457, 96);
            this.panelControl1.TabIndex = 1;
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(5, 63);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Size = new System.Drawing.Size(292, 20);
            this.searchControl1.TabIndex = 0;
            // 
            // BasicSizeTableIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlGridMain);
            this.Controls.Add(this.pnlToolButtonMain);
            this.Name = "BasicSizeTableIndex";
            this.Size = new System.Drawing.Size(1457, 705);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridMain)).EndInit();
            this.pnlGridMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolButtonMain)).EndInit();
            this.pnlToolButtonMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlGridMain;
        private SusGrid susGrid1;
        private DevExpress.XtraEditors.PanelControl pnlToolButtonMain;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private SusToolBar susToolBar1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
    }
}
