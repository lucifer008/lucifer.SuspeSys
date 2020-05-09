using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action
{
    public class frmLoginAction:BaseAction
    {
        public bool Login(string userName, string password, string clientId)
        {
            return permissionService.Login(userName, password, clientId);
        }

        public IList<ClientMachinesModel> GetAllClientMachines()
        {
            return _PersonnelManagementService.GetAllClientMachines();
        }

        public void UnLockScreem(string userName, string password)
        {
            permissionService.UnLockScreem(userName, password);
        }

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="hostName"></param>
        public void GranValid(string hostName)
        {
            var client = commonService.GetList<ClientMachines>()
                                      .Where(o => o.ClientMachineName.Equals(hostName, StringComparison.OrdinalIgnoreCase));
            if (client == null || client.Count() == 0)
                throw new Exception("未授权");
            else
            {
                var clientObj = client.FirstOrDefault();

                SuspeSys.Utils.Authorization.AuthorizationDetection.Instance.Authorization(clientObj.AuthorizationInformation);
                //SuspeSys.Utils.Authorization.AuthorizationDetection.Instance.Authorization(clientObj.AuthorizationInformation);
            }
        }

        public void ChangePassword(string userName, string orgPassword, string newPassword)
        {
            permissionService.ChangePassword(userName, orgPassword, newPassword);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public void ResetPassword(string userName, string password)
        {
            permissionService.ResetPassword(userName, password);
        }
    }
}
