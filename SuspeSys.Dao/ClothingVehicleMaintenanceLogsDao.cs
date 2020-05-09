using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣车维修日志Dao
    /// </summary>
    public class ClothingVehicleMaintenanceLogsDao : DataBase<ClothingVehicleMaintenanceLogs> {
        private static readonly ClothingVehicleMaintenanceLogsDao clothingvehiclemaintenancelogsDao=new ClothingVehicleMaintenanceLogsDao();
        private ClothingVehicleMaintenanceLogsDao() { }
        public static  ClothingVehicleMaintenanceLogsDao Instance {
            get {
                return  clothingvehiclemaintenancelogsDao;
            }
        }
    }
}
