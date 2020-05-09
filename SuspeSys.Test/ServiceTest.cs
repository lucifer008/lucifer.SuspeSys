using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Service.Impl.Common;
using SuspeSys.Service.Impl.CommonService;
using SuspeSys.Service.Impl.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Test
{
    [TestClass()]
    public class ServiceTest : TestBase
    {
        private long total;

        [TestMethod]
        public void TestQuery()
        {
            var service = new CommonServiceImpl();
            var sql = new StringBuilder();
            sql.Append("select * from CustomerPurchaseOrder where id=?");
            var orderCondition = new Dictionary<string,string>();
            orderCondition.Add("InsertDateTime", "desc");

            long totalCount;
            //var list= service.Query<DaoModel.CustomerPurchaseOrder>(sql, orderCondition, true, new object[] { "404d80b4cc1f4f64acf89708c323f4f3" });
            //var list = service.Query<DaoModel.CustomerPurchaseOrder>(sql, orderCondition, false, new object[] { "404d80b4cc1f4f64acf89708c323f4f3" });
            sql.Clear();
            sql.Append("select * from CustomerPurchaseOrder where id=? and CusName like ?");
            var pValues = new object[] {
                "404d80b4cc1f4f64acf89708c323f4f3"
                ,"%测试%"
            };
            var list = service.Query<DaoModel.CustomerPurchaseOrder>(sql, 1, 20, out totalCount, orderCondition, false, pValues);

            if (list.Count != 0)
            {
                var cad = list[0].Customer?.Address;
                Console.WriteLine(cad);
            }
        }

        [TestMethod]
        public void TestQuery2() {
            var service = new CommonServiceImpl();
            var sql = new StringBuilder();
            sql.Append("select * from CustomerPurchaseOrder");
            var orderCondition = new Dictionary<string,string>();
            orderCondition.Add("InsertDateTime", "desc");
            var condition = new Dictionary<string,string>();
            condition.Add("CusName", " like '%测试%'");
          var list=  service.Query<DaoModel.CustomerPurchaseOrder>(sql, 1, 20, out total, condition, orderCondition, false);
        }
        [TestMethod]
        public void TestCurrentProductNumber() {
            var ps = new ProductsServiceImpl();
            var pNumber=ps.GetCurrentProductNumber();

        }
    }
}
