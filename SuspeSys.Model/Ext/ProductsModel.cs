using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {

    /// <summary>
    /// 制品 扩展Model
    /// </summary>
    [Serializable]
    public class ProductsModel : Products {
        public virtual string StatusText { set; get; }
        /// <summary>
        /// 累计产出
        /// </summary>
        public virtual int TotalProdOutNum { set; get; }
        /// <summary>
        /// 累计挂片
        /// </summary>
        public virtual int TotalHangingNum { set; get; }
        public virtual string ProcessFlowChartId { set; get; }
        public virtual string ProcessOrderId { set; get; }

    }
}
