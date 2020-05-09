using log4net;
using log4net.Config;
using NHibernate.Util;
using SuspeSys.Domain;
using SuspeSys.Domain.Common;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Support.Enums;
using SuspeSys.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Permisson
{
    public class UserService:ServiceBase
    {
        private static readonly UserService _userService = new UserService();
        private UserService() { }
        public static UserService Instance
        {
            get
            {
                return _userService;
            }
        }

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public bool Login(string userName, string password, string clientId)
        {
            try
            {

                //判断用户名，密码
                var user = QueryForObject<Users>("select * from Users a where a.UserName =?", false, userName);


                if (user == null)
                    throw new BusinessException("用户名不存在");

                if (user.Password.Trim() != Utils.Security.MD5.Encrypt(password))
                    throw new BusinessException("用户名或密码错误");




                
                CurrentUser.Instance.LoginInfo = "12123123";

                //用户角色
                this.ProcessUserRole(user.Id);

                this.ProcessRolesModules(user.Id);
                CurrentUser.Instance.LoginInfo = "用户角色";


                //客户机
                this.ProcessMachine(user.Id);

                if (CurrentUser.Instance.IsSuperAdmin)
                {
                    //获取选择客户机
                    CurrentUser.Instance.CurrentClientMachines = null;
                }
                else
                {
                    if (string.IsNullOrEmpty(clientId))
                        throw new BusinessException("请选择客户机");
                    //获取选择客户机
                    //CurrentUser.Instance.CurrentClientMachines = Dao.ClientMachinesDao.Instance.GetById(clientId);
                    string sql = "select * from ClientMachines where ClientMachineName  = @ClientMachineName";
                    CurrentUser.Instance.CurrentClientMachines = Dao.DapperHelp.QueryForObject< ClientMachines>(sql, new { ClientMachineName = clientId });
                }

                if (!CurrentUser.Instance.IsSuperAdmin)
                    if (CurrentUser.Instance.UserClientMachines == null || CurrentUser.Instance.UserClientMachines.Count == 0)
                        throw new BusinessException(string.Format("当前用户{0}没有设置客户机,请联系管理员进行设置！", user.UserName, CurrentUser.Instance.CurrentClientMachines.ClientMachineName));


                if (!CurrentUser.Instance.IsSuperAdmin)
                    if (!CurrentUser.Instance.UserClientMachines.Any(o => o.ClientMachineName == clientId))
                        throw new BusinessException(string.Format("当前用户{0}没有客户机{1}的权限,请联系管理员进行设置！", user.UserName, CurrentUser.Instance.CurrentClientMachines.ClientMachineName));




                //站点组
                this.ProcessSiteGroup(user.Id);

                if (!CurrentUser.Instance.IsSuperAdmin)
                    if (CurrentUser.Instance.UserSiteGroupList == null || CurrentUser.Instance.UserSiteGroupList.Count == 0)
                        throw new BusinessException(string.Format("当前用户{0}没有配置站点组", user.UserName));


                //用户信息
                Domain.Common.CurrentUser.Instance.User = user;

                return true;
            }
            catch (SuspeSys.Utils.Exceptions.BusinessException ex)
            {
                var log4netFileInfo = new FileInfo("Config/log4net.cfg.xml");
                XmlConfigurator.Configure(log4netFileInfo);
                var log = LogManager.GetLogger("LogLogger");
                log.Error(ex);

                throw ex;
            }
        }

        private void ProcessUserRole(string userId)
        {
            string sql = @"SELECT a.* from Roles A
						  LEFT JOIN UserRoles  B ON A.Id = B.ROLES_Id
						   WHERE B.USERS_Id = ? AND B.Id IS NOT NULL";

            CurrentUser.Instance.UserRole = base.Query<Roles>(sql, false, userId);
        }

        private void ProcessRolesModules(string userId)
        {
            string sql = @"select a.* 
                            from RolesModules  a
                            left join UserRoles b on a.ROLES_Id = b.ROLES_Id
                            where b.USERS_Id = ? and b.Id is not null";
            StringBuilder builder = new StringBuilder(sql);

            //获取用户角色
            CurrentUser.Instance.RolesModules = base.Query<RolesModules>(sql, false, userId);
        }

        private void ProcessMachine(string userId)
        {
            string sql = @"select a.*
                            from ClientMachines a
                            left join UserClientMachines b on a.id = b.ClientMachineId
                            where b.UserId = ? and b.Id is not null";
            if (CurrentUser.Instance.IsSuperAdmin)
                sql = @"select a.*
                            from ClientMachines a
                            left join UserClientMachines b on a.id = b.ClientMachineId or '1' <> ?";

            CurrentUser.Instance.UserClientMachines = base.Query<ClientMachines>(sql, false, userId);
        }

        private void ProcessSiteGroup(string userId)
        {
            string sql = @"
                        SELECT
                        distinct   a.*
                        FROM SiteGroup a
						left join Pipelining b on a.Id = b.SITEGROUP_Id
						left join UserClientMachinesPipelinings c on b.Id = c.PIPELINING_Id
						left join UserClientMachines d on c.USERCLIENTMACHINES_Id = d.Id
						where d.UserId = ? 
						  and b.id is not null 
						  and c.Id is not null 
						  and d.Id is not null";
            if (CurrentUser.Instance.IsSuperAdmin)
                sql = @"SELECT
                        distinct   a.*
                        FROM SiteGroup a where 1=1 or '1'<> ?";      
          CurrentUser.Instance.UserSiteGroupList = base.Query<SiteGroup>(sql, false, userId);
        }

        #endregion
    }
}
