using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public enum SystemParameter
    {
        /// <summary>
        /// 用户参数
        /// </summary>
        UserParameter,

        /// <summary>
        /// 用户Logo
        /// </summary>
        UserLogo,

        /// <summary>
        /// 用户信息
        /// </summary>
        UserInfo,



        /// <summary>
        /// 吊挂线
        /// </summary>
        DropperParameter

    }

    #region 吊挂线 其他
    public enum SystemParameterHangUpOther
    {
        /// <summary>
        /// 站点终端允许修改系统参数权限
        /// </summary>
        [SystemPrarmeter("checkbox", "站点终端允许修改系统参数权限")]  //1
        CanEditSystemPara,
        //[SystemPrarmeter("checkbox", "站点面板修改站内衣数权限，选择时可直接修改，未选择需刷管理卡")]
        //CanEditRackQtyFromTerm,
        /// <summary>
        /// 自动停止期间，禁止手工运行吊挂线
        /// </summary>
        [SystemPrarmeter("checkbox", "自动停止期间，禁止手工运行吊挂线")]//2
        CanNotRunWhenAutoStop,
        /// <summary>
        /// 站位须登录衣车后做工
        /// </summary>
        [SystemPrarmeter("checkbox", "站位须登录衣车后做工")]//3
        MachMustLogin,
        /// <summary>
        /// 员工登出站点时，同时登出衣车
        /// </summary>
        [SystemPrarmeter("checkbox", "员工登出站点时，同时登出衣车")]//4
        MachLogoutByEmpLogout,
        /// <summary>
        /// 允许衣车快速报修
        /// </summary>
        [SystemPrarmeter("checkbox", "允许衣车快速报修")]//5
        CanQuickReportFaultMach,
        /// <summary>
        /// 允许登出已故障报修的衣车
        /// </summary>
        [SystemPrarmeter("checkbox", "允许登出已故障报修的衣车")]//6
        CanLogoutFaultMach,
        /// <summary>
        /// 终端有衣车登录时，允许刷其他衣车卡替换
        /// </summary>
        [SystemPrarmeter("checkbox", "终端有衣车登录时，允许刷其他衣车卡替换")]//7
        CanLogoutOtherMach,
        /// <summary>
        /// 末道工序启动装箱功能
        /// </summary>
        [SystemPrarmeter("checkbox", "末道工序启动装箱功能")]//8
        EnablePacking,
        /// <summary>
        /// 呼叫显示到指定站点屏幕
        /// </summary>
        [SystemPrarmeter("number", "呼叫显示到指定站点屏幕")]//9
        CallNoticeStNo,
    }
    #endregion

    #region 吊挂线 生产线

    public enum SystemParameterHangUpProductsLine
    {
        [SystemPrarmeter("checkbox", "允许员工从终端登录")]  //1
        CanLoginFromStation,
        [SystemPrarmeter("checkbox", "员工从终端登录时要求验证密码")]//2
        VertifyWhenLoginStation,
        [SystemPrarmeter("checkbox", "检测站点满站时，包括线上待进衣架")]//3
        RackQtyContainOnline,
        [SystemPrarmeter("checkbox", "站点加工连续的非合并工序，一次性加工完成")]//4
        SeqListCombine,
        [SystemPrarmeter("checkbox", "产品出站前须分配下一站点")]//5
        AssignAimWhenOut,
        [SystemPrarmeter("checkbox", "下道工序不能进站时，暂停出站")]//6
        StopOutWhenNextFull,
        [SystemPrarmeter("checkbox", "员工做前后不同工序时，产品优先选择同一人生产")]//7
        AssSameEmpUseSameEmp,
        [SystemPrarmeter("checkbox", "允许车缝站返工")]//8
        GenStationCanQC,
        [SystemPrarmeter("checkbox", "返工送至原工序站点，未选送至原工序员工")]//9
        FailReturnToStation,
        [SystemPrarmeter("checkbox", "校验返工指令中的疵点代码")]//10
        VertifyFailCode,
        [SystemPrarmeter("checkbox", "终端显示工序信息方式")]//11
        SeqInfoType,
        [SystemPrarmeter("number", "同工序站点站内衣数低于此值时依次分配")]//12
        RackLow,
        [SystemPrarmeter("number", "线上衣架超时时间(分)")]//13
        OnlineRackOverMins,
        [SystemPrarmeter("number", "主轨推杆负载上限（%）")]//14
        HangerLoadMax,
        [SystemPrarmeter("number", "每个衣架最大挂衣数（即产品单位允许输入的最大值）")]//15
        HangerUnitMax,
    }

    public enum SeqInfoType
    {
        [Description("工序号+代码")]
        ProcessAndCode,
        [Description("工序号+名称")]
        ProcessAndName,
        [Description("工序号+代码+名称")]
        ProcessAndCodeAndName,
    }
    #endregion

    #region 考勤
    public enum SystemParameterSystemAttendance
    {
        /// <summary>
        /// 允许员工多站点登录
        /// </summary>
        [SystemPrarmeter("checkbox", "允许员工多站点登录")]
        CanLoginMultiStation,
        /// <summary>
        /// 终端有员工登录时，允许其他员工刷卡替换
        /// </summary>
        [SystemPrarmeter("checkbox", "终端有员工登录时，允许其他员工刷卡替换")]
        CanLogoutOtherEmp,
        /// <summary>
        /// 员工必须按排班时间登录
        /// </summary>
        [SystemPrarmeter("checkbox", "员工必须按排班时间登录")]
        EmpLogBySchdule,
        /// <summary>
        /// 员工必须按排班时间登录
        /// </summary>
        [SystemPrarmeter("checkbox", "加班时间计入工序用时")]
        OTTimeBlongWork,
        /// <summary>
        /// 加班时间计入非本位用时
        /// </summary>
        [SystemPrarmeter("checkbox", "加班时间计入非本位用时")]
        OTTimeBlongOffstd,
        /// <summary>
        /// 班次开始前有效考勤的提前分钟数
        /// </summary>
        [SystemPrarmeter("number", "班次开始前有效考勤的提前分钟数")]
        AttIn_BeforeMins,
        /// <summary>
        /// 班次结束后有效考勤的延迟分钟数
        /// </summary>
        [SystemPrarmeter("number", "班次结束后有效考勤的延迟分钟数")]
        AttOut_AfterMins,
        /// <summary>
        /// 第二天早班开始时间与第一天晚班结束时间的最小间隔(小时)
        /// </summary>
        [SystemPrarmeter("number", "第二天早班开始时间与第一天晚班结束时间的最小间隔(小时)")]
        DayNightShiftHours,
        /// <summary>
        /// 考勤分析按一天刷两次卡处理
        /// </summary>
        [SystemPrarmeter("checkbox", "考勤分析按一天刷两次卡处理")]
        WorkAnalysisOneDaTwoCards,

        /// <summary>
        /// 月薪结算日
        /// </summary>
        [SystemPrarmeter("number", "月薪结算日")]
        SalaryDay,
    }

    /// <summary>
    /// 系统---生产
    /// </summary>
    public enum SystemAttendanceProduct
    {
        [SystemPrarmeter("checkbox", "计算工序用工时间")]
        CalcRealMinute1,
        [SystemPrarmeter("checkbox", "计算工序车缝时间")]
        CalcRealMinute2,
        [SystemPrarmeter("dropdown", "显示效率类别")]
        EffType,
        [SystemPrarmeter("dropdown", "工序分配站点方式")]
        ProcessSiteMode
    }

    public enum ProcessSiteMode
    {
        [Description("站内衣架数 ")]
        StandNumberOfUnderwear,
        [Description("站内总工时 ")]
        TotalWorkingHoursStation
    }

    /// <summary>
    /// 显示效率类别
    /// </summary>
    public enum EffType
    {
        [Description("用工效率")]
        DmploymentEfficiency,
        [Description("车缝效率")]
        SewingEfficiency
    }

    public enum SystemAttendanceOther
    {
        [SystemPrarmeter("number", "自动锁屏时间（秒）")]
        lockScreen,
        [SystemPrarmeter("number", "系统日志保留天数")]
        SaveLogDays,
    }
    #endregion

    #region 吊挂线 挂片站


    /// <summary>
    /// 吊挂线-->挂片站
    /// </summary>
    public enum SystemParameterHangUpLineHanger
    {
        [SystemPrarmeter("checkbox","衣架须注册后才能使用")]  //1
        RackUseAfterReg = 0,
        [SystemPrarmeter("checkbox", "注册在本流水线的衣架不外借")] //2
        RackNotLendOtherLine,

        [SystemPrarmeter("checkbox", "挂片站出衣架达到计划数后停止出衣")] //3
        StartingStopOutWhenOverPlan ,

        [SystemPrarmeter("checkbox", "从挂片站打出衣架时，清除半成品信息")] //4
        ResetRackWhenStart,
        [SystemPrarmeter("checkbox", "挂片站有扎卡数据才能出站")]//5
        StartAfterBund ,
        [SystemPrarmeter("checkbox", "挂片站刷扎卡后自动切换当前上线在制品")] //6
        ChgProOnlineAfterBund ,
        [SystemPrarmeter("checkbox", "同组内挂片站可共用扎卡")]//7
        MultiStCanUseSameBund ,

        [SystemPrarmeter("checkbox", "不同组挂片站可共用扎卡")]//8
        MultiLineCanUseSameBund ,
        [SystemPrarmeter("checkbox", "在制品添加产品上线时，或修改任务数量时，校验数量是否大于制单数量")] // 9
        ChecksumQuantityGreaterThanOrderQuantity,
        [SystemPrarmeter("dropdown", "空衣架回挂片站方式")]//10
        ToOriginWay,

        [SystemPrarmeter("number", "单个衣架最大挂衣数（即产品单位允许输入的最大值）")]//11
        SingleHangerMaximumNumber,
    }

    public enum ToOriginWay
    {
        [Description("回起始站点")]
        OutletStation,
        [Description("平均分配")]
        AverageYield,
    }

    public class SystemPrarmeterAttribute : Attribute
    {
        public string ControlType { get; set; }

        public string Description { get; set; }

        public string Key { get; set; }

        //public string DefaultValue { get; set; }

        public SystemPrarmeterAttribute() { }

        public SystemPrarmeterAttribute(string controlType, string description)
        {
            this.ControlType = controlType;
            this.Description = description;
            //this.DefaultValue = defaultValue;
        }
    }

    public static class SystemPrarmeterAttributeHelp
    {
        /// <summary>
        /// 获取枚举的Description
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static SystemPrarmeterAttribute Get(this System.Enum e)
        {
            Type type = e.GetType();
            string name = System.Enum.GetName(type, e);
            if (name == null)
            {
                return null;
            }
            FieldInfo field = type.GetField(name);
            SystemPrarmeterAttribute attribute = Attribute.GetCustomAttribute(field, typeof(SystemPrarmeterAttribute)) as SystemPrarmeterAttribute;
            if (attribute == null)
            {
                return null;
            }
            return attribute;
        }

        /// <summary>
        /// 获取字典类型的枚举所有元素
        /// </summary>
        /// <param name="t">枚举</param>
        /// <returns></returns>
        public static List<SystemPrarmeterAttribute> GetList(Type t)
        {
            List<SystemPrarmeterAttribute> list = new List<SystemPrarmeterAttribute>();
            Array _enumValues = System.Enum.GetValues(t);
            foreach (System.Enum value in _enumValues)
            {
                var obj = Get(value);
                obj.Key = value.ToString();
                list.Add(obj);
            }
            return list;
        }
    }

    #endregion
}


