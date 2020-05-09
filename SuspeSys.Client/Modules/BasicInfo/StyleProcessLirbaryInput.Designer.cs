namespace SuspeSys.Client.Modules.BasicInfo
{
    partial class StyleProcessLirbaryInput
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.susToolBar1 = new SuspeSys.Client.Modules.SusToolBar();
            this.comStyleList = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.gcStyleSelectDialog = new DevExpress.XtraGrid.GridControl();
            this.gvSelectDialog = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.styleProcessLirbaryInputMain1 = new SuspeSys.Client.Modules.BasicInfo.StyleProcessLirbaryInputMain();
            this.xtraUserControl1 = new DevExpress.XtraEditors.XtraUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comStyleList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStyleSelectDialog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectDialog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlTop.Controls.Add(this.susToolBar1);
            this.pnlTop.Controls.Add(this.comStyleList);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1292, 60);
            this.pnlTop.TabIndex = 2;
            // 
            // susToolBar1
            // 
            this.susToolBar1.Location = new System.Drawing.Point(232, 7);
            this.susToolBar1.Name = "susToolBar1";
            this.susToolBar1.ShowAddButton = true;
            this.susToolBar1.ShowCancelButton = false;
            this.susToolBar1.ShowDeleteButton = false;
            this.susToolBar1.ShowExportButton = true;
            this.susToolBar1.ShowMaxButton = true;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowQueryButton = false;
            this.susToolBar1.ShowRefreshButton = true;
            this.susToolBar1.ShowSaveAndAddButton = true;
            this.susToolBar1.ShowSaveAndCloseButton = true;
            this.susToolBar1.ShowSaveButton = true;
            this.susToolBar1.Size = new System.Drawing.Size(646, 50);
            this.susToolBar1.TabIndex = 1;
            // 
            // comStyleList
            // 
            this.comStyleList.Location = new System.Drawing.Point(5, 27);
            this.comStyleList.Name = "comStyleList";
            this.comStyleList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "款式", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.comStyleList.Properties.PopupControl = this.popupContainerControl1;
            this.comStyleList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.comStyleList.Size = new System.Drawing.Size(200, 21);
            this.comStyleList.TabIndex = 0;
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.gcStyleSelectDialog);
            this.popupContainerControl1.Location = new System.Drawing.Point(320, 199);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(558, 226);
            this.popupContainerControl1.TabIndex = 2;
            // 
            // gcStyleSelectDialog
            // 
            this.gcStyleSelectDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcStyleSelectDialog.Location = new System.Drawing.Point(0, 0);
            this.gcStyleSelectDialog.MainView = this.gvSelectDialog;
            this.gcStyleSelectDialog.Name = "gcStyleSelectDialog";
            this.gcStyleSelectDialog.Size = new System.Drawing.Size(558, 226);
            this.gcStyleSelectDialog.TabIndex = 0;
            this.gcStyleSelectDialog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSelectDialog});
            // 
            // gvSelectDialog
            // 
            this.gvSelectDialog.GridControl = this.gcStyleSelectDialog;
            this.gvSelectDialog.Name = "gvSelectDialog";
            this.gvSelectDialog.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.popupContainerControl1);
            this.panelControl1.Controls.Add(this.styleProcessLirbaryInputMain1);
            this.panelControl1.Controls.Add(this.xtraUserControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 60);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1292, 602);
            this.panelControl1.TabIndex = 3;
            // 
            // styleProcessLirbaryInputMain1
            // 
            this.styleProcessLirbaryInputMain1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.styleProcessLirbaryInputMain1.Location = new System.Drawing.Point(2, 2);
            this.styleProcessLirbaryInputMain1.Name = "styleProcessLirbaryInputMain1";
            this.styleProcessLirbaryInputMain1.Size = new System.Drawing.Size(1288, 598);
            this.styleProcessLirbaryInputMain1.TabIndex = 1;
            this.styleProcessLirbaryInputMain1.ucMain = null;
            // 
            // xtraUserControl1
            // 
            this.xtraUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraUserControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraUserControl1.Name = "xtraUserControl1";
            this.xtraUserControl1.Size = new System.Drawing.Size(1288, 598);
            this.xtraUserControl1.TabIndex = 0;
            // 
            // StyleProcessLirbaryInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.pnlTop);
            this.Name = "StyleProcessLirbaryInput";
            this.Size = new System.Drawing.Size(1292, 662);
            this.Load += new System.EventHandler(this.StyleProcessLirbaryInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comStyleList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStyleSelectDialog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectDialog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private SusToolBar susToolBar1;
        private DevExpress.XtraEditors.PopupContainerEdit comStyleList;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.XtraUserControl xtraUserControl1;
        private StyleProcessLirbaryInputMain styleProcessLirbaryInputMain1;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraGrid.GridControl gcStyleSelectDialog;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSelectDialog;
    }
}
