using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public class CardRequestType
    {
        /// <summary>
        /// 卡错误
        /// </summary>
        public  static readonly CardRequestType CardError = new CardRequestType(0, "卡错误");
        /// <summary>
        /// 衣架卡
        /// </summary>
        public static readonly CardRequestType HangerCard = new CardRequestType(1, "衣架卡");
        /// <summary>
        /// 衣车卡
        /// </summary>
        public static readonly CardRequestType SewingCard = new CardRequestType(2, "机修卡");
        /// <summary>
        ///// 机修卡
        ///// </summary>
        //public static readonly CardRequestType MechanicCard = new CardRequestType(3, "机修卡");
        /// <summary>
        /// 员工已登录
        /// </summary>
        public static readonly CardRequestType EmployeeLogined = new CardRequestType(3, "员工已登录");
        /// <summary>
        /// 员工已登出
        /// </summary>
        public static readonly CardRequestType EmployeeLoginOut = new CardRequestType(4, "员工已登出");
        
        /// <summary>
        /// 员工已下班
        /// </summary>
        public static readonly CardRequestType EmployeeGoOffWork = new CardRequestType(6, "员工已下班");
        private CardRequestType(short _value, string desption)
        {
            Value = _value;
            Desption = desption;
        }
        public short Value { set; get; }
        public string Desption { set; get; }
    }
}
