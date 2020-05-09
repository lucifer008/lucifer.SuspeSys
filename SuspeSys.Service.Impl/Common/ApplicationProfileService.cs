using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Common
{
    /// <summary>
    /// 系統配置信息(系统初始化数据)
    /// </summary>
    public class ApplicationProfileService : IApplicationProfileService
    {

        public void AddOrUpdate(Domain.ApplicationProfile profile)
        {
            var dbProfile = Dao.DapperHelp.QueryForObject<ApplicationProfile>("SELECT * FROM ApplicationProfile WHERE name =@Name", new { Name = profile.Name});
            if (dbProfile == null)
            {
                profile.CreatedDate = DateTime.Now;
                profile.Id = SuspeSys.Utils.GUIDHelper.GetGuidString();
                string sql = @"INSERT INTO [dbo].[ApplicationProfile]
                               ([Id]
                               ,[Name]
                               ,[ParaValue]
                               ,[CreatedDate]
                               ,[Memo])
                         VALUES
                               (@Id
                               ,@Name
                               ,@ParaValue
                               ,@CreatedDate
                               ,@Memo)";
                Dao.DapperHelp.Execute(sql,profile);
            }
            else
            {
                string sql = @"UPDATE [ApplicationProfile]
                                  SET [ParaValue] = @ParaValue
                                WHERE Id = @Id";
                dbProfile.ParaValue = profile.ParaValue;

                Dao.DapperHelp.Execute(sql, profile);
            }
        }

        public void AddOrUpdate(List<Domain.ApplicationProfile> profile)
        {
            profile.ForEach((o) => {
                this.AddOrUpdate(o);
            });
        }

        /// <summary>
        /// 通过名称获取系统配置信息
        /// </summary>
        /// <param name="SysProfileEnum"></param>
        /// <returns></returns>
        public string GetByName(ApplicationProfileEnum SysProfileEnum)
        {
            string sql = "select ParaValue from ApplicationProfile where Name = @Name";

            return Dao.DapperHelp.ExecuteScalar<string>(sql, new { Name = SysProfileEnum.ToString() });
        }

        
    }
}
