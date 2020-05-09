namespace SuspeSys.Client.Modules.AuthorizationManagement
{
    partial class RegIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegIndex));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCompanyName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtClientName = new DevExpress.XtraEditors.TextEdit();
            this.btnOnLine = new DevExpress.XtraEditors.SimpleButton();
            this.btnGenerate = new DevExpress.XtraEditors.SimpleButton();
            this.btnUse = new DevExpress.XtraEditors.SimpleButton();
            this.txtMessage = new DevExpress.XtraEditors.MemoEdit();
            this.lblEndDate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(63, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "客户名称";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(132, 20);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(332, 20);
            this.txtCompanyName.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(51, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "客户端名称";
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(132, 64);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(332, 20);
            this.txtClientName.TabIndex = 2;
            // 
            // btnOnLine
            // 
            this.btnOnLine.Location = new System.Drawing.Point(48, 135);
            this.btnOnLine.Name = "btnOnLine";
            this.btnOnLine.Size = new System.Drawing.Size(114, 48);
            this.btnOnLine.TabIndex = 3;
            this.btnOnLine.Text = "在线授权";
            this.btnOnLine.Click += new System.EventHandler(this.btnOnLine_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(203, 135);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(114, 48);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "生成申请文件";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnUse
            // 
            this.btnUse.Location = new System.Drawing.Point(379, 135);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(114, 48);
            this.btnUse.TabIndex = 5;
            this.btnUse.Text = "使用续期文件";
            this.btnUse.Click += new System.EventHandler(this.btnUse_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.EditValue = "提示：";
            this.txtMessage.Location = new System.Drawing.Point(13, 214);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Properties.LinesCount = 3;
            this.txtMessage.Size = new System.Drawing.Size(519, 96);
            this.txtMessage.TabIndex = 3;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Location = new System.Drawing.Point(132, 101);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(0, 14);
            this.lblEndDate.TabIndex = 0;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(63, 101);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "有效期至";
            // 
            // RegIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 195);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnUse);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnOnLine);
            this.Controls.Add(this.txtClientName);
            this.Controls.Add(this.txtCompanyName);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RegIndex";
            this.Text = "产品注册";
            this.Load += new System.EventHandler(this.RegIndex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCompanyName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtClientName;
        private DevExpress.XtraEditors.SimpleButton btnOnLine;
        private DevExpress.XtraEditors.SimpleButton btnGenerate;
        private DevExpress.XtraEditors.SimpleButton btnUse;
        private DevExpress.XtraEditors.MemoEdit txtMessage;
        private DevExpress.XtraEditors.LabelControl lblEndDate;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}