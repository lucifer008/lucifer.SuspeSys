using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sus.Net.Common.Utils
{
    public class StringUtils
    {
        /// <summary>
        /// 格式化字符串长度，最少10位，不足补0
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string ToFixLenStringFormat(string v) {
            return string.Format("{0:0000000000}", v).PadLeft(10, '0');
        }
        /// <summary>
        /// 格式化字符串长度，最少4位，不足补0
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string ToFixLen4StringFormat(string v)
        {
            return string.Format("{0:0000}", v).PadLeft(4, '0');
        }

    }
}
