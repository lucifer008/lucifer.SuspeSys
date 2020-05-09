using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.Permission
{
    public class RoleModuleIndexAction : BaseAction
    {
        public IList<Domain.UsersModel> GetEmployeeListByRoleId(string roleId)
        {
            return permissionService.GetUsersListByRoleId(roleId);
        }

        /// <summary>
        /// 通过角色Id获取菜单信息，并且选中该角色所拥有的菜单信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<Domain.ModulesModel> GetModulesByRoleId(string roleId)
        {
            return permissionService.GetModulesByRoleId(roleId);
        }

        /// <summary>
        /// 保存角色菜单
        /// </summary>
        /// <param name="rolesModels"></param>
        /// <param name="roleId"></param>
        public void SaveRolesModules(IList<RolesModules> rolesModels, string roleId)
        {
            permissionService.SaveRolesModules(rolesModels, roleId);
        }


    }
}
