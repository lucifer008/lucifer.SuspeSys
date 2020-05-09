using Microsoft.VisualStudio.TestTools.UnitTesting;
using SusNet.Common.Model;
using SusNet.Common.Utils;
using System.Threading;

namespace SuspeSys.CoreTest.Core.Hanging
{
    [TestClass]
    public class HangingTest: TestBase
    {
        
        [TestMethod]
        public void TestHangerReadCard()
        {
            var hanger = new Hanger();
            var hexHangerNo = HexHelper.TenToHexString10Len(1);
            hanger.MainTrackNo = "1";
            hanger.StatingNo = "4";
            var fData = string.Format("{0} {1} 06 FF 00 54 01 {2}", HexHelper.TenToHexString2Len(hanger.MainTrackNo), HexHelper.TenToHexString2Len(hanger.StatingNo), hexHangerNo);
            var data = HexHelper.StringToHexByte(fData);
             client.SendData(data);
            Thread.Sleep(25000);
        }

      
    }
}
