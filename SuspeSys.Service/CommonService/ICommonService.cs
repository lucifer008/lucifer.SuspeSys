using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.CommonService
{
    /// <summary>
    /// 针对T的增、删、改、查 接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommonService<T>
    {
        IList<T> GetList();
        IList<T> GetAllList(bool transformer = false);
        void Save(T t);

        T Get(string id);
        T LoadById(string id);
        IList<T> LoadByPage(int pageIndex, int pageSize, out int totalCount);
        void Update(T model);
        bool CheckIsExist(Hashtable hashCondition);
        void LogicDelete(string id);

    }
}
