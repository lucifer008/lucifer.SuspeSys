using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusCache.Model
{
    public class MainTrackStatingCacheModel
    {
        /// <summary>
        /// 主轨号
        /// </summary>
        public virtual int MainTrackNumber { set; get; }
        /// <summary>
        /// 站号
        /// </summary>
        public  virtual int StatingNo { set; get; }
        /// <summary>
        /// 站容量
        /// </summary>
        public virtual long Capacity { set; get; }
        /// <summary>
        /// 分配数
        /// </summary>
        public virtual long AllocationNum { set; get; }
        /// <summary>
        /// 在线数/站内数
        /// </summary>
        public virtual long OnLineSum { set; get; }
        /// <summary>
        /// 是否满站
        /// </summary>
        public virtual bool IsFullSite { set; get; }
        /// <summary>
        /// 站类型
        /// </summary>
        public virtual int StatingType { set; get; }
    }
}
