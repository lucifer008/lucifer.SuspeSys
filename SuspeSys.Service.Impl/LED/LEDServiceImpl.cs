using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.LED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Dao;
using NHibernate.Type;

namespace SuspeSys.Service.Impl.LED
{
    public class LEDServiceImpl : ServiceBase, ILEDService
    {
        public void DeleteLEDConfig(string id)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var lpList = LedScreenPageDao.Instance.GetAll().Where(f=>null!= f.LedScreenConfig && f.LedScreenConfig.Id.Equals(id)).Select(k=>new {k.Id });
                    foreach (var lp in lpList) {
                        session.Delete("FROM LedScreenPage WHERE LEDSCREENCONFIG_Id=?", new object[] { id }, new IType[] { NHibernate.NHibernateUtil.String });
                    }
                    session.Delete("FROM LedScreenConfig WHERE Id=?", new object[] { id }, new IType[] { NHibernate.NHibernateUtil.String });
                    session.Flush();
                    trans.Commit();
                }

            }
        }

        public void DeleteLedLedHoursPlanTableIteme(string id)
        {
            var session = SessionFactory.OpenSession();
            session.Delete("FROM LedHoursPlanTableItem WHERE Id=?", new object[] { id }, new IType[] { NHibernate.NHibernateUtil.String });
            session.Flush();
        }

        public void DeleteLedScreenPage(string id)
        {
            var session = SessionFactory.OpenSession();
            session.Delete("FROM LedScreenPage WHERE Id=?", new object[] { id }, new IType[] { NHibernate.NHibernateUtil.String });
            session.Flush();
        }

        public void SaveLedHoursPlanTableItem(List<LedHoursPlanTableItem> ledHoursPlanTableItemList)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    foreach (var lsc in ledHoursPlanTableItemList)
                    {
                        if (string.IsNullOrEmpty(lsc.Id))
                        {
                            LedHoursPlanTableItemDao.Instance.Save(lsc, true);
                        }
                        else
                        {
                            LedHoursPlanTableItemDao.Instance.Update(lsc, true);
                        }
                    }
                    session.Flush();
                    trans.Commit();
                }

            }
        }

        public void SaveLedScreenInfo(IList<LedScreenConfig> ledScreenConfigList)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    foreach (var lsc in ledScreenConfigList)
                    {
                        if (string.IsNullOrEmpty(lsc.Id))
                        {
                            LedScreenConfigDao.Instance.Save(lsc, true);
                        }
                        else
                        {
                            LedScreenConfigDao.Instance.Update(lsc, true);
                        }
                        foreach (var lp in lsc.LedScreenPageList)
                        {
                            lp.LedScreenConfig = lsc;
                            if (string.IsNullOrEmpty(lp.Id))
                            {
                                LedScreenPageDao.Instance.Save(lp, true);
                            }
                            else
                            {
                                LedScreenPageDao.Instance.Update(lp, true);
                            }
                        }
                    }
                    session.Flush();
                    trans.Commit();
                }

            }
        }
    }
}
