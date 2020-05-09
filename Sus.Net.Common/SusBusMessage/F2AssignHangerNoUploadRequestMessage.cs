using Sus.Net.Common.Constant;
using SusNet.Common.Message;
using SusNet.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sus.Net.Common.SusBusMessage
{
    /// <summary>
    /// F2指定衣架号上传
    /// </summary>
    public class F2AssignHangerNoUploadRequestMessage : MessageBody
    {
        public F2AssignHangerNoUploadRequestMessage(byte[] resBytes) : base(resBytes)
        {
        }
       
        public static F2AssignHangerNoUploadRequestMessage isEqual(byte[] resBytes)
        {
            var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            if (!SuspeConstants.cmd_F2_Assign_Req.Equals(rCmd)) return null;

            var raddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_F2_Upload_HangerNO_Assign.Equals(raddress)) return null;
            IList<byte> bList = new List<byte>();
            for (var index = 7; index < 12; index++)
            {
                bList.Add(resBytes[index]);
            }
            return new F2AssignHangerNoUploadRequestMessage(resBytes)
            {
                //TargertMainTrackNuber = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[6])),
                //TargertStatingNo = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[2])),
                SourceMainTrackNuber = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[0])),
                SourceStatingNo = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[1])),
                HangerNo = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()))
                //HangerNo = HexHelper.HexToTen(HexHelper.BytesToHexString(bList.ToArray()))
            };
        }
        /// <summary>
        /// 发起主轨
        /// </summary>
        public int SourceMainTrackNuber { set; get; }
        /// <summary>
        /// 发起站点
        /// </summary>
        public int SourceStatingNo { set; get; }
        /// <summary>
        /// 指定主轨
        /// </summary>
        public int TargertMainTrackNuber { set; get; }
        /// <summary>
        /// 指定站点
        /// </summary>
        public int TargertStatingNo { set; get; }
        public int HangerNo { set; get; }
    }

}
