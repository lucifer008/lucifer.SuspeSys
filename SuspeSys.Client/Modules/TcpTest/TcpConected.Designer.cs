namespace SuspeSys.Client.Modules.TcpTest
{
    partial class TcpConected
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
            this.btnClientConected = new DevExpress.XtraEditors.SimpleButton();
            this.txtTcpPort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtTcpPort.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClientConected
            // 
            this.btnClientConected.Location = new System.Drawing.Point(233, 37);
            this.btnClientConected.Name = "btnClientConected";
            this.btnClientConected.Size = new System.Drawing.Size(103, 23);
            this.btnClientConected.TabIndex = 8;
            this.btnClientConected.Text = "连接服务器端";
            this.btnClientConected.Click += new System.EventHandler(this.btnClientConected_Click);
            // 
            // txtTcpPort
            // 
            this.txtTcpPort.EditValue = "9999";
            this.txtTcpPort.Location = new System.Drawing.Point(122, 39);
            this.txtTcpPort.Name = "txtTcpPort";
            this.txtTcpPort.Size = new System.Drawing.Size(90, 20);
            this.txtTcpPort.TabIndex = 7;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(28, 39);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 14);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "连接消息端口:";
            // 
            // TcpConected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 112);
            this.Controls.Add(this.btnClientConected);
            this.Controls.Add(this.txtTcpPort);
            this.Controls.Add(this.labelControl2);
            this.Name = "TcpConected";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TcpConected";
            ((System.ComponentModel.ISupportInitialize)(this.txtTcpPort.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClientConected;
        private DevExpress.XtraEditors.TextEdit txtTcpPort;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}