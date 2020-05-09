using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Dao {
    using NHibernate;
    using  SuspeSys.Domain;
    using SuspeSys.Dao.Base;
    using SuspeSys.Dao.Nhibernate;
    
    /// <summary>
    /// 卡片信息(
    ///   1:衣架卡
    ///   2:衣车卡
    ///   3:机修卡
    ///   4:员工卡)Dao
    /// </summary>
    public class CardInfoDao : DataBase<CardInfo> {
        private static readonly CardInfoDao cardinfoDao=new CardInfoDao();
        private CardInfoDao() { }
        public static  CardInfoDao Instance {
            get {
                return  cardinfoDao;
            }
        }
    }
}
