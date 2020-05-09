using System;
using System.Text;
using System.Collections.Generic;


namespace SuspeSys.Domain {
    
    /// <summary>
    /// 流水线 扩展Model
    /// </summary>
    [Serializable]
    public class PipeliningModel : Pipelining {
        public bool Checked { get; set; }

        /// <summary>
        /// 组编号
        /// </summary>
        public virtual string GroupNO { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        public virtual string GroupName { get; set; }

        /// <summary>
        /// 主轨号
        /// </summary>
        public virtual int MainTrackNumber { get; set; }
    }

    public class PipeliningCache
    {
        /// <summary>
        /// 生产线Id
        /// </summary>
        public string PipelingId { get; set; }

        /// <summary>
        /// 主轨
        /// </summary>
        public string MainTrackNumber { get; set; }

        /// <summary>
        /// 站点
        /// </summary>
        public string StatingNo { get; set; }
    }
}
