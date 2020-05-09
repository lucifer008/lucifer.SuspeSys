using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public sealed class SusModuleType : BaseSusEnum
    {
        /// <summary>
        /// 用户参数
        /// </summary>
        public static readonly SusModuleType UserParamter = new SusModuleType(0, "用户参数");

        /// <summary>
        /// 客户机参数
        /// </summary>
        public static readonly SusModuleType CustomerMancheParamter = new SusModuleType(1, "客户机参数");

        /// <summary>
        /// 吊挂线
        /// </summary>
        public static readonly SusModuleType HangUpLine = new SusModuleType(2, "吊挂线");

        /// <summary>
        /// 系统
        /// </summary>
        public static readonly SusModuleType System = new SusModuleType(3, "系统");


        private SusModuleType(short _value, string _desption):base(_value,_desption){
        }

    }
}
