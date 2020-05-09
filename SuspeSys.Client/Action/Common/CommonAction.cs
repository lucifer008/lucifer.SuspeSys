
using SuspeSys.Client.ViewModels;
using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Common;
using SuspeSys.Service.Impl.Common;
using SuspeSys.Service.Impl.CommonService;
using SuspeSys.Service.Impl.Support;
using SuspeSys.Support.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Action.Common
{

    public class CommonAction : BaseAction
    {

        public static CommonAction Instance
        {
            get { return new CommonAction(); }
        }
        public IEnumerable<T> QueryBySql<T>(string sql, object parames = null)
        {
            var commonQueryService = new CommonServiceImpl<T>();
            return commonQueryService.QueryBySql(sql, parames);
        }
        public T QueryObjectBySql<T>(string sql, object parames = null)
        {
            var commonQueryService = new CommonServiceImpl<T>();
            return commonQueryService.QueryObjectBySql(sql, parames);
        }
        public IList<SuspeSys.Domain.Style> GetAllStyleList()
        {
            return commonService.GetAllStyleList();
        }
        public IList<SuspeSys.Domain.PoColor> GetAllColorList()
        {
            return commonService.GetColorList();
        }
        public IList<SuspeSys.Domain.PSize> GetSizeList()
        {
            return commonService.GetSizeList();
        }

        //public IList<T> GetList<T>()
        //{
        //    return commonService.GetList<T>();
        //}
        public IList<DaoModel.StyleProcessFlowStoreModel> GetStyleProcessFlowStore()
        {
            return commonService.GetStyleProcessFlowStore();
        }
        public void Save<T>(T t)
        {
            CommonServiceImpl<T> commonQueryService = new CommonServiceImpl<T>();
            commonQueryService.Save(t);
        }

        public void Update<T>(T t)
        {
            CommonServiceImpl<T> commonQueryService = new CommonServiceImpl<T>();
            commonQueryService.Update(t);
        }

        public int Update<T>(string sql, object paob = null)
        {
            //CommonServiceImpl<T> commonQueryService = new CommonServiceImpl<T>();
            return new CommonServiceImpl<T>().Update<T>(sql, paob);
        }
        //public void LogicDelete<T>(string id)
        //{
        //    CommonServiceImpl<T> commonQueryService = new CommonServiceImpl<T>();
        //    commonQueryService.LogicDelete(id);
        //}
        public void LogicDelete<T>(string id)
        {
            new CommonServiceImpl<T>().LogicDelete(id);
        }
        public void PhysicsDelte<T>(string id)
        {
            new CommonServiceImpl<T>().PhysicsDelte(id);
        }
        public T Get<T>(string id)
        {
            return new CommonServiceImpl<T>().LoadById(id);
        }
        public long NextId(Type type)
        {
            return IdGeneratorSupport.Instance.NextId(type);
        }
        public bool CheckIsExist<T>(Hashtable ht)
        {
            var commonQueryService = new CommonServiceImpl<T>();
            return commonQueryService.CheckIsExist(ht);
        }
        public static IList<T> GetList<T>(bool transformer = false)
        {
            var commonQueryService = new CommonServiceImpl<T>();
            if (!transformer)
            {

                return commonQueryService.GetList();
            }
            return commonQueryService.GetAllList(transformer);
        }

        /// <summary>
        /// Dapper查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paob"></param>
        /// <returns></returns>
        public static IList<T> GetList<T>(string sql, object paob = null)
        {
            var commonQueryService = new CommonServiceImpl<T>();

            return commonQueryService.QueryBySql(sql, paob).ToList<T>();
        }

        /// <summary>
        /// Dapper 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int UpdateBySql<T>(string sql, object paras = null)
        {
            var commonQueryService = new CommonServiceImpl<T>();
            return commonQueryService.UpdateBySql(sql, paras);
        }
        /// <summary>
        /// Dapper分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="para"></param>
        /// <param name="orderString"></param>
        /// <returns></returns>
        public IList<T> QueryForList<T>(string sql, int pageIndex, int pageSize, out long totalCount, object para = null, string orderString = null)
        {
            var commonQueryService = new CommonServiceImpl<T>();
            return commonQueryService.QueryForList<T>(sql, pageIndex, pageSize, out totalCount, para, orderString);
        }
        /// <summary>
        /// 创建日志
        /// 添加之后记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formName"></param>
        /// <param name="formCode"></param>
        public void CreateLog<T>(string id, string formName, string formCode, string opTableCode)
        {
            UserOperateLogs log = new UserOperateLogs()
            {
                Deleted = 0,
                OpDataCode = id,
                OpFormName = formName,
                OpFormCode = formCode,
                OpTableCode = opTableCode,
                OpType = (int)OperateType.Create
            };

            Save<UserOperateLogs>(log);
        }

        /// <summary>
        /// Update log
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="newModel"></param>
        /// <param name="formName">页面名</param>
        /// <param name="formCode">页面Code</param>
        /// <param name="opTableCode">操作数据库表名</param>
        public void UpdateLog<T>(string id, T newModel, string formName, string formCode, string opTableCode)
        {
            var service = new UserOpLogServiceImpl();

            service.UpdateLog<T>(id, newModel, formName, formCode, opTableCode);
        }

        /// <summary>
        /// Insert log
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="formName">页面名</param>
        /// <param name="formCode">页面Code</param>
        /// <param name="opTableCode"></param>
        public void InsertLog<T>(string id, string formName, string formCode, string opTableCode)
        {
            var service = new UserOpLogServiceImpl();

            service.InsertLog<T>(id, formName, formCode, opTableCode);
        }

        /// <summary>
        /// Delete log
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="formName">页面名</param>
        /// <param name="formCode">页面Code</param>
        /// <param name="opTableCode"></param>
        public void DeleteLog<T>(string id, string formName, string formCode, string opTableCode)
        {
            var service = new UserOpLogServiceImpl();

            service.DeleteLog<T>(id, formName, formCode, opTableCode);
        }

        /// <summary>
        /// 添加配置信息
        /// </summary>
        /// <param name="profile"></param>
        public void AddApplicationProfile(DaoModel.ApplicationProfile profile)
        {
            IApplicationProfileService service = new ApplicationProfileService();

            service.AddOrUpdate(profile);
        }

        public void AddApplicationProfile(List<DaoModel.ApplicationProfile> profile)
        {
            IApplicationProfileService service = new ApplicationProfileService();
            service.AddOrUpdate(profile);
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public string GetApplicationProfileByName(ApplicationProfileEnum profile)
        {
            IApplicationProfileService service = new ApplicationProfileService();
            return service.GetByName(profile);
        }

        internal void CheckStatingIsExist(BridgeSet m, ref string info)
        {
            commonService.CheckStatingIsExist(m, ref info);
        }

        /// <summary>
        /// 添加或更新客户端授权信息
        /// </summary>
        /// <param name="clientName">客户端信息</param>
        /// <param name="grant">授权信息</param>
        public void OnLineGrantProcess(string clientName, string grant)
        {
            //更新授权信息
            ProductionLineSetService.AddOrUpdateClientMachine(clientName, grant);
        }

        public void SaveEmployeeCardInfo(List<DaoModel.Ext.CardInfoModel> cardInfoModelList)
        {
            _PersonnelManagementService.SaveEmployeeCardInfo(cardInfoModelList);
        }

        public OAuthData GetOAuthData()
        {
            OAuthData authData = new OAuthData();

            authData.ClientId = this.GetApplicationProfileByName(ApplicationProfileEnum.ClientId);
            authData.ClientPwd = this.GetApplicationProfileByName(ApplicationProfileEnum.ClientPwd);
            authData.UserName = this.GetApplicationProfileByName(ApplicationProfileEnum.UserName);
            authData.UserPwd = this.GetApplicationProfileByName(ApplicationProfileEnum.UserPwd);

            return authData;
        }

        /// <summary>
        /// 是否能删除站点
        /// </summary>
        /// <param name="statingId"></param>
        /// <returns></returns>
        public bool CanDeleteStating(string statingId)
        {
            return commonService.CanDeleteStating(statingId);
        }
        /// <summary>
        /// 针对单表写入
        /// </summary>
        /// <param name="model"></param>
        public int UpdateByDapper<T>(T obj)
        {
            return new CommonServiceImpl<T>().UpdateByDapper(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public T GetMaxOrderNo<T>(string columnName, string tableName)
        {
            return commonService.GetMaxOrderNo<T>(columnName, tableName);
        }
    }
}
