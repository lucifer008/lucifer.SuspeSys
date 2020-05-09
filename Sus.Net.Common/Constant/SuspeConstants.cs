using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sus.Net.Common.Constant
{
    /// <summary>
    /// 常量地址均为16进制
    /// </summary>
    public class SuspeConstants
    {
        public const string XOR = "FF";

        public const string address_MainTrack = "0037";
        public const string cmd_MainTrack_Request = "03";
        public const string cmd_MainTrack_Response = "04";
        public const string data_MainTrack_StartTag = "0010";//启动主轨回应
        public const string data_MainTrack_StopTag = "0011";//启动主轨回应
        public const string data_MainTrack_EmergencyTag = "0012";//启动主轨回应

        public const string address_Client_Manche_Online = "0035";//客户机直接上线
        public const string cmd_Client_Manchine_Online_Res = "04";
        //public const string cmd_Client_Manchine_Online_Req = "06";
        public const string cmd_Clinet_Machine_Online_Req = "03";

        public const string address_HangpieceOnline = "0035";//挂片站上线请求
        public const string cmd_HangPieceOnline_Req = "06";
        public const string cmd_HangPieceOnline_Res = "05";

        public const string address_Client_Manche_Online_Return_Display = "005A";//客户机直接上线回显给挂片站
        public const string cmd_Client_Manche_Online_Return_Display = "05";//客户机直接上线回显给挂片站CMD

        //正常衣架出站产量推送
        public const string Address_CommonSite_HangerOutSite_Data_Send = "005b";
        public const string cmd_CommonSite_HangerOutSite_Data_Send = "05";

        //返工衣架出站产量推送
        public const string Address_ReworkHangerOutSite_HangerOutSite_Data_Send = "005c";
        public const string cmd_ReworkHangerOutSite_HangerOutSite_Data_Send = "05";

        public const string address_Hanger_OutSite = "0055";//衣架出站
        public const string cmd_Hanger_OutSite_Req = "06";//衣架出站
        public const string cmd_Hanger_OutSite_Res = "05";//衣架出站

        public const string address_Site_Allocation_Hanger = "0051";//给站点分配衣架
        public const string cmd_Site_Allocation_Res = "04";
        public const string cmd_Site_Allocation_Req = "03";

        public const string address_Hanger_InSite = "0050";//衣架进站
        public const string cmd_Hanger_InSite_Req = "02";//"06";//衣架进站


        public const string address_Flow_Compare = "0054";//工序比较，及挂片站产量推送
        public const string cmd_Flow_Compare_Res = "06";
        public const string cmd_Flow_Compare_Req = "05";

        public const string address_OutSite_Yield = "005b";//出站产量推送

        public const string address_ReturnWork_Request = "0056";//返工首次请求
        public const string cmd_ReturnWork_Req = "06";
        public const string cmd_ReturnWork_Res = "05";


        public const string address_FullSite = "001B";//满站 数据位最后1字节为01代表满站
        public const string cmd_FullSite = "06";//满站 数据位最后1字节为01代表满站
        public const string data_FullSite_Tag = "01";//满站标识
        public const string cmd_FullSite_Req = "01";
        public const string cmd_FullSite_Res = "02";

        public const string address_Monitor = "0006";//监测点地址
        public const string cmd_Monitor = "02";//监测点cmd

        public const string address_Card_Login = "0060";//卡片登录
        public const string cmd_Card_Login_Req = "06";
        public const string cmd_Card_Login_Res = "05";

        public const string address_Clear_HangerCache = "0052";
        public const string cmd_Clear_HangerCache_Res = "04";
        public const string cmd_Clear_HangerCache_Req = "03";

        //疵点及工序
        public const int address_ReturnBegin = 0x0090;
        public const int address_ReturnEnd = 0x0099;
        public const string cmd_Rework = "06";

        //比较工序相关
        //工序不同时
        public const int address_Fow_Compare_ProductOut_NoEqualFlow_Begin = 0x0160;
        public const int address_Fow_Compare_ProductOut_NoEqualFlow_End = 0x017F;

        /// <summary>
        /// 返工时///lucifer/2019-5-27 10:19:52/变更地址位：由0158~015F， 改为0180~019F
        /// </summary>
        ///
        public const int address_Fow_Compare_ProductOut_ReworkFlow_Begin = 0x0180;//0x0158;
        /// <summary>
        /// /lucifer/2019-5-27 10:19:52/变更地址位：由0158~015F， 改为0180~019F
        /// </summary>
        public const int address_Fow_Compare_ProductOut_ReworkFlow_End = 0x019F;//0x015F;

        //异常及提示命令相关(pc-->硬件)
        public const string address_ExcpetionOrPromptInfo = "0130";
        public const string cmd_ExcpetionOrPromptInfo = "05";

        //异常及提示标识
        /// <summary>
        /// 未发现上线产品
        /// </summary>
        public const int tag_ExcpetionOrPromptInfo_NotFoundOnlineProducts = 0x25;
        /// <summary>
        /// 不允许返工
        /// </summary>
        public const int tag_ExcpetionOrPromptInfo_NotAllowRework = 0x24;
        /// <summary>
        /// 疵点不存在
        /// </summary>
        public const int tag_ExcpetionOrPromptInfo_DefectCodeNotFoundSystem = 0x18;
        /// <summary>
        /// 员工未打卡上班，请确认登录后，再按出站按钮。
        /// </summary>
        public const int tag_ExcpetionOrPromptInfo_EmployeeNoLoginStating = 0x01;
        /// <summary>
        /// 该卡在系统中不存在，请找系统管理员处理！
        /// </summary>
        public const int tag_ExcpetionOrPromptInfo_NoFoundCard = 0x02;
        /// <summary>
        /// 机修卡在系统中不存在，请找系统管理员处理！
        /// </summary>
        public const int tag_ExcpetionOrPromptInfo_NotFoundMechanicRepair = 0x03;

        public const int tag_ExcpetionOrPromptInfo_NotFoundStating = 0x04;
        /// <summary>
        /// //下一工序[99]无法分配工位，请检查工艺路线图是否有分配站点，停止进衣，指定颜色/尺码等情况，或联系管理员处理！
        /// </summary>
        public const int tag_ExcpetionOrPromptInfo_NotFoundNextStating = 0x81;

        /// <summary>
        /// 衣架原分配站点与本站不匹配，不允许出站，请联系管理员处理！
        /// </summary>
        public const int tag_ExcptionNonAllocationOutStating = 0x1E;

        /// <summary>
        /// 当前站点，在系统中不存在，请检查！
        /// </summary>
        public const int tag_ExcptionNonExistStating = 0x1D;

        /// <summary>
        /// 硬件提示：返工工序不存在或未生产，请重新输入！
        /// </summary>
        public const int tag_ExcpetionOrPromptInfo_ReworkFlowNoFound = 0x01;

        /// <summary>
        /// 站点满或停止工作！
        /// </summary>
        [Obsolete("此类已废弃;由tag_FullSiteOrStopWork_New代替")]
        public const int tag_FullSiteOrStopWork = 0x23;
        /// <summary>
        /// 站点满或停止工作
        /// </summary>
        public const int tag_FullSiteOrStopWork_New = 0x81;

        public const int tag_StatingNoExist = 0x1D;



        //员工卡登录登出员工信息推送
        public const int address_Send_Employee_Login_Info_Begin = 0x0140;
        public const int address_Send_Employee_Login_Info_End = 0x0147;
        public const string cmd_Send_Employee_Login_Info = "05";

        //衣车卡登录登出信息推送
        public const int address_Send_Clothes_Cards_Login_Info_Begin = 0x0148;
        public const int address_Send_Clothes_Cards_Login_Info_End = 0x014F;

        //监测点设置
        public const int address_Set_Monitor_Stating = 0x0017;
        public const string cmd_Set_Monitor_Stating = "03";
        public const string state_Set_Monitor_Open = "01";
        public const string state_Set_Monitor_Close = "00";
        /// <summary>
        /// 站点类型修改
        /// </summary>
        public const int address_StatingType_ADDH = 0x00;
        public const int address_StatingType_ADDL = 0x3F;

        /// <summary>
        /// 硬件-->上位机
        /// </summary>
        public const string cmd_StatingTyp_Res = "04";
        /// <summary>
        /// 上位机-->硬件
        /// </summary>
        public const string cmd_StatingTyp_Req = "03";

        /// <summary>
        /// 站点容量修改
        /// </summary>
        public const int address_StatingCapacity_ADDH = 0x00;
        public const int address_StatingCapacity_ADDL = 0x33;
        /// <summary>
        /// 硬件-->上位机
        /// </summary>
        public const string cmd_StatingCapacity_4 = "04";
        /// <summary>
        /// 上位机-->硬件
        /// </summary>
        public const string cmd_StatingCapacity_3 = "03";

        /// <summary>
        /// 查询站容量  上位机-->硬件
        /// </summary>
        public const string cmd_StatingCapacity_1 = "01";

        /// <summary>
        /// 查询站容量（回复）  硬件-->上位机
        /// </summary>
        public const string cmd_StatingCapacity_2 = "02";

        /// <summary>
        /// 设置站容量（回复）  上位机-->硬件
        /// </summary>
        public const string cmd_StatingCapacity_5 = "05";

        /// <summary>
        /// 设置站容量（回复）  硬件-->上位机
        /// </summary>
        public const string cmd_StatingCapacity_6 = "06";

        //暂停/接收衣架
        public const string address_Suspend_OR_Receive_Hanger = "0019";
        public const string cmd_Suspend_OR_Receive_Hanger_Request = "03";

        /// <summary>
        /// 下位机上传(暂停/接收衣架)
        /// </summary>
        public const string cmd_Lower_Machine_Suspend_OR_Receive_Hanger_Request = "06";//下位机上传

        /// <summary>
        /// 下位机上传,上位机回复(暂停/接收衣架)
        /// </summary>
        public const string cmd_Lower_Machine_Suspend_OR_Receive_Hanger_Response = "05";//下位机上传,上位机回复


        /// <summary>
        /// 上电初始化--硬件--->pc
        /// </summary>
        public const string address_Power_Supply_Init = "0208";
        /// <summary>
        /// 上电初始化
        /// </summary>
        public const string cmd_Power_Supply_Init = "02";
        /// <summary>
        /// SN版本号
        /// </summary>
        internal static readonly string address_SN_Serial_Number = "0007";
        /// <summary>
        /// SN版本号
        /// </summary>
        internal static readonly string cmd_SN_Serial_Number = "02";
        /// <summary>
        /// 主版本
        /// </summary>
        internal static readonly string address_Mainboard_Version = "0008";
        /// <summary>
        /// 主版本
        /// </summary>
        internal static readonly string cmd_Mainboard_Version = "02";

        /// <summary>
        /// 站内数
        /// </summary>
        public static readonly string address_Stating_Num = "0032";
        /// <summary>
        /// 站内数
        /// </summary>
        public static readonly string cmd_Stating_Num = "03";

        /// <summary>
        /// 上电初始化--pc--->硬件
        /// </summary>
        public const string address_Power_Supply_Init_UpperComputer = "0209";
        /// <summary>
        /// 请求
        /// </summary>
        internal const string cmd_Power_Supply_Init_UpperComputer_Req = "01";
        /// <summary>
        /// 应答
        /// </summary>
        internal const string cmd_Power_Supply_Init_UpperComputer_Res = "02";
        /// <summary>
        /// 主轨上传
        /// </summary>
        internal const string address_MainTrakNumberUpload = "0002";
        /// <summary>
        /// 主轨上传cmd
        /// </summary>
        internal const string cmd_MainTrakNumberUpload_Req = "06";

        /// <summary>
        ///  连接后主轨查询
        /// </summary>
        public const string address_Connected_Query_MaintrackNumber = "001C";
        /// <summary>
        /// 连接后主轨查询cmd请求
        /// </summary>
        public const string cmd_Connected_Query_MaintrackNumber_Req = "01";
        /// <summary>
        /// 连接后主轨查询cmd响应
        /// </summary>
        public const string cmd_Connected_Query_MaintrackNumber_Res = "02";


        /// <summary>
        /// 重新连接后主轨查询cmd请求
        /// </summary>
        public const string cmd_Reconnected_Query_MaintrackNumber_Req = "01";
        /// <summary>
        /// 重新连接后主轨查询cmd响应
        /// </summary>
        public const string cmd_Reconnected_Query_MaintrackNumber_Res = "02";
        /// <summary>
        ///  重新连接后主轨查询主轨查询
        /// </summary>
        public const string address_Reconnected_Query_MaintrackNumber = "001D";

        #region 呼叫缺料相关
        /// <summary>
        /// PC--->硬件缺料发送
        /// 发送缺料信息到接收站
        /// </summary>

        public const string address_lack_Material_Send_to_Stating = "021A";

        /// <summary>
        /// PC--->硬件缺料发送
        /// 发送缺料信息到接收站
        /// </summary>
        public const string cmd_lack_Material_Send_to_Stating = "03";

        /// <summary>
        /// 硬件--->pc 缺料呼叫
        /// </summary>
        public const string address_Lack_Materials_Call = "0220";
        /// <summary>
        /// 硬件--->pc 缺料呼叫cmd
        /// </summary>
        public const string cmd_Lack_Materials_Call = "06";

        /// <summary>
        /// PC ---> 硬件  站点应答
        /// </summary>
        public const string cmd_Lack_Materials_Reponse = "04";

        /// <summary>
        /// 硬件--->pc 缺料呼叫上传
        /// </summary>
        public const string address_Lack_Materials_Call_Upload = "0010";
        /// <summary>
        /// 硬件--->pc 缺料呼叫上传cmd
        /// </summary>
        public const string cmd_Lack_Materials_Call_Upload = "06";

        /// <summary>
        /// pc--->硬件 开始地址   缺料明细数据
        /// </summary>
        public const int address_lack_meterials_response_begin = 6800;
        /// <summary>
        /// pc--->硬件 结束地址   缺料明细数据
        /// </summary>
        public const int address_lack_meterials_response_end = 6807;

        /// <summary>
        /// pc--->硬件 响应 cmd
        /// </summary>
        public const string cmd_meterials_Res = "05";

        /// <summary>
        /// 硬件--->pc 缺料终止
        /// </summary>
        public const string address_Lack_Materials_Call_Stop = "0223";

        /// <summary>
        /// 硬件--->pc 缺料终止
        /// </summary>
        public const string cmd_Lack_Materials_Call_Stop = "06";

        /// <summary>
        /// pc--->硬件 取消呼叫：
        /// </summary>
        public const string address_Lack_Materials_Call_Stop_To_Stating = "0223";

        /// <summary>
        /// pc--->硬件 取消呼叫：
        /// </summary>
        public const string cmd_Lack_Materials_Call_Stop_To_Stating = "05";
        #endregion

        #region F2指定相关
        /// <summary>
        /// F2业务衣架号上传
        /// </summary>
        public const string address_F2_Upload_HangerNO_Assign = "005D";

        /// <summary>
        /// F2指定业务动作指令
        /// 跨主轨组合命令：
        ///06 005D  00+5字节ID号码
        ///06 0058  4字节00+主轨号+ 站点
        ///不跨主轨
        ///06 005D  00+5字节ID号码
        ///06 0058  5字节00+站点号
        /// </summary>
        public const string address_F2_Assign_Action = "0058";
        /// <summary>
        /// F2 上传cmd
        /// </summary>
        public const string cmd_F2_Assign_Req = "06";

        /// <summary>
        /// F2 响应 cmd
        /// </summary>
        public const string cmd_F2_Assign_Res = "05";

        #endregion

        #region 呼叫机修
        /// <summary>
        /// 发起呼叫机修
        /// </summary>
        public const string address_call_machine_repair_start = "0221";
        /// <summary>
        /// 呼叫机修cmd
        /// </summary>
        public const string cmd_call_machine_repair_req = "06";
        //   public const string cmd_call_machine_repair_res = "05";

        #endregion

        #region 呼叫管理
        /// <summary>
        /// 发起呼叫管理
        /// </summary>
        public const string address_call_management_start = "0222";
        /// <summary>
        /// 呼叫管理cmd
        /// </summary>
        public const string cmd_call_management_req = "06";
        //   public const string cmd_call_machine_repair_res = "05";

        #endregion
        /// <summary>
        /// 中止呼叫
        /// </summary>
        public const string address_call_stop = "0223";

        #region 衣车管理

        //故障报修衣车类别上传请求
        public const string address_Fault_Repair_Upload_Start = "021B";
        public const string cmd_Fault_Repair_Upload_Start = "06";

        //故障报修故障代码请求
        public const string address_Fault_Repair_Req = "0224";
        public const string cmd_Fault_Repair = "06";
        //故障报修衣车类别和故障代码请求
        public const string address_Fault_Repair_ClothingType_AND_Fault_Code_Req = "0225";
        public const string cmd_Fault_Repair_ClothingType_AND_Fault_Code = "06";

        //中止故障报修
        public const string address_Fault_Repair_Stop = "021C";
        public const string cmd_Fault_Repair_Stop = "06";
        public const string cmd_Fault_Repair_Stop_Res = "05";

        //机修开始维修
        public const string address_Fault_Repair_Start_Repair = "031D";
        public const string cmd_Fault_Repair_Start_Repair = "06";

        //机修完成维修
        public const string address_Fault_Repair_Sucess_Repair = "031E";
        public const string cmd_Fault_Repair_Sucess_Repair = "06";

        public const int address_fault_code_send_start = 0x6A00;
        public const int address_fault_code_send_end = 0x6A4F;

        /// <summary>
        /// 故障代码一级菜单开始地址(衣车类别)
        /// </summary>
        public const int address_fault_code_first_menu_start = 0x6A00;
        /// <summary>
        /// 故障代码一级菜单末尾地址(衣车类别)
        /// </summary>
        public const int address_fault_code_first_menu_end = 0x6A4F;

        /// <summary>
        /// 故障代码二级菜单开始地址(衣车类别)
        /// </summary>
        public const int address_fault_code_second_menu_start = 0x6B00;
        /// <summary>
        /// 故障代码二级菜单末尾地址(衣车类别)
        /// </summary>
        public const int address_fault_code_second_menu_end = 0x6B7F;

        /// <summary>
        /// 机修完成推送
        /// </summary>
        public const string address_fault_repair_start = "021D";
        /// <summary>
        /// 机修完成推送cmd
        /// </summary>
        public const string cmd_fault_repair_start = "05";


        /// <summary>
        /// 机修完成推送
        /// </summary>
        public const string address_fault_repair_success = " 021E";
        /// <summary>
        /// 机修完成推送cmd
        /// </summary>
        public const string cmd_fault_repair_success = "05";

        
        #endregion
    }

    /// <summary>
    /// 0130 异常标签
    /// </summary>
    public enum _0130ExcpetionTag
    {
        /// <summary>
        /// 允许员工从终端登录
        /// </summary>
        CanLoginFromStation = 0x01,

        /// <summary>
        /// 挂片站出衣架达到计划数后停止出衣
        /// </summary>
        StartingStopOutWhenOverPlan = 0x1A,
    }
}
