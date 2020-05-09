namespace SuspeSys.Client.Modules
{
    partial class SusTreeView
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
            this.tvData = new DevExpress.XtraTreeList.TreeList();
            this.susPage1 = new SuspeSys.Client.Modules.SusPage();
            ((System.ComponentModel.ISupportInitialize)(this.tvData)).BeginInit();
            this.SuspendLayout();
            // 
            // tvData
            // 
            this.tvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvData.Location = new System.Drawing.Point(0, 0);
            this.tvData.Name = "tvData";
            this.tvData.Size = new System.Drawing.Size(1276, 690);
            this.tvData.TabIndex = 0;
            // 
            // susPage1
            // 
            this.susPage1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.susPage1.Location = new System.Drawing.Point(0, 690);
            this.susPage1.Name = "susPage1";
            this.susPage1.Size = new System.Drawing.Size(1276, 31);
            this.susPage1.TabIndex = 1;
            // 
            // SusTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvData);
            this.Controls.Add(this.susPage1);
            this.Name = "SusTreeView";
            this.Size = new System.Drawing.Size(1276, 721);
            ((System.ComponentModel.ISupportInitialize)(this.tvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tvData;
        private SusPage susPage1;
    }
}
