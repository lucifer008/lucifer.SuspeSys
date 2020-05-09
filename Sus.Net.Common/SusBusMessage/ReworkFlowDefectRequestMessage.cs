using Sus.Net.Common.Constant;
using Sus.Net.Common.Model;
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
    /// 0090~0099
    /// 返工工序和疵点代码，60字节全部为ASCII码，未用部份尾部填00

    ///以ASCII码传送
    ///业务上返工操作最多传递6个工序及6个疵点；
    ///例如：6道工序和疵点的传送格式如下：
    ///123，342；124，320；125，380；123，342；124，320；125，380

    ///传送时直接将液晶中填入的最多60字节返回，如果不足60字节，自动填00

    /// </summary>
    public class ReworkFlowDefectRequestMessage : MessageBody
    {
        private static bool _isOK = false;
        private static object _lock = new object();
        //[ThreadStatic]
        public static IDictionary<FlowDefectCodeModel, List<FlowDefectCodeItem>> DicFlowDefectList = new Dictionary<FlowDefectCodeModel, List<FlowDefectCodeItem>>();
        public ReworkFlowDefectRequestMessage(byte[] resBytes) : base(resBytes)
        {

        }
        public static IDictionary<FlowDefectCodeModel, List<FlowDefectCodeItem>> isEqual(Byte[] resBytes)
        {
            // Array ar = null;

            // var hexStr = HexHelper.byteToHexStr(bList.ToArray());
            var xid = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[0]));
            var id = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[1]));
            var cmd = HexHelper.TenToHexString2Len(resBytes[2]);
            var hexAddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            var address = HexHelper.HexToTen(hexAddress);
            if (address >= SuspeConstants.address_ReturnBegin && address <= SuspeConstants.address_ReturnEnd)
            {
                tcpLogHardware.Info(string.Format("【返工工序号及疵点】---->{0}", HexHelper.BytesToHexString(resBytes)));

                var fdc = new FlowDefectCodeModel(xid, id);
                lock (_lock)
                {
                    //if (!_isOK)
                    //{
                        var dBytes = new byte[] { resBytes[6], resBytes[7], resBytes[8], resBytes[9], resBytes[10], resBytes[11] };
                        if (DicFlowDefectList.ContainsKey(fdc))
                        {
                            var fdcc = DicFlowDefectList.Keys.Where(f => f.MainTrackNumber.Equals(xid) && f.StatingNo.Equals(id)).ToList()[0];
                            var v = DicFlowDefectList[fdcc];
                            
                            var fdcItem = new FlowDefectCodeItem(hexAddress);
                            if (v.Contains(fdcItem))
                            {
                                v.RemoveAll(ff => ff.HexAddress.Equals(hexAddress));
                                fdcItem.Data.AddRange(dBytes);
                                v.Add(fdcItem);
                                return DicFlowDefectList;
                            }
                            fdcItem.HexAddress = hexAddress;
                            fdcItem.Data.AddRange(dBytes);
                            v.Add(fdcItem);
                            return DicFlowDefectList;
                        }
                        else
                        {
                            DicFlowDefectList.Add(fdc, new List<FlowDefectCodeItem>() { new FlowDefectCodeItem(hexAddress) { Data = new List<byte>(dBytes) } });
                        }
                       // _isOK = true;
                   // }
                }
                return DicFlowDefectList;
            }
            return null;
        }
    }
}
