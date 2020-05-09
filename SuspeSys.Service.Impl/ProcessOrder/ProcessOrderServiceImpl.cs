
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
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.Support;
using SuspeSys.Service.Impl.ProcessOrder;
using SuspeSys.Domain.Ext;

namespace SuspeSys.Service.Impl
{
    public class ProcessOrderServiceImpl : ServiceBase, IProcessOrderService
    {
        /// <summary>
        /// 添加制单
        /// </summary>
        /// <param name="pOrder"></param>
        /// <param name="pOrderItemList"></param>
        public void AddProcessOrder(SuspeSys.Domain.ProcessOrder pOrder, IList<ProcessOrderColorItemModel> pOrderColorItemList)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    pOrder.POrderNum = IdGeneratorSupport.Instance.NextId(typeof(SuspeSys.Domain.ProcessOrder));
                    ProcessOrderDao.Instance.Save(pOrder, true);
                    foreach (var pi in pOrderColorItemList)
                    {
                        pi.ProcessOrder = pOrder;
                        ProcessOrderColorItem pocItem = BeanUitls<ProcessOrderColorItem, ProcessOrderColorItemModel>.Mapper(pi);
                        if(!string.IsNullOrEmpty(pi.CustomerpurchaseorderId))
                            pocItem.CustomerPurchaseOrder = CustomerPurchaseOrderDao.Instance.GetById(pi.CustomerpurchaseorderId);
                        pocItem.ProcessOrder = pOrder;
                        if (null != pocItem.CustomerPurchaseOrder)
                        {
                            if (string.IsNullOrEmpty(pocItem.CustomerPurchaseOrder.Id))
                                CustomerPurchaseOrderDao.Instance.Save(pocItem.CustomerPurchaseOrder, true);
                            //else
                            //    CustomerPurchaseOrderDao.Instance.Update(pocItem.CustomerPurchaseOrder, true);
                        }
                        ProcessOrderColorItemDao.Instance.Save(pocItem, true);
                        foreach (var psi in pi.ProcessOrderColorSizeItemList)
                        {
                            psi.ProcessOrderColorItem = pocItem;
                            ProcessOrderColorSizeItemDao.Instance.Save(psi, true);
                        }
                    }
                    session.Flush();
                    trans.Commit();
                }
            }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 添加制单
        /// </summary>
        /// <param name="pOrder"></param>
        /// <param name="pOrderItemList"></param>
        public void UpdateProcessOrder(SuspeSys.Domain.ProcessOrder pOrder, IList<ProcessOrderColorItemModel> pOrderColorItemList)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Clear();
                    ProcessOrderDao.Instance.Update(pOrder, true);
                    session.Delete("from ProcessOrderColorSizeItem where PROCESSORDERCOLORITEM_Id IN(select Id from ProcessOrderColorItem where PROCESSORDER_Id=?)",
                        new object[] { pOrder.Id }, new IType[] { NHibernate.NHibernateUtil.String });
                    session.Delete("from ProcessOrderColorItem where PROCESSORDER_Id=?", new object[] { pOrder.Id }, new IType[] { NHibernate.NHibernateUtil.String });
                    foreach (var pi in pOrderColorItemList)
                    {
                        pi.ProcessOrder = pOrder;
                        ProcessOrderColorItem pocItem = BeanUitls<ProcessOrderColorItem, ProcessOrderColorItemModel>.Mapper(pi);
                        //                        ProcessOrderColorSizeItemDao.Instance.Save(psi, true);
                        if(!string.IsNullOrEmpty(pi.CustomerpurchaseorderId))
                            pocItem.CustomerPurchaseOrder = CustomerPurchaseOrderDao.Instance.GetById(pi.CustomerpurchaseorderId);
                        pocItem.ProcessOrder = pOrder;
                        ProcessOrderColorItemDao.Instance.Save(pocItem, true);
                        if (null != pocItem.CustomerPurchaseOrder)
                        {
                            if(string.IsNullOrEmpty(pocItem.CustomerPurchaseOrder.Id))
                                CustomerPurchaseOrderDao.Instance.Save(pocItem.CustomerPurchaseOrder, true);
                            //else
                            //    CustomerPurchaseOrderDao.Instance.Update(pocItem.CustomerPurchaseOrder, true);
                        }
                        foreach (var psi in pi.ProcessOrderColorSizeItemList)
                        {
                            psi.ProcessOrderColorItem = pocItem;
                            ProcessOrderColorSizeItemDao.Instance.Save(psi, true);
                        }
                    }
                    session.Flush();
                    trans.Commit();
                }
            }
        }
        /// <summary>
        /// 添加制单工序
        /// </summary>
        /// <param name="pOrder"></param>
        /// <param name="pfVersion"></param>
        /// <param name="pfList"></param>
        public void AddProcessOrderFlow(DaoModel.ProcessOrder pOrder, DaoModel.ProcessFlowVersion pfVersion, IList<ProcessFlow> pfList)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    pfVersion.ProcessOrder = pOrder;
                    ProcessFlowVersionDao.Instance.Save(pfVersion, true);
                    foreach (var pi in pfList)
                    {
                        pi.ProcessFlowVersion = pfVersion;
                        //ProcessOrderColorItem pocItem = BeanUitls<ProcessOrderColorItem, ProcessOrderColorItemModel>.Mapper(pi);
                        ProcessFlowDao.Instance.Save(pi, true);
                    }
                    session.Flush();
                    trans.Commit();
                }
            }

        }
        /// <summary>
        /// 更新制单工序
        /// </summary>
        /// <param name="pOrder"></param>
        /// <param name="pfVersion"></param>
        /// <param name="pfList"></param>
        public void UpdateProcessOrderFlow(DaoModel.ProcessOrder pOrder, DaoModel.ProcessFlowVersion pfVersion, IList<ProcessFlow> pfList)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Clear();
                    pfVersion.ProcessOrder = pOrder;
                    ProcessFlowVersionDao.Instance.Update(pfVersion, true);
                  //  session.Delete("from ProcessFlow where PROCESSFLOWVERSION_Id=?", new object[] { pfVersion.Id }, new IType[] { NHibernate.NHibernateUtil.String });
                    foreach (var pi in pfList)
                    {
                        pi.ProcessFlowVersion = pfVersion;
                        if (string.IsNullOrEmpty(pi.Id))
                            ProcessFlowDao.Instance.Save(pi, true);
                        else
                            ProcessFlowDao.Instance.Update(pi, true);
                    }
                    session.Flush();
                    trans.Commit();
                }
            }

        }

        #region 复制制单
        public void CopyProcessOrderInfo(string pOrderId, bool copyProcessOrder, bool copyProcessOrderItem, bool copyProcessFlow, bool copyProcessFlowChart)
        {
            var cpProcessOrder = new CopyProcessOrderModel();
            var pOrderService = new ProcessOrderQueryServiceImpl();
            var pChartService = new ProcessFlowChartServiceImpl();
            //IList<DaoModel.ProcessOrderColorItemModel> pFlowList = new List<DaoModel.ProcessOrderColorItemModel>();
            IList<DaoModel.ProcessFlowVersion> pFlowVersionList = new List<DaoModel.ProcessFlowVersion>();
            //IList<DaoModel.ProcessFlowVersionModel> pFlowVersionModelList = new List<DaoModel.ProcessFlowVersionModel>();
            //IList<DaoModel.ProcessFlowChart> pFlowChartList = new List<DaoModel.ProcessFlowChart>();
            //IList<DaoModel.ProcessFlowChartModel> pFlowChartModelList = new List<DaoModel.ProcessFlowChartModel>();
            var pOrder = Dao.ProcessOrderDao.Instance.GetById(pOrderId);
            cpProcessOrder.ProcessOrder = pOrder;
            if (copyProcessOrderItem)
            {
                cpProcessOrder.ProcessOrderColorItemModelList = pOrderService.GetProcessOrderItem(pOrderId);
            }
            if (copyProcessFlow)
            {
                pFlowVersionList = pOrderService.GetProcessOrderFlowVersionList(pOrderId);
                foreach (var fv in pFlowVersionList)
                {
                    cpProcessOrder.ProcessFlowVersionList.Add(fv);
                }
            }
            if (copyProcessFlowChart)
            {
                foreach (var flowVersion in cpProcessOrder.ProcessFlowVersionList)
                {
                    var pChartList = pChartService.GetProcessFlowChartList(flowVersion.Id);
                    foreach (var pc in pChartList)
                    {
                        cpProcessOrder.ProcessFlowChartlList.Add(pc);
                    }
                }
            }
            //   var newOrder = BeanUitls<DaoModel.ProcessOrder, DaoModel.ProcessOrder>.Mapper(pOrder);
            CopyProcessOrderImpl(cpProcessOrder);
        }
        void CopyProcessOrderImpl(CopyProcessOrderModel cpProcessOrder)
        {

        }
        #endregion

        //public string 
    }
}
