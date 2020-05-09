using SuspeSys.Client.Action.SuspeRemotingClient;
using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.Common
{
    public class SystemParameterAction : BaseAction
    {
        public IEnumerable<Domain.PipeliningModel> GetPipelining()
        {
            return systemPapameterService.GetPipelining();
        }

        public List<Domain.SystemModuleParameterModel> GetAllSystemModuleParameter()
        {
            return systemPapameterService.GetAllSystemModuleParameter();
        }

        public void SaveOrUpdate(SystemModuleParameterValue parameterValue)
        {
             systemPapameterService.SaveOrUpdateSystemModuleParameterValue(parameterValue);
            SuspeRemotingService.reloadCacheService.ReloadSystemParameter();
        }
    }
}
