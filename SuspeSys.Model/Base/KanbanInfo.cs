using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain
{
    /// <summary>
    /// lcd看板
    /// </summary>
    public class KanbanInfo
    {
        /// <summary>
        /// LogId
        /// </summary>
        public string LogId { get; set; }
        /// <summary>
        /// 车间编号
        /// </summary>
        public string WorkShop { get; set; }
        /// <summary>
        /// 组编号
        /// </summary>
        public string GroupNo { get; set; }

        /// <summary>
        /// 站点编号
        /// </summary>
        public string StationNo { get; set; }
        /// <summary>
        /// 报修时间
        /// </summary>
        public DateTime CallTime { get; set; }
        /// <summary>
        /// 机修姓名
        /// </summary>
        public string Mechanic { get; set; }
        /// <summary>
        /// 故障代码
        /// </summary>
        public string FaultCode { get; set; }
        /// <summary>
        /// 故障名称
        /// </summary>
        public string Fault { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime InsertDateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }
    }
}
