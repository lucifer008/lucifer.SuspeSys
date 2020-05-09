using NHibernate.Type;
using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Service.BasicInfo;
using SuspeSys.Service.Impl.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain;
using SuspeSys.Domain.Cus;

namespace SuspeSys.Service.Impl.BasicInfo
{
    public class BasicInfoServiceImpl : ServiceBase, IBasicInfoService
    {
        public void AddStyleProcessFlow(SuspeSys.Domain.Style style,IList<SuspeSys.Domain.StyleProcessFlowSectionItem> styleProcessFlowSectionItemList)
        {
           // if (styleProcessFlowSectionItemList.Count == 0) return;
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var styleId=StyleDao.Instance.Save(style, true);
                    foreach (var spf in styleProcessFlowSectionItemList)
                    {
                        spf.Style = style;
                        StyleProcessFlowSectionItemDao.Instance.Save(spf,true);
                    }
                    session.Flush();
                    trans.Commit();
                }
            }
        }

        public IList<PoColor> SearchBasicColorTable(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from PoColor T1 where Deleted <> 1 ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                //尺码及描述
                queryString += string.Format(@" AND (T1.ColorValue like ? OR T1.ColorDescption like ?) ");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey)};
            }
            var rslt1 = Query<PoColor>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<BasicProcessFlow> SearchBasicProcessLirbary(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from BasicProcessFlow T1 where Deleted <> 1 ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                //工序代码及工序名称
                queryString += string.Format(@" AND (T1.ProcessCode like ? OR T1.ProcessName like ?) ");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<BasicProcessFlow>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<DefectCodeTable> SearchDefectCodeTable(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from DefectCodeTable T1 where Deleted <> 1 ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                //工序段代码及工序段名称
                queryString += string.Format(@" AND (T1.DefectCode like ? OR T1.DefectName like ?) ");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<DefectCodeTable>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<LackMaterialsTable> SearchLackMaterialsTable(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from LackMaterialsTable T1 where Deleted <> 1 ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                //工序段代码及工序段名称
                queryString += string.Format(@" AND (T1.LackMaterialsCode like ? OR T1.LackMaterialsName like ?) ");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<LackMaterialsTable>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<ProcessFlowSection> SearchProcessFlowSection(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from ProcessFlowSection T1 where Deleted <> 1  ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                //工序段代码及工序段名称
                queryString += string.Format(@" AND (T1.ProSectionCode like ? OR T1.ProSectionName like ?) ");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<ProcessFlowSection>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<PSize> SearchPSize(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from PSize T1 where Deleted <> 1 ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                //尺码代码及名称
                queryString += string.Format(@" AND (T1.Size like ? OR T1.SizeDesption like ?) ");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<PSize>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<Style> SearchStyle(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from Style T1 where Deleted <> 1 ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                //尺码代码及名称
                queryString += string.Format(@" AND (T1.StyleNo like ? OR T1.StyleName like ?) ");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<Style>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public IList<StyleProcessFlowSectionItemExtModel> SearchStyleProcessFlow(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select T1.Id As StyleId2,T1.StyleNo,T1.StyleName,T2.* from Style T1 left Join StyleProcessFlowSectionItem T2 ON T1.Id=T2.STYLE_Id where 1=1 ";
            string[] paramValues = null;
            if (!string.IsNullOrEmpty(searchKey))
            {
                //输入客户编号、名称、订单、颜色、尺码查询
                queryString += string.Format(@" AND (T1.StyleNo like ? OR T1.StyleName like ? OR T2.ProcessCode like ? OR T2.ProcessName like ?) ");
                paramValues = new string[] { string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey), string.Format("%{0}%", searchKey),
                    string.Format("%{0}%", searchKey)};
            }
            var rslt1 = Query<StyleProcessFlowSectionItemExtModel>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, true, paramValues);
            return rslt1;
        }

        public void UpdateStyleProcessFlow(SuspeSys.Domain.Style style, IList<SuspeSys.Domain.StyleProcessFlowSectionItem> styleProcessFlowSectionItemList)
        {
           // if (styleProcessFlowSectionItemList.Count == 0) return;
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Clear();
                    StyleDao.Instance.Update(style, true);
                    session.Delete("from StyleProcessFlowSectionItem where STYLE_Id=?",
                      new object[] { style.Id }, new IType[] { NHibernate.NHibernateUtil.String });
                    foreach (var spf in styleProcessFlowSectionItemList)
                    {
                        StyleProcessFlowSectionItemDao.Instance.Save(spf, true);
                    }
                    session.Flush();
                    trans.Commit();
                }
            }
        }
    }
}
