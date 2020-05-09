using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 故障代码表 扩展Model
    /// </summary>
    [Serializable]
    public class FaultCodeTableModel : FaultCodeTable {
        public virtual string ClothingVehicleName { set; get; }
        public virtual string ClothingVehicleTypeId { set; get; }
    }
}
