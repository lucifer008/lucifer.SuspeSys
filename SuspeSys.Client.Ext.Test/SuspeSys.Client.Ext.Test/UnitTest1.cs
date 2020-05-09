using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Client.Sqlite.Repository;
using SuspeSys.Client.Sqlite.Entity;

namespace SuspeSys.Client.Ext.Test
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
            repository.Insert(new BasicInfo() {
                Name = "appId",
                Value = "123123",
                CreatedDate = DateTime.Now,
                Description = ""
            });
        }
    }
}
