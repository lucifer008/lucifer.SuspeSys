using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Sqlite.Entity
{
    public class Users
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool SaveUserName { get; set; }

        public bool SavePassword { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
