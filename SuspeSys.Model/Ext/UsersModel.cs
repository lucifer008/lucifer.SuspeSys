using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain
{

    [Serializable]
    public class UsersModel : Users
    {

        public virtual string EmployeeName { get; set; }
    }

    [Serializable]
    public class UserDto
    {
        public UserDto()
        {
            this.Roles = new List<RolesModel>();
            this.UserClientMachines = new List<ClientMachinesModel>();
        }

        public Users Users { get; set; }

        public IList<RolesModel> Roles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<ClientMachinesModel> UserClientMachines { get; set; }
    }
}
