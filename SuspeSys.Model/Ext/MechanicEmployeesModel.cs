using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 机修人员表 扩展Model
    /// </summary>
    [Serializable]
    public class MechanicEmployeesModel : MechanicEmployees {
        public bool StatusV { set; get; }
    }
}
