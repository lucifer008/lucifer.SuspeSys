using NHibernate;
using SuspeSys.Dao.Base;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain;

using System.Collections.Generic;

namespace SuspeSys.Dao.Test
{
    using System;
    public class CatDao : DataBase<Cat>
    {
        private static readonly CatDao catDao = new CatDao();
        private CatDao() { }

        public static CatDao Instance {
            get { return catDao; }
        }
        public IList<Cat> GetCatList()
        {
            IList<Cat> catList = null;
            using (var session = SessionFactory.OpenSession()) //NHibernateHelper.GetSessionFactory().OpenSession())
            {
                //var tx = session.BeginTransaction();
                IQuery query = session.CreateQuery("select c from Cat as c where c.Sex = :sex");
                query.SetCharacter("sex", 'F');
                catList = query.List<Cat>();
                //tx.Commit();

            }
            return catList;
        }
        public void AddCat()
        {
            using (var session = SessionFactory.OpenSession())
            {
                ITransaction tx = session.BeginTransaction();

                Cat princess = new Cat();
                princess.Name = "Princess-"+new Random().Next(0,10000);
                princess.Sex = 'F';
                princess.Weight = 7.4f;

                session.Save(princess);
                tx.Commit();
            }
        }

        
    }
}
