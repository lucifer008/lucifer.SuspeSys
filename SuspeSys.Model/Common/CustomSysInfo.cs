using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Common
{
    /// <summary>
    /// 客戶端自定義屬性
    /// </summary>
    public class CustomSysInfo
    {
        private static readonly CustomSysInfo _sysInfo = new CustomSysInfo();

        private CustomSysInfo() { }
        public static CustomSysInfo Instance
        {
            get
            {
                return _sysInfo;
            }
        }

        /// <summary>
        /// 客戶機名稱
        /// </summary>
        public string ClientName { get; set; }
    }
}
