using NHibernate.Transform;
using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain;
using SuspeSys.Service.Common;
using SuspeSys.Service.Impl.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Impl.Common
{

    public class CommonServiceImpl : ServiceBase, ICommonService
    {
        public IList<Style> GetAllStyleList()
        {
            return StyleDao.Instance.GetAll();
        }
        public IList<PoColor> GetColorList()
        {
            return PoColorDao.Instance.GetAll();
        }
        public IList<PSize> GetSizeList()
        {
            return PSizeDao.Instance.GetAll();
        }
        /// <summary>
        /// 获取款式工序库详情
        /// </summary>
        /// <param name="styleNo"></param>
        /// <returns></returns>
        public IList<DaoModel.StyleProcessFlowStoreModel> GetStyleProcessFlowStore()
        {
            var session = SessionFactory.OpenSession();
            string sql = string.Format(@"
              Select T4.*,T5.ProSectionName,T2.StyleNo,T2.StyleName,T1.Id As StyleProcessFlowSectionItemId FROM StyleProcessFlowSectionItem T1
                INNER JOIN Style T2 ON T1.STYLE_Id=T2.Id
				INNER JOIN ProcessFlowSection T5 ON T5.Id=T1.PROCESSFLOWSECTION_Id
			INNER JOIN BasicProcessFlow T4 ON T1.BASICPROCESSFLOW_Id=T4.Id ");
            //  var rslt = session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToBean(typeof(DaoModel.BasicProcessFlowModel))).List<DaoModel.BasicProcessFlowModel>();
            var rslt = session.CreateSQLQuery(sql).SetResultTransformer(SuspeSys.Dao.Nhibernate.Suspe.Transformers.AliasToBean(typeof(DaoModel.StyleProcessFlowStoreModel))).List<DaoModel.StyleProcessFlowStoreModel>();
            return rslt;
        }

        public IList<Domain.City> GetCityListByProvinceId(string proviceId)
        {
            var session = SessionFactory.OpenSession();
            string sql = @"SELECT Id,CityCode,CityName  FROM CITY WHERE PROVINCE_Id = ?";

            return session.CreateSQLQuery(sql)
                          .SetParameter(0,proviceId)
                          .SetReadOnly(true)
                          .SetResultTransformer(Transformers.AliasToBean(typeof(DaoModel.City)))
                          .List<City>();
        }

        public IList<Domain.Area> GetAreaListByCityId(string cityId)
        {
            var session = SessionFactory.OpenSession();
            string sql = @"SELECT  Id,AreaName,AreaCode FROM Area WHERE CITY_Id = ?";

            return session.CreateSQLQuery(sql)
                          .SetParameter(0, cityId)
                          .SetReadOnly(true)
                          .SetResultTransformer(Transformers.AliasToBean(typeof(DaoModel.Area)))
                          .List<Area>();
        }

        public IList<T> GetList<T>()
        {
            var session = SessionFactory.OpenSession();
            string sql = @"SELECT  * FROM  " + typeof(T).Name;

            return session.CreateSQLQuery(sql)
                          .SetResultTransformer(Transformers.AliasToBean(typeof(T)))
                          .List<T>();
        }

        /// <summary>
        /// 获取制单号列表
        /// </summary>
        /// <returns></returns>
        public List<Domain.ProcessOrder> GetProcessOrderList()
        {
            string sql = "SELECT DISTINCT POrderNo  FROM ProcessOrder";
            var result = DapperHelp.Query< Domain.ProcessOrder>(sql);
            if (result == null)
                return new List<DaoModel.ProcessOrder>();
            else
                return result.ToList();
        }

        /// <summary>
        /// 获取PO列表
        /// </summary>
        /// <returns></returns>
        public List<Domain.ProcessOrder> GetCustPurchaseOrderNoList()
        {
            string sql = "SELECT DISTINCT CustPurchaseOrderNo  FROM ProcessOrder";
            var result = DapperHelp.Query<Domain.ProcessOrder>(sql);
            if (result == null)
                return new List<DaoModel.ProcessOrder>();
            else
                return result.ToList();
        }

        //select DISTINCT FlowSection From Products

        /// <summary>
        /// 获取工段
        /// </summary>
        /// <returns></returns>
        public List<Domain.Products> GetFlowSectionList()
        {
            string sql = "select DISTINCT FlowSection From Products";
            var result = DapperHelp.Query<Domain.Products>(sql);
            if (result == null)
                return new List<DaoModel.Products>();
            else
                return result.ToList();
        }

        /// <summary>
        /// 获取款号
        /// </summary>
        /// <returns></returns>
        public List<Domain.Style> GetStyleList()
        {
            string sql = "SELECT *  FROM Style";
            var result = DapperHelp.Query<Domain.Style>(sql);
            if (result == null)
                return new List<DaoModel.Style>();
            else
                return result.ToList();
        }

        /// <summary>
        /// 获取Color
        /// </summary>
        /// <returns></returns>
        public List<Domain.PoColor> GetPoColorList()
        {
            string sql = "select * from PoColor";
            var result = DapperHelp.Query<Domain.PoColor>(sql);

            if (result == null)
                return new List<PoColor>();
            else
                return result.ToList();
        }

        /// <summary>
        /// 获取车间
        /// </summary>
        /// <returns></returns>
        public List<Domain.Workshop> GetWorkshopList()
        {
            string sql = "select * from Workshop";
            var result = DapperHelp.Query<Domain.Workshop>(sql);

            if (result == null)
                return new List<Workshop>();
            else
                return result.ToList();
        }

        /// <summary>
        /// 获取车间
        /// </summary>
        /// <returns></returns>
        public List<Domain.PSize> GetPSize()
        {
            string sql = "select * from PSize";
            var result = DapperHelp.Query<Domain.PSize>(sql);

            if (result == null)
                return new List<PSize>();
            else
                return result.ToList();
        }

        /// <summary>
        /// 获取组别
        /// </summary>
        /// <returns></returns>
        public List<Domain.SiteGroup> GetSiteGroup()
        {
            string sql = "select DISTINCT GroupNO, GroupName From SiteGroup";
            var result = DapperHelp.Query<Domain.SiteGroup>(sql);

            if (result == null)
                return new List<SiteGroup>();
            else
                return result.ToList();
        }
        /// <summary>
        /// 是否能删除站点
        /// </summary>
        /// <param name="statingId"></param>
        /// <returns></returns>
        public bool CanDeleteStating(string statingId)
        {
            string sql = @" 
declare @StatingId char(36);
declare @StatingNo varchar(20);
declare @MainTrackNumber smallint;

 
select @StatingId = a.Id,@StatingNo = a.StatingNo,@MainTrackNumber = MainTrackNumber
from Stating a where a.Id = @Id
declare @StatingRef int;
set @StatingRef = 0;
 
--select @StatingRef = COUNT(1) + @StatingRef from  FlowStating where STATING_Id = @StatingId;
select @StatingRef = COUNT(1) + @StatingRef from  FlowStatingColor a
left join PROCESSFLOWSTATINGITEM b on a.PROCESSFLOWSTATINGITEM_Id = b.Id  where b.STATING_Id = @StatingId ;
select @StatingRef = COUNT(1) + @StatingRef from FlowStatingResume where STATING_Id = @StatingId;
select @StatingRef = COUNT(1) + @StatingRef from FlowStatingSize a
left join PROCESSFLOWSTATINGITEM b on a.PROCESSFLOWSTATINGITEM_Id = b.Id  where b.STATING_Id = @StatingId ;
select @StatingRef = COUNT(1) + @StatingRef from HangerProductFlowChart where StatingId = @StatingId;
select @StatingRef = COUNT(1) + @StatingRef from HangerReworkRequest where StatingNo = @StatingNo and MainTrackNumber = @MainTrackNumber;
select @StatingRef = COUNT(1) + @StatingRef from HangerReworkRequestItem where StatingNo = @StatingNo and MainTrackNumber = @MainTrackNumber;
select @StatingRef = COUNT(1) + @StatingRef from HangerReworkRequestQueue where StatingNo = @StatingNo and MainTrackNumber = @MainTrackNumber;
select @StatingRef = COUNT(1) + @StatingRef from HangerReworkRequestQueueItem where StatingNo = @StatingNo and MainTrackNumber = @MainTrackNumber;
select @StatingRef = COUNT(1) + @StatingRef from ProcessFlowStatingItem where STATING_Id = @StatingId;
select @StatingRef = COUNT(1) + @StatingRef from SuccessHangerProductFlowChart where StatingId = @StatingId;
select @StatingRef";

            int effCount = DapperHelp.ExecuteScalar<int>(sql, new { Id = statingId });

            return (effCount == 0);
        }

        public void CheckStatingIsExist(BridgeSet m, ref string info)
        {
            var sql = string.Format(@"select * from Stating where StatingNo = @StatingNo and MainTrackNumber = @MainTrackNumber and Deleted=0");
            var statingA = DapperHelp.QueryForObject<Stating>(sql, new { StatingNo =m.ASiteNo.Value, MainTrackNumber =m.AMainTrackNumber.Value});
            if (null==statingA) {
                info = string.Format("桥接主轨:{0} 站点:{1}不存在!",m.AMainTrackNumber,m.ASiteNo);
                return;
            }
            statingA = DapperHelp.QueryForObject<Stating>(sql, new { StatingNo = m.BSiteNo.Value, MainTrackNumber = m.BMainTrackNumber.Value });
            if (null == statingA)
            {
                info = string.Format("桥接主轨:{0} 站点:{1}不存在!", m.BMainTrackNumber, m.BSiteNo);
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public T GetMaxOrderNo<T>(string columnName, string tableName)
        {
            string sql = $"select isnull(max({columnName}),0)  from {tableName}";

            return DapperHelp.ExecuteScalar<T>(sql, null);
        }
    }
}
