using Sus.Net.Client;
using Sus.Net.Common.Constant;
using SusNet.Common.Message;
using SusNet.Common.Utils;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.AuxiliaryTools
{
    public class ToolsBase
    {
        public static SusTCPClient client = null;

        /// <summary>
        /// 是否是单元测试输出
        /// </summary>
        public static bool isUnitTest = false;
        //public static IList<Hanger> AllocationHangerList = new List<Hanger>();
        public static readonly ConcurrentDictionary<string, HangerModel> AllocationHanger = new ConcurrentDictionary<string, HangerModel>();

        //private object k
        public static HangerModel ParseMessage(string hexMessage)
        {
            var byteData = HexHelper.StringToHexByte(hexMessage);
            var messageBody = new MessageBody(byteData);
            var hanger = new HangerModel();
            hanger.HexMaintrackNumber = messageBody.XID;
            hanger.HexStatingNo = messageBody.ID;
            hanger.HexCMD = messageBody.CMD;
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(byteData[index]);
            }
            hanger.HangerNo = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()));
            return hanger;
        }
    }
}
