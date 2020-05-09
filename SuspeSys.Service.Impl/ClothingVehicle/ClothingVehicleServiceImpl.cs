using SuspeSys.Service.Impl.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Da = SuspeSys.Domain;
namespace SuspeSys.Service.ClothingVehicle
{
    public class ClothingVehicleServiceImpl : ServiceBase,IClothingVehicleService
    {
        public static ClothingVehicleServiceImpl Instance = new ClothingVehicleServiceImpl();

        public IList<Da.ClothingVehicleModel> SearchClothingVehicle(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            string queryString = @"select cv.*,cvt.Name ClothingVehicleName from ClothingVehicle cv 
 left join ClothingVehicleType cvt on cvt.Id=cv.CLOTHINGVEHICLETYPE_Id where 1=1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(wher))
            {
                queryString += string.Format(@" AND (CardNo like ? )");
                paramValues = new string[] { string.Format("%{0}%", wher) };
            }
            var rslt1 = Query<Domain.ClothingVehicleModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<Da.FaultCodeTableModel> GetFaultCodeTableAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            string queryString = @" select fct.*,cvt.Name ClothingVehicleName from FaultCodeTable fct
 left join ClothingVehicleType cvt on cvt.Id=fct.CLOTHINGVEHICLETYPE_Id where 1=1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(wher))
            {
                queryString += string.Format(@" AND (FaultName like ? )");
                paramValues = new string[] { string.Format("%{0}%", wher) };
            }
            var rslt1 = Query<Domain.FaultCodeTableModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<Da.ClothingVehicleType> GetClothingVehicleTypeAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            string queryString = "select * from ClothingVehicleType where 1=1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(wher))
            {
                queryString += string.Format(@" AND (Name like ? )");
                paramValues = new string[] { string.Format("%{0}%", wher) };
            }
            var rslt1 = Query<Domain.ClothingVehicleType>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<Da.SewingMachineLoginLogModel> GetSewingMachineLoginLogAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            string queryString = "select * from SewingMachineLoginLog where 1=1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(wher))
            {
                queryString += string.Format(@" AND (Workshop like ? )");
                paramValues = new string[] { string.Format("%{0}%", wher) };
            }
            var rslt1 = Query<Domain.SewingMachineLoginLogModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<Da.MechanicEmployeesModel> GetMechanicEmployeesAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            string queryString = "select * from MechanicEmployees where 1=1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(wher))
            {
                queryString += string.Format(@" AND (RealName like ? )");
                paramValues = new string[] { string.Format("%{0}%", wher) };
            }
            var rslt1 = Query<Domain.MechanicEmployeesModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<Da.ClothingVehicleMaintenanceLogs> GetClothingVehicleMaintenanceLogsAllList(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string wher)
        {
            string queryString = "select * from ClothingVehicleMaintenanceLogs where 1=1";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(wher))
            {
                queryString += string.Format(@" AND (WorkShiop like ? )");
                paramValues = new string[] { string.Format("%{0}%", wher) };
            }
            var rslt1 = Query<Domain.ClothingVehicleMaintenanceLogs>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }
    }
}
