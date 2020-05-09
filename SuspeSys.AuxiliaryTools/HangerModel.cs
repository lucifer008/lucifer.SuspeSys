using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.AuxiliaryTools
{
    
    /// <summary>
    /// 衣架 扩展Model
    /// </summary>
    public class HangerModel  {
        public string HexMaintrackNumber { set; get; }
        public string HexStatingNo { set; get; }
        public string TenHangerNo { set; get; }
        public string HexCMD { set; get; }

        public virtual long? HangerNo { get; set; }
    }
}
