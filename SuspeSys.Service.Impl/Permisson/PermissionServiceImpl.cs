using SuspeSys.Service.Permisson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Support.Utilities;
using NHibernate.Util;
using NHibernate;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Utils.Exceptions;
using SuspeSys.Domain.Common;
using SuspeSys.Service.Impl.Common;
using SuspeSys.Support.Enums;
using SuspeSys.Utils;
using SuspeSys.Domain.Ext;
using SuspeSys.Dao;

namespace SuspeSys.Service.Impl.Permisson
{
    public class PermissionServiceImpl : ServiceBase, IPermissionService
    {
        public PermissionServiceImpl() { }
        public static PermissionServiceImpl Instance { get { return new PermissionServiceImpl(); } }

        ///// <summary>
        ///// 添加模块信息
        ///// </summary>
        ///// <param name="module"></param>
        ///// <returns></returns>
        //public bool AddModule(Modules module)
        //{
        //    base.
        //}

        /// <summary>
        /// 获取所有菜单信息
        /// </summary>
        /// <returns></returns>
        public IList<ModulesModel> GetAllModules()
        {
            return  SuspeSys.Dao.ModulesDao.Instance.GetAll()
                                                    //.Where(o => o.Deleted == 0)
                                                    .TransformTo<Modules, ModulesModel>().ToList();

        }

        #region 角色
        public IList<Domain.Roles> SearchRole(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from Roles where 1=1 and Deleted = 0";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (actionName like ? )");
                paramValues = new string[] { string.Format("%{0}%", searchKey)};
            }
            var rslt1 = Query<Domain.Roles>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, false, paramValues);
            return rslt1;
        }

        /// <summary>
        /// 通过角色id 获取拥有该角色的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<Domain.UsersModel> GetUsersListByRoleId(string roleId)
        {
            string sql = @"select 
                            a.*
							,C.RealName EmployeeName 
                            from Users a
                            left join UserRoles b on a.Id = b.USERS_Id
							left join Employee C on a.Employee_id = C.id
							where b.Id is not null AND  b.ROLES_Id = ?";
            return Query<Domain.UsersModel>(new StringBuilder(sql), null, true, roleId);
        }
        #endregion

        #region 角色权限
        /// <summary>
        /// 通过角色Id获取菜单信息，并且选中该角色所拥有的菜单信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<Domain.ModulesModel> GetModulesByRoleId(string roleId)
        {
            //获取所有菜单
            var moduleAll = this.GetAllModules().Where( o => o.Deleted == 0);

            //获取该角色有的权限
            var rolesModules = this.GetRolesModulesByRoleId(roleId);

            moduleAll.ForEach(o =>{
                o.Checked = rolesModules.Any(a => a.Modules.Id == o.Id);
            });

            return moduleAll.ToList();
        }

        private IList<Domain.RolesModules> GetRolesModulesByRoleId(string roleId)
        {
            string sql = "select * from RolesModules where ROLES_Id = ?";

            return base.Query<Domain.RolesModules>(new StringBuilder(sql), null, false, roleId);
        }

        public void SaveRolesModules(IList<RolesModules> rolesModels, string roleId)
        {
            ISession session = SessionFactory.GetCurrentSession();

            using (ITransaction tran = session.BeginTransaction())
            {
                try
                {
                    
                    //删除该角色的所有菜单
                    SuspeSys.Dao.RolesModulesDao.Instance.DeleteByHql(string.Format("from RolesModules where ROLES_Id = '{0}'", roleId), true);
                    rolesModels.ForEach(o => 
                    {
                        SuspeSys.Dao.RolesModulesDao.Instance.Save(o, true);
                    });
                    

                    tran.Commit();
                }
                catch (Exception ex)
                {

                    tran.Rollback();
                    throw ex;
                }
            }

            
        }
        #endregion

        public IList<UsersModel> SearchUsers(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = @"select 
                                    a.* 
                                    ,b.RealName EmployeeName 
                                    from Users a
                                    left join Employee b on a.Employee_id = b.id";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                queryString += string.Format(@" AND (Code like ? or RealName like ?)");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Domain.UsersModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        /// <summary>
        /// 通过用户Id获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserDto GetUserInfoByUserId(string userId)
        {
            UserDto dto = new UserDto();

            dto.Users = Dao.UsersDao.Instance.GetById(userId);

            if (dto.Users == null)
                dto.Users = new Users();

            //获取所有角色
            dto.Roles = base.Query<Domain.RolesModel>("select * from Roles where  Deleted = 0", true, null);

            //所有客户机列表
            dto.UserClientMachines = base.Query<Domain.ClientMachinesModel>("select * from ClientMachines where  Deleted = 0", true, null);

            #region Pipelining sql
            string sql = @"
          select a.*, b.GroupNO,b.GroupName,
b.MainTrackNumber 
from
[Pipelining] a
left join SiteGroup b on a.SITEGROUP_Id = b.Id
where a.Deleted = 0 and b.Id is not null ";
            #endregion

            dto.UserClientMachines.ForEach(o =>{

                //获取客户机生产线
                o.Pipelining = base.Query<Domain.PipeliningModel>(sql, true);
            });

            if (!string.IsNullOrEmpty(dto.Users.Id))
            {
                this.CheckUserInfo(dto);
            }


            return dto;
        }

        private void CheckUserInfo(UserDto dto)
        {
            string userId = dto.Users.Id;
            
            //用户角色
            var userRole = base.Query<Domain.UserRoles>("SELECT * FROM UserRoles WHERE USERS_Id = ?", false, userId );
            //用户客户机
            var userClients = base.Query<Domain.UserClientMachines>("SELECT * FROM UserClientMachines WHERE UserId = ?", false,  userId );

            //处理dto数据，选中当前选择用户的数据bein


            dto.Roles.ForEach(o => {
                o.Checked = userRole.Any(sub => o.Id == sub.Roles.Id);
            });

            dto.UserClientMachines.ForEach((Action<ClientMachinesModel>)(o => {
                o.Checked = userClients.Any((Func<UserClientMachines, bool>)(sub => o.Id == sub.ClientMachineId));

                string sql = @"select a.* 
                                from [Pipelining] a
                                where a.Deleted = 0
                                and a.id in 
	                                (	
		                                select PIPELINING_Id from UserClientMachinesPipelinings
		                                where USERCLIENTMACHINES_Id in (select id from UserClientMachines  b where b.ClientMachineId = ? and b.UserId = ?)
                                    )";
                //获取当前用户，客户机下的生产线
                var list = base.Query<Pipelining>(sql, false, o.Id, userId);

                //选中已有生产线
                o.Pipelining.ForEach(b => {
                    b.Checked = list.Any(c => b.Id == c.Id);
                });

            }));
            //处理dto End
        }

        /// <summary>
        /// 用户名是否重复
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UserNameExist(Users user)
        {

            var dbUser = base.Query<Users>(new StringBuilder("SELECT * FROM Users where UserName = ?"),null,false ,user.UserName);

            if (dbUser == null || dbUser.Count == 0)
                return false;
            else
            {
                if (!string.IsNullOrEmpty(user.Id))
                {
                    if (dbUser.Any(o => o.Id != user.Id))
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            }


        }

        public void SaveUserInfo(UserDto dto)
        {
            var opService = new UserOpLogServiceImpl();
            UserOperateLogsModel log = new UserOperateLogsModel()
            {
                Deleted = 0,
                //OpDataCode = id,
                OpFormName = "用户管理",
                OpFormCode = "UserIndex",
                OpTableCode = dto.Users.GetType().Name,
                //OpType = (int)OperateType.Delete
            };

            List<Variance> diffs = new List<Variance>();


            if (this.UserNameExist(dto.Users))
                throw new SuspeSys.Utils.Exceptions.BusinessException("用户名已经存在");

            if (string.IsNullOrEmpty(dto.Users.Id))
            {
                //保存用户信息
                Dao.UsersDao.Instance.Save(dto.Users);
                //opService.InsertLog<Users>(dto.Users.Id, "用户管理", "UserIndex", dto.Users.GetType().Name, true);
                log.OpType = (int)OperateType.Create;
            }
            else
            {
                //获取原有实体
                var dbModel = base.QueryForObject<Users>("select * from Users where id = ?",false, dto.Users.Id);
                if (dbModel == null)
                    dbModel = new Users();

                var diffObj = Eextentions.Diff<Users>(dbModel, dto.Users);

                diffs = diffs.Concat(diffObj).ToList();

                //opService.UpdateLog<Users>(dto.Users.Id, dto.Users, "用户管理", "UserIndex", dto.Users.GetType().Name, true);
                Dao.UsersDao.Instance.Update(dto.Users);
            }

            #region 日志處理
            //原有角色
            var dbUserRoleOrg = base.Query<UserRoles>( "select * from UserRoles where USERS_Id = ?", false, dto.Users.Id);

            //获取删除角色Id
            var diffDelRole = dbUserRoleOrg.Select(o => o.Roles.Id).Except(dto.Roles.Where(o => o.Checked).Select(o => o.Id ))
                                           .Select(o => new Variance
                                           {
                                               NewValue = "",
                                               OriginalValue = o,
                                               PropCode = "RoleId",
                                               PropDescription = "角色"
                                           }).ToList();

            diffs = diffs.Concat(diffDelRole).ToList();
            //获取新增角色
            var diffAddRole = dto.Roles.Where(o => o.Checked).Select(o => o.Id).Except(dbUserRoleOrg.Select(o => o.Roles.Id ))
                                           .Select(o => new Variance
                                           {
                                               NewValue =o ,
                                               OriginalValue = "",
                                               PropCode = "RoleId",
                                               PropDescription = "角色"
                                           }).ToList();
            diffs = diffs.Concat(diffAddRole).ToList();

            //獲取原有客戶機
            var userClientMachineOrg = base.Query<UserClientMachines>("select * from UserClientMachines where UserId = ?", false, dto.Users.Id);

            //删除Client
            var diffClient = userClientMachineOrg.Select(o => o.ClientMachineId).Except(dto.UserClientMachines.Where(o => o.Checked).Select(o => o.Id))
                                           .Select(o => new Variance
                                           {
                                               NewValue = "",
                                               OriginalValue = o,
                                               PropCode = "ClientMachineId",
                                               PropDescription = "客户机Id"
                                           });
            diffs = diffs.Concat(diffClient).ToList();

            //新增Client
            var diffAddClient = dto.UserClientMachines.Where(o => o.Checked).Select(o => o.Id).Except(userClientMachineOrg.Select(o => o.ClientMachineId))
                                          .Select(o => new Variance
                                          {
                                              NewValue = o,
                                              OriginalValue = "",
                                              PropCode = "ClientMachineId",
                                              PropDescription = "客户机Id"
                                          });
            diffs = diffs.Concat(diffAddClient).ToList();

            
            #endregion



            Dao.UserRolesDao.Instance.DeleteByHql(string.Format("from  UserRoles where Users.Id = '{0}'", dto.Users.Id), false);
            //保存角色
            dto.Roles.ForEach(o => {
                if (o.Checked)
                {
                    Dao.UserRolesDao.Instance.Save(new UserRoles()
                    {
                        Roles = new Roles() { Id = o.Id },
                        Users = new Users() { Id = dto.Users.Id }
                    });
                }
            });

            //先获取，后删除
            //var listUCP = base.Query<UserClientMachinesPipelinings>(@"
            //                    select a.* 
            //                    from UserClientMachinesPipelinings a 
            //                    left join  UserClientMachines  b on a.USERCLIENTMACHINES_Id = b.Id
            //                    where b.Id is not null and b.UserId = ?", false, dto.Users.Id);

            //if (listUCP != null)
            //{
            //    listUCP.ForEach((Action<UserClientMachinesPipelinings>)(o =>
            //    {
            //        Dao.UserClientMachinesPipeliningsDao.Instance.Delete(o.Id, true);
            //    }));
            //}


            //新流水线Id 日志使用
            List<string> newPipeIds = new List<string>();

            //删除关系表
            Dao.UserClientMachinesPipeliningsDao
               .Instance
               .DeleteByHql(string.Format("from UserClientMachinesPipelinings where UserClientMachines.UserId = '{0}'", dto.Users.Id));
            //删除用户客户机机表
            Dao.UserClientMachinesDao.Instance.DeleteByHql(string.Format("from  UserClientMachines where UserId = '{0}'", dto.Users.Id));
            //保存客户机
            dto.UserClientMachines.ForEach((Action<ClientMachinesModel>)(o => {
                if (o.Checked)
                {
                    var UserClientMachines = new UserClientMachines()
                    {
                        UserId = dto.Users.Id,
                        ClientMachineId = o.Id,
                    };

                    Dao.UserClientMachinesDao.Instance.Save(UserClientMachines);

                    //保存客户机
                    o.Pipelining.ForEach(p => {
                        if (p.Checked)
                        {
                            newPipeIds.Add(p.Id);
                            Dao.UserClientMachinesPipeliningsDao.Instance.Save(new UserClientMachinesPipelinings() {
                                UserClientMachines = UserClientMachines,
                                Pipelining = (Pipelining)p
                            });
                        }
                    });
                }
            }));

            #region 日志相关
            //獲取原有Pipelinings
            string sql = @"select a.* 
                            from UserClientMachinesPipelinings a
                            left join UserClientMachines b on a.USERCLIENTMACHINES_Id = b.Id
                            where b.UserId = ?";

            var userPipeliningsOrg = base.Query<UserClientMachinesPipelinings>(sql, false, dto.Users.Id);




            //删除流水线
            var diffDelPipel = userPipeliningsOrg.Select(o => o.Pipelining.Id).Except(newPipeIds)
                                                 .Select(o => new Variance
                                                 {
                                                     NewValue = "",
                                                     OriginalValue = o,
                                                     PropCode = "PipeliningId",
                                                     PropDescription = "生产线"
                                                 });

            diffs = diffs.Concat(diffDelPipel).ToList();

            //新增流水线
            var diffAddPipel = newPipeIds.Except(userPipeliningsOrg.Select(o => o.Pipelining.Id))
                                         .Select(o => new Variance
                                         {
                                             NewValue = "",
                                             OriginalValue = o,
                                             PropCode = "PipeliningId",
                                             PropDescription = "生产线"
                                         });

            diffs = diffs.Concat(diffAddPipel).ToList();
            #endregion

            log.OperateLogDetailModels = diffs.Select( o=> new UserOperateLogDetailModel {
                                             BeforeChange = o.OriginalValue,
                                             Changed = o.NewValue,
                                             FieldCode = o.PropCode,
                                             FieldName = o.PropDescription
                                        }).ToList();
            opService.AddLog(log);
            //整体提交
            //SessionFactory.GetCurrentSession().Flush();
        }

        public bool Login(string userName, string password, string clientId)
        {
            //UserService userService = new UserService(0;)
            //登录
           bool loginResult =  UserService.Instance.Login(userName, password, clientId);

            UserLoginInfo loginInfo = new UserLoginInfo() {
                EmployeeId = CurrentUser.Instance.User.Employee?.Id,
                EmployeeName = CurrentUser.Instance.User.Employee?.RealName,
                LoginDate = DateTime.Now,
                SessionId = Guid.NewGuid().ToString().Replace("-",""),
                UserId = CurrentUser.Instance.UserId,
                UserName = CurrentUser.Instance.UserName,
            };

            CurrentUser.Instance.SessionId = loginInfo.SessionId;
            //记录登录日志
            Dao.UserLoginInfoDao.Instance.Save(loginInfo);

            return loginResult;
        }

        /// <summary>
        /// 解锁屏幕
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void UnLockScreem(string userName, string password)
        {
            //判断用户名，密码
            var user = QueryForObject<Users>("select * from Users a where a.UserName =?", false, userName);


            if (user == null)
                throw new BusinessException("用户名不存在");

            if (user.Password.Trim() != Utils.Security.MD5.Encrypt(password))
                throw new BusinessException("用户名或密码错误");

            if (!userName.ToLower().Equals(CurrentUser.Instance.UserName.ToLower()))
            {
                throw new BusinessException("解锁用户不是当前登录用户");
            }
        }

        public IList<UserRolesCache> GetUserRolesCache()
        {
            string sql = @"select a.ROLES_Id RoleId , a.USERS_Id UserId, c.ActionName RoleName, b.UserName UserName
                            from UserRoles  a
                            left join users  b on a.USERS_Id = b.id
                            left join Roles c on a.ROLES_Id = c.id";

            return this.Query<UserRolesCache>(sql,true);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="orgPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangePassword(string userName, string orgPassword, string newPassword)
        {
            string sql = "SELECT * FROM Users WHERE UserName = @Name";
            //获取用户
            var user = DapperHelp.QueryForObject<Users>(sql, new { Name = userName });

            if (user == null)
                throw new Exception("未获取到用户");

            if (!Utils.Security.MD5.Encrypt(orgPassword).Equals(user.Password.Trim()))
            {
                throw new Exception("原密码不正确");
            }

            //if (orgPassword != newPassword)
            //    throw new Exception("两次输入密码不一致");

            sql = "UPDATE Users set Password = @Password where Id = @Id";

            newPassword = Utils.Security.MD5.Encrypt(newPassword);

            DapperHelp.Execute(sql, new { Password = newPassword, Id = user.Id });
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns></returns>
        public void ResetPassword(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("密码不能为空");

            string sql = "SELECT * FROM Users WHERE UserName = @Name";
            //获取用户
            var user = DapperHelp.QueryForObject<Users>(sql, new { Name = userName });

            if (user == null)
                throw new Exception("未获取到用户");

            sql = "UPDATE Users set Password = @Password where Id = @Id";

            string newPassword = Utils.Security.MD5.Encrypt(password);

            DapperHelp.Execute(sql, new { Password = newPassword, Id = user.Id });
        }
    }
}
