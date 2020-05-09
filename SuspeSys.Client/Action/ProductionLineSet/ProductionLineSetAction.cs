using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action.ProductionLineSet.Model;
using SuspeSys.Client.Action.SuspeRemotingClient;
using SuspeSys.Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Action.ProductionLineSet
{
    public class ProductionLineSetAction : BaseAction
    {
        public ProductionLineSetModel Model = new ProductionLineSetModel();
        public void AddPipelining()
        {
            var pipeling = ProductionLineSetService.AddPipelining(Model.Pipelining, Model.StatingList, Model.SiteGroup);

            SendMsgToMachineProcess(pipeling);
        }
        public IList<DaoModel.SiteGroup> GetPipeliningSiteGroupList(string pipeliningId)
        {
            return ProductionLineSetQueryService.GetPipeliningSiteGroupList(pipeliningId);
        }
        public IList<DaoModel.Stating> GetStatingList(string siteGroupId)
        {
            return ProductionLineSetQueryService.GetStatingList(siteGroupId).Where(o => o.Deleted == 0).ToList();
        }

        public void UpdatePipelining()
        {
            var statList = new List<StatingMsg>();
            try
            {
                statList = ProductionLineSetService.UpdatePipelining(Model.Pipelining, Model.StatingList, Model.SiteGroup);
            }
            finally
            {

                try
                {
                    SendMsgToMachineProcess(statList);

                }
                catch (Exception ex)
                {
                    throw new ApplicationException(string.Format("数据已保存成功!但下位机监测,站类型，容量设置异常:"), ex);
                }
            }
        }

        private void SendMsgToMachineProcess(List<StatingMsg> pipeling)
        {
            try
            {
                SuspeRemotingService.statingService.SendMsgToMachineProcess(pipeling);
                foreach (var stating in pipeling)
                {
                    SuspeRemotingService.statingService.OpenOrCloseMainTrackStatingMonitor(stating.MainTrackNumber, int.Parse(stating.StatingNo), stating.IsLoadMonitor);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        internal IList<DaoModel.BridgeSet> SearchBridgeSet(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string v)
        {
            return ProductionLineSetService.SearchBridgeSet(currentPageIndex, pageSize, out totalCount, ordercondition, v);
        }
        /// <summary>
        /// 暂停或者接收衣架
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="suspendReceive">1：暂停接收衣架;0:接收衣架</param>
        public void SuspendOrReceiveHanger()
        {
            var list = Model.ProductRealtimeInfoModelList;
            var statingList = CommonAction.GetList<DaoModel.Stating>();
            foreach (var stating in list)
            {
                if (stating.MainTrackNumber == null) {
                    log.InfoFormat("站点：{0} 无主轨号",stating.StatingNo?.Trim());
                    continue;
                }
                int mainTrackNo = stating.MainTrackNumber.Value;
                int statingNo = int.Parse(stating.StatingNo);
                int suspendReceive = stating.SuspendReceive;
                SuspeRemotingService.statingService.SuspendOrReceiveHanger(mainTrackNo, statingNo, suspendReceive);
                var sList = statingList.Where(f => f.MainTrackNumber.Value == stating.MainTrackNumber.Value && f.StatingNo.Trim().Equals(stating.StatingNo.Trim()));
                if (sList.Count()!=1) {
                    throw new ApplicationException(string.Format("主轨:{0} 站号:{1} 一个主轨出现多个相同的站!",stating.MainTrackNumber,stating.StatingNo?.Trim()));
                }
                var s = sList.First();

                log.InfoFormat("【站点状态变更】 主轨:{0} 站点:{1} 变更前站点状态:{2}",stating.MainTrackNumber,stating.StatingNo?.Trim(), (s.IsReceivingHanger!=null && s.IsReceivingHanger.Value)?"接收":"停止");
                s.IsReceivingHanger = suspendReceive == 0 ? true : false;
                CommonAction.Instance.Update(s);
                log.InfoFormat("【站点状态变更】 主轨:{0} 站点:{1} 变更后站点状态:{2}", stating.MainTrackNumber, stating.StatingNo?.Trim(), (s.IsReceivingHanger != null && s.IsReceivingHanger.Value) ? "接收" : "停止");

                //更新缓存（站点及工艺图缓存）
                SuspeRemotingService.statingService.UpdateStatingCache(mainTrackNo, statingNo);

                log.InfoFormat("【站点状态变更】 主轨:{0} 站点:{1} 站点状态更新到缓存！", stating.MainTrackNumber, stating.StatingNo?.Trim());
            }

        }
        /// <summary>
        /// 员工离线
        /// </summary>
        internal void EmployeeOffline()
        {
            var list = Model.ProductRealtimeInfoModelList;
            foreach (var stating in list)
            {
                var cardLogInfo = CommonAction.Instance.Get<DaoModel.CardLoginInfo>(stating.StatingLoginId);
                cardLogInfo.IsOnline = false;
                cardLogInfo.LogOutDate = DateTime.Now;
                CommonAction.Instance.Update<DaoModel.CardLoginInfo>(cardLogInfo);
                SuspeRemotingService.statingService.ManualEmployeeOffline(stating.MainTrackNumber.Value, int.Parse(stating.StatingNo), 4, int.Parse(cardLogInfo.CardInfo.CardNo));
            }
        }
        /// <summary>
        /// 重置站内数
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        public void ResetStatingInNum()
        {
            var list = Model.ProductRealtimeInfoModelList;
            foreach (var stating in list)
            {
                SuspeRemotingService.statingService.ResetStatingInNum(stating.MainTrackNumber.Value, int.Parse(stating.StatingNo));
            }
        }
    }
}
