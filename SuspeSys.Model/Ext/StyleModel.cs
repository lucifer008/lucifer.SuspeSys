using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 款式工艺 扩展Model
    /// </summary>
    public class StyleModel : Style {

        public virtual bool IsSelected { set; get; }
    }
}
