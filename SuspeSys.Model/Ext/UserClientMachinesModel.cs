using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    [Serializable]
    public class UserClientMachinesModel : UserClientMachines {
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; }
    }
}
