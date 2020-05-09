using SuspeSys.Client.Modules.Ext;
using SuspeSys.Domain;

namespace SuspeSys.Client.Modules
{
    partial class SusQueryControl
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.rgDateRange = new DevExpress.XtraEditors.RadioGroup();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txtFlowSelection = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateBegin = new DevExpress.XtraEditors.DateEdit();
            this.dateEnd = new DevExpress.XtraEditors.DateEdit();
            this.txtProcessOrderNo = new DevExpress.XtraEditors.PopupContainerEdit();
            this.txtColor = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtSize = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtWorkshop = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtGroupNo = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtPO = new DevExpress.XtraEditors.PopupContainerEdit();
            this.txtStyleCode = new DevExpress.XtraEditors.PopupContainerEdit();
            this.txtEmployeeName = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgDateRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlowSelection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkshop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmployeeName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.rgDateRange);
            this.panelControl2.Controls.Add(this.btnQuery);
            this.panelControl2.Controls.Add(this.txtFlowSelection);
            this.panelControl2.Controls.Add(this.labelControl10);
            this.panelControl2.Controls.Add(this.labelControl11);
            this.panelControl2.Controls.Add(this.labelControl8);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.labelControl9);
            this.panelControl2.Controls.Add(this.labelControl7);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.dateBegin);
            this.panelControl2.Controls.Add(this.dateEnd);
            this.panelControl2.Controls.Add(this.txtProcessOrderNo);
            this.panelControl2.Controls.Add(this.txtColor);
            this.panelControl2.Controls.Add(this.txtSize);
            this.panelControl2.Controls.Add(this.txtWorkshop);
            this.panelControl2.Controls.Add(this.txtGroupNo);
            this.panelControl2.Controls.Add(this.txtPO);
            this.panelControl2.Controls.Add(this.txtStyleCode);
            this.panelControl2.Controls.Add(this.txtEmployeeName);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1480, 58);
            this.panelControl2.TabIndex = 3;
            // 
            // rgDateRange
            // 
            this.rgDateRange.Location = new System.Drawing.Point(1048, 14);
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
            this.btnQuery.Location = new System.Drawing.Point(1365, 26);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(76, 23);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtFlowSelection
            // 
            this.txtFlowSelection.Location = new System.Drawing.Point(231, 30);
            this.txtFlowSelection.Name = "txtFlowSelection";
            this.txtFlowSelection.Size = new System.Drawing.Size(116, 20);
            this.txtFlowSelection.TabIndex = 2;
            this.txtFlowSelection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessOrderNo_KeyDown);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(862, 38);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 14);
            this.labelControl10.TabIndex = 1;
            this.labelControl10.Text = "结束日期";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(713, 12);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(24, 14);
            this.labelControl11.TabIndex = 1;
            this.labelControl11.Text = "员工";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(528, 33);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(24, 14);
            this.labelControl8.TabIndex = 1;
            this.labelControl8.Text = "组别";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(353, 33);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.TabIndex = 1;
            this.labelControl6.Text = "尺码";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(178, 30);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "工段";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(862, 13);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(48, 14);
            this.labelControl9.TabIndex = 1;
            this.labelControl9.Text = "开始日期";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(528, 7);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 14);
            this.labelControl7.TabIndex = 1;
            this.labelControl7.Text = "车间";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(353, 7);
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
            this.labelControl3.Location = new System.Drawing.Point(175, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 14);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "PO号";
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
            this.dateBegin.Location = new System.Drawing.Point(926, 11);
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
            this.dateBegin.Size = new System.Drawing.Size(116, 20);
            this.dateBegin.TabIndex = 2;
            this.dateBegin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessOrderNo_KeyDown);
            // 
            // dateEnd
            // 
            this.dateEnd.EditValue = null;
            this.dateEnd.Location = new System.Drawing.Point(926, 35);
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
            this.dateEnd.Size = new System.Drawing.Size(116, 20);
            this.dateEnd.TabIndex = 2;
            this.dateEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessOrderNo_KeyDown);
            // 
            // txtProcessOrderNo
            // 
            this.txtProcessOrderNo.Location = new System.Drawing.Point(56, 3);
            this.txtProcessOrderNo.Name = "txtProcessOrderNo";
            this.txtProcessOrderNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtProcessOrderNo.Size = new System.Drawing.Size(116, 20);
            this.txtProcessOrderNo.TabIndex = 2;
            this.txtProcessOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessOrderNo_KeyDown);
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(406, 7);
            this.txtColor.Name = "txtColor";
            this.txtColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtColor.Size = new System.Drawing.Size(116, 20);
            this.txtColor.TabIndex = 2;
            this.txtColor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessOrderNo_KeyDown);
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(406, 30);
            this.txtSize.Name = "txtSize";
            this.txtSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSize.Size = new System.Drawing.Size(116, 20);
            this.txtSize.TabIndex = 2;
            this.txtSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessOrderNo_KeyDown);
            // 
            // txtWorkshop
            // 
            this.txtWorkshop.Location = new System.Drawing.Point(581, 7);
            this.txtWorkshop.Name = "txtWorkshop";
            this.txtWorkshop.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtWorkshop.Size = new System.Drawing.Size(116, 20);
            this.txtWorkshop.TabIndex = 2;
            this.txtWorkshop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessOrderNo_KeyDown);
            // 
            // txtGroupNo
            // 
            this.txtGroupNo.Location = new System.Drawing.Point(581, 30);
            this.txtGroupNo.Name = "txtGroupNo";
            this.txtGroupNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtGroupNo.Size = new System.Drawing.Size(116, 20);
            this.txtGroupNo.TabIndex = 2;
            this.txtGroupNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessOrderNo_KeyDown);
            // 
            // txtPO
            // 
            this.txtPO.Location = new System.Drawing.Point(231, 4);
            this.txtPO.Name = "txtPO";
            this.txtPO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtPO.Size = new System.Drawing.Size(116, 20);
            this.txtPO.TabIndex = 2;
            this.txtPO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessOrderNo_KeyDown);
            // 
            // txtStyleCode
            // 
            this.txtStyleCode.Location = new System.Drawing.Point(56, 30);
            this.txtStyleCode.Name = "txtStyleCode";
            this.txtStyleCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtStyleCode.Size = new System.Drawing.Size(116, 20);
            this.txtStyleCode.TabIndex = 2;
            this.txtStyleCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessOrderNo_KeyDown);
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(743, 9);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEmployeeName.Size = new System.Drawing.Size(104, 20);
            this.txtEmployeeName.TabIndex = 2;
            this.txtEmployeeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessOrderNo_KeyDown);
            // 
            // SusQueryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Name = "SusQueryControl";
            this.Size = new System.Drawing.Size(1480, 58);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgDateRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFlowSelection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkshop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyleCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmployeeName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.RadioGroup rgDateRange;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.TextEdit txtFlowSelection;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateBegin;
        private DevExpress.XtraEditors.DateEdit dateEnd;
        private DevExpress.XtraEditors.PopupContainerEdit txtProcessOrderNo;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtColor;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtSize;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtWorkshop;
        private DevExpress.XtraEditors.CheckedComboBoxEdit txtGroupNo;
        private DevExpress.XtraEditors.PopupContainerEdit txtPO;
        private DevExpress.XtraEditors.PopupContainerEdit txtStyleCode;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.PopupContainerEdit txtEmployeeName;
    }
}
