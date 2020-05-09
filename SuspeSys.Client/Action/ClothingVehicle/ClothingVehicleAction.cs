using SuspeSys.Service.ClothingVehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Da = SuspeSys.Domain;
namespace SuspeSys.Client.Action.ClothingVehicle
{
   public class ClothingVehicleAction
    {
        /// <summary>
        /// 衣车资料
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="wher"></param>
        /// <returns></returns>
        public IList<Da.ClothingVehicleModel> GetClothingVehicleAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            return ClothingVehicleServiceImpl.Instance.SearchClothingVehicle(currentPageIndex, pageSize, out totalCount, ordercondition, wher);
        }
        /// <summary>
        /// 故障代码
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="wher"></param>
        /// <returns></returns>
        internal IList<Da.FaultCodeTableModel> GetFaultCodeTableAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            return ClothingVehicleServiceImpl.Instance.GetFaultCodeTableAllList(currentPageIndex, pageSize, out totalCount, ordercondition, wher);
        }
        /// <summary>
        /// 衣车类别
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="wher"></param>
        /// <returns></returns>
        internal IList<Da.ClothingVehicleType> GetClothingVehicleTypeAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            return ClothingVehicleServiceImpl.Instance.GetClothingVehicleTypeAllList(currentPageIndex, pageSize, out totalCount, ordercondition, wher);
        }

        internal IList<Da.SewingMachineLoginLogModel> GetSewingMachineLoginLogAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            return ClothingVehicleServiceImpl.Instance.GetSewingMachineLoginLogAllList(currentPageIndex, pageSize, out totalCount, ordercondition, wher);
        }

        internal IList<Da.ClothingVehicleMaintenanceLogs> GetClothingVehicleMaintenanceLogsAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            return ClothingVehicleServiceImpl.Instance.GetClothingVehicleMaintenanceLogsAllList(currentPageIndex, pageSize, out totalCount, ordercondition, wher);
        }

        internal IList<Da.MechanicEmployeesModel> GetMechanicEmployeesAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            return ClothingVehicleServiceImpl.Instance.GetMechanicEmployeesAllList(currentPageIndex, pageSize, out totalCount, ordercondition, wher);
        }
    }
}
