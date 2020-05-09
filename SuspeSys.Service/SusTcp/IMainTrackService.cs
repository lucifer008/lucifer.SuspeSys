using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.SusTcp
{
    /// <summary>
    /// 主轨控制相关服务
    /// </summary>
    public interface IMainTrackService
    {
        /// <summary>
        /// 启动主轨
        /// </summary>
        /// <param name="mainTrackNo"></param>
        bool StartMainTrack(string groupNo,string mainTrackNo,ref string errMsg);
        /// <summary>
        /// 停止主轨
        /// </summary>
        /// <param name="mainTrackNo"></param>
        bool StopMainTrack(string groupNo, string mainTrackNo, ref string errMsg);
        /// <summary>
        /// 急停主轨
        /// </summary>
        /// <param name="mainTrackNo"></param>
        bool EmergencyStopMainTrack(string groupNo, string mainTrackNo, ref string errMsg);
    }
}
