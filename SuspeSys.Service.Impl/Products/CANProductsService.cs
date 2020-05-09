using log4net;
using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DaoModel = SuspeSys.Domain;
using Sus.Net.Common.Model;
using Sus.Net.Common.Constant;
using SusNet.Common.Utils;
using SuspeSys.Service.Impl.Products.SusThread;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Domain.Ext;
using SuspeSys.Service.Impl.Products.SusCache;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using Newtonsoft.Json;
using SuspeSys.Utils;
using SuspeSys.Domain.Cus;
using SuspeSys.Domain.Ext.CANModel;
using SuspeSys.Service.Impl.SusTcp;
using SuspeSys.Service.Impl.Core.Cache;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Service.Impl.Core.Bridge;
using SuspeSys.Service.Impl.Core.Flow;
using SuspeSys.Service.Impl.Core.OutSite;
using SuspeSys.Service.Impl.Core;
using SuspeSys.Service.Impl.Products.SusCache.Service;

namespace SuspeSys.Service.Impl.Products
{
    public class CANProductsService : ServiceBase
    {
        private static readonly CANProductsService _instance = new CANProductsService();
        public static CANProductsService Instance
        {
            get { return _instance; }
        }
        public bool IsHangPieceStating(int mainTrackNo, int statingNo)
        {
            var mainKey = string.Format("{0}:{1}", mainTrackNo, statingNo);
            var dicStatingCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.StatingModel>(SusRedisConst.STATING_TABLE);
            if (dicStatingCache.ContainsKey(mainKey))
            {
                return null != dicStatingCache[mainKey].StatingRoles ? dicStatingCache[mainKey].StatingRoles.RoleCode.Trim().Equals(StatingType.StatingHanger.Code) : false;
            }
            var ex = new ApplicationException(string.Format("主轨:{0} 站点:{1} 站点站类型不存在", mainTrackNo, statingNo));
            tcpLogError.Error(ex);
            throw ex;
        }

        internal bool IsReworkFlow(int mainTrackNo, int statingNo, string tenHangerNo)
        {
            var fcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo);
            if (fcList.Where(f => f.StatingNo != null && f.StatingNo.Value == statingNo && f.MainTrackNumber.Value == mainTrackNo && f.FlowType.Value == 1 && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool IsStatingStorage(int mainTrackNo, int statingNo)
        {
            var mainKey = string.Format("{0}:{1}", mainTrackNo, statingNo);
            var dicStatingCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.StatingModel>(SusRedisConst.STATING_TABLE);
            if (dicStatingCache.ContainsKey(mainKey))
            {
                return null != dicStatingCache[mainKey].StatingName ? dicStatingCache[mainKey].StatingRoles.RoleCode.Trim().Equals(StatingType.StatingStorage.Code) : false;
            }
            var ex = new ApplicationException(string.Format("主轨:{0} 站点:{1} 站点站类型不存在", mainTrackNo, statingNo));
            throw ex;

        }
        public void UpdateMainTrackStatingStatus(int mainTrackNo, int statingNo, bool isFullSite)
        {
            var mainTrackStatingInfo = new MainTrackStatingCacheModel()
            {
                MainTrackNumber = mainTrackNo,
                StatingNo = statingNo,
                IsFullSite = isFullSite
            };
            var maintrackStatingStatusJson = Newtonsoft.Json.JsonConvert.SerializeObject(mainTrackStatingInfo);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_STATUS, maintrackStatingStatusJson);
            //MAINTRACK_STATING_INFO
        }
        /// <summary>
        /// 记录返工衣架疵点信息，并计算返工站是否满足进站条件等
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="defectCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool RecordReworkDefectHangerInfo(int mainTrackNo, int hangerNo, int statingNo, int defectCode, ref string nextStatingNo, ref string errMsg)
        {
            //查找工序的制作站点
            var reworHangerReworkRecordInfo = CANProductsValidService.Instance.GetReworHangerReworkRecordInfo(mainTrackNo, hangerNo, statingNo, defectCode);
            reworHangerReworkRecordInfo.DefectCode = defectCode.ToString();
            HangerReworkRecordDao.Instance.Save(reworHangerReworkRecordInfo);

            //计算返工站是否满足进站条件，如果满足返工到达的站点号
            var wetherAllow = CANProductsValidService.Instance.CalculationWhetherAllowIncomeStatingAndNextStating(reworHangerReworkRecordInfo, ref nextStatingNo);
            return wetherAllow;
        }

        /// <summary>
        /// 记录返工请求
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="reworkFlowCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool RecordReworkHangerInfo(int mainTrackNo, int hangerNo, int statingNo, ref string errMsg)
        {
            var list = productsQueryService.GetHangerPieceStatingList(null, statingNo.ToString(), mainTrackNo.ToString());
            var isHangingPieceStating = list.Count > 0;
            if (isHangingPieceStating)//如果出战消息来源于挂片站
            {
                var ex = new HangingPieceReworkException(string.Format("【衣架返工】 挂片站不能返工! 主轨:{0} 衣架:{1} 站点:{2}", mainTrackNo, hangerNo, statingNo));
                tcpLogError.Error(ex);
                throw ex;
            }


            ////查找工序的制作站点
            //// var dicSort = new Dictionary<string, string>();
            //var sql = new StringBuilder("select * from HangerProductItem where HangerNo =? and ProcessFlowCode=?");
            //var reworkHangerInfo = QueryForObject<DaoModel.HangerProductItem>(sql, null, false, hangerNo);
            //if (null == reworkHangerInfo)
            //{
            //    var ex = new ApplicationException(string.Format("【衣架返工】 找不到返工衣架的上一站点信息! 主轨:{0} 衣架:{1} 站点:{2} 返工工序代码:{3}", mainTrackNo, hangerNo, statingNo, reworkFlowCode));
            //    tcpLogError.Error(ex);
            //    throw ex;
            //}

            //var reworkRecord = BeanUitls<DaoModel.HangerReworkRecord, DaoModel.HangerProductItem>.Mapper(reworkHangerInfo);
            //reworkRecord.ReturnWorkSiteNo = statingNo.ToString();
            //reworkRecord.IsSucessedFlow = false;
            //HangerReworkRecordDao.Instance.Save(reworkRecord);
            return true;
        }

        /// <summary>
        /// 比较衣架工序信息【衣架号，颜色，尺码，工序】
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="tag">0:工序相同;1:工序不同:2:返工衣架</param>
        /// <param name="info">衣架工序不同时发送给硬件的消息</param>
        /// <param name="errMsg"></param>
        /// <param name="isReworkFlow"></param>
        /// <param name="isPowerSupply">是否是断电重启</param>
        /// <returns></returns>
        public bool CompareHangerFlowExt(string mainTrackNo, string hangerNo, string statingNo, ref int tag, ref string info, ref string errMsg, ref bool isReworkFlow, bool isPowerSupply = false)
        {


            DaoModel.HangerProductItemModel currentHangerProductItem = null;
            var dicHangerProductItemCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM);
            if (dicHangerProductItemCache.ContainsKey(hangerNo))
            {
                var hangerProductItemList = dicHangerProductItemCache[hangerNo];
                foreach (var hpi in hangerProductItemList)
                {
                    if (hpi.SiteNo.Equals(statingNo) && hpi.MainTrackNumber.Value == short.Parse(mainTrackNo))// && hpi.CompareDate == null)
                    {
                        currentHangerProductItem = hpi;
                        hpi.CompareDate = DateTime.Now;
                        //hpi.FlowChartd=
                        dicHangerProductItemCache[hangerNo] = hangerProductItemList;
                        break;
                    }
                }
                if (null == currentHangerProductItem)
                {
                    currentHangerProductItem = hangerProductItemList.OrderByDescending(f => f.OutSiteDate).First();
                }
            }
            ////获取站内最近出战的衣架信息
            //IDictionary<string, string> dicOrder = new Dictionary<string, string>();
            //dicOrder.Add("OutSiteDate", "DESC");
            //var sql = new StringBuilder("select * from StatingHangerProductItem where SiteNo=?");
            //var statingHangerProductItem = QueryForObject<DaoModel.StatingHangerProductItem>(sql, dicOrder, false, int.Parse(statingNo));

            ////获取当前衣架信息
            //var currentHangerSQL = new StringBuilder("select * from WaitProcessOrderHanger where HangerNo=?");
            //var currentFlow = QueryForObject<DaoModel.WaitProcessOrderHanger>(currentHangerSQL, null, false, hangerNo);

            ////修正衣架生产记录的比较时间
            //var hangerProductItemSQL = new StringBuilder("select * from HangerProductItem where HangerNo=? and SiteNo=?");
            //var hangerProductItem = QueryForObject<DaoModel.HangerProductItem>(hangerProductItemSQL, null, false, hangerNo, int.Parse(statingNo));
            //hangerProductItem.CompareDate = DateTime.Now;
            //HangerProductItemDao.Instance.Update(hangerProductItem);



            //更新衣架生产工艺图制作站点的信息

            //var ppChart = GetHangerProductFlowChart(int.Parse(mainTrackNo), hangerNo, statingNo);
            //if (null == ppChart)
            //{
            //    var ex = new ApplicationException(string.Format("找不着衣架生产工艺图信息! 主轨:{0} 衣架号:{1} 站点:{2}", mainTrackNo, hangerNo, statingNo));
            //    errorLog.Error("【衣架工序比较】", ex);

            //}
            //ppChart.CompareDate = DateTime.Now;
            //HangerProductFlowChartDao.Instance.Update(ppChart);

            //【已分配未进站，出站】手动拿进站的衣架
            if (currentHangerProductItem == null)
            {
                //var dicHangerProcessFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                if (NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo))//dicHangerProcessFlowChartCache.ContainsKey(hangerNo))
                {
                    var hpfcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo);//dicHangerProcessFlowChartCache[hangerNo];

                    foreach (var fItem in hpfcList)
                    {
                        if (fItem.MainTrackNumber.Value == short.Parse(mainTrackNo) && null != fItem.StatingNo && fItem.StatingNo.Value == short.Parse(statingNo)
                            //&& fItem.Status.Value==HangerProductFlowChartStaus.WaitingProducts.Value
                            && fItem.IncomeSiteDate == null && fItem.AllocationedDate != null && fItem.isAllocationed && fItem.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value)
                        {
                            currentHangerProductItem = new Domain.HangerProductItemModel();
                            currentHangerProductItem.HangerNo = hangerNo;
                            currentHangerProductItem.SiteNo = statingNo;
                            currentHangerProductItem.PSize = fItem.PSize;
                            currentHangerProductItem.PColor = fItem.PColor;
                            currentHangerProductItem.SizeNum = string.IsNullOrEmpty(fItem.Num) ? 0 : int.Parse(fItem.Num);
                            currentHangerProductItem.FlowNo = fItem.FlowNo;
                            currentHangerProductItem.ProcessFlowName = fItem.FlowName;
                            currentHangerProductItem.ProcessOrderNo = fItem.ProcessOrderNo;
                            currentHangerProductItem.ProcessFlowId = fItem.FlowId;
                            currentHangerProductItem.ProductsId = fItem.ProductsId;
                            currentHangerProductItem.MainTrackNumber = fItem.MainTrackNumber;
                            currentHangerProductItem.FlowIndex = (short)fItem.FlowIndex;
                            currentHangerProductItem.ProcessFlowCode = fItem.FlowCode;
                            currentHangerProductItem.LineName = fItem.LineName;
                            currentHangerProductItem.FlowChartd = fItem.ProcessChartId;
                            currentHangerProductItem.CompareDate = DateTime.Now;
                            currentHangerProductItem.IsMergeForward = (null != fItem.IsMergeForward) ? fItem.IsMergeForward.Value : false;
                            currentHangerProductItem.MergeProcessFlowChartFlowRelationId = fItem?.MergeProcessFlowChartFlowRelationId;
                            currentHangerProductItem.ProcessFlowChartFlowRelationId = fItem?.ProcessFlowChartFlowRelationId;
                            currentHangerProductItem.IsReturnWorkFlow = (null != fItem.FlowType && fItem.FlowType.Value == 2);
                            //currentHangerProductItem.s = fItem.StanardHours,
                            //StandardPrice = hpfc?.StandardPrice,
                            currentHangerProductItem.FlowChartd = fItem.ProcessChartId;
                            var currentHangerProductItemModel = Utils.Reflection.BeanUitls<DaoModel.HangerProductItemModel, DaoModel.HangerProductItem>.Mapper(currentHangerProductItem);
                            var hangerProductItemCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM);
                            if (!hangerProductItemCache.ContainsKey(hangerNo))
                            {
                                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM).Add(hangerNo, new List<Domain.HangerProductItemModel>() { currentHangerProductItemModel });
                            }

                            else
                            {
                                var hpList = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM)[hangerNo];

                                hpList.Add(currentHangerProductItemModel);
                                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM)[hangerNo] = hpList;
                            }
                            break;
                        }
                    }
                }
            }

            if (currentHangerProductItem == null)
            {
                throw new ApplicationException(string.Format("【衣架比较异常】 主轨:{0} 站点:{1} 衣架:{2}", mainTrackNo, statingNo, hangerNo));
            }
            var mergeProcessFlowNos = string.Empty;
            var mergeProcessFlowNames = string.Empty;

            HangerProductFlowChartModel currentHangerProductFlowChart = null;
            //   var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo))//dicHangerProcessFlowChart.ContainsKey(hangerNo))
            {
                var hpfChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo); //dicHangerProcessFlowChart[hangerNo];
                foreach (var pf in hpfChartList)
                {
                    if (pf.StatingNo.Value == short.Parse(statingNo) && pf.MainTrackNumber.Value == short.Parse(mainTrackNo) && (null == pf.IsFlowSucess || !pf.IsFlowSucess.Value))
                    {
                        pf.CompareDate = DateTime.Now;
                        currentHangerProductFlowChart = pf;
                    }
                }
                //dicHangerProcessFlowChart[hangerNo] = hpfChartList;
                NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(hangerNo, hpfChartList);
                var isExistMergeProcess = hpfChartList.Where(f => null != f.MergeProcessFlowChartFlowRelationId && f.MergeProcessFlowChartFlowRelationId.Equals(currentHangerProductItem.ProcessFlowChartFlowRelationId)).Count() > 0;
                if (isExistMergeProcess)
                {
                    mergeProcessFlowNos = string.Join(",", hpfChartList.Where(f => null != f.MergeProcessFlowChartFlowRelationId && f.MergeProcessFlowChartFlowRelationId.Equals(currentHangerProductItem.ProcessFlowChartFlowRelationId)).Select(k => k.FlowNo?.Trim()));
                    mergeProcessFlowNames = string.Join(",", hpfChartList.Where(f => null != f.MergeProcessFlowChartFlowRelationId && f.MergeProcessFlowChartFlowRelationId.Equals(currentHangerProductItem.ProcessFlowChartFlowRelationId)).Select(k => k.FlowName?.Trim()));
                }
            }

            //var productCache = SusCache.Service.SusCacheProductService.Instance.GetOnLineProeuctCacheById(currentHangerProductItem.ProductsId);
            //string rPO = productCache.Po?.Trim();

            ////比较工序
            DaoModel.HangerProductItemModel lastHangerInfo = null;
            string lastCompareStr = string.Empty;
            //获取最后一次读卡的衣架
            var currentStatingHangerList = new List<DaoModel.HangerProductItemModel>();
            foreach (var key in dicHangerProductItemCache.Keys)
            {
                var lst = dicHangerProductItemCache[key].Where(f => !f.HangerNo.Equals(hangerNo) && f.SiteNo == statingNo && f.MainTrackNumber.Value == short.Parse(mainTrackNo)).ToList<DaoModel.HangerProductItemModel>();
                currentStatingHangerList.AddRange(lst.ToArray());
            }
            if (currentStatingHangerList.Count > 0)
            {
                var lastHangerList = currentStatingHangerList.OrderByDescending(f => f.CompareDate);
                if (lastHangerList.Count() > 0)
                {
                    lastHangerInfo = lastHangerList.First();
                    lastCompareStr = lastHangerInfo?.ProcessOrderNo?.Trim() + lastHangerInfo?.PColor?.Trim() + lastHangerInfo?.PSize?.Trim() + lastHangerInfo?.ProcessFlowId?.Trim();
                }
            }
            //var shpKey = string.Format("{0}:{1}", int.Parse(mainTrackNo), int.Parse(statingNo));
            //var dicStatingHangerProductItemCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.StatingHangerProductItem>>(SusRedisConst.STATING_HANGER_PRODUCT_ITEM);
            //if (dicStatingHangerProductItemCache.ContainsKey(shpKey))
            //{
            //    var statingHangerProductItemList = dicStatingHangerProductItemCache[shpKey];
            //    if (statingHangerProductItemList.Count > 0)
            //    {
            //        lastHangerInfo = statingHangerProductItemList.OrderByDescending(f => f.OutSiteDate).First();
            //        lastCompareStr = lastHangerInfo?.ProcessOrderNo?.Trim() + lastHangerInfo?.PColor?.Trim() + lastHangerInfo?.PSize?.Trim() + lastHangerInfo?.ProcessFlowId?.Trim();
            //    }
            //}

            var currrentCompareStr = currentHangerProductItem?.ProcessOrderNo?.Trim() + currentHangerProductItem?.PColor?.Trim() + currentHangerProductItem?.PSize?.Trim() + currentHangerProductItem?.ProcessFlowId?.Trim();
            //var lastCompareStr = statingHangerProductItem?.ProcessOrderNo?.Trim() + statingHangerProductItem?.PColor?.Trim() + statingHangerProductItem?.PSize?.Trim() + statingHangerProductItem?.ProcessFlowId?.Trim();
            isReworkFlow = currentHangerProductFlowChart?.FlowType == 1;
            tag = currentHangerProductFlowChart?.FlowType == 1 ? 2 : (currrentCompareStr.Equals(lastCompareStr) ? 0 : 1);
            if (isReworkFlow)
            {
                //info = string.Format("本衣架:{0},{1},{2},返工工序:{3};疵点:{4}", currentHangerProductFlowChart?.HangerNo?.Trim(), currentHangerProductFlowChart?.PColor?.Trim(), currentHangerProductFlowChart?.PSize?.Trim(), currentHangerProductFlowChart?.FlowCode?.Trim(), currentHangerProductFlowChart?.DefectCode?.Trim());
                var pOrderNo = currentHangerProductFlowChart?.ProcessOrderNo?.Trim();
                string rHangerNo = currentHangerProductFlowChart?.HangerNo?.Trim();
                string rColor = currentHangerProductFlowChart?.PColor?.Trim();
                string rSize = currentHangerProductFlowChart?.PSize?.Trim();
                //string rFlowCode = currentHangerProductFlowChart?.FlowCode?.Trim();
                string rFlowNo = currentHangerProductFlowChart?.FlowNo?.Trim();
                string rFlowName = currentHangerProductFlowChart?.FlowName?.Trim();
                string rDefectCode = currentHangerProductFlowChart?.DefectCode?.Trim();
                string rDefectName = GetDefectInfo(currentHangerProductFlowChart.DefectCode?.Trim())?.DefectName?.Trim();
                var rPieceNum = currentHangerProductFlowChart.Num?.Trim();
                var productCache = SusCache.Service.SusCacheProductService.Instance.GetOnLineProeuctCacheById(currentHangerProductItem.ProductsId);
                string rPO = productCache.Po?.Trim();
                /*
                 制单，PO、颜色、尺码、件数
                    返工工序：2，上领；...等
                    返工原因：2，破洞；...等

                1001, QR001，白色，S，1件
                返工工序
                返工原因
                 */

                //info = $"本衣架:{rHangerNo},{rColor},{rSize},PO:{rPO},返工工序:{rFlowCode};疵点:{rDefectCode}";
                info = $"{pOrderNo},{rPO},{rColor},{rSize},{rPieceNum}件,返工工序:{rFlowNo},{rFlowName};返工原因:{rDefectCode},{rDefectName}";
                return false;
            }
            //断电重启发送不比较衣架内容
            if (isPowerSupply)
            {
                //info = string.Format("本衣架:{0},{1},{2},分配工序:{3}", currentHangerProductItem?.ProcessOrderNo?.Trim(), currentHangerProductItem?.PColor?.Trim(), currentHangerProductItem?.PSize?.Trim(), currentHangerProductItem?.ProcessFlowCode?.Trim());
                // info = $"本衣架:{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},PO:{rPO},分配工序:{currentHangerProductItem?.ProcessFlowCode?.Trim()}"; ;
                if (string.IsNullOrEmpty(mergeProcessFlowNos))
                {
                    info = $"{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},{currentHangerProductItem?.SizeNum},本站工序:{currentHangerProductItem?.FlowNo?.Trim()},{currentHangerProductItem?.ProcessFlowName?.Trim()}";
                }
                else
                {
                    info = $"{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},{currentHangerProductItem?.SizeNum},本站工序:{currentHangerProductItem?.FlowNo?.Trim()},{mergeProcessFlowNos};{currentHangerProductItem?.ProcessFlowName?.Trim()},{mergeProcessFlowNames}";
                }

                if (null == lastHangerInfo)
                {
                    //info + string.Format("");
                    return false;
                }
                ////info += string.Format("前一衣架:{0},{1},{2},分配工序:{3}", lastHangerInfo?.ProcessOrderNo?.Trim(), lastHangerInfo?.PColor?.Trim(), lastHangerInfo?.PSize?.Trim(), lastHangerInfo?.ProcessFlowCode?.Trim());
                //info += $"前一衣架:{lastHangerInfo?.ProcessOrderNo?.Trim()},{lastHangerInfo?.PColor?.Trim()},{lastHangerInfo?.PSize?.Trim()},PO:{rPO},分配工序:{lastHangerInfo?.ProcessFlowCode?.Trim()}"; ;
                return false;
            }
            if (!currrentCompareStr.Equals(lastCompareStr))
            {
                //info = string.Format("本衣架:{0},{1},{2},分配工序:{3}", currentHangerProductItem?.ProcessOrderNo?.Trim(), currentHangerProductItem?.PColor?.Trim(), currentHangerProductItem?.PSize?.Trim(), currentHangerProductItem?.ProcessFlowCode?.Trim());
                // info = $"本衣架:{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},PO:{rPO},分配工序:{currentHangerProductItem?.ProcessFlowCode?.Trim()}";
                if (string.IsNullOrEmpty(mergeProcessFlowNos))
                {
                    info = $"{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},{currentHangerProductItem?.SizeNum},本站工序:{currentHangerProductItem?.FlowNo?.Trim()},{currentHangerProductItem?.ProcessFlowName?.Trim()}";
                }
                else
                {
                    info = $"{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},{currentHangerProductItem?.SizeNum},本站工序:{currentHangerProductItem?.FlowNo?.Trim()},{mergeProcessFlowNos};{currentHangerProductItem?.ProcessFlowName?.Trim()},{mergeProcessFlowNames}";
                }
                if (null == lastHangerInfo)
                {
                    //info + string.Format("");
                    return false;
                }
                ////info += string.Format("前一衣架:{0},{1},{2},分配工序:{3}", lastHangerInfo?.ProcessOrderNo?.Trim(), lastHangerInfo?.PColor?.Trim(), lastHangerInfo?.PSize?.Trim(), lastHangerInfo?.ProcessFlowCode?.Trim());
                //info += $"前一衣架:{lastHangerInfo?.ProcessOrderNo?.Trim()},{lastHangerInfo?.PColor?.Trim()},{lastHangerInfo?.PSize?.Trim()},PO:{rPO},分配工序:{lastHangerInfo?.ProcessFlowCode?.Trim()}";
                return false;
            }

            return true;
        }
        /// <summary>
        /// 衣架完成修正产出工序
        /// </summary>
        /// <param name="tenHangerNo"></param>
        /// <param name="ppChart"></param>
        internal void CorrectHangerSuccessOutFlow(string tenHangerNo, HangerProductFlowChartModel ppChart)
        {
            if (null == ppChart)
            {
                tcpLogError.Info($"异常衣架:{tenHangerNo} ppChart为空!");
                return;
            }
            var outFlowId = ppChart.FlowId;
            var outFlowNo = ppChart.FlowNo;
            var outFlowName = ppChart.FlowName;

            string sql = string.Format($"Update WaitProcessOrderHanger set OutFlowId=@outFlowId,OutFlowNo=@outFlowNo,OutFlowName=@outFlowName where ProductsId=@ProductsId and HangerNo=@tenHangerNo");
            var rows = DapperHelp.Execute(sql, new { outFlowId = outFlowId, outFlowNo = outFlowNo, outFlowName = outFlowName, tenHangerNo = tenHangerNo, ProductsId = ppChart.ProductsId });
        }

        internal void CorrectHangerResumeLastFlowSuccess(int tenMaintracknumber, int tenStatingNo, string tenHangerNo, string flowNo)
        {
            var hProductsChartResumeList = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[tenHangerNo];
            hProductsChartResumeList.ForEach(delegate (HangerProductFlowChartModel hpfc)
            {
                if (tenMaintracknumber == hpfc.MainTrackNumber.Value && hpfc.StatingNo != null && hpfc.StatingNo.Value == tenStatingNo && null != hpfc.FlowNo && hpfc.FlowNo.Equals(flowNo))
                {
                    hpfc.IsFlowSucess = true;
                }
            });
            SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME)[tenHangerNo] = hProductsChartResumeList;
        }

        /// <summary>
        /// 重新修正当前衣架生产的工序
        /// </summary>
        /// <param name="tenHangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="hangerProcessFlowChartList"></param>
        internal void RecalibrationHangerNextFlowHandler(string tenHangerNo, int outMainTrackNumber, List<HangerProductFlowChartModel> hangerProcessFlowChartList)
        {
            var mainTrackNumber = outMainTrackNumber;
            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(mainTrackNumber))
            {
                var ex = new ApplicationException(string.Format("主轨:{0}无桥接站!", outMainTrackNumber));
                tcpLogError.Error(ex);
                throw ex;
            }
            if (!dicBridgeCache.ContainsKey(dicBridgeCache[outMainTrackNumber].BMainTrackNumber.Value))
            {
                var ex = new ApplicationException(string.Format("主轨:{0}无桥接站!", dicBridgeCache[outMainTrackNumber].BMainTrackNumber));
                tcpLogError.Error(ex);
                throw ex;
            }
            var b_BridgeSet = dicBridgeCache[dicBridgeCache[outMainTrackNumber].BMainTrackNumber.Value];
            var nextFlowChartList = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value != 1 && f.StatingNo.Value != -1 && f.MainTrackNumber.Value == dicBridgeCache[outMainTrackNumber].BMainTrackNumber.Value
                         && (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
                        && (null != f.IsReceivingHangerStating && f.IsReceivingHangerStating.Value)
                        && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward));
            //var nextFlowChartList = nextFlowChartList;
            HangerProductFlowChartModel nextFlowChart = null;
            if (nextFlowChartList.Count() == 0)
            {
                var ex = new ApplicationException(string.Format("主轨:{0}无携带工序!", dicBridgeCache[outMainTrackNumber].BMainTrackNumber));
                tcpLogError.Error(ex);
                throw ex;
            }
            nextFlowChart = nextFlowChartList.OrderBy(f => f.FlowIndex.Value).First();
            //发布衣架下一站工序状态
            CANProductsService.Instance.CorrectHangerNextFlowHandler(tenHangerNo, nextFlowChart);
        }
        /// <summary>
        /// 桥接是否携带工序
        /// </summary>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="hangerProcessFlowChartList"></param>
        /// <returns></returns>
        internal bool IsBridgeInverseBridge(string outMainTrackNumber, string nextStatingNo, List<HangerProductFlowChartModel> hangerProcessFlowChartList)
        {
            var mainTrackNumber = int.Parse(outMainTrackNumber);
            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(mainTrackNumber))
            {
                var ex = new ApplicationException(string.Format("主轨:{0}无桥接站!", outMainTrackNumber));
                tcpLogError.Error(ex);
                throw ex;
            }
            var outStatingMaintrackNumberBridge = dicBridgeCache[mainTrackNumber];
            var isExistFlow = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value != 1
                        && f.StatingNo.Value != -1 && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value && f.MainTrackNumber.Value == outStatingMaintrackNumberBridge.AMainTrackNumber.Value
                       && (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
                      && (null != f.IsReceivingHangerStating && f.IsReceivingHangerStating.Value)
                      && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)).Count() > 0;
            return isExistFlow;
        }

        /// <summary>
        /// 桥接是否携带工序
        /// </summary>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="hangerProcessFlowChartList"></param>
        /// <returns></returns>
        internal HangerProductFlowChartModel GetBirdgeFlow(string outMainTrackNumber, string nextStatingNo, List<HangerProductFlowChartModel> hangerProcessFlowChartList)
        {
            HangerProductFlowChartModel nonSucessedFlow = null;
            var mainTrackNumber = int.Parse(outMainTrackNumber);
            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(mainTrackNumber))
            {
                var ex = new ApplicationException(string.Format("主轨:{0}无桥接站!", outMainTrackNumber));
                tcpLogError.Error(ex);
                throw ex;
            }
            var outStatingMaintrackNumberBridge = dicBridgeCache[mainTrackNumber];
            var bridgeFlowList = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value != 1 && f.StatingNo.Value != -1 && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value
            && f.MainTrackNumber.Value == outStatingMaintrackNumberBridge.AMainTrackNumber.Value
                       && (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
                      && (null != f.IsReceivingHangerStating && f.IsReceivingHangerStating.Value)
                      && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward));
            return bridgeFlowList.Count() > 0 ? bridgeFlowList.First() : nonSucessedFlow;
        }

        /// <summary>
        /// 桥接是否携带工序
        /// </summary>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="hangerProcessFlowChartList"></param>
        /// <returns></returns>
        internal bool IsBridgeInverseBridgeAndFlowSuccess(string outMainTrackNumber, string nextStatingNo, List<HangerProductFlowChartModel> hangerProcessFlowChartList, ref HangerProductFlowChartModel nonSucessedFlow)
        {
            var mainTrackNumber = int.Parse(outMainTrackNumber);
            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(mainTrackNumber))
            {
                var ex = new ApplicationException(string.Format("主轨:{0}无桥接站!", outMainTrackNumber));
                tcpLogError.Error(ex);
                throw ex;
            }
            var outStatingMaintrackNumberBridge = dicBridgeCache[mainTrackNumber];
            var isExistFlowSuccess = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value != 1 && f.StatingNo.Value != -1 && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value
            && f.MainTrackNumber.Value == outStatingMaintrackNumberBridge.AMainTrackNumber.Value && f.Status.Value == HangerProductFlowChartStaus.Successed.Value
                       && (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
                      && (null != f.IsReceivingHangerStating && f.IsReceivingHangerStating.Value)
                      && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)).Count() > 0;
            if (!isExistFlowSuccess)
            {
                var liist = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value != 1 && f.StatingNo.Value != -1 && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value
             && f.MainTrackNumber.Value == outStatingMaintrackNumberBridge.AMainTrackNumber.Value && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                        && (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
                       && (null != f.IsReceivingHangerStating && f.IsReceivingHangerStating.Value)
                       && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward));
                nonSucessedFlow = liist.Count() > 0 ? liist.First() : null;
            }
            return isExistFlowSuccess;
        }
        /// <summary>
        /// 桥接是携带工序且未完成
        /// </summary>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="hangerProcessFlowChartList"></param>
        /// <returns></returns>
        internal bool IsBridgeContainsFlowNonSuccess(string outMainTrackNumber, string nextStatingNo, List<HangerProductFlowChartModel> hangerProcessFlowChartList, ref HangerProductFlowChartModel nonSuccessFlow)
        {
            var mainTrackNumber = int.Parse(outMainTrackNumber);
            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(mainTrackNumber))
            {
                var ex = new ApplicationException(string.Format("主轨:{0}无桥接站!", outMainTrackNumber));
                tcpLogError.Error(ex);
                throw ex;
            }
            var outStatingMaintrackNumberBridge = dicBridgeCache[mainTrackNumber];
            var isContainsFlow = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value != 1 && f.StatingNo.Value != -1 && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value
            && f.MainTrackNumber.Value == outStatingMaintrackNumberBridge.AMainTrackNumber.Value //&& f.Status.Value == HangerProductFlowChartStaus.Successed.Value
                       && (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
                      && (null != f.IsReceivingHangerStating && f.IsReceivingHangerStating.Value)
                      && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)).Count() > 0;
            if (isContainsFlow)
            {
                var liist = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value != 1 && f.StatingNo.Value != -1 && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value
             && f.MainTrackNumber.Value == outStatingMaintrackNumberBridge.AMainTrackNumber.Value && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
                        && (null != f.IsReceivingHanger && f.IsReceivingHanger.Value == 1)
                       && (null != f.IsReceivingHangerStating && f.IsReceivingHangerStating.Value)
                       && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward));
                if (liist.Count() > 0)
                {
                    nonSuccessFlow = liist.First();
                }
                return liist.Count() > 0 ? true : false;
            }
            return false;
        }
        /// <summary>
        /// 比较衣架工序信息【衣架号，颜色，尺码，工序】
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="tag">0:工序相同;1:工序不同:2:返工衣架</param>
        /// <param name="info">衣架工序不同时发送给硬件的消息</param>
        /// <param name="errMsg"></param>
        /// <param name="isReworkFlow"></param>
        /// <param name="isPowerSupply">是否是断电重启</param>
        /// <returns></returns>
        public bool CompareHangerFlow(string mainTrackNo, string hangerNo, string statingNo, ref int tag, ref string info, ref string errMsg, ref bool isReworkFlow, ref string flowInfo, bool isPowerSupply = false)
        {
            lock (locObject)
            {
                //  var dicHangerProcessFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel current = null;
                //var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                if (!NewCacheService.Instance.HangerCurrentFlowIsContains(hangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(hangerNo))
                {
                    var infoEx = string.Format("【错误】主轨:【{0}】站点:【{1}】衣架【{2} 】 衣架未注册!", mainTrackNo, statingNo, hangerNo);

                    var ex = new ApplicationException(infoEx);
                    tcpLogError.Error(ex);
                    throw ex;
                }
                current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo); //dicCurrentHangerProductingFlowModelCache[hangerNo];
                var currentFlowNo = current.FlowNo;
                var isBridgeStating = IsBridge(int.Parse(mainTrackNo), int.Parse(statingNo));
                var nonFlowSuccessFlowNoAndInFlowChart = NonFlowSuccessFlowNoAndInFlowChart(mainTrackNo, statingNo, hangerNo, NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo)); //dicHangerProcessFlowChartCache[hangerNo]);
                if (isBridgeStating && !string.IsNullOrEmpty(nonFlowSuccessFlowNoAndInFlowChart))
                {
                    currentFlowNo = nonFlowSuccessFlowNoAndInFlowChart;
                }
                //读卡且已出战的衣架站点产品明细
                //站点上一个衣架
                //lucifer/2018年10月12日
                var statingHangerKey = string.Format("{0}:{1}", int.Parse(mainTrackNo), int.Parse(statingNo));
                DaoModel.HangerProductItemModel lastHangerInfo = null;
                var dicHangerProductItemListExt = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<SuspeSys.Domain.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM_EXT);
                //var statingHangerList = dicHangerProductItemListExt.Values.Where(f=>f.);
                if (dicHangerProductItemListExt.ContainsKey(statingHangerKey))
                {
                    var oList = dicHangerProductItemListExt[statingHangerKey].Where(f => f.OutSiteDate != null);
                    if (oList.Count() > 0)
                        lastHangerInfo = oList.OrderByDescending(f => f.OutSiteDate).First();
                }
                var siteGroupNo = StatingServiceImpl.Instance.GetGroupNo(int.Parse(mainTrackNo));
                DaoModel.HangerProductItemModel currentHangerProductItem = null;

                //var dicHangerProductItemCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM);
                //if (dicHangerProductItemCache.ContainsKey(hangerNo))
                //{
                //    var hangerProductItemList = dicHangerProductItemCache[hangerNo];
                //    foreach (var hpi in hangerProductItemList)
                //    {
                //        if (hpi.SiteNo.Equals(statingNo) && hpi.MainTrackNumber.Value == short.Parse(mainTrackNo))// && hpi.CompareDate == null)
                //        {
                //            currentHangerProductItem = hpi;
                //            hpi.CompareDate = DateTime.Now;
                //            //hpi.FlowChartd=
                //            dicHangerProductItemCache[hangerNo] = hangerProductItemList;
                //            break;
                //        }
                //    }
                //    if (null == currentHangerProductItem)
                //    {
                //        currentHangerProductItem = hangerProductItemList.OrderByDescending(f => f.OutSiteDate).First();
                //    }
                //}

                ////获取站内最近出战的衣架信息
                //IDictionary<string, string> dicOrder = new Dictionary<string, string>();
                //dicOrder.Add("OutSiteDate", "DESC");
                //var sql = new StringBuilder("select * from StatingHangerProductItem where SiteNo=?");
                //var statingHangerProductItem = QueryForObject<DaoModel.StatingHangerProductItem>(sql, dicOrder, false, int.Parse(statingNo));

                ////获取当前衣架信息
                //var currentHangerSQL = new StringBuilder("select * from WaitProcessOrderHanger where HangerNo=?");
                //var currentFlow = QueryForObject<DaoModel.WaitProcessOrderHanger>(currentHangerSQL, null, false, hangerNo);

                ////修正衣架生产记录的比较时间
                //var hangerProductItemSQL = new StringBuilder("select * from HangerProductItem where HangerNo=? and SiteNo=?");
                //var hangerProductItem = QueryForObject<DaoModel.HangerProductItem>(hangerProductItemSQL, null, false, hangerNo, int.Parse(statingNo));
                //hangerProductItem.CompareDate = DateTime.Now;
                //HangerProductItemDao.Instance.Update(hangerProductItem);



                //更新衣架生产工艺图制作站点的信息

                //var ppChart = GetHangerProductFlowChart(int.Parse(mainTrackNo), hangerNo, statingNo);
                //if (null == ppChart)
                //{
                //    var ex = new ApplicationException(string.Format("找不着衣架生产工艺图信息! 主轨:{0} 衣架号:{1} 站点:{2}", mainTrackNo, hangerNo, statingNo));
                //    errorLog.Error("【衣架工序比较】", ex);

                //}
                //ppChart.CompareDate = DateTime.Now;
                //HangerProductFlowChartDao.Instance.Update(ppChart);

                //【已分配未进站，出站】手动拿进站的衣架
                HangerProductFlowChartModel currentHangerProductFlowChart = null;


                if (NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo))//dicHangerProcessFlowChartCache.ContainsKey(hangerNo))
                {
                    var hpfcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo).Where(f => f.FlowNo.Equals(currentFlowNo));

                    foreach (var fItem in hpfcList)
                    {
                        if (fItem.MainTrackNumber.Value == short.Parse(mainTrackNo) && null != fItem.StatingNo && fItem.StatingNo.Value == short.Parse(statingNo)
                            && fItem.Status.Value != HangerProductFlowChartStaus.Successed.Value)
                        {
                            currentHangerProductFlowChart = fItem;
                            currentHangerProductItem = new Domain.HangerProductItemModel();
                            currentHangerProductItem.GroupNo = siteGroupNo;
                            currentHangerProductItem.HangerNo = hangerNo;
                            currentHangerProductItem.SiteNo = statingNo;
                            currentHangerProductItem.PSize = fItem.PSize;
                            currentHangerProductItem.PColor = fItem.PColor;
                            currentHangerProductItem.SizeNum = string.IsNullOrEmpty(fItem.Num) ? 0 : int.Parse(fItem.Num);
                            currentHangerProductItem.FlowNo = fItem.FlowNo;
                            currentHangerProductItem.ProcessFlowName = fItem.FlowName;
                            currentHangerProductItem.ProcessOrderNo = fItem.ProcessOrderNo;
                            currentHangerProductItem.ProcessFlowId = fItem.FlowId;
                            currentHangerProductItem.ProductsId = fItem.ProductsId;
                            currentHangerProductItem.MainTrackNumber = fItem.MainTrackNumber;
                            currentHangerProductItem.FlowIndex = (short)fItem.FlowIndex;
                            currentHangerProductItem.ProcessFlowCode = fItem.FlowCode;
                            currentHangerProductItem.LineName = fItem.LineName;
                            currentHangerProductItem.FlowChartd = fItem.ProcessChartId;
                            currentHangerProductItem.CompareDate = DateTime.Now;
                            currentHangerProductItem.IsMergeForward = (null != fItem.IsMergeForward) ? fItem.IsMergeForward.Value : false;
                            currentHangerProductItem.MergeProcessFlowChartFlowRelationId = fItem?.MergeProcessFlowChartFlowRelationId;
                            currentHangerProductItem.ProcessFlowChartFlowRelationId = fItem?.ProcessFlowChartFlowRelationId;
                            currentHangerProductItem.IsReturnWorkFlow = (null != fItem.FlowType && fItem.FlowType.Value == 2);
                            //currentHangerProductItem.s = fItem.StanardHours,
                            //StandardPrice = hpfc?.StandardPrice,
                            currentHangerProductItem.FlowChartd = fItem.ProcessChartId;
                            var currentHangerProductItemModel = Utils.Reflection.BeanUitls<DaoModel.HangerProductItemModel, DaoModel.HangerProductItem>.Mapper(currentHangerProductItem);
                            var hangerProductItemCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM);
                            if (!hangerProductItemCache.ContainsKey(hangerNo))
                            {
                                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM).Add(hangerNo, new List<Domain.HangerProductItemModel>() { currentHangerProductItemModel });
                            }

                            else
                            {
                                var hpList = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM)[hangerNo];

                                hpList.Add(currentHangerProductItemModel);
                                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM)[hangerNo] = hpList;
                            }
                            break;
                        }
                    }
                }


                if (currentHangerProductItem == null)
                {
                    var exx = new ApplicationException(string.Format("【衣架比较异常】 主轨:{0} 站点:{1} 衣架:{2}", mainTrackNo, statingNo, hangerNo));
                    tcpLogError.Error(exx);
                    return true;
                }
                var mergeProcessFlowNos = string.Empty;
                var mergeProcessFlowNames = string.Empty;


                // var dicHangerProcessFlowChart = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                if (NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo))//dicHangerProcessFlowChartCache.ContainsKey(hangerNo))
                {
                    var hpfChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo);//dicHangerProcessFlowChartCache[hangerNo];
                    foreach (var pf in hpfChartList.Where(f => f.FlowNo.Equals(currentFlowNo)))
                    {
                        if (pf.StatingNo.Value == short.Parse(statingNo) && pf.MainTrackNumber.Value == short.Parse(mainTrackNo) && (null == pf.IsFlowSucess || !pf.IsFlowSucess.Value))
                        {
                            pf.CompareDate = DateTime.Now;
                            currentHangerProductFlowChart = pf;
                        }
                    }
                    // dicHangerProcessFlowChartCache[hangerNo] = hpfChartList;
                    NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(hangerNo, hpfChartList);
                    var isExistMergeProcess = hpfChartList.Where(f => null != f.MergeProcessFlowChartFlowRelationId && f.MergeProcessFlowChartFlowRelationId.Equals(currentHangerProductItem.ProcessFlowChartFlowRelationId)).Count() > 0;
                    if (isExistMergeProcess)
                    {
                        mergeProcessFlowNos = string.Join(",", hpfChartList.Where(f => null != f.MergeProcessFlowChartFlowRelationId && f.MergeProcessFlowChartFlowRelationId.Equals(currentHangerProductItem.ProcessFlowChartFlowRelationId)).Select(k => k.FlowNo?.Trim()));
                        mergeProcessFlowNames = string.Join(",", hpfChartList.Where(f => null != f.MergeProcessFlowChartFlowRelationId && f.MergeProcessFlowChartFlowRelationId.Equals(currentHangerProductItem.ProcessFlowChartFlowRelationId)).Select(k => k.FlowName?.Trim()));
                    }
                }

                //var productCache = SusCache.Service.SusCacheProductService.Instance.GetOnLineProeuctCacheById(currentHangerProductItem.ProductsId);
                //string rPO = productCache.Po?.Trim();

                ////比较工序
                //DaoModel.HangerProductItemModel lastHangerInfo = null;
                string lastCompareStr = string.Empty;
                //获取最后一次读卡的衣架
                //var currentStatingHangerList = new List<DaoModel.HangerProductItemModel>();
                //foreach (var key in dicHangerProductItemCache.Keys)
                //{
                //    var lst = dicHangerProductItemCache[key].Where(f => !f.HangerNo.Equals(hangerNo) && f.SiteNo == statingNo && f.MainTrackNumber.Value == short.Parse(mainTrackNo)).ToList<DaoModel.HangerProductItemModel>();
                //    currentStatingHangerList.AddRange(lst.ToArray());
                //}
                //if (currentStatingHangerList.Count > 0)
                //{
                //    var lastHangerList = currentStatingHangerList.OrderByDescending(f => f.CompareDate);
                //    if (lastHangerList.Count() > 0)
                //    {
                //        lastHangerInfo = lastHangerList.First();
                //        lastCompareStr = lastHangerInfo?.ProcessOrderNo?.Trim() + lastHangerInfo?.PColor?.Trim() + lastHangerInfo?.PSize?.Trim() + lastHangerInfo?.ProcessFlowId?.Trim();
                //    }
                //}
                //var shpKey = string.Format("{0}:{1}", int.Parse(mainTrackNo), int.Parse(statingNo));
                //var dicStatingHangerProductItemCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.StatingHangerProductItem>>(SusRedisConst.STATING_HANGER_PRODUCT_ITEM);
                //if (dicStatingHangerProductItemCache.ContainsKey(shpKey))
                //{
                //    var statingHangerProductItemList = dicStatingHangerProductItemCache[shpKey];
                //    if (statingHangerProductItemList.Count > 0)
                //    {
                //        lastHangerInfo = statingHangerProductItemList.OrderByDescending(f => f.OutSiteDate).First();
                //        lastCompareStr = lastHangerInfo?.ProcessOrderNo?.Trim() + lastHangerInfo?.PColor?.Trim() + lastHangerInfo?.PSize?.Trim() + lastHangerInfo?.ProcessFlowId?.Trim();
                //    }
                //}

                lastCompareStr = lastHangerInfo?.ProcessOrderNo?.Trim() + lastHangerInfo?.PColor?.Trim() + lastHangerInfo?.PSize?.Trim() + lastHangerInfo?.ProcessFlowId?.Trim();

                var currrentCompareStr = currentHangerProductItem?.ProcessOrderNo?.Trim() + currentHangerProductItem?.PColor?.Trim() + currentHangerProductItem?.PSize?.Trim() + currentHangerProductItem?.ProcessFlowId?.Trim();
                //var lastCompareStr = statingHangerProductItem?.ProcessOrderNo?.Trim() + statingHangerProductItem?.PColor?.Trim() + statingHangerProductItem?.PSize?.Trim() + statingHangerProductItem?.ProcessFlowId?.Trim();
                isReworkFlow = currentHangerProductFlowChart?.FlowType == 1;
                tag = currentHangerProductFlowChart?.FlowType == 1 ? 2 : (currrentCompareStr.Equals(lastCompareStr) ? 0 : 1);
                flowInfo = currentHangerProductItem?.ProcessOrderNo?.Trim() + "," + currentHangerProductItem?.PColor?.Trim() + "," + currentHangerProductItem?.PSize?.Trim() + "," + currentHangerProductItem?.FlowNo?.Trim() + "," + currentHangerProductItem?.ProcessFlowName?.Trim();
                tcpLogInfo.InfoFormat("【工序比较】当前工序信息-->{0}", currrentCompareStr);
                if (isReworkFlow)
                {
                    //info = string.Format("本衣架:{0},{1},{2},返工工序:{3};疵点:{4}", currentHangerProductFlowChart?.HangerNo?.Trim(), currentHangerProductFlowChart?.PColor?.Trim(), currentHangerProductFlowChart?.PSize?.Trim(), currentHangerProductFlowChart?.FlowCode?.Trim(), currentHangerProductFlowChart?.DefectCode?.Trim());
                    var pOrderNo = currentHangerProductFlowChart?.ProcessOrderNo?.Trim();
                    string rHangerNo = currentHangerProductFlowChart?.HangerNo?.Trim();
                    string rColor = currentHangerProductFlowChart?.PColor?.Trim();
                    string rSize = currentHangerProductFlowChart?.PSize?.Trim();
                    //string rFlowCode = currentHangerProductFlowChart?.FlowCode?.Trim();
                    string rFlowNo = currentHangerProductFlowChart?.FlowNo?.Trim();
                    string rFlowName = currentHangerProductFlowChart?.FlowName?.Trim();
                    string rDefectCode = currentHangerProductFlowChart?.DefectCode?.Trim();
                    string rDefectName = GetDefectInfo(currentHangerProductFlowChart.DefectCode?.Trim())?.DefectName?.Trim();
                    var rPieceNum = currentHangerProductFlowChart.Num?.Trim();
                    var productCache = SusCache.Service.SusCacheProductService.Instance.GetOnLineProeuctCacheById(currentHangerProductItem.ProductsId);
                    string rPO = productCache.Po?.Trim();
                    /*
                     制单，PO、颜色、尺码、件数
                        返工工序：2，上领；...等
                        返工原因：2，破洞；...等

                    1001, QR001，白色，S，1件
                    返工工序
                    返工原因
                     */

                    //info = $"本衣架:{rHangerNo},{rColor},{rSize},PO:{rPO},返工工序:{rFlowCode};疵点:{rDefectCode}";
                    info = $"{pOrderNo},{rPO},{rColor},{rSize},{rPieceNum}件,返工工序:{rFlowNo},{rFlowName};返工原因:{rDefectCode},{rDefectName}";
                    // flowInfo = info;
                    return false;
                }
                //断电重启发送不比较衣架内容
                if (isPowerSupply)
                {
                    //info = string.Format("本衣架:{0},{1},{2},分配工序:{3}", currentHangerProductItem?.ProcessOrderNo?.Trim(), currentHangerProductItem?.PColor?.Trim(), currentHangerProductItem?.PSize?.Trim(), currentHangerProductItem?.ProcessFlowCode?.Trim());
                    // info = $"本衣架:{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},PO:{rPO},分配工序:{currentHangerProductItem?.ProcessFlowCode?.Trim()}"; ;
                    if (string.IsNullOrEmpty(mergeProcessFlowNos))
                    {
                        info = $"{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},{currentHangerProductItem?.SizeNum},本站工序:{currentHangerProductItem?.FlowNo?.Trim()},{currentHangerProductItem?.ProcessFlowName?.Trim()}";
                    }
                    else
                    {
                        info = $"{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},{currentHangerProductItem?.SizeNum},本站工序:{currentHangerProductItem?.FlowNo?.Trim()},{mergeProcessFlowNos};{currentHangerProductItem?.ProcessFlowName?.Trim()},{mergeProcessFlowNames}";
                    }

                    if (null == lastHangerInfo)
                    {
                        // flowInfo = info;
                        //info + string.Format("");
                        return false;
                    }
                    //flowInfo = info;
                    ////info += string.Format("前一衣架:{0},{1},{2},分配工序:{3}", lastHangerInfo?.ProcessOrderNo?.Trim(), lastHangerInfo?.PColor?.Trim(), lastHangerInfo?.PSize?.Trim(), lastHangerInfo?.ProcessFlowCode?.Trim());
                    //info += $"前一衣架:{lastHangerInfo?.ProcessOrderNo?.Trim()},{lastHangerInfo?.PColor?.Trim()},{lastHangerInfo?.PSize?.Trim()},PO:{rPO},分配工序:{lastHangerInfo?.ProcessFlowCode?.Trim()}"; ;
                    return false;
                }
                if (!currrentCompareStr.Equals(lastCompareStr))
                {
                    //info = string.Format("本衣架:{0},{1},{2},分配工序:{3}", currentHangerProductItem?.ProcessOrderNo?.Trim(), currentHangerProductItem?.PColor?.Trim(), currentHangerProductItem?.PSize?.Trim(), currentHangerProductItem?.ProcessFlowCode?.Trim());
                    // info = $"本衣架:{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},PO:{rPO},分配工序:{currentHangerProductItem?.ProcessFlowCode?.Trim()}";
                    if (string.IsNullOrEmpty(mergeProcessFlowNos))
                    {
                        info = $"{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},{currentHangerProductItem?.SizeNum},本站工序:{currentHangerProductItem?.FlowNo?.Trim()},{currentHangerProductItem?.ProcessFlowName?.Trim()}";
                    }
                    else
                    {
                        info = $"{currentHangerProductItem?.ProcessOrderNo?.Trim()},{currentHangerProductItem?.PColor?.Trim()},{currentHangerProductItem?.PSize?.Trim()},{currentHangerProductItem?.SizeNum},本站工序:{currentHangerProductItem?.FlowNo?.Trim()},{mergeProcessFlowNos};{currentHangerProductItem?.ProcessFlowName?.Trim()},{mergeProcessFlowNames}";
                    }
                    if (null == lastHangerInfo)
                    {
                        //info + string.Format("");
                        //flowInfo = info;
                        return false;
                    }
                    ////info += string.Format("前一衣架:{0},{1},{2},分配工序:{3}", lastHangerInfo?.ProcessOrderNo?.Trim(), lastHangerInfo?.PColor?.Trim(), lastHangerInfo?.PSize?.Trim(), lastHangerInfo?.ProcessFlowCode?.Trim());
                    //info += $"前一衣架:{lastHangerInfo?.ProcessOrderNo?.Trim()},{lastHangerInfo?.PColor?.Trim()},{lastHangerInfo?.PSize?.Trim()},PO:{rPO},分配工序:{lastHangerInfo?.ProcessFlowCode?.Trim()}";
                    //flowInfo = info;
                    return false;
                }

                return true;
            }
        }

        private string NonFlowSuccessFlowNoAndInFlowChart(string mainTrackNo, string statingNo, string hangerNo, List<HangerProductFlowChartModel> list)
        {
            var flowNoList = list.Where(f => f.MainTrackNumber.Value == int.Parse(mainTrackNo) && f.StatingNo != null && f.StatingNo.Value == int.Parse(statingNo) && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
            && !string.IsNullOrEmpty(f.FlowNo)).Select(f => f.FlowNo).Distinct();
            var flowNo = flowNoList.Count() > 0 ? flowNoList.First() : null;
            return flowNo;
        }

        internal IList<int> GetAllMainTracknumber()
        {
            IList<int> list = new List<int>();
            string sql = string.Format("select Distinct MainTrackNumber from Stating where Deleted=0 and MainTrackNumber is not null");
            list = DapperHelp.QueryForList<int>(sql, null);
            return list;
        }

        /// <summary>
        /// 解析返工工序及疵点代码关系
        /// </summary>
        /// <param name="reqMess"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="hangerNo"></param>
        /// <param name="tag">
        /// 00＝允许出站；
        /// 01＝不允许出站，站点不存在
        /// 02＝不允许出站，疵点错误，请重新输入疵点
        /// 03＝不允许出站，工序代码错误或者未找到生产的工序代码
        /// 04＝不允许出站，站点满或停止工作
        /// 05＝不允许出站，不允许返工操作
        /// 06=不允许出站，站点员工未登录
        /// 07=不允许出站，（同一站点同一时间内重复返工）同一衣架号已出站，拿回来再返工的情况，针对返工还在进行中，取下衣架又拿到返工发起站点做返工。
        /// </param>
        public void FlowReworkAction(Dictionary<FlowDefectCodeModel, List<FlowDefectCodeItem>> reqMess, ref int currentMainTrackNumber, ref int currentStatingNo, ref string nextStatingNo, ref int outMainTrackNumber, ref int hangerNo, ref int tag, ref bool isRequireBridge, ref string info)
        {
            foreach (var k in reqMess.Keys)
            {
                var kStr = k.ToString();
                var beginAddress = new FlowDefectCodeItem(HexHelper.TenToHexString4Len(SuspeConstants.address_ReturnBegin));
                var endAddress = new FlowDefectCodeItem(HexHelper.TenToHexString4Len(SuspeConstants.address_ReturnEnd));

                if (reqMess[k].Contains(endAddress) && reqMess[k].Contains(beginAddress))
                {
                    //是否需要开单独线程处理？
                    // outMainTrackNumber = k.MainTrackNumber;
                    currentMainTrackNumber = k.MainTrackNumber;
                    currentStatingNo = k.StatingNo;
                    SusReworkService.Instance.ReworkHander(k.MainTrackNumber, k.StatingNo, reqMess[k], ref nextStatingNo, ref hangerNo, ref tag, ref outMainTrackNumber, ref isRequireBridge, ref info);
                    reqMess.Remove(k);


                    break;
                    //更新返工工序未未完成，zxl,2018年4月11日 23:42:00

                }
                else
                {
                    //没有末尾地址忽略或者开始地址都没忽略
                    continue;
                }
            }
        }

        bool IsHangerRepeatInStating(int mainTrackNumber, string tenStatingNo, string tenHangerNo, IList<HangerProductFlowChartModel> fcList)
        {
            var dicHangerStatingInfo = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION);
            if (!dicHangerStatingInfo.ContainsKey(tenHangerNo))
            {
                var ex = new ApplicationException($"【衣架进站异常】 衣架:{tenHangerNo} 主轨:{mainTrackNumber} 站点:{tenStatingNo}  衣架进站异常!");
                tcpLogInfo.Error(ex);
                throw ex;
            }
            var hangerStatingIncomeList = dicHangerStatingInfo[tenHangerNo];
            foreach (var hs in hangerStatingIncomeList)
            {
                if (hs.HangerStatus == 1 && hs.MainTrackNumber.Value == mainTrackNumber && hs.StatingNo.Value == int.Parse(tenStatingNo))
                {
                    return true;
                }
            }
            var hangerStatingNumOrOnlineIsAllocation = hangerStatingIncomeList.Where(hs => (hs.HangerStatus == 0 || hs.HangerStatus == 4) && hs.MainTrackNumber.Value == mainTrackNumber && hs.StatingNo.Value == int.Parse(tenStatingNo)).Count() > 0;
            if (!hangerStatingNumOrOnlineIsAllocation)
            {
                var flowIsSuccess = FlowService.Instance.FlowIsSuccess(int.Parse(tenHangerNo), fcList.ToList(), mainTrackNumber + "", tenStatingNo + "");
                if (flowIsSuccess)
                {
                    var ex = new ApplicationException($"【衣架进站异常】 衣架:{tenHangerNo} 主轨:{mainTrackNumber} 站点:{tenStatingNo}  工序已完成!");
                    tcpLogInfo.Error(ex);
                    throw ex;
                }
            }
            // var dicHangerStatingALlo = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            if (NewCacheService.Instance.HangerIsContainsAllocationItem(tenHangerNo))//dicHangerStatingALlo.ContainsKey(tenHangerNo))
            {
                var hangerStatingAllocationList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(tenHangerNo);//dicHangerStatingALlo[tenHangerNo];
                if (0 == hangerStatingAllocationList.Count()) return false;
                var lastAllocationItem = hangerStatingAllocationList.Where(f => f.NextSiteNo != null).OrderByDescending(f => f.AllocatingStatingDate).First();
                if (!lastAllocationItem.NextSiteNo.Equals(tenStatingNo))
                {
                    var ex = new ApplicationException($"【衣架进站异常】 衣架:{tenHangerNo} 主轨:{mainTrackNumber} 站点:{tenStatingNo}  站点未分配进站!");
                    tcpLogInfo.Error(ex);
                    throw ex;
                }
            }
            return false;
        }
        /// <summary>
        /// 衣架进站记录
        /// </summary>
        /// <param name="tenHangerNo"></param>
        public void RecordHangerArriveStating(int mainTrackNumber, string tenHangerNo, string tenStatingNo, ref bool isHangerRepeatInStating)
        {
            SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel current = null;
            // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            if (!NewCacheService.Instance.HangerCurrentFlowIsContains(tenHangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(tenHangerNo))
            {
                var info = string.Format("【错误】主轨:【{0}】站点:【{1}】衣架【{2} 】 衣架未注册!", mainTrackNumber, tenStatingNo, tenHangerNo);

                var ex = new ApplicationException(info);
                tcpLogError.Error(ex);
                throw ex;
            }
            current = NewCacheService.Instance.GetHangerCurrentFlow(tenHangerNo); //dicCurrentHangerProductingFlowModelCache[tenHangerNo];
            var isHangingPieceStating = CANProductsService.Instance.IsHangPieceStating(mainTrackNumber, int.Parse(tenStatingNo));
            if (isHangingPieceStating)
            {
                ////站点分配数-1
                //var hangingPieceStatingAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = int.Parse(tenStatingNo), AllocationNum = -1 };
                //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(hangingPieceStatingAllocationNumModel));
                OutSiteCommonHangerHandler.Instance.CorrecHangerReturnStatingStatingNumHandler(tenHangerNo, mainTrackNumber, tenStatingNo, -3);
                return;
            }
            //处理衣架重复进站的问题

            var hfcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo); //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[tenHangerNo];
            //foreach (var fc in hfcList)
            //{
            //    if (current.FlowIndex == fc.FlowIndex.Value && fc.MainTrackNumber == (short)mainTrackNumber && fc.StatingNo.Value == short.Parse(tenStatingNo) && fc.Status.Value == HangerProductFlowChartStaus.Producting.Value
            //        && ((null != fc.IsReworkSourceStating && !fc.IsReworkSourceStating.Value) || (null == fc.IsReworkSourceStating)))
            //    {
            //        isHangerRepeatInStating = true;
            //        break;
            //    }
            //}
            isHangerRepeatInStating = IsHangerRepeatInStating(mainTrackNumber, tenStatingNo, tenHangerNo, hfcList);
            if (isHangerRepeatInStating) return;
            var currentStatingIsBridge = IsBridge(mainTrackNumber, int.Parse(tenStatingNo));
            var isInFlowChart = hfcList.Where(f => f.MainTrackNumber == (short)mainTrackNumber && f.StatingNo.Value == short.Parse(tenStatingNo)).Count() > 0;
            var keyBridgeCache = string.Format("{0}:{1}:{2}", mainTrackNumber, tenStatingNo, tenHangerNo);
            var dicHangerBridgeOutSiteCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, HangerProductFlowChartModel>(SusRedisConst.BRIDGE_STATING_HANGER_IN_COME_ITEM);
            if (currentStatingIsBridge && !isHangerRepeatInStating && !isInFlowChart)
            {
                //if (dicHangerBridgeOutSiteCache.ContainsKey(keyBridgeCache))
                //{
                //    var info = string.Format("重复进站! 主轨:{0} 站点:{1} 衣架号:{2}", mainTrackNumber, tenStatingNo, tenHangerNo);
                //    tcpLogInfo.Info(info);
                //    isHangerRepeatInStating = true;
                //    return;
                //}
                if (!dicHangerBridgeOutSiteCache.ContainsKey(keyBridgeCache))
                {
                    var bcModel = new BridgeCacheModel();
                    bcModel.HangerNo = tenHangerNo;
                    bcModel.MainTrackNumber = (short)mainTrackNumber;
                    bcModel.StatingNo = short.Parse(tenStatingNo);
                    SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, BridgeCacheModel>(SusRedisConst.BRIDGE_STATING_HANGER_IN_COME_ITEM).Add(keyBridgeCache, bcModel);
                }
            }

            //更新缓存站点工序衣架进站关系
            HangerProductFlowChartModel hpfc = null;
            foreach (var fc in hfcList)
            {
                if (current.FlowIndex == fc.FlowIndex.Value && fc.MainTrackNumber == (short)mainTrackNumber && fc.StatingNo.Value == short.Parse(tenStatingNo) && fc.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value)
                {
                    fc.IncomeSiteDate = DateTime.Now;
                    fc.Status = HangerProductFlowChartStaus.Producting.Value;//生产中
                    hpfc = fc;
                    break;
                }
            }
            //处理桥接携带工序出战后又进该站的情况
            if (null == hpfc)
            {
                foreach (var fc in hfcList)
                {
                    //桥接站携带工序，且桥接站下一站的桥接站也携带工序，当前衣架的工序定位在下一站
                    if ((current.FlowIndex == fc.FlowIndex.Value || currentStatingIsBridge) && fc.MainTrackNumber == (short)mainTrackNumber && fc.StatingNo.Value == short.Parse(tenStatingNo) && fc.Status.Value != HangerProductFlowChartStaus.Successed.Value)
                    {
                        fc.IncomeSiteDate = DateTime.Now;
                        fc.Status = HangerProductFlowChartStaus.Producting.Value;//生产中
                        hpfc = fc;
                        break;
                    }
                }
            }
            //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[tenHangerNo] = hfcList;
            NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(tenHangerNo, hfcList);
            HangerProductFlowChartModel nonSucessedFlow = null;
            var flowSuccess = false;
            if (currentStatingIsBridge)
            {
                flowSuccess = CANProductsService.Instance.IsBridgeInverseBridgeAndFlowSuccess(mainTrackNumber + "", tenStatingNo + "", hfcList, ref nonSucessedFlow);
            }
            //桥接站携带工序走2遍进站，履历只更新一次
            HangerProductsChartResumeModel hpResume = null;
            if (null != hpfc)
            {
                //【衣架生产履历】本站衣架生产履历Cache写入
                //if (null != hpfc)
                //{
                hpResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = tenHangerNo,
                    StatingNo = tenStatingNo,
                    MainTrackNumber = mainTrackNumber,
                    HangerProductFlowChart = hpfc,
                    Action = 2
                };
                var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hangerResumeJson);
            }
            //桥接不携带工序进站
            if (currentStatingIsBridge && !isInFlowChart)
            {
                //桥接不携带工序进站
                hpResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = tenHangerNo,
                    StatingNo = tenStatingNo,
                    MainTrackNumber = mainTrackNumber,
                    HangerProductFlowChart = new HangerProductFlowChartModel() { MainTrackNumber = (short)mainTrackNumber, StatingNo = short.Parse(tenStatingNo) },
                    Action = 8//桥接不携带工序进站
                };
                var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
                //   NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hangerResumeJson);

                //桥接不携带工序进站
                var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssoc.Action = 11;//桥接不携带工序进站
                hnssoc.HangerNo = tenHangerNo;
                hnssoc.MainTrackNumber = mainTrackNumber;
                hnssoc.StatingNo = int.Parse(tenStatingNo);
                var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
            }
            else if (currentStatingIsBridge && isInFlowChart && flowSuccess)
            {//桥接站且在携带工序，且工序已完成又进站

                hpResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = tenHangerNo,
                    StatingNo = tenStatingNo,
                    MainTrackNumber = mainTrackNumber,
                    HangerProductFlowChart = new HangerProductFlowChartModel() { MainTrackNumber = (short)mainTrackNumber, StatingNo = short.Parse(tenStatingNo) },
                    Action = 8//桥接站且在携带工序，且工序已完成又进站
                };
                var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hangerResumeJson);
                //桥接站且在携带工序，且工序已完成又进站
                var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssoc.Action = 11;//桥接站且在携带工序，且工序已完成又进站
                hnssoc.HangerNo = tenHangerNo;
                hnssoc.MainTrackNumber = mainTrackNumber;
                hnssoc.StatingNo = int.Parse(tenStatingNo);
                var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
            }
            else if (currentStatingIsBridge && isInFlowChart && !flowSuccess)
            {
                //修正删除的站内数及明细、缓存
                var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssoc.Action = 1;
                hnssoc.HangerNo = tenHangerNo;
                hnssoc.MainTrackNumber = mainTrackNumber;
                hnssoc.StatingNo = int.Parse(tenStatingNo);
                hnssoc.FlowNo = nonSucessedFlow.FlowNo;
                hnssoc.FlowIndex = nonSucessedFlow.FlowIndex.Value;
                hnssoc.HangerProductFlowChartModel = hpResume?.HangerProductFlowChart;
                var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
            }
            else
            {
                ////站内数+1
                //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = int.Parse(statingNo), OnLineSum = 1 };
                //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));

                ////站点分配数-1
                //var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = mainTrackNumber, StatingNo = int.Parse(statingNo), AllocationNum = -1 };
                //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));

                //修正删除的站内数及明细、缓存
                var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssoc.Action = 1;
                hnssoc.HangerNo = tenHangerNo;
                hnssoc.MainTrackNumber = mainTrackNumber;
                hnssoc.StatingNo = int.Parse(tenStatingNo);
                hnssoc.FlowNo = current.FlowNo;
                hnssoc.FlowIndex = current.FlowIndex;
                hnssoc.HangerProductFlowChartModel = hpResume?.HangerProductFlowChart;
                var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                //   NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
            }
            // }
            //更新衣架绑定关系
            var wp = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DaoModel.WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER)[tenHangerNo];
            wp.IsIncomeSite = true;
            wp.FlowIndex++;

            NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DaoModel.WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER)[tenHangerNo] = wp;


            //currentFlow.ProcessOrderNo?.Trim() + currentFlow.PColor?.Trim() + currentFlow.PSize?.Trim() + currentFlow.ProcessFlowId?.Trim();
            //记录衣架进站信息
            if (null != hpfc)
            {
                var hangerProductItemCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM);
                if (!hangerProductItemCache.ContainsKey(tenHangerNo))
                {
                    var hItem = new DaoModel.HangerProductItemModel();
                    hItem.ProductsId = hpfc?.ProductsId;
                    hItem.ProcessOrderNo = wp?.ProcessOrderNo;
                    hItem.PColor = hpfc?.PColor;
                    hItem.PSize = hpfc?.PSize;
                    hItem.ProcessOrderId = wp?.ProcessOrderId;
                    hItem.FlowNo = hpfc?.FlowNo;
                    hItem.ProcessFlowCode = hpfc?.FlowCode;
                    hItem.ProcessFlowName = hpfc?.FlowName;
                    hItem.ProcessFlowId = hpfc?.FlowId;
                    hItem.IncomeSiteDate = DateTime.Now;
                    hItem.SiteNo = tenStatingNo;
                    hItem.HangerNo = tenHangerNo;
                    hItem.MainTrackNumber = (short)mainTrackNumber;
                    hItem.FlowIndex = (short)hpfc?.FlowIndex.Value;
                    hItem.StanardHours = hpfc?.StanardHours;
                    hItem.StandardPrice = hpfc?.StandardPrice;
                    hItem.FlowChartd = hpfc?.ProcessChartId;
                    hItem.LineName = wp?.LineName;
                    hItem.SizeNum = string.IsNullOrEmpty(hpfc?.Num) ? 0 : int.Parse(hpfc.Num);
                    hItem.IsMergeForward = (null != hpfc.IsMergeForward) ? hpfc.IsMergeForward.Value : false;
                    hItem.MergeProcessFlowChartFlowRelationId = hpfc?.MergeProcessFlowChartFlowRelationId;
                    hItem.ProcessFlowChartFlowRelationId = hpfc?.ProcessFlowChartFlowRelationId;
                    hItem.IsReturnWorkFlow = (null != hpfc?.FlowType && hpfc.FlowType.Value == 2);
                    NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM).Add(tenHangerNo, new List<Domain.HangerProductItemModel>() { hItem });
                }
                else
                {
                    var hpList = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM)[tenHangerNo];
                    var hItem = new DaoModel.HangerProductItemModel();

                    hItem.ProductsId = hpfc?.ProductsId;
                    hItem.ProcessOrderNo = wp?.ProcessOrderNo;
                    hItem.PColor = hpfc?.PColor;
                    hItem.PSize = hpfc?.PSize;
                    hItem.ProcessOrderId = wp?.ProcessOrderId;
                    hItem.FlowNo = hpfc?.FlowNo;
                    hItem.ProcessFlowCode = hpfc?.FlowCode;
                    hItem.ProcessFlowName = hpfc?.FlowName;
                    hItem.ProcessFlowId = hpfc?.FlowId;
                    hItem.IncomeSiteDate = DateTime.Now;
                    hItem.SiteNo = tenStatingNo;
                    hItem.HangerNo = tenHangerNo;
                    hItem.MainTrackNumber = (short)mainTrackNumber;
                    hItem.FlowIndex = (short)hpfc?.FlowIndex.Value;
                    hItem.StanardHours = hpfc?.StanardHours;
                    hItem.StandardPrice = hpfc?.StandardPrice;
                    hItem.FlowChartd = hpfc?.ProcessChartId;
                    hItem.LineName = wp?.LineName;
                    hItem.SizeNum = string.IsNullOrEmpty(hpfc?.Num) ? 0 : int.Parse(hpfc.Num);
                    hItem.IsMergeForward = (null != hpfc?.IsMergeForward) ? hpfc.IsMergeForward.Value : false;
                    hItem.MergeProcessFlowChartFlowRelationId = hpfc?.MergeProcessFlowChartFlowRelationId;
                    hItem.ProcessFlowChartFlowRelationId = hpfc?.ProcessFlowChartFlowRelationId;
                    hItem.IsReturnWorkFlow = (null != hpfc?.FlowType && hpfc.FlowType.Value == 2);

                    hpList.Add(hItem);
                    NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM)[tenHangerNo] = hpList;
                }
            }

            //发布进站处理 消息
            var reslut = new HangerInSiteResult();
            reslut.StatingNo = tenStatingNo;
            reslut.HangerNo = tenHangerNo;
            reslut.MainTrackNumber = mainTrackNumber;
            reslut.WaitProcessOrderHanger = wp;
            var reslutJosn = Newtonsoft.Json.JsonConvert.SerializeObject(reslut);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_IN_SITE_ACTION, reslutJosn);

            //  var dicHangerStatingAllocationItem = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            if (NewCacheService.Instance.HangerIsContainsAllocationItem(tenHangerNo))//dicHangerStatingAllocationItem.ContainsKey(tenHangerNo))
            {
                var hsAllocationItemList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(tenHangerNo);//dicHangerStatingAllocationItem[tenHangerNo];
                hsAllocationItemList.Where(f => (!string.IsNullOrEmpty(f.NextSiteNo) && int.Parse(f.NextSiteNo) == int.Parse(tenStatingNo)) && f.MainTrackNumber.Value == mainTrackNumber).ToList<DaoModel.HangerStatingAllocationItem>().ForEach(k => k.IncomeSiteDate = DateTime.Now);
                //dicHangerStatingAllocationItem[tenHangerNo] = hsAllocationItemList;
                NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(tenHangerNo, hsAllocationItemList);
            }
            if (NewCacheService.Instance.F2AssignIsContains(int.Parse(tenHangerNo)))
            {
                var currentF2Assign = NewCacheService.Instance.GetCurrentF2AssignInfo(int.Parse(tenHangerNo));
                if (mainTrackNumber == currentF2Assign.TargertMainTrackNumber && int.Parse(tenStatingNo) == currentF2Assign.TargertStatingNo)
                {
                    currentF2Assign.F2AssignTag = -1;
                    NewCacheService.Instance.UpdateCurrentF2AssignInfo(int.Parse(tenHangerNo), currentF2Assign);
                    var info = string.Format("【F2指定】主轨:【{0}】站点:【{1}】衣架【{2} 】 衣架进站!", mainTrackNumber, tenStatingNo, tenHangerNo);
                    tcpLogInfo.Info(info);
                }
            }
        }
        /// <summary>
        /// 获取下一道工序制作站点详细信息
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="hangerNo"></param>
        /// <param name="productId"></param>
        /// <param name="sourceStatingNo"></param>
        /// <returns></returns>
        public DaoModel.HangerProductFlowChart GetHangerProductFlowChart(int mainTrackNumber, string hangerNo, string sourceStatingNo)
        {
            var dicOrders = new Dictionary<string, string>();
            dicOrders.Add("FlowIndex", "ASC");

            var dicCondi = new Dictionary<string, string>();
            dicCondi.Add("MainTrackNumber", mainTrackNumber.ToString());
            dicCondi.Add("HangerNo", hangerNo);
            // dicCondi.Add("ProductsId", productId);
            dicCondi.Add("StatingNo", sourceStatingNo);
            var sql = string.Format(@"select * from HangerProductFlowChart where MainTrackNumber=:MainTrackNumber and HangerNo=:HangerNo  
            AND StatingNo=:StatingNo and IsFlowSucess=0 AND IsHangerSucess=0");
            var p = QueryForObject<DaoModel.HangerProductFlowChart>(new StringBuilder(sql), dicOrders, false, dicCondi);
            return p;
        }
        /// <summary>
        /// 获取生产完成的衣架工序站点信息
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="hangerNo"></param>
        /// <param name="productId"></param>
        /// <param name="sourceStatingNo"></param>
        /// <returns></returns>
        public DaoModel.HangerProductFlowChart GetCompleteHangerProductFlowChart(int mainTrackNumber, string hangerNo, string sourceStatingNo)
        {
            var dicOrders = new Dictionary<string, string>();
            dicOrders.Add("FlowIndex", "ASC");

            var dicCondi = new Dictionary<string, string>();
            dicCondi.Add("MainTrackNumber", mainTrackNumber.ToString());
            dicCondi.Add("HangerNo", hangerNo);
            // dicCondi.Add("ProductsId", productId);
            dicCondi.Add("StatingNo", sourceStatingNo);
            var sql = string.Format(@"select * from HangerProductFlowChart where MainTrackNumber=:MainTrackNumber and HangerNo=:HangerNo  
            AND StatingNo=:StatingNo and IsFlowSucess=1");
            var p = QueryForObject<DaoModel.HangerProductFlowChart>(new StringBuilder(sql), dicOrders, false, dicCondi);
            return p;
        }


        /// <summary>
        /// 制品界面上线 获取上线产品信息
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="productNumber"></param>
        /// <returns></returns>



        /// <summary>
        /// 下一站衣架分配成功，上一站衣架出站
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo">分配下一站响应站点</param>
        /// <param name="hangerNo">衣架号</param>
        /// <param name="outSiteStatingNo">出站站点号</param>
        /// <param name="flowType">0:正常衣架；1:返工衣架</param>
        /// <param name="isMonitoringAllocation">是否是监测点分配</param>
        /// <param name="isStatingStorage">是否是存储站出战</param>
        public void HangerOutStatingResponse(string mainTrackNo, string statingNo, string hangerNo, ref int outSiteMainTrackNumber, ref string outSiteStatingNo, ref int flowType, ref bool isReworkSourceStating, ref bool isMonitoringAllocation, ref bool isStatingStorage, ref bool isBridgeStatingOutStatingAllocate, ref bool isF2Assign)
        {
            //lock (locObject)
            {
                ////1.检查是否时返工工序，若是返工工序，则直接向返工工序来源站点发送出站命令
                //var sqlReworkFlow = new StringBuilder("select * from HangerReworkRecord where SiteNo=? AND HangerNo=?");
                //var reworkFlow = QueryForObject<DaoModel.HangerReworkRecord>(sqlReworkFlow, null, false, statingNo, hangerNo);
                //if (null != reworkFlow)
                //{
                //    outSiteStatingNo = reworkFlow.ReturnWorkSiteNo?.Trim();
                //    return;
                //}
                //2.从衣架站点分配关系中查找出站衣架的站点(未完成的站点分配)
                //过滤掉挂片站

                //衣架回流
                var isHangingPiece = CANProductsService.Instance.IsHangPieceStating(int.Parse(mainTrackNo), int.Parse(statingNo));
                if (isHangingPiece)
                {
                    int tenOutSiteStatingNo = 0;
                    // int outSiteMainTrackNumber = 0;
                    CANProductsValidService.Instance.GetSucessHangerEndStating(hangerNo, int.Parse(mainTrackNo), int.Parse(statingNo), ref outSiteMainTrackNumber, ref tenOutSiteStatingNo);
                    outSiteStatingNo = tenOutSiteStatingNo.ToString();// HexHelper.TenToHexString2Len(tenOutSiteStatingNo);

                    //转移生产完成的数据
                    var reslut = new DaoModel.WaitProcessOrderHanger();
                    reslut.HangerNo = hangerNo;
                    var reslutJosn = Newtonsoft.Json.JsonConvert.SerializeObject(reslut);
                    NewSusRedisClient.subcriber.Publish(SusRedisConst.PRODUCT_SUCCESS_COPY_DATA_ACTION, reslutJosn);

                    var hangerOutSiteRecord = new CurrentHangerOutSiteModel();
                    hangerOutSiteRecord.OutSite = DateTime.Now;
                    hangerOutSiteRecord.MainTrackNumber = outSiteMainTrackNumber;
                    hangerOutSiteRecord.StatingNo = int.Parse(outSiteStatingNo);
                    hangerOutSiteRecord.FlowNo = string.Empty;
                    hangerOutSiteRecord.FlowIndex = -20;
                    hangerOutSiteRecord.HangerNo = hangerNo;
                    hangerOutSiteRecord.FlowType = -11;
                    NewCacheService.Instance.UpdateHangerOutSiteRecord(hangerNo, hangerOutSiteRecord);
                    return;
                }

                //var dicHangerStatingAllocationItem = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
                if (!NewCacheService.Instance.HangerIsContainsAllocationItem(hangerNo)) //dicHangerStatingAllocationItem.ContainsKey(hangerNo))
                {
                    var ex = new ApplicationException(string.Format("【衣架出站响应】找不到出站站点! 主轨:{0} 衣架号:{1} 下一道工序站点:{1}", mainTrackNo, hangerNo, statingNo));
                    tcpLogError.Error("【衣架出站响应】", ex);
                    return;
                }

                // var dicHangerProcessFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);


                //List<HangerProductFlowChartModel> hangerProcessFlowChartLis;
                //dicHangerProcessFlowChart.TryGetValue(hangerNo, out hangerProcessFlowChartLis);

                DaoModel.HangerStatingAllocationItem hsAllocationItem = null;
                var hAllItemList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo);// dicHangerStatingAllocationItem[hangerNo];
                hsAllocationItem = hAllItemList.Where(f => f.Status == (byte)HangerStatingAllocationItemStatus.Allocationed.Value &&
                statingNo.Equals(f.NextSiteNo)
            // && f.OutMainTrackNumber == short.Parse(mainTrackNo)
            ).OrderByDescending(f => f.AllocatingStatingDate).First();
                //var list = dicHangerStatingAllocationItem[hangerNo];
                //foreach (var hsItem in list)
                //{
                //    if (hsItem.Id.Equals(hsAllocationItem.Id))
                //    {
                //        hsItem.Status = 1;
                //        //hsItem.OutSiteDate = DateTime.Now;
                //        break;
                //    }
                //}
                //SusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = list;

                //foreach (var hsa in hangerStatingAllocationItemList)
                //{
                //    if (hsa.HangerNo.Equals(hangerNo) && hsa.MainTrackNumber.Value == short.Parse(mainTrackNo) && (null != hsa.NextSiteNo && hsa.NextSiteNo.Equals(statingNo)) && (string.IsNullOrEmpty(hsa.Memo) || !hsa.Memo.Equals("-1")))
                //    {
                //        hsAllocationItem = hsa;
                //        break;
                //    }
                //}
                if (null == hsAllocationItem)
                {
                    var ex = new ApplicationException(string.Format("【衣架出站响应】找不到出站站点! 主轨:{0} 衣架号:{1} 下一道工序站点:{1}", mainTrackNo, hangerNo, statingNo));
                    tcpLogError.Error("【衣架出站响应】", ex);
                    return;
                }
                outSiteStatingNo = hsAllocationItem?.SiteNo;
                outSiteMainTrackNumber = hsAllocationItem.OutMainTrackNumber;//衣架请求出战的主轨
                isStatingStorage = hsAllocationItem.IsStatingStorageOutStating;
                isMonitoringAllocation = hsAllocationItem.isMonitoringAllocation;
                isBridgeStatingOutStatingAllocate = hsAllocationItem.IsBridgeStatingOutStatingAllocate;
                isF2Assign = hsAllocationItem.isF2AssgnAllocation;
                if (null != hsAllocationItem.IsReturnWorkFlow)
                {
                    if (null != hsAllocationItem.IsReworkSourceStating && null != hsAllocationItem.IsReworkSourceStating && hsAllocationItem.IsReworkSourceStating.Value)
                    {
                        isReworkSourceStating = hsAllocationItem.IsReworkSourceStating.Value;
                    }
                    else
                    {
                        //发起返工源头站点不发送产量
                        flowType = hsAllocationItem.IsReturnWorkFlow.Value ? 1 : 0;
                    }
                }
                if (!string.IsNullOrEmpty(outSiteStatingNo))
                {
                    if (!isMonitoringAllocation)
                    {
                        var hangerOutSiteRecord = new CurrentHangerOutSiteModel();
                        hangerOutSiteRecord.OutSite = DateTime.Now;
                        hangerOutSiteRecord.MainTrackNumber = outSiteMainTrackNumber;
                        hangerOutSiteRecord.StatingNo = int.Parse(outSiteStatingNo);
                        hangerOutSiteRecord.FlowNo = hsAllocationItem.FlowNo;
                        hangerOutSiteRecord.FlowIndex = hsAllocationItem.FlowIndex.Value;
                        hangerOutSiteRecord.HangerNo = hangerNo;
                        hangerOutSiteRecord.FlowType = null == hsAllocationItem.HangerType ? -10 : hsAllocationItem.HangerType.Value;
                        NewCacheService.Instance.UpdateHangerOutSiteRecord(hangerNo, hangerOutSiteRecord);
                    }
                }
                var isHangingPieceStatingLast = new ProductsQueryServiceImpl().isHangingPiece(null, statingNo, mainTrackNo);
                if (!isHangingPieceStatingLast)
                {

                    ////下一站分配数+1
                    //var allocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = int.Parse(mainTrackNo), StatingNo = int.Parse(statingNo), AllocationNum = 1 };
                    //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(allocationNumModel));

                    //  var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                    if (!NewCacheService.Instance.HangerCurrentFlowIsContains(hangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(hangerNo.ToString()))
                    {
                        //衣架衣架生产完成!
                        tcpLogError.ErrorFormat("【衣架出站响应】衣架无生产记录!衣架:{0} 主轨:{1} 站点：{2}", hangerNo, mainTrackNo, statingNo);
                        return;
                    }
                }
            }

        }
        /// <summary>
        /// 桥接分配处理
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="bridgeStatingNo"></param>
        /// <param name="aMainTracknumberBridgeStatingIsInFlowChart">出战衣架的下一站是否经过同主轨的桥接站，且桥接站在工艺图上</param>
        public void BridgeAllocationRelation(int tenMaintracknumber, int tenStatingNo, int outMainTrackNumber, string nextStatingNo, int tenHangerNo, string bridgeStatingNo, bool aMainTracknumberBridgeStatingIsInFlowChart, bool isMonitoringAllocation = false)
        {
            // var dicHangerStatingAllocationItem = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            if (!NewCacheService.Instance.HangerIsContainsAllocationItem(tenHangerNo + ""))//dicHangerStatingAllocationItem.ContainsKey(tenHangerNo.ToString()))
            {
                var ex = new ApplicationException(string.Format("分配错误!找不到衣架分配关系缓存 主轨:{0} 衣架号:{1}", tenMaintracknumber, tenHangerNo));
                tcpLogError.Error("【衣架出站】", ex);
                throw ex;
            }
            var hangerStatingAllocationList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(tenHangerNo + "");//dicHangerStatingAllocationItem[tenHangerNo.ToString()];
            var allocation = new DaoModel.HangerStatingAllocationItem();
            allocation.MainTrackNumber = (short)tenMaintracknumber;
            allocation.Status = Convert.ToByte(HangerStatingAllocationItemStatus.Allocationed.Value);
            allocation.AllocatingStatingDate = DateTime.Now;
            allocation.NextSiteNo = bridgeStatingNo;
            allocation.SiteNo = tenStatingNo.ToString();
            allocation.OutMainTrackNumber = tenMaintracknumber;
            allocation.isMonitoringAllocation = isMonitoringAllocation;
            allocation.BatchNo = BridgeService.Instance.GetBatchNo(tenHangerNo + "");
            allocation.GroupNo = BridgeService.Instance.GetGroupNo(outMainTrackNumber, int.Parse(nextStatingNo));
            hangerStatingAllocationList.Add(allocation);
            // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo.ToString()] = hangerStatingAllocationList;
            NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(tenHangerNo + "", hangerStatingAllocationList);

            //记录衣架分配
            var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(allocation);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

            if (aMainTracknumberBridgeStatingIsInFlowChart)//处理衣架分配/站内明细
            {
                var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
                if (dicBridgeCache.ContainsKey(tenMaintracknumber))
                {
                    var aBridgeSet = dicBridgeCache[tenMaintracknumber];
                    var bBridgeSet = dicBridgeCache[aBridgeSet.BMainTrackNumber.Value];
                    // var dicHangerFlowChartCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                    var ppChart = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo + "").Where(f => f.MainTrackNumber.Value == bBridgeSet.AMainTrackNumber.Value && f.StatingNo.Value == bBridgeSet.ASiteNo.Value).First();
                    CorrectStatingNumAndCacheByOutSiteByBirdge(bBridgeSet.AMainTrackNumber.Value.ToString(), bBridgeSet.ASiteNo.Value.ToString(), tenHangerNo.ToString(), ppChart);

                }
            }
            //清除桥接站进站缓存
            SusCacheBootstarp.Instance.ClearBridgeStatingHangerInSiteItem(tenHangerNo.ToString());
        }

        /// <summary>
        /// 下一站是否经过桥接
        /// </summary>
        /// <param name="tenMaintracknumber"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="bridgeType"></param>
        /// <param name="bridge"></param>
        /// <returns></returns>
        public bool IsBridgeByOutSiteRequestAction(int tenMaintracknumber, int tenStatingNo, int outMainTrackNumber, string nextStatingNo, int tenHangerNo, ref string bridgeType, ref SuspeSys.Domain.BridgeSet bridge)
        {
            //主轨相同不需要桥接
            if (tenMaintracknumber == outMainTrackNumber) return false;
            if (string.IsNullOrEmpty(nextStatingNo)) return false;

            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(tenMaintracknumber) || !dicBridgeCache.ContainsKey(outMainTrackNumber))
            {
                var exNonFoundBridgeSet = new ApplicationException(string.Format("无桥接配置不能桥接!请检查桥接设置。衣架号:{0} 从主轨{1}的站点{2} --->{3}主轨", tenHangerNo, tenMaintracknumber, tenStatingNo, outMainTrackNumber));
                tcpLogError.Error(exNonFoundBridgeSet);
                throw exNonFoundBridgeSet;
            }
            var outStatingMaintrackNumberBridge = dicBridgeCache[tenMaintracknumber];
            var nextStatingMaintrackBridge = dicBridgeCache[outMainTrackNumber];
            //var dicHangerFlowChartCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(tenHangerNo.ToString()))
            {
                return false;
            }
            var outsiteStatingNotEqualBridgeStating = (outStatingMaintrackNumberBridge.AMainTrackNumber.Value == tenMaintracknumber) && outStatingMaintrackNumberBridge.ASiteNo.Value != tenStatingNo;
            //出战站点就是桥接站
            var outsiteStatingEqualBridgeStating = (outStatingMaintrackNumberBridge.AMainTrackNumber.Value == tenMaintracknumber) && outStatingMaintrackNumberBridge.ASiteNo.Value == tenStatingNo;
            var outsiteOfBridgeStating = (outStatingMaintrackNumberBridge.AMainTrackNumber.Value == tenMaintracknumber) && outStatingMaintrackNumberBridge.ASiteNo.Value == tenStatingNo;
            var nextSiteNotEqualBridgeStating = (outMainTrackNumber == outStatingMaintrackNumberBridge.BMainTrackNumber.Value) && outStatingMaintrackNumberBridge.BSiteNo.Value != int.Parse(nextStatingNo);

            //下一站就是桥接站
            var nextSiteEqualBridgeStating = (outMainTrackNumber == outStatingMaintrackNumberBridge.BMainTrackNumber.Value) && outStatingMaintrackNumberBridge.BSiteNo.Value == int.Parse(nextStatingNo);

            var hangerFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo.ToString());
            var nonExistA = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value && f.MainTrackNumber.Value == tenMaintracknumber).Count() == 0;
            var nonExistB = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.BSiteNo.Value && f.MainTrackNumber.Value == outMainTrackNumber).Count() == 0;
            //桥接站都不在工艺图上,且桥街站不是请求出战点，且下一站不是下一站主轨的桥接站点
            if (nonExistA && nonExistB && outsiteStatingNotEqualBridgeStating && nextSiteNotEqualBridgeStating)
            {
                bridgeType = BridgeType.Bridge_Stating_Non_Flow_Chart_ALL;
                bridge = outStatingMaintrackNumberBridge;
                return true;
            }
            if (nonExistA && !nonExistB && outsiteStatingNotEqualBridgeStating && nextSiteNotEqualBridgeStating)
            {
                bridgeType = BridgeType.Bridge_Stating_One_In_Flow_Chart;
                bridge = outStatingMaintrackNumberBridge;
                return true;
            }
            if (nonExistA && !nonExistB && outsiteStatingNotEqualBridgeStating && !nextSiteNotEqualBridgeStating)
            {
                bridgeType = BridgeType.Bridge_Stating_One_In_Flow_Chart;
                bridge = outStatingMaintrackNumberBridge;
                return true;
            }
            if (!nonExistA && nonExistB && outsiteStatingNotEqualBridgeStating && nextSiteNotEqualBridgeStating)
            {
                bridgeType = BridgeType.Bridge_Stating_One_In_Flow_Chart;
                bridge = outStatingMaintrackNumberBridge;
                return true;
            }
            if (!nonExistA && !nonExistB && outsiteStatingNotEqualBridgeStating && nextSiteNotEqualBridgeStating)
            {
                bridgeType = BridgeType.Bridge_Stating_IN_Flow_Chart_ALL;
                bridge = outStatingMaintrackNumberBridge;
                return true;
            }
            if (!nonExistA && !nonExistB && outsiteStatingEqualBridgeStating && nextSiteEqualBridgeStating)
            {
                bridgeType = BridgeType.Bridge_Stating_IN_Flow_Chart_ALL;
                bridge = outStatingMaintrackNumberBridge;
                return true;
            }
            //if (!nonExistA && !nonExistB && outsiteOfBridgeStating)
            //{
            //    bridgeType = BridgeType.Bridge_Stating_IN_Flow_Chart_ALL_Bridge_OutSite;
            //    bridge = outStatingMaintrackNumberBridge;
            //    return true;
            //}
            // var requestOutSiteStatingIsInFlowChart = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == tenStatingNo).Count() == 0;
            // var 
            return false;
        }

        /// <summary>
        /// 检查桥接站是否在工艺图上;是：false;否:true;
        /// </summary>
        /// <param name="tenMaintracknumber">比较主轨</param>
        /// <param name="tenStatingNo">比较站点</param>
        /// <param name="tenHangerNo">比较衣架</param>
        /// <returns></returns>
        internal bool IsNonInBridgeByCompareFlowAction(int tenMaintracknumber, int tenStatingNo, int tenHangerNo)
        {

            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (dicBridgeCache.ContainsKey(tenMaintracknumber))
            {
                var outStatingMaintrackNumberBridge = dicBridgeCache[tenMaintracknumber];
                // var dicHangerFlowChartCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                if (!NewCacheService.Instance.HangerIsContainsFlowChart(tenHangerNo.ToString()))//dicHangerFlowChartCache.ContainsKey(tenHangerNo.ToString()))
                {
                    return false;
                }
                //站点不是桥接站
                if (outStatingMaintrackNumberBridge.ASiteNo.Value != tenStatingNo)
                {
                    return false;
                }
                //var hangerFlowChartList = dicHangerFlowChartCache[tenHangerNo.ToString()];
                //var nonExistA = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value && f.MainTrackNumber.Value == tenMaintracknumber).Count() == 0;
                ////var nonExistB = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.BSiteNo.Value).Count() == 0;
                ////桥接站都不在工艺图上
                //if (nonExistA)
                //{
                //    return true;
                //}
                return true;
            }
            return false;
        }
        /// <summary>
        /// 桥接站不在工艺图上且请求出战点是桥接站;
        /// 是：true;否:false;
        /// </summary>
        /// <param name="tenMaintracknumber">比较主轨</param>
        /// <param name="tenStatingNo">比较站点</param>
        /// <param name="tenHangerNo">比较衣架</param>
        /// <returns></returns>
        public bool IsBridgeByOutSiteRequestAction(int tenMaintracknumber, int tenStatingNo, int tenHangerNo)
        {

            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (dicBridgeCache.ContainsKey(tenMaintracknumber))
            {
                var outStatingMaintrackNumberBridge = dicBridgeCache[tenMaintracknumber];
                //    var dicHangerFlowChartCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                if (!NewCacheService.Instance.HangerIsContainsFlowChart(tenHangerNo + ""))//dicHangerFlowChartCache.ContainsKey(tenHangerNo.ToString()))
                {
                    return false;
                }
                //站点不是桥接站
                if (outStatingMaintrackNumberBridge.ASiteNo.Value != tenStatingNo)
                {
                    return false;
                }
                var hangerFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo + "");//dicHangerFlowChartCache[tenHangerNo.ToString()];
                var nonExistA = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value && f.MainTrackNumber.Value == tenMaintracknumber).Count() == 0;
                var currentStatingNoEqualBridgeStating = outStatingMaintrackNumberBridge.ASiteNo.Value == tenStatingNo;
                //var nonExistB = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.BSiteNo.Value).Count() == 0;
                //桥接站都不在工艺图上
                if (nonExistA && currentStatingNoEqualBridgeStating)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 在桥接站的对面主轨站点 发出的出战指令 确定反向的桥接站是否在工艺图上，是：false;否:true;
        /// </summary>
        /// <param name="outSiteMaintrackNumber"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="bridgeSet"></param>
        /// <returns></returns>
        internal bool IsNonInBridgeByOutsiteResponseAction(int outSiteMaintrackNumber, string outSiteStatingNo, int tenHangerNo, int tenMaintrackNumber, int tenStatingNo,
            ref SuspeSys.Domain.BridgeSet bridgeSet)
        {
            if (string.IsNullOrEmpty(outSiteStatingNo)) return false;
            //var mainTrackNumber = int.Parse(tenMainTrackNo);
            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(outSiteMaintrackNumber))
            {
                return false;
            }

            var outStatingMaintrackNumberBridge = dicBridgeCache[outSiteMaintrackNumber];
            if (!dicBridgeCache.ContainsKey(outStatingMaintrackNumberBridge.BMainTrackNumber.Value))
            {
                return false;
            }
            //if (outSiteMaintrackNumber != tenMaintrackNumber) return false;
            if (outSiteMaintrackNumber != outStatingMaintrackNumberBridge.AMainTrackNumber.Value && int.Parse(outSiteStatingNo) != outStatingMaintrackNumberBridge.ASiteNo.Value)
                return false;
            // var dicHangerFlowChartCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(tenHangerNo + ""))//dicHangerFlowChartCache.ContainsKey(tenHangerNo.ToString()))
            {
                return false;
            }
            bridgeSet = dicBridgeCache[outStatingMaintrackNumberBridge.BMainTrackNumber.Value];
            var bridSet = dicBridgeCache[outStatingMaintrackNumberBridge.BMainTrackNumber.Value];
            var hangerFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo + "");// dicHangerFlowChartCache[tenHangerNo.ToString()];

            var isInFlowChart = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == bridSet.ASiteNo.Value && f.MainTrackNumber.Value == bridSet.AMainTrackNumber.Value).Count() > 0;
            if (isInFlowChart)
            {
                //修正删除的站内数及明细、缓存
                var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssocDeleteStating.Action = 10;
                hnssocDeleteStating.HangerNo = tenHangerNo.ToString();
                hnssocDeleteStating.MainTrackNumber = bridSet.AMainTrackNumber.Value;
                hnssocDeleteStating.StatingNo = bridSet.ASiteNo.Value;
                //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
                var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
                //   NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocDeleteStatingJson);
            }
            //var dicHangerFlowChartCache = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            //if (!dicHangerFlowChartCache.ContainsKey(tenHangerNo.ToString()))
            //{
            //    return false;
            //}
            //var dicBridgeCache = SusRedis.SusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            //if (dicBridgeCache.ContainsKey(outSiteMaintrackNumber))
            //{
            //    var outStatingMaintrackNumberBridge = dicBridgeCache[outSiteMaintrackNumber];
            //    if (!dicBridgeCache.ContainsKey(outStatingMaintrackNumberBridge.BMainTrackNumber.Value)) return false;
            //    var dicReverseBridge = dicBridgeCache[outStatingMaintrackNumberBridge.BMainTrackNumber.Value];

            //    var hangerFlowChartList = dicHangerFlowChartCache[tenHangerNo.ToString()];
            //    var nonExistA = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == dicReverseBridge.ASiteNo.Value && f.MainTrackNumber.Value == dicReverseBridge.AMainTrackNumber.Value).Count() == 0;
            //    //var nonExistB = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == dicReverseBridge.BSiteNo.Value).Count() == 0;
            //    //桥接站都不在工艺图上
            //    if (nonExistA)
            //    {
            //        bridgeSet = dicReverseBridge;
            //        return true;
            //    }
            //}
            return true;
        }
        ///衣架出站【硬件请求pc】
        private ProductsQueryServiceImpl productsQueryService = new ProductsQueryServiceImpl();

        //private object GetCurrentProductingIndex(List<HangerProductFlowChartModel> list, string mainTrackNo, string statingNo)
        //{
        //    throw new NotImplementedException();
        //}
        /// <summary>
        /// 获取当前生产到第几道工序
        /// </summary>
        /// <param name="hangerProuctList"></param>
        /// <returns></returns>
        public int GetCurrentProductingIndex(int hangerNo, List<HangerProductFlowChartModel> hangerProuctList, string mainTrackNo, string statingNo)
        {
            //var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            if (!NewCacheService.Instance.HangerCurrentFlowIsContains(hangerNo + ""))//dicCurrentHangerProductingFlowModelCache.ContainsKey(hangerNo.ToString()))
            {
                //衣架衣架生产完成!
                tcpLogError.ErrorFormat("衣架无生产记录!衣架:{0} 主轨:{1} 站点：{2}", hangerNo, mainTrackNo, statingNo);
                return -2;
            }
            var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");//dicCurrentHangerProductingFlowModelCache[hangerNo.ToString()];

            //当前站点衣架未生产的工序
            var nonProductsStatingFlow = hangerProuctList.Where(f => f.StatingNo != null && f.StatingNo.Value != -1
        && f.StatingNo.Value == current.StatingNo
        && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        && ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        || ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0))));
            if (nonProductsStatingFlow.Count() == 0)
            {
                //衣架生产完成
                if (current.FlowIndex == 1)
                {
                    return current.FlowIndex;
                }

                var ex = new FlowDeleteOrStatingDeletedException();
                ex.MainTrackNumber = int.Parse(mainTrackNo);
                ex.StatingNo = int.Parse(statingNo);
                ex.NextFlowIndex = current.FlowIndex;
                ex.FlowNo = current.FlowNo;
                ex.HangerNo = hangerNo;
                var eqFlowStating = hangerProuctList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value != -1).Select(k => k.StatingNo).Distinct();
                var equalFlowIsExistOtherStating = hangerProuctList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value != current.StatingNo && !f.IsFlowSucess.Value).Count() > 0;
                ex.EqualFlowIsExistOtherStating = equalFlowIsExistOtherStating;
                throw ex;
            }

            var currentHangerFlowisExist = nonProductsStatingFlow.Where(f => f.FlowNo.Equals(current.FlowNo)).Count() > 0;
            //当前工序不存在
            if (!currentHangerFlowisExist)
            {
                var ex = new FlowDeleteOrStatingDeletedException();
                ex.MainTrackNumber = int.Parse(mainTrackNo);
                ex.StatingNo = int.Parse(statingNo);
                ex.NextFlowIndex = current.FlowIndex;
                ex.HangerNo = hangerNo;
                ex.FlowNo = current.FlowNo;
                var equalFlowIsExistOtherStating = hangerProuctList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value != current.StatingNo).Count() > 0;
                ex.EqualFlowIsExistOtherStating = equalFlowIsExistOtherStating;
                throw ex;
            }
            var currentHangerFlowIsMatching = nonProductsStatingFlow.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo.Value == current.StatingNo).Count() > 0;
            //匹配走正常流程
            if (currentHangerFlowIsMatching)
            {
                ////普通工序是否完成
                //var commonFlowIsSuccess = hangerProuctList.Where(ff=> 
                //ff.FlowNo.Equals(current.FlowNo) && 
                //ff.StatingNo!=null && (int.Parse(statingNo) != current.StatingNo && ff.StatingNo.Value==int.Parse(statingNo))
                //&& ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count()>0;
                //if (commonFlowIsSuccess) {
                //    //返工工序是否完成
                //    var reworkFlowNonSuccess = hangerProuctList.Where(ff =>
                //      ff.FlowNo.Equals(current.FlowNo) &&
                //      ff.StatingNo != null && ff.StatingNo.Value == current.StatingNo
                //      && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() > 0;
                //    if (reworkFlowNonSuccess) {
                //        return current.FlowIndex;
                //    }
                //    return -1;
                //}

                return current.FlowIndex;
            }
            var currentHangerFlowIsNoMatching = nonProductsStatingFlow.Where(f => !f.FlowNo.Equals(current.FlowNo) && f.StatingNo.Value == current.StatingNo).Count() > 0;
            //不匹配取最小的未生产的工序
            if (!currentHangerFlowIsNoMatching)
            {
                var nextFlowList = hangerProuctList.Where(f => f.StatingNo != null && f.StatingNo.Value != -1
      //&& f.FlowIndex.Value > current.FlowIndex
      && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
      && ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
      || ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
      ).Select(f => f.FlowIndex);
                if (nextFlowList.Count() > 0)
                {
                    return nextFlowList.Min().Value;
                }
            }
            //工序一致，站点不一致
            var isFlowEqualStatingNoEqual = nonProductsStatingFlow.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo.Value != current.StatingNo).Count() > 0;
            if (isFlowEqualStatingNoEqual)
            {
                return current.FlowIndex;
            }
            //工序不一致，站点不一致
            var isFlowNonEqualStatingNoEqual = nonProductsStatingFlow.Where(f => !f.FlowNo.Equals(current.FlowNo) && f.StatingNo.Value != current.StatingNo).Count() == 0;
            if (isFlowEqualStatingNoEqual)
            {
                var nonAllocationOutStatingException = new NonAllocationOutStatingException(string.Format("衣架原分配站点与本站不匹配，不允许出站，请联系管理员处理！ 主轨:{0} 站点:{1} 衣架:{2}", mainTrackNo, statingNo, hangerNo));
                tcpLogError.Error(nonAllocationOutStatingException);
                throw nonAllocationOutStatingException;
            }
            //    //如果工序被删除
            //    var flowIsExist = hangerProuctList.Where(f => f.FlowNo.Equals(current.FlowNo)).Count() > 0;
            //    if (!flowIsExist)
            //    {
            //        var nextFlowList = hangerProuctList.Where(f => f.StatingNo != null && f.StatingNo.Value != -1
            ////&& f.FlowIndex.Value > current.FlowIndex
            //&& ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
            //&& ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
            //|| ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
            //).Select(f => f.FlowIndex);
            //        if (nextFlowList.Count() > 0)
            //        {
            //            return nextFlowList.Min().Value;
            //        }
            //    }

            var currentHangerProductFlow = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + ""); //dicCurrentHangerProductingFlowModelCache[hangerNo.ToString()];

            return currentHangerProductFlow.FlowIndex;
            //var flowIndexList = hangerProuctList.Select(f => f.FlowIndex).Distinct();
            //var maxFlowIndex = flowIndexList.Max().Value;
            //var maxFlowIsSuccessed = hangerProuctList.Where(f => f.FlowIndex == maxFlowIndex && f.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() > 0;
            //if (maxFlowIsSuccessed)
            //{
            //    //衣架衣架生产完成!
            //    return -2;
            //}
            //var currentIndex = hangerProuctList.Where(f => f.MergeProcessFlowChartFlowRelationId == null && f.StatingNo != null && f.StatingNo.Value != -1
            //      && f.MainTrackNumber.Value == int.Parse(mainTrackNo)
            //      && f.StatingNo.Value == int.Parse(statingNo)
            //      && f.FlowIndex.Value > 1
            //      && ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
            //   || ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
            //    ).Select(m => m.FlowIndex).Distinct();
            //if (null != currentIndex && currentIndex.Count() > 0)
            //{
            //    var vIndex = currentIndex.Min().Value;
            //    var dicAllocationCache = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
            //    if (!dicAllocationCache.ContainsKey(hangerNo.ToString()))
            //    {
            //        return -3;
            //    }
            //    var hangerAllocationList = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo.ToString()];
            //    var curAllocationList = hangerAllocationList.Where(f => !"-1".Equals(f.Memo) && null != f.NextSiteNo && int.Parse(f.NextSiteNo) == int.Parse(statingNo)).OrderByDescending(k => k.AllocatingStatingDate);
            //    if (curAllocationList.Count() > 0)
            //    {
            //        var curAllcation = curAllocationList.First();
            //        if (curAllcation.FlowIndex.Value >= vIndex)
            //        {
            //            return curAllcation.FlowIndex.Value;
            //        }
            //    }
            //    return vIndex;
            //};

            ////站点或者工序删除指向下一道工序
            //currentIndex = hangerProuctList.Where(f => f.MergeProcessFlowChartFlowRelationId == null && f.StatingNo != null && f.StatingNo.Value != -1
            //     && f.FlowIndex.Value > 1
            //     && ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
            //  || ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
            //   ).Select(m => m.FlowIndex).Distinct();
            //if (null != currentIndex && currentIndex.Count() > 0)
            //{
            //    var vIndex = currentIndex.Min().Value;
            //    vIndex = currentIndex.Where(f => f.Value > vIndex).Min().Value;
            //    return vIndex;
            //};
            // return -1;
        }
        public bool StatingFlowIsSuccessed(int currentFlowIndex, string mainTrackNo, string statingNo, List<HangerProductFlowChartModel> hangerProuctList)
        {
            var isSuccessed = hangerProuctList.Where(f => f.FlowIndex.Value == currentFlowIndex
            && f.MainTrackNumber.Value == short.Parse(mainTrackNo) && null != f.StatingNo && f.StatingNo.Value == short.Parse(statingNo)
            && (
                     //有返工未完成,且正常工序是完成的。
                     true == !(
                        (hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() != 0)
                       &&
                        (
                            (hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)
                            )
                    )
                    //没有返工未完成，且正常工序是完成的。
                    || (
                        (hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() != 0)
                       && ((hangerProuctList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() == 0))
                    )
                )
            ).Count() > 0;

            return isSuccessed;
        }

        ///// <summary>
        ///// 处理衣架出站请求
        ///// </summary>
        ///// <param name="tenMainTrackNo"></param>
        ///// <param name="tenStatingNo"></param>
        ///// <param name="outType"></param>
        ///// <param name="tenHangerNo"></param>
        ///// <param name="nextStatingNo"></param>
        ///// <param name="outMainTrackNumber"></param>
        ///// <param name="isFlowSucess">没有绑定制单的的衣架/重复出站/衣架已经在该站点生产完成</param>
        ///// <param name="info"></param>
        ///// <param name="aMainTracknumberBridgeStatingIsInFlowChart">出战衣架的下一站是否经过同主轨的桥接站，且桥接站在工艺图上</param>
        ///// <returns></returns>
        //public bool HangerOutStatingRequest(string tenMainTrackNo, string tenStatingNo, int outType, string tenHangerNo, ref string nextStatingNo, ref int outMainTrackNumber, ref bool isFlowSucess, ref string info, ref bool aMainTracknumberBridgeStatingIsInFlowChart)
        //{
        //    // DateTime dtNow = DateTime.Now;
        //    var registerHangerMessage = string.Format("主轨:{0} 站点:{1} 类型:{2} 衣架:{3}", tenMainTrackNo, tenStatingNo, outType, tenHangerNo);
        //    timersLog.Info("开始:" + registerHangerMessage);
        //    // var isHangingPieceStating = productsQueryService.isHangingPiece(null, statingNo, mainTrackNo);
        //    if (outType == 1)//如果出战消息来源于挂片站
        //    {
        //        var isSucess = RegisterHangerToProducts(tenMainTrackNo, tenStatingNo, tenHangerNo, ref nextStatingNo, ref outMainTrackNumber);
        //        //   DateTime dtEnd = DateTime.Now;
        //        timersLog.Info("结束:" + registerHangerMessage);
        //        return isSucess;
        //    }
        //    /*
        //         * 1、按出站按时，系统须判断该衣架分配的下一工序，下一工序站点的衣架数，是否达到出站条件，不满足出站时有对应的相关警报提示；
        //           2、若允许出站时，则下位机提升衣架出站，并记录本站点工序产量；
        //           3、出站时，要求下一站点应答分配衣架指令
        //         * 
        //         */
        //    try
        //    {
        //        HangerOutStatingHandler(tenMainTrackNo, tenStatingNo, tenHangerNo, ref nextStatingNo, ref outMainTrackNumber, ref isFlowSucess, ref info, ref aMainTracknumberBridgeStatingIsInFlowChart);
        //    }
        //    catch (FlowDeleteOrStatingDeletedException exNon)
        //    {
        //        FlowDeleteOrStatingDeletedHandler(exNon, tenMainTrackNo, tenStatingNo, ref outMainTrackNumber, ref nextStatingNo);
        //    }
        //    catch (FlowMoveAndStatingMoveException fmasmEx)
        //    {
        //        FlowMoveAndStatingMoveExceptionHandler(fmasmEx, tenMainTrackNo, tenStatingNo, ref outMainTrackNumber, ref nextStatingNo);
        //    }
        //    //catch (Exception ex)
        //    //{
        //    //    throw ex;
        //    //}
        //    return true;
        //}

        private void FlowMoveAndStatingMoveExceptionHandler(FlowMoveAndStatingMoveException fmasmEx, string mainTrackNo, string statingNo, ref int outMainTrackNumber, ref string nextStatingNo)
        {
            var hangerNo = fmasmEx.HangerNo.ToString();
            var nextFlowIndex = fmasmEx.NextFlowIndex;
            //  var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            var fcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo);// dicHangerProcessFlowChart[hangerNo];
                                                                                           // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo); //dicCurrentHangerProductingFlowModelCache[hangerNo];
            var nextFlowStatlist = fcList.Where(k => k.FlowIndex.Value == fmasmEx.NextFlowIndex
            && k.StatingNo != null && k.StatingNo.Value != -1
            && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 0
            && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
            && null != k.StatingRoleCode
            //&& !k.StatingRoleCode.Equals(StatingType.StatingStorage.Code)
            ).Select(f => new ProductsProcessOrderModel()
            {
                StatingNo = f.StatingNo.ToString(),
                MainTrackNumber = (int)f.MainTrackNumber,
                StatingCapacity = f.StatingCapacity.Value,
                Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                ProcessChartId = f.ProcessChartId,
                FlowNo = f.FlowNo,
                StatingRoleCode = f.StatingRoleCode

            }).ToList<ProductsProcessOrderModel>();
            if (nextFlowStatlist.Count == 0)
            {
                //下一道没有可以接收衣架的站

                var exx = new NoFoundStatingException(string.Format("【工序及站点同时改变】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackNo, statingNo, hangerNo))
                {
                    FlowNo = nextFlowStatlist.Count() > 0 ? nextFlowStatlist.First().FlowNo : "-1"
                };
                tcpLogError.Error(exx);
                throw exx;
            }
            //【获取下一站】
            OutSiteService.Instance.AllocationNextProcessFlowStating(nextFlowStatlist, ref outMainTrackNumber, ref nextStatingNo);
            if (string.IsNullOrEmpty(nextStatingNo))
            {
                var exx = new NoFoundStatingException(string.Format("【工序及站点同时改变】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackNo, statingNo, hangerNo))
                {
                    FlowNo = fcList.Where(k => k.FlowIndex.Value == fmasmEx.NextFlowIndex).First().FlowNo
                };
                tcpLogError.Error(exx);
                throw exx;
            }
            //更新衣架分配记录为处理状态到缓存
            //讲当前出战衣架的分配记录的出战时间和工序完成状态
            var dicHangerStatingALloList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo);//NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo];
            var ntStatingNo = nextStatingNo;
            //  var dicHangerProcessFlowChart = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            var hangerProcessFlowChartLis = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo);//dicHangerProcessFlowChart[hangerNo];
                                                                                                              //  NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = dicHangerStatingALloList;
            var nextHangerFlowChartList = hangerProcessFlowChartLis.Where(f => f.FlowIndex.Value == nextFlowIndex && f.StatingNo.Value == short.Parse(ntStatingNo) && (f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value || f.Status.Value == HangerProductFlowChartStaus.Producting.Value)).ToList<HangerProductFlowChartModel>();
            if (nextHangerFlowChartList.Count > 0)
            {
                var nextHangerFlowChart = nextHangerFlowChartList[0];
                var nextHangerStatingAllocationItem = new DaoModel.HangerStatingAllocationItem();
                nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
                nextHangerStatingAllocationItem.FlowIndex = (short)nextHangerFlowChart.FlowIndex;
                nextHangerStatingAllocationItem.SiteNo = statingNo;
                nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
                nextHangerStatingAllocationItem.HangerNo = hangerNo;
                nextHangerStatingAllocationItem.NextSiteNo = nextStatingNo;
                nextHangerStatingAllocationItem.OutMainTrackNumber = outMainTrackNumber;
                nextHangerStatingAllocationItem.FlowNo = nextHangerFlowChart.FlowNo;
                nextHangerStatingAllocationItem.ProcessFlowCode = nextHangerFlowChart.FlowCode;
                nextHangerStatingAllocationItem.ProcessFlowName = nextHangerFlowChart.FlowName;
                nextHangerStatingAllocationItem.HangerType = nextHangerFlowChart.FlowType;
                //控制出站产量推送方式(返工还是正常)
                nextHangerStatingAllocationItem.IsReturnWorkFlow = false;
                nextHangerStatingAllocationItem.ProcessFlowId = nextHangerFlowChart.FlowId;
                nextHangerStatingAllocationItem.MainTrackNumber = (short)outMainTrackNumber;
                nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
                nextHangerStatingAllocationItem.FlowChartd = nextHangerFlowChart.ProcessChartId;
                nextHangerStatingAllocationItem.ProductsId = nextHangerFlowChart.ProductsId;
                nextHangerStatingAllocationItem.PSize = nextHangerFlowChart.PSize;
                nextHangerStatingAllocationItem.PColor = nextHangerFlowChart.PColor;
                nextHangerStatingAllocationItem.ProcessOrderNo = nextHangerFlowChart.ProcessOrderNo;
                nextHangerStatingAllocationItem.StyleNo = nextHangerFlowChart.StyleNo;
                nextHangerStatingAllocationItem.LastFlowIndex = nextFlowIndex;
                nextHangerStatingAllocationItem.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo);
                nextHangerStatingAllocationItem.GroupNo = BridgeService.Instance.GetGroupNo(outMainTrackNumber, int.Parse(nextStatingNo));

                dicHangerStatingALloList.Add(nextHangerStatingAllocationItem);
                //  NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = dicHangerStatingALloList;
                NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo, dicHangerStatingALloList);

                //记录衣架分配
                var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

                foreach (var hfc in hangerProcessFlowChartLis)
                {
                    if (nextStatingNo.Equals(hfc.StatingNo.Value.ToString()) && null != hfc.StatingNo && hfc.Status.Value == 0 && nextHangerFlowChart.FlowIndex.Value == hfc.FlowIndex.Value)
                    {
                        hfc.isAllocationed = true;
                        hfc.AllocationedDate = DateTime.Now;
                        hfc.IsStorageStatingAgainAllocationedSeamsStating = false;
                        //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo] = hangerProcessFlowChartLis;
                        NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(hangerNo, hangerProcessFlowChartLis);
                        break;
                    }
                }
                //修正本站衣架轨迹
                var hpResumeOut = new HangerProductsChartResumeModel()
                {
                    HangerNo = hangerNo,
                    StatingNo = statingNo,
                    MainTrackNumber = int.Parse(mainTrackNo),
                    FlowNo = current.FlowNo,
                    // HangerProductFlowChart = cflowChartList.First(),
                    Action = 7
                };
                var hpResumeOutJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResumeOut);
                //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hpResumeOutJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hpResumeOutJson);
                //【衣架生产履历】下一站分配Cache写入
                var nextStatingHPResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = hangerNo,
                    StatingNo = statingNo,
                    MainTrackNumber = int.Parse(mainTrackNo),
                    HangerProductFlowChart = nextHangerFlowChart,
                    Action = 1,
                    NextStatingNo = nextStatingNo
                };
                var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                //发布衣架状态
                var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
                chpf.HangerNo = hangerNo;
                chpf.MainTrackNumber = outMainTrackNumber;
                chpf.StatingNo = int.Parse(nextStatingNo);
                chpf.FlowNo = nextHangerFlowChart.FlowNo;
                chpf.FlowIndex = nextHangerFlowChart.FlowIndex.Value;
                chpf.FlowType = null == nextHangerFlowChart?.FlowType ? 0 : nextHangerFlowChart.FlowType.Value;
                var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);


                // 修正的站内数及明细、缓存
                var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssocDeleteStating.Action = 8;
                hnssocDeleteStating.HangerNo = hangerNo;
                hnssocDeleteStating.MainTrackNumber = current.MainTrackNumber;
                hnssocDeleteStating.StatingNo = current.StatingNo;
                hnssocDeleteStating.FlowNo = current.FlowNo;
                hnssocDeleteStating.FlowIndex = current.FlowIndex;
                //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
                var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocDeleteStatingJson);

                //修正分配明细
                var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssoc.Action = 0;
                hnssoc.HangerNo = hangerNo;
                hnssoc.MainTrackNumber = outMainTrackNumber;
                hnssoc.StatingNo = int.Parse(nextStatingNo);
                hnssoc.FlowNo = nextHangerFlowChart.FlowNo;
                hnssoc.FlowIndex = nextHangerFlowChart.FlowIndex.Value;
                hnssoc.HangerProductFlowChartModel = nextHangerFlowChart;
                var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                //   NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
            }
        }

        private void FlowDeleteOrStatingDeletedHandler(FlowDeleteOrStatingDeletedException fdsdEx, string mainTrackNo, string statingNo, ref int outMainTrackNumber, ref string nextStatingNo)
        {
            var hangerNo = fdsdEx.HangerNo.ToString();
            //  var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(fdsdEx.HangerNo + ""))//dicHangerProcessFlowChart.ContainsKey(fdsdEx.HangerNo.ToString()))
            {
                var eeInfo = string.Format("【错误】主轨:【{0}】站点:【{1}】衣架【{2}】衣架不在工艺图上!", mainTrackNo, statingNo, fdsdEx.HangerNo.ToString());
                tcpLogError.Error(eeInfo);
                throw new ApplicationException(eeInfo);
            }

            //同工序是否存在站点
            var equalFlowIsExistOtherStating = false;
            var isDeleteFlow = false;
            var isOnlyMoveFlow = fdsdEx.IsOnlyMoveFlow;//是否仅移动工序
            IEnumerable<int?> nextFlowIndexList = null;
            equalFlowIsExistOtherStating = fdsdEx.EqualFlowIsExistOtherStating;
            if (!fdsdEx.EqualFlowIsExistOtherStating)
            {

                nextFlowIndexList = NewCacheService.Instance.GetHangerFlowChartListForRedis(fdsdEx.HangerNo.ToString()).Where(k => k.FlowIndex.Value >= fdsdEx.NextFlowIndex
                && k.StatingNo != null && k.StatingNo.Value != -1
                && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 0
                && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
                && null != k.StatingRoleCode
              //&& !k.StatingRoleCode.Equals(StatingType.StatingStorage.Code)
              ).Select(f => f.FlowIndex);

                //当前工序删除，生产顺序上移，即下道工序是未完成的,查询当前工序生产顺序是否有未完成的工序
                if (nextFlowIndexList.Count() == 0)
                {
                    nextFlowIndexList = NewCacheService.Instance.GetHangerFlowChartListForRedis(fdsdEx.HangerNo.ToString()).Where(k => k.FlowIndex.Value > fdsdEx.NextFlowIndex
                     && k.StatingNo != null && k.StatingNo.Value != -1
                     && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 0
                     && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
                     && null != k.StatingRoleCode
                   //&& !k.StatingRoleCode.Equals(StatingType.StatingStorage.Code)
                   ).Select(f => f.FlowIndex);
                }
            }
            else
            {
                nextFlowIndexList = NewCacheService.Instance.GetHangerFlowChartListForRedis(fdsdEx.HangerNo.ToString()).Where(k => k.FlowIndex.Value == fdsdEx.NextFlowIndex
                   && k.StatingNo != null && k.StatingNo.Value != -1
                   && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 0
                   && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
                   && null != k.StatingRoleCode
                 //&& !k.StatingRoleCode.Equals(StatingType.StatingStorage.Code)
                 ).Select(f => f.FlowIndex);
            }
            var nextFlowIndex = nextFlowIndexList.Count() > 0 ? nextFlowIndexList.Min().Value : -1;
            var fChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(fdsdEx.HangerNo.ToString());
            var nextFlowStatlist = fChartList.Where(k => k.FlowIndex.Value == nextFlowIndex
              && k.StatingNo != null && k.StatingNo.Value != -1
              && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 0
              && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
              && null != k.StatingRoleCode
              //&& !k.StatingRoleCode.Equals(StatingType.StatingStorage.Code)
              ).Select(f => new ProductsProcessOrderModel()
              {
                  StatingNo = f.StatingNo.ToString(),
                  MainTrackNumber = (int)f.MainTrackNumber,
                  StatingCapacity = f.StatingCapacity.Value,
                  Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                  ProcessChartId = f.ProcessChartId,
                  FlowNo = f.FlowNo,
                  StatingRoleCode = f.StatingRoleCode

              }).ToList<ProductsProcessOrderModel>();
            if (nextFlowStatlist.Count == 0)
            {
                //下一道没有可以接收衣架的站
                var ffList = fChartList.Where(k => k.FlowIndex.Value == nextFlowIndex);
                var exx = new NoFoundStatingException(string.Format("【工序或者站点删除】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackNo, statingNo, fdsdEx.HangerNo.ToString()))
                {
                    FlowNo = ffList.Count() > 0 ? ffList.First().FlowNo : "-1"
                };
                tcpLogError.Error(exx);
                throw exx;
            }
            //【获取下一站】
            OutSiteService.Instance.AllocationNextProcessFlowStating(nextFlowStatlist, ref outMainTrackNumber, ref nextStatingNo);
            if (string.IsNullOrEmpty(nextStatingNo))
            {
                var exx = new NoFoundStatingException(string.Format("【工序或者站点删除】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", mainTrackNo, statingNo, fdsdEx.HangerNo.ToString()))
                {
                    FlowNo = fChartList.Where(k => k.FlowIndex.Value == nextFlowIndex).First().FlowNo
                };
                tcpLogError.Error(exx);
                throw exx;
            }
            //更新衣架分配记录为处理状态到缓存
            //讲当前出战衣架的分配记录的出战时间和工序完成状态
            var dicHangerStatingALloList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(hangerNo); //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo];
            var ntStatingNo = nextStatingNo;
            //  var dicHangerProcessFlowChart = SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            var hangerProcessFlowChartLis = NewCacheService.Instance.GetHangerFlowChartListForRedis(fdsdEx.HangerNo.ToString());
            // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = dicHangerStatingALloList;
            NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo, dicHangerStatingALloList);
            var nextHangerFlowChartList = hangerProcessFlowChartLis.Where(f => f.FlowIndex.Value == nextFlowIndex && f.StatingNo.Value == short.Parse(ntStatingNo) && (f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value || f.Status.Value == HangerProductFlowChartStaus.Producting.Value)).ToList<HangerProductFlowChartModel>();
            if (nextHangerFlowChartList.Count > 0)
            {
                var nextHangerFlowChart = nextHangerFlowChartList[0];
                var nextHangerStatingAllocationItem = new DaoModel.HangerStatingAllocationItem();
                nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
                nextHangerStatingAllocationItem.FlowIndex = (short)nextHangerFlowChart.FlowIndex;
                nextHangerStatingAllocationItem.SiteNo = statingNo;
                nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
                nextHangerStatingAllocationItem.HangerNo = hangerNo;
                nextHangerStatingAllocationItem.NextSiteNo = nextStatingNo;
                nextHangerStatingAllocationItem.OutMainTrackNumber = outMainTrackNumber;
                nextHangerStatingAllocationItem.FlowNo = nextHangerFlowChart.FlowNo;
                nextHangerStatingAllocationItem.ProcessFlowCode = nextHangerFlowChart.FlowCode;
                nextHangerStatingAllocationItem.ProcessFlowName = nextHangerFlowChart.FlowName;
                nextHangerStatingAllocationItem.HangerType = nextHangerFlowChart.FlowType;
                //控制出站产量推送方式(返工还是正常)
                nextHangerStatingAllocationItem.IsReturnWorkFlow = false;
                nextHangerStatingAllocationItem.ProcessFlowId = nextHangerFlowChart.FlowId;
                nextHangerStatingAllocationItem.MainTrackNumber = (short)outMainTrackNumber;
                nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
                nextHangerStatingAllocationItem.FlowChartd = nextHangerFlowChart.ProcessChartId;
                nextHangerStatingAllocationItem.ProductsId = nextHangerFlowChart.ProductsId;
                nextHangerStatingAllocationItem.PSize = nextHangerFlowChart.PSize;
                nextHangerStatingAllocationItem.PColor = nextHangerFlowChart.PColor;
                nextHangerStatingAllocationItem.ProcessOrderNo = nextHangerFlowChart.ProcessOrderNo;
                nextHangerStatingAllocationItem.StyleNo = nextHangerFlowChart.StyleNo;
                nextHangerStatingAllocationItem.LastFlowIndex = fdsdEx.NextFlowIndex;
                nextHangerStatingAllocationItem.BatchNo = BridgeService.Instance.GetBatchNo(hangerNo);
                nextHangerStatingAllocationItem.GroupNo = BridgeService.Instance.GetGroupNo(outMainTrackNumber, int.Parse(nextStatingNo));

                dicHangerStatingALloList.Add(nextHangerStatingAllocationItem);
                //  NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = dicHangerStatingALloList;
                NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(hangerNo, dicHangerStatingALloList);

                //记录衣架分配
                var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

                foreach (var hfc in hangerProcessFlowChartLis)
                {
                    if (nextStatingNo.Equals(hfc.StatingNo.Value.ToString()) && null != hfc.StatingNo && hfc.Status.Value == 0 && nextHangerFlowChart.FlowIndex.Value == hfc.FlowIndex.Value)
                    {
                        hfc.isAllocationed = true;
                        hfc.AllocationedDate = DateTime.Now;
                        hfc.IsStorageStatingAgainAllocationedSeamsStating = false;
                        // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo] = hangerProcessFlowChartLis;
                        NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(hangerNo, hangerProcessFlowChartLis);
                        break;
                    }
                }
                SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel current = null;
                //  var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                if (NewCacheService.Instance.HangerCurrentFlowIsContains(fdsdEx.HangerNo + ""))//dicCurrentHangerProductingFlowModelCache.ContainsKey(fdsdEx.HangerNo.ToString()))
                {
                    current = NewCacheService.Instance.GetHangerCurrentFlow(fdsdEx.HangerNo + "");//dicCurrentHangerProductingFlowModelCache[fdsdEx.HangerNo.ToString()];
                    //是否记录产量(移动工序的情况)
                    var isRecordProducts = NewCacheService.Instance.GetHangerFlowChartListForRedis(fdsdEx.HangerNo.ToString()).Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value == current.StatingNo).Select(k => k.FlowIndex).Count() > 0;
                    if (isRecordProducts)
                    {
                        var outSiteResult = new HangerOutSiteResult();
                        outSiteResult.MainTrackNumber = int.Parse(mainTrackNo);
                        outSiteResult.HangerNo = hangerNo;
                        outSiteResult.StatingNo = statingNo;
                        outSiteResult.HangerProductFlowChart = fChartList.Where(f => f.FlowNo.Equals(current.FlowNo)).First();
                        var outSiteJson = Newtonsoft.Json.JsonConvert.SerializeObject(outSiteResult);
                        //SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_OUT_SITE_ACTION, outSiteJson);
                        NewSusRedisClient.Instance.HangerOutSiteAction(new StackExchange.Redis.RedisChannel(), outSiteJson);
                        //var newFlowIndex = dicHangerProcessFlowChart[fdsdEx.HangerNo.ToString()].Where(f => f.FlowNo.Equals(current.FlowNo)).Select(k => k.FlowIndex).First().Value;
                    }

                    isDeleteFlow = NewCacheService.Instance.GetHangerFlowChartListForRedis(fdsdEx.HangerNo.ToString()).Where(f => f.FlowNo.Equals(current.FlowNo)).Count() == 0;

                    //【衣架生产履历】本站衣架生产履历Cache写入

                    //工序是否可接收
                    var cflowChartList = fChartList.Where(f => f.FlowNo.Equals(current.FlowNo));
                    if (cflowChartList.Count() > 0)
                    {
                        var hpResumeOut = new HangerProductsChartResumeModel()
                        {
                            HangerNo = hangerNo,
                            StatingNo = statingNo,
                            MainTrackNumber = int.Parse(mainTrackNo),
                            HangerProductFlowChart = cflowChartList.First(),
                            Action = 3
                        };
                        var hpResumeOutJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResumeOut);
                        //   NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hpResumeOutJson);
                        NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hpResumeOutJson);
                    }
                }

                //【衣架生产履历】下一站分配Cache写入
                var nextStatingHPResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = hangerNo,
                    StatingNo = statingNo,
                    MainTrackNumber = int.Parse(mainTrackNo),
                    HangerProductFlowChart = nextHangerFlowChart,
                    Action = 1,
                    NextStatingNo = nextStatingNo
                };
                var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                //发布衣架状态
                var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
                chpf.HangerNo = hangerNo;
                chpf.MainTrackNumber = outMainTrackNumber;
                chpf.StatingNo = int.Parse(nextStatingNo);
                chpf.FlowNo = nextHangerFlowChart.FlowNo;
                chpf.FlowIndex = nextHangerFlowChart.FlowIndex.Value;
                chpf.FlowType = null == nextHangerFlowChart?.FlowType ? 0 : nextHangerFlowChart.FlowType.Value;
                var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);

                //修正删除的站内衣架数
                if (equalFlowIsExistOtherStating)
                {
                    //修正删除的站内数及明细、缓存
                    var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                    hnssocDeleteStating.Action = 3;
                    hnssocDeleteStating.HangerNo = hangerNo;
                    hnssocDeleteStating.MainTrackNumber = current.MainTrackNumber;
                    hnssocDeleteStating.StatingNo = current.StatingNo;
                    hnssocDeleteStating.FlowNo = current.FlowNo;
                    hnssocDeleteStating.FlowIndex = current.FlowIndex;
                    //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
                    var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
                    // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
                    NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocDeleteStatingJson);
                    //【衣架生产履历】站点删除 修正衣架生产履历Cache
                    var hpResume = new HangerProductsChartResumeModel()
                    {
                        HangerNo = hangerNo,
                        StatingNo = current.StatingNo.ToString(),
                        MainTrackNumber = current.MainTrackNumber,
                        FlowNo = current.FlowNo,
                        Action = 5
                    };
                    var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
                    // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
                    NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hangerResumeJson);
                }
                if (isDeleteFlow)
                {
                    //修正删除的站内数及明细、缓存
                    var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                    hnssocDeleteStating.Action = 4;
                    hnssocDeleteStating.HangerNo = hangerNo;
                    hnssocDeleteStating.MainTrackNumber = current.MainTrackNumber;
                    hnssocDeleteStating.StatingNo = current.StatingNo;
                    hnssocDeleteStating.FlowNo = current.FlowNo;
                    hnssocDeleteStating.FlowIndex = current.FlowIndex;
                    //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
                    var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
                    // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
                    NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocDeleteStatingJson);

                    //【衣架生产履历】站点删除 修正衣架生产履历Cache
                    var hpResume = new HangerProductsChartResumeModel()
                    {
                        HangerNo = hangerNo,
                        StatingNo = current.StatingNo.ToString(),
                        MainTrackNumber = current.MainTrackNumber,
                        FlowNo = current.FlowNo,
                        Action = 6
                    };
                    var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
                    //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
                    NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hangerResumeJson);
                }
                if (isOnlyMoveFlow)
                {
                    //修正删除的站内数及明细、缓存
                    var hnssocDeleteStating = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                    hnssocDeleteStating.Action = 2;
                    hnssocDeleteStating.HangerNo = hangerNo;
                    hnssocDeleteStating.MainTrackNumber = current.MainTrackNumber;
                    hnssocDeleteStating.StatingNo = current.StatingNo;
                    hnssocDeleteStating.FlowNo = current.FlowNo;
                    hnssocDeleteStating.FlowIndex = current.FlowIndex;
                    //hnssocDeleteStating.HangerProductFlowChartModel = nextHangerFlowChart;
                    var hnssocDeleteStatingJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocDeleteStating);
                    //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocDeleteStatingJson);
                    NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocDeleteStatingJson);
                }
                //修正删除的站内数及明细、缓存
                var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssoc.Action = 0;
                hnssoc.HangerNo = hangerNo;
                hnssoc.MainTrackNumber = outMainTrackNumber;
                hnssoc.StatingNo = int.Parse(nextStatingNo);
                hnssoc.FlowNo = nextHangerFlowChart.FlowNo;
                hnssoc.FlowIndex = nextHangerFlowChart.FlowIndex.Value;
                hnssoc.HangerProductFlowChartModel = nextHangerFlowChart;
                var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
            }
        }
        /// <summary>
        /// 计算衣架去下一道工序的站点
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <returns></returns>
        string CalculationNextFlowStatingNo(string hangerNo)
        {
            var sql = new StringBuilder("select top 1 * from WaitProcessOrderHanger where HangerNo=?");
            var wp = QueryForObject<DaoModel.WaitProcessOrderHanger>(sql, null, false, hangerNo);
            var flowChart = ProcessFlowChartDao.Instance.GetById(wp.FlowChartd);
            var flowIndex = wp.FlowIndex + (short)1;
            var pfcFlowRelation = GetNextProcessFlow(flowChart, (short)flowIndex);
            if (null == pfcFlowRelation)
            {
                var ex = new ApplicationException("工序不存在!");
                tcpLogError.Error(ex);
                throw ex;
            }
            var statingList = CANProductsValidService.Instance.GetHangerNextProcessFlowStatingList(flowChart.Id, pfcFlowRelation.ProcessFlow?.Id);
            if (statingList.Count == 0)
            {
                var ex = new ApplicationException("工序找不到站点!");
                tcpLogError.Error(ex);
                throw ex;
            }
            string nextStatingNo = statingList[0]?.Stating?.StatingNo?.Trim();
            return nextStatingNo;
        }

        //        ///// <summary>
        //        ///// 普通站衣架出站处理
        //        ///// 说明:
        //        ///// 1.更新衣架工序生产明细的衣架出站时间
        //        ///// 2.记录站点衣架生产记录
        //        ///// 3.计算下一道工序站点并 改变待生产明细的工序将其指向下一道工序
        //        ///// </summary>
        //        ///// <param name="tenMainTrackNo"></param>
        //        ///// <param name="tenStatingNo"></param>
        //        ///// <param name="tenHangerNo"></param>
        //        ///// <param name="isFlowSucess">没有绑定制单的的衣架/重复出站/衣架已经在该站点生产完成</param>
        //        ///// <param name="aMainTracknumberBridgeStatingIsInFlowChart">出战衣架的下一站是否经过同主轨的桥接站，且桥接站在工艺图上</param>
        //        //public void HangerOutStatingHandler(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, ref string nextFlowStatingNo, ref int outMainTrackNumber, ref bool isFlowSucess, ref string info, ref bool aMainTracknumberBridgeStatingIsInFlowChart)
        //        {

        //            var isBridgeRequire = false;//是否需要桥接
        //            //var aMainTracknumberBridgeStatingIsInFlowChart = false;//是否在工艺图
        //            var bMainTrackNumberBridgeStatingIsInFlowChart = false;//是否在工艺图
        //            var nextStatingPassBridgeStatingAndBridgeStatingInFlowChart = false; //下一站需要经过桥接，且桥接站在工艺图上，且桥接站和下一站相同
        //            //检查出站站点是否有员工登录，没有登录不能出站
        //            bool isLogin = productsQueryService.CheckStatingIsLogin(tenStatingNo, int.Parse(tenMainTrackNo));
        //            if (!isLogin)
        //            {
        //                var ex = new StatingNoLoginEmployeeException(string.Format("【错误】主轨:【{0}】站点:【{1}】没有员工登录!不能出站", tenMainTrackNo, tenStatingNo));
        //                tcpLogError.Error(ex);
        //                throw ex;
        //            }
        //            //衣架生产工艺图
        //            //检查站点是否生产过该衣架
        //            //   var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
        //            if (!NewCacheService.Instance.HangerIsContainsFlowChart(tenHangerNo))//dicHangerProcessFlowChart.ContainsKey(tenHangerNo))
        //            {
        //                info = string.Format("【错误】主轨:【{0}】站点:【{1}】衣架【{2}】衣架不在工艺图上!", tenMainTrackNo, tenStatingNo, tenHangerNo);
        //                tcpLogInfo.Info(info);
        //                isFlowSucess = true;
        //                return;
        //            }
        //            bool isRepeatOutSite = NewCacheService.Instance.IsReatOutSite(int.Parse(tenMainTrackNo), int.Parse(tenStatingNo), int.Parse(tenHangerNo));// IsCheckRepeatOutSite(tenMainTrackNo, tenStatingNo, tenHangerNo);
        //            if (isRepeatOutSite)
        //            {
        //                info = string.Format("2【错误】主轨:【{0}】站点:【{1}】衣架【{2}】重复出站", tenMainTrackNo, tenStatingNo, tenHangerNo);
        //                tcpLogInfo.Info(info);
        //                isFlowSucess = true;
        //                return;
        //            }
        //            //桥接站出战，且出战站点不在工艺图上
        //            var currentStatingEqualBridgeStatingNotInFlowChart = IsBridgeByOutSiteRequestAction(int.Parse(tenMainTrackNo), int.Parse(tenStatingNo), int.Parse(tenHangerNo));
        //            if (currentStatingEqualBridgeStatingNotInFlowChart)
        //            {
        //                BridgeStatingNoToNextSatingHandler(tenMainTrackNo, tenStatingNo, tenHangerNo, ref nextFlowStatingNo, ref outMainTrackNumber, ref isFlowSucess, ref info);
        //                CorrectBridgeInStatingNum(tenMainTrackNo, tenStatingNo, outMainTrackNumber, tenHangerNo);
        //                return;
        //            }
        //            //桥接站出战，且在工艺图上，需要反向标记桥接站的产量
        //#warning 桥接站出战，且在工艺图上，需要反向标记桥接站的产量

        //            ///
        //            int currentIndex = 0;
        //            bool isBSiteFlowChart = false;
        //            DaoModel.BridgeSet bBridge = null;
        //            DaoModel.BridgeSet aBirdeSet = null;
        //            bool isBridgeOutSiteAndInFlowChart = IsBridgeOutSiteAndInFlowChart(tenMainTrackNo, tenStatingNo, tenHangerNo, ref currentIndex, ref isBSiteFlowChart, ref bBridge, ref aBirdeSet);
        //            bool isFlowMoveAndStatingMove = IsFlowMoveAndStatingMove(tenMainTrackNo, tenStatingNo, tenHangerNo);
        //            if (!isFlowMoveAndStatingMove && !isBridgeOutSiteAndInFlowChart)
        //                currentIndex = GetCurrentProductingIndex(int.Parse(tenHangerNo), NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo), tenMainTrackNo, tenStatingNo);
        //            if (currentIndex == 0)
        //            {
        //                var ex = new ApplicationException(string.Format("下一站计算错误! 主轨:{0} 衣架号:{1} 站点:{2}", tenMainTrackNo, tenHangerNo, tenStatingNo));
        //                errorLog.Error("【衣架出站】", ex);
        //                return;
        //            }
        //            //检查站点是否被删除
        //            var fccList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo);//dicHangerProcessFlowChart[tenHangerNo];
        //            ///
        //            var isStatingExist = fccList.Where(f => f.FlowIndex.Value == currentIndex && f.MainTrackNumber.Value == short.Parse(tenMainTrackNo)
        //            && f.StatingNo != null && f.StatingNo.Value == short.Parse(tenStatingNo)).Count() > 0;

        //            //是否存在返工工序
        //            //返工到同工序站的情况待修正
        //            var isExistReworkFlow = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo).Where(f => !f.IsFlowSucess.Value && f.StatingNo.Value == short.Parse(tenStatingNo) && f.FlowType.Value == 1).Select(k => k.HangerNo).ToList<string>().Count > 0;
        //            if (!isExistReworkFlow)
        //            {

        //                if (currentIndex == -2 || currentIndex == -1)
        //                {
        //                    info = string.Format("1【错误】主轨:【{0}】站点:【{1}】衣架【{2}】重复出站", tenMainTrackNo, tenStatingNo, tenHangerNo);
        //                    tcpLogInfo.Info(info);
        //                    isFlowSucess = true;
        //                    return;
        //                }
        //                if (currentIndex == -3)
        //                {
        //                    var nonAllocationOutStatingException = new NonAllocationOutStatingException(string.Format("衣架原分配站点与本站不匹配，不允许出站，请联系管理员处理！ 主轨:{0} 站点:{1} 衣架:{2}", tenMainTrackNo, tenStatingNo, tenHangerNo));
        //                    tcpLogError.Error(nonAllocationOutStatingException);
        //                    throw nonAllocationOutStatingException;
        //                }
        //                var hangerList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo);// dicHangerProcessFlowChart[tenHangerNo];
        //                // var statingFlowIndexList = hangerList.Where(f => currentIndex == f.FlowIndex.Value && f.MainTrackNumber.Value == short.Parse(mainTrackNo) && null != f.StatingNo && f.StatingNo.Value == short.Parse(statingNo)).Select(ff => new { FlowIndex = ff.FlowIndex }).Distinct();
        //                if (currentIndex == 1)//statingFlowIndexList.Count() > 0)
        //                {
        //                    info = string.Format("【错误】主轨:【{0}】站点:【{1}】衣架【{2} 】 衣架已经生产完成!", tenMainTrackNo, tenStatingNo, tenHangerNo);
        //                    isFlowSucess = true;
        //                    tcpLogInfo.Info(info);
        //                    return;
        //                }
        //            }
        //            ///工序不存在：已删除/没有分配工序
        //            if (!isStatingExist)
        //            {
        //                // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
        //                var ex = new FlowDeleteOrStatingDeletedException();
        //                ex.MainTrackNumber = int.Parse(tenMainTrackNo);
        //                ex.StatingNo = int.Parse(tenStatingNo);
        //                ex.NextFlowIndex = currentIndex;
        //                ex.HangerNo = int.Parse(tenHangerNo);
        //                if (NewCacheService.Instance.HangerCurrentFlowIsContains(tenHangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(tenHangerNo))
        //                {
        //                    var current = NewCacheService.Instance.GetHangerCurrentFlow(tenHangerNo); //dicCurrentHangerProductingFlowModelCache[tenHangerNo];
        //                    var equalFlowIsExistOtherStating = fccList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value != current.StatingNo).Count() > 0;
        //                    ex.EqualFlowIsExistOtherStating = equalFlowIsExistOtherStating;
        //                    ex.IsOnlyMoveFlow = CheckIsOnlyMoveFlow(current, fccList);
        //                }

        //                throw ex;
        //            }

        //            List<HangerProductFlowChartModel> hangerProcessFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo);
        //            //  dicHangerProcessFlowChart.TryGetValue(tenHangerNo, out hangerProcessFlowChartList);

        //            //更新衣架站点生产信息
        //            CorrectHangerProductItemToCacheByOutSite(tenMainTrackNo, tenStatingNo, tenHangerNo);

        //            //标记当前工序衣架完成
        //            List<HangerProductFlowChartModel> ppChartList = hangerProcessFlowChartList.Where(f =>
        //            currentIndex == f.FlowIndex.Value &&
        //            f.MainTrackNumber == short.Parse(tenMainTrackNo) &&
        //            f.StatingNo.Value == short.Parse(tenStatingNo) &&
        //            f.Status.Value != HangerProductFlowChartStaus.Successed.Value).OrderBy(f => f.FlowIndex).OrderByDescending(ff => ff.AllocationedDate).ToList<HangerProductFlowChartModel>();
        //            HangerProductFlowChartModel ppChart = null;
        //            if (ppChartList.Count > 0)
        //            {
        //                ppChart = ppChartList.First();
        //            }
        //            //更新衣架生产工艺图制作站点的信息
        //            //var ppChart = GetHangerProductFlowChart(int.Parse(mainTrackNo), hangerNo, statingNo);

        //            if (null == ppChart)
        //            {
        //                var ex = new ApplicationException(string.Format("找不着衣架生产工艺图信息! 主轨:{0} 衣架号:{1} 站点:{2}", tenMainTrackNo, tenHangerNo, tenStatingNo));
        //                errorLog.Error("【衣架出站】", ex);
        //                return;
        //            }
        //            //桥接站是否只有一道站点
        //            IList<HangerProductFlowChartModel> nextPPChartList = null;
        //            var nCount = hangerProcessFlowChartList.Where(f => f.Status.Value != HangerProductFlowChartStaus.Successed.Value && f.FlowIndex.Value > ppChart.FlowIndex.Value//currentFlowIndex
        //        && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        //        && ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        //        || ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0))

        //        ) && f.MainTrackNumber.Value == aBirdeSet.AMainTrackNumber.Value
        //      ).Count() == 0;
        //            //      var tt = hangerProcessFlowChartList.Where(f => f.Status.Value != HangerProductFlowChartStaus.Successed.Value && f.FlowIndex.Value > ppChart.FlowIndex.Value//currentFlowIndex
        //            //   && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        //            //   && ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        //            //   || ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0))

        //            //   )
        //            //   && f.MainTrackNumber.Value == aBirdeSet.AMainTrackNumber.Value
        //            //);
        //            if (!isBSiteFlowChart && !nCount)
        //            {
        //                nextPPChartList = hangerProcessFlowChartList.Where(f => f.Status.Value != HangerProductFlowChartStaus.Successed.Value && f.FlowIndex.Value > ppChart.FlowIndex.Value//currentFlowIndex
        //           && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        //           && ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        //           || ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
        //           ).OrderBy(f => f.FlowIndex).ToList<HangerProductFlowChartModel>();
        //            }
        //            else if (isBSiteFlowChart && nCount)
        //            {
        //                //

        //                nextPPChartList = hangerProcessFlowChartList.Where(f => f.Status.Value != HangerProductFlowChartStaus.Successed.Value && f.FlowIndex.Value != ppChart.FlowIndex.Value//currentFlowIndex
        //                && f.FlowIndex.Value != 1
        //         && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        //         && ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        //         || ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0))

        //         )
        //         ).OrderBy(f => f.FlowIndex).ToList<HangerProductFlowChartModel>();
        //            }
        //            else
        //            {
        //                nextPPChartList = hangerProcessFlowChartList.Where(f => f.Status.Value != HangerProductFlowChartStaus.Successed.Value && f.FlowIndex.Value > ppChart.FlowIndex.Value

        //        //currentFlowIndex
        //        && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
        //        && ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
        //        || ((hangerProcessFlowChartList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0))
        //        && f.MainTrackNumber.Value != bBridge.AMainTrackNumber.Value && f.StatingNo.Value != bBridge.ASiteNo.Value
        //        )
        //        ).OrderBy(f => f.FlowIndex).ToList<HangerProductFlowChartModel>();
        //            }

        //            //

        //            //if (nextPPChartList.Count != 0)
        //            //{//生产完成（返工时）

        //            //}
        //            var nextPPChart = nextPPChartList.Count > 0 ? nextPPChartList.OrderBy(f => f.FlowIndex.Value).First() : null;

        //            //是否是存储站出战:存储站不记录产量
        //            var statingRole = ppChart.StatingRoleCode?.Trim();
        //            var storeStatingRoleCode = StatingType.StatingStorage.Code?.Trim();
        //            var isStoreStatingOutSite = storeStatingRoleCode.Equals(statingRole);
        //            var IsStorageStatingAgainAllocationedSeamsStating = false;
        //            if (isStoreStatingOutSite)
        //            {
        //                nextPPChart = ppChart;
        //                IsStorageStatingAgainAllocationedSeamsStating = true;
        //            }

        //            //需要处理人为把衣架取下来的情况

        //            //计算下一道工序站点并 改变待生产明细的工序将其指向下一道工序
        //            //var flowChart = ProcessFlowChartDao.Instance.GetById(obj.FlowChartd);
        //            int flowIndex = 0;
        //            int currentFlowIndex = ppChart.FlowIndex.Value;
        //            string nextStatingNo = null;
        //            HangerProductFlowChartModel hpFlowChart = null;
        //            //是否是返工工序
        //            var isRewokFlow = false;
        //            isRewokFlow = ppChart.FlowType.Value == 1;

        //            if (isRewokFlow)
        //            {
        //                ReworkHandlerByOutSiteRequest(tenHangerNo, ref outMainTrackNumber, hangerProcessFlowChartList, ppChart, nextPPChart, out flowIndex, ref nextStatingNo, out hpFlowChart);
        //            }
        //            else
        //            {
        //                if (null != nextPPChart)
        //                {
        //                    flowIndex = nextPPChart.FlowIndex.Value; //obj.FlowIndex + (short)1;
        //                                                             //var fchartId = ppChart.ProcessChartId;
        //                    DaoModel.ProcessFlowChartFlowRelation pfcFlowRelation = null;
        //                    //zxl 2018年7月22日 10:28:14
        //                    //OutSiteService.Instance.GetHangerNextSite(int.Parse(mainTrackNo), hangerNo, flowIndex, ref nextStatingNo, ref pfcFlowRelation, ref outMainTrackNumber, ref hpFlowChart);
        //                    DaoModel.WaitProcessOrderHanger p = null;
        //                    var dicWaitProcessOrderHanger = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DaoModel.WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER);
        //                    dicWaitProcessOrderHanger.TryGetValue(tenHangerNo?.Trim(), out p);
        //                    p.SiteNo = tenStatingNo;
        //                    p.MainTrackNumber = short.Parse(tenMainTrackNo);
        //                    //【获取下一站】
        //                    OutSiteService.Instance.GetHangerNextSiteExt(p, flowIndex, ref nextStatingNo, ref outMainTrackNumber, ref hpFlowChart, ref pfcFlowRelation, isStoreStatingOutSite);
        //                    hpFlowChart = nextPPChart;
        //                }
        //            }
        //            //更新本站工序
        //            ppChart.OutSiteDate = DateTime.Now;
        //            ppChart.FlowRealyProductStatingNo = short.Parse(tenStatingNo);
        //            ppChart.IsFlowSucess = true;
        //            ppChart.Status = HangerProductFlowChartStaus.Successed.Value;//生产完成

        //            //合并工序处理
        //            MergeProcessFlowChartFlowHanlder(ppChart, ref hangerProcessFlowChartList);


        //            var isBridgeOutSite = IsBridgeOutSite(tenMainTrackNo, tenStatingNo);
        //            if (outMainTrackNumber != 0 && outMainTrackNumber != int.Parse(tenMainTrackNo))
        //            {
        //                isBridgeRequire = true;
        //                var mainTrackNumber = int.Parse(tenMainTrackNo);
        //                var statingNo = int.Parse(tenStatingNo);
        //                var hangerNo = int.Parse(tenHangerNo);
        //                //下一站需要经过桥接，且桥接站在工艺图上，且桥接站和下一站相同

        //                //桥接验证
        //                BridgeActionHandler(mainTrackNumber, statingNo, outMainTrackNumber, nextStatingNo, hangerNo, ref aMainTracknumberBridgeStatingIsInFlowChart, ref bMainTrackNumberBridgeStatingIsInFlowChart, ref nextStatingPassBridgeStatingAndBridgeStatingInFlowChart);
        //                //下一站是桥接站且在工艺图上（）
        //                if (nextStatingPassBridgeStatingAndBridgeStatingInFlowChart)
        //                {
        //                    nextPPChart = hangerProcessFlowChartList.Where(f => f.MainTrackNumber.Value == mainTrackNumber && f.StatingNo.Value == statingNo).First();
        //                }
        //            }

        //            #region//存储站出战恢复站点状态
        //            if (isStoreStatingOutSite)
        //            {
        //                hangerProcessFlowChartList.Where(f => f.MainTrackNumber.Value == ppChart.MainTrackNumber.Value &&
        //                  f.StatingNo.Value == ppChart.StatingNo.Value).ToList().ForEach(delegate (HangerProductFlowChartModel fc)
        //                  {
        //                      fc.AllocationedDate = null;
        //                      fc.IncomeSiteDate = null;
        //                      fc.OutSiteDate = null;
        //                      fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
        //                      fc.IsFlowSucess = false;
        //                      fc.IsStorageStatingOutSite = true;
        //                  });
        //                //foreach (var fc in wHangerProcessChartList)
        //                //{
        //                //    fc.AllocationedDate = null;
        //                //    fc.IncomeSiteDate = null;
        //                //    fc.OutSiteDate = null;
        //                //    fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
        //                //    fc.IsFlowSucess = false;
        //                //    fc.IsStorageStatingOutSite = true;
        //                //}
        //                //  SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[ppChart.HangerNo] = hangerProcessFlowChartLis;

        //            }
        //            else
        //            {
        //                //刷新同工序的存储站点的状态
        //                var fIndex = ppChart.FlowIndex.Value;
        //                hangerProcessFlowChartList.Where(f => f.MainTrackNumber.Value == ppChart.MainTrackNumber.Value &&
        //                 f.FlowIndex.Value == fIndex && null != f.StatingRoleCode && f.StatingRoleCode.Equals(StatingType.StatingStorage.Code)).ToList().ForEach(delegate (HangerProductFlowChartModel fc)
        //                 {
        //                     fc.AllocationedDate = null;
        //                     fc.IncomeSiteDate = null;
        //                     fc.OutSiteDate = null;
        //                     fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
        //                     fc.IsFlowSucess = false;
        //                     fc.IsStorageStatingOutSite = false;
        //                 });
        //                //foreach (var fc in wHangerProcessChartList)
        //                //{
        //                //    fc.AllocationedDate = null;
        //                //    fc.IncomeSiteDate = null;
        //                //    fc.OutSiteDate = null;
        //                //    fc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
        //                //    fc.IsFlowSucess = false;
        //                //    fc.IsStorageStatingOutSite = false;
        //                //}
        //                //SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[ppChart.HangerNo] = hangerProcessFlowChartLis;
        //            }
        //            #endregion

        //            //【衣架生产履历】本站衣架生产履历Cache写入
        //            RecordHangerOutSiteProductResume(tenMainTrackNo, tenStatingNo, tenHangerNo, ppChart);
        //            //非桥接或者需要桥接，且桥接都在工艺图
        //            if (!isBridgeRequire || nextStatingPassBridgeStatingAndBridgeStatingInFlowChart)
        //            {
        //                //【衣架生产履历】下一站分配Cache写入
        //                RecordHangerNextSatingAllocationResume(tenMainTrackNo, tenStatingNo, tenHangerNo, nextPPChart, nextStatingNo);
        //            }


        //            //   NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[tenHangerNo] = hangerProcessFlowChartList;
        //            NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(tenHangerNo, hangerProcessFlowChartList);

        //            //非桥接或者需要桥接，且桥接都在工艺图
        //            if (!isBridgeRequire || nextStatingPassBridgeStatingAndBridgeStatingInFlowChart)
        //            {
        //                //发布衣架下一站工序状态
        //                CorrectHangerNextFlowHandler(tenHangerNo, nextPPChart);
        //            }

        //            //**************************
        //            //【衣架站点工艺图db写入】【记录衣架工序及站点信息】
        //            //【记录产量】
        //            RecordEmployeeFlowYieldHandler(tenMainTrackNo, tenStatingNo, tenHangerNo, hangerProcessFlowChartList, ppChart, nextStatingNo);

        //            //更新等待衣架缓存
        //            CorrectWaitForHangerCache(tenHangerNo, nextStatingNo, hpFlowChart);

        //            //生产完成，对站内数和分配数更新到缓存
        //            if (string.IsNullOrEmpty(nextStatingNo))
        //            {
        //                var isHangingPieceStating = new ProductsQueryServiceImpl().isHangingPiece(null, tenStatingNo, tenMainTrackNo);
        //                if (!isHangingPieceStating)
        //                {
        //                    ////出站站内数-1
        //                    //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = int.Parse(mainTrackNo), StatingNo = int.Parse(statingNo), OnLineSum = -1 };
        //                    //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
        //                    //最后一道工序出战站内数及分配标识修正
        //                    LastFlowStatingHandler(tenMainTrackNo, tenStatingNo, tenHangerNo, ppChart);
        //                }

        //                return;
        //            }
        //            //更新衣架分配记录为处理状态到缓存
        //            //讲当前出战衣架的分配记录的出战时间和工序完成状态
        //            List<DaoModel.HangerStatingAllocationItem> dicHangerStatingALloList = CorrectCurrentFlowAllotcationStatusToComplete(tenMainTrackNo, tenStatingNo, tenHangerNo, ppChart);

        //            var nextFlowIndex = 0;
        //            var nextHangerFlowChartList = hangerProcessFlowChartList.Where(f => f.FlowIndex.Value == flowIndex && f.StatingNo.Value == short.Parse(nextStatingNo) && (f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value || f.Status.Value == HangerProductFlowChartStaus.Producting.Value)).ToList<HangerProductFlowChartModel>();
        //            if (nextHangerFlowChartList.Count > 0)
        //            {
        //                //非桥接或者需要桥接，且桥接都在工艺图
        //                if (!isBridgeRequire || nextStatingPassBridgeStatingAndBridgeStatingInFlowChart)
        //                {
        //                    HangerProductFlowChartModel nextHangerFlowChart;
        //                    DaoModel.HangerStatingAllocationItem nextHangerStatingAllocationItem;
        //                    //修正下一道工序的站点分配cache及db维护
        //                    CorrectNextFlowAllocationCache(tenMainTrackNo, tenStatingNo, tenHangerNo, ppChart, nextPPChart, flowIndex, currentFlowIndex, nextStatingNo, dicHangerStatingALloList, out nextFlowIndex, nextHangerFlowChartList, out nextHangerFlowChart, out nextHangerStatingAllocationItem, isBridgeOutSite);

        //                    //记录衣架分配
        //                    var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
        //                    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

        //                    // //修正删除的站内数及明细、缓存
        //                    CorrectStatingNumAndCacheByAllocation(tenHangerNo, outMainTrackNumber, nextStatingNo, nextHangerFlowChart);
        //                }

        //                //修正出战 站点的分配数，站内数
        //                CorrectStatingNumAndCacheByOutSite(tenMainTrackNo, tenStatingNo, tenHangerNo, ppChart);

        //            }
        //            nextFlowStatingNo = nextStatingNo;

        //            //非桥接或者需要桥接，且桥接都在工艺图
        //            if (!isBridgeRequire || nextStatingPassBridgeStatingAndBridgeStatingInFlowChart)
        //            {
        //                //更新【衣架工艺图】衣架下一站的站点分配状态和时间
        //                if (!string.IsNullOrEmpty(nextStatingNo))
        //                    CorrectNextFlowAllocationStatusToCacheByAllocationSucess(tenHangerNo, outMainTrackNumber, IsStorageStatingAgainAllocationedSeamsStating, nextStatingNo, nextFlowIndex);
        //            }
        //        }
        public SuspeSys.Domain.BridgeSet GetReverseBridge(string tenMainTrackNo, string tenStatingNo)
        {
            var mainTrackNumber = int.Parse(tenMainTrackNo);
            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(mainTrackNumber))
            {
                var ex = new ApplicationException($"无桥接信息!主轨:{tenMainTrackNo} 站点:{tenStatingNo}");
                tcpLogError.Error(ex);
                throw ex;
            }
            if (!dicBridgeCache.ContainsKey(dicBridgeCache[mainTrackNumber].BMainTrackNumber.Value))
            {
                var ex = new ApplicationException($"无桥接信息!主轨:{dicBridgeCache[mainTrackNumber].BMainTrackNumber.Value}");
                tcpLogError.Error(ex);
                throw ex;
            }
            var outStatingMaintrackNumberBridge = dicBridgeCache[dicBridgeCache[mainTrackNumber].BMainTrackNumber.Value];
            return outStatingMaintrackNumberBridge;
        }
        public bool IsBridgeOutSite(string tenMainTrackNo, string tenStatingNo)
        {
            lock (locObject)
            {
                var mainTrackNumber = int.Parse(tenMainTrackNo);
                var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
                if (!dicBridgeCache.ContainsKey(mainTrackNumber))
                {
                    return false;
                }
                var outStatingMaintrackNumberBridge = dicBridgeCache[mainTrackNumber];
                if (mainTrackNumber == outStatingMaintrackNumberBridge.AMainTrackNumber.Value && int.Parse(tenStatingNo) == outStatingMaintrackNumberBridge.ASiteNo.Value)
                    return true;

                return false;
            }
        }
        private bool IsBridgeOutSiteAndInFlowChart(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, ref int currentIndex, ref bool isBSiteInFlowChart,
            ref DaoModel.BridgeSet bBirdeSet, ref DaoModel.BridgeSet aBirdeSet)
        {
            var mainTrackNumber = int.Parse(tenMainTrackNo);
            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(mainTrackNumber))
            {
                return false;
            }
            var outStatingMaintrackNumberBridge = dicBridgeCache[mainTrackNumber];
            if (mainTrackNumber != outStatingMaintrackNumberBridge.AMainTrackNumber.Value && int.Parse(tenStatingNo) != outStatingMaintrackNumberBridge.ASiteNo.Value)
                return false;

            // var nextStatingMaintrackBridge = dicBridgeCache[outMainTrackNumber];
            //var dicHangerFlowChartCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(tenHangerNo))//dicHangerFlowChartCache.ContainsKey(tenHangerNo))
            {
                return false;
            }
            var hangerFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo); //dicHangerFlowChartCache[tenHangerNo];
            //b站桥接站
            bBirdeSet = dicBridgeCache[outStatingMaintrackNumberBridge.BMainTrackNumber.Value];
            var bbBridgeSet = bBirdeSet;
            aBirdeSet = outStatingMaintrackNumberBridge;
            //b站桥接站是否在工艺图上
            isBSiteInFlowChart = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == bbBridgeSet.ASiteNo.Value && f.MainTrackNumber.Value == bbBridgeSet.AMainTrackNumber.Value && f.Status.Value != 2).Count() > 0;
            var isBridgeOutSiteAndInFlowChart = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value && f.MainTrackNumber.Value == mainTrackNumber && f.Status.Value != 2).Count() > 0;
            if (isBridgeOutSiteAndInFlowChart)
            {
                currentIndex = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value && f.MainTrackNumber.Value == mainTrackNumber && f.Status.Value != 2)
                    .Select(ff => ff.FlowIndex.Value).Min();
            }
            return isBridgeOutSiteAndInFlowChart;
        }

        /// <summary>
        /// 桥接处理
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextFlowStatingNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="aMainTracknumberBridgeStatingIsInFlowChart"></param>
        /// <param name="bMainTrackNumberBridgeStatingIsInFlowChart"></param>
        private void BridgeActionHandler(int mainTrackNumber, int statingNo, int outMainTrackNumber, string nextFlowStatingNo, int hangerNo,
            ref bool aMainTracknumberBridgeStatingIsInFlowChart, ref bool bMainTrackNumberBridgeStatingIsInFlowChart,
            ref bool nextStatingPassBridgeStatingAndBridgeStatingInFlowChart)
        {
            var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
            if (!dicBridgeCache.ContainsKey(mainTrackNumber) || !dicBridgeCache.ContainsKey(outMainTrackNumber))
            {
                var exNonFoundBridgeSet = new ApplicationException(string.Format("无桥接配置不能桥接!请检查桥接设置。衣架号:{0} 从主轨{1}的站点{2} --->{3}主轨", hangerNo, mainTrackNumber, statingNo, outMainTrackNumber));
                tcpLogError.Error(exNonFoundBridgeSet);
                throw exNonFoundBridgeSet;
            }
            var outStatingMaintrackNumberBridge = dicBridgeCache[mainTrackNumber];
            //if (!string.IsNullOrEmpty(nextFlowStatingNo)) {
            //    if (int.Parse(nextFlowStatingNo) == outStatingMaintrackNumberBridge.ASiteNo.Value) {
            //        nextStatingPassBridgeStatingAndBridgeStatingInFlowChart = true;
            //    }
            //}
            // var nextStatingMaintrackBridge = dicBridgeCache[outMainTrackNumber];
            // var dicHangerFlowChartCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo + ""))//dicHangerFlowChartCache.ContainsKey(hangerNo.ToString()))
            {
                var exNonFoundBridgeSet = new ApplicationException(string.Format("非法衣架。衣架号:{0} 从主轨{1}的站点{2} --->{3}主轨", hangerNo, mainTrackNumber, statingNo, outMainTrackNumber));
                tcpLogError.Error(exNonFoundBridgeSet);
                throw exNonFoundBridgeSet;
            }
            var hangerFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo + "");//dicHangerFlowChartCache[hangerNo.ToString()];
            aMainTracknumberBridgeStatingIsInFlowChart = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value && f.MainTrackNumber.Value == mainTrackNumber).Count() > 0;
            bMainTrackNumberBridgeStatingIsInFlowChart = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.BSiteNo.Value && f.MainTrackNumber.Value == outMainTrackNumber).Count() > 0;
            nextStatingPassBridgeStatingAndBridgeStatingInFlowChart = hangerFlowChartList.Where(f => f.StatingNo != null
             && f.StatingNo.Value == outStatingMaintrackNumberBridge.BSiteNo.Value
             && f.StatingNo.Value == int.Parse(nextFlowStatingNo)
             && f.MainTrackNumber.Value == outStatingMaintrackNumberBridge.BMainTrackNumber.Value
             && outStatingMaintrackNumberBridge.BMainTrackNumber.Value == outMainTrackNumber
             && f.MainTrackNumber.Value == outMainTrackNumber).Count() > 0;
        }



        /// <summary>
        /// 更新等待衣架缓存
        /// </summary>
        /// <param name="tenHangerNo"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="hpFlowChart"></param>
        public void CorrectWaitForHangerCache(string tenHangerNo, string nextStatingNo, HangerProductFlowChartModel hpFlowChart)
        {
            lock (locObject)
            {
                if (!string.IsNullOrEmpty(nextStatingNo) && hpFlowChart != null)
                {
                    var dicWaitProcessOrderHanger = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DaoModel.WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER);
                    if (dicWaitProcessOrderHanger.ContainsKey(tenHangerNo))
                    {
                        dicWaitProcessOrderHanger[tenHangerNo].FlowIndex = (short)hpFlowChart.FlowIndex;
                        dicWaitProcessOrderHanger[tenHangerNo].FlowNo = hpFlowChart.FlowNo;
                        dicWaitProcessOrderHanger[tenHangerNo].ProcessFlowCode = hpFlowChart.FlowCode;
                        dicWaitProcessOrderHanger[tenHangerNo].ProcessFlowId = hpFlowChart.FlowId;
                        dicWaitProcessOrderHanger[tenHangerNo].ProcessFlowName = hpFlowChart.FlowName;
                        dicWaitProcessOrderHanger[tenHangerNo] = dicWaitProcessOrderHanger[tenHangerNo];
                    }
                }
            }
        }

        private static readonly object locObject = new object();

        /// <summary>
        /// 修正桥接站逆向站点内数
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="tenHangerNo"></param>
        public void CorrectBridgeInStatingNum(string tenMainTrackNo, string tenStatingNo, int outMainTrackNumber, string tenHangerNo)
        {
            lock (locObj)
            {
                if (string.IsNullOrEmpty(tenMainTrackNo) || string.IsNullOrEmpty(tenMainTrackNo) || outMainTrackNumber == 0)
                {
                    return;
                }
                //出战站的主轨与下一站是不同主轨
                if (int.Parse(tenMainTrackNo) != outMainTrackNumber)
                {
                    return;
                }
                var isBridge = IsBridge(int.Parse(tenMainTrackNo), int.Parse(tenStatingNo));
                if (!isBridge) return;//非桥接站
                                      //var dicHangerFlowChartCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                                      //if (!dicHangerFlowChartCache.ContainsKey(tenHangerNo.ToString()))
                                      //{
                                      //    return;
                                      //}
                var dicBridgeCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<int, SuspeSys.Domain.BridgeSet>(SusRedisConst.BRIDGE_SET);
                var outStatingMaintrackNumberBridge = dicBridgeCache[int.Parse(tenMainTrackNo)];

                var hangerFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo);// dicHangerFlowChartCache[tenHangerNo.ToString()];
                var nonExistA = hangerFlowChartList.Where(f => f.StatingNo != null && f.StatingNo.Value == outStatingMaintrackNumberBridge.ASiteNo.Value
                && f.MainTrackNumber.Value == int.Parse(tenMainTrackNo)).Count() == 0;
                if (nonExistA)
                {//不在工艺图上
                 //修正删除的站内数及明细、缓存
                    var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                    hnssoc.Action = 14;
                    hnssoc.HangerNo = tenHangerNo.ToString();
                    hnssoc.MainTrackNumber = outStatingMaintrackNumberBridge.BMainTrackNumber.Value;
                    hnssoc.StatingNo = outStatingMaintrackNumberBridge.BSiteNo.Value;

                    var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                    // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                    NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
                    //修正删除的站内数及明细、缓存
                    var outSiteMainTrackNumberBridgeNotInFlowChart = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                    outSiteMainTrackNumberBridgeNotInFlowChart.Action = 9;
                    outSiteMainTrackNumberBridgeNotInFlowChart.HangerNo = tenHangerNo.ToString();
                    outSiteMainTrackNumberBridgeNotInFlowChart.MainTrackNumber = outStatingMaintrackNumberBridge.AMainTrackNumber.Value;
                    outSiteMainTrackNumberBridgeNotInFlowChart.StatingNo = outStatingMaintrackNumberBridge.ASiteNo.Value;

                    var hnssocJsonOutSiteMainTrackNumberBridgeNotInFlowChart = Newtonsoft.Json.JsonConvert.SerializeObject(outSiteMainTrackNumberBridgeNotInFlowChart);
                    //  NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJsonOutSiteMainTrackNumberBridgeNotInFlowChart);
                    NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJsonOutSiteMainTrackNumberBridgeNotInFlowChart);

                    //修正本站站内数
                }
                else if (isBridge && !nonExistA)//返工路过桥接出战
                {
                    // 修正删除的站内数及明细、缓存
                    var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                    hnssoc.Action = 14;
                    hnssoc.HangerNo = tenHangerNo.ToString();
                    hnssoc.MainTrackNumber = outStatingMaintrackNumberBridge.BMainTrackNumber.Value;
                    hnssoc.StatingNo = outStatingMaintrackNumberBridge.BSiteNo.Value;

                    var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                    // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                    NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
                }
            }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 出战时针对下一站是返工的衣架的处理
        /// </summary>
        /// <param name="tenHangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="hangerProcessFlowChartLis"></param>
        /// <param name="ppChart"></param>
        /// <param name="nextPPChart"></param>
        /// <param name="flowIndex"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="hpFlowChart"></param>
        private static void ReworkHandlerByOutSiteRequest(string tenHangerNo, ref int outMainTrackNumber, List<HangerProductFlowChartModel> hangerProcessFlowChartLis, HangerProductFlowChartModel ppChart, HangerProductFlowChartModel nextPPChart, out int flowIndex, ref string nextStatingNo, out HangerProductFlowChartModel hpFlowChart)
        {
            //下道工序是否存在返工
            var nextFlowIsReworkFlow = hangerProcessFlowChartLis.Where(k => k.FlowIndex.Value == nextPPChart.FlowIndex.Value &&
             k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 1).Count() > 0;
            if (!nextFlowIsReworkFlow)//不存在直接找发起返工的源头站点
            {


                //找返工发起站点
                var reworkFlowStatlist = hangerProcessFlowChartLis.Where(k => k.FlowIndex.Value == nextPPChart.FlowIndex.Value &&
                k.StatingNo.Value == ppChart.ReworkStatingNo.Value && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && (null != k.IsReceivingHangerStating && k.IsReceivingHangerStating.Value)
                && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
                ).Select(f => new ProductsProcessOrderModel()
                {
                    StatingNo = f.StatingNo.ToString(),
                    MainTrackNumber = (int)f.MainTrackNumber,
                    StatingCapacity = f.StatingCapacity.Value,
                    Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                    ProcessChartId = f.ProcessChartId,
                    FlowNo = f.FlowNo,
                    StatingRoleCode = f.StatingRoleCode

                }).ToList<ProductsProcessOrderModel>();
                if (reworkFlowStatlist.Count == 0)
                {
                    //下一道没有可以接收衣架的站
                    throw new NoFoundStatingException(string.Format("【返工衣架出战异常】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", ppChart.MainTrackNumber, ppChart.StatingNo, tenHangerNo?.Trim()))
                    {
                        FlowNo = nextPPChart?.FlowNo?.Trim()
                    };
                }
                //【获取下一站】
                OutSiteService.Instance.AllocationNextProcessFlowStating(reworkFlowStatlist, ref outMainTrackNumber, ref nextStatingNo);
                hpFlowChart = nextPPChart;
                flowIndex = nextPPChart.FlowIndex.Value;
            }
            else
            {

                //找下一返工站点
                var reworkFlowStatlist = hangerProcessFlowChartLis.Where(k => k.FlowIndex.Value == nextPPChart.FlowIndex.Value &&
                k.StatingNo != null && k.StatingNo.Value != -1 && k.Status.Value != HangerProductFlowChartStaus.Successed.Value && k.FlowType.Value == 1
                && (null != k.IsReceivingHanger && k.IsReceivingHanger.Value == 1)
                ).Select(f => new ProductsProcessOrderModel()
                {
                    StatingNo = f.StatingNo.ToString(),
                    MainTrackNumber = (int)f.MainTrackNumber,
                    StatingCapacity = f.StatingCapacity.Value,
                    Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                    ProcessChartId = f.ProcessChartId,
                    FlowNo = f.FlowNo,
                    StatingRoleCode = f.StatingRoleCode

                }).ToList<ProductsProcessOrderModel>();
                if (reworkFlowStatlist.Count == 0)
                {
                    //下一道没有可以接收衣架的站
                    throw new NoFoundStatingException(string.Format("【返工衣架出战异常】主轨:{0} 站点:{1} 衣架号:{2} 找不到下一站!", nextPPChart.MainTrackNumber, nextPPChart.StatingNo, tenHangerNo?.Trim()))
                    {
                        FlowNo = nextPPChart?.FlowNo?.Trim()
                    };
                }
                //【获取下一站】
                OutSiteService.Instance.AllocationNextProcessFlowStating(reworkFlowStatlist, ref outMainTrackNumber, ref nextStatingNo);
                hpFlowChart = nextPPChart;
                flowIndex = nextPPChart.FlowIndex.Value;
            }
        }
        /// <summary>
        /// 更新衣架站点生产信息
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        private static void CorrectHangerProductItemToCacheByOutSite(string tenMainTrackNo, string tenStatingNo, string tenHangerNo)
        {
            var hpList = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM);
            if (hpList.ContainsKey(tenHangerNo))
            {
                var hangerHangerProductList = hpList[tenHangerNo];
                foreach (var f in hangerHangerProductList)
                {
                    if (f.HangerNo.Equals(tenHangerNo) && f.SiteNo.Equals(tenStatingNo) && (f.IsSucessedFlow == null || !f.IsSucessedFlow.Value))
                    {
                        f.IsSucessedFlow = true;
                        f.OutSiteDate = DateTime.Now;

                        //缓存站点生产记录
                        var dicStatingHangerProductItemCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.StatingHangerProductItem>>(SusRedisConst.STATING_HANGER_PRODUCT_ITEM); //STATING_HANGER_PRODUCT_ITEM
                        var sKey = string.Format("{0}:{1}", tenMainTrackNo, tenStatingNo);
                        if (!dicStatingHangerProductItemCache.ContainsKey(sKey))
                        {
                            var shpItem = BeanUitls<DaoModel.StatingHangerProductItem, DaoModel.HangerProductItemModel>.Mapper(f);
                            dicStatingHangerProductItemCache.Add(sKey, new List<DaoModel.StatingHangerProductItem>() { shpItem });
                        }
                        else
                        {
                            var shpItem = BeanUitls<DaoModel.StatingHangerProductItem, DaoModel.HangerProductItem>.Mapper(f);
                            var list = dicStatingHangerProductItemCache[sKey];
                            list.Add(shpItem);
                            dicStatingHangerProductItemCache[sKey] = list;
                        }
                    }
                }
                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerProductItemModel>>(SusRedisConst.HANGER_PRODUCT_ITEM)[tenHangerNo] = hangerHangerProductList;
                //NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(tenHangerNo, hangerHangerProductList);
            }
        }

        /// <summary>
        /// 更新【衣架工艺图】衣架下一站的站点分配状态和时间
        /// </summary>
        /// <param name="tenHangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="IsStorageStatingAgainAllocationedSeamsStating"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="nextFlowIndex"></param>
        /// <returns></returns>
        public static StackExchange.Redis.DataTypes.Collections.RedisDictionary<string, List<HangerProductFlowChartModel>> CorrectNextFlowAllocationStatusToCacheByAllocationSucess(string tenHangerNo, int outMainTrackNumber, bool IsStorageStatingAgainAllocationedSeamsStating, string nextStatingNo, int nextFlowIndex)
        {
            //StackExchange.Redis.DataTypes.Collections.RedisDictionary<string, List<HangerProductFlowChartModel>> dicHangerProcessFlowChart;
            {
                // dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                if (!NewCacheService.Instance.HangerIsContainsFlowChart(tenHangerNo))//dicHangerProcessFlowChart.ContainsKey(tenHangerNo))
                {
                    tcpLogError.ErrorFormat("衣架:{0} 找不到生产工艺图缓存! 站点:{1} 主轨:{2}", tenHangerNo, nextStatingNo, outMainTrackNumber);
                }
                else
                {
                    var nStatingNo = nextStatingNo;
                    var hangerFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo);// dicHangerProcessFlowChart[tenHangerNo];
                    foreach (var hfc in hangerFlowChartList)
                    {
                        if (hfc.FlowIndex.Value == nextFlowIndex && nextStatingNo.Equals(hfc.StatingNo.Value.ToString()) && null != hfc.StatingNo && hfc.Status.Value == 0)
                        {
                            hfc.isAllocationed = true;
                            hfc.AllocationedDate = DateTime.Now;
                            hfc.IsStorageStatingAgainAllocationedSeamsStating = IsStorageStatingAgainAllocationedSeamsStating;
                            // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[tenHangerNo] = hangerFlowChartList;
                            NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(tenHangerNo, hangerFlowChartList);
                            break;
                        }
                    }
                }
            }

            return NewCacheService.Instance.GetHangerFlowChartListForRedisCache();
        }
        /// <summary>
        /// 修正出战 站点的分配数，站内数
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="ppChart"></param>
        public void CorrectStatingNumAndCacheByOutSite(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, HangerProductFlowChartModel ppChart)
        {
            lock (locObject)
            {
                var hnssocCurrent = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssocCurrent.Action = 2;
                hnssocCurrent.HangerNo = tenHangerNo;
                hnssocCurrent.MainTrackNumber = int.Parse(tenMainTrackNo);
                hnssocCurrent.StatingNo = int.Parse(tenStatingNo);
                hnssocCurrent.FlowNo = ppChart.FlowNo;
                hnssocCurrent.FlowIndex = ppChart.FlowIndex.Value;
                hnssocCurrent.HangerProductFlowChartModel = ppChart;
                var hnssocCurrentJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocCurrent);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocCurrentJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocCurrentJson);
            }
        }
        /// <summary>
        /// 修正出战 站点的分配数，站内数
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="ppChart"></param>
        private static void CorrectStatingNumAndCacheByOutSiteByBirdge(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, HangerProductFlowChartModel ppChart)
        {
            var hnssocCurrent = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssocCurrent.Action = 3;
            hnssocCurrent.HangerNo = tenHangerNo;
            hnssocCurrent.MainTrackNumber = int.Parse(tenMainTrackNo);
            hnssocCurrent.StatingNo = int.Parse(tenStatingNo);
            hnssocCurrent.FlowNo = ppChart.FlowNo;
            hnssocCurrent.FlowIndex = ppChart.FlowIndex.Value;
            hnssocCurrent.HangerProductFlowChartModel = ppChart;
            var hnssocCurrentJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocCurrent);
            //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocCurrentJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocCurrentJson);
            //站内数-1
            var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = int.Parse(tenMainTrackNo), StatingNo = int.Parse(tenStatingNo), OnLineSum = -1 };
            //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
            NewSusRedisClient.Instance.UpdateMainTrackStatingInNumAction(new StackExchange.Redis.RedisChannel(), JsonConvert.SerializeObject(inNumModel));
        }
        /// <summary>
        /// 修正已分配站点的分配数，站内数
        /// </summary>
        /// <param name="tenHangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="nextHangerFlowChart"></param>
        public static void CorrectStatingNumAndCacheByAllocation(string tenHangerNo, int outMainTrackNumber, string nextStatingNo, HangerProductFlowChartModel nextHangerFlowChart)
        {
            var hnssocNext = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssocNext.Action = 0;
            hnssocNext.HangerNo = tenHangerNo;
            hnssocNext.MainTrackNumber = outMainTrackNumber;
            hnssocNext.StatingNo = int.Parse(nextStatingNo);
            hnssocNext.FlowNo = nextHangerFlowChart.FlowNo;
            hnssocNext.FlowIndex = nextHangerFlowChart.FlowIndex.Value;
            hnssocNext.HangerProductFlowChartModel = nextHangerFlowChart;
            var hnssocNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocNext);
            // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocNextJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocNextJson);
        }
        /// <summary>
        /// 修正下一道工序的站点分配cache及db维护
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="ppChart"></param>
        /// <param name="nextPPChart"></param>
        /// <param name="flowIndex"></param>
        /// <param name="currentFlowIndex"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="dicHangerStatingALloList"></param>
        /// <param name="nextFlowIndex"></param>
        /// <param name="nextHangerFlowChartList"></param>
        /// <param name="nextHangerFlowChart"></param>
        /// <param name="nextHangerStatingAllocationItem"></param>
        public void CorrectNextFlowAllocationCache(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, HangerProductFlowChartModel ppChart,
            HangerProductFlowChartModel nextPPChart, int flowIndex, int currentFlowIndex, string nextStatingNo,
            List<DaoModel.HangerStatingAllocationItem> dicHangerStatingALloList, out int nextFlowIndex, List<HangerProductFlowChartModel> nextHangerFlowChartList,
            out HangerProductFlowChartModel nextHangerFlowChart, out DaoModel.HangerStatingAllocationItem nextHangerStatingAllocationItem, bool isBridgeOutSite)
        {
            nextHangerFlowChart = nextHangerFlowChartList[0];
            nextFlowIndex = nextHangerFlowChart.FlowIndex.Value;
            nextHangerStatingAllocationItem = new DaoModel.HangerStatingAllocationItem();
            nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
            nextHangerStatingAllocationItem.FlowIndex = (short)flowIndex;
            nextHangerStatingAllocationItem.SiteNo = tenStatingNo;
            nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
            nextHangerStatingAllocationItem.HangerNo = tenHangerNo;
            nextHangerStatingAllocationItem.NextSiteNo = nextStatingNo;
            nextHangerStatingAllocationItem.OutMainTrackNumber = int.Parse(tenMainTrackNo);
            nextHangerStatingAllocationItem.FlowNo = nextHangerFlowChart.FlowNo;
            nextHangerStatingAllocationItem.ProcessFlowCode = nextHangerFlowChart.FlowCode;
            nextHangerStatingAllocationItem.ProcessFlowName = nextHangerFlowChart.FlowName;
            nextHangerStatingAllocationItem.HangerType = nextHangerFlowChart.FlowType;
            //控制出站产量推送方式(返工还是正常)
            nextHangerStatingAllocationItem.IsReturnWorkFlow = null != ppChart.FlowType ? ppChart.FlowType.Value == (byte)1 : false;
            nextHangerStatingAllocationItem.ProcessFlowId = nextHangerFlowChart.FlowId;
            nextHangerStatingAllocationItem.MainTrackNumber = nextHangerFlowChart.MainTrackNumber.Value;
            nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
            nextHangerStatingAllocationItem.FlowChartd = nextHangerFlowChart.ProcessChartId;
            nextHangerStatingAllocationItem.ProductsId = nextHangerFlowChart.ProductsId;
            nextHangerStatingAllocationItem.PSize = nextHangerFlowChart.PSize;
            nextHangerStatingAllocationItem.PColor = nextHangerFlowChart.PColor;
            nextHangerStatingAllocationItem.ProcessOrderNo = nextHangerFlowChart.ProcessOrderNo;
            nextHangerStatingAllocationItem.StyleNo = nextPPChart.StyleNo;
            nextHangerStatingAllocationItem.LastFlowIndex = currentFlowIndex;
            nextHangerStatingAllocationItem.IsStatingStorageOutStating = IsStoreStating(ppChart);
            nextHangerStatingAllocationItem.IsBridgeStatingOutStatingAllocate = isBridgeOutSite;
            dicHangerStatingALloList.Add(nextHangerStatingAllocationItem);
            nextHangerStatingAllocationItem.BatchNo = BridgeService.Instance.GetBatchNo(tenHangerNo);
            nextHangerStatingAllocationItem.GroupNo = BridgeService.Instance.GetGroupNo(int.Parse(tenMainTrackNo), int.Parse(nextStatingNo));
            // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo] = dicHangerStatingALloList;
            NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(tenHangerNo, dicHangerStatingALloList);

            //发布 待生产衣架信息
            var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
            NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);
        }

        public static List<DaoModel.HangerStatingAllocationItem> CorrectCurrentFlowAllotcationStatusToComplete(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, HangerProductFlowChartModel ppChart)
        {
            var dicHangerStatingALloList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(tenHangerNo); //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo];

            foreach (var item in dicHangerStatingALloList)
            {
                if (tenStatingNo.Equals(item.NextSiteNo) && item.MainTrackNumber.Value == short.Parse(tenMainTrackNo))
                {
                    item.IsSucessedFlow = true;
                    item.Status = (byte)HangerStatingAllocationItemStatus.Successed.Value;
                    item.HangerType = ppChart.FlowType;
                    item.OutSiteDate = DateTime.Now;
                }
            }
            //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo] = dicHangerStatingALloList;
            NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(tenHangerNo, dicHangerStatingALloList);
            return dicHangerStatingALloList;
        }
        /// <summary>
        /// 最后一道工序出战站内数及分配标识修正
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="ppChart"></param>
        public static void LastFlowStatingHandler(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, HangerProductFlowChartModel ppChart)
        {
            var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
            hnssoc.Action = 2;
            hnssoc.HangerNo = tenHangerNo;
            hnssoc.MainTrackNumber = int.Parse(tenMainTrackNo);
            hnssoc.StatingNo = int.Parse(tenStatingNo);
            hnssoc.FlowNo = ppChart.FlowNo;
            hnssoc.FlowIndex = ppChart.FlowIndex.Value;
            hnssoc.HangerProductFlowChartModel = ppChart;
            var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
            //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
            NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
            ////出站站点分配数-1
            //var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = int.Parse(mainTrackNo), StatingNo = int.Parse(statingNo), OnLineSum = -1 };
            //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));

            //更新衣架分配记录为处理状态到缓存
            var dicHangerStatingALloList1 = NewCacheService.Instance.GetHangerAllocationItemListForRedis(tenHangerNo); //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo];

            foreach (var item in dicHangerStatingALloList1)
            {
                if (tenStatingNo.Equals(item.NextSiteNo) && item.MainTrackNumber.Value == short.Parse(tenMainTrackNo))
                {
                    item.IsSucessedFlow = true;
                    item.Status = (byte)HangerStatingAllocationItemStatus.Successed.Value;
                    item.HangerType = ppChart.FlowType;
                    item.OutSiteDate = DateTime.Now;
                }
            }
            //  NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo] = dicHangerStatingALloList1;
            NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(tenHangerNo, dicHangerStatingALloList1);
        }

        /// <summary>
        /// 记录员工产量到cahe和db
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="hangerProcessFlowChartLis"></param>
        /// <param name="ppChart"></param>
        /// <param name="nextStatingNo"></param>
        public void RecordEmployeeFlowYieldHandler(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, List<HangerProductFlowChartModel> hangerProcessFlowChartLis, HangerProductFlowChartModel ppChart, string nextStatingNo)
        {
            lock (locObject)
            {
                var outSiteResult = new HangerOutSiteResult();
                outSiteResult.MainTrackNumber = int.Parse(tenMainTrackNo);
                outSiteResult.HangerNo = tenHangerNo;
                outSiteResult.StatingNo = tenStatingNo;
                outSiteResult.HangerProductFlowChart = ppChart;
                var outSiteJson = Newtonsoft.Json.JsonConvert.SerializeObject(outSiteResult);
                // SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_OUT_SITE_ACTION, outSiteJson);
                NewSusRedisClient.Instance.HangerOutSiteAction(new StackExchange.Redis.RedisChannel(), outSiteJson);

                //更新衣架工艺图缓存
                ppChart.NextStatingNo = !string.IsNullOrEmpty(nextStatingNo) ? short.Parse(nextStatingNo) : default(short?);
                //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[tenHangerNo] = hangerProcessFlowChartLis;
                NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(tenHangerNo, hangerProcessFlowChartLis);
            }
        }
        /// <summary>
        /// 当前衣架生产最新的工序--->修正衣架下一道工序状态到cache
        /// </summary>
        /// <param name="tenHangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextPPChart"></param>
        /// <param name="nextStatingNo"></param>
        public void CorrectHangerNextFlowHandler(string tenHangerNo, HangerProductFlowChartModel nextPPChart)
        {
            lock (locObject)
            {
                var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
                chpf.HangerNo = tenHangerNo;
                chpf.MainTrackNumber = nextPPChart.MainTrackNumber.Value;
                chpf.StatingNo = nextPPChart.StatingNo.Value;
                chpf.FlowNo = nextPPChart?.FlowNo;
                chpf.FlowIndex = nextPPChart == null ? 1 : nextPPChart.FlowIndex.Value;
                chpf.FlowType = null == nextPPChart?.FlowType ? 0 : nextPPChart.FlowType.Value;
                var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
                //  SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW)[tenHangerNo] = chpf;
                NewCacheService.Instance.UpdateCurrentHangerFlowCacheToRedis(tenHangerNo, chpf);
            }
            // NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);
        }
        /// <summary>
        /// 修正 下一站分配缓存
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="nextPPChart"></param>
        /// <param name="nextStatingNo"></param>
        public static void RecordHangerNextSatingAllocationResume(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, HangerProductFlowChartModel nextPPChart, string nextStatingNo)
        {
            var nextStatingHPResume = new HangerProductsChartResumeModel()
            {
                HangerNo = tenHangerNo,
                StatingNo = tenStatingNo,
                MainTrackNumber = int.Parse(tenMainTrackNo),
                HangerProductFlowChart = nextPPChart,
                Action = 1,
                NextStatingNo = nextStatingNo
            };
            var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
            // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
            NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
        }
        /// <summary>
        /// 修正 衣架生产履历
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="ppChart"></param>
        public void RecordHangerOutSiteProductResume(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, HangerProductFlowChartModel ppChart)
        {
            lock (locObject)
            {

                var hpResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = tenHangerNo,
                    StatingNo = tenStatingNo,
                    MainTrackNumber = int.Parse(tenMainTrackNo),
                    HangerProductFlowChart = ppChart,
                    Action = 3
                };
                var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hangerResumeJson);
            }
        }
        // private static readonly object locObject = new object();
        public void BridgeStatingNoToNextSatingHandler(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, ref string nextFlowStatingNo, ref int outMainTrackNumber, ref bool isFlowSucess, ref string info, bool isRedirect = false)
        {
            lock (locObject)
            {
                // var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                // var fccList = dicHangerProcessFlowChart[hangerNo];
                var hangerProcessFlowChartLis = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo);//dicHangerProcessFlowChart[tenHangerNo];
                var dicBridgeReworkHangerCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, ReworkModel>(SusRedisConst.BRIDGE_STATING_NEXT_STATING_ITEM);
                if (dicBridgeReworkHangerCache.ContainsKey(tenHangerNo))
                {
                    tcpLogInfo.InfoFormat("返工衣架桥接站出战处理开始");
                    var reworkModel = dicBridgeReworkHangerCache[tenHangerNo];
                    var nextStatingList = reworkModel.NextStatingList;

                    int mainTrackNumber = int.Parse(tenMainTrackNo);
                    int statingNo = int.Parse(tenStatingNo);
                    string nextStatingNo = null;
                    int hangerNo = int.Parse(tenHangerNo);
                    var nextFlowNo = reworkModel.ReworkFlow.FlowNo;
                    List<string> strFlowNoList2 = reworkModel.ReworkFlowNoList;
                    var outMainTrackNumber1 = outMainTrackNumber;

                    //检查工序是否完成
                    var nonSuccessCount = hangerProcessFlowChartLis.Where(f => f.FlowNo.Equals(nextStatingList.First().FlowNo) && f.MainTrackNumber.Value == outMainTrackNumber1 && f.StatingNo != null && f.StatingNo.Value == int.Parse(tenStatingNo)
                          && f.Status.Value != HangerProductFlowChartStaus.Successed.Value && f.FlowNo.Equals(nextFlowNo)).Count() > 0;
                    if (nonSuccessCount)
                    {
                        OutSiteService.Instance.AllocationNextProcessFlowStating(nextStatingList, ref outMainTrackNumber, ref nextFlowStatingNo);
                    }
                    else
                    {
                        ///
                        SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, ReworkModel>(SusRedisConst.BRIDGE_STATING_NEXT_STATING_ITEM).Remove(tenHangerNo);
                        BridgeStatingNoToNextSatingHandler(tenMainTrackNo, tenStatingNo, tenHangerNo, ref nextFlowStatingNo, ref outMainTrackNumber, ref isFlowSucess, ref info, true);
                        return;
                    }
                    nextStatingNo = nextFlowStatingNo;
                    if (outMainTrackNumber == 0 || string.IsNullOrEmpty(nextStatingNo))
                    {
                        var ex = new FlowNotFoundException(string.Format("【衣架返工桥接出战】 主轨:{0} 站点:{1}下一站主轨为0或者站点未找到! 下一站主轨:站点【{2}:{3}】 ", mainTrackNumber, statingNo, outMainTrackNumber, nextStatingNo));
                        tcpLogError.Error(ex);
                        throw ex;
                    }
                    if (int.Parse(tenMainTrackNo) != outMainTrackNumber)
                    {

                        var nextPPChart2 = hangerProcessFlowChartLis.Where(f => f.FlowNo.Equals(nextStatingList.First().FlowNo) && f.MainTrackNumber.Value == outMainTrackNumber1 && f.StatingNo != null && f.StatingNo.Value == int.Parse(tenStatingNo)
                        && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).First();
                        BridgeService.Instance.BridgeHandler(int.Parse(tenMainTrackNo), int.Parse(tenStatingNo), tenHangerNo, hangerProcessFlowChartLis, outMainTrackNumber, nextFlowStatingNo, nextPPChart2);
                        return;
                    }
                    int currentIndex = reworkModel.CurrentIndex;
                    List<HangerProductFlowChartModel> cpHangerProductFlowChartList = reworkModel.CpHangerProductFlowChartList;
                    List<HangerProductFlowChartModel> reworkResultHangerProductsFlowChartList = reworkModel.ReworkResultHangerProductsFlowChartList;
                    HangerProductFlowChartModel reworkFlow = reworkModel.ReworkFlow;
                    int flowIndex = reworkModel.FlowIndex;
                    SusReworkService.Instance.ReworkAction(mainTrackNumber, statingNo, nextStatingNo, hangerNo, outMainTrackNumber, strFlowNoList2, currentIndex,
                        cpHangerProductFlowChartList, reworkResultHangerProductsFlowChartList, reworkFlow, flowIndex, reworkModel.SourceRewokFlowChart);
                    SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, ReworkModel>(SusRedisConst.BRIDGE_STATING_NEXT_STATING_ITEM).Remove(tenHangerNo);
                    tcpLogInfo.InfoFormat("返工衣架桥接站出战处理结束");
                    LowerPlaceInstr.Instance.AllocationHangerToNextStating(tenHangerNo, nextFlowStatingNo + "", outMainTrackNumber);

                    var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new DaoModel.HangerStatingAllocationItem()
                    {
                        HangerNo = hangerNo + "",
                        MainTrackNumber = (short)outMainTrackNumber,
                        SiteNo = nextFlowStatingNo + ""
                         ,
                        AllocatingStatingDate = DateTime.Now
                    });
                    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);

                    return;
                }
                //var keyBridgeCache = string.Format("{0}:{1}:{2}", tenMainTrackNo, tenStatingNo, tenHangerNo);
                //var dicHangerBridgeOutSiteCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, HangerProductFlowChartModel>(SusRedisConst.BRIDGE_STATING_HANGER_OUT_SITE_RESUME);
                //if (dicHangerBridgeOutSiteCache.ContainsKey(keyBridgeCache))
                //{
                //    info = string.Format("重复出战! 主轨:{0} 站点:{1} 衣架号:{2}", tenMainTrackNo, tenStatingNo, tenHangerNo);
                //    tcpLogInfo.Info(info);
                //    isFlowSucess = true;
                //    return;
                //}

                var nextPPChartList = hangerProcessFlowChartLis.Where(f => f.FlowIndex.Value != 1
                && f.Status.Value != HangerProductFlowChartStaus.Successed.Value //currentFlowIndex
                && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
                && ((hangerProcessFlowChartLis.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
                || ((hangerProcessFlowChartLis.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
                && !(f.isAllocationed && IsBridge(f.MainTrackNumber.Value, f.StatingNo.Value)/*排除掉Bridge*/)
                ).OrderBy(f => f.FlowIndex).ToList<HangerProductFlowChartModel>();
                if (nextPPChartList.Count == 0)
                {
                    var ex = new ApplicationException("桥接站下一站无站点，请检查工艺图!");
                    tcpLogError.ErrorFormat("衣架号:{0} 错误:{1}", tenHangerNo, ex);
                    throw ex;
                }
                var nextFlowIndex = nextPPChartList.First().FlowIndex.Value;

                var nxtPPChartList = nextPPChartList.Where(k => k.FlowIndex.Value == nextFlowIndex).Select(f => new ProductsProcessOrderModel()
                {
                    StatingNo = f.StatingNo.ToString(),
                    MainTrackNumber = (int)f.MainTrackNumber,
                    StatingCapacity = f.StatingCapacity.Value,
                    Proportion = f.Proportion.HasValue ? f.Proportion.Value : 0,
                    ProcessChartId = f.ProcessChartId,
                    FlowNo = f.FlowNo,
                    StatingRoleCode = f.StatingRoleCode

                }).ToList<ProductsProcessOrderModel>();
                //if (nextPPChartList.Count != 0)
                //{//生产完成（返工时）

                //}
                //var nextPPChart = nextPPChartList.Count > 0 ? nextPPChartList.First() : null;
                //【获取下一站】
                OutSiteService.Instance.AllocationNextProcessFlowStating(nxtPPChartList, ref outMainTrackNumber, ref nextFlowStatingNo);
                var nextPPChart = nextPPChartList.First();
                nextPPChart.MainTrackNumber = (short)outMainTrackNumber;
                nextPPChart.StatingNo = short.Parse(nextFlowStatingNo);

                if (int.Parse(tenMainTrackNo) != outMainTrackNumber)
                {
                    BridgeService.Instance.BridgeHandler(int.Parse(tenMainTrackNo), int.Parse(tenStatingNo), tenHangerNo, hangerProcessFlowChartLis, outMainTrackNumber, nextFlowStatingNo, nextPPChart);
                    return;
                }
                //【衣架生产履历】下一站分配Cache写入
                var nextStatingHPResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = tenHangerNo,
                    StatingNo = tenStatingNo,
                    MainTrackNumber = int.Parse(tenMainTrackNo),
                    HangerProductFlowChart = nextPPChart,
                    Action = 1,
                    NextStatingNo = nextFlowStatingNo
                };
                var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
                //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), nextStatingHPResumeJson);
                //SusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo] = hangerProcessFlowChartLis;

                //发布衣架状态
                var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
                chpf.HangerNo = tenHangerNo;
                chpf.MainTrackNumber = outMainTrackNumber;
                chpf.StatingNo = int.Parse(string.IsNullOrEmpty(nextFlowStatingNo) ? "-1" : nextFlowStatingNo);
                chpf.FlowNo = nextPPChart?.FlowNo;
                chpf.FlowIndex = nextPPChart == null ? 1 : nextPPChart.FlowIndex.Value;
                chpf.FlowType = null == nextPPChart?.FlowType ? 0 : nextPPChart.FlowType.Value;
                var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);

                //衣架站内数处理
                CorrectStatingNumAndCacheByAllocation(tenHangerNo, outMainTrackNumber, nextFlowStatingNo, nextPPChart);

                //当前衣架工序维护
                CorrectHangerNextFlowHandler(tenHangerNo, nextPPChart);

                //更新等待衣架缓存
                if (!string.IsNullOrEmpty(nextFlowStatingNo))
                {
                    var dicWaitProcessOrderHanger = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DaoModel.WaitProcessOrderHanger>(SusRedisConst.WAIT_PROCESS_ORDER_HANGER);
                    if (dicWaitProcessOrderHanger.ContainsKey(tenHangerNo))
                    {
                        dicWaitProcessOrderHanger[tenHangerNo].FlowIndex = (short)nextPPChart.FlowIndex;
                        dicWaitProcessOrderHanger[tenHangerNo].FlowNo = nextPPChart.FlowNo;
                        dicWaitProcessOrderHanger[tenHangerNo].ProcessFlowCode = nextPPChart.FlowCode;
                        dicWaitProcessOrderHanger[tenHangerNo].ProcessFlowId = nextPPChart.FlowId;
                        dicWaitProcessOrderHanger[tenHangerNo].ProcessFlowName = nextPPChart.FlowName;
                        dicWaitProcessOrderHanger[tenHangerNo] = dicWaitProcessOrderHanger[tenHangerNo];
                    }
                }

                var nStatingNo = nextFlowStatingNo;
                /////var nextFlowIndex = 0;
                var dicHangerStatingALloList = NewCacheService.Instance.GetHangerAllocationItemListForRedis(tenHangerNo);// NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo];
                var nextHangerFlowChartList = hangerProcessFlowChartLis.Where(f => f.FlowIndex.Value == nextPPChart.FlowIndex.Value && f.StatingNo.Value == short.Parse(nStatingNo)
                && (f.Status.Value == HangerProductFlowChartStaus.WaitingProducts.Value || f.Status.Value == HangerProductFlowChartStaus.Producting.Value)).ToList<HangerProductFlowChartModel>();
                if (nextHangerFlowChartList.Count > 0)
                {
                    var nextHangerFlowChart = nextHangerFlowChartList[0];
                    nextFlowIndex = nextHangerFlowChart.FlowIndex.Value;
                    var nextHangerStatingAllocationItem = new DaoModel.HangerStatingAllocationItem();
                    nextHangerStatingAllocationItem.Id = GUIDHelper.GetGuidString();
                    nextHangerStatingAllocationItem.FlowIndex = (short)nextPPChart.FlowIndex.Value;
                    nextHangerStatingAllocationItem.SiteNo = tenStatingNo;
                    nextHangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
                    nextHangerStatingAllocationItem.HangerNo = tenHangerNo;
                    nextHangerStatingAllocationItem.NextSiteNo = nextFlowStatingNo;
                    nextHangerStatingAllocationItem.OutMainTrackNumber = int.Parse(tenMainTrackNo);
                    nextHangerStatingAllocationItem.FlowNo = nextHangerFlowChart.FlowNo;
                    nextHangerStatingAllocationItem.ProcessFlowCode = nextHangerFlowChart.FlowCode;
                    nextHangerStatingAllocationItem.ProcessFlowName = nextHangerFlowChart.FlowName;
                    nextHangerStatingAllocationItem.HangerType = nextHangerFlowChart.FlowType;
                    //控制出站产量推送方式(返工还是正常)
                    nextHangerStatingAllocationItem.IsReturnWorkFlow = false;
                    nextHangerStatingAllocationItem.ProcessFlowId = nextHangerFlowChart.FlowId;
                    nextHangerStatingAllocationItem.MainTrackNumber = nextHangerFlowChart.MainTrackNumber.Value;
                    nextHangerStatingAllocationItem.AllocatingStatingDate = DateTime.Now;
                    nextHangerStatingAllocationItem.FlowChartd = nextHangerFlowChart.ProcessChartId;
                    nextHangerStatingAllocationItem.ProductsId = nextHangerFlowChart.ProductsId;
                    nextHangerStatingAllocationItem.PSize = nextHangerFlowChart.PSize;
                    nextHangerStatingAllocationItem.PColor = nextHangerFlowChart.PColor;
                    nextHangerStatingAllocationItem.ProcessOrderNo = nextHangerFlowChart.ProcessOrderNo;
                    nextHangerStatingAllocationItem.StyleNo = nextPPChart.StyleNo;
                    nextHangerStatingAllocationItem.LastFlowIndex = 0;
                    nextHangerStatingAllocationItem.IsStatingStorageOutStating = false;
                    nextHangerStatingAllocationItem.IsBridgeStatingOutStatingAllocate = true;
                    nextHangerStatingAllocationItem.BatchNo = BridgeService.Instance.GetBatchNo(tenHangerNo);
                    nextHangerStatingAllocationItem.GroupNo = BridgeService.Instance.GetGroupNo(nextHangerFlowChart.MainTrackNumber.Value, int.Parse(nStatingNo));
                    dicHangerStatingALloList.Add(nextHangerStatingAllocationItem);
                    // NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo] = dicHangerStatingALloList;
                    NewCacheService.Instance.UpdateHangerAllocationItemListToRedis(tenHangerNo, dicHangerStatingALloList);

                    //记录衣架分配
                    var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextHangerStatingAllocationItem);
                    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

                    LowerPlaceInstr.Instance.AllocationHangerToNextStating(tenHangerNo, nextFlowStatingNo + "", outMainTrackNumber);

                    var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new DaoModel.HangerStatingAllocationItem()
                    {
                        HangerNo = tenHangerNo + "",
                        MainTrackNumber = (short)outMainTrackNumber,
                        SiteNo = nextFlowStatingNo + ""
                         ,
                        AllocatingStatingDate = DateTime.Now
                    });
                    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);


                    //记录桥接站出战缓存

                    //if (!dicHangerBridgeOutSiteCache.ContainsKey(keyBridgeCache))
                    //{
                    //    dicHangerBridgeOutSiteCache.Add(keyBridgeCache, new HangerProductFlowChartModel()
                    //    {
                    //        HangerNo = tenHangerNo,
                    //        MainTrackNumber = short.Parse(tenMainTrackNo),
                    //        StatingNo = short.Parse(tenStatingNo),
                    //        BridgeOutSiteSucessed = true
                    //    });
                    //}
                }


                //更新【衣架工艺图】衣架下一站的站点分配状态和时间
                if (!string.IsNullOrEmpty(nextFlowStatingNo))
                {
                    //  dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                    if (!NewCacheService.Instance.HangerIsContainsFlowChart(tenHangerNo))//dicHangerProcessFlowChart.ContainsKey(tenHangerNo))
                    {
                        tcpLogError.ErrorFormat("衣架:{0} 找不到生产工艺图缓存! 站点:{1} 主轨:{2}", tenHangerNo, nStatingNo, outMainTrackNumber);
                    }
                    else
                    {
                        //var nStatingNo = nextStatingNo;
                        var hangerFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(tenHangerNo);//dicHangerProcessFlowChart[tenHangerNo];
                        foreach (var hfc in hangerFlowChartList)
                        {
                            if (hfc.FlowIndex.Value == nextFlowIndex && nStatingNo.Equals(hfc.StatingNo.Value.ToString()) && null != hfc.StatingNo && hfc.Status.Value == 0)
                            {
                                hfc.isAllocationed = true;
                                hfc.AllocationedDate = DateTime.Now;
                                hfc.IsStorageStatingAgainAllocationedSeamsStating = false;
                                //  NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[tenHangerNo] = hangerFlowChartList;
                                NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(tenHangerNo, hangerProcessFlowChartLis);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private bool IsFlowMoveAndStatingMove(string mainTrackNo, string statingNo, string hangerNo)
        {
            //  var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            var onlineHangerProcessFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<ProductsFlowChartCacheTempModel>>(SusRedisConst.ON_LINE_PRODUCTS_FLOW_CHART);
            var fcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo); //dicHangerProcessFlowChart[hangerNo];
            var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
            var current = dicCurrentHangerProductingFlowModelCache[hangerNo];
            var onlineDicProducts = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.ProductsModel>(SusRedisConst.ON_LINE_PRODUCTS);

            if (onlineDicProducts.Keys.Count == 0)
            {
                var exx = new ApplicationException("上线产品不存在!请联系管理员.");
                tcpLogError.Error(exx);
                throw exx;
            }
            var onProduct = onlineDicProducts.First().Value;

            //桥接站兼容有问题
            var dicHangingPieceMainTrackNumber = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, string>(SusRedisConst.HANGER_HANGING_PIECE_MAINTRACK_NUMBER);
            if (!dicHangingPieceMainTrackNumber.ContainsKey(hangerNo))
            {
                var exx = new ApplicationException("上线产品不存在!请联系管理员.");
                tcpLogError.Error(exx);
                throw exx;
            }
            var hangerHangingPieceStatingMaintrackNumber = dicHangingPieceMainTrackNumber[hangerNo];

            var onlinepKey = string.Format("{0}:{1}", hangerHangingPieceStatingMaintrackNumber, onProduct.ProductionNumber.Value);
            if (!onlineHangerProcessFlowChartCache.ContainsKey(onlinepKey))
            {
                var exx = new ApplicationException("上线产品不存在!请联系管理员.");
                tcpLogError.Error(exx);
                throw exx;
            }
            var onlineHangerProcessFlowChart = onlineHangerProcessFlowChartCache[onlinepKey];
            //工序已经由别的站点生产完成
            var isStatingExist = fcList.Where(f => f.StatingNo != null && f.StatingNo.Value == int.Parse(statingNo) && f.StatingNo.Value == current.StatingNo
            && f.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() > 0;
            var isStatingExist2 = onlineHangerProcessFlowChart.Where(f => f.StatingNo != null && int.Parse(f.StatingNo) == int.Parse(statingNo) && int.Parse(f.StatingNo) == current.StatingNo).Count() > 0;
            var isFlowExist = fcList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() > 0;
            var isChangeFlowIndex = fcList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
             && f.FlowIndex.Value != current.FlowIndex
            ).Count() > 0;
            var flowNonSuccess = current.FlowIndex != 1;
            var currentHangerStatingFlowIsChange = fcList.Where(f => f.StatingNo != null && f.StatingNo.Value == int.Parse(statingNo) && f.FlowNo.Equals(current.FlowNo) && f.Status.Value != HangerProductFlowChartStaus.Successed.Value
            ).Count() == 0;
            if ((isStatingExist || isStatingExist2) && isFlowExist && currentHangerStatingFlowIsChange && flowNonSuccess)
            {
                //var oldFlowIndex = fcList.Where(f => f.FlowNo.Equals(current.FlowNo)).Select(k => k.FlowIndex.Value).First();
                //if (oldFlowIndex < current.FlowIndex)
                //{

                //}
                var flowIndex = 0;
                var nextFlowList = fcList.Where(f => f.StatingNo != null && f.StatingNo.Value != -1
      && f.FlowIndex.Value != 1
      && ((null != f.IsMergeForward && !f.IsMergeForward.Value) || null == f.IsMergeForward)
      && ((fcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 0 && ff.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 0)
      || ((fcList.Where(ff => ff.FlowIndex.Value == f.FlowIndex.Value && ff.FlowType.Value == 1 && ff.Status.Value != HangerProductFlowChartStaus.Successed.Value).Count() != 0)))
      ).Select(f => f.FlowIndex);
                if (nextFlowList.Count() > 0)
                {
                    flowIndex = nextFlowList.Min().Value;
                }
                var fsEx = new FlowMoveAndStatingMoveException("工序及站点同时移动");
                fsEx.StatingNo = int.Parse(statingNo);
                fsEx.HangerNo = int.Parse(hangerNo);
                fsEx.FlowNo = current.FlowNo;
                fsEx.NextFlowIndex = flowIndex;
                tcpLogError.Error(fsEx);
                throw fsEx;
                //return true;
            }
            return false;
        }
        private bool IsCheckRepeatOutSite(string mainTrackNo, string statingNo, string hangerNo)
        {
            // var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            var fcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo); //dicHangerProcessFlowChart[hangerNo];
            var statFlowNoList = fcList.Where(f => f.MainTrackNumber.Value == int.Parse(mainTrackNo) && f.StatingNo != null && f.StatingNo.Value == int.Parse(statingNo));
            if (statFlowNoList.Count() == 1 && statFlowNoList.Where(f => f.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() == 1)
            {
                return true;
            }
            else if (statFlowNoList.Count() > 1)
            {
                // var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                var current = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo);// dicCurrentHangerProductingFlowModelCache[hangerNo];
                var currentStatingFlowNoList = statFlowNoList.Where(f => f.Status.Value == HangerProductFlowChartStaus.Successed.Value).Select(ff => ff.FlowNo);
                if (statFlowNoList.Count() == currentStatingFlowNoList.Count())
                {
                    return true;
                }
                if (!currentStatingFlowNoList.Contains(current.FlowNo) && current.StatingNo != int.Parse(statingNo))
                {
                    return true;
                }
                if (currentStatingFlowNoList.Contains(current.FlowNo) && current.FlowIndex == 1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckIsOnlyMoveFlow(CurrentHangerProductingFlowModel current, List<HangerProductFlowChartModel> fccList)
        {
            var isExistFlowNo = fccList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value == current.StatingNo).Count() > 0;
            if (isExistFlowNo)
            {
                //工序生产顺序变化
                var fcc = fccList.Where(f => f.FlowNo.Equals(current.FlowNo) && f.StatingNo != null && f.StatingNo.Value == current.StatingNo).Where(ff => ff.FlowIndex.Value != current.FlowIndex).Count();
                return fcc > 0;
            }
            return false;
        }

        public bool IsStoreStating(HangerProductFlowChartModel ppChart)
        {
            if (ppChart == null) return false;
            if (null != ppChart.StatingRoleCode && ppChart.StatingRoleCode.Equals(StatingType.StatingStorage.Code))
            {
                return true;
            }
            return false;
        }


        public void MergeProcessFlowChartFlowHanlder(HangerProductFlowChartModel ppChart, ref List<HangerProductFlowChartModel> hangerProcessFlowChartList)
        {
            foreach (var hpfc in hangerProcessFlowChartList)
            {
                if (ppChart.ProcessFlowChartFlowRelationId.Equals(hpfc.MergeProcessFlowChartFlowRelationId))// && hpfc.Status.Value != HangerProductFlowChartStaus.Successed.Value)
                {
                    hpfc.IncomeSiteDate = ppChart.IncomeSiteDate;
                    hpfc.CompareDate = ppChart.CompareDate;
                    hpfc.OutSiteDate = ppChart.OutSiteDate;
                    hpfc.IsFlowSucess = true;
                    hpfc.Status = HangerProductFlowChartStaus.Successed.Value;
                    hpfc.FlowRealyProductStatingNo = ppChart.StatingNo.Value;
                    hpfc.FlowRealyProductStatingRoleCode = ppChart.StatingRoleCode?.Trim();
                }
            }
        }

        private static readonly object locObj = new Object();
        /// <summary>
        /// 缓存衣架挂片读卡记录
        /// </summary>
        /// <param name="xID"></param>
        /// <param name="iD"></param>
        /// <param name="hangerNo"></param>
        internal void CacheHangingHangerRequest(int xID, int iD, int hangerNo)
        {
            lock (locObj)
            {
                var dicHanger = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, HangerHangingRequestModel>(SusRedisConst.HANGER_HNGING_PIECE_REQUEST_ACTION);
                if (dicHanger.ContainsKey(hangerNo.ToString()))
                {
                    dicHanger.Remove(hangerNo.ToString());
                }
                dicHanger.Add(hangerNo.ToString(), new HangerHangingRequestModel()
                {
                    MainTrackNumber = xID,
                    StatingNo = iD,
                    CompareDate = DateTime.Now
                });
            }
        }


        /// <summary>
        /// 检查工序是否生产完成,若完成将完成数据转移
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        bool CheckFlowIsSuccessed(string flowChartId, int flowIndex)
        {
            var sql = new StringBuilder(@"select ISNULL(count(1),0) total from [ProcessFlowChartFlowRelation] where PROCESSFLOWCHART_Id=?");
            var totalFlowCount = QueryForObject<int>(sql, null, true, flowChartId);
            if (flowIndex == totalFlowCount)//生产完成
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 检查衣架是否生产完工艺图上的的工序
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        public bool CheckHangerProcessChartIsSuccessed(string hangerNo)
        {
            //var dicCondi = new Dictionary<string, string>();
            //dicCondi.Add("HangerNo", hangerNo);
            //// dicCondi.Add("ProductsId", productId);
            ////  dicCondi.Add("FlowIndex", pFlowIndex.ToString());
            //var sql = string.Format(@"select * from(
            //    select T.FlowIndex,COUNT(1) TotalCount,(
            //     select COUNT(1) NonProductsFlowCount 
            //     from HangerProductFlowChart 
            //     where IsFlowSucess=0 And FlowIndex=T.FlowIndex AND HangerNo=:HangerNo
            //     )NonProductsFlowCount 
            //    from HangerProductFlowChart T 
            //    where  T.HangerNo=:HangerNo GROUP BY T.FlowIndex)Res where Res.TotalCount-Res.NonProductsFlowCount=0
            //    ");
            //var p = Query<FlowProductTag>(new StringBuilder(sql), null, true, dicCondi);
            //return !(p.Count > 0);
            //  var dicHangerProductFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            if (!NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo))//dicHangerProductFlowChart.ContainsKey(hangerNo))
            {
                tcpLogError.Error(string.Format("未找到生产衣架:{0}", hangerNo));
                return false;
            }
            var hangerProductFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo);// NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo];
            var hangerProductFlowChartArr = new HangerProductFlowChartModel[hangerProductFlowChartList.Count];
            hangerProductFlowChartList.CopyTo(hangerProductFlowChartArr);
            //过滤掉第一道工序
            var cloneHangerProductFlowChartList = new List<HangerProductFlowChartModel>(hangerProductFlowChartArr.Where(f => f.FlowIndex > 1));
            // var totalProductsFlowCount = cloneHangerProductFlowChartList.GroupBy(f => f.FlowIndex.Value);
            //var nonProductsFlowCount = hangerProductFlowChartList.Where(f => !f.IsFlowSucess.Value).GroupBy(f => f.FlowIndex.Value).Select(k=>k.Key);
            //.Where(f => !f.IsFlowSucess.Value).ToList<HangerProductFlowChartModel>().Count
            //var nonProductsFlowCount =
            var sucessFlowCount = 0;
            //过滤掉第一道工序
            var flowIndexList = cloneHangerProductFlowChartList.Select(f => f.FlowIndex.Value).Where(f => f > 1).ToList<int>().Distinct().ToList<int>();
            foreach (var f in flowIndexList)
            {
                foreach (var hpfc in cloneHangerProductFlowChartList)
                {
                    if (hpfc.FlowIndex.Value == f && hpfc.IsFlowSucess.Value)
                    {
                        sucessFlowCount++;
                        break;
                    }
                }
            }
            return flowIndexList.Count == sucessFlowCount;
        }
        //衣架生产完成时，转移生产数据
        //public void CopySucessData(object data)
        //{

        //    var wpHangerData = data as DaoModel.WaitProcessOrderHanger;
        //    try
        //    {
        //        var sucessProcessOrderHanger = BeanUitls<DaoModel.SucessProcessOrderHanger, DaoModel.WaitProcessOrderHanger>.Mapper(wpHangerData);
        //        SucessProcessOrderHangerDao.Instance.Save(sucessProcessOrderHanger);
        //        WaitProcessOrderHangerDao.Instance.Delete(wpHangerData.Id);


        //        var hangerProductItemList = Query<DaoModel.HangerProductItem>(new StringBuilder("select * from HangerProductItem where HangerNo=?"), null, false, wpHangerData.HangerNo);
        //        foreach (var pi in hangerProductItemList)
        //        {
        //            var hpi = BeanUitls<DaoModel.SucessHangerProductItem, DaoModel.HangerProductItem>.Mapper(pi);
        //            hpi.Id = null;
        //            SucessHangerProductItemDao.Instance.Save(hpi);
        //            HangerProductItemDao.Instance.Delete(pi.Id);
        //        }
        //        var hangerReworkRecordList = Query<DaoModel.HangerReworkRecord>(new StringBuilder("select * from HangerReworkRecord where HangerNo=?"), null, false, wpHangerData.HangerNo);
        //        foreach (var pi in hangerReworkRecordList)
        //        {
        //            var hpi = BeanUitls<DaoModel.SucessHangerReworkRecord, DaoModel.HangerReworkRecord>.Mapper(pi);
        //            hpi.Id = null;
        //            SucessHangerReworkRecordDao.Instance.Save(hpi);
        //            HangerReworkRecordDao.Instance.Delete(pi.Id);
        //        }
        //        var statingHangerProductItemList = Query<DaoModel.StatingHangerProductItem>(new StringBuilder("select * from StatingHangerProductItem where HangerNo=?"), null, false, wpHangerData.HangerNo);
        //        foreach (var pi in statingHangerProductItemList)
        //        {
        //            var hpi = BeanUitls<DaoModel.SuccessStatingHangerProductItem, DaoModel.StatingHangerProductItem>.Mapper(pi);
        //            hpi.Id = null;
        //            SuccessStatingHangerProductItemDao.Instance.Save(hpi);
        //            StatingHangerProductItemDao.Instance.Delete(pi.Id);
        //        }
        //        var hangerStatingAllocationItemList = Query<DaoModel.HangerStatingAllocationItem>(new StringBuilder("select * from HangerStatingAllocationItem where HangerNo=?"), null, false, wpHangerData.HangerNo);
        //        foreach (var pi in hangerStatingAllocationItemList)
        //        {
        //            var hpi = BeanUitls<DaoModel.SuccessHangerStatingAllocationItem, DaoModel.HangerStatingAllocationItem>.Mapper(pi);
        //            hpi.Id = null;
        //            //SuccessHangerStatingAllocationItemDao.Instance.Save(hpi);
        //            //HangerStatingAllocationItemDao.Instance.Delete(pi.Id);
        //        }
        //        var employeeFlowProductionList = Query<DaoModel.EmployeeFlowProduction>(new StringBuilder("select * from EmployeeFlowProduction where HangerNo=?"), null, false, wpHangerData.HangerNo);
        //        foreach (var pi in employeeFlowProductionList)
        //        {
        //            var hpi = BeanUitls<DaoModel.SucessEmployeeFlowProduction, DaoModel.EmployeeFlowProduction>.Mapper(pi);
        //            hpi.Id = null;
        //            SucessEmployeeFlowProductionDao.Instance.Save(hpi);
        //            EmployeeFlowProductionDao.Instance.Delete(pi.Id);
        //        }
        //        //产出量+1
        //        if (!string.IsNullOrEmpty(wpHangerData.ProductsId))
        //        {
        //            var sqlUpdateProducts = "Update Products set TotalProdOutNum=TotalProdOutNum+1 where id=?";
        //            ExecuteUpdate(sqlUpdateProducts, wpHangerData.ProductsId);
        //        }
        //        //BeanTransformerAdapter<DaoModel.SuccessStatingHangerProductItem,DaoModel.>.ma
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("CopySucessData", ex);
        //    }
        //    // var proucts
        //    //var data = BeanUitls <>
        //    //   SucessProcessOrderHangerDao.Instance.Save();
        //}

        #region 生产完成数据转移
        public void CopySucessDataExt(DaoModel.WaitProcessOrderHanger wpHangerData)
        {


            //var wpHangerData = data as DaoModel.WaitProcessOrderHanger;
            try
            {
                var wpoHanger = DapperHelp.QueryForObject<DaoModel.WaitProcessOrderHanger>("select top 1 * from WaitProcessOrderHanger where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });
                var sucessProcessOrderHanger = BeanUitls<DaoModel.SucessProcessOrderHanger, DaoModel.WaitProcessOrderHanger>.Mapper(wpoHanger);
                DapperHelp.Add(sucessProcessOrderHanger);
                DapperHelp.Execute("delete from WaitProcessOrderHanger where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });
                //SucessProcessOrderHangerDao.Instance.Save(sucessProcessOrderHanger);
                //WaitProcessOrderHangerDao.Instance.Delete(wpHangerData.Id);


                var hangerProductItemList = DapperHelp.Query<DaoModel.HangerProductItem>("select * from HangerProductItem where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });
                foreach (var pi in hangerProductItemList)
                {
                    var hpi = BeanUitls<DaoModel.SucessHangerProductItem, DaoModel.HangerProductItem>.Mapper(pi);
                    //hpi.Id = null;
                    //SucessHangerProductItemDao.Instance.Save(hpi);
                    //HangerProductItemDao.Instance.Delete(pi.Id);
                    DapperHelp.Add(hpi);

                    //HangerProductItemDao.Instance.Delete(pi.Id);
                }
                DapperHelp.Execute("delete  from HangerProductItem where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });
                var hangerReworkRecordList = DapperHelp.Query<DaoModel.HangerReworkRecord>("select * from HangerReworkRecord where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });
                foreach (var pi in hangerReworkRecordList)
                {
                    var hpi = BeanUitls<DaoModel.SucessHangerReworkRecord, DaoModel.HangerReworkRecord>.Mapper(pi);
                    DapperHelp.Add(hpi);
                    //hpi.Id = null;
                    //SucessHangerReworkRecordDao.Instance.Save(hpi);
                    //HangerReworkRecordDao.Instance.Delete(pi.Id);
                }
                DapperHelp.Execute("delete  from HangerReworkRecord where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });

                //影响产量推送，暂时注释/lucifer/2018年10月15日 15:42:34
                //var statingHangerProductItemList = DapperHelp.Query<DaoModel.StatingHangerProductItem>("select * from StatingHangerProductItem where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });
                //foreach (var pi in statingHangerProductItemList)
                //{
                //    var hpi = BeanUitls<DaoModel.SuccessStatingHangerProductItem, DaoModel.StatingHangerProductItem>.Mapper(pi);
                //    //hpi.Id = null;
                //    //SuccessStatingHangerProductItemDao.Instance.Save(hpi);
                //    //StatingHangerProductItemDao.Instance.Delete(pi.Id);
                //    DapperHelp.Add(hpi);
                //}
                //DapperHelp.Execute("delete  from StatingHangerProductItem where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });

                //lucifer
                //var hangerStatingAllocationItemList = DapperHelp.Query<DaoModel.HangerStatingAllocationItem>("select * from HangerStatingAllocationItem where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });
                //foreach (var pi in hangerStatingAllocationItemList)
                //{
                //    var hpi = BeanUitls<DaoModel.SuccessHangerStatingAllocationItem, DaoModel.HangerStatingAllocationItem>.Mapper(pi);
                //    //hpi.Id = null;
                //    //SuccessHangerStatingAllocationItemDao.Instance.Save(hpi);
                //    //HangerStatingAllocationItemDao.Instance.Delete(pi.Id);
                //    DapperHelp.Add(hpi);
                //}
                //    DapperHelp.Execute("delete  from HangerStatingAllocationItem where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });

                var employeeFlowProductionList = DapperHelp.Query<DaoModel.EmployeeFlowProduction>("select * from EmployeeFlowProduction where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });
                foreach (var pi in employeeFlowProductionList)
                {
                    var hpi = BeanUitls<DaoModel.SucessEmployeeFlowProduction, DaoModel.EmployeeFlowProduction>.Mapper(pi);
                    DapperHelp.Add(hpi);
                }
                DapperHelp.Execute("delete  from EmployeeFlowProduction where HangerNo=@HangerNo", new { HangerNo = wpHangerData.HangerNo });
                ////产出量+1
                //if (!string.IsNullOrEmpty(wpHangerData.ProductsId))
                //{
                //    var sqlUpdateProducts = "Update Products set TotalProdOutNum=TotalProdOutNum+1 where id=?";
                //    ExecuteUpdate(sqlUpdateProducts, wpHangerData.ProductsId);
                //}
                //BeanTransformerAdapter<DaoModel.SuccessStatingHangerProductItem,DaoModel.>.ma
            }
            catch (Exception ex)
            {
                log.Error("CopySucessData", ex);
            }
            // var proucts
            //var data = BeanUitls <>
            //   SucessProcessOrderHangerDao.Instance.Save();
        }

        #endregion

        public void CopyHangerProductChart(string hangerNo)
        {
            var shpfcSql = new StringBuilder("select * from HangerProductFlowChart where HangerNo=? AND IsFlowSucess=1");
            var hangerProductFlowChartList = Query<DaoModel.HangerProductFlowChart>(shpfcSql, null, false, hangerNo);
            foreach (var pc in hangerProductFlowChartList)
            {
                var successHangerProductFlowChart = BeanUitls<DaoModel.SuccessHangerProductFlowChart, DaoModel.HangerProductFlowChart>.Mapper(pc);
                successHangerProductFlowChart.IsHangerSucess = true;
                SuccessHangerProductFlowChartDao.Instance.Save(successHangerProductFlowChart);
                // HangerProductFlowChartDao.Instance.Delete(successHangerProductFlowChart.Id);
            }
            HangerProductFlowChartDao.Instance.DeleteByHql(string.Format("from  HangerProductFlowChart where HangerNo = '{0}'", hangerNo));
        }
        /// <summary>
        /// 计算是否允许出站
        /// </summary>
        /// <param name="statingNo"></param>
        /// <returns></returns>
        public bool WhetherAllowedCalculationOutStating(string statingNo)
        {
            string sqlHangerStatingNum = "select count(*) total from WaitProcessOrderHanger where  SiteNo=?";
            var num = QueryForObject<int>(new StringBuilder(sqlHangerStatingNum), null, true, statingNo?.Trim());
            if (num == 0)
            {
                return true;
                //var waitProcessOrderHanger= WaitProcessOrderHangerDao
            }
            return false;
        }

        /// <summary>
        /// 注册衣架
        /// </summary>
        /// <param name="hangingStationNo"></param>
        /// <param name="hangerNo"></param>
        /// <param name="productNumber"></param>
        public bool RegisterHangerToProducts(string mainTrackNo, string hangingStationNo, string hangerNo, ref string nextStatingNo, ref int outMainTrackNumber)
        {
            lock (locObj)
            {
                if (string.IsNullOrEmpty(hangingStationNo))
                {
                    throw new ApplicationException("挂片站编号不能为空!");
                }
                //检查出站站点是否有员工登录，没有登录不能出站
                bool isLogin = productsQueryService.CheckStatingIsLogin(hangingStationNo, int.Parse(mainTrackNo));
                if (!isLogin)
                {
                    var ex = new StatingNoLoginEmployeeException(string.Format("【错误】主轨:【{0}】站点:【{1}】没有员工登录!,不能出站", mainTrackNo, hangingStationNo));
                    tcpLogError.Error(ex);
                    throw ex;
                }

                ////清除衣架缓存
                //SusCacheBootstarp.Instance.ClearHangerCache(hangerNo.ToString());

                // int outMainTrackNumber = 0;
                HangerProductFlowChartModel nextHangerProductFlowChart = null;
                DaoModel.ProductsModel p;
                DaoModel.WaitProcessOrderHanger wp;
                HangerProductFlowChartModel hangingPieceHanger;
                //
                /// 0：正常衣架
                /// 1:挂片站出站，未进普通站 再挂片
                /// 2:挂片站出站，进了普通站或到了加工点读卡器， 拿来再挂片挂片
                /// 3:挂片站出站，进了普通站，有生产了工序(普通站出过站的衣架)
                /// 
                //
                int hangingTag = 0;

                CANProductsValidService.Instance.BindHangerToProducts(mainTrackNo, hangingStationNo, hangerNo, out p, out wp, out hangingPieceHanger, ref hangingTag);
                switch (hangingTag)
                {
                    case 0:
                        //清除衣架缓存
                        //SusCacheBootstarp.Instance.ClearHangerAllCache(hangerNo.ToString());
                        HandlerCommonHangerAllocationData(hangerNo);
                        break;
                    case 3:
                        HandlerNoSucessHangerProdcutRecord(hangerNo, true);
                        break;
                    default:
                        HandlerNoSucessHangerProdcutRecord(hangerNo);
                        break;
                }

                DaoModel.ProcessFlowChartFlowRelation pfFlowRelation = null;


                HandlerHangingPieceStaingData(mainTrackNo, hangingStationNo, hangerNo);


                //zxl 2018年7月22日 10:25:05
                //OutSiteService.Instance.GetHangerNextSite(int.Parse(mainTrackNo), hangerNo, 0, ref nextStatingNo, ref pfFlowRelation, ref outMainTrackNumber, ref hpFlowChart);
                OutSiteService.Instance.GetHangerNextSiteExt(wp, 0, ref nextStatingNo, ref outMainTrackNumber, ref nextHangerProductFlowChart, ref pfFlowRelation);

                //var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                //下一站是否是桥接站
                var nextStatingIsBridgeStating = CANProductsService.Instance.IsBridge(outMainTrackNumber, int.Parse(nextStatingNo));
                var fcList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo); //dicHangerProcessFlowChart[hangerNo];
                                                                                                //是否有桥接需求
                var isRequireBridge = CANProductsService.Instance.IsRequireBridge(mainTrackNo, fcList);

                //下一站是是否携带工序
                var isBridgeInverseBridge = false;
                if (isRequireBridge)
                    isBridgeInverseBridge = CANProductsService.Instance.IsBridgeInverseBridge(outMainTrackNumber + "", nextStatingNo, fcList);
                //桥接站的
                if (isBridgeInverseBridge && nextStatingIsBridgeStating && isRequireBridge)
                {
                    CANProductsService.Instance.RecalibrationHangerNextFlowHandler(hangerNo, outMainTrackNumber, fcList);
                }
                else
                {
                    //发布衣架状态
                    var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
                    chpf.HangerNo = hangerNo;
                    chpf.MainTrackNumber = outMainTrackNumber;
                    chpf.StatingNo = int.Parse(nextStatingNo);
                    chpf.FlowNo = nextHangerProductFlowChart.FlowNo;
                    chpf.FlowIndex = nextHangerProductFlowChart.FlowIndex.Value;
                    chpf.FlowType = null == nextHangerProductFlowChart?.FlowType ? 0 : nextHangerProductFlowChart.FlowType.Value;
                    // var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);
                    //NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);
                    //var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                    if (!NewCacheService.Instance.HangerCurrentFlowIsContains(hangerNo))//dicCurrentHangerProductingFlowModelCache.ContainsKey(hangerNo))
                    {
                        //  SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW).Add(hangerNo, chpf);
                        // tcpLogInfo.InfoFormat("【维护衣架状态】 完成【{0}】", hJson);
                        NewCacheService.Instance.AddHangerCurrentFlowToRedis(hangerNo, chpf);
                    }
                    else
                    {
                        // SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW)[hangerNo] = chpf;
                        //tcpLogInfo.InfoFormat("【维护衣架状态】 完成【{0}】", arg2);
                        NewCacheService.Instance.UpdateCurrentHangerFlowCacheToRedis(hangerNo, chpf);
                    }

                }


                //对挂片后未进站的衣架下一站分配数清0
                if (1 == hangingTag && !string.IsNullOrEmpty(nextStatingNo))
                {
                    //出站站内数-1
                    //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = outMainTrackNumber, StatingNo = int.Parse(nextStatingNo), OnLineSum = -1 };
                    //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));
                    //修正删除的站内数及明细、缓存
                    var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                    hnssoc.Action = 1;
                    hnssoc.HangerNo = hangerNo;
                    hnssoc.MainTrackNumber = outMainTrackNumber;
                    hnssoc.StatingNo = int.Parse(nextStatingNo);
                    hnssoc.FlowNo = nextHangerProductFlowChart.FlowNo;
                    hnssoc.FlowIndex = nextHangerProductFlowChart.FlowIndex.Value;
                    hnssoc.HangerProductFlowChartModel = nextHangerProductFlowChart;
                    var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
                    //    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
                    NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
                }
                //发布 待生产衣架信息
                var wpJson = Newtonsoft.Json.JsonConvert.SerializeObject(wp);
                NewSusRedisClient.subcriber.Publish(SusRedisConst.WAIT_PROCESS_ORDER_HANGER, wpJson);



                //**************************
                ///【衣架站点工艺图db写入】
                //记录挂片信息(衣架挂片工艺图)
                //挂片站产量记录
                //DapperHelp.Add(inHangerFlowChart);
                // tcpLogInfo.Info("【挂片站站点挂片产量记录----------------】");
                var outSiteJson = Newtonsoft.Json.JsonConvert.SerializeObject(hangingPieceHanger);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_OUT_HANGING_STATION_ACTION, outSiteJson);
                NewSusRedisClient.Instance.HangingStationOutSiteAction(new StackExchange.Redis.RedisChannel(), outSiteJson);

                //【衣架生产履历】本站衣架生产履历Cache写入
                var hpResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = hangerNo,
                    StatingNo = hangingStationNo,
                    MainTrackNumber = int.Parse(mainTrackNo),
                    HangerProductFlowChart = hangingPieceHanger,
                    Action = 0
                };


                var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
                NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hangerResumeJson);

                //【衣架生产履历】下一站分配Cache写入
                var ntStatingNo = nextStatingNo;
                var fccccList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo);
                var nextRealyFlowChartList = fccccList.Where(f => f.FlowIndex.Value == nextHangerProductFlowChart.FlowIndex.Value
                 && f.StatingNo.Value == short.Parse(ntStatingNo));
                if (nextRealyFlowChartList.Count() == 0)
                {
                    var eex = new ApplicationException(string.Format("衣架:{0} 找不到生产工艺图缓存! 站点:{1} 主轨:{2}", hangerNo, nextStatingNo, outMainTrackNumber));
                    tcpLogError.Error(eex);
                    throw eex;
                }
                var nextRealyFlowChart = nextRealyFlowChartList.First();
                var nextStatingHPResume = new HangerProductsChartResumeModel()
                {
                    HangerNo = hangerNo,
                    StatingNo = hangingStationNo,
                    MainTrackNumber = int.Parse(mainTrackNo),
                    HangerProductFlowChart = nextRealyFlowChart,
                    Action = 1,
                    NextStatingNo = nextStatingNo
                };

                //var nextStatingHPResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(nextStatingHPResume);
                //SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
                NextStatingCacheHandler.Instance.NextStatingHangerResumeCorrect(nextStatingHPResume);

                var hnssocAll = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
                hnssocAll.Action = 0;
                hnssocAll.HangerNo = hangerNo;
                hnssocAll.MainTrackNumber = outMainTrackNumber;
                hnssocAll.StatingNo = int.Parse(nextStatingNo);
                hnssocAll.FlowNo = nextRealyFlowChart.FlowNo;
                hnssocAll.FlowIndex = nextRealyFlowChart.FlowIndex.Value;
                hnssocAll.HangerProductFlowChartModel = nextRealyFlowChart;
                var hnssocAllJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssocAll);
                // NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocAllJson);
                NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocAllJson);
                try
                {
                    //记录衣架号与挂片主轨的关系
                    var dicHangerHangingPieceMainTrackNumber = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, string>(SusRedisConst.HANGER_HANGING_PIECE_MAINTRACK_NUMBER);
                    if (dicHangerHangingPieceMainTrackNumber.ContainsKey(hangerNo))
                    {
                        NewSusRedisClient.RedisTypeFactory.GetDictionary<string, string>(SusRedisConst.HANGER_HANGING_PIECE_MAINTRACK_NUMBER)[hangerNo] = mainTrackNo;
                    }
                    else
                    {
                        NewSusRedisClient.RedisTypeFactory.GetDictionary<string, string>(SusRedisConst.HANGER_HANGING_PIECE_MAINTRACK_NUMBER).Add(hangerNo, mainTrackNo);
                    }

                    //记录挂片数
                    //var pIndex = productsQueryService.GetNextIndex(hangerNo, "HangerStatingAllocationItem");
                    var hsaItem = BeanUitls<DaoModel.HangerStatingAllocationItem, DaoModel.WaitProcessOrderHanger>.Mapper(wp);
                    hsaItem.Id = GUIDHelper.GetGuidString();
                    hsaItem.HsaiNdex = 1;
                    hsaItem.Memo = "-1";
                    hsaItem.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
                    hsaItem.SiteNo = hangingStationNo?.Trim();
                    hsaItem.FlowNo = "1";

                    hsaItem.AllocatingStatingDate = DateTime.Now;
                    //HangerStatingAllocationItemDao.Instance.Insert(hsaItem);



                    //发布 记录挂片数
                    var hsaItemJson = Newtonsoft.Json.JsonConvert.SerializeObject(hsaItem);
                    //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemJson);

                    //var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
                    //{
                    //    HangerNo = tenHangerNo + "",
                    //    MainTrackNumber = (short)outMainTrackNumber,
                    //    SiteNo = nextStatingNo + "",
                    //    Memo = "-1"
                    //});
                    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, hsaItemJson);

                    //记录下一道工序站点
                    //pIndex = productsQueryService.GetNextIndex(hangerNo, "HangerStatingAllocationItem");
                    var hsaItemNext = BeanUitls<DaoModel.HangerStatingAllocationItem, DaoModel.WaitProcessOrderHanger>.Mapper(wp);
                    hsaItemNext.SiteNo = hangingStationNo;
                    hsaItemNext.AllocatingStatingDate = DateTime.Now;
                    hsaItemNext.NextSiteNo = nextStatingNo;
                    hsaItemNext.OutMainTrackNumber = outMainTrackNumber;
                    hsaItemNext.Status = (byte)HangerStatingAllocationItemStatus.Allocationed.Value;
                    hsaItemNext.Id = GUIDHelper.GetGuidString();
                    hsaItemNext.FlowIndex = short.Parse(nextHangerProductFlowChart.FlowIndex.Value + "");
                    hsaItemNext.FlowNo = nextHangerProductFlowChart.FlowNo;
                    hsaItemNext.FlowChartd = nextHangerProductFlowChart.ProcessChartId;
                    hsaItemNext.Unit = p.Unit;
                    hsaItemNext.StyleNo = p.StyleNo;
                    hsaItemNext.LineName = wp.LineName;
                    hsaItemNext.ProcessFlowCode = nextHangerProductFlowChart.FlowCode;
                    hsaItemNext.ProcessFlowName = nextHangerProductFlowChart.FlowName;
                    hsaItemNext.ProcessOrderNo = p.ProcessOrderNo;
                    hsaItemNext.LastFlowIndex = 1;
                    //   HangerStatingAllocationItemDao.Instance.Insert(hsaItemNext);

                    //发布 待生产衣架信息
                    var hsaItemNextJson = Newtonsoft.Json.JsonConvert.SerializeObject(hsaItemNext);
                    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_ALLOCATION_ITME_DB_RECORD_ACTION, hsaItemNextJson);

                    //缓存衣架分配记录
                    var listHangerStatingAllocationItemList = new List<DaoModel.HangerStatingAllocationItem>();
                    listHangerStatingAllocationItemList.AddRange(new DaoModel.HangerStatingAllocationItem[] { hsaItem, hsaItemNext });
                    // var dicHangerStatingALlo = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
                    //dicHangerStatingALlo.Add(hangerNo, listHangerStatingAllocationItemList);
                    NewCacheService.Instance.AddHangerAllocationItem(hangerNo, listHangerStatingAllocationItemList);

                    //if (!string.IsNullOrEmpty(nextStatingNo))
                    //{
                    //    //分配数+1
                    //    var allocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = int.Parse(mainTrackNo), StatingNo = int.Parse(nextStatingNo), AllocationNum = 1 };
                    //    SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(allocationNumModel));
                    //}
                    //更新衣架分配
                    if (!string.IsNullOrEmpty(nextStatingNo))
                    {

                        if (!NewCacheService.Instance.HangerIsContainsFlowChart(hangerNo)) //dicHangerProcessFlowChart.ContainsKey(hangerNo))
                        {
                            tcpLogError.ErrorFormat("衣架:{0} 找不到生产工艺图缓存! 站点:{1} 主轨:{2}", hangerNo, nextStatingNo, outMainTrackNumber);
                        }
                        else
                        {
                            var nStatingNo = nextStatingNo;
                            var hangerFlowChartList = NewCacheService.Instance.GetHangerFlowChartListForRedis(hangerNo); //dicHangerProcessFlowChart[hangerNo];
                            foreach (var hfc in hangerFlowChartList)
                            {
                                if (nextStatingNo.Equals(hfc.StatingNo.Value.ToString()) && null != hfc.StatingNo && hfc.Status.Value == 0)
                                {
                                    hfc.isAllocationed = true;
                                    hfc.AllocationedDate = DateTime.Now;
                                    //  NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[hangerNo] = hangerFlowChartList;
                                    NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(hangerNo, hangerFlowChartList);
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    redisLog.Error($"【衣架挂片异常】 衣架号:{hangerNo} 主轨:{mainTrackNo} 站点:{hangingStationNo}", ex);
                    log.Error($"【衣架挂片异常】 衣架号:{hangerNo} 主轨:{mainTrackNo} 站点:{hangingStationNo}", ex);
                    tcpLogError.Error($"【衣架挂片异常】 衣架号:{hangerNo} 主轨:{mainTrackNo} 站点:{hangingStationNo}", ex);
                }
                return true;
            }
        }

        //衣架号重复生产，数据转移
        private void HandlerCommonHangerAllocationData(string hangerNo)
        {
            //lucifer
            var hangerStatingAllocationItemList = DapperHelp.Query<DaoModel.HangerStatingAllocationItem>("select * from HangerStatingAllocationItem where HangerNo=@HangerNo", new { HangerNo = hangerNo });
            foreach (var pi in hangerStatingAllocationItemList)
            {
                var hpi = BeanUitls<DaoModel.SuccessHangerStatingAllocationItem, DaoModel.HangerStatingAllocationItem>.Mapper(pi);
                //hpi.Id = null;
                //SuccessHangerStatingAllocationItemDao.Instance.Save(hpi);
                //HangerStatingAllocationItemDao.Instance.Delete(pi.Id);
                DapperHelp.Add(hpi);
            }
            DapperHelp.Execute("Delete HangerStatingAllocationItem WHERE HangerNo=@HangerNo", new { HangerNo = hangerNo });

        }

        /// <summary>
        /// 是否需要跨主轨
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextStatingNo"></param>
        /// <param name="fcList"></param>
        /// <returns></returns>
        public bool IsRequireBridge(string mainTrackNo, List<HangerProductFlowChartModel> fcList)
        {
            var isRequireBridge = fcList.Where(ff => ff.MainTrackNumber.Value != 0).Select(k => k.MainTrackNumber.Value).Distinct().Count() > 1;
            return isRequireBridge;
        }

        private void HandlerHangingPieceStaingData(string mainTrackNo, string hangingStationNo, string hangerNo)
        {
            //挂片站内数-1
            //var inNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = int.Parse(mainTrackNo), StatingNo = int.Parse(hangingStationNo), OnLineSum = -1 };
            //NewSusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_IN_NUM, JsonConvert.SerializeObject(inNumModel));



            var logMessage = string.Format("【挂片站站点处理】衣架号:{0} 主轨:{1} 站点:{2} -->衣架站内衣架数据!", hangerNo, mainTrackNo, hangingStationNo);
            tcpLogInfo.Info(logMessage);

            var mainStatingKey = string.Format("{0}:{1}", mainTrackNo, hangingStationNo);
            var dicHangingPieceStating = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<ProductsFlowChartCacheTempModel>>(SusRedisConst.MAINTRACK_STATING_IN_HANGER);
            if (dicHangingPieceStating.ContainsKey(mainStatingKey))
            {
                var incomeStatingHangerList = dicHangingPieceStating[mainStatingKey];
                incomeStatingHangerList.RemoveAll(f => f.HangerNo == int.Parse(hangerNo) && f.MainTrackNumber == int.Parse(mainTrackNo) && null != f.StatingNo && f.StatingNo.Equals(hangingStationNo + ""));
                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<ProductsFlowChartCacheTempModel>>(SusRedisConst.MAINTRACK_STATING_IN_HANGER)[mainStatingKey] = incomeStatingHangerList;
            }
        }

        /// <summary>
        /// 处理生产了一半的衣架数据
        /// </summary>
        /// <param name="hangerNo"></param>
        /// <param name="isCalculationYield">是否计算产量</param>
        private void HandlerNoSucessHangerProdcutRecord(string hangerNo, bool isCalculationYield = false)
        {
            //衣架工艺信息处理(衣架号变负数)
            var hangerProductFlowChartList = DapperHelp.Query<DaoModel.HangerProductFlowChart>("select * from HangerProductFlowChart WHERE HangerNo=@HangerNo", new { HangerNo = hangerNo });
            foreach (var hpf in hangerProductFlowChartList)
            {
                hpf.HangerNo = isCalculationYield ? hpf.HangerNo : string.Format("-{0}", hpf.HangerNo);
                var successHangerProductFlowChart = BeanUitls<DaoModel.SuccessHangerProductFlowChart, DaoModel.HangerProductFlowChart>.Mapper(hpf);
                DapperHelp.Add<DaoModel.SuccessHangerProductFlowChart>(successHangerProductFlowChart);
                DapperHelp.Execute("Delete HangerProductFlowChart WHERE HangerNo=@HangerNo", new { HangerNo = hangerNo });
            }
            //当前处理衣架记录
            var waitProcessOrderHangerList = DapperHelp.Query<DaoModel.WaitProcessOrderHanger>("select * from WaitProcessOrderHanger WHERE HangerNo=@HangerNo", new { HangerNo = hangerNo });
            foreach (var wp in waitProcessOrderHangerList)
            {
                wp.HangerNo = string.Format("-{0}", wp.HangerNo);
                if (isCalculationYield) wp.Memo = "半成品衣架";
                var successHangerProductFlowChart = BeanUitls<DaoModel.SucessProcessOrderHanger, DaoModel.WaitProcessOrderHanger>.Mapper(wp);
                DapperHelp.Add<DaoModel.SucessProcessOrderHanger>(successHangerProductFlowChart);
                DapperHelp.Execute("Delete WaitProcessOrderHanger WHERE HangerNo=@HangerNo", new { HangerNo = hangerNo });
            }
            //衣架分配记录
            var hangerStatingAllocationItemList = DapperHelp.Query<DaoModel.HangerStatingAllocationItem>("select * from HangerStatingAllocationItem WHERE HangerNo=@HangerNo", new { HangerNo = hangerNo });
            foreach (var wp in hangerStatingAllocationItemList)
            {
                wp.HangerNo = isCalculationYield ? wp.HangerNo : string.Format("-{0}", wp.HangerNo);
                var successHangerStatingAllocationItem = BeanUitls<DaoModel.SuccessHangerStatingAllocationItem, DaoModel.HangerStatingAllocationItem>.Mapper(wp);
                DapperHelp.Add<DaoModel.SuccessHangerStatingAllocationItem>(successHangerStatingAllocationItem);

            }
            DapperHelp.Execute("Delete HangerStatingAllocationItem WHERE HangerNo=@HangerNo", new { HangerNo = hangerNo });
            //衣架生产明细

            var hangerProductItemList = DapperHelp.Query<DaoModel.HangerProductItem>("select * from HangerProductItem WHERE HangerNo=@HangerNo", new { HangerNo = hangerNo });
            foreach (var wp in hangerProductItemList)
            {
                wp.HangerNo = isCalculationYield ? wp.HangerNo : string.Format("-{0}", wp.HangerNo);
                var sucessHangerProductItem = BeanUitls<DaoModel.SucessHangerProductItem, DaoModel.HangerProductItem>.Mapper(wp);
                DapperHelp.Add<DaoModel.SucessHangerProductItem>(sucessHangerProductItem);
                DapperHelp.Execute("Delete HangerProductItem WHERE HangerNo=@HangerNo", new { HangerNo = hangerNo });
            }
        }
        private DaoModel.ProcessFlowChartFlowRelation GetNextProcessFlow(DaoModel.ProcessFlowChart pfChart, short index)
        {
            var pfcProcessFlowList = CANProductsValidService.Instance.GetFlowChartFlowItemList(pfChart?.Id);
            return pfcProcessFlowList.Where(f => f.CraftFlowNo.Equals(index.ToString())).Single();
        }

        ///// <summary>
        ///// 计算衣架的下一站
        ///// </summary>
        ///// <param name="statingNos"></param>
        ///// <returns></returns>
        //string CalculationNextSating(string statingNos)
        //{
        //    var sql = string.Format("select SiteNo,ISNULL(COUNT(1),0) StatingNum from WaitProcessOrderHanger where SiteNo IN('{0}') and IsIncomeSite=1 Group BY SiteNo", statingNos.Replace(",", "','").Replace(" ", ""));
        //    var wpOrderHanderList = Query<DaoModel.Cus.StatingModel>(new StringBuilder(sql), null, true, null);
        //    if (wpOrderHanderList.Count == 0)
        //    {
        //        return null;
        //    }
        //    return wpOrderHanderList.OrderBy(f => f.StatingNum).SingleOrDefault().SiteNo;
        //}
        //public DaoModel.Products GetProducts(string productionNumber, string hangingPieceSiteNo)
        //{
        //    string queryString = string.Format("select top 1 * from [dbo].[Products] where productionNumber=:productionNumber and hangingPieceSiteNo=:hangingPieceSiteNo");
        //    var session = SessionFactory.OpenSession();
        //    var products = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.Products))
        //        .SetString("productionNumber", productionNumber)
        //        .SetString("hangingPieceSiteNo", hangingPieceSiteNo).UniqueResult<DaoModel.Products>();
        //    return products;
        //}
        //public IList<DaoModel.ProcessFlowStatingItem> GetProcessChartStatingList(string productionNumber, string hangingPieceSiteNo)
        //{
        //    string queryString = string.Format("select top 1 * from [dbo].[Products] where productionNumber=:productionNumber and hangingPieceSiteNo=:hangingPieceSiteNo");
        //    var session = SessionFactory.OpenSession();
        //    var products = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.Products))
        //        .SetString("productionNumber", productionNumber)
        //        .SetString("hangingPieceSiteNo", hangingPieceSiteNo).UniqueResult<DaoModel.Products>();
        //    if (null == products)
        //    {
        //        log.Info(string.Format("排产号【{0}】挂片站【{1}】 找不到上线的产品!", productionNumber, hangingPieceSiteNo));
        //        return null;
        //    }
        //    var processFlowList = CANProductsValidService.Instance.GetFlowChartFlowItemList(products.ProcessFlowChart.Id);
        //    List<DaoModel.ProcessFlowStatingItem> statingList = new List<DaoModel.ProcessFlowStatingItem>();
        //    foreach (var pf in processFlowList)
        //    {
        //        var pcStatList = CANProductsValidService.Instance.GetHangerNextProcessFlowStatingList(pf.ProcessFlowChart?.Id, pf.ProcessFlow?.Id);
        //        var pcStatingArr = new DaoModel.ProcessFlowStatingItem[pcStatList.Count];
        //        statingList.AddRange(pcStatList.ToArray());
        //    }
        //    return statingList;
        //}

        /// <summary>
        /// 卡片登录相关
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="type"></param>
        public void CardLogin(int mainTrackNo, int statingNo, int cardNo, ref int type, ref string other, ref string info)
        {
            ////允许员工从终端登录配置
            //string value = SystemParameterService.Instance.GetHangUpLineProductsLineValue(SystemParameterHangUpProductsLine.CanLoginFromStation, mainTrackNo.ToString(), statingNo.ToString());
            //if (string.IsNullOrEmpty(value) || value == "0")
            //{
            //    throw new CanLoginFromStationException($"【错误】主轨:【{mainTrackNo}】站点:【{statingNo}】不允许员工从终端登录配置");
            //}

            var cardService = new CommonService.CommonServiceImpl<DaoModel.CardInfo>();
            var cardList = DapperHelp.Query<DaoModel.CardInfo>("select * from CardInfo", null).Where(f => f.CardNo.Equals(cardNo.ToString())).ToList<DaoModel.CardInfo>();
            var sewingMachineCardList = DapperHelp.Query<DaoModel.ClothingVehicle>($"select Id from ClothingVehicle where CardNo=@CardNo ", new { CardNo = cardNo });
            // var mechanicEmployeeList = DapperHelp.Query<DaoModel.MechanicEmployees>("select Id from MechanicEmployees where CardNo=@CardNo", new { CardNo = cardNo });
            if ((null == cardList || cardList.Count == 0) && sewingMachineCardList.Count() == 0) //&& mechanicEmployeeList.Count() == 0)
            {
                var ex = new ApplicationException(string.Format("找不到卡号:{0}", cardNo));
                tcpLogError.Error("【卡片请求】", ex);
                throw ex;
            }
            if (sewingMachineCardList.Count() != 0)
            {
                type = 2;
            }
            //else if (mechanicEmployeeList.Count() != 0)
            //{
            //    type = 3;
            //}
            else
            {
                var cardInfo = cardList[0];
                type = (short)cardInfo.CardType;
            }

            switch (type)
            {
                case 1://衣架卡
                    break;
                case 2://衣车卡
                    type = ClothesCarLoginOrLogOutHandler(mainTrackNo, statingNo, cardNo, ref other, ref info);
                    break;
                case 3://机修卡

                    break;
                case 4://员工卡
                    type = EmplyeeCardLoginOrLogOutHandler(mainTrackNo, statingNo, cardNo, ref other, ref info, cardList);
                    break;
            }
        }

        private int ClothesCarLoginOrLogOutHandler(int mainTrackNo, int statingNo, int cardNo, ref string other, ref string info)
        {
            var sewingMachineCardList = DapperHelp.Query<DaoModel.ClothingVehicle>($"select Id,LoginTime,No from ClothingVehicle where CardNo=@CardNo ", new { CardNo = cardNo });
            if (sewingMachineCardList.Count() != 1)
            {
                var ex = new ApplicationException(string.Format("卡号不唯一:{0}", cardNo));
                tcpLogError.Error("【卡片请求】", ex);
                throw ex;
            }
            var currentLoginClothesCar = sewingMachineCardList.First();
            var groupNo = BridgeService.Instance.GetGroupNo(mainTrackNo, statingNo);
            var sewingMachineLoginLog = new DaoModel.SewingMachineLoginLog();
            sewingMachineLoginLog.CardNo = cardNo + "";
            sewingMachineLoginLog.InsertDateTime1 = DateTime.Now;
            sewingMachineLoginLog.ReadCardDateTime = DateTime.Now;
            sewingMachineLoginLog.Workshop = currentLoginClothesCar.WorkShop;
            sewingMachineLoginLog.GroupNo = groupNo;
            sewingMachineLoginLog.MainTrackNumber = mainTrackNo + "";
            sewingMachineLoginLog.StatingNo = statingNo + "";
            sewingMachineLoginLog.SewingMachineNo = currentLoginClothesCar.No;

            if (currentLoginClothesCar.LoginTime == null)
            {
                using (var session = SessionFactory.OpenSession())
                {
                    using (var trans = session.BeginTransaction())
                    {
                        var cvModelLogin = ClothingVehicleDao.Instance.GetById(currentLoginClothesCar.Id);
                        cvModelLogin.LoginTime = DateTime.Now;
                        cvModelLogin.GroupNo = groupNo;
                        cvModelLogin.StatingNo = statingNo + "";
                        ClothingVehicleDao.Instance.Update(cvModelLogin, true);
                        sewingMachineLoginLog.LoginStatus = 0;
                        SewingMachineLoginLogDao.Instance.Save(sewingMachineLoginLog, true);
                        session.Flush();
                        trans.Commit();
                    }
                }
                info = string.Format($"衣车{currentLoginClothesCar.No?.Trim()}已登录站点");
                return 5;
            }
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var cvModel = ClothingVehicleDao.Instance.GetById(currentLoginClothesCar.Id);
                    cvModel.LoginTime = null;
                    cvModel.Times = null;
                    cvModel.GroupNo = null;
                    cvModel.StatingNo = null;
                    ClothingVehicleDao.Instance.Update(cvModel);
                    sewingMachineLoginLog.LoginStatus = 1;
                    SewingMachineLoginLogDao.Instance.Save(sewingMachineLoginLog);
                    session.Flush();
                    trans.Commit();
                }
            }
            //SewingMachineLoginLogDao
            info = string.Format($"衣车{currentLoginClothesCar.No?.Trim()}已登出站点");
            return 6;
        }

        private int EmplyeeCardLoginOrLogOutHandler(int mainTrackNo, int statingNo, int cardNo, ref string other, ref string info, List<DaoModel.CardInfo> cardList)
        {
            int type;
            var sql = "select * from Employee";
            var employeesList = DapperHelp.Query<DaoModel.Employee>(sql, null).Where(f => null != f.CardNo && f.CardNo.Equals(cardNo.ToString())).ToList<DaoModel.Employee>();// EmployeeDao.Instance.GetAll().Where(f => null != f.CardNo && f.CardNo.Equals(cardNo.ToString())).ToList<DaoModel.Employee>();
            if (null == employeesList || employeesList.Count == 0)
            {
                var ex = new ApplicationException(string.Format("找不到卡号:{0}", cardNo));
                tcpLogError.Error("【卡片请求】", ex);
                throw ex;
            }
            var employees = employeesList.First<DaoModel.Employee>(); //EmployeeCardRelationDao.Instance.GetAll().Where(f => f.CardInfo.Id.Equals(cardInfo.Id)).Select(x => x.Employee).First<DaoModel.Employee>();
            if (null != employees)
            {
                other = employees.RealName?.Trim();
            }
            var bToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            var eToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(1);
            var employeeLoginService = new CommonService.CommonServiceImpl<DaoModel.CardLoginInfo>();
            var logInfoList = productsQueryService.GetEmployeeLoginInfoList(mainTrackNo, statingNo.ToString(), cardNo.ToString());

            //员工必须按排班时间登陆
            var EmpLogBySchdule = SystemParameterService.Instance.GetSystemAttendance(SystemParameterSystemAttendance.EmpLogBySchdule);
            if (0 != logInfoList.Count)
            {
                //允许员工多站点登陆
                var CanLoginMultiStation = SystemParameterService.Instance.GetSystemAttendance(SystemParameterSystemAttendance.CanLoginMultiStation);
                if (CanLoginMultiStation == "0" || String.IsNullOrWhiteSpace(CanLoginMultiStation))
                {
                    var loginCount = logInfoList.GroupBy(o => new { o.EmployeeId, o.LoginStatingNo }).Count();
                    if (loginCount != 0)
                    {
                        var loginInfo = logInfoList.First();
                        var loginEmployee = DapperHelp.FirstOrDefault<DaoModel.Employee>("select * From Employee a where a.Id = @Id ", new { Id = logInfoList[0].EmployeeId });
                        var ex = new ApplicationException($"员工已经在主轨【{mainTrackNo}】,站点【{loginInfo.LoginStatingNo}】 登陆");
                        tcpLogError.Error("【卡片请求】", ex);
                        throw ex;
                    }
                }

                //终端有员工登录时，允许其他员工刷卡替换（将站点的所有员工都登出）
                var CanLogoutOtherEmp = SystemParameterService.Instance.GetSystemAttendance(SystemParameterSystemAttendance.CanLogoutOtherEmp);
                if (CanLogoutOtherEmp == "0" || String.IsNullOrWhiteSpace(CanLogoutOtherEmp))
                {
                    var loginEmployee = DapperHelp.FirstOrDefault<DaoModel.Employee>("select * From Employee a where a.Id = @Id ", new { Id = logInfoList[0].EmployeeId });
                    var ex = new ApplicationException(string.Format("已经有员工登陆:{0}", loginEmployee.RealName));
                    tcpLogError.Error("【卡片请求】", ex);
                    throw ex;
                }


                type = CardRequestType.EmployeeLoginOut.Value;
                var cardLogInfoId = logInfoList[0].Id;
                if (!string.IsNullOrEmpty(cardLogInfoId))
                {
                    var cardLogin = CardLoginInfoDao.Instance.GetById(cardLogInfoId);
                    cardLogin.IsOnline = false;
                    cardLogin.LogOutDate = DateTime.Now;
                    CardLoginInfoDao.Instance.Update(cardLogin);
                }

                CompulsionStatingEmployeeOffLine(mainTrackNo, statingNo);
            }
            else
            {
                var cardInfo = cardList[0];
                //根据上下班时间判断下班
                //刷新站点已登录为离线状态,跨天的情况
                CompulsionStatingEmployeeOffLine(mainTrackNo, statingNo);

                type = CardRequestType.EmployeeLogined.Value;
                var cardLoginInfo = new DaoModel.CardLoginInfo();
                cardLoginInfo.MainTrackNumber = mainTrackNo;
                cardLoginInfo.CardInfo = cardInfo;
                cardLoginInfo.LoginDate = DateTime.Now;
                cardLoginInfo.LoginStatingNo = statingNo.ToString();
                cardLoginInfo.IsOnline = true;
                CardLoginInfoDao.Instance.Save(cardLoginInfo);
                info = string.Format("[{0}]{1}", employees.RealName?.Trim(), employees.Code?.Trim());
            }

            return type;
        }

        /// <summary>
        /// 强制已登录的站点的所有员工员工下线
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        public void CompulsionStatingEmployeeOffLine(int mainTrackNumber, int statingNo)
        {
            DapperHelp.Execute(@"Update CardLoginInfo set IsOnline=0,LogOutDate=getDate() WHERE MainTrackNumber=@MainTrackNumber AND LoginStatingNo=@LoginStatingNo AND IsOnline=1", new { MainTrackNumber = mainTrackNumber, LoginStatingNo = statingNo });

            //var sql = "SELECT Id FROM CardLoginInfo WHERE MainTrackNumber=@MainTrackNumber AND LoginStatingNo=@LoginStatingNo AND IsOnline=1";
            //var list = DapperHelp.Query<DaoModel.CardLoginInfo>(sql, new { MainTrackNumber = mainTrackNumber, LoginStatingNo = statingNo });
            //foreach (var cl in list)
            //{
            //    //cl.IsOnline = false;
            //    //cl.LogOutDate = DateTime.Now;
            //    DapperHelp.Execute(@"Update CardLoginInfo set IsOnline=0,LogOutDate=getDate() WHERE MainTrackNumber=@MainTrackNumber AND LoginStatingNo=@LoginStatingNo AND IsOnline=1", new { MainTrackNumber = mainTrackNumber, LoginStatingNo = statingNo });
            //}
        }
        /// <summary>
        /// 更改硬件发送上线的产品的排产号为 当前上线的产品
        /// </summary>
        /// <param name="productNumber"></param>
        /// <returns></returns>
        public bool BindHangpieceStatingOnlineProductNumber(int mainTrackNumber, int productNumber)
        {

            try
            {
                var sqlGroup = string.Format(@"	select distinct Sg.GroupNO from SiteGroup SG
						left join Stating ST ON SG.Id=ST.SITEGROUP_Id
						where  St.MainTrackNumber=@MainTrackNumber"); //new StringBuilder("select GroupNO from SiteGroup where MainTrackNumber=?");
                var siteGroup = QueryForObject<DaoModel.SiteGroup>(sqlGroup, new { MainTrackNumber = mainTrackNumber });
                if (null == siteGroup)
                {
                    var ex = new ApplicationException(string.Format("主轨号:{0}找不到站点组", mainTrackNumber));
                    tcpLogError.Error(ex);
                    throw ex;
                }

                var sql = new StringBuilder("Update Products set Status=:Status1 where Status=:Status2 AND GroupNo=:GroupNo");
                var session = SessionFactory.OpenSession();
                var r = session.CreateQuery(sql.ToString())
                    .SetInt32("Status1", ProductsStatusType.Allocationed.Value)
                    .SetInt32("Status2", ProductsStatusType.Onlineed.Value)
                    .SetString("GroupNo", siteGroup.GroupNo?.Trim())
                    .ExecuteUpdate();
                var sqlProdu = "Update Products set Status=? WHERE ProductionNumber=? AND GroupNo=?";
                ExecuteUpdate(sqlProdu, ProductsStatusType.Onlineed.Value, productNumber, siteGroup.GroupNo?.Trim());

                SusCacheBootstarp.Instance.Init();

            }
            catch (Exception ex)
            {
                var exx = new ApplicationException("挂片站上线排产号变更为当前上线产品错误:{0}", ex);
                log.Error(ex);
                throw exx;
            }
            return true;
        }
        public DaoModel.DefectCodeTableModel GetDefectInfo(string defectNoOrDefectCode)
        {
            var dicDefectCodeTableCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DaoModel.DefectCodeTableModel>(SusRedisConst.DEFECT_CODE_TABLE);
            if (dicDefectCodeTableCache.ContainsKey(defectNoOrDefectCode))
            {
                return dicDefectCodeTableCache[defectNoOrDefectCode];
            }
            errorLog.InfoFormat("疵点代码或者序号:{0} 未找到疵点缓存", defectNoOrDefectCode);
            return null;
        }
        /// <summary>
        /// 是否是桥接站
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <returns></returns>
        public bool IsBridge(int mainTrackNumber, int statingNo)
        {
            lock (locObject)
            {
                var bridgeCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<int, DaoModel.BridgeSet>(SusRedisConst.BRIDGE_SET);
                if (!bridgeCache.ContainsKey(mainTrackNumber)) return false;
                var bridge = bridgeCache[mainTrackNumber];
                if (bridge.AMainTrackNumber.Value == mainTrackNumber && bridge.ASiteNo.Value == statingNo)
                {
                    return true;
                }
                return false;
            }
        }
    }
    class RecordEmployeeFlowProductionModel
    {
        protected static readonly ILog log = LogManager.GetLogger("LogLogger");
        protected static readonly ILog errorLog = LogManager.GetLogger("Error");
        public bool IsEndFlow { set; get; }
        public string StatingNo { set; get; }
        public string HangerNo { set; get; }
        public int MainTrackNumber { set; get; }
        public string StatingHangerProductItemId { get; internal set; }

        //记录员工产量
        public void RecordEmployeeFlowProduction()
        {
            var pQueryService = new ProductsQueryServiceImpl();
            //pQueryService.CheckStatingIsLogin(StatingNo);
            var statingEmList = pQueryService.GetEmployeeLoginInfoList(StatingNo, MainTrackNumber);
            var emStatingInfo = statingEmList[0];
            var hsaItem = StatingHangerProductItemDao.Instance.GetById(StatingHangerProductItemId);
            var efpModel = BeanUitls<DaoModel.EmployeeFlowProduction, DaoModel.StatingHangerProductItem>.Mapper(hsaItem);
            efpModel.Id = null;
            efpModel.CardNo = emStatingInfo.CardNo;
            efpModel.EmployeeId = emStatingInfo.EmployeeId;
            efpModel.EmployeeName = emStatingInfo.RealName;
            EmployeeFlowProductionDao.Instance.Save(efpModel);

            var ppChart = CANProductsService.Instance.GetCompleteHangerProductFlowChart(MainTrackNumber, HangerNo, StatingNo);
            if (null == ppChart)
            {
                var ex = new ApplicationException(string.Format("找不着衣架生产工艺图信息! 主轨:{0} 衣架号:{1} 站点:{2}", MainTrackNumber, HangerNo, StatingNo));
                errorLog.Error("【衣架出站】", ex);
                return;
            }
            ppChart.EmployeeName = emStatingInfo.RealName;
            ppChart.CardNo = emStatingInfo.CardNo;
            HangerProductFlowChartDao.Instance.Update(ppChart);

            if (IsEndFlow)
            {
                // CANProductsService.Instance.CopyHangerProductChart(HangerNo);
            }
        }

        /// <summary>
        /// 修改/新增站点类型
        /// </summary>
        /// <param name="mainTrackNo">主轨号</param>
        /// <param name="statingNo">站点号</param>
        /// <param name="type">操作类型 01/02</param>
        /// <param name="statingType">站点类型</param>
        public void ModifyStatingType(string mainTrackNo, string statingNo, string type, string statingType)
        {

        }

        /// <summary>
        /// 修改站点容量
        /// </summary>
        /// <param name="mainTrackNo">主轨号</param>
        /// <param name="statingNo">站点号</param>
        /// <param name="capacity">容量</param>
        public void ModifyStatingCapacity(string mainTrackNo, string statingNo, string capacity)
        {

        }

    }
}
