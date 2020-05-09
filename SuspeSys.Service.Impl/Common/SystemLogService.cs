using SuspeSys.Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Common
{
    public class SystemLogService
    {
        private static readonly SystemLogService _SystemLogService = new SystemLogService();
        public static SystemLogService Instance
        {
            get
            {
                return _SystemLogService;
            }
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="log"></param>
        public void AddLogs(SystemLogs log)
        {
            log.Id = Guid.NewGuid().ToString("X");

            Dao.DapperHelp.Add(log);
        }
    }
}
