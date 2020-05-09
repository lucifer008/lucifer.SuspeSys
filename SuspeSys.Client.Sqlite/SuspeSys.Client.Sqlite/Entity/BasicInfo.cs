using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Sqlite.Entity
{
    public class BasicInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public enum BasicInfoEnum
    {
        /// <summary>
        /// 记住用户名
        /// </summary>
        SaveUserName = 0,

        /// <summary>
        /// 记住密码
        /// </summary>
        SavePassword = 1,

        /// <summary>
        /// 默认客户机
        /// </summary>
        DefaultClient
    }
}
