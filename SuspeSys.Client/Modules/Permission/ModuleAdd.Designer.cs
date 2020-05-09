namespace SuspeSys.Client.Modules.Permission
{
    partial class ModuleAdd
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtResourceName = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtResourceIdentifying = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSortNumber = new System.Windows.Forms.Label();
            this.textBox1 = new DevExpress.XtraEditors.TextEdit();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbResourceType = new System.Windows.Forms.ComboBox();
            this.btnEditParent = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResourceName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResourceIdentifying.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditParent.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "资源名：";
            // 
            // txtResourceName
            // 
            this.txtResourceName.Location = new System.Drawing.Point(132, 73);
            this.txtResourceName.Name = "txtResourceName";
            this.txtResourceName.Size = new System.Drawing.Size(288, 20);
            this.txtResourceName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "描述：";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(132, 148);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(288, 20);
            this.txtDescription.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "资源标识：";
            // 
            // txtResourceIdentifying
            // 
            this.txtResourceIdentifying.Location = new System.Drawing.Point(132, 109);
            this.txtResourceIdentifying.Name = "txtResourceIdentifying";
            this.txtResourceIdentifying.Size = new System.Drawing.Size(288, 20);
            this.txtResourceIdentifying.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "资源类型：";
            // 
            // txtSortNumber
            // 
            this.txtSortNumber.AutoSize = true;
            this.txtSortNumber.Location = new System.Drawing.Point(76, 239);
            this.txtSortNumber.Name = "txtSortNumber";
            this.txtSortNumber.Size = new System.Drawing.Size(43, 14);
            this.txtSortNumber.TabIndex = 0;
            this.txtSortNumber.Text = "描述：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(132, 235);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(288, 20);
            this.textBox1.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(112, 321);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "添 加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(270, 321);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "添 加";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "父级资源：";
            // 
            // cmbResourceType
            // 
            this.cmbResourceType.FormattingEnabled = true;
            this.cmbResourceType.Location = new System.Drawing.Point(132, 190);
            this.cmbResourceType.Name = "cmbResourceType";
            this.cmbResourceType.Size = new System.Drawing.Size(288, 22);
            this.cmbResourceType.TabIndex = 2;
            // 
            // btnEditParent
            // 
            this.btnEditParent.Location = new System.Drawing.Point(132, 34);
            this.btnEditParent.Name = "btnEditParent";
            this.btnEditParent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditParent.Size = new System.Drawing.Size(288, 20);
            this.btnEditParent.TabIndex = 4;
            // 
            // ModuleAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEditParent);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbResourceType);
            this.Controls.Add(this.txtResourceIdentifying);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtResourceName);
            this.Controls.Add(this.txtSortNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "ModuleAdd";
            this.Size = new System.Drawing.Size(507, 412);
            this.Load += new System.EventHandler(this.ModuleAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtResourceName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResourceIdentifying.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditParent.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtResourceName;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtResourceIdentifying;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label txtSortNumber;
        private DevExpress.XtraEditors.TextEdit textBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbResourceType;
        private DevExpress.XtraEditors.ButtonEdit btnEditParent;
    }
}