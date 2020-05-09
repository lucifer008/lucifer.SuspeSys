using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils
{
    public class GrantOnLine
    {
        public string GrantLocal { get; set; }

        /// <summary>
        /// 加密后的Mac地址
        /// </summary>
        public string Mac { get; set; }

        public string HashCode { get; set; }

        public string CustomerName { get; set; }
        public string ClientName { get; set; }

        /// <summary>
        /// 唯一标识(返回客户端)
        /// </summary>
        public string CustomerCode { get; set; }
    }
}
