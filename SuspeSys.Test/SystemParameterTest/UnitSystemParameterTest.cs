using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Service.Impl.SusRedis;
using SuspeSys.Service.Impl.Products.SusCache.Service;

namespace SuspeSys.Test.SystemParameterTest
{
    [TestClass]
    public class UnitSystemParameterTest
    {
        //public static string Main

        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestInitialize]
        public void TestMethod()
        {
            NewSusRedisClient.Instance.Init();

            SusCacheProductService.Instance.LoadSystemParameter();

            SusCacheProductService.Instance.LoadSystemParameter();
        }

        [TestMethod]
        public void TestGetHangUpStating1()
        {
            string value = SystemParameterService.Instance.GetHangUpLineParameterValue(Domain.SusEnum.SystemParameterHangUpLineHanger.ChecksumQuantityGreaterThanOrderQuantity, "1", "1");

            Assert.IsTrue(value.Equals("100"));
        }

        [TestMethod]
        public void TestGetHangUpStating2()
        {
            string value = SystemParameterService.Instance.GetHangUpLineParameterValue(Domain.SusEnum.SystemParameterHangUpLineHanger.ChecksumQuantityGreaterThanOrderQuantity, "1", "5");

            Assert.IsTrue(value.Equals(""));
        }

        [TestMethod]
        public void TestGetSystemProduct1()
        {
            string value = SystemParameterService.Instance.GetSystemProduct(Domain.SusEnum.SystemAttendanceProduct.CalcRealMinute1);
            Assert.IsFalse(value.Equals("1"));
        }

        [TestMethod]
        public void TestGetSystemProduct2()
        {
            string value = SystemParameterService.Instance.GetSystemProduct(Domain.SusEnum.SystemAttendanceProduct.EffType);
            Assert.IsTrue(value.Equals("SewingEfficiency"));
        }
    }
}
