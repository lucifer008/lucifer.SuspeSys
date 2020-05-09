//using log4net;
using log4net;
using log4net.Config;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using System.IO;
using System.Xml;

namespace SuspeSys.Dao.Nhibernate
{
    public class SessionFactory
    {
        private SessionFactory() { }
        static string hibernaterConfigPath;
        protected static ISessionFactory sessionFactory = null;// new Configuration().Configure(hibernaterConfigPath).BuildSessionFactory();
        //private static ILog log = LogManager.GetLogger(typeof(SessionFactory));

        //Several functions omitted for brevity
        static SessionFactory()
        {

        }
        public static ISession OpenSession()
        {
            ////if (null == sessionFactory)
            ////    sessionFactory = GetSessionFactory();
            //if (!CurrentSessionContext.HasBind(sessionFactory))
            //    CurrentSessionContext.Bind(sessionFactory.OpenSession(new SQLWatcher()));
            //////var currentSession = GetSessionFactory().GetCurrentSession();

            ////if (!sessionFactory.GetCurrentSession().IsOpen)
            ////    return sessionFactory.OpenSession(new SQLWatcher());
            var currentSession = GetCurrentSession();
            if (null != currentSession)
            {
                return currentSession;
            }
            var session = sessionFactory.OpenSession(new SQLWatcher());
            if (!CurrentSessionContext.HasBind(sessionFactory))
                CurrentSessionContext.Bind(session);
            ////    currentSession.
            return session; //sessionFactory.OpenSession(new SQLWatcher());
        }
        public static ISession GetCurrentSession()
        {
            if (!CurrentSessionContext.HasBind(sessionFactory))
                return null;
            var sess = sessionFactory.GetCurrentSession();
            if (null != sess && !sess.IsOpen)
            {
                var session = sessionFactory.OpenSession(new SQLWatcher());
                CurrentSessionContext.Bind(session);
            }
            return sessionFactory.GetCurrentSession();
        }

        private static ISessionFactory GetSessionFactory()
        {
            return new Configuration().Configure().BuildSessionFactory();
        }

        public static void DisposeCurrentSession()
        {
            ISession currentSession = CurrentSessionContext.Unbind(GetSessionFactory());

            currentSession.Close();
            currentSession.Dispose();
        }
        static string BaseApplicationPath;
        static bool IsStartService;
        public static void Init(string baseApplicationPath = null, bool isStartService = false)
        {
            BaseApplicationPath = baseApplicationPath;
            IsStartService = isStartService;

            var log4netFileInfo = new FileInfo("Config/log4net.cfg.xml");
            XmlConfigurator.Configure(log4netFileInfo);
            var log = LogManager.GetLogger("LogLogger");

            log.Info("info---->hibernate init start....");
            FileInfo nhFileInfo = null;
            if (!IsStartService)
                nhFileInfo = new FileInfo("Config/hibernate.cfg.xml");
            else
                nhFileInfo = new FileInfo(string.Format("{0}\\Config\\hibernate.cfg.xml", BaseApplicationPath));

            if (!nhFileInfo.Exists)
            {
                log.Info("文件不存在!");
                log.Info("配置文件路径:" + BaseApplicationPath);
            }
            hibernaterConfigPath = nhFileInfo.FullName;
            if (null == sessionFactory)
            {
                sessionFactory = new Configuration().Configure(hibernaterConfigPath).BuildSessionFactory();

                //XmlDocument myDoc = new XmlDocument();
                //myDoc.Load(hibernaterConfigPath);
                //XmlNamespaceManager nsMgr = new XmlNamespaceManager(myDoc.NameTable); nsMgr.AddNamespace("ns", "urn:nhibernate-configuration-2.2");
                //XmlNode myNode = myDoc.SelectSingleNode("//ns:session-factory", nsMgr);//查找appSettings结点
                //var myXmlElement = (XmlElement)myNode.SelectSingleNode("//ns:property [@name='connection.connection_string']", nsMgr);//查找add结点中key=sql的结点
                //var config = GetConfig(myXmlElement.InnerText);
                //sessionFactory = config.BuildSessionFactory();


                //sessionFactory = new Configuration().Configure(hibernaterConfigPath).BuildSessionFactory();
                //sessionFactory = new Configuration().AddDocument(myDoc).BuildSessionFactory();
                //using (var reader = XmlReader.Create(hibernaterConfigPath))
                //{
                //    //sessionFactory = new Configuration().Configure(hibernaterConfigPath).BuildSessionFactory();
                //    sessionFactory = new Configuration().Configure(reader).BuildSessionFactory();
                //}
            }
            //跟踪nhibernate执行的sql配置
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            log.Info("info---->hibernate init sucess!");
        }
        public static NHibernate.Cfg.Configuration GetConfig(string setConnfig)
        {


            NHibernate.Cfg.Configuration config = new NHibernate.Cfg.Configuration();
            config.SetProperty("hibernate.connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            config.SetProperty("hibernate.connection.driver_class", "NHibernate.Driver.SqlClientDriver");
            config.SetProperty("hibernate.connection.connection_string", setConnfig);
            config.SetProperty("hibernate.dialect", "NHibernate.Dialect.MsSql2008Dialect,NHibernate");
            //config.SetProperty("hibernate.use_outer_join", "true");
            config.SetProperty("hibernate.show_sql", "true");
           // config.SetProperty("proxyfactory.factory_class", "NHibernate.ByteCode.LinFu.ProxyFactoryFactory, NHibernate.ByteCode.LinFu");
            config.AddAssembly("SuspeSys.Domain");
            //config.AddAssembly("SuspeSys.Dao");
            return config;

        }


    }
}
