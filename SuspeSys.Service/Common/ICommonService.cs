using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Common
{
    public interface ICommonService
    {
        IList<Style> GetAllStyleList();
        IList<PoColor> GetColorList();
        IList<PSize> GetSizeList();
        IList<DaoModel.StyleProcessFlowStoreModel> GetStyleProcessFlowStore();

        IList<Domain.City> GetCityListByProvinceId(string proviceId);

        IList<Domain.Area> GetAreaListByCityId(string cityId);

        IList<T> GetList<T>();

        /// <summary>
        /// 是否能删除站点
        /// </summary>
        /// <param name="statingId"></param>
        /// <returns></returns>
        bool CanDeleteStating(string statingId);
        void CheckStatingIsExist(BridgeSet m, ref string info);

        /// <summary>
        ///  获取指定字段的最大值
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        T GetMaxOrderNo<T>(string columnName, string tableName);
    }
}
