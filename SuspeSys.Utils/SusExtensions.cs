using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils
{
    public static class SusExtensions
    {
        //public static string FormatDBValue(this string v) {
        //    if (string.IsNullOrEmpty(v))
        //    {
        //        return "NULL";
        //    }
        //    else {
        //        return string.Format("'{0}'",v);
        //    }
        //}
        public static string FormatDBValue(this object v)
        {
            if (null== v)
            {
                return "NULL";
            }
            else
            {
                return string.Format("'{0}'", v);
            }
        }
        public static string FormatDateTime(this object v) {
            var result = string.Empty;
            result = Convert.ToDateTime(v.ToString()).ToString("yyyy-MM-dd HH:mm:sss").FormatDBValue();
            return result;
        }
        
    }
}
