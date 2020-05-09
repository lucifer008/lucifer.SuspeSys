namespace SuspeSys.Client.Modules.ProductionLineSet
{
    partial class StatingInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatingInput));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtBeginNum = new DevExpress.XtraEditors.TextEdit();
            this.txtMainTrackNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtEndNum = new DevExpress.XtraEditors.TextEdit();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMainTrackNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndNum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(25, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "站点编号";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(37, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "主轨号";
            // 
            // txtBeginNum
            // 
            this.txtBeginNum.Location = new System.Drawing.Point(99, 20);
            this.txtBeginNum.Name = "txtBeginNum";
            this.txtBeginNum.Properties.Mask.EditMask = "[0-9]*";
            this.txtBeginNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtBeginNum.Size = new System.Drawing.Size(136, 20);
            this.txtBeginNum.TabIndex = 1;
            // 
            // txtMainTrackNumber
            // 
            this.txtMainTrackNumber.Enabled = true;
            this.txtMainTrackNumber.Location = new System.Drawing.Point(99, 64);
            this.txtMainTrackNumber.Name = "txtMainTrackNumber";
            this.txtMainTrackNumber.Properties.Mask.EditMask = "[0-9]*";
            this.txtMainTrackNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtMainTrackNumber.Size = new System.Drawing.Size(335, 20);
            this.txtMainTrackNumber.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(258, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(12, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "至";
            // 
            // txtEndNum
            // 
            this.txtEndNum.Location = new System.Drawing.Point(276, 17);
            this.txtEndNum.Name = "txtEndNum";
            this.txtEndNum.Properties.Mask.EditMask = "[0-9]*";
            this.txtEndNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtEndNum.Size = new System.Drawing.Size(158, 20);
            this.txtEndNum.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(66, 110);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(89, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(190, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // StatingInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 309);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtMainTrackNumber);
            this.Controls.Add(this.txtEndNum);
            this.Controls.Add(this.txtBeginNum);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StatingInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加站点";
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMainTrackNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndNum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtBeginNum;
        private DevExpress.XtraEditors.TextEdit txtMainTrackNumber;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtEndNum;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}