using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 工序段工序明细 扩展Model
    /// </summary>
    public class StyleProcessFlowSectionItemModel : StyleProcessFlowSectionItem {
        public virtual string BASICPROCESSFLOW_Id { set; get; }
        public virtual string STYLE_Id { set; get; }
        public virtual string PROCESSFLOWSECTION_Id { set; get; }
        public virtual string SAM { set; get; }
        public virtual string ProSectionName { set; get; }
        //public virtual string StyleNo { set; get; }
        //public virtual string StyleName { set; get; }
        //public virtual string StyleProcessFlowSectionItemId { set; get; }
    }
}
