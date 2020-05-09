using SuspeSys.Client.Action.Query.Model;
using SuspeSys.Service.Impl.CommonService;
using System.Collections.Generic;

namespace SuspeSys.Client.Action.Query
{
    public class CommonQueryAction<T> : BaseAction
    {
        public CommonQueryModel<T> Model = new CommonQueryModel<T>();
        CommonServiceImpl<T> commonQueryService = new CommonServiceImpl<T>();
        public void GetList()
        {
            Model.List = commonQueryService.GetList();
        }
        public T Get(string id)
        {
            return commonQueryService.Get(id);
        }
        /// <summary>
        /// 懒加载获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T LoadById(string id)
        {
            return commonQueryService.LoadById(id);
        }
        public IList<T> GetAllList()
        {
            return commonQueryService.GetList();
        }
        public IList<T> LoadByPage(int pageIndex, int pageSize, out int totalCount)
        {
            return commonQueryService.LoadByPage(pageIndex,pageSize,out totalCount);
        }
    }
}
