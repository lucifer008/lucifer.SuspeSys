using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusCache.Model
{
    public class MainTrackStatingMontorModel
    {
        /// <summary>
        /// 主轨号
        /// </summary>
        public virtual int MainTrackNumber { set; get; }
        /// <summary>
        /// 站号
        /// </summary>
        public virtual int StatingNo { set; get; }

        /// <summary>
        /// 衣架号
        /// </summary>
        public virtual string HangerNo { set; get; }
        /// <summary>
        /// 次数
        /// </summary>
        public virtual int Times { set; get; }

    }
}
