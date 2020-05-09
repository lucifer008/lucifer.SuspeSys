using log4net;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Transform;
using SuspeSys.Service.Impl.CommonQuery;
using SuspeSys.Service.Impl.CommonService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Impl.CommonQuery.Tests
{
    
    [TestClass()]
    public class CommonQueryServiceImplTests
    {
        static string hibernaterConfigPath = new FileInfo("Config/hibernate.cfg.xml").FullName;
        [TestInitialize]
        public void Init()
        {
            //var log4netFileInfo = new FileInfo("Config/log4net.cfg.xml");
            //XmlConfigurator.Configure(log4netFileInfo);

            //   ILog log =  LogManager.GetLogger(typeof(CommonQueryServiceImplTests));
            //log.Info("info---->hibernate init....");

            //Console.WriteLine("Init....");

            //跟踪nhibernate执行的sql配置
           // HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }
     
        [TestMethod()]
        public void GetListTest()
        {
            var service = new CommonServiceImpl<SuspeSys.Domain.Customer>();
            var lst = service.GetList();
            Console.WriteLine("记录数:"+ lst.Count);
            Console.WriteLine("Hello Word");
        }
        Random rdom = new Random(100);
        [TestMethod()]
        public void SaveTest()
        {
            var service = new CommonServiceImpl<SuspeSys.Domain.Customer>();
            SuspeSys.Domain.Customer cus = new Domain.Customer();
            cus.Id = Guid.NewGuid().ToString().Substring(0, 32);
            cus.CusNo = "c0001"+ rdom.Next();
            cus.CusName = "测试客户0001" + rdom.Next(); ;
            cus.Address = "北京市朝阳区民族家园" + rdom.Next(); ;
            cus.Tel = "18311329010";
            cus.LinkMan = "lucifer" + rdom.Next(); ;
           
            service.Save(cus);
            
            Console.WriteLine("success");
        }
        [TestMethod()]
        public void SaveOrderTest()
        {
            var service = new CommonServiceImpl<SuspeSys.Domain.ProcessOrder>();
            SuspeSys.Domain.ProcessOrder pOrder = new Domain.ProcessOrder();
           // pOrder.Id = Guid.NewGuid().ToString().Substring(0, 32);
            pOrder.POrderNo = "c0001" + rdom.Next();
            pOrder.CustomerName = "测试订单0001" + rdom.Next(); ;
            pOrder.Status =0;
            
            service.Save(pOrder);

            Console.WriteLine("success");
        }
        /// <summary>
        /// 通过Discriminator直接映射子类
        /// </summary>
        [TestMethod]
        public void TestQuery() {
            var session = Dao.Nhibernate.SessionFactory.OpenSession();
            var queryString = string.Format(@"SELECT [Id]
                              ,[STYLE_Id]
                              ,[CUSTOMER_Id]
                              ,[POrderNo]
                              ,[MOrderNo]
                              ,[ProductNoticeOrderNo]
                              ,[Num]
                              ,[Status]
	                          ,(case [Status]
	                          when 0 then '待审核'
	                          when 1 then '已审核'
	                          when 2 then '制作完成'
	                          when 3 then '生产中'
	                          when 4 then '完成'
	                          else
	                          '错误状态' 
	                          end 
	                          ) StatusText
                              ,[StyleCode]
                              ,[StyleName]
                              ,[CustomerNO]
                              ,[CustomerName]
                              ,[CustomerStyle]
                              ,[DeliveryDate]
                              ,[GenaterOrderDate]
                              ,[OrderNo]
                              ,[InsertDateTime]
                              ,[UpdateDateTime]
                              ,[InsertUser]
                              ,[UpdateUser]
                              ,[Deleted]
                              ,[CompanyId]
                          FROM [dbo].[ProcessOrder]");
            var rslt1 = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessOrderModel)).List<DaoModel.ProcessOrderModel>();
            Console.WriteLine("rowCount="+rslt1.Count);
        }

        [TestMethod]
        public void TestMappingQuery()
        {
            var session = Dao.Nhibernate.SessionFactory.OpenSession();
            var queryString = string.Format(@"select * from Customer");
            var rslt1 = session.CreateSQLQuery(queryString).SetResultTransformer(Transformers.AliasToBean(typeof(DaoModel.CustomerModel))).List<DaoModel.CustomerModel>();
            //  .AddEntity(typeof(DaoModel.CustomerModel)).List<DaoModel.CustomerModel>();
            Console.WriteLine("rowCount=" + rslt1.Count);
        }
        [TestMethod]
        public void TestDiscriminatorQuery()
        {
            var session = Dao.Nhibernate.SessionFactory.OpenSession();
            var queryString = string.Format(@"SELECT [Id]
                              ,[STYLE_Id] StyeId ,[CUSTOMER_Id] CustomerId,[POrderNo],[MOrderNo]
                              ,[ProductNoticeOrderNo] ,[Num],[Status]
	                          ,(case [Status]
	                          when 0 then '待审核'
	                          when 1 then '已审核'
	                          when 2 then '制作完成'
	                          when 3 then '生产中'
	                          when 4 then '完成'
	                          else
	                          '错误状态' 
	                          end 
	                          ) StatusText,[StyleCode],[StyleName],[CustomerNo],[CustomerName]
                              ,[CustomerStyle] ,[DeliveryDate],[GenaterOrderDate],[OrderNo],[InsertDateTime]
                              ,[UpdateDateTime]
                              ,[InsertUser]
                              ,[UpdateUser]
                              ,[Deleted]
                              ,[CompanyId]
                          FROM [dbo].[ProcessOrder]");
            var rslt1 = session.CreateSQLQuery(queryString).SetResultTransformer(Transformers.AliasToBean(typeof(DaoModel.ProcessOrderModel))).List<DaoModel.ProcessOrderModel>();
            Console.WriteLine("rowCount=" + rslt1.Count);
        }

    }
}