using SuspeSys.Dao;
using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl
{
    public class IdGeneratorService
    {
        private IdGeneratorService() { }
        public readonly static IdGeneratorService Instance = new IdGeneratorService();
        private object ob = new object();
        public long GetBatchNo(string hangerNo)
        {
            lock (ob)
            {
                var sql = string.Format("select ISNULL(MAX(ISNULL(BatchNo,0)),0) BatchNo from SucessProcessOrderHanger where (HangerNo=@HangerNo OR HangerNo=@HangerNo1)");
                var sp = DapperHelp.QueryForObject<SucessProcessOrderHanger>(sql, new { HangerNo = hangerNo, HangerNo1="-"+hangerNo });
                if (null != sp.BatchNo && sp.BatchNo.Value != 0)
                {
                    return sp.BatchNo.Value + 1;
                }
            }
            return 1;
        }
    }
}
