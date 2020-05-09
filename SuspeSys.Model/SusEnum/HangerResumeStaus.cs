using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public class HangerResumeStaus
    {
        /// <summary>
        /// 分配站点(0)
        /// </summary>
        public static readonly HangerResumeStaus HangingPiece = new HangerResumeStaus(0, "挂片");
        /// <summary>
        /// 重复挂片-1
        /// </summary>
        public static readonly HangerResumeStaus RepeatHangingPeiece = new HangerResumeStaus(-1, "重复挂片");

        /// <summary>
        /// 分配站点(1)
        /// </summary>
        public static readonly HangerResumeStaus AllocationStating = new HangerResumeStaus(1, "分配");

        /// <summary>
        ///衣架进站(2)
        /// </summary>
        public static readonly HangerResumeStaus IncomeStating = new HangerResumeStaus(2, "进站");

        /// <summary>
        /// 衣架出战(3)
        /// </summary>
        public static readonly HangerResumeStaus OutSiteStating = new HangerResumeStaus(3, "出战");
        /// <summary>
        /// 合并工序出战(31)
        /// </summary>
        public static readonly HangerResumeStaus MeregeOutSiteStating = new HangerResumeStaus(31, "合并工序出战");

        /// <summary>
        /// 返工(4)
        /// </summary>
        public static readonly HangerResumeStaus Rework = new HangerResumeStaus(4, "返工");
        /// <summary>
        /// 站点删除(5)
        /// </summary>
        public static readonly HangerResumeStaus StatingDelete = new HangerResumeStaus(5, "站点删除");

        /// <summary>
        /// 工序删除
        /// </summary>
        public static readonly HangerResumeStaus FlowDeleted = new HangerResumeStaus(6, "工序删除");

        /// <summary>
        /// 工序或站点移动
        /// </summary>
        public static readonly HangerResumeStaus FlowMoveOrStatingMove = new HangerResumeStaus(7, "工序或站点移动");

        /// <summary>
        /// 桥接不携带工序进站
        /// </summary>
        public static readonly HangerResumeStaus BridgeNonFlowIncomeStating = new HangerResumeStaus(8, "桥接不携带工序进站");

        /// <summary>
        /// 桥接不携带工序进站
        /// </summary>
        public static readonly HangerResumeStaus BridgeCarryFlowSucessedAginAollocation = new HangerResumeStaus(9, "桥接携带工序且工序已完成，再次分配");

        /// <summary>
        /// F2指定站出战衣架轨迹修正(F2指定发起源头站)
        /// </summary>
        public static readonly HangerResumeStaus F2SourceHangerOutSiteCorrorHangerResume = new HangerResumeStaus(10, "F2指定站出战衣架轨迹修正(F2指定发起源头站)");

        /// <summary>
        /// F2指定站出战衣架轨迹修正
        /// </summary>
        public static readonly HangerResumeStaus F2HangerOutSiteCorrorHangerResume = new HangerResumeStaus(11, "F2指定站出战衣架轨迹修正");

        private HangerResumeStaus(int _value, string desption)
        {
            Value = _value;
            Desption = desption;
        }
        public int Value { set; get; }
        public string Desption { set; get; }
    }
}
