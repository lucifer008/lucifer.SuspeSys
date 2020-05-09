using System;
using DevExpress.XtraEditors.Controls;

namespace SuspeSys.Client.Modules.Reports
{
    partial class ProductItemReport
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
            this.susGrid1 = new SuspeSys.Client.Modules.SusGrid();
            this.pnlToolButtonMain = new DevExpress.XtraEditors.PanelControl();
            this.susToolBar1 = new SuspeSys.Client.Modules.SusToolBar();
            this.susQueryControl1 = new SuspeSys.Client.Modules.SusQueryControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolButtonMain)).BeginInit();
            this.pnlToolButtonMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // susGrid1
            // 
            this.susGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.susGrid1.Location = new System.Drawing.Point(0, 58);
            this.susGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.susGrid1.Name = "susGrid1";
            this.susGrid1.Size = new System.Drawing.Size(1555, 480);
            this.susGrid1.TabIndex = 4;
            // 
            // pnlToolButtonMain
            // 
            this.pnlToolButtonMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlToolButtonMain.Controls.Add(this.susGrid1);
            this.pnlToolButtonMain.Controls.Add(this.susQueryControl1);
            this.pnlToolButtonMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolButtonMain.Location = new System.Drawing.Point(0, 45);
            this.pnlToolButtonMain.Name = "pnlToolButtonMain";
            this.pnlToolButtonMain.Size = new System.Drawing.Size(1555, 538);
            this.pnlToolButtonMain.TabIndex = 11;
            // 
            // susToolBar1
            // 
            this.susToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.susToolBar1.Location = new System.Drawing.Point(0, 0);
            this.susToolBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.susToolBar1.Name = "susToolBar1";
            this.susToolBar1.ShowAddButton = false;
            this.susToolBar1.ShowCancelButton = false;
            this.susToolBar1.ShowDeleteButton = false;
            this.susToolBar1.ShowExportButton = true;
            this.susToolBar1.ShowMaxButton = true;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowQueryButton = true;
            this.susToolBar1.ShowRefreshButton = true;
            this.susToolBar1.ShowSaveAndAddButton = false;
            this.susToolBar1.ShowSaveAndCloseButton = false;
            this.susToolBar1.ShowSaveButton = false;
            this.susToolBar1.Size = new System.Drawing.Size(1555, 45);
            this.susToolBar1.TabIndex = 5;
            // 
            // susQueryControl1
            // 
            this.susQueryControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.susQueryControl1.Location = new System.Drawing.Point(0, 0);
            this.susQueryControl1.Name = "susQueryControl1";
            this.susQueryControl1.Size = new System.Drawing.Size(1555, 58);
            this.susQueryControl1.TabIndex = 5;
            // 
            // ProductItemReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlToolButtonMain);
            this.Controls.Add(this.susToolBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ProductItemReport";
            this.Size = new System.Drawing.Size(1555, 583);
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolButtonMain)).EndInit();
            this.pnlToolButtonMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void searchControl1_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private SusGrid susGrid1;
        private DevExpress.XtraEditors.PanelControl pnlToolButtonMain;
        private SusToolBar susToolBar1;
        private SusQueryControl susQueryControl1;
    }
}
