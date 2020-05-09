using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Dao.Nhibernate
{
    /// <summary>
    /// SQL拦截器
    /// </summary>
    public class SQLWatcher: EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
          //  System.Diagnostics.Debug.WriteLine("sql语句:" + sql,"sql_logger");
            //Console.WriteLine("sql语句:" + sql);
            return base.OnPrepareStatement(sql);
        }
    }
}
