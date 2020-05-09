namespace SuspeSys.Client.Modules.Reports
{
    partial class UCBaseReort
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
            this.pcContent = new DevExpress.XtraEditors.PanelControl();
            this.pcContentMain = new DevExpress.XtraEditors.PanelControl();
            this.pcContentTop = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.rgDateRange = new DevExpress.XtraEditors.RadioGroup();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txtGroupNo = new DevExpress.XtraEditors.TextEdit();
            this.txtSize = new DevExpress.XtraEditors.TextEdit();
            this.txtFlowSelection = new DevExpress.XtraEditors.TextEdit();
            this.txtStyleCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtWorkshop = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtColor = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtPO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtProcessOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateBegin = new DevExpress.XtraEditors.DateEdit();
            this.dateEnd = new DevExpress.XtraEditors.DateEdit();
            this.pcSusToolButton = new DevExpress.XtraEditors.PanelControl();
            this.susGrid1 = new SuspeSys.Client.Modules.SusGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pcContent)).BeginInit();
            this.pcContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcContentMain)).BeginInit();
            this.pcContentMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcContentTop)).BeginInit();
            this.pcContentTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgDateRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlowSelection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkshop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcSusToolButton)).BeginInit();
            this.pcSusToolButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // susToolBar1
            // 
            this.susToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.susToolBar1.Location = new System.Drawing.Point(2, 2);
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
            this.susToolBar1.Size = new System.Drawing.Size(1312, 45);
            this.susToolBar1.TabIndex = 0;
            // 
            // pcContent
            // 
            this.pcContent.Controls.Add(this.pcContentMain);
            this.pcContent.Controls.Add(this.pcContentTop);
            this.pcContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcContent.Location = new System.Drawing.Point(0, 53);
            this.pcContent.Name = "pcContent";
            this.pcContent.Size = new System.Drawing.Size(1316, 562);
            this.pcContent.TabIndex = 1;
            // 
            // pcContentMain
            // 
            this.pcContentMain.Controls.Add(this.susGrid1);
            this.pcContentMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcContentMain.Location = new System.Drawing.Point(2, 85);
            this.pcContentMain.Name = "pcContentMain";
            this.pcContentMain.Size = new System.Drawing.Size(1312, 475);
            this.pcContentMain.TabIndex = 1;
            // 
            // pcContentTop
            // 
            this.pcContentTop.Controls.Add(this.panelControl2);
            this.pcContentTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcContentTop.Location = new System.Drawing.Point(2, 2);
            this.pcContentTop.Name = "pcContentTop";
            this.pcContentTop.Size = new System.Drawing.Size(1312, 83);
            this.pcContentTop.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.rgDateRange);
            this.panelControl2.Controls.Add(this.btnQuery);
            this.panelControl2.Controls.Add(this.txtGroupNo);
            this.panelControl2.Controls.Add(this.txtSize);
            this.panelControl2.Controls.Add(this.txtFlowSelection);
            this.panelControl2.Controls.Add(this.txtStyleCode);
            this.panelControl2.Controls.Add(this.labelControl10);
            this.panelControl2.Controls.Add(this.labelControl8);
            this.panelControl2.Controls.Add(this.txtWorkshop);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.txtColor);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.labelControl9);
            this.panelControl2.Controls.Add(this.labelControl7);
            this.panelControl2.Controls.Add(this.txtPO);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.txtProcessOrderNo);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.dateBegin);
            this.panelControl2.Controls.Add(this.dateEnd);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1308, 79);
            this.panelControl2.TabIndex = 3;
            // 
            // rgDateRange
            // 
            this.rgDateRange.Location = new System.Drawing.Point(845, 8);
            this.rgDateRange.Name = "rgDateRange";
            this.rgDateRange.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("-1", "全部"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "今天"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Yesterday", "昨天"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("LatelyThreeDay", "最近三天"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("CurrentWeek", "本周"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("CurrentMonth", "本月"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("LastWeek", "上周"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("LastMonth", "上月")});
            this.rgDateRange.Size = new System.Drawing.Size(311, 41);
            this.rgDateRange.TabIndex = 4;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(1172, 21);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(76, 23);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            // 
            // txtGroupNo
            // 
            this.txtGroupNo.Location = new System.Drawing.Point(546, 32);
            this.txtGroupNo.Name = "txtGroupNo";
            this.txtGroupNo.Size = new System.Drawing.Size(93, 20);
            this.txtGroupNo.TabIndex = 2;
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(377, 30);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(99, 20);
            this.txtSize.TabIndex = 2;
            // 
            // txtFlowSelection
            // 
            this.txtFlowSelection.Location = new System.Drawing.Point(218, 30);
            this.txtFlowSelection.Name = "txtFlowSelection";
            this.txtFlowSelection.Size = new System.Drawing.Size(89, 20);
            this.txtFlowSelection.TabIndex = 2;
            // 
            // txtStyleCode
            // 
            this.txtStyleCode.Location = new System.Drawing.Point(56, 30);
            this.txtStyleCode.Name = "txtStyleCode";
            this.txtStyleCode.Size = new System.Drawing.Size(92, 20);
            this.txtStyleCode.TabIndex = 2;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(660, 34);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 14);
            this.labelControl10.TabIndex = 1;
            this.labelControl10.Text = "结束日期";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(493, 35);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(24, 14);
            this.labelControl8.TabIndex = 1;
            this.labelControl8.Text = "组别";
            // 
            // txtWorkshop
            // 
            this.txtWorkshop.Location = new System.Drawing.Point(546, 9);
            this.txtWorkshop.Name = "txtWorkshop";
            this.txtWorkshop.Size = new System.Drawing.Size(93, 20);
            this.txtWorkshop.TabIndex = 2;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(324, 33);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.TabIndex = 1;
            this.labelControl6.Text = "尺码";
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(377, 7);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(99, 20);
            this.txtColor.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(165, 30);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "工段";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(660, 9);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(48, 14);
            this.labelControl9.TabIndex = 1;
            this.labelControl9.Text = "开始日期";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(493, 9);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 14);
            this.labelControl7.TabIndex = 1;
            this.labelControl7.Text = "车间";
            // 
            // txtPO
            // 
            this.txtPO.Location = new System.Drawing.Point(218, 4);
            this.txtPO.Name = "txtPO";
            this.txtPO.Size = new System.Drawing.Size(89, 20);
            this.txtPO.TabIndex = 2;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(324, 7);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "颜色";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(3, 33);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "款号";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(162, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 14);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "PO号";
            // 
            // txtProcessOrderNo
            // 
            this.txtProcessOrderNo.Location = new System.Drawing.Point(56, 3);
            this.txtProcessOrderNo.Name = "txtProcessOrderNo";
            this.txtProcessOrderNo.Size = new System.Drawing.Size(92, 20);
            this.txtProcessOrderNo.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "制单号";
            // 
            // dateBegin
            // 
            this.dateBegin.EditValue = null;
            this.dateBegin.Location = new System.Drawing.Point(724, 7);
            this.dateBegin.Name = "dateBegin";
            this.dateBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBegin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBegin.Properties.DisplayFormat.FormatString = "";
            this.dateBegin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateBegin.Properties.EditFormat.FormatString = "";
            this.dateBegin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateBegin.Properties.Mask.EditMask = "";
            this.dateBegin.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateBegin.Size = new System.Drawing.Size(91, 20);
            this.dateBegin.TabIndex = 2;
            // 
            // dateEnd
            // 
            this.dateEnd.EditValue = null;
            this.dateEnd.Location = new System.Drawing.Point(724, 31);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Properties.DisplayFormat.FormatString = "";
            this.dateEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEnd.Properties.EditFormat.FormatString = "";
            this.dateEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEnd.Properties.Mask.EditMask = "";
            this.dateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEnd.Size = new System.Drawing.Size(91, 20);
            this.dateEnd.TabIndex = 2;
            // 
            // pcSusToolButton
            // 
            this.pcSusToolButton.Controls.Add(this.susToolBar1);
            this.pcSusToolButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcSusToolButton.Location = new System.Drawing.Point(0, 0);
            this.pcSusToolButton.Name = "pcSusToolButton";
            this.pcSusToolButton.Size = new System.Drawing.Size(1316, 53);
            this.pcSusToolButton.TabIndex = 2;
            // 
            // susGrid1
            // 
            this.susGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.susGrid1.Location = new System.Drawing.Point(2, 2);
            this.susGrid1.Name = "susGrid1";
            this.susGrid1.Size = new System.Drawing.Size(1308, 471);
            this.susGrid1.TabIndex = 0;
            // 
            // UCBaseReort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pcContent);
            this.Controls.Add(this.pcSusToolButton);
            this.Name = "UCBaseReort";
            this.Size = new System.Drawing.Size(1316, 615);
            ((System.ComponentModel.ISupportInitialize)(this.pcContent)).EndInit();
            this.pcContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcContentMain)).EndInit();
            this.pcContentMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcContentTop)).EndInit();
            this.pcContentTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgDateRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlowSelection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkshop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcSusToolButton)).EndInit();
            this.pcSusToolButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SusToolBar susToolBar1;
        private DevExpress.XtraEditors.PanelControl pcContent;
        private DevExpress.XtraEditors.PanelControl pcContentTop;
        private DevExpress.XtraEditors.PanelControl pcSusToolButton;
        private DevExpress.XtraEditors.PanelControl pcContentMain;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.RadioGroup rgDateRange;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.TextEdit txtGroupNo;
        private DevExpress.XtraEditors.TextEdit txtSize;
        private DevExpress.XtraEditors.TextEdit txtFlowSelection;
        private DevExpress.XtraEditors.TextEdit txtStyleCode;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtWorkshop;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtColor;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtPO;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtProcessOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateBegin;
        private DevExpress.XtraEditors.DateEdit dateEnd;
        private SusGrid susGrid1;
    }
}
