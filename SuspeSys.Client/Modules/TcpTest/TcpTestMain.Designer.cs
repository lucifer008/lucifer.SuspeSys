namespace SuspeSys.Client.Modules.TcpTest
{
    partial class TcpTestMain
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPublicMessage = new DevExpress.XtraEditors.SimpleButton();
            this.btnClientConected = new DevExpress.XtraEditors.SimpleButton();
            this.txtTcpPort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTcpPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnPublicMessage);
            this.panelControl1.Controls.Add(this.btnClientConected);
            this.panelControl1.Controls.Add(this.txtTcpPort);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1247, 65);
            this.panelControl1.TabIndex = 0;
            // 
            // btnPublicMessage
            // 
            this.btnPublicMessage.Location = new System.Drawing.Point(163, 23);
            this.btnPublicMessage.Name = "btnPublicMessage";
            this.btnPublicMessage.Size = new System.Drawing.Size(103, 23);
            this.btnPublicMessage.TabIndex = 8;
            this.btnPublicMessage.Text = "发布";
            this.btnPublicMessage.Click += new System.EventHandler(this.btnPublicMessage_Click);
            // 
            // btnClientConected
            // 
            this.btnClientConected.Location = new System.Drawing.Point(674, 21);
            this.btnClientConected.Name = "btnClientConected";
            this.btnClientConected.Size = new System.Drawing.Size(103, 23);
            this.btnClientConected.TabIndex = 8;
            this.btnClientConected.Text = "监听";
            this.btnClientConected.Click += new System.EventHandler(this.btnClientConected_Click);
            // 
            // txtTcpPort
            // 
            this.txtTcpPort.EditValue = "9999";
            this.txtTcpPort.Location = new System.Drawing.Point(563, 23);
            this.txtTcpPort.Name = "txtTcpPort";
            this.txtTcpPort.Size = new System.Drawing.Size(90, 20);
            this.txtTcpPort.TabIndex = 7;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(469, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 14);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "连接消息端口:";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 65);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new System.Drawing.Size(1247, 593);
            this.xtraTabControl1.TabIndex = 1;
            // 
            // TcpTestMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "TcpTestMain";
            this.Size = new System.Drawing.Size(1247, 658);
            this.Load += new System.EventHandler(this.TcpTestMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTcpPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraEditors.SimpleButton btnClientConected;
        private DevExpress.XtraEditors.TextEdit txtTcpPort;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnPublicMessage;
    }
}
