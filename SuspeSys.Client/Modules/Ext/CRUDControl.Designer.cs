﻿namespace SuspeSys.Client.Modules.Ext
{
    partial class CRUDControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.susToolBar1 = new SuspeSys.Client.Modules.SusToolBar();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pnlGridMain = new DevExpress.XtraEditors.PanelControl();
            this.susGrid1 = new SuspeSys.Client.Modules.SusGrid();
            this.pnlToolButtonMain = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridMain)).BeginInit();
            this.pnlGridMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolButtonMain)).BeginInit();
            this.pnlToolButtonMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // susToolBar1
            // 
            this.susToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.susToolBar1.Location = new System.Drawing.Point(0, 0);
            this.susToolBar1.Name = "susToolBar1";
            this.susToolBar1.ShowAddButton = true;
            this.susToolBar1.ShowCancelButton = false;
            this.susToolBar1.ShowDeleteButton = true;
            this.susToolBar1.ShowExportButton = true;
            this.susToolBar1.ShowMaxButton = true;
            this.susToolBar1.ShowModifyButton = true;
            this.susToolBar1.ShowRefreshButton = true;
            this.susToolBar1.ShowSaveAndAddButton = false;
            this.susToolBar1.ShowSaveAndCloseButton = false;
            this.susToolBar1.ShowSaveButton = true;
            this.susToolBar1.Size = new System.Drawing.Size(1138, 55);
            this.susToolBar1.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.pnlGridMain);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1142, 500);
            this.panelControl1.TabIndex = 9;
            // 
            // pnlGridMain
            // 
            this.pnlGridMain.Controls.Add(this.susGrid1);
            this.pnlGridMain.Controls.Add(this.pnlToolButtonMain);
            this.pnlGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridMain.Location = new System.Drawing.Point(0, 0);
            this.pnlGridMain.Name = "pnlGridMain";
            this.pnlGridMain.Size = new System.Drawing.Size(1142, 500);
            this.pnlGridMain.TabIndex = 3;
            // 
            // susGrid1
            // 
            this.susGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.susGrid1.Location = new System.Drawing.Point(2, 101);
            this.susGrid1.Name = "susGrid1";
            this.susGrid1.Size = new System.Drawing.Size(1138, 397);
            this.susGrid1.TabIndex = 7;
            // 
            // pnlToolButtonMain
            // 
            this.pnlToolButtonMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlToolButtonMain.Controls.Add(this.panelControl2);
            this.pnlToolButtonMain.Controls.Add(this.panelControl3);
            this.pnlToolButtonMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolButtonMain.Location = new System.Drawing.Point(2, 2);
            this.pnlToolButtonMain.Name = "pnlToolButtonMain";
            this.pnlToolButtonMain.Size = new System.Drawing.Size(1138, 99);
            this.pnlToolButtonMain.TabIndex = 6;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.susToolBar1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1138, 57);
            this.panelControl2.TabIndex = 0;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.searchControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1138, 99);
            this.panelControl3.TabIndex = 1;
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(5, 72);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Properties.NullValuePrompt = "输入角色名";
            this.searchControl1.Properties.ShowClearButton = false;
            this.searchControl1.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.searchControl1_Properties_ButtonClick);
            this.searchControl1.Size = new System.Drawing.Size(318, 20);
            this.searchControl1.TabIndex = 0;
            this.searchControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchControl1_KeyDown);
            // 
            // CRUDControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "CRUDControl";
            this.Load += new System.EventHandler(this.CRUDControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridMain)).EndInit();
            this.pnlGridMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolButtonMain)).EndInit();
            this.pnlToolButtonMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SusToolBar susToolBar1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl pnlGridMain;
        private SusGrid susGrid1;
        private DevExpress.XtraEditors.PanelControl pnlToolButtonMain;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SearchControl searchControl1;
    }
}
