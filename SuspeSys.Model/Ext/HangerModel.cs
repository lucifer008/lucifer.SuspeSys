using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 衣架 扩展Model
    /// </summary>
    public class HangerModel : Hanger {
        public string HexMaintrackNumber { set; get; }
        public string HexMStatingNo { set; get; }
        public string TenHangerNo { set; get; }
        public string HexCMD { set; get; }
       
    }
}
