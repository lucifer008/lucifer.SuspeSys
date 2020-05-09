using SuspeSys.Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Cus
{
    public class HangerNonStandardStatingOrCacheModel
    {
        /// <summary>
        /// -4:衣架回流未进挂片站监测点再分配
        /// -3: 衣架回流进站
        /// -2: 挂片衣架回流分配
        /// -1:衣架挂片
        /// 0:衣架分配;
        /// 1:衣架进战
        /// 2.衣架出站
        /// 3.站点删除
        /// 4.工序删除
        /// 6.衣架返工
        /// 7.过监测点已分配或者已进站修正
        /// 8.工序移动及站点移动
        /// 9.桥接站出战逆向桥接站内数修正(逆向站点无携带工序)
        /// 10.桥接出战逆向桥接站内数修正(逆向站点携带工序)
        /// 11 :桥接不携带工序进站或者 桥接站且在携带工序，且工序已完成又进站
        /// 12.桥接不携带工序出战
        /// 13: 桥接携带工序且已完成，后又分配
        /// 14.桥接站出战 清除桥接反向站内数
        /// 15: 桥接携带工序出战，反向工序未完成,清除站内数
        /// 16. F2指定分配清除当前站站内数或者在线数
        /// 17: F2指定分配
        /// </summary>
        public int Action { set; get; }

        public string HangerNo { set; get; }
        public int MainTrackNumber { set; get; }
        public int StatingNo { set; get; }
        public string FlowNo { set; get; }
        public int FlowIndex { set; get; }
        public int LastStatingNo { set; get; }
        public int NextMainTrackNumber { set; get; }
        public int NextStatingNo { set; get; }
        public string NextFlowNo { set; get; }
        public int NextFlowIndex { set; get; }
        public bool IsHanging { set; get; }

        public HangerProductFlowChartModel HangerProductFlowChartModel { set; get; }
        /// <summary>
        /// 是否是桥接分配
        /// </summary>
        public bool IsBridgeAllocation { set; get; }//是否是桥接分配
    }
}
