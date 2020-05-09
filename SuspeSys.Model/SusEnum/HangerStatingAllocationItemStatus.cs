using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public class HangerStatingAllocationItemStatus
    {

        /// <summary>
        /// 已分配/重新分配【0】
        /// </summary>
        public static readonly HangerStatingAllocationItemStatus Allocationed = new HangerStatingAllocationItemStatus(0, "已分配");

        /// <summary>
        /// 已完成/出站【1】
        /// </summary>
        public static readonly HangerStatingAllocationItemStatus Successed = new HangerStatingAllocationItemStatus(1, "已完成");

        /// <summary>
        /// 已进站【2】
        /// </summary>
        public static readonly HangerStatingAllocationItemStatus EnteredStating = new HangerStatingAllocationItemStatus(2, "已进站");

       
        private HangerStatingAllocationItemStatus(short _value, string desption)
        {
            Value = _value;
            Desption = desption;
        }
        public short Value { set; get; }
        public string Desption { set; get; }
    }
}
