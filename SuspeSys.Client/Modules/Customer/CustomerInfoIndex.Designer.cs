namespace SuspeSys.Client.Modules
{
    partial class CustomerInfoIndex
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
            this.susGrid1 = new SuspeSys.Client.Modules.SusGrid();
            this.pnlToolButtonMain = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pnlGridMain = new DevExpress.XtraEditors.PanelControl();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolButtonMain)).BeginInit();
            this.pnlToolButtonMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridMain)).BeginInit();
            this.pnlGridMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // susToolBar1
            // 
            this.susToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.susToolBar1.Location = new System.Drawing.Point(0, 0);
            this.susToolBar1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.susToolBar1.Size = new System.Drawing.Size(1453, 71);
            this.susToolBar1.TabIndex = 1;
            // 
            // susGrid1
            // 
            this.susGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.susGrid1.Location = new System.Drawing.Point(0, 0);
            this.susGrid1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.susGrid1.Name = "susGrid1";
            this.susGrid1.Size = new System.Drawing.Size(1453, 752);
            this.susGrid1.TabIndex = 4;
            // 
            // pnlToolButtonMain
            // 
            this.pnlToolButtonMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlToolButtonMain.Controls.Add(this.panelControl2);
            this.pnlToolButtonMain.Controls.Add(this.pnlGridMain);
            this.pnlToolButtonMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolButtonMain.Location = new System.Drawing.Point(0, 0);
            this.pnlToolButtonMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlToolButtonMain.Name = "pnlToolButtonMain";
            this.pnlToolButtonMain.Size = new System.Drawing.Size(1453, 127);
            this.pnlToolButtonMain.TabIndex = 5;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.susToolBar1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1453, 73);
            this.panelControl2.TabIndex = 0;
            // 
            // pnlGridMain
            // 
            this.pnlGridMain.Controls.Add(this.searchControl1);
            this.pnlGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridMain.Location = new System.Drawing.Point(0, 0);
            this.pnlGridMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGridMain.Name = "pnlGridMain";
            this.pnlGridMain.Size = new System.Drawing.Size(1453, 127);
            this.pnlGridMain.TabIndex = 1;
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(6, 93);
            this.searchControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.NullValuePrompt = "输入客户编号，名称、手机查询";
            this.searchControl1.Properties.ShowClearButton = false;
            this.searchControl1.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.searchControl1_Properties_ButtonClick);
            this.searchControl1.Size = new System.Drawing.Size(363, 24);
            this.searchControl1.TabIndex = 0;
            this.searchControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchControl1_KeyDown);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.susGrid1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 127);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1453, 752);
            this.panelControl1.TabIndex = 6;
            // 
            // CustomerInfoIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.pnlToolButtonMain);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomerInfoIndex";
            this.Size = new System.Drawing.Size(1453, 879);
            this.Load += new System.EventHandler(this.CustomerInfoIndex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolButtonMain)).EndInit();
            this.pnlToolButtonMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridMain)).EndInit();
            this.pnlGridMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SusToolBar susToolBar1;
        private SusGrid susGrid1;
        private DevExpress.XtraEditors.PanelControl pnlToolButtonMain;
        private DevExpress.XtraEditors.PanelControl pnlGridMain;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
    }
}
