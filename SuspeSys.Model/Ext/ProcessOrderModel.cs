using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {

    /// <summary>
    /// 制单 扩展Model
    /// </summary>
    [Serializable]
    public class ProcessOrderModel : ProcessOrder {
        public ProcessOrderModel() { }

        public virtual string StatusText { set; get; }
        public virtual string StyleId { set; get; }
        public virtual string CustomerId { set; get; }
    }
}
