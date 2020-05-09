using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣车Dao
    /// </summary>
    public class ClothingVehicleDao : DataBase<ClothingVehicle> {
        private static readonly ClothingVehicleDao clothingvehicleDao=new ClothingVehicleDao();
        private ClothingVehicleDao() { }
        public static  ClothingVehicleDao Instance {
            get {
                return  clothingvehicleDao;
            }
        }
    }
}
