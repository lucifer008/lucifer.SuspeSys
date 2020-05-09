using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Sus.Net.Common.SusBusMessage;
using SuspeSys.Dao;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Service.Impl.Core.Bridge;
using SuspeSys.Utils;
using SuspeSys.Service.Impl.Core.Check;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Service.Impl.SusTcp;
using SuspeSys.Domain.SusEnum;

namespace SuspeSys.Service.Impl.Core.FaultRepair
{
    public class FaultRepairAdapter : SusLog
    {
        private FaultRepairAdapter() { }
        public static readonly FaultRepairAdapter Instance = new FaultRepairAdapter();
        //故障报修-->故障代码下发请求
        internal void DoService(FaultRepairUploadStartRequestMessage faultRepairUploadStartRequestMessage, TcpClient tcpClient)
        {
            var groupNo = BridgeService.Instance.GetGroupNo(faultRepairUploadStartRequestMessage.MainTrackNuber, faultRepairUploadStartRequestMessage.StatingNo);
            var sewingMachineCardList = DapperHelp.Query<DaoModel.ClothingVehicle>($"select Id from ClothingVehicle where GroupNo=@GroupNo AND StatingNo=@StatingNo ", new { GroupNo = groupNo, StatingNo = faultRepairUploadStartRequestMessage.StatingNo });
            if (0 == sewingMachineCardList.Count())
            {
                LowerPlaceInstr.Instance.SendExcpetionOrPromptInfo(faultRepairUploadStartRequestMessage.MainTrackNuber, faultRepairUploadStartRequestMessage.StatingNo, 33, null, tcpClient);
                tcpLogError.Error(new ApplicationException($"主轨:{faultRepairUploadStartRequestMessage.MainTrackNuber} 站点:{faultRepairUploadStartRequestMessage.StatingNo} 衣车未登录!"));
                return;
            }
            bool isLogin = CheckService.Instance.CheckStatingIsLogin(faultRepairUploadStartRequestMessage.MainTrackNuber, faultRepairUploadStartRequestMessage.StatingNo);
            if (!isLogin)
            {
                LowerPlaceInstr.Instance.SendExcpetionOrPromptInfo(faultRepairUploadStartRequestMessage.MainTrackNuber, faultRepairUploadStartRequestMessage.StatingNo, 34, null, tcpClient);
                tcpLogError.Error(new ApplicationException($"主轨:{faultRepairUploadStartRequestMessage.MainTrackNuber} 站点:{faultRepairUploadStartRequestMessage.StatingNo} 员工未登录!"));
                return;

            }
            var clothingVehicleTypeList = DapperHelp.Query<DaoModel.ClothingVehicleType>($"select * from ClothingVehicleType");
            if (0 == clothingVehicleTypeList.Count())
            {
                LowerPlaceInstr.Instance.SendExcpetionOrPromptInfo(faultRepairUploadStartRequestMessage.MainTrackNuber, faultRepairUploadStartRequestMessage.StatingNo, 35, null, tcpClient);
                tcpLogError.Error(new ApplicationException($"主轨:{faultRepairUploadStartRequestMessage.MainTrackNuber} 站点:{faultRepairUploadStartRequestMessage.StatingNo} 无衣车类别列表!"));
                return;
            }
            if (10 < clothingVehicleTypeList.Count())
            {
                tcpLogError.Error(new ApplicationException($"主轨:{faultRepairUploadStartRequestMessage.MainTrackNuber} 站点:{faultRepairUploadStartRequestMessage.StatingNo} 故障代码超出最大限制10个!"));
                return;
            }
            LowerPlaceInstr.Instance.SendClothingVehicleTypeMessageList(faultRepairUploadStartRequestMessage.MainTrackNuber, faultRepairUploadStartRequestMessage.StatingNo, clothingVehicleTypeList.ToList());
           // var currentStatingInfo = sewingMachineCardList.First();
           // FaultRepairService.Instance.FaultRepairUploadStartHandler(faultRepairUploadStartRequestMessage.MainTrackNuber, faultRepairUploadStartRequestMessage.StatingNo);
        }
        //故障报修请求
        internal void DoService(FaultRepairReqtRequestMessage faultRepairReqtRequestMessage, TcpClient tcpClient)
        {
            var clothingVehicleTypeCode = faultRepairReqtRequestMessage.ClothingVehicleTypeCode;
            var faultCodeList = DapperHelp.Query<DaoModel.FaultCodeTable>(@"select * from FaultCodeTable fct
inner join ClothingVehicleType cvt on fct.CLOTHINGVEHICLETYPE_Id = cvt.Id
where cvt.Code = @SerialNumber", new { SerialNumber = clothingVehicleTypeCode });
            if (0 == faultCodeList.Count())
            {
                LowerPlaceInstr.Instance.SendExcpetionOrPromptInfo(faultRepairReqtRequestMessage.MainTrackNuber, faultRepairReqtRequestMessage.StatingNo, 36, null, tcpClient);
                tcpLogError.Error(new ApplicationException($"主轨:{faultRepairReqtRequestMessage.MainTrackNuber} 站点:{faultRepairReqtRequestMessage.StatingNo} 无故障代码序号-->{clothingVehicleTypeCode}!"));
                return;
            }
            LowerPlaceInstr.Instance.SendFaultCodeMessageList(faultRepairReqtRequestMessage.MainTrackNuber, faultRepairReqtRequestMessage.StatingNo, clothingVehicleTypeCode, faultCodeList.ToList());
           
        }
        //中止故障报修
        internal void DoService(FaultRepairStopRequestMessage faultRepairStopRequestMessage, TcpClient tcpClient)
        {
            var isLogin=CheckService.Instance.CheckEmployeeIsLoginStating(faultRepairStopRequestMessage.StatingNo, faultRepairStopRequestMessage.MainTrackNuber, faultRepairStopRequestMessage.CardNo+"");
            if (!isLogin)
            {
                LowerPlaceInstr.Instance.SendExcpetionOrPromptInfo(faultRepairStopRequestMessage.MainTrackNuber, faultRepairStopRequestMessage.StatingNo, 37, null, tcpClient);
                tcpLogError.Error(new ApplicationException($"主轨:{faultRepairStopRequestMessage.MainTrackNuber} 站点:{faultRepairStopRequestMessage.StatingNo} 卡号未登录当前站点!"));
                return;
            }
            LowerPlaceInstr.Instance.SendStopFaultRepair(faultRepairStopRequestMessage.MainTrackNuber + "", faultRepairStopRequestMessage.StatingNo + "", null, tcpClient);
            

            //2.1【表Stating字段(FaultInfo)】--->清空
            StatingServiceImpl.Instance.UpdateFaultInfo(faultRepairStopRequestMessage.MainTrackNuber, faultRepairStopRequestMessage.StatingNo, string.Empty);

            //2.2 【KanbanInfo】只改状态
            KanbanInfoServiceImpl.Instance.Cancel(faultRepairStopRequestMessage.MainTrackNuber, faultRepairStopRequestMessage.StatingNo + "", "");
        }
        //故障报修类别及故障代码请求
        internal void DoService(FaultRepairClothingTypeAndFaultCodeRequestMessage faultRepairClothingTypeAndFaultCodeRequestMessage, TcpClient tcpClient)
        {
            var clothingVehicleTypeCode = faultRepairClothingTypeAndFaultCodeRequestMessage.ClothingVehicleTypeCode;
            var faultSe = faultRepairClothingTypeAndFaultCodeRequestMessage.FaultCode;
            var faultCodeList = DapperHelp.Query<DaoModel.FaultCodeTable>(@"
select * from FaultCodeTable fct
inner join ClothingVehicleType cvt on fct.CLOTHINGVEHICLETYPE_Id = cvt.Id
where cvt.Code=@Code and fct.SerialNumber= @SerialNumber", new { SerialNumber = faultSe,Code= clothingVehicleTypeCode });
            if (0 == faultCodeList.Count())
            {
                LowerPlaceInstr.Instance.SendExcpetionOrPromptInfo(faultRepairClothingTypeAndFaultCodeRequestMessage.MainTrackNuber, faultRepairClothingTypeAndFaultCodeRequestMessage.StatingNo, 36, null, tcpClient);
                tcpLogError.Error(new ApplicationException($"主轨:{faultRepairClothingTypeAndFaultCodeRequestMessage.MainTrackNuber} 站点:{faultRepairClothingTypeAndFaultCodeRequestMessage.StatingNo} 无故障代码序列-->【{clothingVehicleTypeCode}-{faultSe}】!"));
                return;
            }
            var cfc = faultCodeList.ToList()[0];

            //2.1【表Stating字段(FaultInfo)】--->清空
            StatingServiceImpl.Instance.UpdateFaultInfo(faultRepairClothingTypeAndFaultCodeRequestMessage.MainTrackNuber, faultRepairClothingTypeAndFaultCodeRequestMessage.StatingNo, cfc.FaultName?.Trim());
            //2、故障写入站点，同步到产线实时信息（表Stating字段(FaultInfo)）及看板写入
            var kanbanInfo = this.BuildKanbanInfo(faultRepairClothingTypeAndFaultCodeRequestMessage.MainTrackNuber, faultRepairClothingTypeAndFaultCodeRequestMessage.StatingNo,
                cfc.FaultCode?.Trim(),cfc.FaultName?.Trim());
            KanbanInfoServiceImpl.Instance.Add(kanbanInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DaoModel.KanbanInfo BuildKanbanInfo(int mainTrackNumber, int statingNo,string faultCode,string faultName)
        {
            var statingDto = KanbanInfoServiceImpl.Instance.GetStating(mainTrackNumber, statingNo.ToString());

            return new DaoModel.KanbanInfo()
            {
                CallTime = DateTime.Now,
                Fault = faultName,
                FaultCode = faultCode,
                LogId = Guid.NewGuid().ToString().Replace("-", ""),
                InsertDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                GroupNo = statingDto.GroupNO,
                Mechanic = string.Empty,
                StationNo = statingNo.ToString(),
                Status = (int)KanbanInfoStatus.Pending,
                WorkShop = statingDto.WorkshopCode,
            };
        }
        //机修开始
        internal void DoService(FaultRepairStartRequestMessage faultRepairStartRequestMessage, TcpClient tcpClient)
        {
            var mechanicEmployeesList = DapperHelp.Query<DaoModel.MechanicEmployees>($"select * from MechanicEmployees where CardNo=@CardNo and Status=1",new { CardNo = faultRepairStartRequestMessage.CardNo});
            if (0 == mechanicEmployeesList.Count())
            {
                LowerPlaceInstr.Instance.SendExcpetionOrPromptInfo(faultRepairStartRequestMessage.MainTrackNuber, faultRepairStartRequestMessage.StatingNo, 38, null, tcpClient);
                tcpLogError.Error(new ApplicationException($"主轨:{faultRepairStartRequestMessage.MainTrackNuber} 站点:{faultRepairStartRequestMessage.StatingNo} 机修卡不存在或未启用!卡号-->{faultRepairStartRequestMessage.CardNo}"));
                return;
            }
            var currentMeEmpInfo = mechanicEmployeesList.ToList()[0];
            LowerPlaceInstr.Instance.SendStartFaultRepair(faultRepairStartRequestMessage.MainTrackNuber + "", faultRepairStartRequestMessage.StatingNo + "", null, tcpClient);
            KanbanInfoServiceImpl.Instance.CorrectFaultRepairByRepairEmoyeeRepairing(faultRepairStartRequestMessage.MainTrackNuber, faultRepairStartRequestMessage.StatingNo, currentMeEmpInfo.RealName?.Trim());
        }
        //机修完成
        internal void DoService(FaultRepairSucessRequestMessage faultRepairSucessRequestMessage, TcpClient tcpClient)
        {
            var mechanicEmployeesList = DapperHelp.Query<DaoModel.MechanicEmployees>($"select * from MechanicEmployees where CardNo=@CardNo and Status=1", new { CardNo = faultRepairSucessRequestMessage.CardNo});
            if (0 == mechanicEmployeesList.Count())
            {
                LowerPlaceInstr.Instance.SendExcpetionOrPromptInfo(faultRepairSucessRequestMessage.MainTrackNuber, faultRepairSucessRequestMessage.StatingNo, 39, null, tcpClient);
                tcpLogError.Error(new ApplicationException($"主轨:{faultRepairSucessRequestMessage.MainTrackNuber} 站点:{faultRepairSucessRequestMessage.StatingNo} 机修卡未登录!卡号-->{faultRepairSucessRequestMessage.CardNo}"));
                return;
            }
            LowerPlaceInstr.Instance.SendSuccessFaultRepair(faultRepairSucessRequestMessage.MainTrackNuber + "", faultRepairSucessRequestMessage.StatingNo + "", null, tcpClient);


            //2.1【表Stating字段(FaultInfo)】--->清空
            StatingServiceImpl.Instance.UpdateFaultInfo(faultRepairSucessRequestMessage.MainTrackNuber, faultRepairSucessRequestMessage.StatingNo, string.Empty);

            KanbanInfoServiceImpl.Instance.CorrectFaultRepairByRepairEmoyeeRepaired(faultRepairSucessRequestMessage.MainTrackNuber, faultRepairSucessRequestMessage.StatingNo);

        }
    }
}
