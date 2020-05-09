using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain;
using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Utils.Reflection;
using NHibernate.Type;

using DaoModel = SuspeSys.Domain;
using System.Collections;
using SuspeSys.Service.CustPurchaseOrder;

namespace SuspeSys.Service.Impl.CustPurchaseOrder
{
    public class CustPurchaseOrderServiceImpl : Base.ServiceBase, ICustPurchaseOrderService
    {
        public void AddCustPurchaseOrder(CustomerPurchaseOrder customerPurchaseOrder, IList<CustomerPurchaseOrderColorItemModel> cPOrderColorItemList)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    CustomerPurchaseOrderDao.Instance.Save(customerPurchaseOrder, true);
                    foreach (var item in cPOrderColorItemList)
                    {
                        var cpoItem = BeanUitls<CustomerPurchaseOrderColorItem, CustomerPurchaseOrderColorItemModel>.Mapper(item);
                        cpoItem.CustomerPurchaseOrder = customerPurchaseOrder;
                        CustomerPurchaseOrderColorItemDao.Instance.Save(cpoItem, true);
                        foreach (var itemSize in item.CustomerPurchaseOrderColorSizeItemList)
                        {
                            itemSize.CustomerPurchaseOrderColorItem = cpoItem;
                            CustomerPurchaseOrderColorSizeItemDao.Instance.Save(itemSize, true);
                        }
                    }
                    session.Flush();
                    trans.Commit();
                }
            }
        }

    
        public List<CustomerPurchaseOrderColorItemModel> GetCustomerOrderItem(string id)
        {
            var pOrderColorList = new List<DaoModel.CustomerPurchaseOrderColorItemModel>();
            string sql = string.Format(@"select * from CustomerPurchaseOrderColorItem T1 where T1.CUSTOMERPURCHASEORDER_Id=:Id");
            var session = SessionFactory.OpenSession();
            var query = session.CreateSQLQuery(sql);
            var pColorList = query.AddEntity(typeof(DaoModel.CustomerPurchaseOrderColorItem)).SetParameter("Id", id).List<DaoModel.CustomerPurchaseOrderColorItem>();
            //var pColorSzieList = query.AddEntity(typeof(DaoModel.ProcessOrderColorSizeItem)).SetParameter("PROCESSORDER_Id", processOrderId).List();
            foreach (var item in pColorList)
            {
                var model = new DaoModel.CustomerPurchaseOrderColorItemModel();
                var sqlSize = string.Format(@"select * from CustomerPurchaseOrderColorSizeItem T where t.CUSTOMERPURCHASEORDERCOLORITEM_Id=:CUSTOMERPURCHASEORDERCOLORITEM_Id");
                var sizeList = session.CreateSQLQuery(sqlSize)
                    .AddEntity(typeof(DaoModel.CustomerPurchaseOrderColorSizeItem))
                    .SetParameter("CUSTOMERPURCHASEORDERCOLORITEM_Id", item.Id)
                    .List<DaoModel.CustomerPurchaseOrderColorSizeItem>();
                model = BeanUitls<DaoModel.CustomerPurchaseOrderColorItemModel, DaoModel.CustomerPurchaseOrderColorItem>.Mapper(item); //(ProcessOrderColorItemModel)item;
                model.CustomerPurchaseOrderColorSizeItemList = sizeList;
                pOrderColorList.Add(model);
            }
            return pOrderColorList;
        }

        public void UpdateCustPurchaseOrder(CustomerPurchaseOrder customerPurchaseOrder, IList<CustomerPurchaseOrderColorItemModel> cPOrderColorItemList)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Clear();
                    CustomerPurchaseOrderDao.Instance.Update(customerPurchaseOrder, true);
                    session.Delete("from CustomerPurchaseOrderColorSizeItem where CUSTOMERPURCHASEORDERCOLORITEM_Id IN(select Id from CustomerPurchaseOrderColorItem where CUSTOMERPURCHASEORDER_Id=?)",
                      new object[] { customerPurchaseOrder.Id }, new IType[] { NHibernate.NHibernateUtil.String });
                    session.Delete("from CustomerPurchaseOrderColorItem where CUSTOMERPURCHASEORDER_Id=?", new object[] { customerPurchaseOrder.Id }, new IType[] { NHibernate.NHibernateUtil.String });

                    foreach (var item in cPOrderColorItemList)
                    {
                        var cpoItem = BeanUitls<CustomerPurchaseOrderColorItem, CustomerPurchaseOrderColorItemModel>.Mapper(item);
                        cpoItem.CustomerPurchaseOrder = customerPurchaseOrder;
                        CustomerPurchaseOrderColorItemDao.Instance.Save(cpoItem, true);
                        foreach (var itemSize in item.CustomerPurchaseOrderColorSizeItemList)
                        {
                            itemSize.CustomerPurchaseOrderColorItem = cpoItem;
                            CustomerPurchaseOrderColorSizeItemDao.Instance.Save(itemSize, true);
                        }
                    }
                    session.Flush();
                    trans.Commit();
                }
            }
        }

        public DaoModel.CustomerPurchaseOrder GetCustPurchaseOrder(string cpOrderId)
        {
            string queryString = string.Format("select * from [dbo].[CustomerPurchaseOrder] where Id=:cpOrderId");
            var session = SessionFactory.OpenSession();
            return session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.CustomerPurchaseOrder))
                .SetString("cpOrderId", cpOrderId)
                .List<DaoModel.CustomerPurchaseOrder>().SingleOrDefault();
        }
        public IList<CustomerPurchaseOrder> SearchCustomerOrder(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from CustomerPurchaseOrder where Deleted=0";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                //输入客户编号、名称、订单、颜色、尺码查询
                queryString += string.Format(@" AND (CusNo like ? OR CusName like ? OR OrderNo like ? OR PurchaseOrderNo like ? OR id in(
                                    SELECT CUSTOMERPURCHASEORDER_Id from CustomerPurchaseOrderColorItem where Color like ?
                                    )
                                    or id in(
                                    SELECT CUSTOMERPURCHASEORDER_Id from CustomerPurchaseOrderColorItem where id in(
                                    SELECT CUSTOMERPURCHASEORDERCOLORITEM_Id from CustomerPurchaseOrderColorSizeItem where SizeDesption like ?
                                    )
                                    )	)");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<DaoModel.CustomerPurchaseOrder>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, false, paramValues);
            return rslt1;
        }

    }
}
