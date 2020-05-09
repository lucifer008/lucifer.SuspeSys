using System;
using System.Text;
using System.Collections.Generic;
using SuspeSys.Support.Enums;
using SuspeSys.Domain.SusEnum;

namespace SuspeSys.Domain {
    
    /// <summary>
    /// 用户操作日志 扩展Model
    /// </summary>
    [Serializable]
    public class UserOperateLogsModel : UserOperateLogs {

        public UserOperateLogsModel() {
            this.OperateLogDetailModels = new List<UserOperateLogDetailModel>();
        }
        public string OperatorTypeName {
            get
            {
                OperateType os = (OperateType)base.OpType;
                return os.Description();
            }
        }

        public List<UserOperateLogDetailModel> OperateLogDetailModels { get; set; }
    }
}
