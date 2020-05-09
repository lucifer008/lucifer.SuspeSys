using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Service.Common;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Support.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuspeSys.Utils;
using SuspeSys.Service.Impl.CommonService;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Utils.Reflection;

namespace SuspeSys.Service.Impl.Common
{
    public class UserOpLogServiceImpl : ServiceBase, IUserOpLogService
    {
        public void UpdateLog<T>(string id, T newModel, string formName, string formCode, string opTableCode, bool includeInTransaction = true)
        {

            try
            {
                //修改数据
                CommonServiceImpl<T> common = new CommonServiceImpl<T>();

                var orgModel = common.Get(id);
                SessionFactory.GetCurrentSession().Evict(orgModel);

                if (orgModel == null)
                    throw new SuspeSys.Utils.Exceptions.BusinessException("修改失败，未找到历史数据");

                common.Update(newModel, includeInTransaction);
                //记录日志
                this.UpdateLog<T>(id, orgModel, newModel, formName, formCode, opTableCode);
            }
            catch (Exception ex)
            {

                log.Error(ex);
            }


        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="newModel"></param>
        /// <param name="formName"></param>
        /// <param name="formCode"></param>
        /// <param name="opTableCode"></param>
        public void UpdateLog<T>(string id, T originalModel, T newModel, string formName, string formCode, string opTableCode, bool includeInTransaction = false)
        {
            try
            {
                CommonServiceImpl<T> common = new CommonServiceImpl<T>();


                UserOperateLogs log = new UserOperateLogs()
                {
                    Deleted = 0,
                    OpDataCode = id,
                    OpFormName = formName,
                    OpFormCode = formCode,
                    OpTableCode = opTableCode,
                    OpType = (int)OperateType.Update
                };

                UserOperateLogsDao.Instance.Save(log, includeInTransaction);




                var diffs = originalModel.Diff<T>(newModel);
                if (diffs != null && diffs.Count > 0)
                {
                    var detailList = diffs.Select(o => new UserOperateLogDetail
                    {
                        UserOperateLogs = log,
                        BeforeChange = o.OriginalValue,
                        Changed = o.NewValue,
                        FieldCode = o.PropCode,
                        FieldName = o.PropDescription.Replace(" ", "")

                    }).ToList();

                    detailList.ForEach(o =>
                    {
                        UserOperateLogDetailDao.Instance.Save(o, includeInTransaction);
                    });
                }
            }
            catch (Exception ex)
            {
#warning 记录日志
                log.Error(ex);
            }
            
            //SessionFactory.GetCurrentSession().Flush();

        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="formName"></param>
        /// <param name="formCode"></param>
        /// <param name="opTableCode"></param>
        public void InsertLog<T>(string id, string formName, string formCode, string opTableCode, bool includeInTransaction = false)
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

            UserOperateLogsDao.Instance.Save(log, includeInTransaction);
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="formName"></param>
        /// <param name="formCode"></param>
        /// <param name="opTableCode"></param>
        public void DeleteLog<T>(string id, string formName, string formCode, string opTableCode, bool includeInTransaction = false)
        {
            UserOperateLogs log = new UserOperateLogs()
            {
                Deleted = 0,
                OpDataCode = id,
                OpFormName = formName,
                OpFormCode = formCode,
                OpTableCode = opTableCode,
                OpType = (int)OperateType.Delete
            };

            UserOperateLogsDao.Instance.Save(log, includeInTransaction);
        }

        /// <summary>
        /// 添加错误日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logDetail"></param>
        public void AddLog(UserOperateLogsModel log, bool includeInTransaction = false)
        {

            var UserOperateLogs = BeanUitls<UserOperateLogs, UserOperateLogsModel>.Mapper(log);
            UserOperateLogsDao.Instance.Save(UserOperateLogs, includeInTransaction);

            if (log.OperateLogDetailModels != null)
            {
                log.OperateLogDetailModels.ForEach(o => 
                {
                    o.UserOperateLogs = UserOperateLogs;
                    UserOperateLogDetailDao.Instance.Save(BeanUitls<UserOperateLogDetail, UserOperateLogDetailModel>.Mapper(o), includeInTransaction);
                });
            }
        }
    }
}
