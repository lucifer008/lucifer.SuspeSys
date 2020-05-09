using NHibernate.Transform;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Utils.Reflection;
using System.Collections.Generic;
using DaoModel = SuspeSys.Domain;
using System;
using SuspeSys.Service.Impl.Base;
using System.Collections;
using SuspeSys.Service.ProcessOrder;
using System.Text;

namespace SuspeSys.Service.Impl.ProcessOrder
{
    /// <summary>
    /// 查询服务
    /// </summary>
    public class ProcessOrderQueryServiceImpl : ServiceBase, IProcessOrderQueryService
    {
        public IList<DaoModel.ProcessOrder> SearchProcessOrder(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, DaoModel.ProcessOrderModel conModel)
        {
            var session = SessionFactory.OpenSession();
            var queryString = string.Format(@"SELECT   Id, STYLE_Id, CUSTOMER_Id, POrderNo, POrderNum, MOrderNo, POrderType, POrderTypeDesption, 
                ProductNoticeOrderNo, Num, Status, StyleCode, StyleName, CustomerNO, CustomerName, 
                CustomerStyle, CustOrderNo, CustPurchaseOrderNo, DeliveryDate, GenaterOrderDate, OrderNo, InsertDateTime, 
                UpdateDateTime, InsertUser, UpdateUser, Deleted, CompanyId 
FROM      dbo.ProcessOrder WHERE Deleted=0 ");

            IList<string> condList = new List<string>();
            if (!string.IsNullOrEmpty(conModel.POrderNo))
            {
                queryString += " AND POrderNo like ?";
                condList.Add(string.Format("%{0}%", conModel.POrderNo.Trim()));
            }
            if (!string.IsNullOrEmpty(conModel.StyleCode))
            {
                queryString += " AND StyleCode like ?";
                condList.Add(string.Format("%{0}%", conModel.StyleCode.Trim()));
            }
            if (!string.IsNullOrEmpty(conModel.CustOrderNo))
            {
                queryString += string.Format(@" AND Id in(
                    select PROCESSORDER_Id from ProcessOrderColorItem where CUSTOMERPURCHASEORDER_Id in(
                    select id from CustomerPurchaseOrder where OrderNo like ?) 
                    )
                    ");
                condList.Add(string.Format("%{0}%", conModel.CustOrderNo.Trim()));
            }
            if (!string.IsNullOrEmpty(conModel.CustomerNo))
            {
                queryString += string.Format(@" AND Id in(
                    select PROCESSORDER_Id from ProcessOrderColorItem where CUSTOMERPURCHASEORDER_Id in(
                    select id from CustomerPurchaseOrder where CusNo like ?) 
                    )
                    ");
                condList.Add(string.Format("%{0}%", conModel.CustomerNo.Trim()));
            }
            var paramValues = new string[condList.Count];
            condList.CopyTo(paramValues, 0);
            var rslt1 = Query<DaoModel.ProcessOrder>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, false, paramValues);
            return rslt1;
        }

        public IList<DaoModel.ProcessOrderModel> SearchProcessOrder()
        {
            var session = SessionFactory.OpenSession();
            var queryString = string.Format(@"SELECT   Id, STYLE_Id, CUSTOMER_Id, POrderNo, POrderNum, MOrderNo, POrderType, POrderTypeDesption, 
                ProductNoticeOrderNo, Num, Status, StyleCode, StyleName, CustomerNO, CustomerName, 
                CustomerStyle, CustOrderNo, CustPurchaseOrderNo, DeliveryDate, GenaterOrderDate, OrderNo, InsertDateTime, 
                UpdateDateTime, InsertUser, UpdateUser, Deleted, CompanyId 
FROM      dbo.ProcessOrder where Deleted=0");
            var rslt1 = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessOrder)).List<DaoModel.ProcessOrder>();
            //  var rslt1 = session.CreateSQLQuery(queryString).SetResultTransformer(Transformers.AliasToBean(typeof(DaoModel.ProcessOrderModel))).List<DaoModel.ProcessOrderModel>();
            var list = new List<DaoModel.ProcessOrderModel>();
            foreach (var m in rslt1) {
                var pOrder=BeanUitls<DaoModel.ProcessOrderModel, DaoModel.ProcessOrder>.Mapper(m);
                list.Add(pOrder);
            }
            return list;
            //  var rslt= session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessOrder)).List<DaoModel.ProcessOrder>();//.List<SuspeSys.Domain.ProcessOrder>();

            //return rslt;//.OfType<DaoModel.ProcessOrderModel>().ToList();//new List<DaoModel.Ext.ProcessOrderModel>(rslt);
        }
        public DaoModel.ProcessOrder GetProcessOrder(string orderId)
        {
            var session = SessionFactory.OpenSession();
            var queryString = string.Format(@"SELECT   Id, STYLE_Id, CUSTOMER_Id, POrderNo, POrderNum, MOrderNo, POrderType, POrderTypeDesption, 
                ProductNoticeOrderNo, Num, Status, StyleCode, StyleName, CustomerNO, CustomerName, 
                CustomerStyle, CustOrderNo, CustPurchaseOrderNo, DeliveryDate, GenaterOrderDate, OrderNo, InsertDateTime, 
                UpdateDateTime, InsertUser, UpdateUser, Deleted, CompanyId 
FROM      dbo.ProcessOrder WHERE Id=:Id");
            var rslt1 = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessOrder)).SetString("Id", orderId).List<DaoModel.ProcessOrder>();
            //  var rslt1 = session.CreateSQLQuery(queryString).SetResultTransformer(Transformers.AliasToBean(typeof(DaoModel.ProcessOrderModel))).List<DaoModel.ProcessOrderModel>();
            return rslt1[0];
            //  var rslt= session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessOrder)).List<DaoModel.ProcessOrder>();//.List<SuspeSys.Domain.ProcessOrder>();

            //return rslt;//.OfType<DaoModel.ProcessOrderModel>().ToList();//new List<DaoModel.Ext.ProcessOrderModel>(rslt);
        }
        public DaoModel.ProcessOrderModel GetProcessOrder2(string orderId)
        {
            var session = SessionFactory.OpenSession();
            var queryString = string.Format(@"SELECT   Id, STYLE_Id, CUSTOMER_Id, POrderNo, POrderNum, MOrderNo, POrderType, POrderTypeDesption, 
                ProductNoticeOrderNo, Num, 
                (CASE [Status] WHEN 0 THEN '待审核' WHEN 1 THEN '已审核' WHEN 2 THEN '制作完成' WHEN 3 THEN '生产中' WHEN 4
                 THEN '完成' ELSE '错误状态' END) AS StatusText, Status, StyleCode, StyleName, CustomerNO, CustomerName, 
                CustomerStyle, CustOrderNo, CustPurchaseOrderNo, DeliveryDate, GenaterOrderDate, OrderNo, InsertDateTime, 
                UpdateDateTime, InsertUser, UpdateUser, Deleted, CompanyId 
FROM      dbo.ProcessOrder WHERE Id=:Id");
            var rslt1 = session.CreateSQLQuery(queryString).SetString("Id", orderId).SetResultTransformer(new Dao.Nhibernate.BeanTransformerAdapter<DaoModel.ProcessOrderModel>()).List<DaoModel.ProcessOrderModel>();
            return rslt1[0];
            //  var rslt= session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessOrder)).List<DaoModel.ProcessOrder>();//.List<SuspeSys.Domain.ProcessOrder>();

            //return rslt;//.OfType<DaoModel.ProcessOrderModel>().ToList();//new List<DaoModel.Ext.ProcessOrderModel>(rslt);
        }
        /// <summary>
        /// 根据款号获取工序
        /// </summary>
        /// <param name="styleNo"></param>
        /// <returns></returns>
        public IList<DaoModel.StyleProcessFlowSectionItemModel> GetProcessOrderStyleFlow(string styleNo)
        {
            var session = SessionFactory.OpenSession();
            //string sql = string.Format(@"
            //    Select T4.*,T1.ProSectionName FROM ProcessFlowSection T1
            //    INNER JOIN Style T2 ON T1.STYLE_Id=T2.Id
            //    INNER JOIN ProcessFlowSectionItem T3 ON T1.Id=T3.PROCESSFLOWSECTION_Id
            //    INNER JOIN BasicProcessFlow T4 ON T3.BASICPROCESSFLOW_Id=T4.Id Where T2.StyleNo='{0}'",styleNo);

            string sql = string.Format(@"SELECT T1.*,T3.ProSectionName FROM StyleProcessFlowSectionItem T1
				INNER JOIN Style T2 ON T1.Style_Id=T2.Id
				INNER JOIN ProcessFlowSection T3 ON T3.Id=T1.ProcessFlowSection_ID
				WHERE T2.StyleNo='{0}'", styleNo);
            //  var rslt = session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToBean(typeof(DaoModel.BasicProcessFlowModel))).List<DaoModel.BasicProcessFlowModel>();
            var rslt = session.CreateSQLQuery(sql).SetResultTransformer(SuspeSys.Dao.Nhibernate.Suspe.Transformers.AliasToBean(typeof(DaoModel.StyleProcessFlowSectionItemModel))).List<DaoModel.StyleProcessFlowSectionItemModel>();
            return rslt;
        }

        public IList<SuspeSys.Domain.ProcessOrder> SearchProcessOrder(DaoModel.ProcessOrder pOrder)
        {
            var session = SessionFactory.OpenSession();
            var queryString = "select * from ProcessOrder";
            return session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessOrder)).List<DaoModel.ProcessOrder>();//.List<SuspeSys.Domain.ProcessOrder>();

            //return Session.CreateSQLQuery("select distinct c.* from Customer c inner join [Order] o on c.Id=o.CustomerId where o.Ordered > :orderDate")
            //    .AddEntity(typeof(Customer))
            //    .SetDateTime("orderDate", orderDate)
            //    .List<Customer>();
        }

        /// <summary>
        /// 获取版本列表
        /// </summary>
        /// <param name="processOrderId"></param>
        /// <returns></returns>
        public IList<DaoModel.ProcessFlowVersion> GetProcessOrderFlowVersionList(string processOrderId)
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlowVersion] where PROCESSORDER_Id=:PROCESSORDER_Id ORDER BY ProVersionNum DESC");
            var session = SessionFactory.OpenSession();
            return session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowVersion))
                .SetString("PROCESSORDER_Id", processOrderId)
                .List<DaoModel.ProcessFlowVersion>();
        }

        /// <summary>
        ///根据版本 获取制单工序
        /// </summary>
        /// <param name="processFlowVersionId"></param>
        /// <returns></returns>
        public IList<DaoModel.ProcessFlow> GetProcessOrderFlowList(string processFlowVersionId)
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlow] where PROCESSFLOWVERSION_Id=:processFlowVersionId");
            var session = SessionFactory.OpenSession();
            return session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlow))
                .SetString("processFlowVersionId", processFlowVersionId)
                .List<DaoModel.ProcessFlow>();
        }

        /// <summary>
        /// 获取制单颜色及尺码明细
        /// </summary>
        /// <param name="processOrderId"></param>
        /// <returns></returns>
       // public IList<DaoModel.ProcessFlow>
        public IList<DaoModel.ProcessOrderColorItemModel> GetProcessOrderItem(string processOrderId)
        {

            //string sqlPoColorItem = string.Format(@"select * from ProcessOrderColorItem where PROCESSORDER_Id:IdPROCESSORDER_Id");
            //var session = SessionFactory.OpenSession();
            //var query=session.CreateSQLQuery(sqlPoColorItem).AddEntity(typeof(DaoModel.ProcessOrderColorItem)).SetString("IdPROCESSORDER_Id", processOrderId);
            //var pocList = query.List<DaoModel.ProcessOrderColorItem>();

            //string sqlProcessOrderColorSize = string.Format(@"select * from ProcessOrderColorSizeItem where PROCESSORDERCOLORITEM_Id:PROCESSORDERCOLORITEM_Id");
            //var list= session.CreateSQLQuery(sqlPoColorItem).AddEntity(typeof(DaoModel.ProcessOrderColorSizeItem)).SetString("PROCESSORDERCOLORITEM_Id", processOrderId);



            var pOrderColorList = new List<DaoModel.ProcessOrderColorItemModel>();
            string sql = string.Format(@"select * from ProcessOrderColorItem T1 where T1.PROCESSORDER_Id=:PROCESSORDER_Id");
            var session = SessionFactory.OpenSession();
            var query = session.CreateSQLQuery(sql);
            var pColorList = query.AddEntity(typeof(DaoModel.ProcessOrderColorItem)).SetParameter("PROCESSORDER_Id", processOrderId).List<DaoModel.ProcessOrderColorItem>();
            //var pColorSzieList = query.AddEntity(typeof(DaoModel.ProcessOrderColorSizeItem)).SetParameter("PROCESSORDER_Id", processOrderId).List();
            foreach (var item in pColorList)
            {
                var model = new DaoModel.ProcessOrderColorItemModel();
                var sqlSize = string.Format(@"select * from ProcessOrderColorSizeItem T where t.PROCESSORDERCOLORITEM_Id=:PROCESSORDERCOLORITEM_Id");
                var sizeList = session.CreateSQLQuery(sqlSize)
                    .AddEntity(typeof(DaoModel.ProcessOrderColorSizeItem))
                    .SetParameter("PROCESSORDERCOLORITEM_Id", item.Id)
                    .List<DaoModel.ProcessOrderColorSizeItem>();
                model = BeanUitls<DaoModel.ProcessOrderColorItemModel, DaoModel.ProcessOrderColorItem>.Mapper(item); //(ProcessOrderColorItemModel)item;
                model.ProcessOrderColorSizeItemList = sizeList;
                pOrderColorList.Add(model);
            }
            //var pColorS
            return pOrderColorList;
            // var model = new DaoModel.Ext.ProcessOrderColorItemModel();
        }

        /// <summary>
        /// 获取版本列表
        /// </summary>
        /// <returns></returns>
        public IList<DaoModel.ProcessFlowVersionModel> GetProcessOrderFlowVersionList()
        {
            string sql = string.Format(@"
                SELECT T1.Id,T1.[PROCESSORDER_Id] AS ProcessOrderId
                      ,T1.[ProVersionNo]
                      ,T1.[ProcessVersionName]
                      ,T1.[EffectiveDate]
                      ,T1.[TotalStandardPrice]
                      ,T1.[TotalSam],
	                  T3.POrderNo,T3.STYLE_Id StyleId,T3.StyleCode,T3.StyleName
                FROM ProcessFlowVersion T1 
                --INNER JOIN ProcessFlow T2 ON T2.[PROCESSFLOWVERSION_Id]=T1.Id
                INNER JOIN ProcessOrder T3 ON T3.Id=T1.PROCESSORDER_Id");

            var session = SessionFactory.OpenSession();
            var rslt = session.CreateSQLQuery(sql).SetResultTransformer(SuspeSys.Dao.Nhibernate.Suspe.Transformers.AliasToBean(typeof(DaoModel.ProcessFlowVersionModel))).List<DaoModel.ProcessFlowVersionModel>();
            return rslt;
        }

        /// <summary>
        /// 根据组获取站点
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        public IList<DaoModel.StatingModel> GetGroupStatingList(string groups)
        {
            string sql = string.Format(@"  
            SELECT 
                  T2.[Id],T2.[SITEGROUP_Id] AS SiteGroupId ,T2.[StatingName],T2.[StatingNo],T2.[Language]
                      ,T2.[Capacity],T2.[ColorValue],T2.[IsLoadMonitor],T2.[IsEnabled],T1.GroupNO,
					  T3.RoleName
                   FROM SiteGroup T1
                  INNER JOIN Stating T2 ON T1.Id=T2.SITEGROUP_Id
				  INNER JOIN StatingRoles T3 ON T3.Id=T2.StatingRoles_Id
				  WHERE  T3.RoleName!='挂片站' AND T2.Deleted=0 AND t3.RoleCode<>'107'
                  AND T2.SITEGROUP_Id IN('{0}')
                  order by T1.GroupNO,CONVERT(int, T2.StatingNo)
                ", groups.Replace(",", "','").Replace(" ", ""));
            var session = SessionFactory.OpenSession();
            var rslt = session.CreateSQLQuery(sql).SetResultTransformer(SuspeSys.Dao.Nhibernate.Suspe.Transformers.AliasToBean(typeof(DaoModel.StatingModel))).List<DaoModel.StatingModel>();
            return rslt;
        }
        public long GetMaxProcessOrderFlowVersion(string processOrderId)
        {
            string queryString = string.Format("select MAX(ProVersionNum) currentMaxNum from [dbo].[ProcessFlowVersion] where PROCESSORDER_Id=:PROCESSORDER_Id");
            var session = SessionFactory.OpenSession();
            var reslt = session.CreateSQLQuery(queryString).SetString("PROCESSORDER_Id", processOrderId).UniqueResult();
            return Convert.ToInt64(reslt);
        }
        public string GetCurrentMaxProcessFlowVersionNo(string processOrderId)
        {
            var processFlowVersionDao = Dao.ProcessFlowVersionDao.Instance;
            var isExist = processFlowVersionDao.CheckIsExist(new System.Collections.Hashtable());
            if (!isExist)
            {
                return "1";
            }
            var maxNum = GetMaxProcessOrderFlowVersion(processOrderId);
            return (maxNum + 1).ToString();
            //var isExt=
        }
        public bool CheckProcessOrderNoIsExist(string pOrderNo)
        {
            var ht = new Hashtable();
            ht.Add("POrderNo", pOrderNo);
            ht.Add("DELETED","0");
            return Dao.ProcessOrderDao.Instance.CheckIsExist(ht);
        }
        public bool CheckProcessOrderIsProducts(string processOrderId)
        {
            var sql = new StringBuilder("select ISNULL(count(1),0) totalCount from Products where PROCESSORDER_Id=?");
            var total = QueryForObject<int>(sql, null, true, processOrderId);
            if (total > 0)
            {
                return true;
            }
            return false;
        }

    }
}
