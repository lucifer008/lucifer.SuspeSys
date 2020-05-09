using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Server.Authorization
{
    public class Grant
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 服务器名
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Mac
        /// </summary>

        public string Mac { get; set; }

        /// <summary>
        /// 授权开始时间
        /// </summary>

        public DateTime Begin { get; set; }

        /// <summary>
        /// 授权结束时间
        /// </summary>

        public DateTime End { get; set; }
    }
}
