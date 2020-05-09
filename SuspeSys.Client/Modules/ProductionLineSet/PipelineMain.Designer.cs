namespace SuspeSys.Client.Modules.ProductionLineSet
{
    partial class PipelineMain
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
            this.components = new System.ComponentModel.Container();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.btnAddSite = new DevExpress.XtraEditors.SimpleButton();
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtPushRodNum = new DevExpress.XtraEditors.TextEdit();
            this.txtPipelineNo = new DevExpress.XtraEditors.TextEdit();
            this.comboSiteGroup = new DevExpress.XtraEditors.PopupContainerEdit();
            this.comboProdType = new DevExpress.XtraEditors.PopupContainerEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPushRodNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPipelineNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboSiteGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboProdType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("0356f30b-ace1-4131-bac0-b6dd7ac24507");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(247, 200);
            this.dockPanel1.Size = new System.Drawing.Size(247, 693);
            this.dockPanel1.Text = "流水线信息";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.btnAddSite);
            this.dockPanel1_Container.Controls.Add(this.txtMemo);
            this.dockPanel1_Container.Controls.Add(this.labelControl3);
            this.dockPanel1_Container.Controls.Add(this.labelControl4);
            this.dockPanel1_Container.Controls.Add(this.labelControl6);
            this.dockPanel1_Container.Controls.Add(this.labelControl5);
            this.dockPanel1_Container.Controls.Add(this.labelControl7);
            this.dockPanel1_Container.Controls.Add(this.txtPushRodNum);
            this.dockPanel1_Container.Controls.Add(this.txtPipelineNo);
            this.dockPanel1_Container.Controls.Add(this.comboSiteGroup);
            this.dockPanel1_Container.Controls.Add(this.comboProdType);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(239, 666);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // btnAddSite
            // 
            this.btnAddSite.Location = new System.Drawing.Point(14, 271);
            this.btnAddSite.Name = "btnAddSite";
            this.btnAddSite.Size = new System.Drawing.Size(123, 23);
            this.btnAddSite.TabIndex = 17;
            this.btnAddSite.Text = "添加站点";
            this.btnAddSite.Click += new System.EventHandler(this.btnAddSite_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(14, 228);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(222, 37);
            this.txtMemo.TabIndex = 16;
            this.txtMemo.Visible = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(14, 8);
            this.labelControl3.Name = "PipeliNo";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "流水线号";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(14, 54);
            this.labelControl4.Name = "GroupNo";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "生产组别";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(14, 162);
            this.labelControl6.Name = "PuttQuantity";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 9;
            this.labelControl6.Text = "推杆数量";
            this.labelControl6.Visible = false;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(14, 107);
            this.labelControl5.Name = "ProductType";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "产线类别";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(14, 208);
            this.labelControl7.Name = "Memo";
            this.labelControl7.Size = new System.Drawing.Size(24, 14);
            this.labelControl7.TabIndex = 8;
            this.labelControl7.Text = "备注";
            this.labelControl7.Visible = false;
            // 
            // txtPushRodNum
            // 
            this.txtPushRodNum.Location = new System.Drawing.Point(15, 182);
            this.txtPushRodNum.Name = "txtPushRodNum";
            this.txtPushRodNum.Properties.Mask.EditMask = "[0-9]*";
            this.txtPushRodNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtPushRodNum.Size = new System.Drawing.Size(222, 20);
            this.txtPushRodNum.TabIndex = 12;
            this.txtPushRodNum.Visible = false;
            // 
            // txtPipelineNo
            // 
            this.txtPipelineNo.Location = new System.Drawing.Point(14, 28);
            this.txtPipelineNo.Name = "txtPipelineNo";
            this.txtPipelineNo.Size = new System.Drawing.Size(222, 20);
            this.txtPipelineNo.TabIndex = 13;
            // 
            // comboSiteGroup
            // 
            this.comboSiteGroup.Location = new System.Drawing.Point(14, 74);
            this.comboSiteGroup.Name = "comboSiteGroup";
            this.comboSiteGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboSiteGroup.Size = new System.Drawing.Size(222, 20);
            this.comboSiteGroup.TabIndex = 15;
            this.comboSiteGroup.EditValueChanged += new System.EventHandler(this.comboSiteGroup_EditValueChanged);
            // 
            // comboProdType
            // 
            this.comboProdType.Location = new System.Drawing.Point(14, 127);
            this.comboProdType.Name = "comboProdType";
            this.comboProdType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboProdType.Size = new System.Drawing.Size(222, 20);
            this.comboProdType.TabIndex = 14;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(247, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1034, 693);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // PipelineMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.dockPanel1);
            this.Name = "PipelineMain";
            this.Size = new System.Drawing.Size(1281, 693);
            this.Load += new System.EventHandler(this.PipelineMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel1_Container.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPushRodNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPipelineNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboSiteGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboProdType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.SimpleButton btnAddSite;
        private DevExpress.XtraEditors.MemoEdit txtMemo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtPushRodNum;
        private DevExpress.XtraEditors.TextEdit txtPipelineNo;
        private DevExpress.XtraEditors.PopupContainerEdit comboSiteGroup;
        private DevExpress.XtraEditors.PopupContainerEdit comboProdType;
    }
}
