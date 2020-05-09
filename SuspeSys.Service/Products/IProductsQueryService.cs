using SuspeSys.Domain.Ext.CANModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Products
{
    public interface IProductsQueryService
    {
        /// <summary>
        /// 获取上线制单列表
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        IList<DaoModel.ProcessOrderModel> GetProcessOrderList(IDictionary<string, string> ht);
        /// <summary>
        /// 获取上线工艺路线图
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        IList<DaoModel.ProcessFlowChart> GetProcessFlowChartList(string pOrderId);

        /// <summary>
        /// 汇总客户制单颜色尺码
        /// </summary>
        /// <param name="pOrderId"></param>
        /// <returns></returns>
        IList<DaoModel.ProcessOrderExtModel> GetProcessOrderItemList(string pOrderId);

        IList<DaoModel.ProcessFlowChartFlowModel> GetProcessFlowChartFlowModelList(string pfChartId);
        IList<DaoModel.StatingModel> GetHangerPieceStatingList(string groupNo = null, string statingNo = null, string mainTrackNo=null);
        /// <summary>
        /// 在制品信息
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <param name="groupNo"></param>
        /// <returns></returns>
        IList<DaoModel.ProductsModel> SearchProductsList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey, string groupNo);
        long GetCurrentMaxProductionNumber();
        IList<EmployeeLoginInfo> GetEmployeeLoginInfoList(int mainTrackNumber,string statingNo, string cardNo);
        bool CheckStatingIsLogin(string statingNo,int mainTrackNumber);
    }
}
