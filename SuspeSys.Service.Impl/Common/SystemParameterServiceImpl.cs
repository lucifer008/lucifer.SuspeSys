using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Domain.Common;
using SuspeSys.Service.Common;
using SuspeSys.Service.Impl.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Common
{
    public class SystemParameterServiceImpl : ServiceBase, ISystemParameterService
    {

        public IEnumerable<Domain.PipeliningModel> GetPipelining()
        {
            string sql = @"select a.*, b.GroupNO,b.GroupName,
                            b.MainTrackNumber
                            from
                            [Pipelining] a
                            left join SiteGroup b on a.SITEGROUP_Id = b.Id
                            where a.Deleted = 0 and b.Id is not null";

            return DapperHelp.Query<Domain.PipeliningModel>(sql);

        }

        public List<Domain.SystemModuleParameterModel> GetAllSystemModuleParameter()
        {

            List<Domain.SystemModuleParameterModel> models = new List<Domain.SystemModuleParameterModel>();

            var dbModels = DapperHelp.Query<Domain.SystemModuleParameterModel>("select * from SystemModuleParameter");
            if (dbModels != null)
                models = dbModels.ToList();
            else
                throw new ArgumentNullException("未获取到系统参数");

            //获取系统参数下拉列表

            foreach (var item in models)
            {
                //获取下拉列表
                if (item.ParamterControlType == "dropdown")
                {
                    string sql = "select * from SystemModuleParameterDomain where  SYSTEMMODULEPARAMETER_Id  = @ParameterId";
                    item.SystemModuleParameterDomainList = DapperHelp.Query<SystemModuleParameterDomain>(sql, new { ParameterId = item.Id });
                }

                {
                    string sql = "select * from SystemModuleParameterValue where  SYSTEMMODULEPARAMETER_Id  = @ParameterId";
                    item.SystemModuleParameterValueList = DapperHelp.Query<SystemModuleParameterValue>(sql, new { ParameterId = item.Id });
                }

                //吊挂线 包含多个值
                //if (item.SysNo.Equals("0003"))
                //{
                //    string sql = "select * from SystemModuleParameterValue where  SYSTEMMODULEPARAMETER_Id  = @ParameterId";
                //    item.SystemModuleParameterValueList = DapperHelp.Query<SystemModuleParameterValue>(sql, new { ParameterId = item.Id });
                //}
                //else if (item.SysNo.Equals("0004"))
                //{
                //    string sql = "select * from SystemModuleParameterValue where  SYSTEMMODULEPARAMETER_Id  = @ParameterId";
                //    item.SystemModuleParameterValue = DapperHelp.QueryForObject<SystemModuleParameterValue>(sql, new { ParameterId = item.Id });
                //}
                //else
                //{
                //    //特殊处理，功能做完后优化
                //    string sql = "select * from SystemModuleParameterValue where  SYSTEMMODULEPARAMETER_Id  = @ParameterId";
                //    item.SystemModuleParameterValue = DapperHelp.QueryForObject<SystemModuleParameterValue>(sql, new { ParameterId = item.Id });
                //}
            }

            return models;
        }

        public void SaveOrUpdateSystemModuleParameterValue(SystemModuleParameterValue parameterValue)
        {
            //判断参数是否存在
            string sql = "select * from SystemModuleParameterValue a where a.SYSTEMMODULEPARAMETER_Id =@SystemModuleId and a.ProductLineId = @ProductLineId";
            if ( string.IsNullOrEmpty( parameterValue.ProductLineId) )
                sql = "select * from SystemModuleParameterValue a where a.SYSTEMMODULEPARAMETER_Id =@SystemModuleId";

            var dbParaValue = DapperHelp.QueryForObject<SystemModuleParameterValue>(sql, new { SystemModuleId = parameterValue.SystemModuleParameter.Id, ProductLineId = parameterValue.ProductLineId });
            if (dbParaValue == null)
            {
                SystemModuleParameterValueDao.Instance.Save(parameterValue);
            }
            else
            {
                sql = @"update SystemModuleParameterValue  
                            set ParameterValue = @ParameterValue,
                                UpdateUser = @UpdateUser,
                                UpdateDateTime= GETDATE() where Id = @Id";

                DapperHelp.Execute(sql, new
                {
                    ParameterValue = parameterValue.ParameterValue,
                    UpdateUser = CurrentUser.Instance.UserName,
                    Id = dbParaValue.Id
                });
                //dbParaValue.SystemModuleParameter = DapperHelp.QueryForObject<SystemModuleParameter>(sql, new { Id = parameterValue.SystemModuleParameter.Id });
                //dbParaValue.ParameterValue = parameterValue.ParameterValue;
                //SystemModuleParameterValueDao.Instance.Update(dbParaValue);
            }
        }

        public List<PipeliningCache> CachePipelining()
        {
            string sql = @"select a.Id PipelingId,c.MainTrackNumber,c.StatingNo
                            from[Pipelining] a
                            left join Stating c on a.SITEGROUP_Id = c.SITEGROUP_Id
                            where a.Deleted = 0 and c.Id is not null";

            IEnumerable<PipeliningCache> cache = DapperHelp.Query<PipeliningCache>(sql);
            if (cache == null)
                cache = new List<PipeliningCache>();

            return cache.ToList();
        }

        public List<Domain.SystemModuleParameterModel> CacheAllSystemModuleParameter()
        {
            List<Domain.SystemModuleParameterModel> models = new List<Domain.SystemModuleParameterModel>();

            var dbModels = DapperHelp.Query<Domain.SystemModuleParameterModel>("select * from SystemModuleParameter");
            if (dbModels != null)
                models = dbModels.ToList();
            else
                throw new ArgumentNullException("未获取到系统参数");

            //获取系统参数下拉列表

            foreach (var item in models)
            {

                {
                    string sql = "select * from SystemModuleParameterValue where  SYSTEMMODULEPARAMETER_Id  = @ParameterId";
                    item.SystemModuleParameterValueList = DapperHelp.Query<SystemModuleParameterValue>(sql, new { ParameterId = item.Id });
                }

               
            }

            return models;
        }
    }
}
