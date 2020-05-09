using log4net;
using NHibernate;
using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain.SusAttr;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Base
{
    /// <summary>
    /// 服务层基类
    /// </summary>
    public abstract class ServiceBase : MarshalByRefObject
    {
        protected static readonly ILog log = LogManager.GetLogger("LogLogger");
        protected static readonly ILog tcpLogInfo = LogManager.GetLogger("TcpLogInfo");
        protected static readonly ILog tcpLogError = LogManager.GetLogger("TcpErrorInfo");
        protected static readonly ILog tcpLogHardware = LogManager.GetLogger("TcpHardwareInfo");
        protected static readonly ILog errorLog = LogManager.GetLogger("Error");
        protected static readonly ILog timersLog = LogManager.GetLogger("Timers");
        protected static readonly ILog redisLog = LogManager.GetLogger("RedisLogInfo");
        protected static readonly ILog cacheInfo = LogManager.GetLogger("CacheInfo");
        protected static readonly ILog montorLog = LogManager.GetLogger("MontorLogger");

        #region Hibernate SQL
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">sql检索字段必须与T属性对应，且大小写一致</typeparam>
        /// <param name="sql">查询sql,不包含order by 字句</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">每页显示的行数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="hashCondition">查询条件</param>
        /// <param name="orderCondition">排序条件</param>
        /// <param name="transformer">是否映射到自定义T,该T不与Table直接对应</param>
        /// <returns></returns>
        public IList<T> Query<T>(StringBuilder sql, int pageIndex, int pageSize, out long totalCount, IDictionary<string, string> hashCondition, IDictionary<string, string> orderCondition, bool transformer = false)
        {
            //Type t = typeof(T);
            var sqlPage = new StringBuilder();
            var sqlTotal = new StringBuilder();
            var orderString = new StringBuilder();
            var conditionString = new StringBuilder();
            if (null == orderCondition || orderCondition.Keys.Count == 0)
            {
                orderString.AppendFormat("ORDER BY Id");
            }
            else
            {
                orderString.Append("ORDER BY ");
                foreach (var key in orderCondition.Keys)
                {
                    orderString.AppendFormat("{0} {1},", key, orderCondition[key]);
                }
                orderString = orderString.Remove(orderString.ToString().Length - 1, 1);
            }
            sql.AppendFormat(" where 1=1 ");

            foreach (var key in hashCondition.Keys)
            {
                sql.AppendFormat(" AND {0} {1}", key, hashCondition[key]);
                //conditionString.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
            }

            sqlTotal.AppendFormat("select count(*) from ({0}) T", sql.ToString());

            sqlPage.AppendFormat(@"SELECT TOP {0} * FROM ({1})T1 WHERE T1.ID NOT IN(
SELECT TOP ({0}*({2}-1)) ID FROM ({1})T2  {3}
) {3}", pageSize, sql.ToString(), pageIndex, orderString.ToString());


            //string.Format("select * from {0}", t.Name);
            using (ISession session = SessionFactory.OpenSession())
            {
                var listCount = session.CreateSQLQuery(sqlTotal.ToString()).UniqueResult();
                totalCount = Convert.ToInt64(listCount);
                if (transformer)
                {
                    var listTran = session.CreateSQLQuery(sqlPage.ToString()).SetResultTransformer(SuspeSys.Dao.Nhibernate.Suspe.Transformers.AliasToBean(typeof(T))).List<T>();
                    return listTran;
                }
                var list = session.CreateSQLQuery(sqlPage.ToString()).AddEntity(typeof(T)).List<T>();
                return list;
            }
            //return new List<T>();
        }

        //        /// <summary>
        //        /// 分页查询(占位符号查询)
        //        /// 样例：select * from t1 where name=?
        //        /// </summary>
        //        /// <typeparam name="T">sql检索字段必须与T属性对应，且大小写一致</typeparam>
        //        /// <param name="sql">占位符号查询sql,不包含order by 字句</param>
        //        /// <param name="pageIndex">当前第几页</param>
        //        /// <param name="pageSize">每页显示的行数</param>
        //        /// <param name="totalCount">总记录数</param>
        //        /// <param name="hashCondition">查询条件</param>
        //        /// <param name="orderCondition">排序条件</param>
        //        /// <param name="transformer">是否映射到自定义T,该T不与Table直接对应</param>
        //        /// <returns></returns>
        //        public IList<T> Query<T>(StringBuilder sql, int pageIndex, int pageSize, out long totalCount, Hashtable hashCondition, Hashtable orderCondition, bool transformer = false)
        //        {
        //            //Type t = typeof(T);
        //            var sqlPage = new StringBuilder();
        //            var sqlTotal = new StringBuilder();
        //            var orderString = new StringBuilder();
        //            var conditionString = new StringBuilder();
        //            if (null == orderCondition || orderCondition.Keys.Count == 0)
        //            {
        //                orderString.AppendFormat("ORDER BY Id");
        //            }
        //            else
        //            {
        //                orderString.Append("ORDER BY ");
        //                foreach (var key in orderCondition.Keys)
        //                {
        //                    orderString.AppendFormat("{0} {1},", key, orderCondition[key]);
        //                }
        //                orderString = orderString.Remove(orderString.ToString().Length - 1, 1);
        //            }
        //            sql.AppendFormat(" where 1=1 ");

        //            //foreach (var key in hashCondition.Keys)
        //            //{
        //            //    sql.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
        //            //    //conditionString.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
        //            //}

        //            sqlTotal.AppendFormat("select count(*) from ({0}) T", sql.ToString());

        //            sqlPage.AppendFormat(@"SELECT TOP {0} * FROM ({1})T1 WHERE T1.ID NOT IN(
        //SELECT TOP ({0}*({2}-1)) ID FROM ({1})T2  {3}
        //) {3}", pageSize, sql.ToString(), pageIndex, orderString.ToString());


        //            //string.Format("select * from {0}", t.Name);
        //            using (ISession session = SessionFactory.OpenSession())
        //            {
        //                var sQueryTotal = session.CreateSQLQuery(sqlTotal.ToString()); ;
        //                foreach (var key in hashCondition.Keys)
        //                {
        //                    var v = hashCondition[key];
        //                    if (v is System.String || v is string)
        //                    {
        //                        sQueryTotal.SetString(Convert.ToString(key), Convert.ToString(v));
        //                    }
        //                    if (v is System.Int16)
        //                    {
        //                        sQueryTotal.SetInt16(Convert.ToString(key), Convert.ToInt16(v));
        //                    }
        //                    if (v is System.Int32)
        //                    {
        //                        sQueryTotal.SetInt32(Convert.ToString(key), Convert.ToInt32(v));
        //                    }
        //                    if (v is System.Int64)
        //                    {
        //                        sQueryTotal.SetInt64(Convert.ToString(key), Convert.ToInt64(v));
        //                    }
        //                    if (v is Guid)
        //                    {
        //                        sQueryTotal.SetGuid(Convert.ToString(key), Guid.Parse(Convert.ToString(v)));
        //                    }
        //                    if (v is DateTime)
        //                    {
        //                        sQueryTotal.SetTime(Convert.ToString(key), Convert.ToDateTime(v));
        //                    }
        //                    if (v is DateTime)
        //                    {
        //                        sQueryTotal.SetBoolean(Convert.ToString(key), Convert.ToBoolean(v));
        //                    }
        //                    if (v is Byte)
        //                    {
        //                        sQueryTotal.SetByte(Convert.ToString(key), Convert.ToByte(v));
        //                    }
        //                    if (v is Double)
        //                    {
        //                        sQueryTotal.SetDouble(Convert.ToString(key), Convert.ToDouble(v));
        //                    }
        //                    if (v is Decimal)
        //                    {
        //                        sQueryTotal.SetDecimal(Convert.ToString(key), Convert.ToDecimal(v));
        //                    }
        //                    if (v is Enum)
        //                    {
        //                        sQueryTotal.SetEnum(Convert.ToString(key), v as Enum);
        //                    }
        //                }

        //                var listCount = session.CreateSQLQuery(sqlTotal.ToString()).UniqueResult();
        //                totalCount = Convert.ToInt64(listCount);
        //                var sQueryData = session.CreateSQLQuery(sqlPage.ToString());
        //                foreach (var key in hashCondition.Keys)
        //                {
        //                    var v = hashCondition[key];
        //                    if (v is System.String || v is string)
        //                    {
        //                        sQueryData.SetString(Convert.ToString(key), Convert.ToString(v));
        //                    }
        //                    if (v is System.Int16)
        //                    {
        //                        sQueryData.SetInt16(Convert.ToString(key), Convert.ToInt16(v));
        //                    }
        //                    if (v is System.Int32)
        //                    {
        //                        sQueryData.SetInt32(Convert.ToString(key), Convert.ToInt32(v));
        //                    }
        //                    if (v is System.Int64)
        //                    {
        //                        sQueryData.SetInt64(Convert.ToString(key), Convert.ToInt64(v));
        //                    }
        //                    if (v is Guid)
        //                    {
        //                        sQueryData.SetGuid(Convert.ToString(key), Guid.Parse(Convert.ToString(v)));
        //                    }
        //                    if (v is DateTime)
        //                    {
        //                        sQueryData.SetTime(Convert.ToString(key), Convert.ToDateTime(v));
        //                    }
        //                    if (v is DateTime)
        //                    {
        //                        sQueryData.SetBoolean(Convert.ToString(key), Convert.ToBoolean(v));
        //                    }
        //                    if (v is Byte)
        //                    {
        //                        sQueryData.SetByte(Convert.ToString(key), Convert.ToByte(v));
        //                    }
        //                    if (v is Double)
        //                    {
        //                        sQueryData.SetDouble(Convert.ToString(key), Convert.ToDouble(v));
        //                    }
        //                    if (v is Decimal)
        //                    {
        //                        sQueryData.SetDecimal(Convert.ToString(key), Convert.ToDecimal(v));
        //                    }
        //                    if (v is Enum)
        //                    {
        //                        sQueryData.SetEnum(Convert.ToString(key), v as Enum);
        //                    }
        //                }
        //                if (transformer)
        //                {
        //                    var listTran = sQueryData.SetResultTransformer(new Dao.Nhibernate.BeanTransformerAdapter<T>()).List<T>();
        //                    return listTran;
        //                }
        //                var list = sQueryData.AddEntity(typeof(T)).List<T>();
        //                return list;
        //            }
        //            //return new List<T>();
        //        }

        /// <summary>
        /// 分页查询(占位符号查询)
        /// 样例：select * from t1 where name=?
        /// </summary>
        /// <typeparam name="T">sql检索字段必须与T属性对应，且大小写一致</typeparam>
        /// <param name="sql">占位符号查询sql,不包含order by 字句</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">每页显示的行数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="orderCondition">排序条件</param>
        /// <param name="transformer">是否映射到自定义T,该T不与Table直接对应</param>
        /// <param name="values">查询条件值列表</param>
        /// <returns></returns>
        public IList<T> Query<T>(StringBuilder sql, int pageIndex, int pageSize, out long totalCount, IDictionary<string, string> orderCondition, bool transformer = false, params Object[] values)
        {
            //Type t = typeof(T);
            var sqlPage = new StringBuilder();
            var sqlTotal = new StringBuilder();
            var orderString = new StringBuilder();
            var conditionString = new StringBuilder();
            if (null == orderCondition || orderCondition.Keys.Count == 0)
            {
                orderString.AppendFormat("ORDER BY Id");
            }
            else
            {
                orderString.Append("ORDER BY ");
                foreach (var key in orderCondition.Keys)
                {
                    orderString.AppendFormat("{0} {1},", key, orderCondition[key]);
                }
                orderString = orderString.Remove(orderString.ToString().Length - 1, 1);
            }
            //  sql.AppendFormat(" where 1=1 ");

            //foreach (var key in hashCondition.Keys)
            //{
            //    sql.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
            //    //conditionString.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
            //}

            sqlTotal.AppendFormat("select count(*) from ({0}) T", sql.ToString());

            sqlPage.AppendFormat(@"SELECT TOP {0} * FROM ({1})T1 WHERE T1.ID NOT IN(
SELECT TOP ({0}*({2}-1)) ID FROM ({1})T2  {3}
) {3}", pageSize, sql.ToString(), pageIndex, orderString.ToString());


            //string.Format("select * from {0}", t.Name);
            using (ISession session = SessionFactory.OpenSession())
            {
                var sQueryTotal = session.CreateSQLQuery(sqlTotal.ToString());
                var sQueryData = session.CreateSQLQuery(sqlPage.ToString());
                if (null != values)
                {
                    for (var index = 0; index < values.Length; index++)
                    {
                        var v = values[index];
                        if (null != v)
                        {
                            sQueryTotal.SetParameter(index, values[index]);
                            sQueryData.SetParameter(index, values[index]);
                        }
                    }
                    //分页参数加倍，多赋值一次
                    var j = values.Length;
                    for (var index = 0; index < values.Length; index++, j++)
                    {
                        var v = values[index];
                        if (null != v)
                        {
                            // sQueryTotal.SetParameter(j, values[index]);
                            sQueryData.SetParameter(j, values[index]);
                        }
                    }
                }
                var listCount = sQueryTotal.UniqueResult();
                totalCount = Convert.ToInt64(listCount);


                if (transformer)
                {
                    var listTran = sQueryData.SetResultTransformer(new Dao.Nhibernate.BeanTransformerAdapter<T>()).List<T>();
                    return listTran;
                }
                var list = sQueryData.AddEntity(typeof(T)).List<T>();
                return list;
            }
            //return new List<T>();
        }

        /// <summary>
        /// 查询(占位符号查询)
        /// 样例：select * from t1 where name=?
        /// </summary>
        /// <typeparam name="T">sql检索字段必须与T属性对应，且大小写一致</typeparam>
        /// <param name="sql">占位符号查询sql,不包含order by 字句</param>
        /// <param name="orderCondition">排序条件</param>
        /// <param name="transformer">是否映射到自定义T,该T不与Table直接对应</param>
        /// <param name="values">查询条件值列表</param>
        /// <returns></returns>
        public IList<T> Query<T>(StringBuilder sql, IDictionary<string, string> orderCondition = null, bool transformer = false, params Object[] values)
        {
            //Type t = typeof(T);
            var sqlData = new StringBuilder();

            var orderString = new StringBuilder();
            var conditionString = new StringBuilder();
            if (null == orderCondition || orderCondition.Count == 0)
            {
                //orderString.AppendFormat("ORDER BY Id");
            }
            else
            {
                orderString.Append("ORDER BY ");
                foreach (var de in orderCondition)
                {
                    orderString.AppendFormat("{0} {1},", de.Key, de.Value);
                }
                orderString = orderString.Remove(orderString.ToString().Length - 1, 1);
            }
            //  sql.AppendFormat(" where 1=1 ");

            //foreach (var key in hashCondition.Keys)
            //{
            //    sql.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
            //    //conditionString.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
            //}

            sqlData.AppendFormat("select * from ({0}) T  {1}", sql.ToString(), orderString.ToString());
            //string.Format("select * from {0}", t.Name);
            using (ISession session = SessionFactory.OpenSession())
            {
                var sQueryData = session.CreateSQLQuery(sqlData.ToString());
                if (null != values)
                {
                    for (var index = 0; index < values.Length; index++)
                    {
                        var v = values[index];
                        var vType = v.GetType();
                        var vDictory = typeof(Dictionary<,>);
                        if (vType.IsGenericType && vType.GetGenericTypeDefinition() == vDictory)
                        {
                            var dic = v as Dictionary<string, string>;
                            if (null != dic)
                            {
                                foreach (var key in dic.Keys)
                                {
                                    sQueryData.SetParameter(key, dic[key]);
                                }
                            }
                            if (null == dic)
                            {
                                throw new Exception("参数必须为Dictionary<string, string> 类型!");
                            }
                        }
                        else
                        {
                            sQueryData.SetParameter(index, v);
                        }
                    }
                }
                if (transformer)
                {
                    var listTran = sQueryData.SetResultTransformer(new Dao.Nhibernate.BeanTransformerAdapter<T>()).List<T>();
                    return listTran;
                }
                var list = sQueryData.AddEntity(typeof(T)).List<T>();
                return list;
            }
            //return new List<T>();
        }

        /// <summary>
        /// 查询(占位符号查询)
        /// 样例：select * from t1 where name=?
        /// </summary>
        /// <typeparam name="T">sql检索字段必须与T属性对应，且大小写一致</typeparam>
        /// <param name="sql">占位符号查询sql,不包含order by 字句</param>
        /// <param name="orderCondition">排序条件</param>
        /// <param name="transformer">是否映射到自定义T,该T不与Table直接对应</param>
        /// <param name="values">查询条件值列表</param>
        /// <returns></returns>
        public IList<T> Query<T>(string sql, bool transformer = false, params Object[] values)
        {
            return this.Query<T>(new StringBuilder(sql), null, transformer, values);
        }


        /// <summary>
        /// 查询(占位符号查询)
        /// 样例：select * from t1 where name=?
        /// </summary>
        /// <typeparam name="T">sql检索字段必须与T属性对应，且大小写一致</typeparam>
        /// <param name="sql">占位符号查询sql,不包含order by 字句</param>
        /// <param name="orderCondition">排序条件</param>
        /// <param name="transformer">是否映射到自定义T,该T不与Table直接对应</param>
        /// <param name="values">查询条件值列表</param>
        /// <returns></returns>
        public T QueryForObject<T>(StringBuilder sql, IDictionary<string, string> orderCondition = null, bool transformer = false, params Object[] values)
        {
            //Type t = typeof(T);
            var sqlData = new StringBuilder();

            var orderString = new StringBuilder();
            var conditionString = new StringBuilder();
            if (null == orderCondition || orderCondition.Count == 0)
            {
                //orderString.AppendFormat("ORDER BY Id");
            }
            else
            {
                orderString.Append("ORDER BY ");
                foreach (var de in orderCondition)
                {
                    orderString.AppendFormat("{0} {1},", de.Key, de.Value);
                }
                orderString = orderString.Remove(orderString.ToString().Length - 1, 1);
            }
            //  sql.AppendFormat(" where 1=1 ");

            //foreach (var key in hashCondition.Keys)
            //{
            //    sql.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
            //    //conditionString.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
            //}

            sqlData.AppendFormat("select top 1 * from ({0}) T  {1}", sql.ToString(), orderString.ToString());
            //string.Format("select * from {0}", t.Name);
            using (ISession session = SessionFactory.OpenSession())
            {
                var sQueryData = session.CreateSQLQuery(sqlData.ToString());
                if (null != values)
                {
                    for (var index = 0; index < values.Length; index++)
                    {
                        var v = values[index];
                        var vType = v.GetType();
                        var vDictory = typeof(Dictionary<,>);
                        if (vType.IsGenericType && vType.GetGenericTypeDefinition() == vDictory)
                        {
                            var dic = v as Dictionary<string, string>;
                            if (null != dic)
                            {
                                foreach (var key in dic.Keys)
                                {
                                    sQueryData.SetParameter(key, dic[key]);
                                }
                            }
                            if (null == dic)
                            {
                                throw new Exception("参数必须为Dictionary<string, string> 类型!");
                            }
                        }
                        else
                        {
                            sQueryData.SetParameter(index, v);
                        }
                    }
                }
                if (transformer)
                {
                    var listTran = sQueryData.SetResultTransformer(new Dao.Nhibernate.BeanTransformerAdapter<T>()).UniqueResult<T>();
                    return listTran;
                }
                var list = sQueryData.AddEntity(typeof(T)).UniqueResult<T>();
                return list;
            }
            //return new List<T>();
        }
        /// <summary>
        /// 参数化处理
        /// </summary>
        /// <param name="command"></param>
        /// <param name="items"></param>
        private void HandleDbCommand(IDbCommand command, params DbParameterItem[] items)
        {
            if (items == null || items.Length <= 0)
            {
                return;
            }
            if (command is SqlCommand)
            {
                foreach (DbParameterItem item in items)
                {
                    if (item != null)
                    {
                        if (!command.CommandText.Contains("@" + item.Name))
                            continue;

                        SqlParameter para = new SqlParameter(item.Name, item.DataType);
                        para.Value = item.Value;
                        command.Parameters.Add(para);
                    }
                }
            }

        }
        /// <summary>
        /// 原生态查询
        /// 样例：select * from t1 where name=?
        /// </summary>
        /// <typeparam name="T">sql检索字段必须与T属性对应，且大小写一致</typeparam>
        /// <param name="sql">占位符号查询sql,不包含order by 字句</param>
        /// <param name="orderCondition">排序条件</param>
        /// <param name="transformer">是否映射到自定义T,该T不与Table直接对应</param>
        /// <param name="values">查询条件值列表</param>
        /// <returns></returns>
        public T QueryForObjectNative<T>(string sql, IDictionary<string, string> values)

        {
            DataTable result = new DataTable();
            IDbCommand command = null;
            try
            {
                var itemsList = new List<DbParameterItem>();
                if (null != values)
                {
                    //for (var index = 0; index < values.Length; index++)
                    //{
                    //    var v = values[index];
                    //    var vType = v.GetType();
                    //    var vDictory = typeof(Dictionary<,>);
                    //    if (vType.IsGenericType && vType.GetGenericTypeDefinition() == vDictory)
                    //    {

                    //    }
                    //}
                    foreach (var key in values.Keys)
                    {
                        itemsList.Add(new DbParameterItem() { Name =  key, Value = values[key] });
                    }
                }
                command = SessionFactory.OpenSession().Connection.CreateCommand();
                command.CommandText = string.Format("select top 1 * from({0})T", sql.ToString());

                HandleDbCommand(command, itemsList.ToArray());//参数化处理

                if (command is SqlCommand)
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command as SqlCommand);
                    dataAdapter.Fill(result);
                }
                if (result.Rows.Count == 0)
                {
                    return default(T);
                }
                DataRow dr = result.Rows[0];
                T t = System.Activator.CreateInstance<T>();
                var type = typeof(T);
                if (typeof(T).IsValueType)
                {
                    return t = (T)dr[0];
                }
                PropertyInfo[] pds = type.GetProperties().Where(p => p.GetMethod.IsVirtual).ToArray();
                
               
                foreach (DataColumn dc in result.Columns)
                {
                    foreach (var pInfo in pds)
                    {
                        if (dc.ColumnName.ToLower().Replace("_", "").Trim().Equals(pInfo.Name.ToLower().Replace("_", "").Trim()))
                        {
                            pInfo.SetValue(t, dr[dc.ColumnName]);
                            break;
                        }
                    }
                }
                return t;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    //command.Connection.Close();
                    command.Dispose();
                    command.Parameters.Clear();
                }
            }



            ////Type t = typeof(T);
            //var sqlData = new StringBuilder();

            //    var orderString = new StringBuilder();
            //    var conditionString = new StringBuilder();
            //    if (null == orderCondition || orderCondition.Count == 0)
            //    {
            //        //orderString.AppendFormat("ORDER BY Id");
            //    }
            //    else
            //    {
            //        orderString.Append("ORDER BY ");
            //        foreach (var de in orderCondition)
            //        {
            //            orderString.AppendFormat("{0} {1},", de.Key, de.Value);
            //        }
            //        orderString = orderString.Remove(orderString.ToString().Length - 1, 1);
            //    }
            //    //  sql.AppendFormat(" where 1=1 ");

            //    //foreach (var key in hashCondition.Keys)
            //    //{
            //    //    sql.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
            //    //    //conditionString.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
            //    //}

            //    sqlData.AppendFormat("select top 1 * from ({0}) T  {1}", sql.ToString(), orderString.ToString());
            //    //string.Format("select * from {0}", t.Name);
            //    using (ISession session = SessionFactory.OpenSession())
            //    {
            //        var sQueryData = session.CreateQuery(sqlData.ToString());
            //        if (null != values)
            //        {
            //            for (var index = 0; index < values.Length; index++)
            //            {
            //                var v = values[index];
            //                var vType = v.GetType();
            //                var vDictory = typeof(Dictionary<,>);
            //                if (vType.IsGenericType && vType.GetGenericTypeDefinition() == vDictory)
            //                {
            //                    var dic = v as Dictionary<string, string>;
            //                    if (null != dic)
            //                    {
            //                        foreach (var key in dic.Keys)
            //                        {
            //                            sQueryData.SetParameter(key, dic[key]);
            //                        }
            //                    }
            //                    if (null == dic)
            //                    {
            //                        throw new Exception("参数必须为Dictionary<string, string> 类型!");
            //                    }
            //                }
            //                else
            //                {
            //                    sQueryData.SetParameter(index, v);
            //                }
            //            }
            //        }
            //        if (transformer)
            //        {
            //            var listTran = sQueryData.SetResultTransformer(new Dao.Nhibernate.BeanTransformerAdapter<T>()).UniqueResult<T>();
            //            return listTran;
            //        }
            //        var list = sQueryData.UniqueResult<T>();
            //        return list;
            //    }
            //return new List<T>();
        }

        /// <summary>
        /// 查询(占位符号查询)
        /// 样例：select * from t1 where name=?
        /// </summary>
        /// <typeparam name="T">sql检索字段必须与T属性对应，且大小写一致</typeparam>
        /// <param name="sql">占位符号查询sql,不包含order by 字句</param>
        /// <param name="orderCondition">排序条件</param>
        /// <param name="transformer">是否映射到自定义T,该T不与Table直接对应</param>
        /// <param name="values">查询条件值列表</param>
        /// <returns></returns>
        public T QueryForObject<T>(string sql, bool transformer = false, params Object[] values)
        {
            //Type t = typeof(T);
            var sqlData = new StringBuilder();


            sqlData.AppendFormat(sql.ToString());
            //string.Format("select * from {0}", t.Name);
            using (ISession session = SessionFactory.OpenSession())
            {
                var sQueryData = session.CreateSQLQuery(sqlData.ToString());
                if (null != values)
                {
                    for (var index = 0; index < values.Length; index++)
                    {
                        var v = values[index];
                        var vType = v.GetType();
                        var vDictory = typeof(Dictionary<,>);
                        if (vType.IsGenericType && vType.GetGenericTypeDefinition() == vDictory)
                        {
                            var dic = v as Dictionary<string, string>;
                            if (null != dic)
                            {
                                foreach (var key in dic.Keys)
                                {
                                    sQueryData.SetParameter(key, dic[key]);
                                }
                            }
                            if (null == dic)
                            {
                                throw new Exception("参数必须为Dictionary<string, string> 类型!");
                            }
                        }
                        else
                        {
                            sQueryData.SetParameter(index, v);
                        }
                    }
                }
                if (transformer)
                {
                    var listTran = sQueryData.SetResultTransformer(new Dao.Nhibernate.BeanTransformerAdapter<T>()).UniqueResult<T>();
                    return listTran;
                }
                var list = sQueryData.AddEntity(typeof(T)).UniqueResult<T>();
                return list;
            }
            //return new List<T>();
        }
        /// <summary>
        /// 执行 update,delete 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        public int ExecuteUpdate(string sql, params object[] values)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                var sqlQuery = session.CreateSQLQuery(sql);
                if (null != values)
                {
                    for (var index = 0; index < values.Length; index++)
                    {
                        var v = values[index];
                        var vType = v.GetType();
                        var vDictory = typeof(Dictionary<,>);
                        if (vType.IsGenericType && vType.GetGenericTypeDefinition() == vDictory)
                        {
                            var dic = v as Dictionary<string, string>;
                            if (null != dic)
                            {
                                foreach (var key in dic.Keys)
                                {
                                    sqlQuery.SetParameter(key, dic[key]);
                                }
                            }
                            if (null == dic)
                            {
                                throw new Exception("参数必须为Dictionary<string, string> 类型!");
                            }
                        }
                        else
                        {
                            sqlQuery.SetParameter(index, v);
                        }
                    }
                }
                return sqlQuery.ExecuteUpdate();
            }
        }

        /// <summary>
        /// 子查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">样例 select * from table where id in(:id) </param>
        /// <param name="dicParamterKeysAndValues">目前只支持List，其他集合类型待扩展</param>
        /// <returns></returns>
        public IList<T> QueryIn<T>(string sql, IDictionary<string, object> dicParamterKeysAndValues)
        {
            ISession session = SessionFactory.OpenSession();
            var sqlQuery = session.CreateSQLQuery(sql);
            foreach (var k in dicParamterKeysAndValues.Keys)
            {
                var v = dicParamterKeysAndValues[k];
                var vType = v.GetType();
                if (vType.IsGenericType && vType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    sqlQuery.SetParameterList(k, v as List<string>);
                }
                else
                {
                    sqlQuery.SetString(k, dicParamterKeysAndValues[k].ToString());
                }
            }
            return sqlQuery.SetResultTransformer(new BeanTransformerAdapter<T>()).List<T>();
        }
        /// <summary>
        /// 子查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">样例 select * from table where id in(:id) </param>
        /// <param name="dicParamterKeysAndValues">目前只支持List，其他集合类型待扩展</param>
        /// <returns></returns>
        public T QueryForObjectIn<T>(string sql, IDictionary<string, object> dicParamterKeysAndValues)
        {
            ISession session = SessionFactory.OpenSession();
            var sqlQuery = session.CreateSQLQuery(sql);
            foreach (var k in dicParamterKeysAndValues.Keys)
            {
                var v = dicParamterKeysAndValues[k];
                var vType = v.GetType();
                if (vType.IsGenericType && vType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    sqlQuery.SetParameterList(k, v as List<string>);
                }
                else
                {
                    sqlQuery.SetString(k, dicParamterKeysAndValues[k].ToString());
                }
            }
            return sqlQuery.SetResultTransformer(new BeanTransformerAdapter<T>()).UniqueResult<T>();
        }

        #endregion

        #region Dapper Query
        /// <summary>
        /// Dapper 查询 Object
        ///  写法参见:GetProcessFlowChartListByOnlineProducts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T QueryForObject<T>(string sql, object obj=null)
        {
            return DapperHelp.QueryForObject<T>(sql, obj);
        }
        /// <summary>
        /// Dapper 查询 List
        ///  写法参见:GetProcessFlowChartListByOnlineProducts
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public  IList<T> QueryForList<T>(string sql, object para = null)
        {
            return DapperHelp.QueryForList<T>(sql, para);
        }
        /// <summary>
        /// Dapper 分页查询 List
        /// 写法参见:GetProcessFlowChartListByOnlineProducts
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public IList<T> QueryForList<T>(string sql, int pageIndex, int pageSize, out long totalCount, object para = null,string orderString=null)
        {
            var priKey = GetPrikeyColumn<T>();
            if (string.IsNullOrEmpty(priKey)) {
                priKey = "Id";
            }
            var sqlRealyPage=string.Format(@"SELECT TOP {0} * FROM ({1})T1 WHERE T1.{4} NOT IN(
SELECT TOP ({0}*({2}-1)) {4} FROM ({1})T2  {3}
) {3}", pageSize, sql, pageIndex, orderString, priKey);
            var sqlTotal = string.Format(@"select count(1) from({0})Res",sql);
            totalCount = DapperHelp.Query<int>(sqlTotal, para).Single();
            return DapperHelp.QueryForList<T>(sqlRealyPage, para);
        }
        private String GetPrikeyColumn<T>() {
            Type type = typeof(T);
            PropertyInfo[] pds = type.GetProperties().Where(p => p.GetMethod.IsVirtual).ToArray();
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
                           if(!keyList.Contains(cBute.PriKey))
                             keyList.Add(cBute.PriKey);
                        }
                    }
                }
            }
            if (keyList.Count>1) {
                throw new ApplicationException("主键超过2列");
            }
            if (keyList.Count == 1) {
                return keyList[0];
            }
            return null;
        }
        /// <summary>
        /// Dapper Update
        ///  写法参见:GetProcessFlowChartListByOnlineProducts
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public int Update<T>(string sql, object para = null)
        {
            return DapperHelp.Execute(sql, para);
        }
        #endregion
    }
    class DbParameterItem
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public SqlDbType DataType { get; set; }

        public DbParameterItem() { }

        public DbParameterItem(string name, SqlDbType datatype, object value)
        {
            this.Name = name;
            this.DataType = datatype;
            this.Value = value;
        }
    }
}
