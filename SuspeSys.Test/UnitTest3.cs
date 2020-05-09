using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Service.Impl.CommonService;
using SuspeSys.Domain;
using System.Xml.Linq;
using System.Linq;
using NHibernate.Cfg;
using System.Xml;
using SuspeSys.Support.Enums;
using SuspeSys.Dao;
using System.Collections.Generic;
using System.Windows.Forms;
using SuspeSys.Service.Impl.Products;
using System.Diagnostics;
using SuspeSys.Service.Impl.ProductionLineSet;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using SuspeSys.Service.Impl.SusRedis;

namespace SuspeSys.Test
{
    [TestClass]
    public class UnitPersonnelManagement
    {
        [TestMethod]
        public void TestDta()
        {
            TestSiteGroup();
            TestPosition();
            TestWorkType();
            TestDeptment();
            TestClientMachines();
            TestEmployee();
            GeneratorTestData data = new GeneratorTestData();
            data.TeatGeneratorUserInfo();

        }
          


        [TestMethod]
        public void TestSiteGroup()
        {
            //站点组测试
            var moduleService = new CommonServiceImpl<Domain.SiteGroup>();

            moduleService.Save(new SiteGroup()
            {
                FactoryCode = "Factory1",
                WorkshopCode = "Workshop1",
                GroupNo = "GroupNo1",
                GroupName = "GroupName1",
                
            });

            moduleService.Save(new SiteGroup()
            {
                FactoryCode = "Factory2",
                WorkshopCode = "Workshop2",
                GroupNo = "GroupNo2",
                GroupName = "GroupName2",

            });

            moduleService.Save(new SiteGroup()
            {
                FactoryCode = "Factory3",
                WorkshopCode = "Workshop3",
                GroupNo = "GroupNo3",
                GroupName = "GroupName3",

            });

        }

        [TestMethod]
        public void TestPosition()
        {
            //站点组测试
            var moduleService = new CommonServiceImpl<Domain.Position>();

            moduleService.Save(new Position()
            {
                PosCode = "PosCode1",
                PosName = "PosName1",

            });

            moduleService.Save(new Position()
            {
                PosCode = "PosCode2",
                PosName = "PosName2",

            });

            moduleService.Save(new Position()
            {
                PosCode = "PosCode3",
                PosName = "PosName3",

            });
        }

        [TestMethod]
        public void TestWorkType()
        {
            //站点组测试
            var moduleService = new CommonServiceImpl<Domain.WorkType>();

            moduleService.Save(new WorkType()
            {
                WTypeCode = "PosCode1",
                WTypeName = "PosName1",

            });

            moduleService.Save(new WorkType()
            {
                WTypeCode = "PosCode2",
                WTypeName = "PosName3",

            });

            moduleService.Save(new WorkType()
            {
                WTypeCode = "PosCode2",
                WTypeName = "PosName3",

            });
        }

        [TestMethod]
        public void TestDeptment()
        {
            //站点组测试
            var moduleService = new CommonServiceImpl<Domain.Department>();

            moduleService.Save(new Department()
            {
                DepNo = "PosCode1",
                DepName = "PosName1",

            });

            moduleService.Save(new Department()
            {
                DepNo = "PosCode1",
                DepName = "PosName1",

            });

            moduleService.Save(new Department()
            {
                DepNo = "PosCode1",
                DepName = "PosName1",

            });
        }

        [TestMethod]
        public void TestClientMachines()
        {
            var service = new CommonServiceImpl<Domain.ClientMachines>();

            service.Save(new ClientMachines() { ClientMachineName = "client1", AuthorizationInformation = "", ClientMachineType =(short)MachineType.Manage });
            service.Save(new ClientMachines() { ClientMachineName = "client2", AuthorizationInformation = "", ClientMachineType = (short)MachineType.Manage });
            service.Save(new ClientMachines() { ClientMachineName = "client3", AuthorizationInformation = "", ClientMachineType = (short)MachineType.Search });
            service.Save(new ClientMachines() { ClientMachineName = "client4", AuthorizationInformation = "", ClientMachineType = (short)MachineType.Manage });
        }

        [TestMethod]
        public void TestEmployee()
        {
            var service = new CommonServiceImpl<Domain.Employee>();

            service.Save(new Employee() { Code = Guid.NewGuid().ToString().Substring(0,6) , RealName =  "张三"});
            service.Save(new Employee() { Code = Guid.NewGuid().ToString().Substring(0, 6), RealName  = "李四" });
        }

        [TestMethod]
        public void TestQuery()
        {
            try
            {
                //var service = new CommonServiceImpl<Domain.UserSiteGroup>();

                //service.Query<Domain.UserSiteGroup>("SELECT * FROM UserSiteGroup WHERE UserId = ?", false, "540d8d97fb844ac48824d00645d4834c");

                var tmep = Dao.UserRolesDao.Instance.CreateQuery("from UserRoles where Users.Id like '%1%'").List();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [TestMethod]
        public void TestEditDbConfig()
        {
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Config/hibernate.cfg.xml");

            XDocument doc = XDocument.Load(path);
            doc.Elements().First()
                .Elements().First()
                .Elements().Where(o => o.Attribute("name").Value == NHibernate.Cfg.Environment.ConnectionString).First()
                .SetValue("123123");
            doc.Save(path, SaveOptions.DisableFormatting);

            Configuration conf = new Configuration();

            conf.Configure(path);

            conf.Properties[NHibernate.Cfg.Environment.ConnectionString] = "";
            //conf.


        }

        [TestMethod]
        public void TestEdit()
        {
            PSize pSize = new PSize()
            {
                Id = "b726c14d034f4c3c907b2ecad2393c13",
                PsNo = "s0010",
                Size = "S",
                SizeDesption = "SSize备注0123123123",
                //InsertDateTime = DateTime.Now
            };

            //PSizeDao.Instance.Edit(pSize);
            DapperHelp.Edit(pSize);
        }

        //[TestMethod]
        //public void TestEdit1()
        //{
        //    Dictionary<string, object> dicColumn = new Dictionary<string, object>();
        //    dicColumn.Add("SizeDesption", "123123123");

        //    Dictionary<string, object> dicWhere = new Dictionary<string, object>();
        //    dicWhere.Add("Id", "2324e52d940343d887f42e60cd2de851");

        //    //PSizeDao.Instance.Edit(dicColumn, dicWhere);
        //    DapperHelp.Edit<PSize>(dicColumn, dicWhere);
        //}

        [TestMethod]
        public void TestAdd()
        {
            PSize pSize = new PSize()
            {
                Id = "2324e52d940343d887f42e60cd2de851",
                PsNo = "s0010",
                Size = "S",
                SizeDesption = "SSize备注0123123123",
                InsertDateTime = DateTime.Now
                //InsertDateTime = DateTime.Now
            };

            PSizeDao.Instance.Insert(pSize); 
        }

        [TestMethod]
        public void TestAdd1()
        {
            PSize pSize = new PSize()
            {
                Id = "2324e52d940343d887f42e60cd2de851",
                PsNo = "s0010",
                Size = "S",
                SizeDesption = "SSize备注0123123123",
                InsertDateTime = DateTime.Now
                //InsertDateTime = DateTime.Now
            };

            //PSizeDao.Instance.Add(pSize);
            DapperHelp.Add(pSize);
        }
        [TestMethod]
        public void TestAdd2()
        {

            //conf.Properties.Add(NHibernate.Cfg.Environment.ConnectionString, SystemConfig.NConnectString);
            //sessionFactory = conf.BuildSessionFactory();

            PSize pSize = new PSize()
            {
                Id = "2324e52d940343d887f42e60cd2de851",
                PsNo = "s0010123",
                Size = "S",
                SizeDesption = "SSize备注0123123123123123121323",
                InsertDateTime = DateTime.Now
                //InsertDateTime = DateTime.Now
            };

            DapperHelp.Add(pSize);
        }

        [TestMethod]
        public void TestAdd3()
        {

            //conf.Properties.Add(NHibernate.Cfg.Environment.ConnectionString, SystemConfig.NConnectString);
            //sessionFactory = conf.BuildSessionFactory();

            PSize pSize = new PSize()
            {
                Id = "2324e52d940343d887f42e60cd2de851",
                PsNo = "s0010",
                Size = "S",
                SizeDesption = "SSize备注0123123123",
                InsertDateTime = DateTime.Now
                //InsertDateTime = DateTime.Now
            };

            DapperHelp.Add(pSize);
        }
        [TestMethod]
        public void TestQuery1()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            string sql = "SELECT * FROM PSize WHERE PsNo = ?";
            CacheService.Instance.Query<PSize>(sql, false, "s");

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            Console.WriteLine(elapsedTime);
        }

        [TestMethod]
        public void TestQuery2()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            string sql = "SELECT * FROM PSize WHERE PsNo = @PsNo";


            var list = DapperHelp.Query<PSize>(sql, new { PsNo = "s" });

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            Console.WriteLine(elapsedTime);
        }

        [TestMethod]
        public void Execute1()
        {
            string sql = "update PSize set Memo= @Memo where Id = @Id";

            int eff = DapperHelp.Execute(sql, new { Memo = "123123", Id = "2324e52d940343d887f42e60cd2de851" });

            Assert.IsTrue(eff > 0);
        }

        [TestMethod]
        public void QueryForObjectOrDefault()
        {
            string sql = "select * from PSize where Id = @Id";

            var obj = DapperHelp.FirstOrDefault<PSize>(sql, new { Memo = "123123", Id = "2324e52d940343d887f42e60cd2de851" });

            Assert.IsTrue(obj != null);
        }

        [TestMethod]
        public void QueryForObjectOrDefault2()
        {
            string sql = "select * from PSize where Id = @Id";

            var obj = DapperHelp.FirstOrDefault<PSize>(sql, new { Memo = "123123", Id = "12324e52d940343d887f42e60cd2de851" });

            Assert.IsTrue(obj == null);
        }

        [TestMethod]
        public void TestLoad()
        {
            NewSusRedisClient.Instance.Init();
            SusCacheProductService.Instance.LoadStating();

            ProductionLineSetServiceImpl.Instance.GetStatingList();
        }
        [TestMethod]
        public void TestByteConvertIntValue() {
            byte[] bb = new byte[] { 0,1,2,3,4,5,6,7,8,9,10};
            foreach (var b in bb) {
                Console.WriteLine(string.Format("byte value: {0} int value: {1}",b,(int)b));
            }
        }
    }
}
