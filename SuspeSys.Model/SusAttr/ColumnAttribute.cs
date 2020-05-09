using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusAttr
{
    // [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : SusAttribute
    {
        private string priKey;
        public ColumnAttribute(string _priKey)
        {
            this.priKey = _priKey;
        }
        public string PriKey
        {
            get { return priKey; }
        }
    }
}
