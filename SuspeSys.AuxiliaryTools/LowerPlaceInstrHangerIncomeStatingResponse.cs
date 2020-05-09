using SusNet.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuspeSys.AuxiliaryTools
{
    public class LowerPlaceInstrHangerIncomeStatingResponse: ToolsBase
    {
        public static LowerPlaceInstrHangerIncomeStatingResponse Instance = new LowerPlaceInstrHangerIncomeStatingResponse();
        /// <summary>
        /// 进站回应
        /// </summary>
        /// <param name="tenMainTrackNumber1"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        public void HangerComeInStating(string tenMainTrackNumber1,string tenStatingNo,string tenHangerNo)
        {
            //Thread.Sleep(2000);
            //衣架进站回应
            var data = string.Format("{0} {1} 02 FF 00 50 00 {2}", HexHelper.TenToHexString2Len(tenMainTrackNumber1), HexHelper.TenToHexString2Len(tenStatingNo), string.Format("{0}", HexHelper.TenToHexString10Len(tenHangerNo)));
            var dBytes = HexHelper.StringToHexByte(data);
            client.SendData(dBytes);
        }
    }
}
