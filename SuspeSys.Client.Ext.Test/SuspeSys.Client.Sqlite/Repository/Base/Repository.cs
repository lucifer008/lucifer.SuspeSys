using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Sqlite.Repository
{
    /// <summary>
    /// 仓储层基类，通过泛型实现通用的CRUD操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        public virtual IEnumerable<T> GetList()
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.GetList<T>().ToList();
            }

        }

        public virtual T GetBySql(string sql,object para)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Query<T>(sql, para).FirstOrDefault();
            }
        }

        public virtual T Get(object id)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Get<T>(id);
            }
        }

        public virtual bool Update(T t)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Update(t);
            }
        }

        public virtual T Insert(T apply)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                conn.Insert(apply);
                return apply;
            }
        }

        public virtual bool Delete(T t)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Delete(t);
            }
        }

        public virtual bool Execute(string sql, object para)
        {
            using (var conn = ConnectionFactory.CreateSqlConnection())
            {
                return conn.Execute(sql, para) > 0;
            }
        }

    }
}
