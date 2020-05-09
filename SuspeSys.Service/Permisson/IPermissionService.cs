using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Permisson
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public interface IPermissionService
    {
        ///// <summary>
        ///// 添加模块信息
        ///// </summary>
        ///// <param name="module"></param>
        ///// <returns></returns>
        //bool AddModule(Domain.Modules module);

        IList<ModulesModel> GetAllModules();

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<Domain.Roles> SearchRole(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

        /// <summary>
        /// 通过角色id 获取拥有该角色的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IList<Domain.UsersModel> GetUsersListByRoleId(string roleId);

        /// <summary>
        /// 通过角色Id获取菜单信息，并且选中该角色所拥有的菜单信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IList<Domain.ModulesModel> GetModulesByRoleId(string roleId);

        /// <summary>
        /// 保存角色菜单
        /// </summary>
        /// <param name="rolesModels"></param>
        /// <param name="roleId"></param>
        void SaveRolesModules(IList<RolesModules> rolesModels, string roleId);

        /// <summary>
        /// 获取系统用户
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<UsersModel> SearchUsers(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);

        /// <summary>
        /// 通过用户Id获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserDto GetUserInfoByUserId(string userId);

        void SaveUserInfo(UserDto dto);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        bool Login(string userName, string password, string clientId);

        /// <summary>
        /// 解锁屏幕
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        void UnLockScreem(string userName, string password);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="orgPassword"></param>
        /// <param name="newPassword"></param>
        void ChangePassword(string userName, string orgPassword, string newPassword);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns></returns>
        void ResetPassword(string userName, string password);
    }
}
