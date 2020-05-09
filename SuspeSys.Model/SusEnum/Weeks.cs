using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace SuspeSys.Domain.SusEnum
{
    public enum Weeks
    {

        [Description("请选择")]
        None = 0,

        [Description("星期一")]
        Monday = 1,

        [Description("星期二")]
        Tuesday = 2,

        [Description("星期三")]
        Wednesday = 3,

        [Description("星期四")]
        Thursday = 4,

        [Description("星期五")]
        Friday =5,
        [Description("星期六")]
        Saturday = 6,
        [Description("星期日")]
        Sunday = 7,
    }
}
