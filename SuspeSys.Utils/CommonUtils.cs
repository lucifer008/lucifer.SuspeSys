using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils
{
    public class CommonUtils
    {
        static long countIndex = 1;
        public static string GetNewGuid() {
            return Guid.NewGuid().ToString();
        }
        public static long Counter{
            get { return countIndex++; }
        }
    }
}
