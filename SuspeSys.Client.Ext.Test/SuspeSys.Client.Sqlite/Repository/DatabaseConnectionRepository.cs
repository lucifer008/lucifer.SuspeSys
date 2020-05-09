using SuspeSys.Client.Sqlite.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Sqlite.Repository
{
    public class DatabaseConnectionRepository:Repository<DatabaseConnection>
    {
        private static DatabaseConnectionRepository _BasicInfoRepository = new DatabaseConnectionRepository();
        private DatabaseConnectionRepository() { }
        public static DatabaseConnectionRepository Instance
        {
            get
            {
                return _BasicInfoRepository;
            }
        }

        public void InsertOrUpdate(List<DatabaseConnection> list)
        {
            list.ForEach(o => {
                if (o.Id > 0)
                {
                    base.Update(o);
                }
                else
                {
                    o.CreatedDate = DateTime.Now;
                    o.EnableTime = DateTime.Now.AddYears(10);
                    base.Insert(o);
                }
            });
        }

        public DatabaseConnection GetDeafult()
        {
            
            return base.GetList().ToList().Where(o => o.IsDefault == true).FirstOrDefault();
        }

    }
}
