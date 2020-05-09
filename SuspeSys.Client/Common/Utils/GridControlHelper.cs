using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Common.Utils
{
    public class GridControlHelper
    {
        public static object GetColumnTag(GridControl gc, string filedName) {
            var gv = gc.MainView as GridView;
            if (null!=gv) {
                foreach (GridColumn gColumn in gv.Columns) {
                    if(gColumn.FieldName.Equals(filedName)){
                        var tag = gColumn.Tag as DaoModel.PSize;
                        return tag;
                    }
                }
            }
            return string.Empty;
        }
    }
}
