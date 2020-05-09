using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Modules.CuttingBed
{
    public class SusTreeList : DevExpress.XtraTreeList.TreeList
    {
        protected override RepositoryItem RequestCellEditor(DevExpress.XtraTreeList.ViewInfo.CellInfo cell)
        {
            return base.RequestCellEditor(cell);
        }
    }
}
