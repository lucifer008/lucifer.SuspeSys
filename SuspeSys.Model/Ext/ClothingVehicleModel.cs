using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 衣车 扩展Model
    /// </summary>
    [Serializable]
    public class ClothingVehicleModel : ClothingVehicle {
        public virtual string ClothingVehicleName { set; get; }
    }
}
