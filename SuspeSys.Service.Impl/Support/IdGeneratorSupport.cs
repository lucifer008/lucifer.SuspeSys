using NHibernate;
using SuspeSys.Dao.Nhibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Impl.Support
{
    public class IdGeneratorSupport
    {
        private static readonly string DEFAULT_FLAG_NO = "default";
        private static readonly string GLOBAL_FLAG_NO = "global";
      //  private static readonly long NOT_ID = -1;

        private IDictionary type2FlagNo = null;
        public IDictionary Type2FlagNo
        {
            set { type2FlagNo = value; }
        }
        private IDictionary parentFlagno2IdMap = new Hashtable();
        private int cacheSize = 1;

        public int CacheSize
        {
            set { cacheSize = value; }
        }

        private IdGeneratorSupport()
        {
            Hashtable hashtable = new Hashtable();

            hashtable.Add(typeof(DaoModel.ProcessOrder).FullName, "ProcessOrder");
            hashtable.Add(typeof(DaoModel.ProcessFlowVersion).FullName, "ProcessFlowVersion");
            hashtable.Add(typeof(DaoModel.ProcessFlowChart).FullName, "ProcessFlowChart");
            hashtable.Add(typeof(DaoModel.Products).FullName, "Products");

            type2FlagNo = Hashtable.Synchronized(hashtable);
        }
        private static readonly IdGeneratorSupport instance = new IdGeneratorSupport();
        public static IdGeneratorSupport Instance
        {
            get { return instance; }
        }
        public long GetCurrentValue(string flagNo) {
           var session= SessionFactory.OpenSession();
            string sql = string.Format(@"  SELECT CURRENT_VALUE
                    FROM ID_GENERATOR
                    WHERE FLAG_NO = ?");
            var query = session.CreateSQLQuery(sql);
            query.SetParameter(0, flagNo);
            return query.UniqueResult<long>();
        }
        public long NextId(Type type)
        {
            string flagno = GetFlagNo(type.FullName);
            lock (flagno)
            {
                return GenerateNextId(flagno, 0);
            }
        }
        private string GetFlagNo(string fullName)
        {
            string flagno = (string)type2FlagNo[fullName];
            if (flagno == null)
                return DEFAULT_FLAG_NO;
            else
                return flagno;
        }
        private long GenerateNextId(String flagno, long branchShopId)
        {
            if (branchShopId == -1)
                flagno = GLOBAL_FLAG_NO;
            else if (flagno == GLOBAL_FLAG_NO)
                branchShopId = -1;

            IDictionary flagno2Id = null;
            MockSequence sequence = null;
            if (branchShopId != -1)
            {
                flagno2Id = (IDictionary)parentFlagno2IdMap[branchShopId];
                if (flagno2Id == null)
                {
                    flagno2Id = new Hashtable();
                    parentFlagno2IdMap.Add(branchShopId, flagno2Id);
                }
                sequence = (MockSequence)flagno2Id[flagno];
            }
            else
            {
                sequence = (MockSequence)parentFlagno2IdMap[branchShopId];
            }

            if (sequence == null)
            {
                sequence = new MockSequence();
                if (branchShopId == -1)
                    parentFlagno2IdMap.Add(branchShopId, sequence);
                else
                    flagno2Id.Add(flagno, sequence);
            }

            if (sequence.New || sequence.CurrentId >= sequence.MaxId)
            {
                DaoModel.IdGenerator domain = NextId(flagno, cacheSize);
                sequence.CurrentId = domain.CurrentValue;
                sequence.MaxId = domain.CurrentValue + cacheSize;
            }

            long returnValue = sequence.CurrentId;
            sequence.CurrentId++;
            return returnValue;
        }
        private DaoModel.IdGenerator NextId(string flagno, int size)
        {
            ISession session = SessionFactory.OpenSession();
            //using (ISession session = SessionFactory.OpenSession())
            //{
            //using (ITransaction tran = session.BeginTransaction())
            //{
            string sql = string.Format(@"  SELECT *
                    FROM ID_GENERATOR
                    WHERE FLAG_NO = ?");
                    var query = session.CreateSQLQuery(sql);
                    query.SetParameter(0, flagno);
                    var d = query.AddEntity(typeof(DaoModel.IdGenerator)).List<DaoModel.IdGenerator>().SingleOrDefault();
                    var d2 = Dao.IdGeneratorDao.Instance.GetById(d.Id);
                    d2.CurrentValue += size;
                    session.Update(d2);
                    //session.Flush();
                  // tran.Commit();
                    return d;
                //}
            }
        //}
    }
    class MockSequence
    {
        private long currentId;
        public long CurrentId
        {
            get { return currentId; }
            set { currentId = value; }
        }

        private long maxId;
        public long MaxId
        {
            get { return maxId; }
            set { maxId = value; }
        }

        public bool New
        {
            get { return currentId == 0; }
        }
    }
}
