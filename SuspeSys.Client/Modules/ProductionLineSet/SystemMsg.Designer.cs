namespace SuspeSys.Client.Modules.ProductionLineSet
{
    partial class SystemMsg
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
            this.components = new System.ComponentModel.Container();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.panelKaoQin = new System.Windows.Forms.Panel();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.panelProduct = new System.Windows.Forms.Panel();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.panelOther = new System.Windows.Forms.Panel();
            this.lblMessage1 = new DevExpress.XtraEditors.LabelControl();
            this.lblMessage2 = new DevExpress.XtraEditors.LabelControl();
            this.lblMessage3 = new DevExpress.XtraEditors.LabelControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1530, 516);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.lblMessage1);
            this.xtraTabPage1.Controls.Add(this.panelKaoQin);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1524, 487);
            this.xtraTabPage1.Text = "考勤";
            // 
            // panelKaoQin
            // 
            this.panelKaoQin.Location = new System.Drawing.Point(31, 23);
            this.panelKaoQin.Name = "panelKaoQin";
            this.panelKaoQin.Size = new System.Drawing.Size(707, 429);
            this.panelKaoQin.TabIndex = 1;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.lblMessage2);
            this.xtraTabPage2.Controls.Add(this.panelProduct);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1524, 487);
            this.xtraTabPage2.Text = "生产";
            // 
            // panelProduct
            // 
            this.panelProduct.Location = new System.Drawing.Point(31, 23);
            this.panelProduct.Name = "panelProduct";
            this.panelProduct.Size = new System.Drawing.Size(707, 429);
            this.panelProduct.TabIndex = 1;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.lblMessage3);
            this.xtraTabPage3.Controls.Add(this.panelOther);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1524, 487);
            this.xtraTabPage3.Text = "其他";
            // 
            // panelOther
            // 
            this.panelOther.Location = new System.Drawing.Point(31, 23);
            this.panelOther.Name = "panelOther";
            this.panelOther.Size = new System.Drawing.Size(707, 429);
            this.panelOther.TabIndex = 0;
            // 
            // lblMessage1
            // 
            this.lblMessage1.Appearance.Font = new System.Drawing.Font("Tahoma", 30F);
            this.lblMessage1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMessage1.Location = new System.Drawing.Point(824, 182);
            this.lblMessage1.Name = "lblMessage1";
            this.lblMessage1.Size = new System.Drawing.Size(230, 48);
            this.lblMessage1.TabIndex = 6;
            this.lblMessage1.Text = "labelControl4";
            // 
            // lblMessage2
            // 
            this.lblMessage2.Appearance.Font = new System.Drawing.Font("Tahoma", 30F);
            this.lblMessage2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMessage2.Location = new System.Drawing.Point(824, 182);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(230, 48);
            this.lblMessage2.TabIndex = 7;
            this.lblMessage2.Text = "labelControl4";
            // 
            // lblMessage3
            // 
            this.lblMessage3.Appearance.Font = new System.Drawing.Font("Tahoma", 30F);
            this.lblMessage3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMessage3.Location = new System.Drawing.Point(824, 182);
            this.lblMessage3.Name = "lblMessage3";
            this.lblMessage3.Size = new System.Drawing.Size(230, 48);
            this.lblMessage3.TabIndex = 7;
            this.lblMessage3.Text = "labelControl4";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SystemMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SystemMsg";
            this.Size = new System.Drawing.Size(1530, 516);
            this.Load += new System.EventHandler(this.SystemMsg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private System.Windows.Forms.Panel panelKaoQin;
        private System.Windows.Forms.Panel panelProduct;
        private System.Windows.Forms.Panel panelOther;
        private DevExpress.XtraEditors.LabelControl lblMessage1;
        private DevExpress.XtraEditors.LabelControl lblMessage2;
        private DevExpress.XtraEditors.LabelControl lblMessage3;
        private System.Windows.Forms.Timer timer1;
    }
}
