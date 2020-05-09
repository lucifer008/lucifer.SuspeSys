using SuspeSys.Dao;
using SuspeSys.Service.Impl.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products
{
    public class CANLackMaterialsQueryService : ServiceBase
    {
        private CANLackMaterialsQueryService() { }
        public static CANLackMaterialsQueryService Instance => new CANLackMaterialsQueryService();

        /// <summary>
        /// 获取所有缺料信息
        /// </summary>
        /// <returns></returns>
        public IList<Domain.LackMaterialsTable> GetAll()
        {
            var sql = "select * from LackMaterialsTable where Deleted = 0";

            return DapperHelp.QueryForList<Domain.LackMaterialsTable>(sql);
        }
    }
}
