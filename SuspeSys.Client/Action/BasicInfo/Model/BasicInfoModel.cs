using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Client.Action.BasicInfo.Model
{
    public class BasicInfoModel
    {
        public DaoModel.Style Style = new DaoModel.Style();
        public IList<DaoModel.StyleProcessFlowSectionItem> StyleProcessFlowSectionItemList = new List<DaoModel.StyleProcessFlowSectionItem>();

    }
}
