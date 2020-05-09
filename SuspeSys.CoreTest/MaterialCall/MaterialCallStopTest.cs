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
    [TestClass]
    public class MaterialCallStopTest : TestBase
    {
        public int mainTrackNumber = 0;

        public int statingNo = 1;

        [TestInitialize]
        public void InitData()
        {
            mainTrackNumber = 1;
            statingNo = 1;
        }

        [TestMethod]
        public void MaterialCallStopTest1()
        {
            var hexMessage = string.Format("{0} {1} 06 FF 02 23 00 00 00 00 00 FF", HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo));

            log.InfoFormat("终止缺料---->{0}", hexMessage);
            var data = HexHelper.StringToHexByte(hexMessage);
            MessageBody message = new MessageBody(data);
            client.SendData(message.GetBytes());
            log.InfoFormat("终止缺料---->{0}", hexMessage);

            Thread.CurrentThread.Join(60000);
        }

        [TestMethod]
        public void MaterialCallStopTest2()
        {
            var hexMessage = string.Format("{0} {1} 06 FF 02 23 00 00 00 00 00 01", HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo));

            log.InfoFormat("终止缺料---->{0}", hexMessage);
            var data = HexHelper.StringToHexByte(hexMessage);
            MessageBody message = new MessageBody(data);
            client.SendData(message.GetBytes());
            log.InfoFormat("终止缺料---->{0}", hexMessage);

            Thread.CurrentThread.Join(60000);
        }
    }
}
