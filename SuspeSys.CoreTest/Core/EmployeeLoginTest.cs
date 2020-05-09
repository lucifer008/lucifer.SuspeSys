using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SusNet.Common.Utils;
using Sus.Net.Common.Utils;
using System.Data;
using System.Threading;

namespace SuspeSys.CoreTest.Core
{
    [TestClass]
    public class EmployeeLoginTest : TestBase
    {
        [TestMethod]
        public void TestStatingEmployeeLogin()
        {
            var data = new DataTable();
            data.Columns.Add("carNo");
            data.Columns.Add("EmployeeName");

            var cardList = new List<string>() { "1003479933", "1003479934", "1003479935", "1003479936" };
            var employeeList = new List<string>() { "张三0", "张三1", "张三2", "张三3" };

            for (var index = 0; index < 4; index++)
            {
                var dr = data.NewRow();
                dr["carNo"] = cardList[index];
                dr["EmployeeName"] = employeeList[index] + "__" + cardList[index];
                data.Rows.Add(dr);
            }
            var mainTrackNos = new List<int>() { 1, 2 };
            foreach (var mNos in mainTrackNos)
            {
                for (var index = 1; index < 13; index++)
                {
                   // var cIndex = GetRandIndex(0);//cardList.Count);
                    var cardNo = cardList[0];
                    //01 04 06 XX 00 60 00 AA BB CC DD EE
                    var fData = string.Format("{0} {1} 06 00 00 60 00 {2}", HexHelper.TenToHexString2Len(mNos), HexHelper.TenToHexString2Len(index), HexHelper.TenToHexString10Len(cardNo));
                    var data1 = HexHelper.StringToHexByte(fData);
                    //susTCPServer.SendMessageToAll(data);
                    client.SendData(data1);
                }
            }
            Thread.CurrentThread.Join(20000);
        }
        private Random random = new Random();
        int GetRandIndex(int rum) {
            return random.Next(rum - 1);
        }
    }
}
