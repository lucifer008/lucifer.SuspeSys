using SusNet.Common.SusBusMessage;
using SusNet.Common.Utils;
using SuspeSys.Service.Impl.Core.OutSite;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.Allocation
{
    public class OutSiteAdapter : SusLog
    {
        private OutSiteAdapter() { }
        public readonly static OutSiteAdapter Instance = new OutSiteAdapter();
        private readonly static object locObj = new object();
        public virtual  void DoService(HangerOutStatingRequestMessage request)
        {
           // lock (locObj)
            {
                var tenMaintracknumber = HexHelper.HexToTen(request.XID);
                var tenStatingNo = HexHelper.HexToTen(request.ID);
                var tenHangerNo = HexHelper.HexToTen(request.HangerNo);
                var info = string.Format($">>>-------------------【出战请求消息】-->衣架:{tenHangerNo} 主轨:{tenMaintracknumber} 站点:{tenStatingNo} --------------------->>>>>>");
                tcpLogInfo.InfoFormat(info);
                var outType = CANProductsService.Instance.IsHangPieceStating(tenMaintracknumber, tenStatingNo) ? 1 : 0;//HexHelper.HexToTen(req.outType);1:挂片站;0:普通站
                if (outType == 1)
                {
                    OutSiteHangingStatingHandler.Instance.Process(tenMaintracknumber, tenStatingNo, tenHangerNo);
                    return;
                }
                OutSiteCommonStatingHandler.Instance.Process(tenMaintracknumber, tenStatingNo, tenHangerNo);
                info = string.Format($"<<<-------------------【出战请求消息】-->衣架:{tenHangerNo} 主轨:{tenMaintracknumber} 站点:{tenStatingNo} ---------------------<<<<<<");
            }
        }
    }
}
