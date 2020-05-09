using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.CustomerControls.Sqlite.Repository.Base
{
    interface IRepository<T>
    {
        IEnumerable<T> GetList();

        T Get(object id);

        bool Update(T t);

        T Insert(T apply);

        bool Delete(T t);
    }
}
