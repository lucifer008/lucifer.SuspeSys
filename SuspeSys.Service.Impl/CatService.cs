using SuspeSys.Domain;
using System.Collections.Generic;
using System;

using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Service.Impl.Base;

namespace SuspeSys.Service.Impl
{
    public class CatService : ServiceBase, ICatService
    {
        private readonly static CatService catService = new CatService();
        private CatService() { }
        public static CatService Instance { get { return catService; } }
        //public void AddCat()
        //{
        //    Cat princess = new Cat();
        //    princess.Name = "Princess" + new Random(1000).Next();
        //    princess.Sex ="F";
        //    princess.Weight = "22";
        //    CatDao.Instance.Save(princess);


        //    //new CatDao().AddCat();
        //}

        //public IList<Cat> GetCatList()
        //{
        //    return null;
        //    //return CatDao.Instance.GetAll();//new CatDao().GetCatList();

        //}
        public string TestRemoting() {
            return "Hello Remoting";
        }
        public void AddEmployee(Employee employee) {
            EmployeeDao.Instance.Save(employee);
        }
        public void TestTransaction()
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var t = session.BeginTransaction())
                {
                    //Cat cat = new Cat();
                    //cat.Name = "张三";
                    //cat.Sex = 'M';
                    //cat.Weight = 220;
                    //session.Save(cat);

                    //ProcessOrder order = new ProcessOrder();
                    //order.CustomerNo = "c0001";
                    //order.InsertDateTime = DateTime.Now;
                    //order.InsertUser = "lucifer";
                    //order.OrderName = "测试订单";
                    //order.ProcessDate = DateTime.Now;

                    //session.Save(order);

                    t.Commit();
                }
            }
        }
    }
}
