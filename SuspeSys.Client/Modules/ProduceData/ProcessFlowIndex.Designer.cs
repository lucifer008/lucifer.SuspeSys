namespace SuspeSys.Client.Modules.ProduceData
{
    partial class ProcessFlowIndex
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
            this.txtProcessOrderNo = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.processFlowMain1 = new SuspeSys.Client.Modules.ProduceData.ProcessFlowMain();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlTop.Controls.Add(this.susToolBar1);
            this.pnlTop.Controls.Add(this.txtProcessOrderNo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1273, 53);
            this.pnlTop.TabIndex = 0;
            // 
            // susToolBar1
            // 
            this.susToolBar1.Location = new System.Drawing.Point(216, 3);
            this.susToolBar1.Name = "susToolBar1";
            this.susToolBar1.ShowAddButton = false;
            this.susToolBar1.ShowCancelButton = true;
            this.susToolBar1.ShowDeleteButton = true;
            this.susToolBar1.ShowExportButton = false;
            this.susToolBar1.ShowMaxButton = true;
            this.susToolBar1.ShowModifyButton = true;
            this.susToolBar1.ShowRefreshButton = true;
            this.susToolBar1.ShowSaveAndAddButton = true;
            this.susToolBar1.ShowSaveAndCloseButton = true;
            this.susToolBar1.ShowSaveButton = true;
            this.susToolBar1.Size = new System.Drawing.Size(802, 50);
            this.susToolBar1.TabIndex = 1;
            // 
            // txtProcessOrderNo
            // 
            this.txtProcessOrderNo.Location = new System.Drawing.Point(5, 27);
            this.txtProcessOrderNo.Name = "txtProcessOrderNo";
            this.txtProcessOrderNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "制单号", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.txtProcessOrderNo.Properties.PopupControl = this.popupContainerControl1;
            this.txtProcessOrderNo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtProcessOrderNo.Size = new System.Drawing.Size(178, 21);
            this.txtProcessOrderNo.TabIndex = 0;
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.gridControl1);
            this.popupContainerControl1.Location = new System.Drawing.Point(301, 188);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(538, 135);
            this.popupContainerControl1.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(538, 135);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.popupContainerControl1);
            this.pnlMain.Controls.Add(this.processFlowMain1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 53);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1273, 535);
            this.pnlMain.TabIndex = 1;
            // 
            // processFlowMain1
            // 
            this.processFlowMain1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processFlowMain1.Location = new System.Drawing.Point(2, 2);
            this.processFlowMain1.Name = "processFlowMain1";
            this.processFlowMain1.Size = new System.Drawing.Size(1269, 531);
            this.processFlowMain1.TabIndex = 0;
            // 
            // ProcessFlowIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Name = "ProcessFlowIndex";
            this.Size = new System.Drawing.Size(1273, 588);
            this.Load += new System.EventHandler(this.ProcessFlowIndex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private ProcessFlowMain processFlowMain1;
        private DevExpress.XtraEditors.PopupContainerEdit txtProcessOrderNo;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private SusToolBar susToolBar1;
    }
}
