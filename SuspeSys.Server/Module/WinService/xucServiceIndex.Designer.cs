namespace SuspeSys.Server.Module.WinService
{
    partial class xucServiceIndex
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtBusinessPort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNetServerPort = new DevExpress.XtraEditors.TextEdit();
            this.btnSend3 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSend2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnSend1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnServerClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnServerTest = new DevExpress.XtraEditors.SimpleButton();
            this.btnCloseClient = new DevExpress.XtraEditors.SimpleButton();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.btnStopService = new DevExpress.XtraEditors.SimpleButton();
            this.btnStartService = new DevExpress.XtraEditors.SimpleButton();
            this.btnUninstallService = new DevExpress.XtraEditors.SimpleButton();
            this.btnInstallService = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServcePath = new DevExpress.XtraEditors.ButtonEdit();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.redisIP = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtRedisIP = new DevExpress.XtraEditors.TextEdit();
            this.txtRedisPort = new DevExpress.XtraEditors.TextEdit();
            this.btnSet = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetServerPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServcePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRedisIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRedisPort.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1086, 558);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "服务管理";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.btnSend3);
            this.panelControl1.Controls.Add(this.btnSend2);
            this.panelControl1.Controls.Add(this.btnSend1);
            this.panelControl1.Controls.Add(this.btnServerClose);
            this.panelControl1.Controls.Add(this.btnServerTest);
            this.panelControl1.Controls.Add(this.btnCloseClient);
            this.panelControl1.Controls.Add(this.btnTest);
            this.panelControl1.Controls.Add(this.btnStopService);
            this.panelControl1.Controls.Add(this.btnStartService);
            this.panelControl1.Controls.Add(this.btnUninstallService);
            this.panelControl1.Controls.Add(this.btnInstallService);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.txtServcePath);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 21);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1082, 535);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnSet);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.txtBusinessPort);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.redisIP);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.txtRedisPort);
            this.groupControl2.Controls.Add(this.txtRedisIP);
            this.groupControl2.Controls.Add(this.txtNetServerPort);
            this.groupControl2.Location = new System.Drawing.Point(14, 193);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1032, 216);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "业务端口管理";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 46);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "悬挂业务端口";
            // 
            // txtBusinessPort
            // 
            this.txtBusinessPort.EditValue = "9999";
            this.txtBusinessPort.Location = new System.Drawing.Point(127, 43);
            this.txtBusinessPort.Name = "txtBusinessPort";
            this.txtBusinessPort.Size = new System.Drawing.Size(180, 20);
            this.txtBusinessPort.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 88);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "CAN服务端端口";
            // 
            // txtNetServerPort
            // 
            this.txtNetServerPort.EditValue = "9998";
            this.txtNetServerPort.Location = new System.Drawing.Point(127, 82);
            this.txtNetServerPort.Name = "txtNetServerPort";
            this.txtNetServerPort.Size = new System.Drawing.Size(180, 20);
            this.txtNetServerPort.TabIndex = 0;
            // 
            // btnSend3
            // 
            this.btnSend3.Location = new System.Drawing.Point(890, 120);
            this.btnSend3.Name = "btnSend3";
            this.btnSend3.Size = new System.Drawing.Size(97, 23);
            this.btnSend3.TabIndex = 2;
            this.btnSend3.Text = "推送3号主轨";
            this.btnSend3.Visible = false;
            this.btnSend3.Click += new System.EventHandler(this.btnSend3_Click);
            // 
            // btnSend2
            // 
            this.btnSend2.Location = new System.Drawing.Point(890, 149);
            this.btnSend2.Name = "btnSend2";
            this.btnSend2.Size = new System.Drawing.Size(97, 23);
            this.btnSend2.TabIndex = 2;
            this.btnSend2.Text = "推送2号主轨";
            this.btnSend2.Visible = false;
            this.btnSend2.Click += new System.EventHandler(this.btnSend2_Click);
            // 
            // btnSend1
            // 
            this.btnSend1.Location = new System.Drawing.Point(775, 149);
            this.btnSend1.Name = "btnSend1";
            this.btnSend1.Size = new System.Drawing.Size(97, 23);
            this.btnSend1.TabIndex = 2;
            this.btnSend1.Text = "推送1号主轨";
            this.btnSend1.Visible = false;
            this.btnSend1.Click += new System.EventHandler(this.btnSend1_Click);
            // 
            // btnServerClose
            // 
            this.btnServerClose.Enabled = false;
            this.btnServerClose.Location = new System.Drawing.Point(628, 149);
            this.btnServerClose.Name = "btnServerClose";
            this.btnServerClose.Size = new System.Drawing.Size(97, 23);
            this.btnServerClose.TabIndex = 2;
            this.btnServerClose.Text = "关闭server端";
            this.btnServerClose.Click += new System.EventHandler(this.btnServerClose_Click);
            // 
            // btnServerTest
            // 
            this.btnServerTest.Location = new System.Drawing.Point(503, 149);
            this.btnServerTest.Name = "btnServerTest";
            this.btnServerTest.Size = new System.Drawing.Size(97, 23);
            this.btnServerTest.TabIndex = 2;
            this.btnServerTest.Text = "server端测试";
            this.btnServerTest.Click += new System.EventHandler(this.btnServerTest_Click);
            // 
            // btnCloseClient
            // 
            this.btnCloseClient.Location = new System.Drawing.Point(673, 88);
            this.btnCloseClient.Name = "btnCloseClient";
            this.btnCloseClient.Size = new System.Drawing.Size(97, 23);
            this.btnCloseClient.TabIndex = 2;
            this.btnCloseClient.Text = "关闭客户端";
            this.btnCloseClient.Visible = false;
            this.btnCloseClient.Click += new System.EventHandler(this.btnCloseClient_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(559, 88);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(97, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "客户端测试";
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnStopService
            // 
            this.btnStopService.Location = new System.Drawing.Point(387, 107);
            this.btnStopService.Name = "btnStopService";
            this.btnStopService.Size = new System.Drawing.Size(97, 23);
            this.btnStopService.TabIndex = 2;
            this.btnStopService.Text = "停止服务";
            this.btnStopService.Click += new System.EventHandler(this.btnStopService_Click);
            // 
            // btnStartService
            // 
            this.btnStartService.Location = new System.Drawing.Point(265, 107);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(97, 23);
            this.btnStartService.TabIndex = 2;
            this.btnStartService.Text = "启动服务";
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            // 
            // btnUninstallService
            // 
            this.btnUninstallService.Location = new System.Drawing.Point(141, 107);
            this.btnUninstallService.Name = "btnUninstallService";
            this.btnUninstallService.Size = new System.Drawing.Size(97, 23);
            this.btnUninstallService.TabIndex = 2;
            this.btnUninstallService.Text = "卸载服务";
            this.btnUninstallService.Click += new System.EventHandler(this.btnUninstallService_Click);
            // 
            // btnInstallService
            // 
            this.btnInstallService.Location = new System.Drawing.Point(27, 107);
            this.btnInstallService.Name = "btnInstallService";
            this.btnInstallService.Size = new System.Drawing.Size(97, 23);
            this.btnInstallService.TabIndex = 2;
            this.btnInstallService.Text = "安装服务";
            this.btnInstallService.Click += new System.EventHandler(this.btnInstallService_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务路径";
            // 
            // txtServcePath
            // 
            this.txtServcePath.Enabled = false;
            this.txtServcePath.Location = new System.Drawing.Point(85, 38);
            this.txtServcePath.Name = "txtServcePath";
            this.txtServcePath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "选择服务", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.txtServcePath.Size = new System.Drawing.Size(432, 20);
            this.txtServcePath.TabIndex = 1;
            this.txtServcePath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtServcePath_ButtonClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // redisIP
            // 
            this.redisIP.Location = new System.Drawing.Point(24, 123);
            this.redisIP.Name = "redisIP";
            this.redisIP.Size = new System.Drawing.Size(47, 14);
            this.redisIP.TabIndex = 1;
            this.redisIP.Text = "Redis IP:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(24, 158);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Redis 端口:";
            // 
            // txtRedisIP
            // 
            this.txtRedisIP.EditValue = "127.0.0.1";
            this.txtRedisIP.Location = new System.Drawing.Point(127, 120);
            this.txtRedisIP.Name = "txtRedisIP";
            this.txtRedisIP.Size = new System.Drawing.Size(180, 20);
            this.txtRedisIP.TabIndex = 0;
            // 
            // txtRedisPort
            // 
            this.txtRedisPort.EditValue = "9998";
            this.txtRedisPort.Location = new System.Drawing.Point(127, 152);
            this.txtRedisPort.Name = "txtRedisPort";
            this.txtRedisPort.Size = new System.Drawing.Size(180, 20);
            this.txtRedisPort.TabIndex = 0;
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(337, 149);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(80, 23);
            this.btnSet.TabIndex = 2;
            this.btnSet.Text = "重置";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // xucServiceIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "xucServiceIndex";
            this.Size = new System.Drawing.Size(1086, 558);
            this.Load += new System.EventHandler(this.xucServiceIndex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetServerPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServcePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRedisIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRedisPort.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.ButtonEdit txtServcePath;
        private DevExpress.XtraEditors.SimpleButton btnStopService;
        private DevExpress.XtraEditors.SimpleButton btnStartService;
        private DevExpress.XtraEditors.SimpleButton btnUninstallService;
        private DevExpress.XtraEditors.SimpleButton btnInstallService;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtNetServerPort;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtBusinessPort;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        private DevExpress.XtraEditors.SimpleButton btnServerTest;
        private DevExpress.XtraEditors.SimpleButton btnServerClose;
        private DevExpress.XtraEditors.SimpleButton btnCloseClient;
        private DevExpress.XtraEditors.SimpleButton btnSend2;
        private DevExpress.XtraEditors.SimpleButton btnSend1;
        private DevExpress.XtraEditors.SimpleButton btnSend3;
        private DevExpress.XtraEditors.LabelControl redisIP;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnSet;
        private DevExpress.XtraEditors.TextEdit txtRedisPort;
        private DevExpress.XtraEditors.TextEdit txtRedisIP;
    }
}
