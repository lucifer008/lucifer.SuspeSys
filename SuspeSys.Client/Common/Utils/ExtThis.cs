using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Modules.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Common.Utils
{
    public static class ExtThis
    {
        /// <summary>
        /// 删除全部行
        /// </summary>
        /// <param name="gridView">GridView</param>
        public static void ClearRows(this GridView gridView)
        {
            bool _mutilSelected = gridView.OptionsSelection.MultiSelect;//获取当前是否可以多选
            if (!_mutilSelected)
                gridView.OptionsSelection.MultiSelect = true;
            gridView.SelectAll();
            gridView.DeleteSelectedRows();
            gridView.OptionsSelection.MultiSelect = _mutilSelected;//还原之前是否可以多选状态
        }
        /// <summary>
        /// 删除全部行
        /// </summary>
        /// <param name="gridView">GridView</param>
        public static void ClearRows(this SusGridView gridView)
        {
            bool _mutilSelected = gridView.OptionsSelection.MultiSelect;//获取当前是否可以多选
            if (!_mutilSelected)
                gridView.OptionsSelection.MultiSelect = true;
            gridView.SelectAll();
            gridView.DeleteSelectedRows();
            gridView.OptionsSelection.MultiSelect = _mutilSelected;//还原之前是否可以多选状态
        }
        /// <summary>
        /// 删除选中行
        /// </summary>
        /// <param name="gridView">GridView</param>
        public static void ClearSelectRow(this GridView gridView)
        {
            if (gridView.GetSelectedRows().Length > 0)
            {
              //  var selIndex = gridView.GetSelectedRows()[0];
                gridView.DeleteSelectedRows();
                gridView.RefreshData();
            }
        }
  
    }
}
