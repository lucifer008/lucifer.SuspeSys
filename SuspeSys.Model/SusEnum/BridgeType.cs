using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public class BridgeType
    {
        /// <summary>
        /// 桥接站都不在工艺图上
        /// </summary>
        public const string Bridge_Stating_Non_Flow_Chart_ALL = "Bridge_Stating_Non_Flow_Chart_ALL";
        /// <summary>
        /// 桥接站有一个在工艺图上
        /// </summary>
        public const string Bridge_Stating_One_In_Flow_Chart = "Bridge_Stating_One_In_Flow_Chart";


        /// <summary>
        /// 桥接站都在工艺图上
        /// </summary>
        public const string Bridge_Stating_IN_Flow_Chart_ALL = "Bridge_Stating_IN_Flow_Chart_ALL";

        /// <summary>
        /// 桥接站都在工艺图上之桥接站出战
        /// </summary>
        public const string Bridge_Stating_IN_Flow_Chart_ALL_Bridge_OutSite = "Bridge_Stating_IN_Flow_Chart_ALL_Bridge_OutSite";

        //public const string 
    }
}
