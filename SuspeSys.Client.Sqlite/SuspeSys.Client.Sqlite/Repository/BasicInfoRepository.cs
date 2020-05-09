using SuspeSys.Client.Sqlite.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Sqlite.Repository
{
    public class BasicInfoRepository: Repository<BasicInfo>
    {
        private static BasicInfoRepository _BasicInfoRepository = new BasicInfoRepository();
        private BasicInfoRepository() { }
        public static BasicInfoRepository Instance
        {
            get
            {
                return _BasicInfoRepository;
            }
        }

        public void Save(BasicInfo basicInfo)
        {
            var dbModel = this.GetList().Where(o => basicInfo.Name == o.Name).FirstOrDefault();
            if (dbModel == null)
            {
                basicInfo.CreatedDate = DateTime.Now;
                this.Insert(basicInfo);
            }
            else
            {
                dbModel.Value = basicInfo.Value;
                this.Update(dbModel);
            }
        }

        public void Update(string value, BasicInfoEnum basicInfoEnum)
        {
            string sql = "UPDATE BasicInfo SET Value = @Value WHERE NAME = @Name";

            base.Execute(sql, new { Value = value, Name  = basicInfoEnum.ToString()});
        }

        //public void InsertOrUpdate(string value, BasicInfoEnum basicInfoEnum)
        //{
        //    string name = basicInfoEnum.ToString();
        //    string sql = "SELECT * FROM BasicInfo WHERE NAME = @Name ";
        //    var basicInfo = base.Execute(sql, new { Name = name });
        //    if (basicInfo == null)

        //}

        public void InsertOrUpdate(List<BasicInfo> list)
        {
            list.ForEach(o => {
                if (o.Id > 0)
                {
                    base.Update(o);
                }
                else
                {
                    base.Insert(o);
                }
            });
        }

        public void DeleteByName(BasicInfoEnum basicInfoEnum)
        {
            base.Execute("DELETE FROM BasicInfo WHERE NAME = @Name", new  { Name = basicInfoEnum.ToString() });
        }

        public string GetByName(BasicInfoEnum basicInfoEnum)
        {
            var dbModel = base.GetBySql("SELECT * FROM BasicInfo WHERE NAME = @Name", new { Name = basicInfoEnum.ToString() });

            if (dbModel == null)
                return string.Empty;
            else
                return dbModel.Value;
        }
       
    }
}
