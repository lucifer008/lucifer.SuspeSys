using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SuspeSys.Domain.Base;
using SuspeSys.Domain.Common;
using SuspeSys.Utils;
using log4net;
using System.Reflection;
using SuspeSys.Domain.SusAttr;

namespace SuspeSys.Dao
{
    public class DapperHelp
    {
        public static string AppPath;

        #region 属性
        private static string _connectionString;
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    string path = string.Empty;
                    if (string.IsNullOrEmpty(AppPath))
                    {
                        path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Config/hibernate.cfg.xml");
                    }
                    else
                        path = string.Format("{0}\\Config\\hibernate.cfg.xml", AppPath);// System.IO.Path.Combine(AppPath, "\\Config\\hibernate.cfg.xml");

                    //LogManager.GetLogger(typeof(DapperHelp)).Info("path==="+ path);

                    Configuration conf = new Configuration();
                    conf = conf.Configure(path);
                    _connectionString = conf.Properties[NHibernate.Cfg.Environment.ConnectionString];
                }

                return _connectionString;
            }
        }

        private static string UserId
        {
            get
            {
                if (CurrentUser.Instance.User != null)
                    return CurrentUser.Instance.UserId;
                else
                    return string.Empty;
            }
        }
        #endregion

        #region Add
        public static string Add<T>(T obj)
        {
            Object id = null;
            if (obj is MetaData)
            {
                MetaData metaData = obj as MetaData;
                metaData.CompanyId = "c001";
                metaData.Deleted = 0;
                metaData.InsertDateTime = DateTime.Now;
                metaData.InsertUser = UserId;
            }

            var sql = GetInsertSql<T>(obj, out id);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Execute(sql, obj);
            }

            return id?.ToString();
        }
        #endregion

        #region Edit
        public static int Edit<T>(T obj)
        {
            if (obj is MetaData)
            {
                MetaData metaData = obj as MetaData;
                metaData.CompanyId = "c001";
                metaData.UpdateDateTime = DateTime.Now;
                metaData.UpdateUser = UserId;

                if (!metaData.Deleted.HasValue)
                    metaData.Deleted = 0;


            }

            //Stopwatch stopwatch1 = new Stopwatch();


            var sql = GetUpdateSql(obj);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(sql, obj);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static int Edit(string sql, object para = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(sql, para);
            }
        }

        #endregion

        #region Query
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T QueryForObject<T>(string sql, object obj)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.QueryFirstOrDefault<T>(sql, obj);
            }
        }
        #endregion

        #region Execute
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(string sql, object obj)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.ExecuteScalar<T>(sql, obj);
            }
        }

        /// <summary>
        /// 例如： SELECT * FROM t WHERE ID = @ID
        /// obj : new Object{ID = ID} 或者对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"> new Object{ID = ID}</param>
        /// <returns></returns>
        public static int Execute(string sql, object obj)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(sql, obj);
            }
        }
        #endregion

        #region Query
        /// <summary>
        /// 返回指定实体
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static IEnumerable<U> Query<U>(string sql, object para = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<U>(sql, para);
            }
        }
        /// <summary>
        /// 返回指定实体的List
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static IList<U> QueryForList<U>(string sql, object para = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<U>(sql, para).ToList<U>();
            }
        }
        /// <summary>
        /// 返回指定实体的List
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static IList<U> QueryForList1<U>(string connectionString,string sql, object para = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<U>(sql, para).ToList<U>();
            }
        }
        public static T FirstOrDefault<T>(string sql, object para)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.QueryFirstOrDefault<T>(sql, para);
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static Domain.PaginationResult<U> Paging<U>(Domain.Pagination pagination)
        {
            Domain.PaginationResult<U> result = new Domain.PaginationResult<U>();
            //计算Count

            result.Total = ExecuteScalar<int>(pagination.QueryCount, null);

            result.Data = QueryForList<U>(pagination.QueryString);

            return result;
        }
        #endregion


        #region SqlMap
        public static string GetInsertSql<T>(T t, out Object pk)
        {
            if (null == t)
            {
                var ex = new ApplicationException("实体不能未空!");
                throw ex;
            }
            Type type = t.GetType();
            PropertyInfo[] pds = type.GetProperties().Where(p => p.GetMethod.IsVirtual).ToArray();
            if (pds.Length == 0)
            {
                var ex = new ApplicationException("没有Virtual属性");
                throw ex;
            }

            StringBuilder columns = new StringBuilder();
            StringBuilder valuePara = new StringBuilder();

            foreach (var pi in pds)
            {
                if (columns.Length > 0)
                    columns.Append(",");

                if (valuePara.Length > 0)
                    valuePara.Append(",");

                columns.AppendFormat(pi.Name);

                valuePara.AppendFormat("@{0}", pi.Name);
                //if (!pi.Name.ToLower().Equals("id"))
                //{
                //    sqlStringBuilder.AppendFormat("{0},", pi.Name);
                //}
            }

            pk = GUIDHelper.GetGuidString();
            if (pds != null && pds.Any(o => o.Name.ToLower() == "id"))
            {
                var para = pds.Where(o => o.Name.ToLower() == "id").First();
                para.SetValue(t, pk);
            }


            var sqlStringBuilder = new StringBuilder();
            sqlStringBuilder.AppendFormat("INSERT INTO {0} ({1}) values ({2}) ", type.Name, columns, valuePara);


            return sqlStringBuilder.ToString();
        }

        public static string GetUpdateSql<T>(T t)
        {
            if (null == t)
            {
                var ex = new ApplicationException("实体不能未空!");
                throw ex;
            }
            Type type = t.GetType();
            PropertyInfo[] pds = type.GetProperties().Where(p => p.GetMethod.IsVirtual).ToArray();
            if (pds.Length == 0)
            {
                var ex = new ApplicationException("没有Virtual属性");
                throw ex;
            }

            var sqlStringBuilder = new StringBuilder();
            var builderWhere = new StringBuilder();
            var builderColumn = new StringBuilder();
            var keyList = new List<string>();
            foreach (var pi in pds)
            {
                //var isAttbute = false;
                var abutes = pi.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (null != abutes && abutes.Count() != 0)
                {
                    foreach (ColumnAttribute cBute in abutes)
                    {
                        if (!string.IsNullOrEmpty(cBute.PriKey))
                        {
                            if (builderWhere.Length > 0)
                                builderWhere.Append(" And ");
                            builderWhere.AppendFormat(" {0} = @{1}", cBute.PriKey, cBute.PriKey);
                           // isAttbute = true;
                            keyList.Add(cBute.PriKey);
                        }
                    }
                }
                //if (isAttbute)
                //{
                //    break;
                //}
                if (keyList.Contains(pi.Name)) continue;
                if (pi.Name.ToLower().Equals("id"))
                {
                    if (builderWhere.Length > 0)
                        builderWhere.Append(" And ");

                    builderWhere.Append(" Id = @Id");

                }
                else
                {
                    //if (pi.GetValue(t) != null)
                    builderColumn.AppendFormat("{0}=@{0},", pi.Name);
                }
            }

            sqlStringBuilder.AppendFormat("UPDATE {0} SET {1} WHERE {2}", type.Name, builderColumn.ToString().Trim(','), builderWhere);

            return sqlStringBuilder.ToString();
        }
        #endregion
    }
}
