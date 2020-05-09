using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain;

namespace SuspeSys.Service.BasicInfo
{
    public interface IBasicInfoService
    {
        void AddStyleProcessFlow(SuspeSys.Domain.Style style,IList<SuspeSys.Domain.StyleProcessFlowSectionItem> styleProcessFlowSectionItemList);
        void UpdateStyleProcessFlow(SuspeSys.Domain.Style style,IList<SuspeSys.Domain.StyleProcessFlowSectionItem> styleProcessFlowSectionItemList);
        IList<Domain.Cus.StyleProcessFlowSectionItemExtModel> SearchStyleProcessFlow(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string p);
        IList<Domain.PoColor> SearchBasicColorTable(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
        IList<BasicProcessFlow> SearchBasicProcessLirbary(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
        IList<ProcessFlowSection> SearchProcessFlowSection(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
        IList<PSize> SearchPSize(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
        IList<DefectCodeTable> SearchDefectCodeTable(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
        IList<Style> SearchStyle(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
        IList<LackMaterialsTable> SearchLackMaterialsTable(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
    }
}
