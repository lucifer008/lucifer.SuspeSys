using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Client.Sqlite.Repository;
using SuspeSys.Client.Sqlite.Entity;

namespace SuspeSys.Client.SqliteTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ConnectionFactory.CreateSqlConnection();

        }

        [TestMethod]
        public void TestMethod2()
        {
            BasicInfo basicInfo = new BasicInfo();

            Repository<BasicInfo> repository = new Repository<BasicInfo>();
            repository.Insert(new BasicInfo()
            {
                Name = "appId123",
                Value = "123123",
                CreatedDate = DateTime.Now,
                Description = ""
            });
        }

        [TestMethod]
        public void TestGetList()
        {
            Repository<BasicInfo> repository = new Repository<BasicInfo>();
            var temp = repository.GetList();

            var temp2 = repository.GetBySql(null, null);
        }
    }
}
