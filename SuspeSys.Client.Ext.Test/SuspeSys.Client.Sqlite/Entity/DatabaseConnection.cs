using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Sqlite.Entity
{
    public class DatabaseConnection
    {
        public int Id { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 服务器地址（IP）
        /// </summary>
        public string ServerIP { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// DB名
        /// </summary>
        public string DBName { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// 启用时间
        /// </summary>
        public DateTime? EnableTime { get; set; }
    }
}
