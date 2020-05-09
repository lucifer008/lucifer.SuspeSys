using SuspeSys.Client.Action.BasicInfo.Model;
using SuspeSys.Domain.Cus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Client.Action.BasicInfo
{
    public class BasicInfoAction:BaseAction
    {

        public BasicInfoModel Model = new BasicInfoModel();
        public void AddStyleProcessFlow() {
            BasicInfoService.AddStyleProcessFlow(Model.Style,Model.StyleProcessFlowSectionItemList);
        }
        public void UpdateStyleProcessFlow()
        {
            BasicInfoService.UpdateStyleProcessFlow(Model.Style, Model.StyleProcessFlowSectionItemList);
        }
        public IList<DaoModel.PoColor> SearchBasicColorTable(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return BasicInfoService.SearchBasicColorTable(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }
        internal IList<StyleProcessFlowSectionItemExtModel> SearchStyleProcessFlow(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return BasicInfoService.SearchStyleProcessFlow(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }

       public IList<DaoModel.BasicProcessFlow> SearchBasicProcessLirbary(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return BasicInfoService.SearchBasicProcessLirbary(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }

        public IList<DaoModel.DefectCodeTable> SearchDefectCodeTable(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return BasicInfoService.SearchDefectCodeTable(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }

        public IList<DaoModel.PSize> SearchPSize(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return BasicInfoService.SearchPSize(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }

        public IList<DaoModel.LackMaterialsTable> SearchLackMaterialsTable(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return BasicInfoService.SearchLackMaterialsTable(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }

        public IList<DaoModel.ProcessFlowSection> SearchProcessFlowSection(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return BasicInfoService.SearchProcessFlowSection(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }

        public IList<DaoModel.Style> SearchStyle(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            return BasicInfoService.SearchStyle(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
        }
    }
}
