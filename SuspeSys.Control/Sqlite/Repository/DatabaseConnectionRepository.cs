﻿using SuspeSys.CustomerControls.Sqlite.Entity;
using SuspeSys.CustomerControls.Sqlite.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.CustomerControls.Sqlite.Repository
{
    public class DatabaseConnectionRepository : Repository<DatabaseConnection>
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
        const string connectionFormat = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
        public string GetDeafultConnectionStr()
        {
            var defaultDB = base.GetList().ToList().Where(o => o.IsDefault == true).FirstOrDefault();
            if (null == defaultDB) return "";
            string myConnect = string.Format(connectionFormat, defaultDB.ServerIP, defaultDB.DBName, defaultDB.UserId, defaultDB.Password);
            return myConnect;
        }
    }
}
