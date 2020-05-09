using SusNet.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.AuxiliaryTools
{
    public class LowerPlaceInstrHangerCompareRequest : ToolsBase
    {
        public static LowerPlaceInstrHangerCompareRequest Instance = new LowerPlaceInstrHangerCompareRequest();
        /// <summary>
        /// 衣架比较
        /// </summary>
        /// <param name="tenMainTrackNumber1"></param>
        /// <param name="tenStatingNo"></param>
        /// <param name="tenHangerNo"></param>
        public void SendHangerCompareRequest(string tenMainTrackNumber1,string tenStatingNo,string tenHangerNo)
        {
            ////衣架比较回应
            var data = string.Format("{0} {1} 06 FF 00 54 00 {2}", HexHelper.TenToHexString2Len(tenMainTrackNumber1), HexHelper.TenToHexString2Len(tenStatingNo), string.Format("{0}", HexHelper.TenToHexString10Len(tenHangerNo)));
            var dBytes = HexHelper.StringToHexByte(data);
            client.SendData(dBytes);

        }
    }
}
