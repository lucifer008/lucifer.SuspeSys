using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.Query.Model
{
   public class CommonQueryModel<T>
    {
        //public T t;
        public IList<T> List = new List<T>();
    }
}
