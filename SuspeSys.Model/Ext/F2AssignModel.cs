using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain.Cus;

namespace SuspeSys.Domain.Ext
{
    [Serializable]
    public class F2AssignModel
    {
        /// <summary>
        /// 衣架号
        /// </summary>
        public int HangerNo { set; get; }

        /// <summary>
        /// F2指定源头主轨
        /// </summary>
        public int SourceMainTrackNuber {
            set;get;
        }
        /// <summary>
        /// F2指定源头站点
        /// </summary>
        public int SourceStatingNo
        {
            set; get;
        }
        /// <summary>
        /// F2指定目的主轨
        /// </summary>
        public int TargertMainTrackNumber
        {
            set; get;
        }
        /// <summary>
        /// F2指定目的站点
        /// </summary>
        public int TargertStatingNo
        {
            set; get;
        }
        /// <summary>
        /// 是否是F2指定的发起站点
        /// </summary>
        public bool IsLaunchSourceStating { set; get; }
        /// <summary>
        /// F2指定发起主轨
        /// </summary>
        public int LaunchMainTrackNumber { set; get; }
        /// <summary>
        /// F2指定发起站点
        /// </summary>
        public int LaunchMainStatingNo { set; get; }
        /// <summary>
        /// 当前衣架工序状态
        /// </summary>
        public CurrentHangerProductingFlowModel CurrentNonFlow { get; set; }

        /// <summary>
        /// F2指定状态：0：F2指定;1:F2返回;-1:F2指定进站
        /// </summary>
        public int F2AssignTag { set; get; }

    }
}
