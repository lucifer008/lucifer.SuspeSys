using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils
{
   public static class ExtUtils
    {
        /// <summary>
        /// 格式化DB串
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string FormatDBValue(this string v)
        {
            if (string.IsNullOrEmpty(v)) {
                return "NULL";
            }
            return string.Format("'{0}'",v);
        }

        /// <summary>
        /// 十进制转16进制字符串
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string TenToHexString(this int v)
        {
            return v.ToString("X2");
        }

        /// <summary>
        /// 十进制转16进制字符串
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string TenToHexString(this string v)
        {
            return int.Parse(v).ToString("X2");
        }

        public static DateTime? ToDateTime(object obj)
        {
            if (obj == null)
                return null;
            else
                return Convert.ToDateTime(obj);
        }
    }
}
