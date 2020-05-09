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
    public class FullSiteMessage : MessageBody
    {
        private const string address = SuspeConstants.address_FullSite;
        public FullSiteMessage(byte[] resBytes) : base(resBytes)
        { }
        public static FullSiteMessage isEqual(Byte[] resBytes)
        {
            var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            if (!SuspeConstants.cmd_FullSite.Equals(rCmd)) return null;

            var raddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!address.Equals(raddress)) return null;
            var tag = HexHelper.BytesToHexString(new byte[] {  resBytes[11] });
            //if (!SuspeConstants.data_FullSite_Tag.Equals(tag)) {
            //    return null;
            //}
            return new FullSiteMessage(resBytes) { IsFullSite  = SuspeConstants.data_FullSite_Tag.Equals(tag) };
        }
        public static FullSiteMessage isFullSiteQueryEqual(Byte[] resBytes)
        {
            var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            if (!SuspeConstants.cmd_FullSite_Res.Equals(rCmd)) return null;

            var raddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!address.Equals(raddress)) return null;
            var tag = HexHelper.BytesToHexString(new byte[] { resBytes[11] });
            //if (!SuspeConstants.data_FullSite_Tag.Equals(tag)) {
            //    return null;
            //}
            return new FullSiteMessage(resBytes) { IsFullSite = SuspeConstants.data_FullSite_Tag.Equals(tag) };
        }
        public bool IsFullSite { set; get; }
    }
}
