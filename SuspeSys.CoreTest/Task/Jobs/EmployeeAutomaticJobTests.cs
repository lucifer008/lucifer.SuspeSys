using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.CoreTest;
using SuspeSys.Service.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Task.Tests
{
    [TestClass()]
    public class EmployeeAutomaticJobTests: QueryTestBase
    {
        [TestMethod()]
        public void EmployeeAutomaticLoginExecuteTest()
        {
            EmployeeAutomaticJob EmployeeAutomaticJob = new EmployeeAutomaticJob();
            EmployeeAutomaticJob.EmployeeAutomaticLoginExecute();
        }
    }
}