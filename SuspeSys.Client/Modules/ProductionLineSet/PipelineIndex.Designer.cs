namespace SuspeSys.Client.Modules.ProductionLineSet
{
    partial class PipelineIndex
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.susToolBar1 = new SuspeSys.Client.Modules.SusToolBar();
            this.comboPipelining = new DevExpress.XtraEditors.PopupContainerEdit();
            this.pipelineMain1 = new SuspeSys.Client.Modules.ProductionLineSet.PipelineMain(this);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboPipelining.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.susToolBar1);
            this.panelControl1.Controls.Add(this.comboPipelining);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1295, 57);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 20);
            this.labelControl1.Name = "Pipeli";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "流水线";
            // 
            // susToolBar1
            // 
            this.susToolBar1.Location = new System.Drawing.Point(227, 3);
            this.susToolBar1.Name = "susToolBar1";
            this.susToolBar1.ShowAddButton = true;
            this.susToolBar1.ShowCancelButton = false;
            this.susToolBar1.ShowDeleteButton = true;
            this.susToolBar1.ShowExportButton = true;
            this.susToolBar1.ShowMaxButton = true;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowRefreshButton = true;
            this.susToolBar1.ShowSaveAndAddButton = true;
            this.susToolBar1.ShowSaveAndCloseButton = true;
            this.susToolBar1.ShowSaveButton = true;
            this.susToolBar1.Size = new System.Drawing.Size(1041, 51);
            this.susToolBar1.TabIndex = 0;
            // 
            // comboPipelining
            // 
            this.comboPipelining.Location = new System.Drawing.Point(67, 17);
            this.comboPipelining.Name = "comboPipelining";
            this.comboPipelining.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboPipelining.Size = new System.Drawing.Size(137, 20);
            this.comboPipelining.TabIndex = 2;
            // 
            // pipelineMain1
            // 
            this.pipelineMain1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pipelineMain1.Location = new System.Drawing.Point(0, 57);
            this.pipelineMain1.Name = "pipelineMain1";
            this.pipelineMain1.Size = new System.Drawing.Size(1295, 606);
            this.pipelineMain1.TabIndex = 1;
            this.pipelineMain1.ucMain = null;
            // 
            // PipelineIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pipelineMain1);
            this.Controls.Add(this.panelControl1);
            this.Name = "PipelineIndex";
            this.Size = new System.Drawing.Size(1295, 663);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboPipelining.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private SusToolBar susToolBar1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
       // private PipelineMain pipelineMain1;
        private DevExpress.XtraEditors.PopupContainerEdit comboPipelining;
        private PipelineMain pipelineMain1;
    }
}
