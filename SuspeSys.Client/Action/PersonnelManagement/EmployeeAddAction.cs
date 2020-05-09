using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.PersonnelManagement
{
    public class EmployeeAddAction : BaseAction
    {
        public IList<Domain.City> GetCityListByProvinceId(string proviceId)
        {
            return commonService.GetCityListByProvinceId(proviceId);
        }

        public IList<Domain.Area> GetAreaListByCityId(string cityId)
        {
            return commonService.GetAreaListByCityId(cityId);
        }

        public void RemoveEmployeePosition(string employeeId)
        {
            _PersonnelManagementService.RemoveAllEmployeePositionByEmployeeId(employeeId);
        }

        public IList<Domain.EmployeePositions> GetPositionsByEmployeeId(string employeeId)
        {
            return _PersonnelManagementService.GetPositionsByEmployeeId(employeeId);
        }
    }
}
