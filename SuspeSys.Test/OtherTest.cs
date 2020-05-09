using log4net;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sus.Net.Client;
using Sus.Net.Client.Sockets;
using Sus.Net.Common.Entity;
using Sus.Net.Common.Event;
using SuspeSys.Remoting;
using SuspeSys.Service.Impl.Common;
using SuspeSys.Service.Impl.CommonService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Test
{
    [TestClass()]
    public class OtherTest//: TestBase
    {

        [TestMethod]
        public void Test() {
            var service = new CommonServiceImpl<DaoModel.ProcessFlowChart>();
            var condition = new Hashtable();
            condition.Add("LinkName", "1_1_171209-01_17120901");
            var reslt = service.CheckIsExist(condition);
            Console.WriteLine(reslt);
        }
        [TestMethod]
        public void TestPageQuery() {
            var sql = @"SELECT [Id],[STYLE_Id] ,[CUSTOMER_Id] ,[POrderNo],[MOrderNo]
                              ,[ProductNoticeOrderNo],[Num],[Status]
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
                              ,[CustomerStyle],[DeliveryDate],[GenaterOrderDate],[OrderNo]
                              ,[InsertDateTime],[UpdateDateTime],[InsertUser],[UpdateUser]
                              ,[Deleted],[CompanyId]
        FROM[dbo].[ProcessOrder]";
            var service = new CommonServiceImpl();
            long totalCount;
            var condi = new Dictionary<string,string>();
           condi.Add("POrderNo","1");
            var orderCondi=new Dictionary<string,string>();
            orderCondi.Add("InsertDateTime","DESC");
            var list=service.Query<DaoModel.ProcessOrderModel>(new StringBuilder(sql), 1, 20, out totalCount, condi, orderCondi);
            Console.WriteLine(list.Count);
        }
    }

   
}
