using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.Permission
{
    public class ModuleAction : BaseAction
    {
        public IList<ModulesModel> GetAllModulesList()
        {
            return permissionService.GetAllModules();
        }
    }
}
