using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Service.Impl.SusCache;
using log4net;
using SuspeSys.Domain.Ext;
using System.Collections.Generic;

namespace SuspeSys.CoreTest.Query
{
    [TestClass]
    public class QueryHangerStatingItemTest : QueryTestBase
    {
        public QueryHangerStatingItemTest(ILog _log)
        {
            if (null == log)
            {
                log = _log;
            }
        }
        [TestMethod]
        public IList<OnlineOrInStationItemModel> TestQueryStatingItem(int mainTrackNumber, string statingNo)
        {
            long totalCount;
            System.Collections.Generic.IDictionary<string, string> ordercondition = null;
            //int mainTrackNumber = 0;
            string productId = null;
            //  string statingNo = null;
            var list = ProductRealtimeInfoServiceImpl.Instance.SearchOnlineOrInStationItemByServer(1, 20, out totalCount, ordercondition, statingNo, productId, mainTrackNumber); //ProductRealtimeInfoServiceImpl.Instance.SearchOnlineOrInStationItem(1, 20, out totalCount, ordercondition, statingNo, productId, mainTrackNumber);
            log.Info($"------------------------------【{mainTrackNumber}--{statingNo}】站内明细begin-----------------------------------------------------------------------------");
            foreach (var item in list)
            {

                log.Info($"【站内明细】主轨----->:{item.MainTrackNumber} 站号----->:{item.StatingNo} 衣架号----->:{item.HangerNo?.Trim()} 生产顺序----->:{item.FlowIndex} 工序号----->:{item.FlowNo?.Trim()} 工序名称----->:{item.FlowName?.Trim()} 站内----->:{(item.InStating ? "是" : "否")} 制单号:{item.ProcessOrderNo?.Trim()} 款号:{item.StyleNo?.Trim()} 颜色:{item.PColor?.Trim()} 尺码:{item.PSize?.Trim()} 数量:{item.Num?.Trim()} 路线图:{item.LineName} 站内:{(item.InStating ? "是" : "否")}");
            }
            log.Info($"------------------------------【{mainTrackNumber}--{statingNo}】站内明细end-----------------------------------------------------------------------------");
            return list;
        }

        [TestMethod]
        public void ExecueTestQueryStatingItem()
        {
            TestQueryStatingItem(1, "1");
        }
        public IList<OnlineOrInStationItemModel> GetStatingItemList(int mainTrackNumber, string statingNo)
        {
            long totalCount;
            System.Collections.Generic.IDictionary<string, string> ordercondition = null;
            //int mainTrackNumber = 0;
            string productId = null;
            var list = ProductRealtimeInfoServiceImpl.Instance.SearchOnlineOrInStationItem(1, 20, out totalCount, ordercondition, statingNo, productId, mainTrackNumber);
            return list;
        }
    }
}
