using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain.Ext.CANModel;
using SuspeSys.Service.Impl.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Service.Impl.Products
{
    public class CacheService : ServiceBase
    {
        private CacheService() { }
        public static CacheService Instance
        {
            get
            {
                return new CacheService();
            }
        }
        public static IList<CProcessFlowChartModel> CProcessFlowChartModelList;
        public void CacheProcessFlowChart()
        {

            var list = new List<CProcessFlowChartModel>();
            var listProcessFlowChart = GetProcessFlowChartList();
            foreach (var pfc in listProcessFlowChart)
            {
                var model = new CProcessFlowChartModel();
                model.ProcessFlowChart = pfc;
                model.ProcessFlowChartFlowRelationList = GetFlowChartFlowItemList(pfc.Id);
                foreach (var pfcfr in model.ProcessFlowChartFlowRelationList)
                {
                    var pfcStatingList = GetHangerNextProcessFlowStatingList(pfcfr.Id);
                    model.ProcessFlowStatingItemList.AddRange(pfcStatingList.ToArray());
                }
                list.Add(model);
            }
            CProcessFlowChartModelList = list;
        }
        public List<CProcessFlowChartModel> CacheProcessFlowChart(string flowChartId)
        {

            var list = new List<CProcessFlowChartModel>();
            var listProcessFlowChart = GetProcessFlowChartList(flowChartId);
            foreach (var pfc in listProcessFlowChart)
            {
                var model = new CProcessFlowChartModel();
                model.ProcessFlowChart = pfc;
                model.ProcessFlowChartFlowRelationList = GetFlowChartFlowItemList(pfc.Id);
                foreach (var pfcfr in model.ProcessFlowChartFlowRelationList)
                {
                    var pfcStatingList = GetHangerNextProcessFlowStatingList(pfcfr.Id);
                    //model.ProcessFlowStatingItemList.AddRange(pfcStatingList.ToArray());
                    pfcfr.ProcessFlowStatingItemList.AddRange(pfcStatingList.ToArray());
                }
                list.Add(model);
            }
            // CProcessFlowChartModelList = list;
            return list;
        }
        /// <summary>
        /// 获取工艺路线图的列表
        /// </summary>
        /// <returns></returns>
        public IList<DaoModel.ProcessFlowChart> GetProcessFlowChartList()
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlowChart]");
            var session = SessionFactory.OpenSession();
            var list = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowChart)).List<DaoModel.ProcessFlowChart>();
            return list;
        }
        /// <summary>
        /// 获取工艺路线图的列表
        /// </summary>
        /// <returns></returns>
        public IList<DaoModel.ProcessFlowChart> GetProcessFlowChartList(string flowChartId)
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlowChart] where Id=:flowChartId");
            var session = SessionFactory.OpenSession();
            var list = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowChart)).SetString("flowChartId", flowChartId).List<DaoModel.ProcessFlowChart>();
            return list;
        }
        /// <summary>
        /// 获取工艺路线图的所有工序列表
        /// </summary>
        /// <param name="flowChartId"></param>
        /// <returns></returns>
        public IList<DaoModel.ProcessFlowChartFlowRelation> GetFlowChartFlowItemList(string flowChartId)
        {
            string queryString = string.Format("select * from [dbo].[ProcessFlowChartFlowRelation] where PROCESSFLOWCHART_Id=:flowChartId ORDER BY CraftFlowNo");
            var session = SessionFactory.OpenSession();
            var list = session.CreateSQLQuery(queryString).AddEntity(typeof(DaoModel.ProcessFlowChartFlowRelation))
                .SetString("flowChartId", flowChartId)
                .List<DaoModel.ProcessFlowChartFlowRelation>();
            return list;
        }
        /// <summary>
        /// 获取工序站点列表
        /// </summary>
        public IList<DaoModel.ProcessFlowStatingItem> GetHangerNextProcessFlowStatingList(string processFlowChartFlowRelationId)
        {
            var session = SessionFactory.OpenSession();
            var sqlProcessFlowItem = string.Format("select * from ProcessFlowStatingItem where IsReceivingHanger=1 and PROCESSFLOWCHARTFLOWRELATION_Id=:processFlowChartFlowRelationId");
            var statingList = session.CreateSQLQuery(sqlProcessFlowItem)
                .AddEntity(typeof(DaoModel.ProcessFlowStatingItem))
                .SetString("processFlowChartFlowRelationId", processFlowChartFlowRelationId)
                .List<DaoModel.ProcessFlowStatingItem>();
            return statingList;
        }
    }
}
