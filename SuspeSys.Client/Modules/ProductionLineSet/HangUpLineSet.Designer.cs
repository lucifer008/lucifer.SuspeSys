namespace SuspeSys.Client.Modules.ProductionLineSet
{
    partial class HangUpLineSet
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
            this.lblMessage1 = new DevExpress.XtraEditors.LabelControl();
            this.panelHangUp = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lookUpHangUp = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.lblMessage2 = new DevExpress.XtraEditors.LabelControl();
            this.panelProduct = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lookUpProduct = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.lblMessage3 = new DevExpress.XtraEditors.LabelControl();
            this.panelOther = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lookUpOther = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpHangUp.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpProduct.Properties)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpOther.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1373, 747);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            this.xtraTabControl1.SelectedPageChanging += new DevExpress.XtraTab.TabPageChangingEventHandler(this.xtraTabControl1_SelectedPageChanging);
            this.xtraTabControl1.TabIndexChanged += new System.EventHandler(this.xtraTabControl1_TabIndexChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.lblMessage1);
            this.xtraTabPage1.Controls.Add(this.panelHangUp);
            this.xtraTabPage1.Controls.Add(this.panel1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1367, 718);
            this.xtraTabPage1.Text = "挂片站";
            // 
            // lblMessage1
            // 
            this.lblMessage1.Appearance.Font = new System.Drawing.Font("Tahoma", 30F);
            this.lblMessage1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMessage1.Location = new System.Drawing.Point(845, 263);
            this.lblMessage1.Name = "lblMessage1";
            this.lblMessage1.Size = new System.Drawing.Size(230, 48);
            this.lblMessage1.TabIndex = 5;
            this.lblMessage1.Text = "labelControl4";
            // 
            // panelHangUp
            // 
            this.panelHangUp.Location = new System.Drawing.Point(41, 72);
            this.panelHangUp.Name = "panelHangUp";
            this.panelHangUp.Size = new System.Drawing.Size(703, 537);
            this.panelHangUp.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lookUpHangUp);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1367, 55);
            this.panel1.TabIndex = 1;
            // 
            // lookUpHangUp
            // 
            this.lookUpHangUp.Location = new System.Drawing.Point(99, 17);
            this.lookUpHangUp.Name = "lookUpHangUp";
            this.lookUpHangUp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpHangUp.Size = new System.Drawing.Size(122, 20);
            this.lookUpHangUp.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(41, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "生产线";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.lblMessage2);
            this.xtraTabPage2.Controls.Add(this.panelProduct);
            this.xtraTabPage2.Controls.Add(this.panel2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1367, 718);
            this.xtraTabPage2.Text = "生产";
            // 
            // lblMessage2
            // 
            this.lblMessage2.Appearance.Font = new System.Drawing.Font("Tahoma", 30F);
            this.lblMessage2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMessage2.Location = new System.Drawing.Point(873, 232);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(230, 48);
            this.lblMessage2.TabIndex = 4;
            this.lblMessage2.Text = "labelControl4";
            // 
            // panelProduct
            // 
            this.panelProduct.Location = new System.Drawing.Point(41, 72);
            this.panelProduct.Name = "panelProduct";
            this.panelProduct.Size = new System.Drawing.Size(703, 537);
            this.panelProduct.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lookUpProduct);
            this.panel2.Controls.Add(this.labelControl2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1367, 55);
            this.panel2.TabIndex = 2;
            // 
            // lookUpProduct
            // 
            this.lookUpProduct.Location = new System.Drawing.Point(97, 17);
            this.lookUpProduct.Name = "lookUpProduct";
            this.lookUpProduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpProduct.Size = new System.Drawing.Size(122, 20);
            this.lookUpProduct.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(41, 20);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "生产线";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.lblMessage3);
            this.xtraTabPage3.Controls.Add(this.panelOther);
            this.xtraTabPage3.Controls.Add(this.panel3);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1367, 718);
            this.xtraTabPage3.Text = "其他";
            // 
            // lblMessage3
            // 
            this.lblMessage3.Appearance.Font = new System.Drawing.Font("Tahoma", 30F);
            this.lblMessage3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMessage3.Location = new System.Drawing.Point(898, 277);
            this.lblMessage3.Name = "lblMessage3";
            this.lblMessage3.Size = new System.Drawing.Size(230, 48);
            this.lblMessage3.TabIndex = 5;
            this.lblMessage3.Text = "labelControl4";
            // 
            // panelOther
            // 
            this.panelOther.Location = new System.Drawing.Point(41, 72);
            this.panelOther.Name = "panelOther";
            this.panelOther.Size = new System.Drawing.Size(703, 537);
            this.panelOther.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lookUpOther);
            this.panel3.Controls.Add(this.labelControl3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1367, 55);
            this.panel3.TabIndex = 3;
            // 
            // lookUpOther
            // 
            this.lookUpOther.Location = new System.Drawing.Point(99, 17);
            this.lookUpOther.Name = "lookUpOther";
            this.lookUpOther.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpOther.Size = new System.Drawing.Size(100, 20);
            this.lookUpOther.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(41, 20);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "生产线";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // HangUpLineSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "HangUpLineSet";
            this.Size = new System.Drawing.Size(1373, 747);
            this.Load += new System.EventHandler(this.HangUpLineSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpHangUp.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpProduct.Properties)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpOther.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpHangUp;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Panel panelHangUp;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.LookUpEdit lookUpOther;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Panel panelProduct;
        private System.Windows.Forms.Panel panelOther;
        private DevExpress.XtraEditors.LabelControl lblMessage1;
        private DevExpress.XtraEditors.LabelControl lblMessage2;
        private DevExpress.XtraEditors.LabelControl lblMessage3;
        private DevExpress.XtraEditors.LookUpEdit lookUpProduct;
        private System.Windows.Forms.Timer timer1;
    }
}
