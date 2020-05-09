using SuspeSys.Domain.SusEnum;
using SuspeSys.Domain.SusEvent;
using SuspeSys.Support.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Common
{
    /// <summary>
    /// 当前登录用户信息
    /// </summary>
    public class CurrentUser
    {
        private static readonly CurrentUser _currentUser = new CurrentUser();
        //public delegate void OnGroupChange(string groupNo);
        public event EventHandler<SusEventArgs> GroupChangeEvent;

        private CurrentUser() { }
        public static CurrentUser Instance
        {
            get
            {
                return _currentUser;
            }
        }

        /// <summary>
        /// 受否授权
        /// </summary>
        public bool IsAuthorization { get; set; }

        public string LoginInfo { get; set; }

        public Users User { get; set; }

        /// <summary>
        /// 当前回话Id
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 当前用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return this.User?.UserName;
            }
        }

        /// <summary>
        /// 当前用户Id
        /// </summary>
        public string UserId
        {
            get
            {
                return this.User.Id;
            }
        }

        /// <summary>
        /// 员工RealName
        /// </summary>
        public string EmployeeName
        {
            get
            {
                if (this.User == null || this.User.Employee == null)
                    return string.Empty;
                else
                    return this.User.Employee.RealName;
            }
        }

        public IList<RolesModules> RolesModules
        {
            get; set;
        }

        public IList<Roles> UserRole { get; set; }

        /// <summary>
        /// 是否包含操作权限
        /// </summary>
        /// <param name="resourceId">Module中配置的ActionName</param>
        /// <returns></returns>
        public bool HasPermisson(string resourceId)
        {
            if (this.UserRole == null)
                return false;

            //超级管理员角色
            if (this.UserRole.Any(o => o.ActionName.ToLower() == Constant.Role_SuperAdmin.ToLower()))
                return true;

            //管理员
            if (this.UserRole.Any(o => o.ActionName.ToLower() == Constant.Role_Admin.ToLower()))
                return true;

            if (this.RolesModules == null)
                return false;

            //当前用户有访问该资源的权限
            if (this.RolesModules.Any(o => o.Modules.ActionKey.ToLower() == resourceId.ToLower()))
                return true;

            return false;
        }

        public bool IsSuperAdmin
        {
            get
            {
                return this.UserRole.Any(o => o.ActionName == Constant.Role_SuperAdmin);
            }
        }

        public bool IsAdmin
        {
            get
            {
                return this.UserRole.Any(o => o.ActionName == Constant.Role_Admin);
            }
        }

        /// <summary>
        /// 当前用户 客户机
        /// </summary>
        public IList<ClientMachines> UserClientMachines { get; set; }

        /// <summary>
        /// 当前用户所属组别
        /// </summary>
        public IList<SiteGroup> UserSiteGroupList { get; set; }

        public ClientMachines CurrentClientMachines { get; set; }

        /// <summary>
        /// 当前客户机类型
        /// </summary>
        public string CurrentClientTypeName
        {
            get
            {
                if (this.CurrentClientMachines != null)
                {
                    MachineType os = (MachineType)this.CurrentClientMachines.ClientMachineType;
                    return os.Description();
                }
                else
                    return string.Empty;
            }
        }

        public SiteGroup CurrentSiteGroup { get; set; }


        /// <summary>
        /// 刷新频率
        /// </summary>
        public int RefreshFrequency { set; get; }
        public System.Timers.Timer Timer = new System.Timers.Timer();
        public void OnGroupChange(SiteGroup siteGroup)
        {
            if (null != GroupChangeEvent)
            {
                GroupChangeEvent(siteGroup, new SusEventArgs()
                {
                    Tag = siteGroup?.GroupNo?.Trim()
                });
            }
        }
    }
}
