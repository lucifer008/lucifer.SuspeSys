using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Service.Impl.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.Tests
{
    [TestClass()]
    public class CacheServiceTests
    {
        [TestMethod()]
        public void CacheProcessFlowChartTest()
        {
            CacheService.Instance.CacheProcessFlowChart();
        }
    }
}