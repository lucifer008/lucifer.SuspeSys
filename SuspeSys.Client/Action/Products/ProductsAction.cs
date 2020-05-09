using SuspeSys.Client.Action.Products.Model;
using SuspeSys.Client.Action.SuspeRemotingClient;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Service.Impl.CommonService;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.Customer.Model;
using SuspeSys.Domain.Common;
using SuspeSys.Service.Impl.Products.SusCache.Model;

using SuspeSys.Service.SusTcp;
using SuspeSys.Service.Impl.SusCache;
using SuspeSys.SusRedis.SusRedis.SusConst;

namespace SuspeSys.Client.Action.Products
{
    public class ProductsAction : BaseAction
    {
        private string errMsg = null;
        public Model.ProductsModel Model = new Model.ProductsModel();
        public static ProductsAction Instance { get { return new ProductsAction(); } }
        /// <summary>
        /// 获取上线制单
        /// </summary>
        public void SearchProcessOrderList()
        {
            Model.ProderOrderModelList = pQueryService.GetProcessOrderList(null);
        }
        public void SearchProcessFlowSection()
        {
            var commonQueryService = new CommonServiceImpl<ProcessFlowSection>();
            Model.ProcessFlowSectionList = commonQueryService.GetList();
        }
        public void SearchFlowChart(string pOrderId)
        {
            //var ht = new Hashtable();
            Model.ProcessFlowChartList = pQueryService.GetProcessFlowChartList(pOrderId);
        }
        public void SearchProcessOrderItemList(string pOrderId)
        {
            //var ht = new Hashtable();
            Model.ProcessOrderExtModelList = pQueryService.GetProcessOrderItemList(pOrderId);
        }
        public void SearchProcessFlowChartFlowModelList(string pfChartId)
        {
            Model.ProcessFlowChartFlowModelList = pQueryService.GetProcessFlowChartFlowModelList(pfChartId);
        }
        public IList<SuspeSys.Domain.StatingModel> GetHangerPieceStatingList(string groupNo = null)
        {
            return pQueryService.GetHangerPieceStatingList(groupNo);
        }
        public bool AddProducts(ref int currentProductNumber, string mainTrackNumber = null)
        {
            var pNumber = productService.GetCurrentProductNumber(CurrentUser.Instance.CurrentSiteGroup.GroupNo?.Trim());//.GetCurrentProductNumber();
            if (pNumber > 255)
            {
                currentProductNumber = (int)pNumber;
                return false;
            }
            return productService.AddProducts(Model.ProductsList, mainTrackNumber, pNumber);
        }
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
        public IList<SuspeSys.Domain.ProductsModel> SearchProductsList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey, string groupNo)
        {
            return pQueryService.SearchProductsList(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey, groupNo);
        }
        public long GetCurrentMaxProductionNumber()
        {
            return pQueryService.GetCurrentMaxProductionNumber();
        }
        public void ProductsHangingPiece(string productsId)
        {
            productService.ProductsHangingPiece(productsId);
        }

        /// <summary>
        /// 客户机指定产品上线
        /// </summary>
        /// <param name="productsId"></param>
        public bool ProductsOnline(DaoModel.Products p, ref string errMsg)
        {
            //bool sucess = false;
            //try
            //{
            //     sucess = productService.ProductsOnline(p);
            //}
            //catch (SocketException ex) {
            //    log.Error(ex);
            //    errMsg = "悬挂客户端未连接到采集服务，请检测服务是否正常!,若服务后启动请重新启动客户端!";
            //    return false;
            //}
            bool sucess = productService.ProductsOnline(p);
            if (sucess)
            {

                productService.ClientMancheOnLine(p, 0, ref errMsg);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        public bool ClearHangingPiece(string productId)
        {
            return productService.ClearHangingPiece(productId);
        }
        public bool AllocationHangerPiece(string productId, string hangingPieceNo)
        {
            return productService.AllocationHangerPiece(productId, hangingPieceNo);
        }

        public bool MarkSuccessProducts(DaoModel.Products row, ref string errMsg)
        {
            return productService.MarkSuccessProducts(row, ref errMsg);
        }
        public IList<ProductRealtimeInfoModel> SearchProductRealtimeInfo(string flowChartId, string groupNo)
        {
            IProductRealtimeInfoService pRealtimeInfoService = new ProductRealtimeInfoServiceImpl();
            flowChartId = flowChartId == null ? string.Empty : flowChartId;
            var list = pRealtimeInfoService.SearchProductRealtimeInfo(flowChartId, groupNo); //SuspeRemotingService.productRealtimeInfoService.SearchProductRealtimeInfo(flowChartId, groupNo);
            if (!string.IsNullOrEmpty(flowChartId))
            {
                var allocationList = pRealtimeInfoService.GetAllocationFlow(flowChartId);
                foreach (var m in list)
                {
                    //  m.IsReceive = (null != m.IsReceivingHanger && m.IsReceivingHanger.Value == 1) ? true : false;
                    //var flows = allocationList.Where(f => f.SiteNo.Equals(m.StatingNo.Trim())).Select(z => z.ProcessFlowName).Distinct();
                    m.ProcessFlowName = pRealtimeInfoService.GetAllocationFlow(m.StatingNo?.Trim(), m.MainTrackNumber.Value, allocationList); //string.Join(",", flows);
                    if ("挂片站".Equals(m.StatingName?.Trim()))
                    {

                        var aList = allocationList.Where(f => string.IsNullOrEmpty(f.MergeProcessFlowChartFlowRelationId) && string.IsNullOrEmpty(f.SiteNo)).Select(f => f.ProcessFlowName);
                        m.ProcessFlowName = aList.Count() > 0 ? aList.First() : null;
                    }
                    var mainStatingKey = string.Format("{0}:{1}", m.MainTrackNumber, m.StatingNo.Trim());
                    //var dicMainTrackStating = SusRedisClient.RedisTypeFactory.GetDictionary<string, MainTrackStatingCacheModel>(SusRedisConst.MAINTRACK_STATING_STATUS);
                    var dicMainTrackStating = SuspeSys.Service.Impl.SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, MainTrackStatingCacheModel>(SusRedisConst.MAINTRACK_STATING_STATUS);
                    if (dicMainTrackStating.Keys.Contains(mainStatingKey))
                    {
                        var mainTrackStating = dicMainTrackStating[mainStatingKey];
                        m.FullSite = mainTrackStating.IsFullSite ? "满站" : "";
                    }
                    var dicStatingInNumCache = SuspeSys.Service.Impl.SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);
                    if (dicStatingInNumCache.Keys.Contains(mainStatingKey))
                    {
                        m.StatingInCount = dicStatingInNumCache[mainStatingKey].ToString();
                    }
                    var dicAllocationNumCache = SuspeSys.Service.Impl.SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM);
                    if (dicAllocationNumCache.Keys.Contains(mainStatingKey))
                    {
                        var onlineCount = dicAllocationNumCache[mainStatingKey];
                        m.OnlineHangerCount = onlineCount > 0 ? onlineCount.ToString() : "0";
                    }
                    var stardHourTotal = m.TotalStanardHours;
                    var reaiyHourTotal = m.ReailHours;
                    //车缝效率 = 标准总工时÷实际总投入工时×100 %
                    if (reaiyHourTotal == 0)
                    {
                        m.SeamsEfficiencySite = "0%";
                    }
                    else
                    {
                        m.SeamsEfficiencySite = string.Format("{0:0%}", (decimal.Parse(stardHourTotal.ToString()) / reaiyHourTotal));
                    }
                }
            }

            return list;
        }
        public SuspeSys.Domain.ProductsModel GetOnLineProduct(string groupNo)
        {
            IProductRealtimeInfoService pRealtimeInfoService = new ProductRealtimeInfoServiceImpl();
            var p = pRealtimeInfoService.GetOnLineProduct(groupNo);
            return p;
            // return SuspeRemotingService.productRealtimeInfoService.GetOnLineProduct(groupNo);
        }

        internal IList<OnlineOrInStationItemModel> SearchOnlineOrInStationItem(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string statingNo, string productId,int mainTrackNumber)
        {
            return ProductRealtimeInfoServiceImpl.Instance.SearchOnlineOrInStationItem(currentPageIndex, pageSize, out totalCount, ordercondition, statingNo, productId, mainTrackNumber);
        }
    }
}
