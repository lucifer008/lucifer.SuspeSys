using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Common.Utils
{
    public static class DateUitls
    {
        //static Nullable<DateTime> dt;
        public static DateTime? FormatDate(this DateTime formatDate) {
            if (formatDate.Year == 1) return null;
            if (formatDate.ToString().Equals("0001-01-01 00:00:00") || formatDate.ToString().Equals("0001/1/1 0:00:00")) {
                //Nullable<DateTime> dt
                return null;
            }
            
            return formatDate;
        }
    }
}
