using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using SuspeSys.Client.Common.Utils.Permission;

namespace SuspeSys.Client.Modules
{
    public delegate void ButtonClickHandel(ButtonName ButtonName);
    public enum ButtonName
    {
        /// <summary>
        /// 全屏
        /// </summary>
        Max,
        /// <summary>
        /// 修改
        /// </summary>
        Modify,
        /// <summary>
        /// 保存或新增
        /// </summary>
        SaveAndAdd,
        /// <summary>
        /// 保存并且关闭
        /// </summary>
        SaveAndClose,
        /// <summary>
        /// 关闭
        /// </summary>
        Close,
        /// <summary>
        /// 刷新
        /// </summary>
        Refresh,
        /// <summary>
        /// 保存
        /// </summary>
        Save,
        /// <summary>
        /// 取消
        /// </summary>
        Cancel,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 新增
        /// </summary>
        Add,
        /// <summary>
        /// 新增
        /// </summary>
        Add2,
        /// <summary>
        /// 导出
        /// </summary>
        Export,
        /// <summary>
        /// 查询
        /// </summary>
        Query,
        /// <summary>
        /// 打印
        /// </summary>
        Print,
        /// <summary>
        /// 自适应
        /// </summary>
        Fix
    }
    public partial class SusToolBar : DevExpress.XtraEditors.XtraUserControl
    {
        [Description("全屏"), Category("导航按钮")]
        public bool ShowMaxButton { get { return btnMax.Visible; } set { btnMax.Visible = value; } }
        [Description("修改"), Category("导航按钮")]
        public bool ShowModifyButton { get { return btnModify.Visible; } set { btnModify.Visible = value; } }

        [Description("保存或新增"), Category("导航按钮")]
        public bool ShowSaveAndAddButton { get { return btnSaveAndAdd.Visible; } set { btnSaveAndAdd.Visible = value; } }

        [Description("保存并且关闭"), Category("导航按钮")]
        public bool ShowSaveAndCloseButton { get { return btnSaveAndClose.Visible; } set { btnSaveAndClose.Visible = value; } }

        [Description("刷新"), Category("导航按钮")]
        public bool ShowRefreshButton { get { return btnRefresh.Visible; } set { btnRefresh.Visible = value; } }

        [Description("保存"), Category("导航按钮")]
        public bool ShowSaveButton { get { return btnSave.Visible; } set { btnSave.Visible = value; } }

        [Description("取消"), Category("导航按钮")]
        public bool ShowCancelButton { get { return btnCancel.Visible; } set { btnCancel.Visible = value; } }
        [Description("删除"), Category("导航按钮")]
        public bool ShowDeleteButton { get { return btnDelete.Visible; } set { btnDelete.Visible = value; } }

        [Description("新增"), Category("导航按钮")]
        public bool ShowAddButton { get { return btnAdd.Visible; } set { btnAdd.Visible = value; } }

        //[Description("新增"), Category("导航按钮")]
        //public bool ShowAddButton2 { get { return btnAdd2.Visible; } set { btnAdd2.Visible = value; } }

        [Description("导出"), Category("导航按钮")]
        public bool ShowExportButton { get { return btnExport.Visible; } set { btnExport.Visible = value; } }

        [Description("查询"), Category("查询按钮")]
        public bool ShowQueryButton { get { return btnQuery.Visible; } set { btnQuery.Visible = value; } }

        [Description("打印"), Category("打印按钮")]
        public bool ShowPrintButton { get { return btnPrint.Visible; } set { btnPrint.Visible = value; } }

        [Description("自适应"), Category("自适应按钮")]
        public bool ShowFixButton { get { return btnFix.Visible; } set { btnFix.Visible = value; } }

        public event ButtonClickHandel OnButtonClick;
        [Description("自定义扩展PanelControl"), Category("扩展PanelControl")]
        public PanelControl CustPanelControl {
            get { return cusPanelControl; }
            set { cusPanelControl = value; }
        }
        ///// <summary>
        ///// 按钮父容器
        ///// </summary>
        //public FlowLayoutPanel flowLayoutButtons { get { return this.flowLayoutPanel1; } }
        public SimpleButton GetButton(ButtonName bName)
        {
            switch (bName)
            {
                case ButtonName.Add:
                    return btnAdd;
                case ButtonName.Print:
                    return btnPrint;

            }
            return null;
        }
        public SusToolBar()
        {
            InitializeComponent();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            try
            {
                btnMax.Cursor = Cursors.WaitCursor;
                OnButtonClick(ButtonName.Max);
            }
            finally
            {
                btnMax.Cursor = Cursors.Default;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                btnModify.Cursor = Cursors.WaitCursor;
                OnButtonClick(ButtonName.Modify);
            }
            finally
            {
                btnModify.Cursor = Cursors.Default;
            }
        }

        private void btnSaveAndAdd_Click(object sender, EventArgs e)
        {
            try
            {
                btnSaveAndAdd.Cursor = Cursors.WaitCursor;
                OnButtonClick(ButtonName.SaveAndAdd);
            }
            finally
            {
                btnSaveAndAdd.Cursor = Cursors.Default;
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            try
            {
                btnSaveAndClose.Cursor = Cursors.WaitCursor;

                OnButtonClick(ButtonName.SaveAndClose);
            }
            finally
            {
                btnSaveAndClose.Cursor = Cursors.Default;
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                btnClose.Cursor = Cursors.WaitCursor;

                OnButtonClick(ButtonName.Close);
            }
            finally
            {
                btnClose.Cursor = Cursors.Default;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                btnRefresh.Cursor = Cursors.WaitCursor;
                OnButtonClick(ButtonName.Refresh);
            }
            finally
            {
                btnRefresh.Cursor = Cursors.Default;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                btnAdd.Cursor = Cursors.WaitCursor;
                OnButtonClick(ButtonName.Add);
            }
            finally
            {
                btnAdd.Cursor = Cursors.Default;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                btnDelete.Cursor = Cursors.WaitCursor;
                OnButtonClick(ButtonName.Delete);
            }
            finally
            {
                btnDelete.Cursor = Cursors.Default;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                btnExport.Cursor = Cursors.WaitCursor;
                OnButtonClick(ButtonName.Export);
            }
            finally
            {
                btnExport.Cursor = Cursors.Default;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Cursor = Cursors.WaitCursor;
                OnButtonClick(ButtonName.Save);
            }
            finally
            {
                btnSave.Cursor = Cursors.Default;
            }
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            OnButtonClick(ButtonName.Add2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SimpleButton CreateButton()
        {
            SimpleButton btn = new SimpleButton();
            btn.Appearance.BackColor = System.Drawing.Color.White;
            btn.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            btn.Image = global::SuspeSys.Client.Properties.Resources.icon_edit_16;
            btn.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            btn.Location = new System.Drawing.Point(201, 3);
            btn.Name = "btnModify";
            btn.Size = new System.Drawing.Size(45, 39);
            btn.TabIndex = 0;
            btn.Margin = this.btnAdd.Margin;
            btn.Padding = this.btnAdd.Padding ;
            btn.Height = this.btnAdd.Height;
            //btn.Text = "修改";
            //btn.Click += new System.EventHandler(this.btnModify_Click);
            //btn.Text = "导出";

            this.flowLayoutPanel1.Controls.Add(btn);

            return btn;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Cursor = Cursors.WaitCursor;
                OnButtonClick(ButtonName.Query);
            }
            finally
            {
                btnSave.Cursor = Cursors.Default;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Cursor = Cursors.WaitCursor;
                OnButtonClick(ButtonName.Print);
            }
            finally
            {
                btnSave.Cursor = Cursors.Default;
            }
        }
        private void btnFix_Click(object sender, EventArgs e)
        {
            try
            {
                btnFix.Cursor = Cursors.WaitCursor;
                OnButtonClick(ButtonName.Fix);
            }
            finally
            {
                btnFix.Cursor = Cursors.Default;
            }
        }
    }
}
