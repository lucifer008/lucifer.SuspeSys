using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Common.Utils
{
   public static class GridViewUils
    {
        public static GridView FormatGridView(this GridView gv) {
            if (null!=gv) {
                var ds = gv.DataSource;
              //  var clumsn = gv.Columns;

                //foreach () {
                //}
            }
            return gv;
        }
    }
}
