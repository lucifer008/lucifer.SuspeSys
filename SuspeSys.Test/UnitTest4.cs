using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Domain;
using System.Collections.Generic;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Domain.Ext;
using SuspeSys.Utils.Reflection;
using System.Linq;
using SusNet.Common.Utils;

namespace SuspeSys.Test
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void TestRemoveAllMethod()
        {
            //var hangerStatingAllocationItemList = new List<HangerStatingAllocationItem>();
            //for (var index = 1; index <= 5; index++)
            //{
            //    var hsaItem = new HangerStatingAllocationItem();
            //    hsaItem.BatchNo = index;
            //    hangerStatingAllocationItemList.Add(hsaItem);
            //}
            //hangerStatingAllocationItemList.RemoveAll(f => f.BatchNo.Value > 3);
            //Console.WriteLine(hangerStatingAllocationItemList.Count);
            //var tt = "-1".Equals("-1");
            //HexHelper.HexToTen()
        }
        [TestMethod]
        public void TestException()
        {
            try
            {
                int k = 10;
                if (k == 10)
                {
                    throw new Exception("漫站异常");
                }
            }
            catch (FullStatingExcpetion ex)
            {
                //throw ex;
                Console.WriteLine(ex.Message);
            }

        }
        [TestMethod]
        public void TestBeaUitls()
        {
            var hangerProductFlowChart = new HangerProductFlowChartModel();
            hangerProductFlowChart.FlowIndex = 10;
            var outHangerProducts = BeanUitls<HangerProductItemModel, HangerProductFlowChartModel>.Mapper(hangerProductFlowChart);
        }
        [TestMethod]
        public void TestListDistinct()
        {
            var dc = new List<int>();
            dc.Add(15);
            var dcc = dc.Where(k => k != 15).Distinct();
            Console.WriteLine(dcc.Max());
        }

        [TestMethod]
        public void TestListExcept()
        {
            var dc = new List<int>();
            dc.Add(1);
            dc.Add(2);
            dc.Add(3);

            var dcc = new List<int>();
            dcc.Add(3);
            dcc.Add(4);
            dcc.Add(5);

            var d1 = dc.Except(dcc);
            var d11 = dcc.Except(dc);
            var d111 = dcc.Intersect(dc);
            //var d1111 = dcc.M(dc);
        }
        private void TestExpcetpion()
        {
            int i = 0;
            i = i / 0;
        }
        [TestMethod]
        public void TestExceptionThrow()
        {
            try
            {
                try
                {
                    TestException();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
