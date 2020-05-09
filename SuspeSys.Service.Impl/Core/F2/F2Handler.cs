using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sus.Net.Common.SusBusMessage;
using System.Net.Sockets;
using SuspeSys.Service.Impl.Core.Cache;

namespace SuspeSys.Service.Impl.Core.F2
{
    public class F2Handler
    {

        public static readonly F2Handler Instance = new F2Handler();
        private F2Handler() { }
        /// <summary>
        /// F2指定Action
        /// </summary>
        /// <param name="request"></param>
        /// <param name="tcpClient"></param>
        internal void Process(F2AssignRequestMessage request, TcpClient tcpClient = null)
        {
            var tag = -1;
            request.HangerNo = NewCacheService.Instance.GetF2AssignLaunchStatingHanger(request.SourceMainTrackNuber, request.SourceStatingNo);
            F2Service.Instance.F2AssgnValid(request.HangerNo, request.SourceMainTrackNuber, request.SourceStatingNo, request.TargertMainTrackNumber, request.TargertStatingNo, ref tag, tcpClient);
            if (tag != 0) return;
            var isCrossMainTrack = request.IsCrossMainTrack;
            F2Service.Instance.F2AssgnAction(request.HangerNo, request.SourceMainTrackNuber, request.SourceStatingNo, request.TargertMainTrackNumber, request.TargertStatingNo, isCrossMainTrack, tcpClient);
            LowerPlaceInstr.Instance.F2AssginOutSite(request.HangerNo, request.TargertMainTrackNumber, request.TargertStatingNo, tag, tcpClient);
        }
        /// <summary>
        /// F2指定主轨衣架上传
        /// </summary>
        /// <param name="request"></param>
        /// <param name="tcpClient"></param>
        internal void Process(F2AssignHangerNoUploadRequestMessage request, TcpClient tcpClient = null)
        {
            F2Service.Instance.F2AssignHangerNoUpload(request.HangerNo, request.SourceMainTrackNuber, request.SourceStatingNo, tcpClient);

        }
    }
}
