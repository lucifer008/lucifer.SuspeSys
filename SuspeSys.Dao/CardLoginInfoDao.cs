using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 卡登录信息Dao
    /// </summary>
    public class CardLoginInfoDao : DataBase<CardLoginInfo> {
        private static readonly CardLoginInfoDao cardlogininfoDao=new CardLoginInfoDao();
        private CardLoginInfoDao() { }
        public static  CardLoginInfoDao Instance {
            get {
                return  cardlogininfoDao;
            }
        }
    }
}
