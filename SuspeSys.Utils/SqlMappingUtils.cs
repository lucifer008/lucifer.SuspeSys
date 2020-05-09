using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils
{
    public class SqlMappingUtils<T>
    {
        private SqlMappingUtils() { }
        public static SqlMappingUtils<T> Instance { get { return new SqlMappingUtils<T>(); } }
        public string GetInsertSql(T t, out object pk)
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
            pk = GUIDHelper.GetGuidString();
            var sqlStringBuilder = new StringBuilder();
            sqlStringBuilder.AppendFormat("INSERT INTO {0}(id,", type.Name);
            foreach (var pi in pds)
            {
                if (!pi.Name.ToLower().Equals("id"))
                {
                    sqlStringBuilder.AppendFormat("{0},", pi.Name);
                }
            }
            sqlStringBuilder.Remove(sqlStringBuilder.Length - 1, 1);
            sqlStringBuilder.AppendFormat(")");
            sqlStringBuilder.AppendFormat(" VALUES('{0}',", pk);
            foreach (var pi in pds)
            {
                if (!pi.Name.ToLower().Equals("id"))
                {
                    if (null != pi.GetValue(t))
                    {
                        var isDate = pi.GetValue(t).GetType().Equals(typeof(DateTime));
                        if (isDate)
                        {
                            sqlStringBuilder.AppendFormat("{0},", pi.GetValue(t).FormatDateTime());
                            continue;
                        }
                    }
                    sqlStringBuilder.AppendFormat("{0},", pi.GetValue(t).FormatDBValue());
                }
            }
            sqlStringBuilder.Remove(sqlStringBuilder.Length - 1, 1);
            sqlStringBuilder.AppendFormat(")");
            return sqlStringBuilder.ToString();
        }

        public string GetUpdateSql(T t)
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
            foreach (var pi in pds)
            {
                if (pi.Name.ToLower().Equals("id"))
                {
                    if (builderWhere.Length > 0)
                        builderWhere.Append(" And ");

                    builderWhere.AppendFormat(" Id = '{0}'", pi.GetValue(t));

                }
                else
                {

                    if (null != pi.GetValue(t))
                    {
                        var isDate = pi.GetValue(t).GetType().Equals(typeof(DateTime));
                        if (isDate)
                        {
                            builderColumn.AppendFormat("{0}={1},", pi.Name, pi.GetValue(t).FormatDateTime());
                            continue;
                        }
                    }
                    //if (pi.GetValue(t) != null)
                    builderColumn.AppendFormat(" {0} = {1},", pi.Name, pi.GetValue(t).FormatDBValue());

                    //if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    //{
                    //    if (pi.PropertyType.GetGenericArguments().First().Name == "DateTime")
                    //    {
                    //        builderColumn.AppendFormat(" {0} = {1}", pi.Name, null);

                    //    }
                    //    else
                    //    {
                    //        builderColumn.AppendFormat(" {0} = '{1}'", pi.Name, pi.GetValue(t));

                    //    }
                    //}
                    //else
                    //{
                    //    builderColumn.AppendFormat(" {0} = '{1}'", pi.Name, pi.GetValue(t));

                    //}

                }
            }

            sqlStringBuilder.AppendFormat("UPDATE {0} SET {1} WHERE {2}", type.Name, builderColumn.ToString().Trim(','), builderWhere);

            return sqlStringBuilder.ToString();
        }
    }
}
