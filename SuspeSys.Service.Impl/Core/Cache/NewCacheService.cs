
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Products.SusCache.Model;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Service.Impl.Core.Cache
{
    public class NewCacheService
    {
        public static readonly NewCacheService Instance = new NewCacheService();
        private static readonly object lockObject = new object();
        public bool HangerIsContainsFlowChart(string tenHangerNo)
        {
            lock (lockObject)
            {
                var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                if (dicHangerProcessFlowChart.ContainsKey(tenHangerNo)) return true;
                return false;
            }
        }
        public List<HangerProductFlowChartModel> GetHangerFlowChartListForRedis(string tenHangerNo)
        {
            lock (lockObject)
            {
                var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                List<HangerProductFlowChartModel> hangerProcessFlowChartList = dicHangerProcessFlowChart[tenHangerNo];
                return hangerProcessFlowChartList;
            }
        }
        public StackExchange.Redis.DataTypes.Collections.RedisDictionary<string, List<HangerProductFlowChartModel>> GetHangerFlowChartListForRedisCache()
        {
            lock (lockObject)
            {
                var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
                return dicHangerProcessFlowChart;
            }
        }
        public void UpdateHangerFlowChartCacheToRedis(string tenHangerNo, IList<HangerProductFlowChartModel> fcList)
        {
            lock (lockObject)
            {
                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[tenHangerNo] = fcList.ToList();
            }
        }
        public void AddHangerCurrentFlowToRedis(string tenHangerNo, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel cfm)
        {
            lock (lockObject)
            {
                SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW).Add(tenHangerNo, cfm);
            }
        }
        public bool HangerCurrentFlowIsContains(string tenHangerNo)
        {
            lock (lockObject)
            {
                var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                if (dicCurrentHangerProductingFlowModelCache.ContainsKey(tenHangerNo))
                {
                    return true;
                }
                return false;
            }
        }
        public SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel GetHangerCurrentFlow(string tenHangerNo)
        {
            lock (lockObject)
            {
                var dicCurrentHangerProductingFlowModelCache = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW);
                var current = dicCurrentHangerProductingFlowModelCache[tenHangerNo];
                return current;
            }
        }
        public void UpdateCurrentHangerFlowCacheToRedis(string tenHangerNo, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel current)
        {
            lock (lockObject)
            {
                SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel>(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW)[tenHangerNo] = current;
            }
        }
        public void UpdateHangerOutSiteRecord(string tenHangerNo, SuspeSys.Domain.Cus.CurrentHangerOutSiteModel current)
        {
            lock (lockObject)
            {
                var dic = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<SuspeSys.Domain.Cus.CurrentHangerOutSiteModel>>(SusRedisConst.Hanger_Out_Site_Record);
                if (!dic.ContainsKey(tenHangerNo))
                {
                    dic.Add(tenHangerNo, new List<DaoModel.Cus.CurrentHangerOutSiteModel>() { current });
                    return;
                }
                var list = dic[tenHangerNo];
                list.Add(current);
                SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<SuspeSys.Domain.Cus.CurrentHangerOutSiteModel>>(SusRedisConst.Hanger_Out_Site_Record)[tenHangerNo] = list;
            }
        }
        private readonly static object lcObject = new object();
        public bool IsReatOutSite(int mainTrackNumber, int statingNo, int hangerNo)
        {
            lock (lcObject)
            {
                var dic = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<SuspeSys.Domain.Cus.CurrentHangerOutSiteModel>>(SusRedisConst.Hanger_Out_Site_Record);
                if (!dic.ContainsKey(hangerNo + ""))
                {
                    return false;
                }
                var list = dic[hangerNo + ""];
                var last = list.OrderByDescending(f => f.OutSite).First();
                var isReatOutSite = last.MainTrackNumber == mainTrackNumber && statingNo == last.StatingNo;
                return isReatOutSite;
            }
        }
        public void RemoveHangerOutSiteRecord(string tenHangerNo)
        {
            lock (lockObject)
            {
                var dic = SusRedis.NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<SuspeSys.Domain.Cus.CurrentHangerOutSiteModel>>(SusRedisConst.Hanger_Out_Site_Record);
                if (dic.ContainsKey(tenHangerNo))
                {
                    dic.Remove(tenHangerNo);
                    return;
                }
            }
        }
        public List<DaoModel.HangerStatingAllocationItem> GetHangerAllocationItemListForRedis(string hangerNo)
        {
            //lock (lockObject)
            //{
                var dicHangerAllocationItem = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
                return dicHangerAllocationItem[hangerNo];
            //}
        }
        public bool HangerIsContainsAllocationItem(string tenHangerNo)
        {
           // lock (lockObject)
            {
                var dicHangerAllocationCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
                if (dicHangerAllocationCache.ContainsKey(tenHangerNo)) return true;

                return false;
            }
        }
        public void UpdateHangerAllocationItemListToRedis(string hangerNo, List<DaoModel.HangerStatingAllocationItem> hsaItemList)
        {
           // lock (lockObject)
            {
                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[hangerNo] = hsaItemList;
            }
        }
        public void AddHangerAllocationItem(string hangerNo, List<DaoModel.HangerStatingAllocationItem> hsaItemList)
        {
            lock (lockObject)
            {
                var dicHangerAllocationItem = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME);
                dicHangerAllocationItem.Add(hangerNo, hsaItemList);
            }
        }
        public DaoModel.StatingModel GetStatingCache(int mainTrackNumber, int statingNo)
        {
            var dicStatingModel = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DaoModel.StatingModel>(SusRedisConst.STATING_TABLE);
            var key = string.Format($"{mainTrackNumber}:{statingNo}");
            if (dicStatingModel.ContainsKey(key)) return dicStatingModel[key];
            return null;
        }
        public DaoModel.StatingModel GetHangingPieceStatingCache(int mainTrackNumber)
        {
            var dicStatingModel = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, DaoModel.StatingModel>(SusRedisConst.STATING_TABLE);
            var keys = dicStatingModel.Keys;
            foreach (var k in keys)
            {
                var st = dicStatingModel[k];
                var isq = st.StatingRoles.RoleCode.Equals("104") && null != st.MainTrackNumber && st.MainTrackNumber.Value == mainTrackNumber;
                if (isq)
                {
                    return dicStatingModel[k];
                }
            }
            return null;
        }
        public long GetStatingOnlineNum(int mainTrackNumber, int statingNo)
        {
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_ONLINE_NUM);
            var key = string.Format($"{mainTrackNumber}:{statingNo}");
            if (!dic.ContainsKey(key)) return 0;
            return dic[key];
        }
        public long GetStatingInNum(int mainTrackNumber, int statingNo)
        {
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);
            var key = string.Format($"{mainTrackNumber}:{statingNo}");
            if (!dic.ContainsKey(key)) return 0;
            return dic[key];
        }
        public HangerProductFlowChartModel GetFlowIndexFlowChart(List<HangerProductFlowChartModel> hangerProcessFlowChartList, int currentIndex, int tenMaintracknumber, int tenStatingNo)
        {
            List<HangerProductFlowChartModel> ppChartList = hangerProcessFlowChartList.Where(f => currentIndex == f.FlowIndex.Value && f.MainTrackNumber == tenMaintracknumber &&
            f.StatingNo.Value == tenStatingNo && f.Status.Value != HangerProductFlowChartStaus.Successed.Value).OrderBy(f => f.FlowIndex).OrderByDescending(ff => ff.AllocationedDate).ToList<HangerProductFlowChartModel>();
            HangerProductFlowChartModel ppChart = null;
            if (ppChartList.Count > 0)
            {
                ppChart = ppChartList.First();
            }
            return ppChart;
        }
        public void PutF2HangerUpload(int launchMainTrack, int launchStatingNo, int hangerNo)
        {
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, int>(SusRedisConst.F2_Hanger_Assign_Upload_Cache);
            var keys = string.Format($"{launchMainTrack}:{launchStatingNo}");
            if (!dic.ContainsKey(keys))
            {
                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, int>(SusRedisConst.F2_Hanger_Assign_Upload_Cache).Add(keys, hangerNo);
                return;
            }
            NewSusRedisClient.RedisTypeFactory.GetDictionary<string, int>(SusRedisConst.F2_Hanger_Assign_Upload_Cache)[keys] = hangerNo;
        }
        //public void RemoveF2AssignHangerUploadCache(int launchMainTrack, int launchStatingNo)
        //{
        //    var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, int>(SusRedisConst.F2_Hanger_Assign_Upload_Cache);
        //    var keys = string.Format($"{launchMainTrack}:{launchStatingNo}");
        //    if (dic.ContainsKey(keys))
        //    {
        //        NewSusRedisClient.RedisTypeFactory.GetDictionary<string, int>(SusRedisConst.F2_Hanger_Assign_Upload_Cache).Remove(keys);
        //        return;
        //    }
        //}
        public int GetF2AssignLaunchStatingHanger(int launchMainTrack, int launchStatingNo)
        {
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, int>(SusRedisConst.F2_Hanger_Assign_Upload_Cache);
            var keys = string.Format($"{launchMainTrack}:{launchStatingNo}");
            return dic[keys];
        }
        public List<F2AssignModel> GetHangerF2AssignList(int hangerNo)
        {
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<int, List<F2AssignModel>>(SusRedisConst.F2_Hanger_Assign_Cache_List);
            return dic[hangerNo];
        }
        public bool F2AssignIsContains(int hangerNo)
        {
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<int, List<F2AssignModel>>(SusRedisConst.F2_Hanger_Assign_Cache_List);
            return dic.ContainsKey(hangerNo);
        }
        public void PutF2AssignHanger(int hangerNo, F2AssignModel assign)
        {
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<int, List<F2AssignModel>>(SusRedisConst.F2_Hanger_Assign_Cache_List);
            var dicCurrent = NewSusRedisClient.RedisTypeFactory.GetDictionary<int, F2AssignModel>(SusRedisConst.Current_F2_Hanger_Assign_Cache);
            if (!dic.ContainsKey(hangerNo))
            {
                var currentHangerFlow = NewCacheService.Instance.GetHangerCurrentFlow(hangerNo + "");

                assign.IsLaunchSourceStating = true;
                assign.LaunchMainTrackNumber = assign.SourceMainTrackNuber;
                assign.LaunchMainStatingNo = assign.SourceStatingNo;
                assign.CurrentNonFlow = currentHangerFlow;
                dic.Add(hangerNo, new List<F2AssignModel>() { assign });
                dicCurrent.Add(hangerNo, assign);
                return;
            }
            var currentF2HangerFlow = GetCurrentF2AssignInfo(hangerNo);
            assign.LaunchMainTrackNumber = currentF2HangerFlow.LaunchMainTrackNumber;
            assign.LaunchMainStatingNo = currentF2HangerFlow.LaunchMainStatingNo;
            assign.CurrentNonFlow = currentF2HangerFlow.CurrentNonFlow;
            NewSusRedisClient.RedisTypeFactory.GetDictionary<int, F2AssignModel>(SusRedisConst.Current_F2_Hanger_Assign_Cache)[hangerNo] = assign;
            var list = NewSusRedisClient.RedisTypeFactory.GetDictionary<int, List<F2AssignModel>>(SusRedisConst.F2_Hanger_Assign_Cache_List)[hangerNo];
            list.Add(assign);
            NewSusRedisClient.RedisTypeFactory.GetDictionary<int, List<F2AssignModel>>(SusRedisConst.F2_Hanger_Assign_Cache_List)[hangerNo] = list;
        }
        public void RemoveF2HangerAssign(int hangerNo)
        {
            var dicCurrent = NewSusRedisClient.RedisTypeFactory.GetDictionary<int, F2AssignModel>(SusRedisConst.Current_F2_Hanger_Assign_Cache);
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<int, List<F2AssignModel>>(SusRedisConst.F2_Hanger_Assign_Cache_List);
            if (dic.ContainsKey(hangerNo))
            {
                dic.Remove(hangerNo);
                dicCurrent.Remove(hangerNo);
            }
        }
        public F2AssignModel GetCurrentF2AssignInfo(int hangerNo)
        {
            var dicCurrent = NewSusRedisClient.RedisTypeFactory.GetDictionary<int, F2AssignModel>(SusRedisConst.Current_F2_Hanger_Assign_Cache);
            return dicCurrent[hangerNo];
        }
        public void UpdateCurrentF2AssignInfo(int hangerNo, F2AssignModel assgin)
        {
            NewSusRedisClient.RedisTypeFactory.GetDictionary<int, F2AssignModel>(SusRedisConst.Current_F2_Hanger_Assign_Cache)[hangerNo] = assgin;
        }
        public HangerProductFlowChartModel GetHangerMainFlowInfo(string tenHangerNo)
        {
            var dicHangerProcessFlowChart = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART);
            List<HangerProductFlowChartModel> hangerProcessFlowChartList = dicHangerProcessFlowChart[tenHangerNo];
            return hangerProcessFlowChartList.Where(f => !string.IsNullOrEmpty(f.FlowNo)).First();
        }
        private NewCacheService() { }
        /// <summary>
        /// 修正衣架下一道工序状态到cache
        /// </summary>
        /// <param name="tenHangerNo"></param>
        /// <param name="outMainTrackNumber"></param>
        /// <param name="nextPPChart"></param>
        /// <param name="nextStatingNo"></param>
        public void CorrectHangerNextFlowHandler(string tenHangerNo, int outMainTrackNumber, HangerProductFlowChartModel nextPPChart, string nextStatingNo)
        {
            var chpf = new SuspeSys.Domain.Cus.CurrentHangerProductingFlowModel();
            chpf.HangerNo = tenHangerNo;
            chpf.MainTrackNumber = outMainTrackNumber;
            chpf.StatingNo = int.Parse(string.IsNullOrEmpty(nextStatingNo) ? "-1" : nextStatingNo);
            chpf.FlowNo = nextPPChart?.FlowNo;
            chpf.FlowIndex = nextPPChart == null ? 1 : nextPPChart.FlowIndex.Value;
            chpf.FlowType = null == nextPPChart?.FlowType ? 0 : nextPPChart.FlowType.Value;
            var hJson = Newtonsoft.Json.JsonConvert.SerializeObject(chpf);

            NewSusRedisClient.subcriber.Publish(SusRedisConst.CURRENT_HANGER_PRODUCTS_FLOW_ACTION, hJson);
        }
        /// <summary>
        /// 修正 下一站分配缓存
        /// </summary>
        /// <param name="tenMainTrackNo"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        /// <param name="nextPPChart"></param>
        /// <param name="nextStatingNo"></param>
        public void RecordHangerNextSatingAllocationResume(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, HangerProductFlowChartModel nextPPChart, string nextStatingNo)
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
            //    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, nextStatingHPResumeJson);
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
            var hpResume = new HangerProductsChartResumeModel()
            {
                HangerNo = tenHangerNo,
                StatingNo = tenStatingNo,
                MainTrackNumber = int.Parse(tenMainTrackNo),
                HangerProductFlowChart = ppChart,
                Action = 3
            };
            var hangerResumeJson = Newtonsoft.Json.JsonConvert.SerializeObject(hpResume);
            //    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_PRODUCTS_CHART_RESUME_ACTION, hangerResumeJson);
            NewSusRedisClient.Instance.HangerProductsChartResumeAction(new StackExchange.Redis.RedisChannel(), hangerResumeJson);
        }

        ///// <summary>
        ///// 记录员工产量到cahe和db
        ///// </summary>
        ///// <param name="tenMainTrackNo"></param>
        ///// <param name="tenStatingNo"></param>
        ///// <param name="tenHangerNo"></param>
        ///// <param name="hangerProcessFlowChartLis"></param>
        ///// <param name="ppChart"></param>
        ///// <param name="nextStatingNo"></param>
        //public void RecordEmployeeFlowYieldHandler(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, List<HangerProductFlowChartModel> hangerProcessFlowChartLis, HangerProductFlowChartModel ppChart, string nextStatingNo)
        //{
        //    var outSiteResult = new HangerOutSiteResult();
        //    outSiteResult.MainTrackNumber = int.Parse(tenMainTrackNo);
        //    outSiteResult.HangerNo = tenHangerNo;
        //    outSiteResult.StatingNo = tenStatingNo;
        //    outSiteResult.HangerProductFlowChart = ppChart;
        //    var outSiteJson = Newtonsoft.Json.JsonConvert.SerializeObject(outSiteResult);
        //    // SusRedisClient.subcriber.Publish(SusRedisConst.HANGER_OUT_SITE_ACTION, outSiteJson);
        //    NewSusRedisClient.Instance.HangerOutSiteAction(new StackExchange.Redis.RedisChannel(), outSiteJson);

        //    //更新衣架工艺图缓存
        //    ppChart.NextStatingNo = !string.IsNullOrEmpty(nextStatingNo) ? short.Parse(nextStatingNo) : default(short?);
        //    //NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<HangerProductFlowChartModel>>(SusRedisConst.HANGER_PROCESS_FLOW_CHART)[tenHangerNo] = hangerProcessFlowChartLis;
        //    NewCacheService.Instance.UpdateHangerFlowChartCacheToRedis(tenHangerNo, hangerProcessFlowChartLis);
        //}

        ///// <summary>
        ///// 最后一道工序出战站内数及分配标识修正
        ///// </summary>
        ///// <param name="tenMainTrackNo"></param>
        ///// <param name="tenStatingNo"></param>
        ///// <param name="tenHangerNo"></param>
        ///// <param name="ppChart"></param>
        //public void LastFlowStatingHandler(string tenMainTrackNo, string tenStatingNo, string tenHangerNo, HangerProductFlowChartModel ppChart)
        //{
        //    var hnssoc = new SuspeSys.Domain.Cus.HangerNonStandardStatingOrCacheModel();
        //    hnssoc.Action = 2;
        //    hnssoc.HangerNo = tenHangerNo;
        //    hnssoc.MainTrackNumber = int.Parse(tenMainTrackNo);
        //    hnssoc.StatingNo = int.Parse(tenStatingNo);
        //    hnssoc.FlowNo = ppChart.FlowNo;
        //    hnssoc.FlowIndex = ppChart.FlowIndex.Value;
        //    hnssoc.HangerProductFlowChartModel = ppChart;
        //    var hnssocJson = Newtonsoft.Json.JsonConvert.SerializeObject(hnssoc);
        //    //NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_STATION_IN_OR_ALLOCATION_ACTION, hnssocJson);
        //    NewSusRedisClient.Instance.HangerStatingOrAllocationAction(new StackExchange.Redis.RedisChannel(), hnssocJson);
        //    ////出站站点分配数-1
        //    //var outAllocationNumModel = new MainTrackStatingCacheModel() { MainTrackNumber = int.Parse(mainTrackNo), StatingNo = int.Parse(statingNo), OnLineSum = -1 };
        //    //SusRedisClient.subcriber.Publish(SusRedisConst.MAINTRACK_STATING_ALLOCATION_NUM, JsonConvert.SerializeObject(outAllocationNumModel));

        //    //更新衣架分配记录为处理状态到缓存
        //    var dicHangerStatingALloList1 = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo];

        //    foreach (var item in dicHangerStatingALloList1)
        //    {
        //        if (tenStatingNo.Equals(item.NextSiteNo) && item.MainTrackNumber.Value == short.Parse(tenMainTrackNo))
        //        {
        //            item.IsSucessedFlow = true;
        //            item.Status = (byte)HangerStatingAllocationItemStatus.Successed.Value;
        //            item.HangerType = ppChart.FlowType;
        //            item.OutSiteDate = DateTime.Now;
        //        }
        //    }
        //    NewSusRedisClient.RedisTypeFactory.GetDictionary<string, List<DaoModel.HangerStatingAllocationItem>>(SusRedisConst.HANGER_ALLOCATION_ITME)[tenHangerNo] = dicHangerStatingALloList1;
        //}
    }
}
