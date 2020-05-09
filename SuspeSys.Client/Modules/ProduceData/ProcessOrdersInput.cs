﻿using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action.CustPurchaseOrder.Model;
using SuspeSys.Client.Action.ProcessOrder;
using SuspeSys.Client.Action.ProcessOrder.Model;
using SuspeSys.Client.Action.Query;
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
    public class ProcessOrdersInput : SusXtraUserControl
    {
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl gpOrderInfo;
        private DevExpress.XtraBars.BarManager barManager1;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnColorAndSize;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit txtProderOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ButtonEdit txtProductNoticeOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.ButtonEdit txtStyleCode;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ButtonEdit btnEditCustomer;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ButtonEdit txtCustomerStyleCode;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.ButtonEdit txtCustomerOrderNo;
        private DevExpress.XtraEditors.DateEdit txtGenaterOrderDate;
        private DevExpress.XtraEditors.DateEdit txtDeliveryDate;
        private DevExpress.XtraEditors.TextEdit txtStyleDesption;
        private DevExpress.XtraEditors.SimpleButton btnAddItem;
        private SusToolBar susToolBar1;

        public ProcessOrdersInput() : base()
        {
            InitializeComponent();
            RegisterClickEvent();
            RegisterEvent();

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.SimpleButton btnClear;
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnAddItem = new DevExpress.XtraEditors.SimpleButton();
            this.btnColorAndSize = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
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
            this.gpOrderInfo = new DevExpress.XtraEditors.GroupControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnEditCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.txtCustomerStyleCode = new DevExpress.XtraEditors.ButtonEdit();
            this.txtCustomerOrderNo = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.susToolBar1 = new SuspeSys.Client.Modules.SusToolBar();
            btnClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.gpOrderInfo)).BeginInit();
            this.gpOrderInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerStyleCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerOrderNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            btnClear.Location = new System.Drawing.Point(102, 5);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(93, 23);
            btnClear.TabIndex = 1;
            btnClear.Text = "清空";
            btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.groupControl3);
            this.pnlMain.Controls.Add(this.panelControl1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 51);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1142, 422);
            this.pnlMain.TabIndex = 0;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.panelControl2);
            this.groupControl3.Controls.Add(this.gridControl1);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(2, 156);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1138, 264);
            this.groupControl3.TabIndex = 1;
            this.groupControl3.Text = "制单信息";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnAddItem);
            this.panelControl2.Controls.Add(this.btnColorAndSize);
            this.panelControl2.Controls.Add(btnClear);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(2, 229);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1134, 33);
            this.panelControl2.TabIndex = 1;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(203, 5);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(86, 23);
            this.btnAddItem.TabIndex = 2;
            this.btnAddItem.Text = "追加一行";
            this.btnAddItem.Visible = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnColorAndSize
            // 
            this.btnColorAndSize.Location = new System.Drawing.Point(3, 5);
            this.btnColorAndSize.Name = "btnColorAndSize";
            this.btnColorAndSize.Size = new System.Drawing.Size(93, 23);
            this.btnColorAndSize.TabIndex = 1;
            this.btnColorAndSize.Text = "颜色和尺码";
            this.btnColorAndSize.Click += new System.EventHandler(this.btnColorAndSize_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 21);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1134, 241);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
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
            this.barDockControlTop.Size = new System.Drawing.Size(1142, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 473);
            this.barDockControlBottom.Size = new System.Drawing.Size(1142, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 473);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1142, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 473);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.gpOrderInfo);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1138, 154);
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
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1134, 80);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "基本信息";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "制单号";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(509, 27);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "下单日期";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(241, 53);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 14);
            this.labelControl10.TabIndex = 2;
            this.labelControl10.Text = "款式描述";
            // 
            // txtProderOrderNo
            // 
            this.txtProderOrderNo.Location = new System.Drawing.Point(63, 24);
            this.txtProderOrderNo.MenuManager = this.barManager1;
            this.txtProderOrderNo.Name = "txtProderOrderNo";
            this.txtProderOrderNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtProderOrderNo.Size = new System.Drawing.Size(160, 20);
            this.txtProderOrderNo.TabIndex = 1;
            // 
            // txtStyleDesption
            // 
            this.txtStyleDesption.Location = new System.Drawing.Point(301, 50);
            this.txtStyleDesption.MenuManager = this.barManager1;
            this.txtStyleDesption.Name = "txtStyleDesption";
            this.txtStyleDesption.Size = new System.Drawing.Size(658, 20);
            this.txtStyleDesption.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(229, 24);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "生产通知单";
            // 
            // txtStyleCode
            // 
            this.txtStyleCode.Location = new System.Drawing.Point(63, 50);
            this.txtStyleCode.Name = "txtStyleCode";
            this.txtStyleCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtStyleCode.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtStyleCode_Properties_ButtonClick);
            this.txtStyleCode.Size = new System.Drawing.Size(160, 20);
            this.txtStyleCode.TabIndex = 1;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(20, 53);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(24, 14);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "款号";
            // 
            // txtProductNoticeOrderNo
            // 
            this.txtProductNoticeOrderNo.Location = new System.Drawing.Point(301, 27);
            this.txtProductNoticeOrderNo.Name = "txtProductNoticeOrderNo";
            this.txtProductNoticeOrderNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtProductNoticeOrderNo.Size = new System.Drawing.Size(160, 20);
            this.txtProductNoticeOrderNo.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(745, 27);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "交货日期";
            // 
            // txtGenaterOrderDate
            // 
            this.txtGenaterOrderDate.EditValue = null;
            this.txtGenaterOrderDate.Location = new System.Drawing.Point(572, 24);
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
            this.txtGenaterOrderDate.Size = new System.Drawing.Size(160, 20);
            this.txtGenaterOrderDate.TabIndex = 1;
            // 
            // txtDeliveryDate
            // 
            this.txtDeliveryDate.EditValue = null;
            this.txtDeliveryDate.Location = new System.Drawing.Point(799, 24);
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
            this.txtDeliveryDate.Size = new System.Drawing.Size(160, 20);
            this.txtDeliveryDate.TabIndex = 1;
            // 
            // gpOrderInfo
            // 
            this.gpOrderInfo.Controls.Add(this.labelControl5);
            this.gpOrderInfo.Controls.Add(this.btnEditCustomer);
            this.gpOrderInfo.Controls.Add(this.txtCustomerStyleCode);
            this.gpOrderInfo.Controls.Add(this.txtCustomerOrderNo);
            this.gpOrderInfo.Controls.Add(this.labelControl8);
            this.gpOrderInfo.Controls.Add(this.labelControl7);
            this.gpOrderInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpOrderInfo.Location = new System.Drawing.Point(2, 88);
            this.gpOrderInfo.Name = "gpOrderInfo";
            this.gpOrderInfo.Size = new System.Drawing.Size(1134, 64);
            this.gpOrderInfo.TabIndex = 1;
            this.gpOrderInfo.Text = "订单信息";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(20, 24);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "客户";
            // 
            // btnEditCustomer
            // 
            this.btnEditCustomer.Location = new System.Drawing.Point(63, 24);
            this.btnEditCustomer.Name = "btnEditCustomer";
            this.btnEditCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditCustomer.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEditCustomer_Properties_ButtonClick_1);
            this.btnEditCustomer.Size = new System.Drawing.Size(160, 20);
            this.btnEditCustomer.TabIndex = 1;
            // 
            // txtCustomerStyleCode
            // 
            this.txtCustomerStyleCode.Location = new System.Drawing.Point(301, 24);
            this.txtCustomerStyleCode.Name = "txtCustomerStyleCode";
            this.txtCustomerStyleCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomerStyleCode.Size = new System.Drawing.Size(160, 20);
            this.txtCustomerStyleCode.TabIndex = 1;
            // 
            // txtCustomerOrderNo
            // 
            this.txtCustomerOrderNo.Location = new System.Drawing.Point(572, 24);
            this.txtCustomerOrderNo.Name = "txtCustomerOrderNo";
            this.txtCustomerOrderNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomerOrderNo.Size = new System.Drawing.Size(160, 20);
            this.txtCustomerOrderNo.TabIndex = 1;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(521, 24);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(36, 14);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "订单号";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(241, 24);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(48, 14);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "客户款号";
            // 
            // susToolBar1
            // 
            this.susToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.susToolBar1.Location = new System.Drawing.Point(0, 0);
            this.susToolBar1.Name = "susToolBar1";
            this.susToolBar1.ShowAddButton = false;
            this.susToolBar1.ShowCancelButton = false;
            this.susToolBar1.ShowDeleteButton = false;
            this.susToolBar1.ShowExportButton = true;
            this.susToolBar1.ShowMaxButton = true;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowRefreshButton = false;
            this.susToolBar1.ShowSaveAndAddButton = true;
            this.susToolBar1.ShowSaveAndCloseButton = true;
            this.susToolBar1.ShowSaveButton = true;
            this.susToolBar1.Size = new System.Drawing.Size(1142, 51);
            this.susToolBar1.TabIndex = 1;
            // 
            // ProcessOrdersInput
            // 
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.susToolBar1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ProcessOrdersInput";
            this.Load += new System.EventHandler(this.ProcessOrdersInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.gpOrderInfo)).EndInit();
            this.gpOrderInfo.ResumeLayout(false);
            this.gpOrderInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerStyleCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerOrderNo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void RegisterClickEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
        }
        private CommonQueryAction<DaoModel.ProcessOrder> commonQueryAction = new CommonQueryAction<ProcessOrder>();
        private ProcessOrderAction processOrderAction = new ProcessOrderAction();
        private ProcessOrderQueryAction processOrderQueryAction = new ProcessOrderQueryAction();
        //保存/保存并新增
        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            DaoModel.ProcessOrder m = null;
            try
            {
                switch (ButtonName)
                {
                    case ButtonName.Save:
                    case ButtonName.SaveAndAdd:
                    case ButtonName.SaveAndClose:
                        m=SaveProcessOrder();
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
                }
                if (ButtonName == ButtonName.Save)
                {
                    txtProderOrderNo.Enabled = false;
                    EditProcessOrderModel =m;
                }
                if (ButtonName == ButtonName.SaveAndClose)
                {
                    ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                }
                if (ButtonName == ButtonName.SaveAndAdd)
                {
                    var processOrder = new ProcessOrdersInput() { Dock = DockStyle.Fill };
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
        DaoModel.ProcessOrder SaveProcessOrder()
        {
            processOrderAction.Model = GetProcessOrderModel();
            var pOrderNo = processOrderAction.Model.ProcessOrder.POrderNo?.Trim();
            if (string.IsNullOrEmpty(pOrderNo))
            {
                XtraMessageBox.Show("制单号不能为空!", "温馨提示");
                return null;
            }
            if (null == EditProcessOrderModel)
            {
                if (processOrderQueryAction.CheckProcessOrderNoIsExist(pOrderNo))
                {
                    XtraMessageBox.Show("制单号已存在,请重新输入!", "温馨提示");
                    txtProderOrderNo.Focus();
                    return null;
                }
            }

            if (null == EditProcessOrderModel)
                processOrderAction.AddProcessOrder();
            else
                processOrderAction.UpdateProcessOrder();
            //XtraMessageBox.Show("保存成功!", "提示");
            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            return processOrderAction.Model.ProcessOrder;
        }
        /// <summary>
        /// 获取Input制单Model
        /// </summary>
        /// <returns></returns>
        Client.Action.ProcessOrder.Model.ProcessOrderModel GetProcessOrderModel()
        {
            var processOrder = new ProcessOrder();
            processOrder.Id = EditProcessOrderModel?.Id;
            processOrder.POrderType = ProcessOrderType.Single.Value;
            processOrder.POrderTypeDesption = ProcessOrderType.Single.Desption;
            processOrder.POrderNo = txtProderOrderNo.Text.Trim();
            processOrder.Status = ProcessOrderStatus.WaitingAudit.Value;
            processOrder.ProductNoticeOrderNo = txtProductNoticeOrderNo.Text.Trim();
            processOrder.GenaterOrderDate = txtGenaterOrderDate.DateTime.FormatDate();
            processOrder.DeliveryDate = txtDeliveryDate.DateTime.FormatDate();
            processOrder.Style = txtStyleCode.Tag as Style;
            processOrder.StyleCode = txtStyleCode.Text.Trim();
            processOrder.StyleName = (txtStyleCode.Tag as Style)?.StyleName;
            var selCustomer = btnEditCustomer.Tag as DaoModel.Customer;
            processOrder.Customer = selCustomer;
            processOrder.CustomerName = selCustomer?.CusName;
            processOrder.CustomerStyle = txtCustomerStyleCode.EditValue?.ToString();
            processOrder.CustomerNo = selCustomer?.CusNo;

            //制单颜色
            var processOrderColorList = ProcessOrderColorList ?? new List<PoColor>();
            var processOrderColorItemList = new List<DaoModel.ProcessOrderColorItemModel>();
            foreach (var item in processOrderColorList)
            {
                var processOrderColorItem = new DaoModel.ProcessOrderColorItemModel() { PoColor = item, Color = item.ColorValue, ColorDescription = item.ColorDescption };
                processOrderColorItemList.Add(processOrderColorItem);

            }
            //颜色大小明细
            var inputColorAndSizeList = (gridControl1.DataSource as List<ColorAndSizeModel>) ?? new List<ColorAndSizeModel>();

            //var processOrderColorSizeItemList = new List<DaoModel.ProcessOrderColorSizeItem>();
            foreach (var cr in processOrderColorItemList)
            {
                cr.ProcessOrderColorSizeItemList = new List<ProcessOrderColorSizeItem>();
                foreach (var item in inputColorAndSizeList)
                {
                    if (cr.PoColor.Id.Equals(item.ColorId))
                    {
                        var colorTotal = 0;
                        for (var i = 1; i < item.SizeColumnCount + 1; i++)
                        {
                            var filedName = "SizeValue" + i;
                            var cSize = GridControlHelper.GetColumnTag(gridControl1, filedName) as PSize;
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
        public List<CusPurchColorAndSizeModel> ProcessOrderColorSizeList { set; get; }
        public List<PoColor> ProcessOrderColorList { get; internal set; }
        public List<PSize> ProcessOrderSizeList { get; internal set; }
        public ProcessOrder EditProcessOrderModel { get; internal set; }
        public XtraUserControl1 ucMain { get; internal set; }
        public StyleProcessFlowStoreModel StyleProcessFlow { get; internal set; }

        private void btnColorAndSize_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var csDialog = new ColorAndSizeDialog(this);
            csDialog.GridControlColorSizes = gridControl1;
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
                GridView gv = (gridControl1.MainView as GridView);
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
                GridView gv = (gridControl1.MainView as GridView);
                // int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                //3.保存选中值到datatable
                //dt.Rows[dataIndex]["value"] = value;
                //dt.Rows[dataIndex]["text"] = text;
                gv.SetFocusedRowCellValue("ColorDescption", (item.Tag as PoColor)?.ColorDescption);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMessageBox.Show(ex.Message, "提示");
            }
        }

        private void txtStyleCode_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var styleDialog = new StyleSelectDialog();
            styleDialog.ShowDialog();
            //txtStyleCode.EditValue = styleDialog.SelectedStyle;
            txtStyleCode.Tag = styleDialog.SelectedStyle;
            txtStyleCode.Text = styleDialog.SelectedStyle?.StyleNo;
            txtStyleDesption.Text = styleDialog.SelectedStyle?.StyleName;
            Cursor = Cursors.Default;
        }


        void RegisterEvent()
        {
            var gv = gridControl1.MainView as GridView;
            if (null != gv)
            {
                gv.CellValueChanged += Gv_CellValueChanged;
                gv.CustomRowCellEdit += Gv_CustomRowCellEdit;
            }
        }
        RepositoryItem _disabledItem;
        private void Gv_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (!e.Column.FieldName.Equals("Total"))//需要设置的列名
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
        // }

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

                    for (var size = 0; size < ProcessOrderSizeList.Count; size++)
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

                    btnEditCustomer.Tag = EditProcessOrderModel.Customer;
                    btnEditCustomer.Text = EditProcessOrderModel.CustomerName;
                    txtCustomerStyleCode.Text = EditProcessOrderModel.CustomerStyle?.Trim();
                    txtCustomerOrderNo.Text = EditProcessOrderModel.CustomerNo?.Trim();

                    //制单明细
                    var queryAction = new ProcessOrderQueryAction();
                    queryAction.GetProcessOrderItem(EditProcessOrderModel.Id);
                    var pOrderColorList = queryAction.Model.ProcessOrderColorSizeItemList;

                    var cusPusOrderColorAndSizeList = new List<CusPurchColorAndSizeModel>();
                    foreach (var item in pOrderColorList)
                    {
                        var r = BeanUitls<CusPurchColorAndSizeModel, DaoModel.ProcessOrderColorItemModel>.Mapper(item);
                        cusPusOrderColorAndSizeList.Add(r);
                    }

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
                        m.Id = item.CustomerpurchaseorderId;
                        pColorList.Add(item.PoColor);
                        var index = 1;
                        foreach (var size in item.ProcessOrderColorSizeItemList)
                        {
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
            var gv = gridControl1.MainView as GridView;
            gv.Columns.Clear();
            gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName= "No",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true},
            });
            int j = 1;
            foreach (var size in selectedSizeList)
            {
                gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = size.Size, Tag = size, FieldName = "SizeValue" + (j++), Visible = true });
            }
            gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "数量合计", FieldName = "Total", Visible = true });
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gv});
            gridControl1.MainView = gv;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gv.BestFitColumns();//按照列宽度自动适配
            gv.GridControl = gridControl1;
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
            gridControl1.DataSource = ProcessOrderColorSizeList;
            gridControl1.MainView.RefreshData();
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
            btnEditCustomer.Tag = customerDialog.SelectedCustomer;
            btnEditCustomer.Text = customerDialog.SelectedCustomer?.CusName;
            Cursor = Cursors.Default;
        }
        int index = 1;
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            var cs = new ColorAndSizeModel() { No = index.ToString() };
            index++;
            var dt = gridControl1.MainView.DataSource as List<ColorAndSizeModel>;
            if (null == dt)
            {

                dt = new List<ColorAndSizeModel>();
                dt.Add(cs);
                gridControl1.DataSource = dt;
                return;
            }

            dt.Add(cs);
            //gridControl1.DataSource = dt;
            gridControl1.MainView.RefreshData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            (gridControl1.MainView as GridView)?.ClearRows();
        }
    }
}