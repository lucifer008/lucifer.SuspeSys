using NHibernate.Type;
using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.ProcessOrder;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Service.Impl.ProcessOrder
{
    public class ProcessFlowChartServiceImpl : ServiceBase, IProcessFlowChartService
    {
        public readonly static ProcessFlowChartServiceImpl Instance = new ProcessFlowChartServiceImpl();
        public void AddProcessFlowChart(DaoModel.ProcessFlowChart processFlowChart, IList<DaoModel.ProcessFlowChartGrop> ProcessFlowChartGroupList, IList<DaoModel.ProcessFlowChartFlowRelationModel> ProcessFlowChartFlowRelationModelList)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    IList<DaoModel.ProcessFlowStatingItem> StatingRoleNameChange = new List<DaoModel.ProcessFlowStatingItem>();


                    Dao.ProcessFlowChartDao.Instance.Save(processFlowChart, true);
                    foreach (var gp in ProcessFlowChartGroupList)
                    {
                        gp.ProcessFlowChart = processFlowChart;
                        Dao.ProcessFlowChartGropDao.Instance.Save(gp, true);
                    }

                    //处理工序合并
                    var processFlowChartRelation = new List<DaoModel.ProcessFlowChartFlowRelation>();
                    //处理工序合并

                    foreach (var pfChartFlowRelation in ProcessFlowChartFlowRelationModelList)
                    {
                        var pfcRelation = BeanUitls<DaoModel.ProcessFlowChartFlowRelation, DaoModel.ProcessFlowChartFlowRelationModel>.Mapper(pfChartFlowRelation);
                        //var pfcRelation = (DaoModel.ProcessFlowChartFlowRelation)pfChart;
                        pfcRelation.IsEnabled = Convert.ToByte(pfChartFlowRelation.IsEn);//是否生效
                        pfcRelation.IsMergeForward = pfChartFlowRelation.IsMergeForw;//是否往前合并
                        pfcRelation.ProcessFlowChart = processFlowChart;
                        pfcRelation.ProcessFlow = pfChartFlowRelation.ProcessFlow;//制单工序Id
                        Dao.ProcessFlowChartFlowRelationDao.Instance.Save(pfcRelation, true);

                        processFlowChartRelation.Add(pfcRelation);

                        //工序站点明细
                        var processFlowStationgItemList = new List<DaoModel.ProcessFlowStatingItem>();
                        foreach (var pfs in pfChartFlowRelation.ProcessFlowChartFlowRelationModelList)
                        {
                            var pfsItem = new DaoModel.ProcessFlowStatingItem();
                            pfsItem.No = pfs.StatingNo?.Trim();
                            pfsItem.Proportion = Convert.ToDecimal(pfs.FlowNo);
                            pfsItem.IsEndStating = pfs.IsEndStating;
                            //pfsItem.IsReceivingHanger = 
                            pfsItem.ReceivingPoNumber = pfs.ReceivingPoNumber;
                            pfsItem.SiteGroupNo = pfs.GroupNo?.Trim();
                            pfsItem.IsReceivingHanger = Convert.ToByte(pfs.IsEn);//是否接收衣架
                            pfsItem.StatingRoleName = pfs.FlowCode;
                            pfsItem.Stating = StatingDao.Instance.GetById(pfs.ProcessflowIdOrStatingId);
                            pfsItem.MainTrackNumber = pfsItem.Stating?.MainTrackNumber;
                            pfsItem.IsReceivingAllColor = pfs.IsReceivingAllColor;
                            pfsItem.IsReceivingAllSize = pfs.IsReceivingAllSize;
                            pfsItem.ReceivingColor = pfs.ReceivingColor;
                            pfsItem.ReceivingSize = pfs.ReceivingSize;
                            pfsItem.StatingRoleCode = StatingType.GetStatingCode(pfsItem.StatingRoleName);
                            processFlowStationgItemList.Add(pfsItem);

                            //if (pfsItem.Stating != null &&
                            //    pfsItem.Stating.StatingRoles != null &&
                            //    !pfsItem.Stating.StatingRoles.RoleName.Equals(pfsItem.StatingRoleName))
                            //{
                            //    if (!statingRoleDescList.Contains(pfsItem.StatingRoleName))
                            //        StatingRoleNameChange.Add(pfsItem);
                            //}

                        }
                        foreach (var pfcRelationItem in processFlowStationgItemList)
                        {
                            pfcRelationItem.ProcessFlowChartFlowRelation = pfcRelation;
                            Dao.ProcessFlowStatingItemDao.Instance.Save(pfcRelationItem, true);
                        }
                    }

                    //处理合并工序

                    //processFlowChartRelation.Reverse();
                    foreach (var item in processFlowChartRelation)
                    {
                        if (item.IsMergeForward.HasValue && item.IsMergeForward.Value)
                        {
                            //找出上一工序
                            var preCraftFlowNo = (int.Parse((item.CraftFlowNo)) - 1).ToString();
                            var preChart = processFlowChartRelation.Where(o => o.CraftFlowNo.Equals(preCraftFlowNo)).FirstOrDefault();
                            if (preChart == null)
                                throw new Exception("已经是第一道工序，不能向前合并");

                            if (preChart.IsMergeForward.HasValue && preChart.IsMergeForward.Value)
                            {
                                item.MergeFlowNo = preChart.MergeFlowNo;
                                item.MergeProcessFlowChartFlowRelationId = preChart.MergeProcessFlowChartFlowRelationId;
                            }
                            else
                            {
                                item.MergeFlowNo = preChart.FlowNo;
                                item.MergeProcessFlowChartFlowRelationId = preChart.Id;
                            }

                        }
                    }

                    //更改站点角色
                    if (StatingRoleNameChange != null &&
                         StatingRoleNameChange.Count > 0)
                    {
                        var temp = StatingRoleNameChange.Distinct().ToList();
                        foreach (var item in temp)
                        {
                            string sql = "select * from StatingRoles where roleName = @RoleName";

                            item.Stating.StatingRoles = DapperHelp.QueryForObject<DaoModel.StatingRoles>(sql, new { RoleName = item.StatingRoleName?.Trim() });
                            item.Stating.StatingName = item.Stating.StatingRoles.RoleName;
                            Dao.StatingDao.Instance.Save(item.Stating, true);
                        }
                    }

                    session.Flush();
                    trans.Commit();
                }

            }
        }

        static IList<String> statingRoleDescList = new List<string>() { "车缝站", "存储站", "多功能站", "返工站", "收尾站" };
        public void UpdateProcessFlowChart(DaoModel.ProcessFlowChart processFlowChart,
              IList<DaoModel.ProcessFlowChartGrop> ProcessFlowChartGroupList,
            IList<DaoModel.ProcessFlowChartFlowRelationModel> ProcessFlowChartFlowRelationModelList)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Clear();
                    Dao.ProcessFlowChartDao.Instance.Update(processFlowChart, true);
                    session.Delete("from ProcessFlowStatingItem where PROCESSFLOWCHARTFLOWRELATION_Id IN(select Id from ProcessFlowChartFlowRelation where PROCESSFLOWCHART_Id=?)",
                        new object[] { processFlowChart.Id }, new IType[] { NHibernate.NHibernateUtil.String });
                    session.Delete("from ProcessFlowChartFlowRelation where PROCESSFLOWCHART_Id=?", new object[] { processFlowChart.Id }, new IType[] { NHibernate.NHibernateUtil.String });

                    session.Delete("from ProcessFlowChartGrop where PROCESSFLOWCHART_Id=?", new object[] { processFlowChart.Id }, new IType[] { NHibernate.NHibernateUtil.String });
                    foreach (var gp in ProcessFlowChartGroupList)
                    {
                        gp.ProcessFlowChart = processFlowChart;
                        Dao.ProcessFlowChartGropDao.Instance.Save(gp, true);
                    }

                    IList<DaoModel.ProcessFlowStatingItem> StatingRoleNameChange = new List<DaoModel.ProcessFlowStatingItem>();

                    //处理工序合并
                    var processFlowChartRelation = new List<DaoModel.ProcessFlowChartFlowRelation>();
                    //处理工序合并

                    foreach (var pfChart in ProcessFlowChartFlowRelationModelList)
                    {
                        var pfcRelation = BeanUitls<DaoModel.ProcessFlowChartFlowRelation, DaoModel.ProcessFlowChartFlowRelationModel>.Mapper(pfChart);
                        //var pfcRelation = (DaoModel.ProcessFlowChartFlowRelation)pfChart;
                        pfcRelation.IsEnabled = Convert.ToByte(pfChart.IsEn);//是否生效
                        pfcRelation.IsMergeForward = pfChart.IsMergeForw;//是否往前合并
                        pfcRelation.ProcessFlowChart = processFlowChart;
                        pfcRelation.ProcessFlow = pfChart.ProcessFlow;//制单工序Id
                        Dao.ProcessFlowChartFlowRelationDao.Instance.Save(pfcRelation, true);
                        processFlowChartRelation.Add(pfcRelation);
                        //工序站点明细
                        var processFlowStationgItemList = new List<DaoModel.ProcessFlowStatingItem>();
                        foreach (var pfs in pfChart.ProcessFlowChartFlowRelationModelList)
                        {
                            var pfsItem = new DaoModel.ProcessFlowStatingItem();
                            pfsItem.No = pfs.StatingNo?.Trim();
                            pfsItem.Proportion = Convert.ToDecimal(pfs.FlowNo);
                            pfsItem.IsEndStating = pfs.IsEndStating;
                            pfsItem.ReceivingPoNumber = pfs.ReceivingPoNumber;
                            pfsItem.IsReceivingAllPoNumber = pfs.IsReceivingAllPoNumber;
                            pfsItem.SiteGroupNo = pfs.GroupNo?.Trim();
                            pfsItem.IsReceivingHanger = Convert.ToByte(pfs.IsEn);//是否接收衣架
                            pfsItem.StatingRoleName = pfs.FlowCode;
                            pfsItem.Stating = StatingDao.Instance.GetById(pfs.ProcessflowIdOrStatingId);
                            pfsItem.MainTrackNumber = pfsItem.Stating?.MainTrackNumber;
                            pfsItem.IsReceivingAllColor = pfs.IsReceivingAllColor;
                            pfsItem.IsReceivingAllSize = pfs.IsReceivingAllSize;
                            pfsItem.ReceivingColor = pfs.ReceivingColor;
                            pfsItem.ReceivingSize = pfs.ReceivingSize;
                            pfsItem.StatingRoleCode = StatingType.GetStatingCode(pfsItem.StatingRoleName);
                            processFlowStationgItemList.Add(pfsItem);

                            //if (pfsItem.Stating != null &&
                            //    pfsItem.Stating.StatingRoles != null &&
                            //    !pfsItem.Stating.StatingRoles.RoleName.Equals(pfsItem.StatingRoleName))
                            //{
                            //    if(!statingRoleDescList.Contains(pfsItem.StatingRoleName))
                            //        StatingRoleNameChange.Add(pfsItem);
                            //}
                        }

                        foreach (var pfcRelationItem in processFlowStationgItemList)
                        {
                            pfcRelationItem.ProcessFlowChartFlowRelation = pfcRelation;
                            Dao.ProcessFlowStatingItemDao.Instance.Save(pfcRelationItem, true);

                        }
                    }

                    //更改站点角色
                    if (StatingRoleNameChange != null &&
                         StatingRoleNameChange.Count > 0)
                    {
                        var temp = StatingRoleNameChange.Distinct().ToList();
                        foreach (var item in temp)
                        {
                            string sql = "select * from StatingRoles where roleName = @RoleName";

                            item.Stating.StatingRoles = DapperHelp.QueryForObject<DaoModel.StatingRoles>(sql, new { RoleName = item.StatingRoleName?.Trim() });
                            item.Stating.StatingName = item.Stating.StatingRoles.RoleName;
                            Dao.StatingDao.Instance.Save(item.Stating, true);
                        }
                    }

                    //处理合并工序
                    foreach (var item in processFlowChartRelation)
                    {
                        if (item.IsMergeForward.HasValue && item.IsMergeForward.Value)
                        {
                            //找出上一工序
                            var preCraftFlowNo = (int.Parse((item.CraftFlowNo)) - 1).ToString();
                            var preChart = processFlowChartRelation.Where(o => o.CraftFlowNo.Equals(preCraftFlowNo)).FirstOrDefault();
                            if (preChart == null)
                                throw new Exception("已经是第一道工序，不能向前合并");

                            if (preChart.IsMergeForward.HasValue && preChart.IsMergeForward.Value)
                            {
                                item.MergeFlowNo = preChart.MergeFlowNo;
                                item.MergeProcessFlowChartFlowRelationId = preChart.MergeProcessFlowChartFlowRelationId;
                            }
                            else
                            {
                                item.MergeFlowNo = preChart.FlowNo;
                                item.MergeProcessFlowChartFlowRelationId = preChart.Id;
                            }
                        }
                    }


                    session.Flush();
                    trans.Commit();
                }
            }
        }

        public IList<DaoModel.ProcessFlowChart> GetProcessFlowChartList(string flowVersionId)
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlowChart] where PROCESSFLOWVERSION_Id=:flowVersionId order by InsertDateTime desc");
            var session = SessionFactory.OpenSession();
            return session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowChart))
                .SetString("flowVersionId", flowVersionId)
                .List<DaoModel.ProcessFlowChart>();
        }

        /// <summary>
        /// 根据工艺图Id获取 路线明细
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        public IList<DaoModel.ProcessFlowChartFlowRelationModel> GetProcessFlowChartLineItem(string flowChartId)
        {
            var pfcRelationList = new List<DaoModel.ProcessFlowChartFlowRelationModel>();

            //工艺图工序
            string queryString = string.Format("select * from [dbo].[ProcessFlowChartFlowRelation] where PROCESSFLOWCHART_Id=:flowChartId");
            var session = SessionFactory.OpenSession();
            var list = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowChartFlowRelation))
                .SetString("flowChartId", flowChartId)
                .List<DaoModel.ProcessFlowChartFlowRelation>();

            //工艺图工序制作站
            //  var idIndex = 1;
            foreach (var r in list)
            {
                var rm = Utils.Reflection.BeanUitls<DaoModel.ProcessFlowChartFlowRelationModel, DaoModel.ProcessFlowChartFlowRelation>.Mapper(r);
                rm.ParentId = "0";
                rm.ProcessflowIdOrStatingId = rm.ProcessFlow?.Id;
                rm.Id = r.Id;
                rm.IsChind = false;
                rm.IsEn = Convert.ToBoolean(r.IsEnabled);
                rm.IsMergeForw = null == r.IsMergeForward ? false : r.IsMergeForward.Value;
                string itemSql = string.Format("select * from [dbo].[ProcessFlowStatingItem] where PROCESSFLOWCHARTFLOWRELATION_Id=:ProcessflowchartflowrelationId");
                var sessionItem = SessionFactory.OpenSession();
                var listItem = sessionItem.CreateSQLQuery(itemSql).AddEntity(typeof(DaoModel.ProcessFlowStatingItem))
                    .SetString("ProcessflowchartflowrelationId", r.Id)
                    .List<DaoModel.ProcessFlowStatingItem>();
                foreach (var im in listItem)
                {
                    pfcRelationList.Add(new DaoModel.ProcessFlowChartFlowRelationModel()
                    {
                        ProcessflowIdOrStatingId = im.Stating?.Id,
                        IsStating = true,
                        IsChind = true,
                        StatingNo = im.No?.Trim(),
                        GroupNo = im.SiteGroupNo?.Trim(),
                        CraftFlowNo = string.Format("{0}-{1}", im.SiteGroupNo?.Trim(), im.No?.Trim()),
                        IsEn = Convert.ToBoolean(im.IsReceivingHanger),
                        FlowNo = im.Proportion?.ToString(),
                        FlowCode = im.StatingRoleName,
                        ParentId = rm.Id,
                        Id = im.Id,
                        IsEndStating = im.IsEndStating,
                        IsReceivingAllSize = im.IsReceivingAllSize,
                        IsReceivingAllColor = im.IsReceivingAllColor,
                        IsReceivingAllPoNumber = im.IsReceivingAllPoNumber,
                        ReceivingSize = im.ReceivingSize,
                        ReceivingColor = im.ReceivingColor,
                        ReceivingPoNumber = im.ReceivingPoNumber,
                        //颜色，尺码，是否收尾站，Po
                        FlowName = string.Format("{0} {1} {2} {3}",
                            !string.IsNullOrEmpty(im.ReceivingColor) ? "颜色:" + im.ReceivingColor : "",
                            !string.IsNullOrEmpty(im.ReceivingSize) ? "尺码:" + im.ReceivingSize : "",
                            im.IsEndStating.HasValue && im.IsEndStating.Value ? "收尾站：是" : "",
                            !string.IsNullOrEmpty(im.ReceivingPoNumber) ? "PO号:" + im.ReceivingPoNumber : "")
                    });
                }
                pfcRelationList.Add(rm);
            }
            return pfcRelationList;
        }

        /// <summary>
        /// 根据工艺图Id获取工艺图制作组
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        public IList<DaoModel.ProcessFlowChartGrop> GetProcessFlowChartGroupList(string flowChartId)
        {
            //工艺图制作组
            var groupSql = string.Format(@"select * from ProcessFlowChartGrop where PROCESSFLOWCHART_Id=:flowChartId");
            var sessionFlowChartGroup = SessionFactory.OpenSession();
            var groupList = sessionFlowChartGroup.CreateSQLQuery(groupSql).AddEntity(typeof(DaoModel.ProcessFlowChartGrop))
                .SetString("flowChartId", flowChartId)
                .List<DaoModel.ProcessFlowChartGrop>();

            return groupList;
        }
        public string GetCurrentMaxProcessFlowChartNo(string processFlowVersionId)
        {
            var processFlowChartDao = Dao.ProcessFlowChartDao.Instance;
            var isExist = processFlowChartDao.CheckIsExist(new System.Collections.Hashtable());
            if (!isExist)
            {
                return "1";
            }
            var maxNum = GetMaxProcessFlowChartNo(processFlowVersionId);
            return (maxNum + 1).ToString();
            //var isExt=
        }
        public long GetMaxProcessFlowChartNo(string processFlowVersionId)
        {
            string queryString = string.Format("select ISNULL(MAX(pFlowChartNum),0) currentMaxNum from [dbo].[ProcessFlowChart] where PROCESSFLOWVERSION_Id=:PROCESSFLOWVERSION_Id");
            var session = SessionFactory.OpenSession();
            var reslt = session.CreateSQLQuery(queryString).SetString("PROCESSFLOWVERSION_Id", processFlowVersionId).UniqueResult();
            return Convert.ToInt64(reslt);
        }
        public bool CheckProcessFlowChartNameIsExist(string lineName)
        {
            var ht = new Hashtable();
            ht.Add("LinkName", lineName?.Trim());
            return Dao.ProcessFlowChartDao.Instance.CheckIsExist(ht);
        }
        public IList<DaoModel.ProcessOrderColorItem> GetProcessOrderColorList(string pOrderId, ref IList<string> sizeList)
        {
            var pOrderColorItemSQL = string.Format(@"select * from ProcessOrderColorItem where PROCESSORDER_Id=:pOrderId");
            var session = SessionFactory.OpenSession();
            var processOrderColorItemList = session.CreateSQLQuery(pOrderColorItemSQL).AddEntity(typeof(DaoModel.ProcessOrderColorItem))
                .SetString("pOrderId", pOrderId)
                .List<DaoModel.ProcessOrderColorItem>();
            sizeList = new List<string>();
            foreach (var pocItem in processOrderColorItemList)
            {
                var pocsSizeList = session.CreateSQLQuery("select * from ProcessOrderColorSizeItem where PROCESSORDERCOLORITEM_Id=:PROCESSORDERCOLORITEM_Id").AddEntity(typeof(DaoModel.ProcessOrderColorSizeItem))
                .SetString("PROCESSORDERCOLORITEM_Id", pocItem.Id)
                .List<DaoModel.ProcessOrderColorSizeItem>();
                foreach (var ps in pocsSizeList)
                {
                    if (!sizeList.Contains(ps.SizeDesption?.Trim()))
                    {
                        sizeList.Add(ps.SizeDesption?.Trim());
                    }
                }
            }
            return processOrderColorItemList;
        }
        public DaoModel.ProcessFlowVersion GetProcessFlowVerson(string flowChartId)
        {
            var sql = string.Format("select * from ProcessFlowVersion where Id in(select PROCESSFLOWVERSION_Id from ProcessFlowChart where id='{0}')", flowChartId);
            return QueryForObject<DaoModel.ProcessFlowVersion>(sql, true, null);
        }

        /// <summary>
        /// 通过制单版本号获取PO列表
        /// </summary>
        /// <param name="processFlowVersionId"></param>
        /// <returns></returns>
        public List<string> GetPoList(string processFlowVersionId)
        {
            string sql = @"select a.PurchaseOrderNo
                        from CustomerPurchaseOrder a
                        inner join ProcessOrderColorItem b on a.Id = b.CUSTOMERPURCHASEORDER_Id
                        inner join ProcessOrder c on c.Id = b.PROCESSORDER_Id
                        inner join ProcessFlowVersion d on d.PROCESSORDER_Id = c.Id
                        where b.Id is not null and c.Id is not null and d.Id is not null
                        and d.id = @Id";

            return DapperHelp.Query<string>(sql, new { Id = processFlowVersionId }).ToList();
        }

        public IList<DaoModel.ProcessFlowChart> GetProcessFlowChartListByOnlineProducts(string GroupNo)
        {
            var sql = string.Format(@"select * from ProcessFlowChart where Id in(
select PROCESSFLOWCHART_Id from Products where GroupNo=@GroupNo
)");
            var list = DapperHelp.Query<DaoModel.ProcessFlowChart>(sql, new { GroupNo = GroupNo }).ToList();
            return list;
        }

        public string GetOnlineFlowChartId(string groupNo)
        {
            var sql = string.Format(@"select * from ProcessFlowChart where Id in(
select PROCESSFLOWCHART_Id from Products where GroupNo=@GroupNo AND Status=2
)");
            var rs = DapperHelp.QueryForObject<DaoModel.ProcessFlowChart>(sql, new { GroupNo = groupNo });
            return rs?.Id;
        }
    }
}


