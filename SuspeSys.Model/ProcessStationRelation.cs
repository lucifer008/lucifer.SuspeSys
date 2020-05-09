using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    public class ProcessStationRelation {
        public virtual Stating Stating { get; set; }
        /// <summary>
        /// 工艺图制作动作_Id
        /// </summary>
        public virtual string ProcessCraftActionId { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public virtual string Id { get; set; }
    }
}
