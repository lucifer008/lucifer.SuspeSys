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

namespace SuspeSys.Client.Modules
{
    /// <summary>
    /// 通用分页处理
    /// </summary>
    public partial class SusPage : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 翻页的事件委托
        /// </summary>
        public delegate void PageChangedHandle(int page);
        public event PageChangedHandle OnPageChanged;
        /// <summary>
        /// 总记录数
        /// </summary>
        private long TotalRowCount;

        private int PageAVG;
        private long PageCount;
        /// <summary>
        /// 当前第几页
        /// </summary>
        private int CurrentPage;

        public SusPage()
        {
            InitializeComponent();
            txtPageSize.SelectedIndex = 0;
        }
        public int PageSize
        {
            get { return Convert.ToInt32(txtPageSize.Text); }
        }
        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            btnFirstPage.Cursor = Cursors.WaitCursor;
            try
            {
                if (null != OnPageChanged)
                {
                    OnPageChanged(1);
                }
            }
            finally
            {
                btnFirstPage.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 初始化分页控件
        /// </summary>
        /// <param name="totalRowCount">总记录数</param>
        /// <param name="pageAVG">每页大小</param>
        /// <param name="currentPage">当前页索引</param>
        public void BindPageData(long totalRowCount, int pageAVG, int currentPage)
        {
            this.CurrentPage = currentPage;
            this.TotalRowCount = totalRowCount;
            this.PageAVG = pageAVG;
            if (pageAVG == 0)
            {
                pageAVG = 1;
            }
            var pageCount = totalRowCount % pageAVG == 0 ? totalRowCount / pageAVG : ((totalRowCount / pageAVG) + 1);
            this.PageCount = pageCount;

            lblTotalCount.Text = TotalRowCount.ToString();
            txtPageSize.Text = PageAVG.ToString();
            lblTotalPage.Text = PageCount.ToString();
            lblPageNumber.Text = CurrentPage.ToString();
        }
        private void btnLastPage_Click(object sender, EventArgs e)
        {
            btnLastPage.Cursor = Cursors.WaitCursor;
            try
            {
                if (CurrentPage > 1)
                {
                    if (OnPageChanged != null)
                    {
                        OnPageChanged(CurrentPage - 1);
                    }
                }
                else
                    XtraMessageBox.Show("已经是第一页了！");
            }
            finally
            {
                btnLastPage.Cursor = Cursors.Default;
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            try
            {
                btnNextPage.Cursor = Cursors.WaitCursor;
                if (CurrentPage < PageCount)
                {
                    if (OnPageChanged != null)
                    {
                        OnPageChanged(CurrentPage + 1);
                    }
                }
                else
                    XtraMessageBox.Show("已经是最后一页了！");
            }
            finally
            {
                btnNextPage.Cursor = Cursors.Default;
            }
        }

        private void btnEndPage_Click(object sender, EventArgs e)
        {
            try
            {
                btnEndPage.Cursor = Cursors.WaitCursor;
                if (OnPageChanged != null)
                {
                    OnPageChanged((int)PageCount);
                }
            }
            finally
            {
                btnEndPage.Cursor = Cursors.Default;
            }
        }

        private void txtPageSize_EditValueChanged(object sender, EventArgs e)
        {
            if (OnPageChanged != null)
            {
                OnPageChanged(1);
            }
        }

        private void txtCurrentPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (!string.IsNullOrEmpty(txtCurrentPage.Text.Trim()))
                {
                    var currentPageIndex = Convert.ToInt16(txtCurrentPage.Text.Trim());
                    if (currentPageIndex > PageCount)
                    {
                        txtCurrentPage.Text = string.Empty;
                        XtraMessageBox.Show(string.Format("当前总共{0}页,你已超出最大页数了!", PageCount), "温馨提示");
                        return;
                    }
                    if (OnPageChanged != null)
                    {
                        OnPageChanged(currentPageIndex);
                    }
                }
            }
        }
    }
}
