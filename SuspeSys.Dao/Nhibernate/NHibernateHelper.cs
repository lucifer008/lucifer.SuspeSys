using log4net;
using log4net.Config;
using NHibernate;
using NHibernate.Cfg;
using System.IO;

namespace SuspeSys.Dao.Nhibernate
{
    public sealed class NHibernateHelper
    {
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory sessionFactory;
        private static ILog log = null;
        static NHibernateHelper()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();

          
            log = LogManager.GetLogger("LogLogger");
            log.Info("info---->hibernate init....");

        }
        public static ISessionFactory GetSessionFactory() {
            return sessionFactory;
        }
        //public static ISession GetCurrentSession()
        //{
        //    HttpContext context = HttpContent.Current;
        //    ISession currentSession = context.Items[CurrentSessionKey] as ISession;

        //    if (currentSession == null)
        //    {
        //        currentSession = sessionFactory.OpenSession();
        //        context.Items[CurrentSessionKey] = currentSession;
        //    }

        //    return currentSession;
        //}

        //public static void CloseSession()
        //{
        //    HttpContext context = HttpContext.Current;
        //    ISession currentSession = context.Items[CurrentSessionKey] as ISession;

        //    if (currentSession == null)
        //    {
        //        // No current session
        //        return;
        //    }

        //    currentSession.Close();
        //    context.Items.Remove(CurrentSessionKey);
        //}

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }
    }

}
