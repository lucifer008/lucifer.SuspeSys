using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
    public class PermissionConstant
    {
        #region susToolBar
        /// <summary>
        /// 全屏
        /// </summary>
        public const string btnMax = "btnMax";
        /// <summary>
        /// btnRefresh
        /// </summary>
        public const string btnRefresh = "btnRefresh";
        /// <summary>
        /// 保存
        /// </summary>
        public const string btnSave = "btnSave";
        /// <summary>
        /// 取消
        /// </summary>
        public const string btnCancel = "btnCancel";
        /// <summary>
        /// 修改
        /// </summary>
        public const string btnModify = "btnModify";
        /// <summary>
        /// 保存&新增
        /// </summary>
        public const string btnSaveAndAdd = "btnSaveAndAdd";
        /// <summary>
        /// 保存&关闭
        /// </summary>
        public const string btnSaveAndClose = "btnSaveAndClose";
        /// <summary>
        /// 新增
        /// </summary>
        public const string btnAdd = "btnAdd";
        /// <summary>
        /// 退出
        /// </summary>
        public const string btnClose = "btnClose";
        /// <summary>
        /// 删除
        /// </summary>
        public const string btnDelete = "btnDelete";
        /// <summary>
        /// 导出
        /// </summary>
        public const string btnExport = "btnExport";
        #endregion

        #region 制单信息
        /// <summary>
        /// 生产制单
        /// </summary>
        public const string Billing_ProcessOrderIndex = "Billing_ProcessOrderIndex";

        /// <summary>
        /// 制单工序
        /// </summary>
        public const string Billing_ProcessFlowIndex = "Billing_ProcessFlowIndex";

        /// <summary>
        /// 工艺路线图
        /// </summary>
        public const string Billing_ProcessFlowChartIndex = "Billing_ProcessFlowChartIndex";

        /// <summary>
        /// 产线实时信息
        /// </summary>
        public const string Billing_ProductRealtimeInfo = "Billing_ProductRealtimeInfo";

        /// <summary>
        /// 在制品信息
        /// </summary>
        public const string Billing_ProductsingInfo = "Billing_ProductsingInfo";

        /// <summary>
        /// 衣架信息
        /// </summary>
        public const string Billing_CoatHanger = "Billing_CoatHanger";

        #endregion

        #region 订单信息
        /// <summary>
        /// 客户信息
        /// </summary>
        public const string OrderInfo_CustomerInfo = "OrderInfo_CustomerInfo";
        /// <summary>
        /// 
        /// </summary>
        public const string OrderInfo_CustomerOrderInfo = "OrderInfo_CustomerOrderInfo";
        #endregion

        #region 产品基本信息
        /// <summary>
        /// 产品部位
        /// </summary>
        public const string ProductBaseData_ProductPart = "ProductBaseData_ProductPart";
        /// <summary>
        /// 基本尺码表
        /// </summary>
        public const string ProductBaseData_BasicSizeTable = "ProductBaseData_BasicSizeTable";
        /// <summary>
        /// 基本颜色表
        /// </summary>
        public const string ProductBaseData_BasicColorTable = "ProductBaseData_BasicColorTable";
        /// <summary>
        /// 基本颜色表
        /// </summary>
        public const string ProductBaseData_Style = "ProductBaseData_Style";
        #endregion

        #region 工艺基础数据
        /// <summary>
        /// 基本工序段
        /// </summary>
        public const string ProcessBaseData_BasicProcessSection = "ProcessBaseData_BasicProcessSection";
        /// <summary>
        /// 基本工序库
        /// </summary>
        public const string ProcessBaseData_BasicProcessLirbary = "ProcessBaseData_BasicProcessLirbary";
        /// <summary>
        /// 款式工序库
        /// </summary>
        public const string ProcessBaseData_StyleProcessLirbary = "ProcessBaseData_StyleProcessLirbary";
        /// <summary>
        /// 疵点代码
        /// </summary>
        public const string ProcessBaseData_DefectCode = "ProcessBaseData_DefectCode";
        /// <summary>
        /// 缺料代码
        /// </summary>
        public const string ProcessBaseData_LackOfMaterialCode = "ProcessBaseData_LackOfMaterialCode";
        #endregion

        #region 生产线
        /// <summary>
        /// 控制端配置
        /// </summary>
        public const string ProductionLine_ControlSet = "ProductionLine_ControlSet";
        /// <summary>
        /// 流水线管理
        /// </summary>
        public const string ProductionLine_PipelineMsg = "ProductionLine_PipelineMsg";
        /// <summary>
        /// 桥接配置
        /// </summary>
        public const string ProductionLine_BridgingSet = "ProductionLine_BridgingSet";
        /// <summary>
        /// 客户机信息
        /// </summary>
        public const string ProductionLine_ClientInfo = "ProductionLine_ClientInfo";
        /// <summary>
        /// 系统参数
        /// </summary>
        public const string ProductionLine_SystemMsg = "ProductionLine_SystemMsg";
        /// <summary>
        /// Tcp测试
        /// </summary>
        public const string ProductionLine_TcpTest = "ProductionLine_TcpTest";

        /// <summary>
        /// 生产线设置
        /// </summary>
        public const string System_Msg_ProductLine = "System_Msg_ProductLine";
        #endregion

        #region 衣车类别表
        /// <summary>
        /// 衣车类别表
        /// </summary>
        public const string ClothingCarManagement_EwingMachineType = "ClothingCarManagement_EwingMachineType";
        /// <summary>
        /// 故障代码表
        /// </summary>
        public const string ClothingCarManagement_FalutCodeTable = "ClothingCarManagement_FalutCodeTable";
        /// <summary>
        /// 衣车资料
        /// </summary>
        public const string ClothingCarManagement_SewingMachineData = "ClothingCarManagement_SewingMachineData";
        /// <summary>
        /// 机修人员表
        /// </summary>
        public const string ClothingCarManagement_MechanicEmployee = "ClothingCarManagement_MechanicEmployee";
        #endregion

        #region 人事管理
        /// <summary>
        /// 生产组别
        /// </summary>
        public const string PersonnelManagement_ProductGroup = "PersonnelManagement_ProductGroup";
        /// <summary>
        /// 部门信息
        /// </summary>
        public const string PersonnelManagement_DepartmentInfo = "PersonnelManagement_DepartmentInfo";
        /// <summary>
        /// 工种信息
        /// </summary>
        public const string PersonnelManagement_ProfessionInfo = "PersonnelManagement_ProfessionInfo";
        /// <summary>
        /// 职务信息
        /// </summary>
        public const string PersonnelManagement_PositionInfo = "PersonnelManagement_PositionInfo";
        /// <summary>
        /// 员工资料
        /// </summary>
        public const string PersonnelManagement_EmployeeInfo = "PersonnelManagement_EmployeeInfo";
        /// <summary>
        /// 管理卡信息
        /// </summary>
        public const string PersonnelManagement_MsgCardInfo = "PersonnelManagement_MsgCardInfo";
        #endregion

        #region 权限管理
        /// <summary>
        /// 菜单管理
        /// </summary>
        public const string AuthorityManagement_ModuleMsg = "AuthorityManagement_ModuleMsg";

        /// <summary>
        /// 角色管理
        /// </summary>
        public const string AuthorityManagement_RoleMsg = "AuthorityManagement_RoleMsg";

        /// <summary>
        /// 角色管理
        /// </summary>
        public const string AuthorityManagement_UserMsg = "AuthorityManagement_UserMsg";
        /// <summary>
        /// 用户操作日志
        /// </summary>
        public const string AuthorityManagement_UserOperatorLog = "AuthorityManagement_UserOperatorLog";
        #endregion

        #region 考勤管理
        /// <summary>
        /// 假日信息
        /// </summary>
        public const string AttendanceManagement_HolidayInfo = "AttendanceManagement_HolidayInfo";
        /// <summary>
        /// 班次信息
        /// </summary>
        public const string AttendanceManagement_ClasssesInfo = "AttendanceManagement_ClasssesInfo";

        /// <summary>
        /// 员工排班表
        /// </summary>
        public const string AttendanceManagement_EmployeeScheduling = "AttendanceManagement_EmployeeScheduling";
        #endregion

        #region 裁床管理

        /// <summary>
        /// 货卡信息
        /// </summary>
        public const string CuttingRoomManage_GoodCardInfo = "CuttingRoomManage_GoodCardInfo";
        public static readonly string Prodcut_Msg= "Prodcut_Msg";
        public static readonly string report_Info= "report_Info";
        public static readonly string yieldCollect= "yieldCollect";
        public static readonly string employeeYieldReport;
        public static readonly string workingHoursAnalysisReport;
        public static readonly string productItemReport;
        public static readonly string reworkDetailReport;
        public static readonly string reworkCollAndDefectAnalysisReport;
        public static readonly string groupCompetitionReport;
        public static readonly string flowBalanceTableReport;
        public static readonly string defectAnalysisReport;
        public static readonly string barBtn_ProcessBaseData;
        public static readonly string barBtn_AuthorityManagement;
        public static readonly string barBtn_PersonnelManagement;
        public static readonly string factory;
        public static readonly string workshop;
        public static readonly string sewingMachineBasic;
        public static readonly string productLine;
        public static readonly string system;
        public static readonly string hangUpLine;
        public static readonly string customerMancheParamter;
        public static readonly string userParamter;
        public static readonly string goodCardInfoIndex;
        public static readonly string barBtn_AttendanceManagement;
        public static readonly string gpOrderInfo;
        public static readonly string basicSizeTable;
        public static readonly string basicColorTable;
        public static readonly string MultLanguageMsg;
        public static readonly string cboxALL;
        public static readonly string cboxLastMonth;
        public static readonly string cboxLastWeek;
        public static readonly string cboxCurrentMonth;
        public static readonly string cboxCurrentWeek;
        public static readonly string cboxLastThreeDay;
        public static readonly string cboxYesterday;
        public static readonly string cboxToday;
        #endregion
    }
}
