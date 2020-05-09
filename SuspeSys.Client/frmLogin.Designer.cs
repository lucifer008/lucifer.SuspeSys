using DevExpress.XtraEditors;

namespace DevExpress.XtraTreeList.Demos
{
    partial class frmLogin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.backWorkLogin = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.comboLanguage = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmClient = new DevExpress.XtraEditors.TextEdit();
            this.btnChangeClient = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.chcSavePwd = new DevExpress.XtraEditors.CheckEdit();
            this.chcSaveUserName = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtUserName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnEditDatabase = new DevExpress.XtraEditors.TextEdit();
            this.cmClientList = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboLanguage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmClient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcSavePwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcSaveUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditDatabase.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // backWorkLogin
            // 
            this.backWorkLogin.WorkerReportsProgress = true;
            this.backWorkLogin.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backWorkLogin_DoWork);
            this.backWorkLogin.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backWorkLogin_ProgressChanged);
            this.backWorkLogin.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backWorkLogin_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Appearance.Options.UseBackColor = true;
            this.panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panel1.Controls.Add(this.comboLanguage);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.cmClient);
            this.panel1.Controls.Add(this.btnChangeClient);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.chcSavePwd);
            this.panel1.Controls.Add(this.chcSaveUserName);
            this.panel1.Controls.Add(this.labelControl5);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Controls.Add(this.labelControl4);
            this.panel1.Controls.Add(this.labelControl3);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Controls.Add(this.btnEditDatabase);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(805, 418);
            this.panel1.TabIndex = 8;
            // 
            // comboLanguage
            // 
            this.comboLanguage.Location = new System.Drawing.Point(504, 275);
            this.comboLanguage.Name = "comboLanguage";
            this.comboLanguage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboLanguage.Size = new System.Drawing.Size(173, 20);
            this.comboLanguage.TabIndex = 28;
            this.comboLanguage.SelectedValueChanged += new System.EventHandler(this.comboLanguage_SelectedValueChanged);
            // 
            // label1
            // 
            //this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文新魏", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(153, 24);
            this.label1.Name = "SuspeSystemName";
            this.label1.Size = new System.Drawing.Size(538, 57);
            this.label1.TabIndex = 27;
            this.label1.Text = "麦仕德智能悬挂系统";
            label1.AutoSize = true;
            label1.Width = 200;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SuspeSys.Client.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(61, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(86, 68);
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // cmClient
            // 
            this.cmClient.Enabled = false;
            this.cmClient.Location = new System.Drawing.Point(504, 157);
            this.cmClient.Name = "cmClient";
            this.cmClient.Properties.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.cmClient.Properties.Appearance.Options.UseBackColor = true;
            this.cmClient.Properties.AutoHeight = false;
            this.cmClient.Size = new System.Drawing.Size(173, 26);
            this.cmClient.TabIndex = 24;
            this.cmClient.TabStop = false;
            // 
            // btnChangeClient
            // 
            this.btnChangeClient.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnChangeClient.Appearance.Options.UseFont = true;
            this.btnChangeClient.Location = new System.Drawing.Point(693, 158);
            this.btnChangeClient.Name = "ChangeAction";
            this.btnChangeClient.Size = new System.Drawing.Size(43, 23);
            this.btnChangeClient.TabIndex = 23;
            this.btnChangeClient.Text = "切换";
            this.btnChangeClient.Click += new System.EventHandler(this.btnChangeClient_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(615, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "取 消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Location = new System.Drawing.Point(546, 326);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(63, 23);
            this.btnLogin.TabIndex = 22;
            this.btnLogin.Text = "登 录";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(693, 123);
            this.simpleButton1.Name = "btnConfirng";
            this.simpleButton1.Size = new System.Drawing.Size(43, 23);
            this.simpleButton1.TabIndex = 21;
            this.simpleButton1.Text = "配置";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // chcSavePwd
            // 
            this.chcSavePwd.Location = new System.Drawing.Point(627, 301);
            this.chcSavePwd.Name = "chcSavePwd";
            this.chcSavePwd.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.chcSavePwd.Properties.Appearance.Options.UseFont = true;
            this.chcSavePwd.Properties.Caption = "记住密码";
            this.chcSavePwd.Size = new System.Drawing.Size(97, 21);
            this.chcSavePwd.TabIndex = 19;
            // 
            // chcSaveUserName
            // 
            this.chcSaveUserName.Location = new System.Drawing.Point(531, 301);
            this.chcSaveUserName.Name = "chcSaveUserName";
            this.chcSaveUserName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.chcSaveUserName.Properties.Appearance.Options.UseFont = true;
            this.chcSaveUserName.Properties.Caption = "记住用户名";
            this.chcSaveUserName.Size = new System.Drawing.Size(90, 21);
            this.chcSaveUserName.TabIndex = 20;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Location = new System.Drawing.Point(410, 275);
            this.labelControl5.Name = "LanguageInfo";
            this.labelControl5.Size = new System.Drawing.Size(28, 19);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "语言";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Location = new System.Drawing.Point(410, 238);
            this.labelControl2.Name = "Password";
            this.labelControl2.Size = new System.Drawing.Size(28, 19);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "密码";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Location = new System.Drawing.Point(410, 126);
            this.labelControl4.Name = "DbNameAction";
            this.labelControl4.Size = new System.Drawing.Size(42, 19);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "数据库";
           // this.labelControl4.AutoSizeMode = LabelAutoSizeMode.Vertical;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Location = new System.Drawing.Point(410, 158);
            this.labelControl3.Name = "ClientMachineAction";
            this.labelControl3.Size = new System.Drawing.Size(60, 19);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "客户机";
           // this.labelControl3.AutoSizeMode = LabelAutoSizeMode.Vertical;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(410, 198);
            this.labelControl1.Name = "UserName";
            this.labelControl1.Size = new System.Drawing.Size(42, 19);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "用户名";
         //   this.labelControl1.AutoSizeMode = LabelAutoSizeMode.Vertical;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(504, 233);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Properties.UseSystemPasswordChar = true;
            this.txtPassword.Size = new System.Drawing.Size(173, 26);
            this.txtPassword.TabIndex = 16;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(504, 193);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtUserName.Properties.Appearance.Options.UseFont = true;
            this.txtUserName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtUserName.Properties.NullText = "请输入用户名";
            this.txtUserName.Size = new System.Drawing.Size(173, 26);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.SelectedIndexChanged += new System.EventHandler(this.txtUserName_SelectedValueChanged);
            // 
            // btnEditDatabase
            // 
            this.btnEditDatabase.EditValue = "请设置数据库";
            this.btnEditDatabase.Enabled = false;
            this.btnEditDatabase.Location = new System.Drawing.Point(504, 123);
            this.btnEditDatabase.Name = "btnEditDatabase";
            this.btnEditDatabase.Properties.Appearance.BackColor = System.Drawing.Color.LightGray;
            this.btnEditDatabase.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnEditDatabase.Properties.Appearance.Options.UseBackColor = true;
            this.btnEditDatabase.Properties.Appearance.Options.UseFont = true;
            this.btnEditDatabase.Properties.NullText = "请选择数据库";
            this.btnEditDatabase.Properties.ReadOnly = true;
            this.btnEditDatabase.Size = new System.Drawing.Size(173, 26);
            this.btnEditDatabase.TabIndex = 25;
            this.btnEditDatabase.TabStop = false;
            // 
            // cmClientList
            // 
            this.cmClientList.Name = "cmClientList";
            this.cmClientList.Size = new System.Drawing.Size(61, 4);
            this.cmClientList.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmClientList_ItemClicked);
            this.cmClientList.Click += new System.EventHandler(this.cmClientList_Click);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = global::SuspeSys.Client.Properties.Resources.login_bg_07181;
            this.ClientSize = new System.Drawing.Size(805, 418);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLogin";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseUp);
            this.Resize += new System.EventHandler(this.frmLogin_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboLanguage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmClient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcSavePwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chcSaveUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditDatabase.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backWorkLogin;
        // private System.Windows.Forms.Panel panel1;
        private XtraEditors.PanelControl panel1;
        private XtraEditors.CheckEdit chcSavePwd;
        private XtraEditors.CheckEdit chcSaveUserName;
        private XtraEditors.LabelControl labelControl2;
        private XtraEditors.LabelControl labelControl4;
        private XtraEditors.LabelControl labelControl3;
        private XtraEditors.LabelControl labelControl1;
        private XtraEditors.TextEdit txtPassword;
        private XtraEditors.ComboBoxEdit txtUserName;
        private XtraEditors.SimpleButton simpleButton1;
        private XtraEditors.SimpleButton btnLogin;
        private XtraEditors.SimpleButton btnCancel;
        private XtraEditors.SimpleButton btnChangeClient;
        private System.Windows.Forms.ContextMenuStrip cmClientList;
        private XtraEditors.TextEdit cmClient;
        private XtraEditors.TextEdit btnEditDatabase;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private XtraEditors.LabelControl labelControl5;
        private XtraEditors.ComboBoxEdit comboLanguage;
    }
}