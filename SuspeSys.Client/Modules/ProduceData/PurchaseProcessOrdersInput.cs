using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action.CustPurchaseOrder;
using SuspeSys.Client.Action.CustPurchaseOrder.Model;
using SuspeSys.Client.Action.ProcessOrder;
using SuspeSys.Client.Action.ProcessOrder.Model;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Modules.SusDialog;
using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Modules.ProduceData
{
    public class PurchaseProcessOrdersInput : SusXtraUserControl//DevExpress.XtraEditors.XtraUserControl
    {
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit txtProderOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ButtonEdit txtProductNoticeOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.ButtonEdit txtStyleCode;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit txtGenaterOrderDate;
        private DevExpress.XtraEditors.DateEdit txtDeliveryDate;
        private DevExpress.XtraEditors.TextEdit txtStyleDesption;
        private SimpleButton btnSelectCustomer;
        private PanelControl panelControl2;
        private SimpleButton btnClearAll;
        private SimpleButton btnDeleteItem;
        private GridControl gridControl2;
        private GridView gridView1;

        //  private PurchaseProcessOrdersInputMain purchaseProcessOrdersInputMain1;
        private SusToolBar susToolBar1;

        public PurchaseProcessOrdersInput() : base()
        {
            InitializeComponent();
            RegisterClickEvent();
            RegisterEvent();
            //panelControl2.Controls.Add(new PurchaseProcessOrdersInputMain() { Dock=DockStyle.Fill});
        }
        public PurchaseProcessOrdersInput(XtraUserControl1 uc) : this()
        {
            this.ucMain = uc;
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClearAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectCustomer = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteItem = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtProderOrderNo = new DevExpress.XtraEditors.ButtonEdit();
            this.txtStyleDesption = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtStyleCode = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtProductNoticeOrderNo = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtGenaterOrderDate = new DevExpress.XtraEditors.DateEdit();
            this.txtDeliveryDate = new DevExpress.XtraEditors.DateEdit();
            this.susToolBar1 = new SuspeSys.Client.Modules.SusToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProderOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleDesption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductNoticeOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGenaterOrderDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGenaterOrderDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gridControl2);
            this.pnlMain.Controls.Add(this.panelControl2);
            this.pnlMain.Controls.Add(this.panelControl1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 66);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1179, 610);
            this.pnlMain.TabIndex = 0;
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(2, 131);
            this.gridControl2.MainView = this.gridView1;
            this.gridControl2.MenuManager = this.barManager1;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1175, 477);
            this.gridControl2.TabIndex = 4;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl2;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowViewCaption = true;
            this.gridView1.ViewCaption = "制单明细";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1});
            this.barManager1.MaxItemId = 1;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ImageUri.Uri = "Add";
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.Width = 0;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1179, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 676);
            this.barDockControlBottom.Size = new System.Drawing.Size(1179, 35);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 676);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1179, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 676);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnClearAll);
            this.panelControl2.Controls.Add(this.btnSelectCustomer);
            this.panelControl2.Controls.Add(this.btnDeleteItem);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 90);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1175, 41);
            this.panelControl2.TabIndex = 3;
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(181, 9);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(90, 27);
            this.btnClearAll.TabIndex = 6;
            this.btnClearAll.Text = "清空明细";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnSelectCustomer
            // 
            this.btnSelectCustomer.Location = new System.Drawing.Point(8, 8);
            this.btnSelectCustomer.Name = "btnSelectCustomer";
            this.btnSelectCustomer.Size = new System.Drawing.Size(83, 28);
            this.btnSelectCustomer.TabIndex = 5;
            this.btnSelectCustomer.Text = "选择订单";
            this.btnSelectCustomer.Click += new System.EventHandler(this.btnSelectCustomer_Click);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(97, 9);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(78, 27);
            this.btnDeleteItem.TabIndex = 6;
            this.btnDeleteItem.Text = "删除订单";
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click_1);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1175, 88);
            this.panelControl1.TabIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.txtProderOrderNo);
            this.groupControl1.Controls.Add(this.txtStyleDesption);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtStyleCode);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.txtProductNoticeOrderNo);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtGenaterOrderDate);
            this.groupControl1.Controls.Add(this.txtDeliveryDate);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1175, 129);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "基本信息";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "制单号";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(509, 31);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 18);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "下单日期";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(244, 61);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(60, 18);
            this.labelControl10.TabIndex = 2;
            this.labelControl10.Text = "款式描述";
            // 
            // txtProderOrderNo
            // 
            this.txtProderOrderNo.Location = new System.Drawing.Point(63, 29);
            this.txtProderOrderNo.MenuManager = this.barManager1;
            this.txtProderOrderNo.Name = "txtProderOrderNo";
            this.txtProderOrderNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtProderOrderNo.Size = new System.Drawing.Size(160, 24);
            this.txtProderOrderNo.TabIndex = 1;
            // 
            // txtStyleDesption
            // 
            this.txtStyleDesption.Location = new System.Drawing.Point(310, 58);
            this.txtStyleDesption.MenuManager = this.barManager1;
            this.txtStyleDesption.Name = "txtStyleDesption";
            this.txtStyleDesption.Size = new System.Drawing.Size(677, 24);
            this.txtStyleDesption.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(229, 29);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(75, 18);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "生产通知单";
            // 
            // txtStyleCode
            // 
            this.txtStyleCode.Location = new System.Drawing.Point(63, 59);
            this.txtStyleCode.Name = "txtStyleCode";
            this.txtStyleCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtStyleCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtStyleCode.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtStyleCode_Properties_ButtonClick);
            this.txtStyleCode.Size = new System.Drawing.Size(160, 24);
            this.txtStyleCode.TabIndex = 1;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(23, 56);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(30, 18);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "款号";
            // 
            // txtProductNoticeOrderNo
            // 
            this.txtProductNoticeOrderNo.Location = new System.Drawing.Point(311, 30);
            this.txtProductNoticeOrderNo.Name = "txtProductNoticeOrderNo";
            this.txtProductNoticeOrderNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtProductNoticeOrderNo.Size = new System.Drawing.Size(160, 24);
            this.txtProductNoticeOrderNo.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(758, 34);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 18);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "交货日期";
            // 
            // txtGenaterOrderDate
            // 
            this.txtGenaterOrderDate.EditValue = null;
            this.txtGenaterOrderDate.Location = new System.Drawing.Point(577, 29);
            this.txtGenaterOrderDate.Name = "txtGenaterOrderDate";
            this.txtGenaterOrderDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtGenaterOrderDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtGenaterOrderDate.Properties.DisplayFormat.FormatString = "";
            this.txtGenaterOrderDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtGenaterOrderDate.Properties.EditFormat.FormatString = "";
            this.txtGenaterOrderDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtGenaterOrderDate.Properties.Mask.EditMask = "";
            this.txtGenaterOrderDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtGenaterOrderDate.Size = new System.Drawing.Size(160, 24);
            this.txtGenaterOrderDate.TabIndex = 1;
            // 
            // txtDeliveryDate
            // 
            this.txtDeliveryDate.EditValue = null;
            this.txtDeliveryDate.Location = new System.Drawing.Point(827, 29);
            this.txtDeliveryDate.Name = "txtDeliveryDate";
            this.txtDeliveryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDeliveryDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDeliveryDate.Properties.DisplayFormat.FormatString = "";
            this.txtDeliveryDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDeliveryDate.Properties.EditFormat.FormatString = "";
            this.txtDeliveryDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDeliveryDate.Properties.Mask.EditMask = "";
            this.txtDeliveryDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtDeliveryDate.Size = new System.Drawing.Size(160, 24);
            this.txtDeliveryDate.TabIndex = 1;
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
            this.susToolBar1.ShowExportButton = false;
            this.susToolBar1.ShowMaxButton = true;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowRefreshButton = false;
            this.susToolBar1.ShowSaveAndAddButton = true;
            this.susToolBar1.ShowSaveAndCloseButton = true;
            this.susToolBar1.ShowSaveButton = true;
            this.susToolBar1.Size = new System.Drawing.Size(1179, 66);
            this.susToolBar1.TabIndex = 1;
            // 
            // PurchaseProcessOrdersInput
            // 
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.susToolBar1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "PurchaseProcessOrdersInput";
            this.Size = new System.Drawing.Size(1179, 711);
            this.Load += new System.EventHandler(this.ProcessOrdersInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProderOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleDesption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductNoticeOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGenaterOrderDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGenaterOrderDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeliveryDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void RegisterClickEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
        }
        private ProcessOrderAction processOrderAction = new ProcessOrderAction();
        private ProcessOrderQueryAction processOrderQueryAction = new ProcessOrderQueryAction();
        //保存/保存并新增
        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            var sucess = false;
            try
            {
                switch (ButtonName)
                {
                    case ButtonName.Save:
                    case ButtonName.SaveAndAdd:
                    case ButtonName.SaveAndClose:
                        sucess = SaveProcessOrder();
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        return;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
                }
                if (sucess)
                {
                    if (ButtonName == ButtonName.Save)
                    {
                        txtProderOrderNo.Enabled = false;
                        EditProcessOrderModel = processOrderAction.Model.ProcessOrder;
                    }
                    if (ButtonName == ButtonName.SaveAndAdd)
                    {
                        var processOrder = new PurchaseProcessOrdersInput(ucMain) { Dock = DockStyle.Fill };
                        var tab = new XtraTabPage();
                        tab.Name = processOrder.Name;
                        tab.Text = "新增制单";
                        if (!ucMain.MainTabControl.TabPages.Contains(tab))
                        {
                            tab.Controls.Add(processOrder);
                            ucMain.MainTabControl.TabPages.Add(tab);
                        }
                        ucMain.MainTabControl.SelectedTabPage = tab;

                    }
                }
                if (ButtonName == ButtonName.SaveAndClose && sucess)
                {
                    ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                    return;

                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        bool SaveProcessOrder()
        {
            processOrderAction.Model = GetProcessOrderModel();
            var pOrderNo = processOrderAction.Model.ProcessOrder.POrderNo?.Trim();
            if (string.IsNullOrEmpty(pOrderNo))
            {
                XtraMessageBox.Show("制单号不能为空!", "温馨提示");
                return false;
            }
            if (null == EditProcessOrderModel)
            {
                if (processOrderQueryAction.CheckProcessOrderNoIsExist(pOrderNo))
                {
                    XtraMessageBox.Show("制单号已存在,请重新输入!", "温馨提示");
                    txtProderOrderNo.Focus();
                    return false;
                }
            }
            if (null == EditProcessOrderModel)
            {
                processOrderAction.AddProcessOrder();
            }
            else
                processOrderAction.UpdateProcessOrder();

            // XtraMessageBox.Show("保存成功!", "温馨提示");

            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            return true;
        }
        //GridControl gridControl1;
        /// <summary>
        /// 获取Input制单Model
        /// </summary>
        /// <returns></returns>
        Client.Action.ProcessOrder.Model.ProcessOrderModel GetProcessOrderModel()
        {
            var processOrder = new ProcessOrder();
            processOrder.Id = EditProcessOrderModel?.Id;
            processOrder.POrderType = ProcessOrderType.Composed.Value;
            processOrder.POrderTypeDesption = ProcessOrderType.Composed.Desption;
            processOrder.POrderNo = txtProderOrderNo.Text.Trim();
            processOrder.Status = ProcessOrderStatus.WaitingAudit.Value;
            processOrder.ProductNoticeOrderNo = txtProductNoticeOrderNo.Text.Trim();
            processOrder.GenaterOrderDate = txtGenaterOrderDate.DateTime.FormatDate();
            processOrder.DeliveryDate = txtDeliveryDate.DateTime.FormatDate();
            processOrder.Style = txtStyleCode.Tag as Style;
            processOrder.StyleCode = txtStyleCode.Text.Trim();
            processOrder.StyleName = (txtStyleCode.Tag as Style)?.StyleName;
            //var selCustomer = btnEditCustomer.Tag as DaoModel.Customer;
            //processOrder.Customer = selCustomer;
            //processOrder.CustomerName = selCustomer?.CusName;
            //processOrder.CustomerStyle = txtCustomerStyleCode.EditValue?.ToString();
            //processOrder.CustomerNo = selCustomer?.CusNo;

            //客户订单
            //
            if (_cusPurchColorSizeSourceList == null)
                _cusPurchColorSizeSourceList = new List<CusPurchColorAndSizeModel>();
            //制单颜色
            var processOrderColorList = ProcessOrderColorList ?? new List<PoColor>();
            var processOrderColorItemList = new List<DaoModel.ProcessOrderColorItemModel>();
            foreach (var item in _cusPurchColorSizeSourceList)
            {
                var processOrderColorItem = new DaoModel.ProcessOrderColorItemModel()
                {
                    CustomerpurchaseorderId = item.Id,
                    //CustomerPurchaseOrder=
                    PoColor = new CommonAction().Get<DaoModel.PoColor>(item.ColorId?.ToString()),
                    Color = item.ColorValue,
                    ColorDescription = item.ColorDescption
                };
                processOrderColorItemList.Add(processOrderColorItem);

            }

            //颜色大小明细
            var inputColorAndSizeList = (gridControl2.DataSource as List<CusPurchColorAndSizeModel>) ?? new List<CusPurchColorAndSizeModel>();

            //var processOrderColorSizeItemList = new List<DaoModel.ProcessOrderColorSizeItem>();
            foreach (var cr in processOrderColorItemList)
            {
                cr.ProcessOrderColorSizeItemList = new List<ProcessOrderColorSizeItem>();
                foreach (var item in inputColorAndSizeList)
                {
                    if (cr.PoColor.Id.Equals(item.ColorId) && cr.CustomerpurchaseorderId.Equals(item.Id))
                    {
                        var colorTotal = 0;
                        for (var i = 1; i < LastSizeList.Count + 1; i++)
                        {
                            //if (LastSizeList[i-1].Id.Equals(item.ColorId))
                            //{
                            var filedName = "SizeValue" + i;
                            var cSize = GridControlHelper.GetColumnTag(gridControl2, filedName) as PSize;
                            var it = new ProcessOrderColorSizeItem()
                            {
                                PSize = cSize,
                                SizeDesption = cSize?.Size,
                                Total = ReflectionUtils<ColorAndSizeModel>.GetPropertyValue(item, filedName)
                            };
                            if (!string.IsNullOrEmpty(it.Total))
                            {
                                colorTotal += Convert.ToInt32(it.Total);
                            }
                            cr.ProcessOrderColorSizeItemList.Add(it);
                            //}
                        }
                        cr.Total = colorTotal;
                    }
                    //  processOrderColorSizeItemList.Add(new ProcessOrderColorSizeItem() { PSize=item,SizeDesption=item.SizeDesption});
                }
            }

            var model = new Client.Action.ProcessOrder.Model.ProcessOrderModel();
            model.ProcessOrder = processOrder;
            model.ProcessOrderItemList = processOrderColorItemList;
            return model;
        }
        public IList<CusPurchColorAndSizeModel> ProcessOrderColorSizeList { set; get; }
        public List<PoColor> ProcessOrderColorList { get; internal set; }
        public List<PSize> ProcessOrderSizeList { get; internal set; }
        public ProcessOrder EditProcessOrderModel { get; internal set; }
        public XtraUserControl1 ucMain { get; internal set; }

        private void btnColorAndSize_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var csDialog = new CustomerOrderSelectDialog();
            //  csDialog.GridControlColorSizes = gridControl1;
            csDialog.ShowDialog();
            Cursor = Cursors.Default;
        }
        IList<PSize> sizeList = new CommonAction().GetSizeList();
        IList<PoColor> cList = new CommonAction().GetAllColorList().ToArray();
        private void BindGridHeader(GridControl gc)
        {
            //            初始时绑定要选中值
            //复制代码

            //private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
            //        {
            //            if (e.Column.Name == "D" && e.RowHandle >= 0)
            //            {
            //                ImageComboBoxEdit edit1 = new ImageComboBoxEdit();
            //                edit1.Properties.Items.AddRange(repositoryItemImageComboBox1.Items);
            //                e.RepositoryItem = edit1.Properties;
            //                foreach (ImageComboBoxItem item in edit1.Properties.Items)
            //                {
            //                    if (gridView1.GetRowCellValue(e.RowHandle, "D").ToString() == item.Value.ToString())
            //                    {
            //                        edit1.SelectedItem = item;
            //                    }
            //                }
            //            }
            //        }

            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new GridView();

            var colorResComboBox = new RepositoryItemComboBox();
            colorResComboBox.NullValuePrompt = "请选择颜色";
            colorResComboBox.SelectedValueChanged += ColorResComboBox_SelectedValueChanged;
            colorResComboBox.ParseEditValue += new ConvertEditValueEventHandler(colorResComboBox_ParseEditValue);
            foreach (var cc in cList)
            {
                colorResComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc.ColorValue,
                    Value = cc.Id,
                    Tag = cc
                });
            }

            var sizeResComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            sizeResComboBox.NullValuePrompt = "请选择颜色";
            sizeResComboBox.SelectedValueChanged += SizeResComboBox_SelectedValueChanged;
            sizeResComboBox.ParseEditValue += new ConvertEditValueEventHandler(sizeResComboBox_ParseEditValue);

            foreach (var cc in sizeList)
            {
                sizeResComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc.Size,
                    Value = cc.Id,
                    Tag = cc
                });
            }
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName="No",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true, ColumnEdit=colorResComboBox},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="SizeValue",Visible=true,ColumnEdit=sizeResComboBox},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码描述",FieldName="SizeDescption",Visible=true}//,
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码数量",FieldName="SizeDescption",Visible=true}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();

            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }

        private void sizeResComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void colorResComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void SizeResComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //var gv = (gridControl1.MainView as GridView);
            //var crCom = sender as RepositoryItemComboBox;
            //var colorKey = (gridControl1.MainView as GridView).FocusedColumn.FieldName;
            //BaseEdit edit = gv.ActiveEditor;
            //gv.SetFocusedRowCellValue("SizeDescption", "2222");
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();
                //var value = (string)item.Value;
                //2.获取gridview选中的行
                GridView gv = (gridControl2.MainView as GridView);
                // int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                //3.保存选中值到datatable
                //dt.Rows[dataIndex]["value"] = value;
                //dt.Rows[dataIndex]["text"] = text;
                gv.SetFocusedRowCellValue("SizeDescption", (item.Tag as PSize)?.SizeDesption);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void ColorResComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //var gv = (gridControl1.MainView as GridView);
            //var crCom = sender as RepositoryItemComboBox;
            //var colorKey = (gridControl1.MainView as GridView).FocusedColumn.FieldName;
            //BaseEdit edit = gv.ActiveEditor;
            //gv.SetFocusedRowCellValue("ColorDescption", "ssss");
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();
                //var value = (string)item.Value;
                //2.获取gridview选中的行
                GridView gv = (gridControl2.MainView as GridView);
                // int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                //3.保存选中值到datatable
                //dt.Rows[dataIndex]["value"] = value;
                //dt.Rows[dataIndex]["text"] = text;
                gv.SetFocusedRowCellValue("ColorDescption", (item.Tag as PoColor)?.ColorDescption);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void txtStyleCode_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                var styleDialog = new StyleSelectDialog();
                styleDialog.ShowDialog();
                //txtStyleCode.EditValue = styleDialog.SelectedStyle;
                if (null != styleDialog.SelectedStyle)
                {
                    txtStyleCode.Tag = styleDialog.SelectedStyle;
                    txtStyleCode.Text = styleDialog.SelectedStyle?.StyleNo;
                    txtStyleDesption.Text = styleDialog.SelectedStyle?.StyleName;
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }


        CommonAction commAction = new CommonAction();
        private void ProcessOrdersInput_Load(object sender, EventArgs e)
        {
            BindEditProcessOrder();
        }
        void BindEditProcessOrder()
        {
            try
            {
                if (null != EditProcessOrderModel)
                {
                    txtProderOrderNo.Text = EditProcessOrderModel.POrderNo?.Trim();
                    txtProderOrderNo.Enabled = false;

                    txtProductNoticeOrderNo.Text = EditProcessOrderModel.ProductNoticeOrderNo?.Trim();
                    if (null != EditProcessOrderModel.GenaterOrderDate)
                        txtGenaterOrderDate.DateTime = EditProcessOrderModel.GenaterOrderDate.Value;

                    if (null != EditProcessOrderModel.DeliveryDate)
                        txtDeliveryDate.DateTime = EditProcessOrderModel.DeliveryDate.Value;
                    txtStyleCode.Text = EditProcessOrderModel.StyleCode?.Trim();
                    txtStyleDesption.Text = EditProcessOrderModel.StyleName?.Trim();
                    txtStyleCode.Tag = EditProcessOrderModel.Style;

                    //btnEditCustomer.Tag = EditProcessOrderModel.Customer;
                    //btnEditCustomer.Text = EditProcessOrderModel.CustomerName;
                    //txtCustomerStyleCode.Text = EditProcessOrderModel.CustomerStyle?.Trim();
                    //txtCustomerOrderNo.Text = EditProcessOrderModel.CustomerNo?.Trim();

                    //制单明细
                    var queryAction = new ProcessOrderQueryAction();
                    queryAction.GetProcessOrderItem(EditProcessOrderModel.Id);
                    var pOrderColorList = queryAction.Model.ProcessOrderColorSizeItemList;

                    //var cusPusOrderColorAndSizeList = new List<CusPurchColorAndSizeModel>();
                    //foreach (var item in pOrderColorList)
                    //{
                    //    var r = BeanUitls<CusPurchColorAndSizeModel, DaoModel.ProcessOrderColorItemModel>.Mapper(item);
                    //    cusPusOrderColorAndSizeList.Add(r);
                    //}

                    var pSizeList = new List<PSize>();
                    var pColorList = new List<PoColor>();
                    var pColorSizeDataList = new List<CusPurchColorAndSizeModel>();
                    var tag = false;
                    foreach (var item in pOrderColorList)
                    {
                        var m = new CusPurchColorAndSizeModel();
                        m.ColorId = item.PoColor.Id;
                        m.SizeColumnCount = item.ProcessOrderColorSizeItemList.Count;
                        m.ColorValue = item.PoColor.ColorValue;
                        m.ColorDescption = item.ColorDescription;
                        m.Total = item.Total;

                        var cusPurOrder = commAction.Get<DaoModel.CustomerPurchaseOrder>(item.CustomerPurchaseOrder?.Id);
                        m.OrderNo = cusPurOrder.OrderNo;
                        m.PurchaseOrderNo = cusPurOrder.PurchaseOrderNo;
                        m.CusNo = cusPurOrder.CusNo;
                        m.CusName = cusPurOrder.CusName;
                        m.Id = cusPurOrder.Id;

                        pColorList.Add(item.PoColor);
                        var index = 1;
                        foreach (var size in item.ProcessOrderColorSizeItemList)
                        {
                            if (!LastSizeList.Exists(f => f.Id.Equals(size.PSize.Id)))
                            {
                                LastSizeList.Add(size.PSize);
                            }
                            if (!tag)
                            {
                                pSizeList.Add(size.PSize);
                            }
                            ReflectionUtils<ColorAndSizeModel>.SetPropertyValue(m, string.Format("SizeValue{0}", index), size.Total);
                            index++;
                        }
                        if (pSizeList.Count > 0)
                        {
                            tag = true;
                        }
                        pColorSizeDataList.Add(m);
                    }
                    ProcessOrderColorSizeList = pColorSizeDataList;
                    _cusPurchColorSizeSourceList = pColorSizeDataList;

                    BindProcessOrderColorSizeList(pColorList, pSizeList);
                    //DaoModel.ProcessOrderColorItem 
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }
        private void BindProcessOrderColorSizeList(List<PoColor> selectedColorList, List<PSize> selectedSizeList)
        {
            var gv = gridControl2.MainView as GridView;
            gv.Columns.Clear();
            gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
               new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
               new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户编号",FieldName="CusNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户名称",FieldName="CusName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO编号",FieldName="PurchaseOrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="订单号",FieldName="OrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true},
            });
            int j = 1;
            foreach (var size in selectedSizeList)
            {
                gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = size.Size, Tag = size, FieldName = "SizeValue" + (j++), Visible = true });
            }
            gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "数量合计", FieldName = "Total", Visible = true });
            gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gv});
            gridControl2.MainView = gv;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gv.BestFitColumns();//按照列宽度自动适配
            gv.GridControl = gridControl2;
            gv.OptionsView.ShowFooter = true;
            gv.OptionsView.ShowGroupPanel = false;

            gv.ClearRows();
            //var processOrderColorSizeList = new List<ColorAndSizeModel>();
            //for (var i = 0; i < selectedColorList.Count; i++)
            //{
            //    var index = 1;
            //    var cs = new ColorAndSizeModel()
            //    {
            //        No = index.ToString(),
            //        ColorId = selectedColorList[i].Id,
            //        ColorValue = selectedColorList[i].ColorValue,
            //        ColorDescption = selectedColorList[i].ColorDescption,
            //        SizeColumnCount = selectedSizeList.Count
            //    };
            //    index++;
            //    //processOrderColorSizeList = GridControlColorSizes.MainView.DataSource as List<ColorAndSizeModel>;
            //    //if (null == processOrderColorSizeList)
            //    //{
            //    //    processOrderColorSizeList = new List<ColorAndSizeModel>();
            //    //}
            //    processOrderColorSizeList.Add(cs);
            //}
            gridControl2.DataSource = ProcessOrderColorSizeList;
            gridControl2.MainView.RefreshData();
            gv.IndicatorWidth = 40;
            gv.OptionsView.ShowIndicator = true;
            gv.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
            //保存选择颜色尺码明细
            //this.ProcessOrderColorSizeList = processOrderColorSizeList;
            this.ProcessOrderColorList = selectedColorList;
            this.ProcessOrderSizeList = selectedSizeList;


        }
        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        private void btnEditCustomer_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var customerDialog = new CustomerSelectDialog();
            customerDialog.ShowDialog();
            //btnEditCustomer.Tag = customerDialog.SelectedCustomer;
            //btnEditCustomer.Text = customerDialog.SelectedCustomer?.CusName;
            Cursor = Cursors.Default;
        }
        int index = 1;

        #region 制单明细处理代码
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            var cs = new ColorAndSizeModel() { No = index.ToString() };
            index++;
            var dt = gridControl2.MainView.DataSource as List<ColorAndSizeModel>;
            if (null == dt)
            {

                dt = new List<ColorAndSizeModel>();
                dt.Add(cs);
                gridControl2.DataSource = dt;
                return;
            }

            dt.Add(cs);
            //gridControl1.DataSource = dt;
            gridControl2.MainView.RefreshData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            (gridControl2.MainView as GridView)?.ClearRows();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {

        }

        List<CusPurchColorAndSizeModel> TotalCusPurchColorAndSizeModelList = new List<CusPurchColorAndSizeModel>();
        private void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            var cusOrderDialog = new CustomerOrderSelectDialog();
            cusOrderDialog.ShowDialog();

            var seleCustomerOrderList = cusOrderDialog.SelectedCustomerPurchaseOrderList ?? new List<DaoModel.CustomerPurchaseOrder>();
            TotalCusPurchColorAndSizeModelList = GeneratorColorAndSizeModel(seleCustomerOrderList.ToList<DaoModel.CustomerPurchaseOrder>());

            var dt = gridControl2.DataSource as List<CusPurchColorAndSizeModel> ?? new List<CusPurchColorAndSizeModel>();
            foreach (var item in seleCustomerOrderList)
            {
                if (dt.Exists(f => f.Id.Equals(item.Id)))
                {
                    XtraMessageBox.Show(string.Format("客户【{0}】的订单【{1}】已经添加到明细，不能重复添加!", item.CusName, item.OrderNo), "温馨提示");
                    return;
                }
                GeneratorProcessOrderItemToGrid(item);
            }
        }
        /// <summary>
        /// 客户订单颜色尺码明细(横向拉平)
        /// </summary>
        List<CusPurchColorAndSizeModel> _cusPurchColorSizeSourceList = new List<CusPurchColorAndSizeModel>();

        public List<CusPurchColorAndSizeModel> CusPurchColorSizeSourceList { get { return _cusPurchColorSizeSourceList; } }

        List<CusPurchColorAndSizeModel> CpOrderSizeList = new List<CusPurchColorAndSizeModel>();
        /// <summary>
        /// 选择的客户颜色尺码明细
        /// </summary>
        List<List<DaoModel.PSize>> CpOrderPSizeList = new List<List<PSize>>();
        /// <summary>
        /// 选择的客户颜色明细
        /// </summary>
        List<List<DaoModel.PoColor>> CpOrderPoColorList = new List<List<PoColor>>();

        List<DaoModel.PSize> LastSizeList = new List<DaoModel.PSize>();
        List<ColorAndSizeModel> pColorSizeDataList = new List<ColorAndSizeModel>();
        List<DaoModel.PSize> GeneratorSizeRelationList(List<DaoModel.PSize> pSizeList)
        {
            var sizeIndex = 1;
            var reslutSizeList = ComposedSize(LastSizeList, pSizeList);
            var colorSizeRelationList = new List<ColorSizeRelationModel>();
            foreach (var s in reslutSizeList)
            {
                colorSizeRelationList.Add(new ColorSizeRelationModel()
                {
                    SizeIndex = index,
                    SizeId = s.Id,
                    SizeValue = s.Size,
                    SizeField = "SizeValue" + sizeIndex
                });
                sizeIndex++;
            }
            return reslutSizeList;
        }
        List<CusPurchColorAndSizeModel> GeneratorColorAndSizeModel(List<DaoModel.CustomerPurchaseOrder> cpOrderList)
        {
            var pColorSizeDataList = new List<CusPurchColorAndSizeModel>();
            foreach (var o in cpOrderList)
            {
                CustPurchaseOrderAction action = new CustPurchaseOrderAction();
                action.GetCustomerOrderItem(o.Id);
                var pOrderColorList = action.Model.CustomerPurchaseOrderColorItemModelList;
                foreach (var cpoColor in pOrderColorList)
                {
                    var m = new CusPurchColorAndSizeModel();
                    m.ColorId = cpoColor.PoColor.Id;
                    m.SizeColumnCount = cpoColor.CustomerPurchaseOrderColorSizeItemList.Count;
                    m.ColorValue = cpoColor.PoColor.ColorValue;
                    m.ColorDescption = cpoColor.ColorDescription;
                    m.Total = Convert.ToInt32(cpoColor.Total);
                    m.Id = o.Id;
                    m.CusName = o.CusName;
                    m.CusNo = o.CusNo;
                    m.PurchaseOrderNo = o.PurchaseOrderNo;
                    m.OrderNo = o.OrderNo;
                    m.CustomerPurchaseOrderColorSizeItemList = cpoColor.CustomerPurchaseOrderColorSizeItemList;
                    pColorSizeDataList.Add(m);
                }
            }
            return pColorSizeDataList;
        }
        void GeneratorProcessOrderItemToGrid(DaoModel.CustomerPurchaseOrder cpOrder)
        {
            CustPurchaseOrderAction action = new CustPurchaseOrderAction();
            action.GetCustomerOrderItem(cpOrder.Id);
            var pOrderColorList = action.Model.CustomerPurchaseOrderColorItemModelList;

            var pSizeList = new List<DaoModel.PSize>();
            var pColorList = new List<DaoModel.PoColor>();

            var tag = false;
            foreach (var item in pOrderColorList)
            {
                var m = new ColorAndSizeModel();
                m.ColorId = item.PoColor.Id;
                m.SizeColumnCount = item.CustomerPurchaseOrderColorSizeItemList.Count;
                m.ColorValue = item.PoColor.ColorValue;
                m.ColorDescption = item.ColorDescription;
                m.Total = Convert.ToInt32(item.Total);

                pColorList.Add(item.PoColor);
                //var index = 1;
                foreach (var size in item.CustomerPurchaseOrderColorSizeItemList)
                {
                    if (!tag)
                    {
                        pSizeList.Add(size.PSize);
                    }
                    // ReflectionUtils<ColorAndSizeModel>.SetPropertyValue(m, string.Format("SizeValue{0}", index), size.Total);
                    //  index++;
                }
                if (pSizeList.Count > 0)
                {
                    tag = true;
                }
                m.CustomerPurchaseOrderColorSizeItemList = item.CustomerPurchaseOrderColorSizeItemList;
                pColorSizeDataList.Add(m);
            }
            //合成尺码
            var sizeIndex = 1;
            var reslutSizeList = ComposedSize(LastSizeList, pSizeList);
            var colorSizeRelationList = new List<ColorSizeRelationModel>();
            foreach (var s in reslutSizeList)
            {
                colorSizeRelationList.Add(new ColorSizeRelationModel()
                {
                    SizeIndex = sizeIndex,
                    SizeId = s.Id,
                    SizeValue = s.Size,
                    SizeField = "SizeValue" + sizeIndex
                });
                sizeIndex++;
            }

            //foreach (var cpsModel in TotalCusPurchColorAndSizeModelList)
            //{
            var cpOrderSizeList = new List<CusPurchColorAndSizeModel>();
            foreach (var d in TotalCusPurchColorAndSizeModelList)
            {
                if (cpOrder.Id == d.Id)
                {
                    var ta = new CusPurchColorAndSizeModel();// BeanUitls<CusPurchColorAndSizeModel, ColorAndSizeModel>.Mapper(d);
                    ta.Id = cpOrder.Id;
                    ta.CusName = cpOrder.CusName;
                    ta.CusNo = cpOrder.CusNo;
                    ta.PurchaseOrderNo = cpOrder.PurchaseOrderNo;
                    ta.OrderNo = cpOrder.OrderNo;
                    ta.ColorId = d.ColorId;
                    ta.ColorValue = d.ColorValue;
                    ta.ColorDescption = d.ColorDescption;
                    //var crl= colorSizeRelationList.Where(f => f.ColorId.Equals(d.ColorId)).SingleOrDefault();
                    // var colorValue=
                    // ReflectionUtils<CusPurchColorAndSizeModel>.SetPropertyValue(ta, string.Format("SizeValue{0}", crl.ColorIndex), ta.);
                    var total = 0;
                    foreach (var item in d.CustomerPurchaseOrderColorSizeItemList)
                    {
                        var crl = colorSizeRelationList.Where(f => f.SizeId.Equals(item.PSize.Id))?.SingleOrDefault();
                        if (null != crl)
                        {
                            ReflectionUtils<CusPurchColorAndSizeModel>.SetPropertyValue(ta, string.Format("SizeValue{0}", crl.SizeIndex), item.Total);
                            if (!string.IsNullOrEmpty(item.Total))
                                total += Convert.ToInt32(item.Total);
                        }
                    }
                    if (total > 0)
                        ta.Total = total;
                    cpOrderSizeList.Add(ta);
                }
            }
            // }
            if (CpOrderSizeList.Count == 0)
            {
                CpOrderSizeList = cpOrderSizeList;
            }
            else
            {
                if (CpOrderSizeList.Exists(f => f.Id.Equals(cpOrder.Id)))
                {
                    XtraMessageBox.Show(string.Format("客户【{0}】的订单【{1}】已经添加到明细，不能重复添加!", cpOrder.CusName, cpOrder.OrderNo), "温馨提示");
                    return;
                }

                cpOrderSizeList.ForEach(a => CpOrderSizeList.Add(a));
                //foreach (var s in cpOrderSizeList) {
                //    foreach (var s2 in ) {

                //    }
                //}
            }

            //记录原始客户订单
            var arrays = new CusPurchColorAndSizeModel[CpOrderSizeList.Count];
            CpOrderSizeList.CopyTo(arrays);
            _cusPurchColorSizeSourceList = arrays.ToList<CusPurchColorAndSizeModel>();

            //分组合计
            //  var lt = GroupBySumCustOrderColorSizeItem(CpOrderSizeList);
            //if (pSizeList.Count > MaxSizeList.Count)
            //{
            //    MaxSizeList = pSizeList;
            //}

            LastSizeList.Clear();
            foreach (var size in reslutSizeList)
            {
                var s = BeanUitls<DaoModel.PSize, DaoModel.PSize>.Mapper(size);
                LastSizeList.Add(s);
                //if (!LastSizeList.Exists(f => f.Id.Equals(size.Id)))
                //{
                //    LastSizeList.Add(s);
                //}
            }
            BindProcessOrderColorSizeList(pColorList, reslutSizeList, CpOrderSizeList);//lt);
        }
        /// <summary>
        /// 合成尺码
        /// </summary>
        private List<DaoModel.PSize> ComposedSize(List<DaoModel.PSize> lastPSizeList, List<DaoModel.PSize> currentPSizeList)
        {
            List<DaoModel.PSize> resultList = new List<DaoModel.PSize>();
            if (lastPSizeList.Count == 0)
            {
                foreach (var size in currentPSizeList)
                {
                    var s = BeanUitls<DaoModel.PSize, DaoModel.PSize>.Mapper(size);
                    resultList.Add(s);
                }
                return resultList;
            }
            foreach (var size in lastPSizeList)
            {
                var s = BeanUitls<DaoModel.PSize, DaoModel.PSize>.Mapper(size);
                resultList.Add(s);
            }
            foreach (var size in currentPSizeList)
            {
                if (!lastPSizeList.Exists(f => f.Id.Equals(size.Id)))
                {
                    resultList.Add(size);
                }
            }
            return resultList;
        }
        List<CusPurchColorAndSizeModel> GroupBySumCustOrderColorSizeItem(List<CusPurchColorAndSizeModel> cpOrderSizeList)
        {
            var query = from l in cpOrderSizeList
                        group l by new { l.CusNo, l.CusName, l.OrderNo, l.PurchaseOrderNo, l.ColorId, l.ColorValue } into g
                        select new CusPurchColorAndSizeModel()
                        {
                            CusNo = g.Key.CusNo,
                            CusName = g.Key.CusName,
                            OrderNo = g.Key.OrderNo,
                            PurchaseOrderNo = g.Key.PurchaseOrderNo,
                            ColorId = g.Key.ColorId,
                            ColorValue = g.Key.ColorValue,
                            SizeValue1 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue1) ? "0" : f.SizeValue1))).ToString(),
                            SizeValue2 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue2) ? "0" : f.SizeValue2))).ToString(),
                            SizeValue3 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue3) ? "0" : f.SizeValue3))).ToString(),
                            SizeValue4 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue4) ? "0" : f.SizeValue4))).ToString(),
                            SizeValue5 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue5) ? "0" : f.SizeValue5))).ToString(),
                            SizeValue6 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue6) ? "0" : f.SizeValue6))).ToString(),
                            SizeValue7 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue7) ? "0" : f.SizeValue7))).ToString(),
                            SizeValue8 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue8) ? "0" : f.SizeValue8))).ToString(),
                            SizeValue9 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue9) ? "0" : f.SizeValue9))).ToString(),
                            SizeValue10 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue10) ? "0" : f.SizeValue10))).ToString(),
                            SizeValue11 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue11) ? "0" : f.SizeValue11))).ToString(),
                            SizeValue12 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue12) ? "0" : f.SizeValue12))).ToString(),
                            SizeValue13 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue13) ? "0" : f.SizeValue13))).ToString(),
                        };
            var resList = query.ToList<CusPurchColorAndSizeModel>();
            return resList;
        }
        private void BindProcessOrderColorSizeList(List<DaoModel.PoColor> selectedColorList, List<DaoModel.PSize> selectedSizeList, List<CusPurchColorAndSizeModel> cpOrderSizeList)
        {
            var gv = gridControl2.MainView as GridView;
            gv.Columns.Clear();
            gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
               new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户编号",FieldName="CusNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户名称",FieldName="CusName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO编号",FieldName="PurchaseOrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="订单号",FieldName="OrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true},
            });
            int j = 1;
            foreach (var size in selectedSizeList)
            {
                gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = size.Size, Tag = size, FieldName = "SizeValue" + (j++), Visible = true });
            }
            gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "数量合计", FieldName = "Total", Visible = true });
            gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gv});
            gridControl2.MainView = gv;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            //gv.BestFitColumns();//按照列宽度自动适配
            //gv.GridControl = gridControl1;
            //gv.OptionsView.ShowFooter = true;
            //gv.OptionsView.ShowGroupPanel = false;

            //gv.ClearRows();

            gridControl2.DataSource = cpOrderSizeList;
            gridControl2.MainView.RefreshData();
            gv.IndicatorWidth = 40;
            //gv.OptionsView.ShowIndicator = true;
            //  gv.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
            //保存选择颜色尺码明细
            //this.ProcessOrderColorSizeList = processOrderColorSizeList;
            //this.CustomerOrderColorList = selectedColorList;
            //this.CustomerOrderSizeList = selectedSizeList;
        }

        void RegisterEvent()
        {
            var gv = gridControl2?.MainView as GridView;
            if (null != gv)
            {
                gv.CellValueChanged += Gv_CellValueChanged;
                gv.CustomRowCellEdit += Gv_CustomRowCellEdit;
            }
        }
        RepositoryItem _disabledItem;
        IList<string> disableColumnList = new List<string>() { "Total", "CusNo", "CusName", "PurchaseOrderNo", "OrderNo", "ColorValue", "ColorDescption" };
        private void Gv_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (!disableColumnList.Contains(e.Column.FieldName))//需要设置的列名
                return;

            if (_disabledItem == null)
            {
                _disabledItem = (RepositoryItem)e.RepositoryItem.Clone();
                _disabledItem.ReadOnly = true;
                _disabledItem.Enabled = false;
            }
            e.RepositoryItem = _disabledItem;
            //判断条件
            //var electric = (sender as GridView).GetRow(e.RowHandle) as SelectElectricShow;
            //if (electric == null)
            //    return;
            ////满足条件，设置成只读
            //if (electric.IsLimited)
            //    e.RepositoryItem = _disabledItem;
        }


        //计算颜色尺码数量
        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.StartsWith("SizeValue"))
            {
                var gv = sender as GridView;
                var selectedRow = gv.GetRow(gv.FocusedRowHandle) as ColorAndSizeModel;
                if (null != selectedRow)
                {
                    var total = 0;

                    for (var size = 0; size < LastSizeList.Count; size++)
                    {
                        var num = ReflectionUtils<ColorAndSizeModel>.GetPropertyValue(selectedRow, "SizeValue" + (size + 1));
                        if (!string.IsNullOrEmpty(num))
                        {
                            total += Convert.ToInt32(num);
                        }
                    }
                    selectedRow.Total = total;
                    gv.RefreshData();
                }
            }
        }

        #endregion

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            (gridControl2.MainView as GridView).ClearRows();
            //    CpOrderSizeList.Clear();

        }

        private void btnDeleteItem_Click_1(object sender, EventArgs e)
        {

            var gv = (gridControl2.MainView as GridView);
            var deleteRows = gv.GetSelectedRows();
            foreach (var inx in deleteRows)
            {
                var sm = gv.GetRow(index) as CusPurchColorAndSizeModel;
                _cusPurchColorSizeSourceList.RemoveAll(f => f.Id.Equals(sm?.Id) && f.ColorId.Equals(sm?.ColorId));
            }
            gv.ClearSelectRow();
            //var pc = gv.GetRow();
            //CpOrderSizeList.RemoveAll(f=>);
        }
    }
}
