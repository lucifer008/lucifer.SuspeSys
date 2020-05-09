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
using DevExpress.XtraGrid;
using static SuspeSys.Client.Modules.General;

namespace SuspeSys.Client.Modules
{
    /// <summary>
    /// 通用查询Grid
    /// </summary>
    public partial class SusGrid : DevExpress.XtraEditors.XtraUserControl
    {
        private int currentPageIndex;
        private int pageSize;
        private long totalCount;
        public event PageChangedHandle OnPageChanged;
        public ContextMenuStrip gridContextMenuStrip
        {
            get { return this.contextMenuStrip1; }
        }
        public int PageSize {
            get{ return Convert.ToInt32(susPage1.PageSize); }
        }
        public SusGrid()
        {
            InitializeComponent();
            InitPageTool();
           // InitPageTool();
            //BindData();
        }

        void InitPageTool()
        {
            susPage1.OnPageChanged += SusPage1_OnPageChanged;
        }

        private void SusPage1_OnPageChanged(int pageIndex)
        {
            OnPageChanged?.Invoke(pageIndex);
        }

        public void SetGridControlData<T>(IList<T> source, int currentPageIndex, int pageSize, long totalCount)
        {
            this.currentPageIndex = currentPageIndex;
            this.pageSize = pageSize;
            this.totalCount = totalCount;

            gridControl1.DataSource = source;
            susPage1.BindPageData(totalCount, pageSize, currentPageIndex);

        }
        //void BindData()
        //{
        //    var dt = new DataTable();
        //    dt.Columns.Add("column1");
        //    dt.Columns.Add("column2");
        //    dt.Columns.Add("column3");
        //    for (var i = 0; i < 10; i++)
        //    {
        //        var dr = dt.NewRow();
        //        dr[0] = i.ToString();
        //        dr[1] = i.ToString();
        //        dr[2] = i.ToString();
        //        dt.Rows.Add(dr);
        //    }
        //    gridControl1.DataSource = dt;

        //}
        public GridControl DataGrid
        {
            get { return this.gridControl1; }
        }
    }

    public class General
    {

        public delegate void PageChangedHandle(int currentPageIndex);
    }
}
