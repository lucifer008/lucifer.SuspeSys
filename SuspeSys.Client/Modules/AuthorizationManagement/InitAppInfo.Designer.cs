namespace SuspeSys.Client.Modules.AuthorizationManagement
{
    partial class InitAppInfo
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
            this.txtConfig = new System.Windows.Forms.TextBox();
            this.btnInit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtConfig
            // 
            this.txtConfig.Location = new System.Drawing.Point(25, 13);
            this.txtConfig.Multiline = true;
            this.txtConfig.Name = "txtConfig";
            this.txtConfig.Size = new System.Drawing.Size(459, 212);
            this.txtConfig.TabIndex = 0;
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(201, 281);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 1;
            this.btnInit.Text = "确  定";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // InitAppInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 316);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.txtConfig);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InitAppInfo";
            this.Text = "初始化系统信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConfig;
        private System.Windows.Forms.Button btnInit;
    }
}