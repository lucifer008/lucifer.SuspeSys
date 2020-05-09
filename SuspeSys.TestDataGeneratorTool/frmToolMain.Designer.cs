namespace SuspeSys.TestDataGeneratorTool
{
    partial class frmToolMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerator = new System.Windows.Forms.Button();
            this.btnGenUserData = new System.Windows.Forms.Button();
            this.btnGenSystemPara = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.cbFanyiApi = new System.Windows.Forms.CheckBox();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnGenerator
            // 
            this.btnGenerator.Location = new System.Drawing.Point(53, 56);
            this.btnGenerator.Name = "btnGenerator";
            this.btnGenerator.Size = new System.Drawing.Size(75, 23);
            this.btnGenerator.TabIndex = 0;
            this.btnGenerator.Text = "生成";
            this.btnGenerator.UseVisualStyleBackColor = true;
            this.btnGenerator.Click += new System.EventHandler(this.btnGenerator_Click);
            // 
            // btnGenUserData
            // 
            this.btnGenUserData.Location = new System.Drawing.Point(164, 56);
            this.btnGenUserData.Name = "btnGenUserData";
            this.btnGenUserData.Size = new System.Drawing.Size(107, 23);
            this.btnGenUserData.TabIndex = 1;
            this.btnGenUserData.Text = "生成用户数据";
            this.btnGenUserData.UseVisualStyleBackColor = true;
            this.btnGenUserData.Click += new System.EventHandler(this.btnGenUserData_Click);
            // 
            // btnGenSystemPara
            // 
            this.btnGenSystemPara.Location = new System.Drawing.Point(300, 56);
            this.btnGenSystemPara.Name = "btnGenSystemPara";
            this.btnGenSystemPara.Size = new System.Drawing.Size(107, 23);
            this.btnGenSystemPara.TabIndex = 1;
            this.btnGenSystemPara.Text = "生成系统参数";
            this.btnGenSystemPara.UseVisualStyleBackColor = true;
            this.btnGenSystemPara.Click += new System.EventHandler(this.btnGenSystemPara_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "xls files (*.xls)|*.xls|xlsx files (*.xlsx)|*.xlsx";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(53, 175);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(301, 21);
            this.textBox1.TabIndex = 2;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(381, 175);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // cbFanyiApi
            // 
            this.cbFanyiApi.AutoSize = true;
            this.cbFanyiApi.Checked = true;
            this.cbFanyiApi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFanyiApi.Location = new System.Drawing.Point(300, 105);
            this.cbFanyiApi.Name = "cbFanyiApi";
            this.cbFanyiApi.Size = new System.Drawing.Size(120, 16);
            this.cbFanyiApi.TabIndex = 4;
            this.cbFanyiApi.Text = "是否调用接口翻译";
            this.cbFanyiApi.UseVisualStyleBackColor = true;
            this.cbFanyiApi.CheckedChanged += new System.EventHandler(this.cbFanyiApi_CheckedChanged);
            // 
            // cbAll
            // 
            this.cbAll.AutoSize = true;
            this.cbAll.Checked = true;
            this.cbAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAll.Location = new System.Drawing.Point(426, 105);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(108, 16);
            this.cbAll.TabIndex = 4;
            this.cbAll.Text = "是否只全部更新";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.CheckedChanged += new System.EventHandler(this.cbAll_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 301);
            this.Controls.Add(this.cbAll);
            this.Controls.Add(this.cbFanyiApi);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnGenSystemPara);
            this.Controls.Add(this.btnGenUserData);
            this.Controls.Add(this.btnGenerator);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerator;
        private System.Windows.Forms.Button btnGenUserData;
        private System.Windows.Forms.Button btnGenSystemPara;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.CheckBox cbFanyiApi;
        private System.Windows.Forms.CheckBox cbAll;
    }
}

