namespace SuspeSys.Client.Modules
{
    partial class SusToolBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SusToolBar));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMax = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnModify = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveAndAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveAndClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnFix = new DevExpress.XtraEditors.SimpleButton();
            this.cusPanelControl = new DevExpress.XtraEditors.PanelControl();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cusPanelControl)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnMax);
            this.flowLayoutPanel1.Controls.Add(this.btnRefresh);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnModify);
            this.flowLayoutPanel1.Controls.Add(this.btnSaveAndAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnSaveAndClose);
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnQuery);
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Controls.Add(this.btnExport);
            this.flowLayoutPanel1.Controls.Add(this.btnPrint);
            this.flowLayoutPanel1.Controls.Add(this.btnFix);
            this.flowLayoutPanel1.Controls.Add(this.cusPanelControl);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(987, 40);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnMax
            // 
            this.btnMax.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnMax.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnMax.Appearance.Options.UseBackColor = true;
            this.btnMax.Appearance.Options.UseFont = true;
            this.btnMax.AutoSize = true;
            this.btnMax.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnMax.Image = global::SuspeSys.Client.Properties.Resources.icon_docking_16;
            this.btnMax.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnMax.Location = new System.Drawing.Point(0, 3);
            this.btnMax.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(35, 39);
            this.btnMax.TabIndex = 0;
            this.btnMax.Text = "全屏";
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnRefresh.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnRefresh.Appearance.Options.UseBackColor = true;
            this.btnRefresh.Appearance.Options.UseFont = true;
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(41, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(35, 39);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.AutoSize = true;
            this.btnSave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnSave.Image = global::SuspeSys.Client.Properties.Resources.icon_save_16;
            this.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(82, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(35, 39);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.AutoSize = true;
            this.btnCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnCancel.Location = new System.Drawing.Point(123, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(35, 39);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnModify
            // 
            this.btnModify.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnModify.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnModify.Appearance.Options.UseBackColor = true;
            this.btnModify.Appearance.Options.UseFont = true;
            this.btnModify.AutoSize = true;
            this.btnModify.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnModify.Image = global::SuspeSys.Client.Properties.Resources.icon_edit_16;
            this.btnModify.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnModify.Location = new System.Drawing.Point(164, 3);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(35, 39);
            this.btnModify.TabIndex = 0;
            this.btnModify.Text = "修改";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnSaveAndAdd
            // 
            this.btnSaveAndAdd.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveAndAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnSaveAndAdd.Appearance.Options.UseBackColor = true;
            this.btnSaveAndAdd.Appearance.Options.UseFont = true;
            this.btnSaveAndAdd.AutoSize = true;
            this.btnSaveAndAdd.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnSaveAndAdd.Image = global::SuspeSys.Client.Properties.Resources.icon_save_as_16;
            this.btnSaveAndAdd.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnSaveAndAdd.Location = new System.Drawing.Point(205, 3);
            this.btnSaveAndAdd.Name = "btnSaveAndAdd";
            this.btnSaveAndAdd.Size = new System.Drawing.Size(66, 39);
            this.btnSaveAndAdd.TabIndex = 0;
            this.btnSaveAndAdd.Text = "保存&新增";
            this.btnSaveAndAdd.Click += new System.EventHandler(this.btnSaveAndAdd_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveAndClose.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnSaveAndClose.Appearance.Options.UseBackColor = true;
            this.btnSaveAndClose.Appearance.Options.UseFont = true;
            this.btnSaveAndClose.AutoSize = true;
            this.btnSaveAndClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnSaveAndClose.Image = global::SuspeSys.Client.Properties.Resources.icon_save_16;
            this.btnSaveAndClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnSaveAndClose.Location = new System.Drawing.Point(277, 3);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(66, 39);
            this.btnSaveAndClose.TabIndex = 0;
            this.btnSaveAndClose.Text = "保存&关闭";
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnAdd.Appearance.Options.UseBackColor = true;
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.AutoSize = true;
            this.btnAdd.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnAdd.Location = new System.Drawing.Point(349, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(35, 39);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnQuery.Appearance.Options.UseBackColor = true;
            this.btnQuery.Appearance.Options.UseFont = true;
            this.btnQuery.AutoSize = true;
            this.btnQuery.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnQuery.Location = new System.Drawing.Point(390, 3);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(35, 39);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            this.btnQuery.Visible = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.AutoSize = true;
            this.btnClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnClose.Image = global::SuspeSys.Client.Properties.Resources.icon_close_16;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnClose.Location = new System.Drawing.Point(431, 3);
            this.btnClose.LookAndFeel.SkinName = "Seven";
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 39);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "退出";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnDelete.Appearance.Options.UseBackColor = true;
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.AutoSize = true;
            this.btnDelete.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnDelete.Location = new System.Drawing.Point(472, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(35, 39);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExport
            // 
            this.btnExport.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.btnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btnExport.Appearance.Options.UseBackColor = true;
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.AutoSize = true;
            this.btnExport.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnExport.Location = new System.Drawing.Point(513, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(35, 39);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AutoSize = true;
            this.btnPrint.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnPrint.Location = new System.Drawing.Point(554, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(35, 40);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnFix
            // 
            this.btnFix.AutoSize = true;
            this.btnFix.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnFix.Image = ((System.Drawing.Image)(resources.GetObject("btnFix.Image")));
            this.btnFix.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnFix.Location = new System.Drawing.Point(595, 3);
            this.btnFix.Name = "btnFix";
            this.btnFix.Size = new System.Drawing.Size(47, 40);
            this.btnFix.TabIndex = 5;
            this.btnFix.Text = "自适应";
            this.btnFix.Click += new System.EventHandler(this.btnFix_Click);
            // 
            // cusPanelControl
            // 
            this.cusPanelControl.Location = new System.Drawing.Point(648, 3);
            this.cusPanelControl.Name = "cusPanelControl";
            this.cusPanelControl.Size = new System.Drawing.Size(200, 34);
            this.cusPanelControl.TabIndex = 4;
            this.cusPanelControl.Visible = false;
            // 
            // SusToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "SusToolBar";
            this.Size = new System.Drawing.Size(987, 40);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cusPanelControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnMax;
        private DevExpress.XtraEditors.SimpleButton btnModify;
        private DevExpress.XtraEditors.SimpleButton btnSaveAndAdd;
        private DevExpress.XtraEditors.SimpleButton btnSaveAndClose;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.PanelControl cusPanelControl;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnFix;
    }
}
