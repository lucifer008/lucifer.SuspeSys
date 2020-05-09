using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Common
{
    public interface IUserOpLogService
    {/// <summary>
     /// 记录日志
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <param name="id"></param>
     /// <param name="newModel"></param>
     /// <param name="formName"></param>
     /// <param name="formCode"></param>
     /// <param name="opTableCode"></param>
        void UpdateLog<T>(string id, T newModel, string formName, string formCode, string opTableCode, bool includeInTransaction = true);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="newModel"></param>
        /// <param name="formName"></param>
        /// <param name="formCode"></param>
        /// <param name="opTableCode"></param>
        void UpdateLog<T>(string id, T OriginalModel, T newModel, string formName, string formCode, string opTableCode, bool includeInTransaction = true);

        /// <summary>
        /// InsertLog
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="formName"></param>
        /// <param name="formCode"></param>
        /// <param name="opTableCode"></param>
        void InsertLog<T>(string id, string formName, string formCode, string opTableCode, bool includeInTransaction = false);

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="formName"></param>
        /// <param name="formCode"></param>
        /// <param name="opTableCode"></param>
        void DeleteLog<T>(string id, string formName, string formCode, string opTableCode, bool includeInTransaction = false);
    }
}
