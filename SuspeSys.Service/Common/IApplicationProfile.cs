using SuspeSys.Domain.SusEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Common
{
    /// <summary>
    /// 系統配置
    /// </summary>
    public interface IApplicationProfileService
    {
        void AddOrUpdate(Domain.ApplicationProfile profile);

        void AddOrUpdate(List<Domain.ApplicationProfile> profile);

        /// <summary>
        /// 通过名称获取系统配置信息
        /// </summary>
        /// <param name="SysProfileEnum"></param>
        /// <returns></returns>
        string GetByName(ApplicationProfileEnum SysProfileEnum);
    }
}
