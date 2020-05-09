using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.TcpTest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Impl.TcpTest
{
    public class TcpTestQueryServiceImpl : ServiceBase, ITcpTestQueryService
    {
        public IList<DaoModel.Products> GetWaitBindHangerProductList(string hangingStationNo)
        {
            var sql = new StringBuilder(@"select * from Products where HangingPieceSiteNo=? and Status=?");
            return Query<DaoModel.Products>(sql, null, false, hangingStationNo, ProductsStatusType.Allocationed.Value);
        }
        public void RegisterHangerToProducts(string hangingStationNo, string hangerNo)
        {
            if (string.IsNullOrEmpty(hangingStationNo))
            {
                throw new ApplicationException("挂片站编号不能为空!");
            }
            var currentHangingProductsList = GetWaitBindHangerProductList(hangingStationNo);
            if (currentHangingProductsList.Count == 0)
            {
                log.Info(string.Format("挂片站:【{0}】无产品可绑定!", hangingStationNo));
                return;
            }
            log.Info(string.Format("挂片站:【{0}】可绑定产品条数:【{0}】", hangingStationNo, currentHangingProductsList.Count));
            foreach (var p in currentHangingProductsList)
            {
                var wp = new DaoModel.WaitProcessOrderHanger();
                lock (wp)
                {
                    //wp.HangerNo = hangerNo?.Trim();
                    //wp.ProcessOrderId = p.ProcessOrder?.Id;
                    //wp.ProcessOrderNo = p.ProcessOrder?.POrderNo?.Trim();
                    //wp.PColor = p.PColor?.Trim();
                    //wp.PSize = p.PSize?.Trim();
                    wp.SiteNo = p.HangingPieceSiteNo?.Trim();
                    //var pfcProcessFlowList = GetFlowChartFlowItemList(p.ProcessFlowChart?.Id);
                    //var firstProcessFlow = pfcProcessFlowList.Where(f => f.CraftFlowNo.Equals("1")).Single();
                    var hangingPieceFlow = GetFirstProcessFlow(p.ProcessFlowChart);
                    wp.ProcessFlowId = hangingPieceFlow.ProcessFlow?.Id;
                    //wp.ProcessFlowCode = hangingPieceFlow.FlowCode?.Trim();
                    //wp.ProcessFlowName = hangingPieceFlow.FlowName?.Trim();
                    wp.IsFlowChatChange = false;
                    WaitProcessOrderHangerDao.Instance.Save(wp);
                    log.Info(string.Format("衣架【{0}】已成功绑定到制单【{1}】!", hangerNo, wp.ProcessOrderNo));

                   //GetHangerNextProcessFlowStatingList(p.ProcessFlowChart?.Id, hangingPieceFlow.ProcessflowId);
                }
            }
        }
        private DaoModel.ProcessFlowChartFlowRelation GetFirstProcessFlow(DaoModel.ProcessFlowChart pfChart)
        {
            var pfcProcessFlowList = GetFlowChartFlowItemList(pfChart?.Id);
            if (string.IsNullOrEmpty(pfChart.BoltProcessFlowId)) {
                return pfcProcessFlowList.Where(f => f.CraftFlowNo.Equals("1")).Single(); ;
            }
           // DaoModel.ProcessFlowChart
           //存在挂片工序
            return GetFlowChartProcessFlow(pfChart.Id,pfChart.BoltProcessFlowId);
        }
        /// <summary>
        /// 根据路线图+工序Id 获取路线图工序详情
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <param name="processFlowId"></param>
        /// <returns></returns>
        private DaoModel.ProcessFlowChartFlowRelation GetFlowChartProcessFlow(string flowChartId,string processFlowId)
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlowChartFlowRelation] where PROCESSFLOWCHART_Id=:flowChartId and PROCESSFLOW_Id=:processFlowId");
            var session = SessionFactory.OpenSession();
            var pfcFlowRelation = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowChartFlowRelation))
                .SetString("flowChartId", flowChartId)
                .SetString("processFlowId", processFlowId).UniqueResult<DaoModel.ProcessFlowChartFlowRelation>();
            return pfcFlowRelation;
        }

        /// <summary>
        /// 获取工艺路线图的所有工序列表
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        private IList<DaoModel.ProcessFlowChartFlowRelation> GetFlowChartFlowItemList(string flowChartId)
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlowChartFlowRelation] where PROCESSFLOWCHART_Id=:flowChartId");
            var session = SessionFactory.OpenSession();
            var list = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowChartFlowRelation))
                .SetString("flowChartId", flowChartId)
                .List<DaoModel.ProcessFlowChartFlowRelation>();
            return list;
        }
        /// <summary>
        /// 获取工序站点列表
        /// </summary>
        public IList<DaoModel.ProcessFlowStatingItem> GetHangerNextProcessFlowStatingList(string pfChartId,string processFlowId)
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlowChartFlowRelation] where PROCESSFLOWCHART_Id=:flowChartId and PROCESSFLOW_Id=:processFlowId");
            var session = SessionFactory.OpenSession();
            var pfcFlowRelation = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowChartFlowRelation))
                .SetString("flowChartId", pfChartId)
                .SetString("processFlowId", processFlowId).UniqueResult<DaoModel.ProcessFlowChartFlowRelation>();
            if (null== pfcFlowRelation) {
                log.Info(string.Format("【第一道工序错误】--->路线图Id【{0}】,工序Id【{1}】 不存在!", pfChartId, processFlowId));
                return new List<DaoModel.ProcessFlowStatingItem>();
            }
            //获取可接收的衣架
            var sqlProcessFlowItem = string.Format("select * from ProcessFlowStatingItem where IsReceivingHanger=1 and PROCESSFLOWCHARTFLOWRELATION_Id=:ProcessFlowChartFlowRelationId");
            var statingList = session.CreateSQLQuery(sqlProcessFlowItem)
                .AddEntity(typeof(DaoModel.ProcessFlowStatingItem))
                .SetString("ProcessFlowChartFlowRelationId", pfcFlowRelation.Id)
                .List<DaoModel.ProcessFlowStatingItem>();

            return statingList;
        }

        /// <summary>
        /// 计算衣架的下一站
        /// </summary>
        /// <param name="statingNos"></param>
        /// <returns></returns>
        string CalculationNextSating(string statingNos) {
            var sql = string.Format("select SiteNo,ISNULL(COUNT(1),0) StatingNum from WaitProcessOrderHanger where SiteNo IN('{0}') and IsIncomeSite=1 Group BY SiteNo", statingNos.Replace(",", "','").Replace(" ", ""));
            var wpOrderHanderList = Query<DaoModel.Cus.StatingModel>(new StringBuilder(sql),null,true,null);
            if (wpOrderHanderList.Count == 0) {
                return null;
            }
            return wpOrderHanderList.OrderBy(f => f.StatingNum).SingleOrDefault().SiteNo;
        }
        public DaoModel.Products GetProducts(string productionNumber, string hangingPieceSiteNo) {
            string queryString = string.Format("select top 1 * from [dbo].[Products] where productionNumber=:productionNumber and hangingPieceSiteNo=:hangingPieceSiteNo");
            var session = SessionFactory.OpenSession();
            var products = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.Products))
                .SetString("productionNumber", productionNumber)
                .SetString("hangingPieceSiteNo", hangingPieceSiteNo).UniqueResult<DaoModel.Products>();
            return products;
        }
        public IList<DaoModel.ProcessFlowStatingItem> GetProcessChartStatingList(string productionNumber, string hangingPieceSiteNo)
        {
            string queryString = string.Format("select top 1 * from [dbo].[Products] where productionNumber=:productionNumber and hangingPieceSiteNo=:hangingPieceSiteNo");
            var session = SessionFactory.OpenSession();
            var products = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.Products))
                .SetString("productionNumber", productionNumber)
                .SetString("hangingPieceSiteNo", hangingPieceSiteNo).UniqueResult<DaoModel.Products>();
            if (null== products) {
                log.Info(string.Format("排产号【{0}】挂片站【{1}】 找不到上线的产品!", productionNumber, hangingPieceSiteNo));
                return null;
            }
             var processFlowList=GetFlowChartFlowItemList(products.ProcessFlowChart.Id);
            List<DaoModel.ProcessFlowStatingItem> statingList = new List<DaoModel.ProcessFlowStatingItem>();
            foreach (var pf in processFlowList) {
                var pcStatList=GetHangerNextProcessFlowStatingList(pf.ProcessFlowChart?.Id, pf.ProcessFlow?.Id);
                var pcStatingArr = new DaoModel.ProcessFlowStatingItem[pcStatList.Count];
                statingList.AddRange(pcStatList.ToArray());
            }
            return statingList;
        }
    }
}
