using SuspeSys.Service.Impl.Core.Check;
using SuspeSys.Service.Impl.Core.F2;
using SuspeSys.Service.Impl.Core.Flow;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Utils;

namespace SuspeSys.Service.Impl.Core.OutSite
{
    public class OutSiteCommonStatingHandler : SusLog
    {
        private OutSiteCommonStatingHandler() { }
        public readonly static OutSiteCommonStatingHandler Instance = new OutSiteCommonStatingHandler();
        private static readonly object locObject = new object();
        internal void Process(int tenMaintracknumber, int tenStatingNo, int tenHangerNo)
        {
           // lock (locObject)
            {
                CheckService.Instance.CheckLoginHandler(tenStatingNo, tenMaintracknumber);
                var isF2Assign = F2Service.Instance.F2Backflow(tenMaintracknumber, tenStatingNo, tenHangerNo);
                if (isF2Assign) return;
                var hangerIsNotFlowChart = CheckService.Instance.HangerIsNotFlowChartHandler(tenMaintracknumber, tenStatingNo, tenHangerNo);
                if (hangerIsNotFlowChart) return;
                var isRepeatOutSite = CheckService.Instance.RepeatOutSiteHandler(tenMaintracknumber, tenStatingNo, tenHangerNo);
                if (isRepeatOutSite) return;
                var hangerNonHangingPiece = CheckService.Instance.HangerNonHangingPiece(tenMaintracknumber, tenStatingNo, tenHangerNo);
                if (hangerNonHangingPiece) return;
                var flowMoveOrStatingMove = FlowService.Instance.IsFlowMoveAndStatingMove(tenMaintracknumber.ToString(), tenStatingNo.ToString(), tenHangerNo.ToString());
                if (flowMoveOrStatingMove) return;
                FlowDeleteOrStatingDeletedException fdssEx = null;
                var flowDeleteOrStatingDelete = FlowService.Instance.FlowDeleteOrStatingDelete(tenMaintracknumber.ToString(), tenStatingNo.ToString(), tenHangerNo.ToString(), ref fdssEx);
                if (flowDeleteOrStatingDelete)
                {
                    FlowDeleteOrStatingDeleteService.Instance.FlowDeleteOrStatingDeletedHandler(fdssEx, tenMaintracknumber.ToString(), tenStatingNo.ToString());
                    return;
                }
                var isNonAllocationOutSite = FlowService.Instance.IsNonAllocationOutSite(tenMaintracknumber, tenStatingNo, tenHangerNo);
                if (isNonAllocationOutSite) return;
                FlowAllocationService.Instance.NextFlowHandler(tenMaintracknumber, tenStatingNo, tenHangerNo);
            }
        }
    }
}
