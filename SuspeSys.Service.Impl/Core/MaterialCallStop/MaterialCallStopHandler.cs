using Sus.Net.Common.SusBusMessage;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.MaterialCallStop
{
    public class MaterialCallStopHandler
    {
        public static readonly MaterialCallStopHandler Instance = new MaterialCallStopHandler();
        private MaterialCallStopHandler() { }

        public void Process(MaterialCallStopRequestMessaage request)
        {
            var message = request;

            var XID = HexHelper.HexToTen(message.XID);
            var ID = HexHelper.HexToTen(message.ID);

            var materialCode = request.MaterialCode;
            var LackMaterials = SusCacheProductService.Instance.LackMaterialsTable().FirstOrDefault(o => o.LackMaterialsCode.Trim().Equals(materialCode.ToString()));
            if (LackMaterials == null)
            {
                CANTcpServer.server.SendExceptionNotMaterial(XID, ID);
                return;
            }

            var materialName = LackMaterials.LackMaterialsName.Trim();


            if (!this.Valid(XID, ID))
                return;
            else
            {
                FaultInfoService.Instance.MaterialCallStop(XID, ID, materialCode.ToString(), materialName);
            }

        }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="tcpClient"></param>
        /// <returns></returns>
        private bool Valid(int mainTrackNumber, int statingNo, TcpClient tcpClient = null)
        {
            bool valid = true;

            var CallNoticeStNo = SystemParameterService.Instance.GetHangUpLineOtherValue(Domain.SusEnum.SystemParameterHangUpOther.CallNoticeStNo, mainTrackNumber.ToString(), statingNo.ToString());

            if (string.IsNullOrEmpty(CallNoticeStNo))
            {
                CANTcpServer.server.MaterialCallUploadNotStating(mainTrackNumber, statingNo, tcpClient);
                valid = false;
            }
            return valid;
        }


    }
}
