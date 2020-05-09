using Suspe.CAN.Action.CAN;
using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.SusTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.SusTcp
{
    public class MainTrackServiceImpl : ServiceBase, IMainTrackService
    {
        /// <summary>
        /// 启动主轨
        /// </summary>
        /// <param name="mainTrackNo"></param>
        public bool StartMainTrack(string groupNo, string mainTrackNo, ref string errMsg)
        {
            try
            {
                if (null == CANTcp.client && null==CANTcpServer.server)
                {
                    throw new ApplicationException("请检查CAN是否连接正常!");
                }
                if(CANTcp.client!=null)
                    CANTcp.client.StartMainTrack(mainTrackNo);
                if (CANTcpServer.server!=null) {
                    CANTcpServer.server.StartMainTrack(mainTrackNo);
                }
                UpdateMainTrackInfo(int.Parse(MainTrackOperType.Start.Value), groupNo, mainTrackNo);

            }
            catch (Exception ex)
            {
                tcpLogError.Error("启动主轨异常", ex);
                errMsg = ex.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 停止主轨
        /// </summary>
        /// <param name="mainTrackNo"></param>
        public bool StopMainTrack(string groupNo, string mainTrackNo, ref string errMsg)
        {
            try
            {
                if (null == CANTcp.client && CANTcpServer.server==null)
                {
                    throw new ApplicationException("请检查CAN是否连接正常!");
                }
                if(CANTcp.client!=null)
                    CANTcp.client.StopMainTrack(mainTrackNo);
                if (CANTcpServer.server != null)
                    CANTcpServer.server.StopMainTrack(mainTrackNo);

                UpdateMainTrackInfo(int.Parse(MainTrackOperType.Stop.Value), groupNo, mainTrackNo);
            }
            catch (Exception ex)
            {
                tcpLogError.Error("停止主轨异常", ex);
                errMsg = ex.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 急停主轨
        /// </summary>
        /// <param name="mainTrackNo"></param>
        public bool EmergencyStopMainTrack(string groupNo, string mainTrackNo, ref string errMsg)
        {
            try
            {
                if (null == CANTcp.client && CANTcpServer.server == null)
                {
                    throw new ApplicationException("请检查CAN是否连接正常!");
                }
                if(null!=CANTcp.client)
                    CANTcp.client.EmergencyStopMainTrack(mainTrackNo);
                if(null!=CANTcpServer.server)
                    CANTcpServer.server.EmergencyStopMainTrack(mainTrackNo);
                UpdateMainTrackInfo(int.Parse(MainTrackOperType.Emergency.Value), groupNo, mainTrackNo);
            }
            catch (Exception ex)
            {
                tcpLogError.Error("急停主轨异常", ex);
                errMsg = ex.Message;
                return false;
            }
            return true;
        }
        private void UpdateMainTrackInfo(int mType, string groupNo, string mainTrackNo)
        {
            var sql = string.Format("select top 1 * from MainTrack where GroupNo=? AND Num=?");
            var mTrack = QueryForObject<MainTrack>(sql, false, groupNo, mainTrackNo);
            if (mTrack == null)
            {
                //var ex = new ApplicationException(string.Format("组号:{0} 主轨:{1} 不存在!请先配置.", groupNo, mainTrackNo));
                //tcpLogError.Error(ex);
                var mainTrack = new MainTrack();
                mainTrack.GroupNo = groupNo;
                mainTrack.Num = Int16.Parse(mainTrackNo);
                mainTrack.InsertDateTime = DateTime.Now;
                MainTrackDao.Instance.Save(mainTrack);
                mTrack = mainTrack;
                //mTrack = QueryForObject<MainTrack>(sql, false, groupNo, mainTrackNo);
            }
            
            switch (mType)
            {
                case 0://启动
                    mTrack.Status = byte.Parse(MainTrackOperType.Start.Value);
                    mTrack.StartDateTime = DateTime.Now;
                    MainTrackDao.Instance.Update(mTrack);
                    var startMainTrackRecord = new MainTrackOperateRecord()
                    {
                        MainTrack = mTrack,
                        Memo = "启动主轨!",
                        MType = byte.Parse(MainTrackOperType.Start.Value)
                    };
                    MainTrackOperateRecordDao.Instance.Save(startMainTrackRecord);
                    break;
                case 1://急停
                    mTrack.Status = byte.Parse(MainTrackOperType.Emergency.Value);
                    mTrack.EmergencyStopDateTime = DateTime.Now;
                    MainTrackDao.Instance.Update(mTrack);
                    var emergencyRecord = new MainTrackOperateRecord()
                    {
                        MainTrack = mTrack,
                        Memo = "急停主轨!",
                        MType = byte.Parse(MainTrackOperType.Emergency.Value)
                    };
                    MainTrackOperateRecordDao.Instance.Save(emergencyRecord);
                    break;
                case 2://停止
                    mTrack.Status = byte.Parse(MainTrackOperType.Stop.Value);
                    mTrack.StopDateTime = DateTime.Now;
                    MainTrackDao.Instance.Update(mTrack);
                    var stopRecord = new MainTrackOperateRecord()
                    {
                        MainTrack = mTrack,
                        Memo = "停止主轨!",
                        MType = byte.Parse(MainTrackOperType.Stop.Value)
                    };
                    MainTrackOperateRecordDao.Instance.Save(stopRecord);
                    break;
            }
            //  var sql = string.Format("");
            //MainTrackDao.Instance.get
        }
    }
}
