using SusNet.Common.SusBusMessage;
using SuspeSys.Domain;
using SuspeSys.Service.Impl.Core.Check;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.OutSite
{
    public class OutSiteHangingStatingHandler : SusLog
    {
        private OutSiteHangingStatingHandler() { }
        public readonly static OutSiteHangingStatingHandler Instance = new OutSiteHangingStatingHandler();
        public virtual void Process(HangerOutStatingRequestMessage request)
        {

        }
        private static readonly object locObject = new object();
        internal void Process(int tenMaintracknumber, int tenStatingNo, int tenHangerNo)
        {
           // lock (locObject)
            {
                //检查出站站点是否有员工登录，没有登录不能出站
                tcpLogInfo.InfoFormat($"------------------------------------------【衣架挂片】衣架号:{tenHangerNo} 主轨:{tenMaintracknumber} 站点:{tenStatingNo}--------------------Begin--------------");
                bool isLogin = CheckService.Instance.CheckStatingIsLogin(tenStatingNo, tenMaintracknumber);
                if (!isLogin)
                {
                    var ex = new StatingNoLoginEmployeeException(string.Format("【错误】主轨:【{0}】站点:【{1}】没有员工登录!,不能出站", tenMaintracknumber, tenStatingNo));
                    tcpLogError.Error(ex);
                    throw ex;
                }
                string nextStatingNo = null;
                int outMainTrackNumber = 0;
                CANProductsService.Instance.RegisterHangerToProducts(tenMaintracknumber + "", tenStatingNo + "", tenHangerNo + "", ref nextStatingNo, ref outMainTrackNumber);
                if (outMainTrackNumber == 0)
                {
                    var ex = new StatingNoLoginEmployeeException(string.Format("【错误】主轨:【{0}】站点:【{1}】找不到下一站!", outMainTrackNumber, nextStatingNo));
                    tcpLogError.Error(ex);
                    throw ex;
                }
                LowerPlaceInstr.Instance.AllocationHangerToNextStating(tenHangerNo + "", nextStatingNo, outMainTrackNumber);

                tcpLogInfo.InfoFormat($"------------------------------------------【衣架挂片】衣架号:{tenHangerNo} 主轨:{tenMaintracknumber} 站点:{tenStatingNo}--------------------End--------------");
            }
            //    var allocationJson = Newtonsoft.Json.JsonConvert.SerializeObject(new HangerStatingAllocationItem()
            //    {
            //        HangerNo = tenHangerNo + "",
            //        MainTrackNumber = (short)outMainTrackNumber,
            //        SiteNo = nextStatingNo+ "",
            //        Memo = "-1"
            //});
            //    NewSusRedisClient.subcriber.Publish(SusRedisConst.HANGER_AOLLOCATION_ACTION, allocationJson);

        }
    }
}
