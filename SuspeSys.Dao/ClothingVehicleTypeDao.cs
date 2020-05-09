using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 衣车类别Dao
    /// </summary>
    public class ClothingVehicleTypeDao : DataBase<ClothingVehicleType> {
        private static readonly ClothingVehicleTypeDao clothingvehicletypeDao=new ClothingVehicleTypeDao();
        private ClothingVehicleTypeDao() { }
        public static  ClothingVehicleTypeDao Instance {
            get {
                return  clothingvehicletypeDao;
            }
        }
    }
}
