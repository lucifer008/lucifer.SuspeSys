using log4net;
using StackExchange.Redis.DataTypes.Collections;
using Sus.Net.Common.Constant;
using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.Ext.CANModel;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.Common;
using SuspeSys.Service.Impl.CommonService;
using SuspeSys.Service.Impl.Permisson;
using SuspeSys.Service.Impl.ProductionLineSet;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.Products.SusThread;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuspeSys.Service.Impl.Products.SusCache.Service
{
    public class SusCacheProductService : ServiceBase
    {
        new protected static readonly ILog redisLog = LogManager.GetLogger("RedisLogInfo");

        private SusCacheProductService() { }
        public static SusCacheProductService Instance { get { return new SusCacheProductService(); } }

        /// <summary>
        /// 启动加载上线制品到cache
        /// </summary>
        public void LoadOnLineProductsFlowChart(string processFlowChartId = null)
        {
            string onlineSql = string.Empty;
            IList<SuspeSys.Domain.ProductsModel> list = null;
            if (string.IsNullOrEmpty(processFlowChartId))
            {
                onlineSql = string.Format(@"select GroupNo,ProductionNumber,PROCESSFLOWCHART_Id ProcessFlowChartId,PROCESSORDER_Id ProcesOrderId,HangingPieceSiteNo from products where status=?");
                list = Query<SuspeSys.Domain.ProductsModel>(onlineSql, true, ProductsStatusType.Onlineed.Value);
            }
            else
            {
                onlineSql = string.Format(@"select GroupNo,ProductionNumber,PROCESSFLOWCHART_Id ProcessFlowChartId,PROCESSORDER_Id ProcesOrderId,HangingPieceSiteNo from products where status=? and PROCESSFLOWCHART_Id=?");
                list = Query<SuspeSys.Domain.ProductsModel>(onlineSql, true, ProductsStatusType.Onlineed.Value, processFlowChartId);
            }

            foreach (var p in list)
            {
                if (string.IsNullOrEmpty(p.HangingPieceSiteNo))
                {
                    var info = string.Format("组:{0}  挂片站为空!", p.GroupNo?.Trim());
                    redisLog.Info(info);
                    continue;
                }


                var sqlGroup = //new StringBuilder("select MainTrackNumber from SiteGroup where GroupNO=?");
                    string.Format(@"select Sg.GroupNO,St.MainTrackNumber,st.StatingNo,st.StatingName from SiteGroup SG
						left join Stating ST ON SG.Id=ST.SITEGROUP_Id
						where Sg.GroupNo=@GroupNo And st.StatingNo=@StatingNo and ST.Deleted=0 and st.StatingName='挂片站'");
                var listCheck = QueryForList<SiteGroup>(sqlGroup, new { GroupNo = p.GroupNo?.Trim(), StatingNo = p.HangingPieceSiteNo?.Trim() });
                if (listCheck.Count == 0)
                {
                    sqlGroup = //new StringBuilder("select MainTrackNumber from SiteGroup where GroupNO=?");
                    string.Format(@"select Sg.GroupNO,St.MainTrackNumber,st.StatingNo,st.StatingName from SiteGroup SG
						left join Stating ST ON SG.Id=ST.SITEGROUP_Id
						where Sg.GroupNo=@GroupNo And st.StatingNo=@StatingNo and ST.Deleted=0");
                    listCheck = QueryForList<SiteGroup>(sqlGroup, new { GroupNo = p.GroupNo?.Trim(), StatingNo = p.HangingPieceSiteNo?.Trim() });
                    if (listCheck.Count == 0)
                    {
                        var exe = new ApplicationException($"【{ p.GroupNo?.Trim()}】组产品 排产号:【{p.ProductionNumber}】的 找不到挂片站点【{p.HangingPieceSiteNo?.Trim()}】!");
                        tcpLogError.Error(exe);
                        continue;
                    }
                    var lcc = listCheck[0];
                    var key1 = string.Format("{0}:{1}", lcc.MainTrackNumber.Value, p.ProductionNumber);
                    var ex = new ApplicationException($"【{ p.GroupNo?.Trim()}】组产品 排产号:【{p.ProductionNumber}】的 产品挂片站已变更为非挂片站！请修正产品的挂片站! 上线产品已清除!");
                    tcpLogError.Error(ex);
                    var dicProductsFlowChartCacheTempModel = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, IList<ProductsFlowChartCacheTempModel>>(SusRedisConst.ON_LINE_PRODUCTS_FLOW_CHART);
                    if (dicProductsFlowChartCacheTempModel.Count > 0)
                    {
                        dicProductsFlowChartCacheTempModel.Remove(key1);
                    }
                    continue;
                }
                if (listCheck.Count > 1)
                {
                    var exInit = new ApplicationException(string.Format("组:{0} 挂片站：{1} 出现多个主轨!", p.GroupNo?.Trim(), p.HangingPieceSiteNo?.Trim()));
                    redisLog.Error(exInit);
                    throw exInit;
                }
                var sgg = listCheck[0];
                var key = string.Format("{0}:{1}", sgg.MainTrackNumber.Value, p.ProductionNumber);
                //var key = string.Format("{0}:{1}", siteGroup.MainTrackNumber.Value, p.ProductionNumber);
                //if (!ProductsCache.OnlineProductsList.ContainsKey(key))
                //{
                //    ProductsCache.OnlineProductsList.Add(key, new Model.ProductsCacheModel()
                //    {
                //        ProductNumber = p.ProductionNumber.Value,
                //        MainTrackNumber = (int)siteGroup.MainTrackNumber,
                //        OnlineProducts = p,
                //        CProcessFlowChartModelList = CacheService.Instance.CacheProcessFlowChart(p.ProcessFlowChartId)
                //    });
                //}
                //else {
                //    ProductsCache.OnlineProductsList[key].OnlineProducts = p;
                //    ProductsCache.OnlineProductsList[key].CProcessFlowChartModelList = CacheService.Instance.CacheProcessFlowChart(p.ProcessFlowChartId);
                //}
                //var flowChartCache = SusCacheMain.Cache.Get(key);
                //if (flowChartCache != null)
                //{
                //    SusCacheMain.Cache.Remove(key);
                //}
                var flowChartModelList = GetProductsFlowChartCacheModel(p.ProcessFlowChartId);
                //SusCacheMain.Cache.Insert(key, flowChartModel);
                try
                {
                    var dicProductsFlowChartCacheTempModel = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, IList<ProductsFlowChartCacheTempModel>>(SusRedisConst.ON_LINE_PRODUCTS_FLOW_CHART);
                    if (dicProductsFlowChartCacheTempModel.Count > 0)
                    {
                        dicProductsFlowChartCacheTempModel.Remove(key);
                    }
                    NewSusRedisClient.RedisTypeFactory.GetDictionary<string, IList<ProductsFlowChartCacheTempModel>>(SusRedisConst.ON_LINE_PRODUCTS_FLOW_CHART).Add(key, flowChartModelList);

                }
                catch (Exception ex)
                {
                    redisLog.Error(ex);
                }
            }

            //加载角色

        }

        internal void LoadFaultCodeSecondAddressMapping()
        {
            try
            {
                var clothVehileList = DapperHelp.Query<ClothingVehicleType>(@"select * from ClothingVehicleType fct");
                var faultCodeList = DapperHelp.Query<FaultCodeTableModel>(@"select *,CLOTHINGVEHICLETYPE_Id ClothingVehicleTypeId from FaultCodeTable fct");

                var dicFaultCodeAndSecondAddressMappingCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, IList<int>>(SusRedisConst.Fault_Code_AND_Second_Address_Mapping);
                if (dicFaultCodeAndSecondAddressMappingCache.Count > 0)
                {
                    NewSusRedisClient.RedisTypeFactory.GetDictionary<string, IList<int>>(SusRedisConst.Fault_Code_AND_Second_Address_Mapping).Clear();
                    //return;
                }
                //SusRedisConst.Fault_Code_AND_First_Address_Mapping;
              
                foreach (var cv in clothVehileList)
                {
                    var faultCodeSecondMenuStart = SuspeConstants.address_fault_code_second_menu_start;
                    var faultCodeSecondMenuEnd = SuspeConstants.address_fault_code_second_menu_end;
                    var cvFaultCodeList = faultCodeList.Where(f => f.ClothingVehicleTypeId.Equals(cv.Id)).ToList();
                    if (cvFaultCodeList.Count > faultCodeSerialNumberLength)
                    {
                        var exx = new ApplicationException("故障二级菜单地址超出!");
                        throw exx;
                    }
                    foreach (var fcc in cvFaultCodeList)
                    {
                        var addressList = new List<int>();
                        for (var yIndex = 0; yIndex < 7; yIndex++)
                        {
                            addressList.Add(faultCodeSecondMenuStart);
                            faultCodeSecondMenuStart++;
                        }
                        dicFaultCodeAndSecondAddressMappingCache.Add(cv.Code?.Trim()+"-"+fcc.SerialNumber?.Trim(), addressList);
                    }
                    if (faultCodeSecondMenuStart > faultCodeSecondMenuEnd)
                    {
                        var exx = new ApplicationException("故障二级菜单地址超出!");
                        throw exx;
                    }
                }

                //for (var index = 1; index <= faultCodeSerialNumberLength; index++)
                //{
                //    if (faultCodeSecondMenuStart > faultCodeSecondMenuEnd)
                //    {
                //        var exx = new ApplicationException("故障二级菜单地址超出!");
                //        throw exx;
                //    }
                //    var addressList = new List<int>();
                //    for (var yIndex = 0; yIndex < 7; yIndex++)
                //    {
                //        addressList.Add(faultCodeSecondMenuStart);
                //        faultCodeSecondMenuStart++;
                //    }
                //    dicFaultCodeAndSecondAddressMappingCache.Add(index + "", addressList);
                //}
            }
            catch (Exception ex)
            {
                redisLog.Error(ex);
            }
        }

        readonly static int clothTypeSerialNumberLength = 10;
        readonly static int faultCodeSerialNumberLength = 16;
        internal void LoadFaultCodeFirstAddressMapping()
        {
            try
            {
                var dicFaultCodeAndFirstAddressMappingCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, IList<int>>(SusRedisConst.Fault_Code_AND_First_Address_Mapping);
                if (dicFaultCodeAndFirstAddressMappingCache.Count > 0)
                {
                    NewSusRedisClient.RedisTypeFactory.GetDictionary<string, IList<int>>(SusRedisConst.Fault_Code_AND_First_Address_Mapping).Clear();
                    //return;
                }
                //SusRedisConst.Fault_Code_AND_First_Address_Mapping;
                var faultCodeFirstMenuStart = SuspeConstants.address_fault_code_first_menu_start;
                var faultCodeFirstMenuEnd = SuspeConstants.address_fault_code_first_menu_end;
                for (var index = 1; index <= clothTypeSerialNumberLength; index++)
                {
                    if (faultCodeFirstMenuStart > faultCodeFirstMenuEnd)
                    {
                        var exx = new ApplicationException("故障一级菜单地址超出!");
                        throw exx;
                    }
                    var addressList = new List<int>();
                    for (var yIndex = 0; yIndex < 7; yIndex++)
                    {
                        addressList.Add(faultCodeFirstMenuStart);
                        faultCodeFirstMenuStart++;
                    }
                    dicFaultCodeAndFirstAddressMappingCache.Add(index + "", addressList);
                }
            }
            catch (Exception ex)
            {
                redisLog.Error(ex);
            }
        }

        /// <summary>
        /// 加载桥接
        /// </summary>
        internal void LoadBridgeSet()
        {
            // string sql = string;
            // throw new NotImplementedException();
            try
            {
                var sql = string.Format("select * from BridgeSet where Enabled=1");
                var bridgeList = QueryForList<BridgeSet>(sql, null);
                if (bridgeList.Count == 0)
                {
                    redisLog.InfoFormat("无桥接配置");
                    return;
                }
                var dicBridge = NewSusRedisClient.RedisTypeFactory.GetDictionary<int, BridgeSet>(SusRedisConst.BRIDGE_SET);

                foreach (var b in bridgeList)
                {

                    if (b.Direction.Value == 0)//双向
                    {
                        if (dicBridge.ContainsKey(b.AMainTrackNumber.Value))
                        {
                            NewSusRedisClient.RedisTypeFactory.GetDictionary<int, BridgeSet>(SusRedisConst.BRIDGE_SET)[b.AMainTrackNumber.Value] = b;
                            tcpLogInfo.InfoFormat("主轨{0}桥接信息加载成功!桥接信息={1}", b.AMainTrackNumber.Value, Newtonsoft.Json.JsonConvert.SerializeObject(b));
                            //continue;
                        }
                        else
                        {
                            NewSusRedisClient.RedisTypeFactory.GetDictionary<int, BridgeSet>(SusRedisConst.BRIDGE_SET).Add(b.AMainTrackNumber.Value, b);
                            tcpLogInfo.InfoFormat("主轨{0}桥接信息加载成功!桥接信息={1}", b.AMainTrackNumber.Value, Newtonsoft.Json.JsonConvert.SerializeObject(b));
                        }
                        var dBridgeInfo = new BridgeSet();
                        dBridgeInfo.AMainTrackNumber = b.BMainTrackNumber.Value;
                        dBridgeInfo.BMainTrackNumber = b.AMainTrackNumber.Value;
                        dBridgeInfo.Direction = b.Direction;
                        dBridgeInfo.DirectionTxt = b.DirectionTxt;
                        dBridgeInfo.ASiteNo = b.BSiteNo;
                        dBridgeInfo.BSiteNo = b.ASiteNo;

                        if (dicBridge.ContainsKey(b.BMainTrackNumber.Value))
                        {
                            NewSusRedisClient.RedisTypeFactory.GetDictionary<int, BridgeSet>(SusRedisConst.BRIDGE_SET)[b.BMainTrackNumber.Value] = dBridgeInfo;
                            tcpLogInfo.InfoFormat("主轨{0}桥接信息加载成功!桥接信息={1}", b.BMainTrackNumber.Value, Newtonsoft.Json.JsonConvert.SerializeObject(dBridgeInfo));
                            //continue;
                        }
                        else
                        {
                            NewSusRedisClient.RedisTypeFactory.GetDictionary<int, BridgeSet>(SusRedisConst.BRIDGE_SET).Add(b.BMainTrackNumber.Value, dBridgeInfo);
                            tcpLogInfo.InfoFormat("主轨{0}桥接信息加载成功!桥接信息={1}", b.BMainTrackNumber.Value, Newtonsoft.Json.JsonConvert.SerializeObject(dBridgeInfo));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                redisLog.ErrorFormat("桥接配置加载错误:{0}", ex);
            }
        }

        /// <summary>
        /// 同步工艺图到生产环境中生产中的衣架工艺缓存
        /// </summary>
        /// <param name="flowChartId"></param>
        internal void UpdateProductingHangerFlowChartCache(string flowChartId)
        {
            try
            {
                //var commonQueryService = new CommonServiceImpl<Stating>();
                //var statingList = commonQueryService.GetList();

                var updateFlowChartModelList = GetProductsFlowChartCacheModel(flowChartId);

                var dicHangerFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                var keys = dicHangerFlowChartCache.Keys;
                foreach (var key in keys)
                {
                    var cacheFlowChartList = dicHangerFlowChartCache[key];
                    //是否是工艺图变更影响到的衣架
                    var isContains = cacheFlowChartList.Where(f => f.ProcessChartId.Equals(flowChartId)).Count() > 0;
                    if (!isContains)
                    {
                        continue;
                    }

                    // var isUpdate = false;
                    //最后一道工序是否完成
                    var maxFlowIndex = cacheFlowChartList.Select(f => f.FlowIndex.Value).Max();
                    var isEndFlowSucessed = cacheFlowChartList.Where(f => f.Status.Value == HangerProductFlowChartStaus.Successed.Value && f.StatingNo.Value != -1
                    && null == f.MergeProcessFlowChartFlowRelationId && f.FlowIndex.Value == maxFlowIndex).Count() > 0;
                    if (isEndFlowSucessed)
                    {
                        continue;
                    }
                    var flowNoList = new List<string>();
                    var nonSucessMainTrackStatingFitler = new List<string>();
                    var nonSucessMainTrackStatingList = new List<FlowMainTrackStating>();
                    for (var index = 0; index < cacheFlowChartList.Count; index++)
                    {
                        var fc = cacheFlowChartList[index];
                        //工序是否已经生产完成
                        var isFlowSucessed = cacheFlowChartList.Where(f => f.FlowNo.Equals(fc.FlowNo) && f.Status.Value == HangerProductFlowChartStaus.Successed.Value).Count() > 0;
                        //是否存在返工待完成
                        var isExistReworkFlow = cacheFlowChartList.Where(f => f.FlowNo.Equals(fc.FlowNo) && f.Status.Value != HangerProductFlowChartStaus.Successed.Value && f.FlowType == 1).Count() > 0;
                        if (isFlowSucessed && !isExistReworkFlow) continue;
                        var mainStatingNo = string.Format("{0}:{1}:{2}", fc.FlowNo?.Trim(), fc.MainTrackNumber.Value, fc.StatingNo.Value);
                        if (!nonSucessMainTrackStatingFitler.Contains(mainStatingNo))
                        {
                            nonSucessMainTrackStatingFitler.Add(mainStatingNo);
                            nonSucessMainTrackStatingList.Add(new FlowMainTrackStating()
                            {
                                FlwoNo = fc.FlowNo?.Trim(),
                                MainTrackNumber = fc.MainTrackNumber.Value,
                                StatingNo = fc.StatingNo.Value
                            });
                        }
                        if (!flowNoList.Contains(fc.FlowNo))
                            flowNoList.Add(fc.FlowNo);
                    }
                    var cacheFlowNoList = cacheFlowChartList.Select(f => f.FlowNo).ToList<string>();
                    var newFlowNoList = updateFlowChartModelList.Select(f => f.FlowNo).ToList<string>();
                    //取交集
                    var intersectFlowNoList = newFlowNoList.Intersect(cacheFlowNoList);

                    var waitUpdateStatingFlowChartList = updateFlowChartModelList.Where(f => flowNoList.Contains(f.FlowNo) && intersectFlowNoList.Contains(f.FlowNo));
                    //检查工序站点是否被deleted掉
                    nonSucessMainTrackStatingList.ForEach(delegate (FlowMainTrackStating fmts)
                    {
                        var isExist = waitUpdateStatingFlowChartList.Where(f => null != f.StatingNo
                        && f.MainTrackNumber == fmts.MainTrackNumber && f.FlowNo.Equals(fmts.FlwoNo) && short.Parse(f.StatingNo) == fmts.StatingNo).Count() > 0;
                        if (!isExist)
                        {
                            fmts.IsDeleted = true;
                        }
                    });

                    #region//1.工序站点update及add
                    foreach (var uFlowChart in waitUpdateStatingFlowChartList)
                    {
                        var condi = cacheFlowChartList.Where(f => f.MainTrackNumber.Value == uFlowChart.MainTrackNumber
                          && null != uFlowChart.StatingNo
                          && f.StatingNo.Value == short.Parse(uFlowChart.StatingNo) && f.FlowNo.Equals(uFlowChart.FlowNo));

                        var isStatingAdd = condi.Count() > 0;

                        if (!isStatingAdd)
                        {
                            var pf = uFlowChart;
                            var prodingHangerFlowChart = cacheFlowChartList.First();
                            var hpfc = new HangerProductFlowChartModel();

                            #region //属性值
                            hpfc.HangerNo = prodingHangerFlowChart.HangerNo;
                            hpfc.BatchNo = prodingHangerFlowChart.BatchNo;
                            hpfc.MainTrackNumber = (short)pf.MainTrackNumber;
                            hpfc.ProductsId = prodingHangerFlowChart.ProductsId;
                            hpfc.IsHangerSucess = false;
                            hpfc.IsFlowSucess = false;
                            hpfc.ProcessChartId = pf.ProcessFlowChartId;
                            hpfc.FlowIndex = short.Parse(pf.CraftFlowNo);
                            hpfc.FlowId = pf.ProcessFlowId;
                            hpfc.FlowNo = pf.FlowNo;
                            hpfc.FlowCode = pf.FlowCode;
                            hpfc.FlowName = pf.FlowName;
                            hpfc.StanardHours = pf.StanardHours;
                            hpfc.StandardPrice = pf.StandardPrice;
                            hpfc.StatingNo = short.Parse(string.IsNullOrEmpty(pf.StatingNo) ? "-1" : pf.StatingNo);
                            hpfc.StatingId = pf.StatingId;
                            hpfc.StatingCapacity = pf.StatingCapacity;
                            hpfc.IsReworkSourceStating = false;
                            hpfc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                            hpfc.StatingRoleCode = pf.StatingRoleCode;
                            hpfc.FlowType = 0;
                            hpfc.PColor = prodingHangerFlowChart?.PColor;
                            hpfc.PSize = prodingHangerFlowChart?.PSize;
                            hpfc.ProcessOrderNo = prodingHangerFlowChart?.ProcessOrderNo?.Trim();
                            hpfc.Num = prodingHangerFlowChart?.Num;
                            hpfc.StyleNo = prodingHangerFlowChart?.StyleNo;

                            hpfc.IsReceivingAllColor = pf.IsReceivingAllColor;
                            hpfc.IsReceivingAllSize = pf.IsReceivingAllSize;
                            hpfc.IsReceivingAllPoNumber = pf.IsReceivingAllPoNumber;
                            hpfc.ReceivingColor = pf.ReceivingColor;
                            hpfc.ReceivingPoNumber = pf.ReceivingPoNumber;
                            hpfc.ReceivingSize = pf.ReceivingSize;
                            hpfc.IsEnabled = pf.IsEnabled;
                            hpfc.Proportion = pf.Proportion;  //分摊比例
                                                              //HangerProductFlowChartDao.Instance.Insert(hpfc);
                            hpfc.IsReceivingHanger = pf.IsReceivingHanger;
                            hpfc.IsReceivingHangerStating = pf.IsReceivingHangerStating;

                            hpfc.IsMergeForward = pf.IsMergeForward;
                            hpfc.MergeProcessFlowChartFlowRelationId = pf.MergeProcessFlowChartFlowRelationId;
                            hpfc.ProcessFlowChartFlowRelationId = pf.ProcessFlowChartFlowRelationId;
                            hpfc.IsProduceFlow = pf.IsProduceFlow;
                            hpfc.MergeFlowNo = pf.MergeFlowNo;
                            // hpfc.ProcessOrderNo = pf.ProcessOrderNo?.Trim();
                            hpfc.LineName = pf.LineName;

                            // hpfc.Num = pf.Num;
                            // hpfc.StatingRoleCode = pf.StatingRoleCode?.Trim();

                            hpfc.IsFlowChartChangeUpdate = true;
                            hpfc.FlowChartChangeUpdateDate = DateTime.Now;
                            #endregion

                            cacheFlowChartList.Add(hpfc);
                            continue;
                        }
                        else
                        {
                            HangerProductFlowChartModel hpfc = null;
                            var pf = uFlowChart;
                            var cFlowCahrtList = cacheFlowChartList.Where(f => f.MainTrackNumber.Value == uFlowChart.MainTrackNumber
                             && f.StatingNo != null && f.StatingNo.Value == int.Parse(uFlowChart.StatingNo)
                             && f.FlowNo.Equals(uFlowChart.FlowNo)
                            );
                            if (cFlowCahrtList.Count() > 0)
                            {
                                hpfc = cacheFlowChartList.Where(f => f.MainTrackNumber.Value == uFlowChart.MainTrackNumber
                            && f.StatingNo != null && f.StatingNo.Value == int.Parse(uFlowChart.StatingNo)
                            && f.FlowNo.Equals(uFlowChart.FlowNo)
                            ).First();
                            }
                            else
                            {
                                continue;
                            }

                            #region //属性值

                            hpfc.IsHangerSucess = false;
                            hpfc.IsFlowSucess = false;

                            hpfc.StanardHours = pf.StanardHours;
                            hpfc.StandardPrice = pf.StandardPrice;

                            hpfc.StatingCapacity = pf.StatingCapacity; ;
                            hpfc.IsReceivingAllColor = pf.IsReceivingAllColor;
                            hpfc.IsReceivingAllSize = pf.IsReceivingAllSize;
                            hpfc.IsReceivingAllPoNumber = pf.IsReceivingAllPoNumber;
                            hpfc.ReceivingColor = pf.ReceivingColor;
                            hpfc.ReceivingPoNumber = pf.ReceivingPoNumber;
                            hpfc.ReceivingSize = pf.ReceivingSize;
                            hpfc.IsEnabled = pf.IsEnabled;
                            hpfc.Proportion = pf.Proportion;  //分摊比例
                                                              //HangerProductFlowChartDao.Instance.Insert(hpfc);
                            hpfc.IsReceivingHanger = pf.IsReceivingHanger;
                            hpfc.IsReceivingHangerStating = pf.IsReceivingHangerStating;
                            hpfc.IsMergeForward = pf.IsMergeForward;
                            hpfc.MergeProcessFlowChartFlowRelationId = pf.MergeProcessFlowChartFlowRelationId;
                            hpfc.ProcessFlowChartFlowRelationId = pf.ProcessFlowChartFlowRelationId;
                            hpfc.IsProduceFlow = pf.IsProduceFlow;
                            hpfc.MergeFlowNo = pf.MergeFlowNo;
                            hpfc.StatingRoleCode = pf.StatingRoleCode?.Trim();

                            hpfc.IsFlowChartChangeUpdate = true;
                            hpfc.FlowChartChangeUpdateDate = DateTime.Now;
                            //cacheFlowChartList.ForEach(delegate(HangerProductFlowChartModel model) {
                            //    if (model.FlowNo.Equals(hpfc.FlowNo) && null!= model.StatingNo && model.StatingNo.Value== hpfc.StatingNo.Value &&
                            //    model.MainTrackNumber.Value==hpfc.MainTrackNumber.Value
                            //    ) {
                            //        model = hpfc;
                            //    }
                            //});
                            #endregion
                        }
                    }
                    #endregion

                    #region//2.工序新增
                    //var isRemove= hFlowChartList.Where(f => f.FlowNo.Equals(fc.FlowNo)
                    var excptFlowNoList = newFlowNoList.Except(cacheFlowNoList);
                    var addFlowChartList = updateFlowChartModelList.Where(f => excptFlowNoList.Contains(f.FlowNo));
                    foreach (var uFlowChart in addFlowChartList)
                    {
                        var pf = uFlowChart;
                        var prodingHangerFlowChart = cacheFlowChartList.First();
                        var hpfc = new HangerProductFlowChartModel();

                        #region //属性值
                        hpfc.HangerNo = prodingHangerFlowChart.HangerNo;
                        hpfc.BatchNo = prodingHangerFlowChart.BatchNo;
                        hpfc.MainTrackNumber = (short)pf.MainTrackNumber;
                        hpfc.ProductsId = prodingHangerFlowChart.ProductsId;
                        hpfc.IsHangerSucess = false;
                        hpfc.IsFlowSucess = false;
                        hpfc.ProcessChartId = pf.ProcessFlowChartId;
                        hpfc.FlowIndex = short.Parse(pf.CraftFlowNo);
                        hpfc.FlowId = pf.ProcessFlowId;
                        hpfc.FlowNo = pf.FlowNo;
                        hpfc.FlowCode = pf.FlowCode;
                        hpfc.FlowName = pf.FlowName;
                        hpfc.StanardHours = pf.StanardHours;
                        hpfc.StandardPrice = pf.StandardPrice;
                        hpfc.StatingNo = short.Parse(string.IsNullOrEmpty(pf.StatingNo) ? "-1" : pf.StatingNo);
                        hpfc.StatingId = pf.StatingId;
                        hpfc.StatingCapacity = pf.StatingCapacity;
                        hpfc.IsReworkSourceStating = false;
                        hpfc.Status = HangerProductFlowChartStaus.WaitingProducts.Value;
                        hpfc.FlowType = 0;
                        hpfc.PColor = pf.PColor;
                        hpfc.PSize = pf.PSize;
                        hpfc.IsReceivingAllColor = pf.IsReceivingAllColor;
                        hpfc.IsReceivingAllSize = pf.IsReceivingAllSize;
                        hpfc.IsReceivingAllPoNumber = pf.IsReceivingAllPoNumber;
                        hpfc.ReceivingColor = pf.ReceivingColor;
                        hpfc.ReceivingPoNumber = pf.ReceivingPoNumber;
                        hpfc.ReceivingSize = pf.ReceivingSize;
                        hpfc.IsEnabled = pf.IsEnabled;
                        hpfc.Proportion = pf.Proportion;  //分摊比例
                                                          //HangerProductFlowChartDao.Instance.Insert(hpfc);
                        hpfc.IsReceivingHanger = pf.IsReceivingHanger;
                        hpfc.IsReceivingHangerStating = pf.IsReceivingHangerStating;

                        hpfc.IsMergeForward = pf.IsMergeForward;
                        hpfc.MergeProcessFlowChartFlowRelationId = pf.MergeProcessFlowChartFlowRelationId;
                        hpfc.ProcessFlowChartFlowRelationId = pf.ProcessFlowChartFlowRelationId;
                        hpfc.IsProduceFlow = pf.IsProduceFlow;
                        hpfc.MergeFlowNo = pf.MergeFlowNo;
                        hpfc.ProcessOrderNo = pf.ProcessOrderNo?.Trim();
                        hpfc.LineName = pf.LineName;
                        hpfc.StyleNo = pf.StyleNo;
                        hpfc.Num = pf.Num;
                        hpfc.StatingRoleCode = pf.StatingRoleCode?.Trim();

                        hpfc.IsFlowChartChangeUpdate = true;
                        hpfc.FlowChartChangeUpdateDate = DateTime.Now;
                        #endregion

                        cacheFlowChartList.Add(hpfc);
                        // isUpdate = true;
                    }
                    #endregion

                    #region//3.工序删除
                    //移除缓存的工序
                    var delFlowNoList = cacheFlowNoList.Except(newFlowNoList);
                    cacheFlowChartList.RemoveAll(f => delFlowNoList.Contains(f.FlowNo));

                    foreach (var fmtc in nonSucessMainTrackStatingList.Where(f => f.IsDeleted).ToList<FlowMainTrackStating>())
                    {
                        cacheFlowChartList.RemoveAll(f => f.FlowNo.Equals(fmtc.FlwoNo) && f.MainTrackNumber.Value == fmtc.MainTrackNumber && f.StatingNo != null && f.StatingNo.Value == fmtc.StatingNo);
                    }
                    #endregion

                    #region 更新工序顺序
                    foreach (var cfc in cacheFlowChartList.Where(f => f.FlowIndex.Value != 1))
                    {
                        foreach (var ufc in updateFlowChartModelList.Where(k => int.Parse(k.CraftFlowNo) != 1))
                        {
                            if (cfc.FlowNo.Equals(ufc.FlowNo))
                            {
                                if (cfc.FlowIndex.Value != int.Parse(ufc.CraftFlowNo))
                                {
                                    cfc.FlowIndex = int.Parse(ufc.CraftFlowNo);
                                    break;
                                }
                            }
                        }
                    }

                    #endregion


                    //校正当前衣架工序顺序缓存
                    //工序顺序下移，同时校正当前工序衣架的工序顺序:下移
                    dicHangerFlowChartCache[key] = cacheFlowChartList;
                    cacheFlowChartList.ForEach(delegate (HangerProductFlowChartModel model)
                    {
                        var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                        if (dicCurrentHangerProductingFlowModelCache.ContainsKey(key))
                        {
                            var current = dicCurrentHangerProductingFlowModelCache[key];
                            if (current.FlowNo.Equals(model.FlowNo) && current.MainTrackNumber == model.MainTrackNumber.Value && current.StatingNo == model.StatingNo.Value && model.FlowIndex.Value > current.FlowIndex)
                            {
                                current.FlowIndex = model.FlowIndex.Value;
                                dicCurrentHangerProductingFlowModelCache[key] = current;
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                redisLog.ErrorFormat("【同步工艺图到生产环境中生产中的衣架工艺缓存】错误{0}", ex);
            }
        }

        /// <summary>
        /// 更新生产中衣架工艺图的站点状态
        /// </summary>
        /// <param name="processFlowChartId"></param>
        public void UpdateHangerFlowChartCache(string processFlowChartId = null)
        {
            try
            {
                var commonQueryService = new CommonServiceImpl<Stating>();
                var statingList = commonQueryService.GetList();

                var dicHangerFlowChartCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                foreach (var key in dicHangerFlowChartCache.Keys)
                {
                    var hFlowChartList = dicHangerFlowChartCache[key];

                    var isUpdate = false;
                    foreach (var fc in hFlowChartList)
                    {
                        var sttList = statingList.Where(f => f.MainTrackNumber.Value == fc.MainTrackNumber.Value && null != f.StatingNo && int.Parse(f.StatingNo.Trim()) == fc.StatingNo.Value);
                        if (sttList.Count() > 0)
                        {
                            var stating = sttList.First();
                            if (fc.MainTrackNumber.Value == stating.MainTrackNumber.Value && fc.StatingNo.Value == int.Parse(stating.StatingNo))
                            {
                                if (fc.IsReceivingHangerStating == null || (null != stating.IsReceivingHanger && fc.IsReceivingHangerStating.Value != stating.IsReceivingHanger.Value))
                                {
                                    fc.IsReceivingHangerStating = stating.IsReceivingHanger;
                                    isUpdate = true;
                                }
                            }
                        }

                    }
                    if (isUpdate)
                    {
                        dicHangerFlowChartCache[key] = hFlowChartList;
                    }
                }

            }
            catch (Exception ex)
            {
                redisLog.Error(ex);
                throw ex;
            }
        }
        public void LoadOnlineProductsToCache()
        {
            //加载上线制品
            var listOnlineProducts = CANProductsValidService.Instance.GetAllOnlineProductList();
            foreach (var op in listOnlineProducts)
            {
                try
                {
                    var key = op.Id;
                    var dicProducts = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, ProductsModel>(SusRedisConst.ON_LINE_PRODUCTS);
                    if (dicProducts.Count > 0)
                    {
                        foreach (var keyed in dicProducts.Keys)
                        {
                            dicProducts.Remove(keyed);
                        }
                    }
                    dicProducts.Add(key, op);
                }
                catch (Exception ex)
                {
                    redisLog.Error(ex);
                }
            }

        }

        public void LoadProductsToCache()
        {
            try
            {
                var listProducts = CANProductsValidService.Instance.GetAllProducts();
                var dicProducts = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, ProductsModel>(SusRedisConst.PRODUCTS);
                if (dicProducts.Count > 0)
                    dicProducts.Clear();

                foreach (var item in listProducts)
                {
                    var key = item.Id;
                    dicProducts.Add(key, item);
                }
            }
            catch (Exception ex)
            {

                redisLog.Error(ex);
            }


        }

        /// <summary>
        /// 通过Id获取在线产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductsModel GetOnLineProeuctCacheById(string id)
        {
            var dicProducts = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, ProductsModel>(SusRedisConst.PRODUCTS);
            if (dicProducts.Keys.Contains(id))
                return dicProducts[id];
            else
                return null;

        }

        public void LoadDefectCodeToCache()
        {
            //加载疵点代码
            var listDefectCode = SusReworkService.Instance.GetDefectCodeList();
            var dicDefectCodeTableCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DefectCodeTableModel>(SusRedisConst.DEFECT_CODE_TABLE);
            foreach (var dc in listDefectCode)
            {
                var key = dc.DefectCode?.Trim();
                if (dicDefectCodeTableCache.ContainsKey(key))
                {
                    NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DefectCodeTableModel>(SusRedisConst.DEFECT_CODE_TABLE).Remove(key);
                }
                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DefectCodeTableModel>(SusRedisConst.DEFECT_CODE_TABLE).Add(key, dc);
            }
        }
        /// <summary>
        /// USER_ROLE_TABLE
        /// </summary>
        public void LoadStating()
        {
            //加载站点信息
            var stating = ProductionLineSetServiceImpl.Instance.GetStatingList();
            var dicStatingCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, StatingModel>(SusRedisConst.STATING_TABLE);
            foreach (var dc in stating)
            {
                var key = string.Format("{0}:{1}", dc.MainTrackNumber.Value, dc.StatingNo?.Trim());
                if (dicStatingCache.ContainsKey(key))
                    NewSusRedisClient.RedisTypeFactory.GetDictionary<string, StatingModel>(SusRedisConst.STATING_TABLE).Remove(key);

                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, StatingModel>(SusRedisConst.STATING_TABLE).Add(key, dc);
            }
        }

        public void LoadUserRole()
        {
            var userRole = PermissionServiceImpl.Instance.GetUserRolesCache();
            var dicUserRoleCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, UserRolesCache>(SusRedisConst.USER_ROLE_TABLE);


            foreach (var dc in userRole)
            {
                var key = string.Format("{0}:{1}", dc.RoleId, dc.UserId);
                if (dicUserRoleCache.ContainsKey(key))
                    NewSusRedisClient.RedisTypeFactory.GetDictionary<string, UserRolesCache>(SusRedisConst.USER_ROLE_TABLE).Remove(key);

                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, UserRolesCache>(SusRedisConst.USER_ROLE_TABLE).Add(key, dc);
            }
        }
        /// <summary>
        /// 获取在线制品
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="productNumber"></param>
        /// <returns></returns>
        public Model.ProductsCacheModel GetOnlineProducts(int mainTrackNumber, int productNumber)
        {
            var key = string.Format("{0}:{1}", mainTrackNumber, productNumber);
            return ProductsCache.OnlineProductsList[key];
        }

        /// <summary>
        /// 拉平工艺图数据:按照工序，站点拉平，过滤掉非生效的工序及不能接收衣架的站点
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        public IList<ProductsFlowChartCacheTempModel> GetProductsFlowChartCacheModel(string flowChartId)
        {
            var sql = string.Format(@"select T1.Id ProcessFlowChartId,T1.BoltProcessFlowId,T2.Id ProcessFlowChartFlowRelationId,T2.CraftFlowNo,T2.FlowNo,T2.FlowCode,T2.FlowName,
(select StanardHours from ProcessFlow where Id=T2.PROCESSFLOW_Id)StanardHours ,
(select StandardPrice from ProcessFlow where Id=T2.PROCESSFLOW_Id)StandardPrice ,
T3.No StatingNo,T3.mainTrackNumber, 
(SELECT Capacity FROM Stating WHERE ID=T3.STATING_Id) StatingCapacity,
T3.StatingRoleCode
,T2.PROCESSFLOW_Id ProcessFlowId,T3.STATING_Id StatingId,
T3.IsReceivingHanger,T3.IsReceivingAllSize,T3.IsReceivingAllColor,T3.ReceivingColor,T3.ReceivingSize,T3.isReceivingAllPONumber,T3.Proportion,T3.IsEndStating ,T2.IsEnabled,T3.ReceivingPONumber,T4.IsReceivingHanger IsReceivingHangerStating,
T2.IsMergeForward,T2.MergeProcessFlowChartFlowRelationId,T2.MergeFlowNo,T2.IsProduceFlow   
FROM ProcessFlowChart T1 
INNER JOIN ProcessFlowChartFlowRelation T2 ON  T1.Id=T2.PROCESSFLOWCHART_Id
LEFT JOIN  ProcessFlowStatingItem T3 ON T2.Id=T3.PROCESSFLOWCHARTFLOWRELATION_Id
Left Join Stating T4 ON t3.STATING_Id=t4.Id
Where T1.ID=@flowChartId AND T2.IsEnabled=1
");
            //var list = Query<ProductsFlowChartCacheTempModel>(sql, true, flowChartId);
            var list = Dao.DapperHelp.Query<ProductsFlowChartCacheTempModel>(sql, new { flowChartId = flowChartId });
            if (list != null && list.Count() > 0)
                return list.ToList();
            else
                return new List<ProductsFlowChartCacheTempModel>();
        }
        /// <summary>
        /// 加载所有卡信息到cache
        /// </summary>
        public void LoadCardInfo()
        {
            var cardService = new CommonService.CommonServiceImpl<CardInfo>();
            var cardList = cardService.GetAllList(true);
            var dicCardCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, CardInfo>(SusRedisConst.CARD_INFO);
            foreach (var cd in cardList)
            {
                var key = string.Format("{0}", cd.CardNo?.Trim());
                if (!dicCardCache.ContainsKey(key))
                {
                    dicCardCache.Add(key, cd);
                }
                else
                {
                    dicCardCache[key] = cd;
                }
            }
        }

        /// <summary>
        /// 缓存系统参数相关
        /// </summary>
        public void LoadSystemParameter()
        {
            var systemParameterService = new SystemParameterServiceImpl();
            var pipeliningCache = systemParameterService.CachePipelining();


            //缓存生产线相关数据
            var pipeliningStating = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, PipeliningCache>(SusRedisConst.PIPELINING_STATING_QUEUE);//.RedisList<string, CardInfo>(SusRedisConst.PIPELINING_STATING_QUEUE);
            if (pipeliningStating != null)
                pipeliningStating.Clear();

            foreach (var cd in pipeliningCache)
            {
                var key = string.Format("{0}:{1}", cd.MainTrackNumber, cd.StatingNo?.Trim());
                if (!pipeliningStating.ContainsKey(key))
                {
                    pipeliningStating.Add(key, cd);
                }
                else
                {
                    pipeliningStating[key] = cd;
                }
            }


            //系统参数缓存
            var systemParameter = systemParameterService.CacheAllSystemModuleParameter();

            var parameter = NewSusRedisClient.RedisTypeFactory.GetList<SystemModuleParameterModel>(SusRedisConst.SYSTEM_PARAMETER_QUEUE);
            //SusRedisClient.clien
            foreach (var item in systemParameter)
            {
                int index = GetIndex(parameter, item);
                if (index > -1)
                    parameter.RemoveAt(index);

                parameter.Add(item);
            }

        }

        /// <summary>
        /// 初始化缺料表
        /// </summary>
        public void InitLackMaterialsTable()
        {
            var info = NewSusRedisClient.RedisTypeFactory.GetList<LackMaterialsTable>(SusRedisConst.LACK_MATERIALS_TABLE_KEY);

            if (info != null && info.Count > 0)
            {
                info.Clear();
            }

            var lackMater = CANLackMaterialsQueryService.Instance.GetAll();

            foreach (var item in lackMater)
            {
                info.Add(item);
            }
        }

        /// <summary>
        /// 返回缺料表
        /// </summary>
        /// <returns></returns>
        public List<Domain.LackMaterialsTable> LackMaterialsTable()
        {
            var info = NewSusRedisClient.RedisTypeFactory.GetList<LackMaterialsTable>(SusRedisConst.LACK_MATERIALS_TABLE_KEY);
            if (info == null || info.Count == 0)
            {
                var lackMater = CANLackMaterialsQueryService.Instance.GetAll();

                foreach (var item in lackMater)
                {
                    info.Add(item);
                }
                //NewSusRedisClient.RedisTypeFactory.
            }

            return info.ToList();
        }


        private int GetIndex(RedisList<SystemModuleParameterModel> redisList, SystemModuleParameterModel t)
        {
            if (redisList == null || redisList.Count == 0)
                return -1;

            for (int i = 0; i < redisList.Count; i++)
            {
                var tItem = redisList[i] as SystemModuleParameterModel;
                if (tItem.Id.Equals(t.Id, StringComparison.OrdinalIgnoreCase))
                    return i;
            }


            return -1;

        }

        /// <summary>
        /// 清除分配比例缓存记录
        /// </summary>
        public void ClearStatingAllocation(string flowChartId)
        {
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, Allocation>(SusRedisConst.STATING_ALLOCATION);

            if (dic != null)
            {
                foreach (var item in dic)
                {
                    if (item.Key.Contains(flowChartId))
                    {
                        dic.Remove(item.Key);
                    }
                }
            }

        }

    }

    public class SystemModuleParameterModelEquality : IEqualityComparer<SystemModuleParameterModel>
    {
        public bool Equals(SystemModuleParameterModel x, SystemModuleParameterModel y)
        {
            return x.Id.Equals(y.Id, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(SystemModuleParameterModel obj)
        {
            if (obj == null)
                return 0;
            else
                return obj.GetHashCode();
        }
    }
    class FlowMainTrackStating
    {
        public string FlwoNo { set; get; }
        public int MainTrackNumber { set; get; }
        public int StatingNo { set; get; }
        public bool IsDeleted { set; get; }

    }
}
