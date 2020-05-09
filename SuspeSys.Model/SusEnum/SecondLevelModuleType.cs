using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public sealed class SecondLevelModuleType : BaseSusEnum
    {
        /// <summary>
        /// 用户
        /// </summary>
        public static readonly SecondLevelModuleType User = new SecondLevelModuleType(0, "用户");

        /// <summary>
        /// 基本
        /// </summary>
        public static readonly SecondLevelModuleType Basic = new SecondLevelModuleType(1, "基本");

        /// <summary>
        /// 挂片站
        /// </summary>
        public static readonly SecondLevelModuleType HangerPiece = new SecondLevelModuleType(2, "挂片站");

        /// <summary>
        /// 生产线
        /// </summary>
        public static readonly SecondLevelModuleType ProductsLine = new SecondLevelModuleType(3, "生产线");

        /// <summary>
        /// 其他
        /// </summary>
        public static readonly SecondLevelModuleType Other = new SecondLevelModuleType(4, "其他");

        /// <summary>
        /// 考勤
        /// </summary>
        public static readonly SecondLevelModuleType Attendance = new SecondLevelModuleType(5, "考勤");
        /// <summary>
        /// 考勤-生产
        /// </summary>
        public static readonly SecondLevelModuleType AttendanceProduct = new SecondLevelModuleType(6, "考勤-生产");
        /// <summary>
        /// 考勤-其他
        /// </summary>
        public static readonly SecondLevelModuleType AttendanceOther = new SecondLevelModuleType(7, "考勤-其他");

        private SecondLevelModuleType(short _value, string _desption):base(_value,_desption){
        }

    }
}
