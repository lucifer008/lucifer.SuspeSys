using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.Permission
{
    public class UsersAddAction : BaseAction
    {
        /// <summary>
        /// 通过用户Id获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserDto GetUserInfoByUserId(string userId)
        {
            return base.permissionService.GetUserInfoByUserId(userId);
        }

        public void SaveUserInfo(UserDto dto)
        {
            base.permissionService.SaveUserInfo(dto);
        }
    }
}
