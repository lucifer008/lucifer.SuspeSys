using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain
{

    /// <summary>
    /// 款式工序库 扩展Model
    /// </summary>
    public class StyleProcessFlowStoreModel : BasicProcessFlowModel
    {
        public virtual string StyleNo { set; get; }
        public virtual string StyleName { set; get; }
        public virtual string StyleProcessFlowSectionItemId { set; get; }
    }
}
