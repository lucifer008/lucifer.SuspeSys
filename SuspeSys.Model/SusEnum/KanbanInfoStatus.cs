using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    /// <summary>
    /// LCD看板状态
    /// </summary>
    public enum KanbanInfoStatus
    {
        /// <summary>
        ///  待处理
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 已处理
        /// </summary>
        Done = 1,

        /// <summary>
        /// 已经取消
        /// </summary>
        Canceled = 2
    }
}
