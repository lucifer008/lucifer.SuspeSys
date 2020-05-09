using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    public class ClientMachinesDao : DataBase<ClientMachines> {
        private static readonly ClientMachinesDao clientmachinesDao=new ClientMachinesDao();
        private ClientMachinesDao() { }
        public static  ClientMachinesDao Instance {
            get {
                return  clientmachinesDao;
            }
        }
    }
}
