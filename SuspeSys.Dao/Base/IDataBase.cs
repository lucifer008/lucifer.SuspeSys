using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Dao.Base
{

    public interface IDataBase<T> where T : class
    {
        IList<T> GetAll();
        IQueryOver<T> QueryOver();
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(string id);

        /// <summary>
        /// 懒加载获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></
        T LoadById(string id);
        /// <summary>
        /// 保存到实体到DB
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="includeInTransaction"></param>
        /// <returns></returns>
        string Save(T obj, bool includeInTransaction = false);
        /// <summary>
        /// 更新一个实体到DB
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="includeInTransaction"></param>
        void Update(T obj, bool includeInTransaction = false);
        /// <summary>
        /// 物理删除：只对业务中间数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeInTransaction"></param>
        void Delete(string id, bool includeInTransaction = false);
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeInTransaction"></param>
        void LogicDelete(string id, bool includeInTransaction = false);
        /// <summary>
        /// sql查询
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        IQuery CreateQuery(string queryString);

        /// <summary>
        /// 分页支持
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IList<T> LoadByPage(int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 准确查找条件是否存在
        /// </summary>
        /// <param name="hashCondition"></param>
        /// <returns></returns>
        bool CheckIsExist(Hashtable hashCondition);
    }
}
