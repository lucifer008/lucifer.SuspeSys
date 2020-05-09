using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public class HangerProductFlowChartStaus
    {
        /// <summary>
        /// 待生产(0)
        /// </summary>
        public static readonly HangerProductFlowChartStaus WaitingProducts = new HangerProductFlowChartStaus(0, "待生产");

        /// <summary>
        ///生产中(1)
        /// </summary>
        public static readonly HangerProductFlowChartStaus Producting = new HangerProductFlowChartStaus(1, "生产中");

        /// <summary>
        /// 已完成(2)
        /// </summary>
        public static readonly HangerProductFlowChartStaus Successed = new HangerProductFlowChartStaus(2, "已完成");


        private HangerProductFlowChartStaus(short _value, string desption)
        {
            Value = _value;
            Desption = desption;
        }
        public short Value { set; get; }
        public string Desption { set; get; }
    }
}
