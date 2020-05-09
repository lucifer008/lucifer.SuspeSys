using SuspeSys.Domain;
using SuspeSys.Domain.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.SusTcp
{
    public interface IStatingService
    {
        void SendMsgToMachineProcess(List<StatingMsg> startingMsg);
        void OpenOrCloseMainTrackStatingMonitor(int mainTrackNumber, int statingNo, bool isOpen);
        /// <summary>
        /// 暂停或者接收衣架
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="suspendReceive">1：暂停接收衣架;0:接收衣架</param>
        void SuspendOrReceiveHanger(int mainTrackNo, int statingNo, int suspendReceive);
        /// <summary>
        /// 手动离线站点登录员工--890--------------------------------------
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="type"></param>
        /// <param name="cardNo"></param>
        void ManualEmployeeOffline(int mainTrackNo, int statingNo, int type, int cardNo);
        void UpdateStatingInNum(string groupNo, string statingNo, int inStatingNum);
        void UpdateStatingCapacity(string groupNo, string statingNo, int capacity);

        /// <summary>
        /// 重置站内数
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        void ResetStatingInNum(int mainTrackNumber, int statingNo);
        void UpdateStatingMainboard(int mainTrackNumber, int statingNo, string mainboardNumber);
        void UpdateStatingSN(int mainTrackNumber, int statingNo, string sn);
        /// <summary>
        /// 获取主轨号
        /// </summary>
        /// <param name="groupNo"></param>
        /// <returns></returns>
        int GetMainTrackNumber(string groupNo,string hangingPieceNo=null);
        bool IsHangingPieceStating(int mainTrackNumber, int statingNo);
        void UpdateStatingCache(int mainTrackNumber, int statingNo);
        IList<EmployeeModel> GetStatingGroupEmployeeList(string groupNo, string code, string emName);
        void ManualEmployeeLoginStating(int mainTrackNo, int statingNo, int type, Employee em);
        void LoadOnLineProductsFlowChart(string processFlowChartId);
        /// <summary>
        /// 跟进
        /// </summary>
        /// <param name="groupNo"></param>
        /// <returns></returns>
        IList<Domain.SiteGroup> GetMainTrackNumberList(string groupNo);
       
    }
}
