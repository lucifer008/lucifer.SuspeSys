﻿using Sus.Net.Common.Constant;
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
    /// 故障报修类别及故障代码请求
    /// </summary>
    public class FaultRepairClothingTypeAndFaultCodeRequestMessage : MessageBody
    {
        public FaultRepairClothingTypeAndFaultCodeRequestMessage(byte[] resBytes) : base(resBytes)
        {
        }
        public static FaultRepairClothingTypeAndFaultCodeRequestMessage isEqual(byte[] resBytes)
        {
            var rCmd = HexHelper.BytesToHexString(new byte[] { resBytes[2] });
            if (!SuspeConstants.cmd_Fault_Repair.Equals(rCmd)) return null;

            var raddress = HexHelper.BytesToHexString(new byte[] { resBytes[4], resBytes[5] });
            if (!SuspeConstants.address_Fault_Repair_ClothingType_AND_Fault_Code_Req.Equals(raddress)) return null;
            return new FaultRepairClothingTypeAndFaultCodeRequestMessage(resBytes)
            {
                MainTrackNuber = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[0])),
                StatingNo = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[1])),
                ClothingVehicleTypeCode = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[10])),
                FaultCode = HexHelper.HexToTen(HexHelper.BytesToHexString(resBytes[11])),
            };
        }
        /// <summary>
        /// 发起主轨
        /// </summary>
        public int MainTrackNuber { set; get; }
        /// <summary>
        /// 发起站点
        /// </summary>
        public int StatingNo { set; get; }
        /// <summary>
        /// 衣车类别代码/衣车类别序号
        /// </summary>
        public int ClothingVehicleTypeCode { set; get; }
        /// <summary>
        /// 故障序号/故障代码
        /// </summary>
        public int FaultCode { set; get; }
    }
}