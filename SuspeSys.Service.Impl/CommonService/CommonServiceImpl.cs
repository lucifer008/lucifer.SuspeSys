using SuspeSys.Dao;
using SuspeSys.Service.CommonService;
using SuspeSys.Service.Impl.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SuspeSys.Service.Impl.CommonService
{
    /// <summary>
    /// 针对T的增、删、改、查
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonServiceImpl<T> : ServiceBase, ICommonService<T>
    {
        /// <summary>
        /// 针对单表查询
        /// </summary>
        /// <returns></returns>
        public IList<T> GetList()
        {
            var t = typeof(T);
            var daoFullName = string.Format(" SuspeSys.Dao.{0}Dao", t.Name);
            Type type = Type.GetType(daoFullName);
            var assem = Assembly.Load("SuspeSys.Dao");
            var daoType = assem.GetType(daoFullName);
            var daoInstance = daoType.GetProperty("Instance").GetValue(null);
            // dynamic daoInstance = assem.GetCustomAttribute(daoType);
            // var daoInstance= ReflectionUtils<T>.GetInstance(daoFullName);
            var methodInfo = daoInstance.GetType().GetMethod("GetAll", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var rslt = methodInfo.Invoke(daoInstance, null);
            return (List<T>)rslt;
        }

        public T QueryObjectBySql(string sql, object parames)
        {
            return DapperHelp.QueryForObject<T>(sql, parames);
        }

        public IEnumerable<T> QueryBySql(string sql,object parames=null)
        {
            return DapperHelp.Query<T>(sql, parames);
        }

        public int UpdateBySql(string sql, object parames = null)
        {
            return DapperHelp.Edit(sql, parames);
        }

        /// <summary>
        /// 针对单条记录
        /// </summary>
        /// <returns></returns>
        public T Get(string id)
        {
            var t = typeof(T);
            var daoFullName = string.Format(" SuspeSys.Dao.{0}Dao", t.Name);
            Type type = Type.GetType(daoFullName);
            var assem = Assembly.Load("SuspeSys.Dao");
            var daoType = assem.GetType(daoFullName);
            var daoInstance = daoType.GetProperty("Instance").GetValue(null);
            // dynamic daoInstance = assem.GetCustomAttribute(daoType);
            // var daoInstance= ReflectionUtils<T>.GetInstance(daoFullName);
            var methodInfo = daoInstance.GetType().GetMethod("GetById", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var rslt = methodInfo.Invoke(daoInstance, new object[] { id });
            return (T)rslt;
        }
        /// <summary>
        /// 懒加载获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T LoadById(string id)
        {
            var t = typeof(T);
            var daoFullName = string.Format(" SuspeSys.Dao.{0}Dao", t.Name);
            Type type = Type.GetType(daoFullName);
            var assem = Assembly.Load("SuspeSys.Dao");
            var daoType = assem.GetType(daoFullName);
            var daoInstance = daoType.GetProperty("Instance").GetValue(null);
            // dynamic daoInstance = assem.GetCustomAttribute(daoType);
            // var daoInstance= ReflectionUtils<T>.GetInstance(daoFullName);
            var methodInfo = daoInstance.GetType().GetMethod("LoadById", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var rslt = methodInfo.Invoke(daoInstance, new object[] { id });
            return (T)rslt;
        }

        /// <summary>
        /// 针对单表的分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IList<T> LoadByPage(int pageIndex, int pageSize, out int totalCount)
        {
            var t = typeof(T);
            var daoFullName = string.Format(" SuspeSys.Dao.{0}Dao", t.Name);
            Type type = Type.GetType(daoFullName);
            var assem = Assembly.Load("SuspeSys.Dao");
            var daoType = assem.GetType(daoFullName);
            var daoInstance = daoType.GetProperty("Instance").GetValue(null);
            // dynamic daoInstance = assem.GetCustomAttribute(daoType);
            // var daoInstance= ReflectionUtils<T>.GetInstance(daoFullName);
            var methodInfo = daoInstance.GetType().GetMethod("LoadByPage", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var paramsValues = new object[] { pageIndex, pageSize, null };
            var rslt = methodInfo.Invoke(daoInstance, paramsValues);
            totalCount = Convert.ToInt32(paramsValues[2]);
            return (IList<T>)rslt;
        }
        /// <summary>
        /// 针对单表写入
        /// </summary>
        /// <param name="model"></param>
        public void Save(T model)
        {
            //var t = typeof(T);
            //var daoFullName = string.Format(" SuspeSys.Dao.{0}Dao", t.Name);
            //Type type = Type.GetType(daoFullName);
            //var assem = Assembly.Load("SuspeSys.Dao");
            //var daoType = assem.GetType(daoFullName);
            //var daoInstance = daoType.GetProperty("Instance").GetValue(null);
            //// dynamic daoInstance = assem.GetCustomAttribute(daoType);
            //// var daoInstance= ReflectionUtils<T>.GetInstance(daoFullName);
            //var methodInfo = daoInstance.GetType().GetMethod("Save", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            //methodInfo.Invoke(daoInstance, new object[] { model, false });
            this.Save(model, false);

        }

        /// <summary>
        /// 多表同时操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="includeInTransaction">是否包含在事物中</param>
        public void Save(T model, bool includeInTransaction = false)
        {
            var t = typeof(T);
            var daoFullName = string.Format(" SuspeSys.Dao.{0}Dao", t.Name);
            Type type = Type.GetType(daoFullName);
            var assem = Assembly.Load("SuspeSys.Dao");
            var daoType = assem.GetType(daoFullName);
            var daoInstance = daoType.GetProperty("Instance").GetValue(null);
            // dynamic daoInstance = assem.GetCustomAttribute(daoType);
            // var daoInstance= ReflectionUtils<T>.GetInstance(daoFullName);
            var methodInfo = daoInstance.GetType().GetMethod("Save", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            methodInfo.Invoke(daoInstance, new object[] { model, includeInTransaction });

        }
        /// <summary>
        /// 针对单表写入
        /// </summary>
        /// <param name="model"></param>
        public void Update(T model)
        {
            //var t = typeof(T);
            //var daoFullName = string.Format(" SuspeSys.Dao.{0}Dao", t.Name);
            //Type type = Type.GetType(daoFullName);
            //var assem = Assembly.Load("SuspeSys.Dao");
            //var daoType = assem.GetType(daoFullName);
            //var daoInstance = daoType.GetProperty("Instance").GetValue(null);
            //// dynamic daoInstance = assem.GetCustomAttribute(daoType);
            //// var daoInstance= ReflectionUtils<T>.GetInstance(daoFullName);
            //var methodInfo = daoInstance.GetType().GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            //methodInfo.Invoke(daoInstance, new object[] { model, false });

            this.Update(model, false);
        }
        /// <summary>
        /// 针对单表写入
        /// </summary>
        /// <param name="model"></param>
        public int UpdateByDapper(T obj)
        {
            return DapperHelp.Edit<T>(obj);
        }


        /// <summary>
        /// 针对单表写入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="includeInTransaction">是否包含在事物中</param>

        public void Update(T model, bool includeInTransaction = false)
        {
            var t = typeof(T);
            var daoFullName = string.Format(" SuspeSys.Dao.{0}Dao", t.Name);
            Type type = Type.GetType(daoFullName);
            var assem = Assembly.Load("SuspeSys.Dao");
            var daoType = assem.GetType(daoFullName);
            var daoInstance = daoType.GetProperty("Instance").GetValue(null);
            // dynamic daoInstance = assem.GetCustomAttribute(daoType);
            // var daoInstance= ReflectionUtils<T>.GetInstance(daoFullName);
            var methodInfo = daoInstance.GetType().GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            methodInfo.Invoke(daoInstance, new object[] { model, false });
        }
        public bool CheckIsExist(Hashtable hashCondition)
        {
            var t = typeof(T);
            var daoFullName = string.Format(" SuspeSys.Dao.{0}Dao", t.Name);
            Type type = Type.GetType(daoFullName);
            var assem = Assembly.Load("SuspeSys.Dao");
            var daoType = assem.GetType(daoFullName);
            var daoInstance = daoType.GetProperty("Instance").GetValue(null);
            // dynamic daoInstance = assem.GetCustomAttribute(daoType);
            // var daoInstance= ReflectionUtils<T>.GetInstance(daoFullName);
            var methodInfo = daoInstance.GetType().GetMethod("CheckIsExist", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var rs = methodInfo.Invoke(daoInstance, new object[] { hashCondition, });
            return (bool)rs;
        }
        public IList<T> GetAllList(bool transformer = false)
        { 
            if(!transformer){
                return GetList();
            }
            var sql = new StringBuilder(string.Format("select * from {0}", typeof(T).Name.Replace("Model","")));
            return Query<T>(sql, null, transformer, null);
        }
        public void LogicDelete(string id) {
            var t = typeof(T);
            var daoFullName = string.Format(" SuspeSys.Dao.{0}Dao", t.Name);
            Type type = Type.GetType(daoFullName);
            var assem = Assembly.Load("SuspeSys.Dao");
            var daoType = assem.GetType(daoFullName);
            var daoInstance = daoType.GetProperty("Instance").GetValue(null);
            // dynamic daoInstance = assem.GetCustomAttribute(daoType);
            // var daoInstance= ReflectionUtils<T>.GetInstance(daoFullName);
            var methodInfo = daoInstance.GetType().GetMethod("LogicDelete", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            methodInfo.Invoke(daoInstance, new object[] { id ,false});
        }
        public void PhysicsDelte(string id)
        {
            var t = typeof(T);
            var daoFullName = string.Format(" SuspeSys.Dao.{0}Dao", t.Name);
            Type type = Type.GetType(daoFullName);
            var assem = Assembly.Load("SuspeSys.Dao");
            var daoType = assem.GetType(daoFullName);
            var daoInstance = daoType.GetProperty("Instance").GetValue(null);
            // dynamic daoInstance = assem.GetCustomAttribute(daoType);
            // var daoInstance= ReflectionUtils<T>.GetInstance(daoFullName);
            var methodInfo = daoInstance.GetType().GetMethod("Delete", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            methodInfo.Invoke(daoInstance, new object[] { id, false });
        }
    }
}
