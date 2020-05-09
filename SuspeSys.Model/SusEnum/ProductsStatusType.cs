using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    /// <summary>
    /// 状态:0:未分配;1:已分配;3.上线;4.已完成
    /// </summary>
    public sealed class ProductsStatusType
    {
        /// <summary>
        /// 未分配
        /// </summary>
        public static readonly ProductsStatusType NonAllocation = new ProductsStatusType(0, "未分配");
        /// <summary>
        /// 已分配
        /// </summary>
        public static readonly ProductsStatusType Allocationed = new ProductsStatusType(1, "已分配");

        /// <summary>
        /// 上线中
        /// </summary>
        public static readonly ProductsStatusType Onlineed = new ProductsStatusType(2, "上线中");

        /// <summary>
        /// 已完成
        /// </summary>
        public static readonly ProductsStatusType Successed = new ProductsStatusType(3, "已完成");
        private ProductsStatusType(byte _value, string desption)
        {
            Value = _value;
            Desption = desption;
        }
        public byte Value { set; get; }
        public string Desption { set; get; }
    }
}
