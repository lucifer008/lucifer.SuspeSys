using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuspeSys.CoreTest
{
    [TestClass]
    public class UnitTestLogin
    {
        [TestMethod]
        public void UnitTestLoginMethod()
        {
            Service.Impl.Permisson.PermissionServiceImpl service = new Service.Impl.Permisson.PermissionServiceImpl();
            string userName = "renshi";
            string pwd = "123";
            string clientId = "3ac620eca56a4b5e87ca96956ab95f8d";

            string msg = string.Empty;
            try
            {
                service.Login(userName, pwd, clientId);
            }
            catch (Exception ex)
            {

                msg = ex.Message;
            }

            Assert.IsTrue(string.IsNullOrEmpty(msg), msg);

        }
        [TestMethod]
        public void UnitTestAdminLoginMethod()
        {
            Service.Impl.Permisson.PermissionServiceImpl service = new Service.Impl.Permisson.PermissionServiceImpl();
            string userName = "admin";
            string pwd = "admin";
            string clientId = "";

            string msg = string.Empty;
            try
            {
                service.Login(userName, pwd, clientId);
            }
            catch (Exception ex)
            {

                msg = ex.Message;
            }

            Assert.IsTrue(string.IsNullOrEmpty(msg), msg);

        }
    }
}
