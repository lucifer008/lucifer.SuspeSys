using Microsoft.VisualStudio.TestTools.UnitTesting;
using SusNet.Common.Message;
using SusNet.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuspeSys.CoreTest.MaterialCall
{
    /// <summary>
    /// 缺料请求测试用例
    /// </summary>

    [TestClass]
    public class MaterialCallTest: TestBase
    {
        public int mainTrackNumber = 0;

        public int statingNo = 1;

        [TestInitialize]
        public void InitData()
        {
            mainTrackNumber = 1;
            statingNo = 1;
        }

        /// <summary>
        /// 0220
        /// </summary>
        [TestMethod]
        public void MaterialCallRequestTest()
        {
            var hexMessage = string.Format("{0} {1} 06 FF 02 20 00 00 00 00 00 00", HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo));

            log.InfoFormat("缺料呼叫---->{0}", hexMessage);
            var data = HexHelper.StringToHexByte(hexMessage);
            MessageBody message = new MessageBody(data);
            client.SendData(message.GetBytes());
            log.InfoFormat("缺料呼叫---->{0}", hexMessage);

            Thread.CurrentThread.Join(60000);
        }
    }
}
