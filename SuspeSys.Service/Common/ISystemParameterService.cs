using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Common
{
    public interface ISystemParameterService
    {
        List<Domain.SystemModuleParameterModel> GetAllSystemModuleParameter();

        /// <summary>
        /// 获取生产线
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.PipeliningModel> GetPipelining();

        void SaveOrUpdateSystemModuleParameterValue(SystemModuleParameterValue parameterValue);
    }
}
