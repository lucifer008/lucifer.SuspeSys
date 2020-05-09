using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    public class ProcessOrderFlow {
        /// <summary>
        /// ID
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 制单_Id
        /// </summary>
        public virtual string ProcessOrderId { get; set; }
        /// <summary>
        /// 工序编号
        /// </summary>
        public virtual string ProcessNo { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public virtual string ProcessName { get; set; }
        /// <summary>
        /// 工序代码
        /// </summary>
        public virtual string ProcessCode { get; set; }
        /// <summary>
        /// 工序状态
        ///   0:待走流程
        ///   1:流程已经完成
        ///   
        /// </summary>
        public virtual byte? ProcessStatus { get; set; }
        /// <summary>
        /// 标准工时
        /// </summary>
        public virtual string StanardHours { get; set; }
        /// <summary>
        /// 标准工价
        /// </summary>
        public virtual decimal? StandardPrice { get; set; }
        /// <summary>
        /// 工艺说明
        /// </summary>
        public virtual string PrcocessRemark { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public virtual string ProcessColor { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        public virtual string Size { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateDateTime { get; set; }
        /// <summary>
        /// 插入用户
        /// </summary>
        public virtual string InsertUser { get; set; }
        /// <summary>
        /// 更新用户
        /// </summary>
        public virtual string UpdateUser { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public virtual byte? Deleted { get; set; }
    }
}
