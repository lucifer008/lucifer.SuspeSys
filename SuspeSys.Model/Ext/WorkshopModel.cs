using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 车间 扩展Model
    /// </summary>
    [Serializable]
    public class WorkshopModel : Workshop {
        public virtual string FacName { set; get; }
        public virtual string FacId { set; get; }
    }
}
