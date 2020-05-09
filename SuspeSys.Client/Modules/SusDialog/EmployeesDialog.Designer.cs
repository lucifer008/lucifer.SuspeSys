namespace SuspeSys.Client.Modules.SusDialog
{
    partial class EmployeesDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeesDialog));
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateGoToWork = new DevExpress.XtraEditors.DateEdit();
            this.dateGoOffWork = new DevExpress.XtraEditors.DateEdit();
            this.susGrid1 = new SuspeSys.Client.Modules.SusGrid();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateGoToWork.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateGoToWork.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateGoOffWork.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateGoOffWork.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(12, 12);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.SearchButton(),
            new DevExpress.XtraEditors.Repository.MRUButton(),
            new DevExpress.XtraEditors.Repository.ClearButton()});
            this.searchControl1.Properties.NullValuePrompt = "输入员工姓名、工号搜索";
            this.searchControl1.Properties.ShowDefaultButtonsMode = DevExpress.XtraEditors.Repository.ShowDefaultButtonsMode.AutoShowClear;
            this.searchControl1.Properties.ShowMRUButton = true;
            this.searchControl1.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.searchControl1_Properties_ButtonClick);
            this.searchControl1.Properties.Click += new System.EventHandler(this.searchControl1_Properties_Click);
            this.searchControl1.Size = new System.Drawing.Size(370, 20);
            this.searchControl1.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1078, 495);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 21);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "取消";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(990, 495);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 21);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(480, 502);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "上班时间";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(696, 502);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "下班时间";
            // 
            // dateGoToWork
            // 
            this.dateGoToWork.EditValue = null;
            this.dateGoToWork.Location = new System.Drawing.Point(547, 499);
            this.dateGoToWork.Name = "dateGoToWork";
            this.dateGoToWork.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateGoToWork.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateGoToWork.Size = new System.Drawing.Size(134, 20);
            this.dateGoToWork.TabIndex = 9;
            // 
            // dateGoOffWork
            // 
            this.dateGoOffWork.EditValue = null;
            this.dateGoOffWork.Location = new System.Drawing.Point(759, 500);
            this.dateGoOffWork.Name = "dateGoOffWork";
            this.dateGoOffWork.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateGoOffWork.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateGoOffWork.Size = new System.Drawing.Size(134, 20);
            this.dateGoOffWork.TabIndex = 9;
            // 
            // susGrid1
            // 
            this.susGrid1.Location = new System.Drawing.Point(1, 39);
            this.susGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.susGrid1.Name = "susGrid1";
            this.susGrid1.Size = new System.Drawing.Size(1279, 443);
            this.susGrid1.TabIndex = 7;
            // 
            // EmployeesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 532);
            this.Controls.Add(this.dateGoOffWork);
            this.Controls.Add(this.dateGoToWork);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.susGrid1);
            this.Controls.Add(this.searchControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择员工";
            this.Load += new System.EventHandler(this.EmployeesDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateGoToWork.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateGoToWork.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateGoOffWork.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateGoOffWork.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SusGrid susGrid1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateGoToWork;
        private DevExpress.XtraEditors.DateEdit dateGoOffWork;
    }
}