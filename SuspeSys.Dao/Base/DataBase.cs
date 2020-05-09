using NHibernate;
using NHibernate.Type;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain.Base;
using SuspeSys.Domain.Common;
using SuspeSys.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Dapper;
using System.Diagnostics;
using NHibernate.Cfg;

namespace SuspeSys.Dao.Base
{
    /// <summary>
    /// DB操作基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DataBase<T> : IDataBase<T> where T : class
    {

        protected ISession Session
        {
            get { return SessionFactory.OpenSession(); }
        }

        /// <summary>
        /// 获取所有实体列表
        /// </summary>
        /// <returns></returns>

        public IList<T> GetAll()
        {
            IList<T> list = Session.CreateCriteria<T>().List<T>();
            return list;
            //var sql = string.Format("select * from {0}", typeof(T).Name);
            //var sqlQuery = Session.CreateSQLQuery(sql);
            //var listTran = sqlQuery.SetResultTransformer(new Dao.Nhibernate.BeanTransformerAdapter<T>()).List<T>();
            //return listTran;
        }

        public virtual IQueryOver<T> QueryOver()
        {
            var result = Session.QueryOver<T>();
            return result;
        }

        public T GetById(string id)
        {
            T obj = Session.Get<T>(id);
            return obj;
        }
        /// <summary>
        /// 懒加载获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T LoadById(string id)
        {
            T obj = Session.Load<T>(id);
            return obj;
        }

        /// <summary>
        /// 保存实体到DB,并生成主键ID
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="includeInTransaction"></param>
        /// <returns></returns>
        public string Save(T obj, bool includeInTransaction = false)
        {
            if (obj is MetaData)
            {
                MetaData metaData = obj as MetaData;
                metaData.CompanyId = "c001";
                metaData.Deleted = 0;
                metaData.InsertDateTime = DateTime.Now;
                metaData.InsertUser = UserId;
            }
            var identifier = Session.Save(obj);
            if (!includeInTransaction)
            {
                SessionFactory.GetCurrentSession().Flush();
            }

            return Convert.ToString(identifier);
        }
        ///// <summary>
        ///// 保存实体到DB,并生成主键ID
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="includeInTransaction"></param>
        ///// <returns></returns>
        //public void Save(T obj, object id, bool includeInTransaction = false)
        //{
        //    if (obj is MetaData)
        //    {
        //        MetaData metaData = obj as MetaData;
        //        metaData.CompanyId = "c001";
        //        metaData.Deleted = 0;
        //        metaData.InsertDateTime = DateTime.Now;
        //        metaData.InsertUser = UserId;
        //    }
        //    Session.Save(obj, id);
        //    if (!includeInTransaction)
        //    {
        //        SessionFactory.GetCurrentSession().Flush();
        //    }
        //}
        /// <summary>
        /// sql语句执行Insert
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Insert(T obj) {
            Object id = null;
            if (obj is MetaData)
            {
                MetaData metaData = obj as MetaData;
                metaData.CompanyId = "c001";
                metaData.Deleted = 0;
                metaData.InsertDateTime = DateTime.Now;
                metaData.InsertUser = UserId;
            }
            var queryString = SqlMappingUtils<T>.Instance.GetInsertSql(obj,out id);
            Session.CreateSQLQuery(queryString).ExecuteUpdate();
            return id?.ToString();
        }
        /// <summary>
        /// 更新实体到DB
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="includeInTransaction"></param>
        public void Update(T obj, bool includeInTransaction = false)
        {
            if (obj is MetaData)
            {
                MetaData metaData = obj as MetaData;
                metaData.CompanyId = "c001";
                metaData.UpdateDateTime = DateTime.Now;
                metaData.UpdateUser = UserId;

                if (!metaData.Deleted.HasValue)
                    metaData.Deleted = 0;


            }
            Session.SaveOrUpdate(obj);
            if (!includeInTransaction)
            {
                SessionFactory.GetCurrentSession().Flush();
            }
        }


 
       



        /// <summary>
        /// 物理删除数据，此方法只针对业务中间数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeInTransaction"></param>
        public void Delete(string id, bool includeInTransaction = false)
        {
            var obj = Session.Get<T>(id);
            if (null == obj)
                throw new ApplicationException("删除的id不存在");
            Session.Delete(obj);
            if (!includeInTransaction)
            {
                SessionFactory.GetCurrentSession().Flush();
            }
        }


        /// <summary>
        /// 物理删除数据 
        /// </summary>
        /// <param name="hql"></param>
        /// <param name="includeInTransaction"></param>
        public void DeleteByHql(string hql, bool includeInTransaction = false)
        {
            Session.Delete(hql);

            if (!includeInTransaction)
            {
                SessionFactory.GetCurrentSession().Flush();
            }
        }

        /// <summary>
        /// 物理删除数据 
        /// </summary>
        /// <param name="hql"></param>
        /// <param name="includeInTransaction"></param>
        public void DeleteByHql(string hql, object[] values, IType[] types, bool includeInTransaction = false)
        {
            Session.Delete(hql, values, types);

            if (!includeInTransaction)
            {
                SessionFactory.GetCurrentSession().Flush();
            }
        }

        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeInTransaction"></param>
        public void LogicDelete(string id, bool includeInTransaction = false)
        {
            var sql = string.Format(" Update {0} Set Deleted=1 WHERE ID='{1}'", typeof(T).Name, id);
            Session.CreateSQLQuery(sql).ExecuteUpdate();
        }

        /// <summary>
        /// sql查询
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public IQuery CreateQuery(string queryString)
        {
            return Session.CreateQuery(queryString);
        }

        public ISQLQuery CreateSqlQuery(string queryString)
        {
            return Session.CreateSQLQuery(queryString);
        }

        /// <summary>
        /// 分页支持
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IList<T> LoadByPage(int pageIndex, int pageSize, out int totalCount)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                totalCount = session.QueryOver<T>().RowCount();
                return session.QueryOver<T>().Skip((pageIndex - 1) * pageSize).Take(pageSize).List();
            }
        }
        /// <summary>
        /// 准确查找条件是否存在
        /// </summary>
        /// <param name="hashCondition"></param>
        /// <returns></returns>
        public bool CheckIsExist(Hashtable hashCondition)
        {
            Type t = typeof(T);
            var sql = new StringBuilder();
            sql.AppendFormat("select * from {0} where 1=1 ", t.Name);
            foreach (var key in hashCondition.Keys)
            {
                sql.AppendFormat(" AND {0}='{1}'", key, hashCondition[key]);
            }
            //string.Format("select * from {0}", t.Name);
            using (ISession session = SessionFactory.OpenSession())
            {
                var list = session.CreateSQLQuery(sql.ToString()).AddEntity(typeof(T)).List<T>();
                if (list.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        //Session.CreateCriteria

        private string UserId
        {
            get
            {
                if (CurrentUser.Instance.User != null)
                    return CurrentUser.Instance.UserId;
                else
                    return string.Empty;
            }
        }

        

    }
}
