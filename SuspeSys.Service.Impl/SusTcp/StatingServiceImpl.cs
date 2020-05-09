using Newtonsoft.Json;
using StackExchange.Redis.DataTypes;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Domain.Common;
using SuspeSys.Domain.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.ProductionLineSet;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using SuspeSys.Service.Impl.SusRedis;

using SuspeSys.Service.SusTcp;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.SusTcp
{
    public class StatingServiceImpl : Base.ServiceBase, IStatingService
    {
        public void SendMsgToMachineProcess()
        {

        }
        public static StatingServiceImpl Instance { get { return new StatingServiceImpl(); } }
        /// <summary>
        /// 重置站内数
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        public void ResetStatingInNum(int mainTrackNumber, int statingNo)
        {
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);
            var key = string.Format("{0}:{1}", mainTrackNumber, statingNo);
            if (dic.ContainsKey(key))
            {
                tcpLogInfo.InfoFormat("清除站内数开始:主轨：{0} 站点:{1}", mainTrackNumber, statingNo);
                dic[key] = 0;
                tcpLogInfo.InfoFormat("清除站内数完成:主轨：{0} 站点:{1}", mainTrackNumber, statingNo);
                return;
            }
            tcpLogInfo.InfoFormat("站内数无数据!:主轨：{0} 站点:{1}", mainTrackNumber, statingNo);
        }
        /// <summary>
        /// 手动离线站点登录员工
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="type"></param>
        /// <param name="cardNo"></param>
        public void ManualEmployeeOffline(int mainTrackNo, int statingNo, int type, int cardNo)
        {
            if (null != CANTcp.client)
                CANTcp.client.ManualEmployeeOffline(mainTrackNo, statingNo, type, cardNo);
            if (null != CANTcpServer.server)
                CANTcpServer.server.ManualEmployeeOffline(mainTrackNo, statingNo, type, cardNo);
        }
        /// <summary>
        /// 暂停或者接收衣架
        /// </summary>
        /// <param name="mainTrackNo"></param>
        /// <param name="statingNo"></param>
        /// <param name="suspendReceive">1：暂停接收衣架;0:接收衣架</param>
        public void SuspendOrReceiveHanger(int mainTrackNo, int statingNo, int suspendReceive)
        {
            if (null != CANTcp.client)
                CANTcp.client.SuspendOrReceiveHanger(mainTrackNo, statingNo, suspendReceive);
            if (null != CANTcpServer.server)
                CANTcpServer.server.SuspendOrReceiveHanger(mainTrackNo, statingNo, suspendReceive);
        }
        /// <summary>
        /// 监测点设置
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="isOpen"></param>
        public void OpenOrCloseMainTrackStatingMonitor(int mainTrackNumber, int statingNo, bool isOpen)
        {
            if (null != CANTcp.client)
                CANTcp.client.OpenOrCloseMainTrackStatingMonitor(mainTrackNumber, statingNo, isOpen);
            if (null != CANTcpServer.server)
                CANTcpServer.server.OpenOrCloseMainTrackStatingMonitor(mainTrackNumber, statingNo, isOpen);
        }
        public void SendMsgToMachineProcess(List<StatingMsg> startingMsg)
        {
            try
            {
                if (startingMsg == null)
                    return;

                foreach (var item in startingMsg)
                {
                    if (!item.IsEnabled.HasValue || !item.IsEnabled.Value)
                        continue;

                    SendToMathine(item);
                }


            }
            catch (Exception ex)
            {
                tcpLogInfo.Error(ex);
                throw new ApplicationException(string.Format("数据已保存成功!但下位机监测,站类型，容量设置异常:"), ex);
            }
            finally
            {
                //重新加载缓存
                SusCacheProductService.Instance.LoadStating();
            }

        }

        private void SendToMathine(StatingMsg statingMsg)
        {
            if (statingMsg.IsNew)
            {
                //新增
                //1、发送新增站点类型指令
                if (null == CANTcp.client && null == CANTcpServer.server)
                {
                    throw new ApplicationException("请检查CAN是否连接正常!");
                }

                if (string.IsNullOrWhiteSpace(statingMsg.StatingName))
                    return;

                int statingType = StatingType.GetStatingType(statingMsg.StatingName);

                if (CANTcp.client != null)
                    CANTcp.client.SendStatingType(statingMsg.MainTrackNumber, statingMsg.StatingNo, 1, statingType);
                if (CANTcpServer.server != null)
                    CANTcpServer.server.SendStatingType(statingMsg.MainTrackNumber, statingMsg.StatingNo, 1, statingType);
                //2、发送修改站点容量指令
                if (statingMsg.Capacity.HasValue)
                {
                    if (CANTcp.client != null)
                        CANTcp.client.ModifyStatingCapacity(statingMsg.MainTrackNumber, statingMsg.StatingNo, statingMsg.Capacity.Value);
                    if (null != CANTcpServer.server)
                        CANTcpServer.server.ModifyStatingCapacity(statingMsg.MainTrackNumber, statingMsg.StatingNo, statingMsg.Capacity.Value);
                }
            }
            else
            {
                //更新

                if (null == CANTcp.client && null == CANTcpServer.server)
                {
                    throw new ApplicationException("请检查CAN是否连接正常!");
                }

                if (!string.IsNullOrWhiteSpace(statingMsg.StatingName))
                {
                    int statingType = StatingType.GetStatingType(statingMsg.StatingName);
                    if (null != CANTcp.client)
                        CANTcp.client.SendStatingType(statingMsg.MainTrackNumber, statingMsg.StatingNo, 2, statingType);
                    if (null != CANTcpServer.server)
                    {
                        CANTcpServer.server.SendStatingType(statingMsg.MainTrackNumber, statingMsg.StatingNo, 2, statingType);
                    }
                }


                //2、发送修改站点容量指令
                if (statingMsg.Capacity.HasValue)
                {
                    if (null != CANTcp.client)
                        CANTcp.client.ModifyStatingCapacity(statingMsg.MainTrackNumber, statingMsg.StatingNo, statingMsg.Capacity.Value);
                    if (null != CANTcpServer.server)
                    {
                        CANTcpServer.server.ModifyStatingCapacity(statingMsg.MainTrackNumber, statingMsg.StatingNo, statingMsg.Capacity.Value);
                    }
                }
            }
        }

        internal void UpdateStatingSuspendOrReceive(string xID, string id, int tag)
        {
            var mainTrackNumber = HexHelper.HexToTen(xID);
            var statingNo = HexHelper.HexToTen(id);
            string sql = string.Format("Update Stating SET IsReceivingHanger={0} WHERE  MainTrackNumber=@MainTrackNumber AND StatingNo=@StatingNo", tag == 0 ? 1 : 0);
            DapperHelp.Execute(sql, new { MainTrackNumber = mainTrackNumber, StatingNo = statingNo });
        }

        /// <summary>
        /// 更新主板号
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="mainboardNumber">十进制</param>
        public void UpdateStatingMainboard(int mainTrackNumber, int statingNo, string mainboardNumber)
        {
            var sql = string.Format("Update Stating set MainboardNumber=@MainboardNumber where MainTrackNumber=@MainTrackNumber and StatingNo=@StatingNo");
            DapperHelp.Execute(sql, new { MainboardNumber = mainboardNumber, MainTrackNumber = mainTrackNumber, StatingNo = statingNo });
        }
        /// <summary>
        /// 更新SN
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="statingNo"></param>
        /// <param name="sn">十进制</param>
        public void UpdateStatingSN(int mainTrackNumber, int statingNo, string sn)
        {
            var sql = string.Format("Update Stating set SerialNumber=@SerialNumber where MainTrackNumber=@MainTrackNumber and StatingNo=@StatingNo");
            DapperHelp.Execute(sql, new { SerialNumber = sn, MainTrackNumber = mainTrackNumber, StatingNo = statingNo });
        }
        /// <summary>
        /// 获取主轨号
        /// </summary>
        /// <param name="groupNo"></param>
        /// <returns></returns>
        public int GetMainTrackNumber(string groupNo, string hangingPieceNo = null)
        {
            // var sql = "select MainTrackNumber from SiteGroup where GroupNO=@GroupNO";
            var sql = string.Format(@"select Sg.GroupNO,St.MainTrackNumber,st.StatingNo,st.StatingName from SiteGroup SG
						left join Stating ST ON SG.Id=ST.SITEGROUP_Id
						where Sg.GroupNo=@GroupNo And st.StatingNo=@StatingNo");
            if (string.IsNullOrEmpty(hangingPieceNo))
            {
                throw new ApplicationException("站号不能为空!");
            }
            return DapperHelp.QueryForObject<Domain.SiteGroup>(sql, new { GroupNo = groupNo, StatingNo = int.Parse(hangingPieceNo) }).MainTrackNumber.Value;
        }
        public bool IsHangingPieceStating(int mainTrackNumber, int statingNo)
        {
            //var sql = string.Format("select * from Stating where MainTrackNumber=@MainTrackNumber AND StatingNo=@StatingNo");
            //var stating = DapperHelp.QueryForObject<Stating>(sql, new { MainTrackNumber = mainTrackNumber, StatingNo = statingNo });
            //return stating == null;
            return false;
        }
        public void UpdateStatingCache(int mainTrackNumber, int statingNo)
        {
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, StatingModel>(SusRedisConst.STATING_TABLE);
            var key = string.Format("{0}:{1}", mainTrackNumber, statingNo);
            if (dic.ContainsKey(key))
            {
                var statingInfo = ProductionLineSetServiceImpl.Instance.GetStatingList(mainTrackNumber, statingNo);
                dic[key] = statingInfo.First();
            }
            SusCacheProductService.Instance.LoadOnLineProductsFlowChart();
            SusCacheProductService.Instance.UpdateHangerFlowChartCache();

        }
        public bool CheckStatingExist(int mainTrackNumber, int statingNo)
        {
            var sql = string.Format("select Id from Stating where MainTrackNumber=@MainTrackNumber AND StatingNo=@StatingNo");
            var stating = DapperHelp.QueryForObject<Stating>(sql, new { MainTrackNumber = mainTrackNumber, StatingNo = statingNo });
            return stating != null;
        }
        public string GetGroupNo(int mainTrackNumber)
        {
            var sql = string.Format(@" select top 1 GroupNo from Stating s1 
 inner join SiteGroup  sg on s1.SITEGROUP_Id=sg.Id
 where s1.MainTrackNumber=@MainTrackNumber");
            return DapperHelp.QueryForObject<Domain.SiteGroup>(sql, new { MainTrackNumber = mainTrackNumber }).GroupNo;
            //    var sql = "select GroupNo from SiteGroup where MainTrackNumber=@MainTrackNumber";
            //    return DapperHelp.QueryForObject<Domain.SiteGroup>(sql, new { MainTrackNumber = mainTrackNumber }).GroupNo;
        }
        public string GetStatingLogInfo(int mainTrackNumber, int statingNo)
        {
            var sql = string.Format(@"select T4.RealName,T4.Code from CardLoginInfo T1
INNER JOIN CardInfo T2 ON T1.CARDINFO_Id=T2.Id
INNER JOIN EmployeeCardRelation T3 ON T3.CARDINFO_Id=T2.Id
INNER JOIN Employee T4 ON T4.Id=T3.EMPLOYEE_Id
WHERE T1.IsOnline=1 AND T1.LoginDate BETWEEN CONVERT(varchar(10), GETDATE(), 120) AND CONVERT(varchar(10), DATEADD(day, 1, GETDATE()), 120)
AND T1.LoginStatingNo=@statingNo AND T1.MainTrackNumber=@mainTrackNumber");
            var em = DapperHelp.QueryForObject<Employee>(sql, new { mainTrackNumber = mainTrackNumber, statingNo = statingNo });
            if (null == em) return "";
            var result = string.Format("[{0}]{1}", em.Code?.Trim(), em.RealName?.Trim());
            return result;
        }

        public void UpdateStatingInNum(string groupNo, string statingNo, int inStatingNum)
        {
            int manTrackNumber = GetMainTrackNumber(groupNo, statingNo);
            var mainStatingKey = string.Format("{0}:{1}", manTrackNumber, statingNo.Trim());
            var dicStatingInNumCache = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);

            tcpLogInfo.InfoFormat("【站内数维护】-->站内数维护:站点主轨{0}", mainStatingKey);
            tcpLogInfo.InfoFormat("【站内数维护】-->站内数维护:内容{0}", inStatingNum);

            if (dicStatingInNumCache.Keys.Contains(mainStatingKey))
            {
                NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM)[mainStatingKey] = inStatingNum;
            }
            else
            {
                dicStatingInNumCache.Add(mainStatingKey, inStatingNum);
            }
        }

        public void UpdateStatingCapacity(string groupNo, string statingNo, int capacity)
        {
            int mainTrackNumber = GetMainTrackNumber(groupNo, statingNo);
            var sql = string.Format("Update Stating set Capacity=@Capacity WHERE StatingNo=@StatingNo AND MainTrackNumber=@MainTrackNumber");
            DapperHelp.Execute(sql, new { Capacity = capacity, StatingNo = statingNo, MainTrackNumber = mainTrackNumber });
            UpdateStatingCache(mainTrackNumber, int.Parse(statingNo));
        }

        public IList<EmployeeModel> GetStatingGroupEmployeeList(string groupNo, string code, string emName)
        {
            var sql = string.Format(@"select T1.Id,T1.Code,T1.RealName,T2.GroupNo SiteGroupNo from Employee T1 Left Join SiteGroup T2 ON T1.SITEGROUP_Id=T2.Id where 1=1 ");
            if (!string.IsNullOrEmpty(groupNo))
            {
                sql += string.Format(" AND T2.GroupNo like '%{0}%'", groupNo);
            }
            if (!string.IsNullOrEmpty(code))
            {
                sql += string.Format(" AND T1.code like '%{0}%'", code);
            }
            if (!string.IsNullOrEmpty(emName))
            {
                sql += string.Format(" AND T1.RealName like '%{0}%'", emName);
            }
            return DapperHelp.Query<EmployeeModel>(sql, null).ToList<EmployeeModel>();
        }

        public void AdjustmentStatingRoles(string statingId, string statingRoleId, string statingRoleName)
        {
            var sql = string.Format("Update Stating SET STATINGROLES_Id=@StatingRoleId,StatingName=@StatingRoleName where id=@Id");
            DapperHelp.Execute(sql, new { StatingRoleId = statingRoleId, Id = statingId, StatingRoleName = statingRoleName });
        }

        private ProductsQueryServiceImpl productsQueryService = new ProductsQueryServiceImpl();
        public CardInfo GetCardInfo(string emId = null)
        {
            var sql = string.Format(@"select top 1 T3.* from Employee T1 INNER JOIN EmployeeCardRelation T2 ON T1.Id=T2.EMPLOYEE_Id
INNER JOIN CardInfo T3 ON T3.Id=T2.CARDINFO_Id WHERE T3.CardType in(4,5) AND T1.Id=@Id");
            return DapperHelp.QueryForObject<CardInfo>(sql, new { Id = emId });
        }
        public void ManualEmployeeLoginStating(int mainTrackNo, int statingNo, int type, Employee employees)
        {
            var cardInfo = GetCardInfo(employees.Id);
            var info = string.Empty;
            var bToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            var eToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(1);
            var employeeLoginService = new CommonService.CommonServiceImpl<CardLoginInfo>();
            if (null == cardInfo)
            {
                throw new Exception(string.Format("员工{0}无卡号", employees.RealName?.Trim()));
            }
            var logInfoList = productsQueryService.GetEmployeeLoginInfoList(mainTrackNo, statingNo.ToString(), cardInfo.CardNo?.Trim()?.ToString());
            if (0 != logInfoList.Count)
            {
                type = CardRequestType.EmployeeLoginOut.Value;
                var cardLogInfoId = logInfoList[0].Id;
                if (!string.IsNullOrEmpty(cardLogInfoId))
                {
                    var cardLogin = CardLoginInfoDao.Instance.GetById(cardLogInfoId);
                    cardLogin.IsOnline = false;
                    cardLogin.LogOutDate = DateTime.Now;
                    CardLoginInfoDao.Instance.Update(cardLogin);
                }
                CANProductsService.Instance.CompulsionStatingEmployeeOffLine(mainTrackNo, statingNo);
            }
            //根据上下班时间判断下班
            //刷新站点已登录为离线状态,跨天的情况
            CANProductsService.Instance.CompulsionStatingEmployeeOffLine(mainTrackNo, statingNo);

            type = CardRequestType.EmployeeLogined.Value;
            var cardLoginInfo = new CardLoginInfo();
            cardLoginInfo.MainTrackNumber = mainTrackNo;
            cardLoginInfo.CardInfo = cardInfo;
            cardLoginInfo.LoginDate = DateTime.Now;
            cardLoginInfo.LoginStatingNo = statingNo.ToString();
            cardLoginInfo.IsOnline = true;
            CardLoginInfoDao.Instance.Save(cardLoginInfo);
            info = string.Format("[{0}]{1}", employees.RealName?.Trim(), employees.Code?.Trim());

            //临时登录
            if (cardInfo.CardType.Value == 5)
            {
                tcpLogInfo.InfoFormat("员工{0}无卡号登录!", employees.RealName?.Trim());
                var infoLogin = string.Format("[{0}]{1}", employees.Code?.Trim(), employees.RealName?.Trim());
                // var infoLogin = StatingServiceImpl.Instance.GetStatingLogInfo(mainTrackNo, statingNo);
                if (!string.IsNullOrEmpty(infoLogin))
                {
                    var emInfoEncoding = UnicodeUtils.CharacterToCoding(infoLogin);
                    var emInfoBytes = HexHelper.StringToHexByte(emInfoEncoding);
                    if (null != CANTcp.client)
                        CANTcp.client.SendDataByEmployeeLoginInfo(new List<byte>(emInfoBytes), HexHelper.TenToHexString2Len(mainTrackNo), HexHelper.TenToHexString2Len(statingNo));
                    if (null != CANTcpServer.server)
                        CANTcpServer.server.SendDataByEmployeeLoginInfo(new List<byte>(emInfoBytes), HexHelper.TenToHexString2Len(mainTrackNo), HexHelper.TenToHexString2Len(statingNo));
                    return;
                }
                return;
            }
            if (null != CANTcp.client)
                CANTcp.client.ManualEmployeeLoginStating(mainTrackNo, statingNo, type, int.Parse(cardInfo?.CardNo?.Trim()), info);
            if (null != CANTcpServer.server)
                CANTcpServer.server.ManualEmployeeLoginStating(mainTrackNo, statingNo, type, int.Parse(cardInfo?.CardNo?.Trim()), info);
        }
        public void LoadOnLineProductsFlowChart(string processFlowChartId)
        {
            SusCacheProductService.Instance.LoadOnLineProductsFlowChart(processFlowChartId);
        }
        /// <summary>
        /// 跟进
        /// </summary>
        /// <param name="groupNo"></param>
        /// <returns></returns>
        public IList<Domain.SiteGroup> GetMainTrackNumberList(string groupNo)
        {
            // var sql = "select MainTrackNumber from SiteGroup where GroupNO=@GroupNO";
            var sql = string.Format(@"select distinct Sg.GroupNO,St.MainTrackNumber from SiteGroup SG
						left join Stating ST ON SG.Id=ST.SITEGROUP_Id
						where Sg.GroupNo=@GroupNo");
            if (string.IsNullOrEmpty(groupNo))
            {
                throw new ApplicationException("组号不能为空!");
            }
            return DapperHelp.QueryForList<Domain.SiteGroup>(sql, new { GroupNo = groupNo });
        }

        /// <summary>
        /// 缺料上传
        /// </summary>
        /// <param name="mainTrackNumber">主轨</param>
        /// <param name="statingNo">站点</param>
        /// <param name="faultInfo">缺料信息</param>"
        public void UpdateFaultInfo(int mainTrackNumber, int statingNo, string faultInfo)
        {
            var sql = @"update Stating 
                        set FaultInfo = @FaultInfo ,
                        UpdateDateTime =@UpdateDateTime,
                        UpdateUser=@UpdateUser
                        where MainTrackNumber = @MainTrackNumber and StatingNo =  @StatingNo";

            Stating stating = new Stating()
            {
                FaultInfo = faultInfo,
                UpdateDateTime = DateTime.Now,
                UpdateUser = UserId,
                MainTrackNumber = short.Parse( mainTrackNumber.ToString()),
                StatingNo =  statingNo.ToString(),
            };

            DapperHelp.Execute(sql, stating);
        }

        private static string UserId
        {
            get
            {
                if (CurrentUser.Instance.User != null)
                    return CurrentUser.Instance.UserId;
                else
                    return string.Empty;
            }
        }
    }
}
