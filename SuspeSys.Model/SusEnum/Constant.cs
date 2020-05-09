using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Support.Enums
{
    public class Constant
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        public const string Role_SuperAdmin = "SuperAdmin";

        /// <summary>
        /// 管理员
        /// </summary>
        public const string Role_Admin = "Admin";
    }

    public enum Sex 
    {
        [Description("男")]
        Male = 1,

        [Description("女")]
        Famale = 2
    }

    /// <summary>
    /// 操作日志类型
    /// </summary>
    public enum OperateType : int
    {
        [Description("创建")]
        Create = 1,
        [Description("读取")]
        Read = 2,
        [Description("修改")]
        Update = 3,
        [Description("删除")]
        Delete = 4
    }

    /// <summary>
    /// 客户机类型
    /// </summary>
    public enum MachineType: short
    {
        [Description("查询机")]
        Search = 1,
        [Description("管理机")]
        Manage =2
    }
}
