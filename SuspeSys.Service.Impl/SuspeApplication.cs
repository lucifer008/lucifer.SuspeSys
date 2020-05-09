using log4net;
using Sus.Net.Client;
using Sus.Net.Common.Constant;
using Sus.Net.Server;
using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Client.Sqlite.Entity;
using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.SusRedis;

using SuspeSys.Service.Impl.SusTcp;
using SuspeSys.SusRedis.SusRedis.SusConst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SuspeSys.Service.Impl
{
    public class SuspeApplication : ISuspeApplication
    {
        protected static readonly ILog tcpLogInfo = LogManager.GetLogger("TcpLogInfo");
        protected static readonly ILog tcpLogError = LogManager.GetLogger("TcpErrorInfo");
        //public static string BaseApplicationPath;
        //public static bool isStartService;
        public SuspeApplication()
        {

        }
        const string connectionFormat = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
        /// <summary>
        /// 重置NHibernate配置
        /// </summary>
        /// <param name="defaultDB"></param>
        public static void ResetConfig(DatabaseConnection defaultDB) {
            string path = new FileInfo("Config/hibernate.cfg.xml").FullName;

            //XDocument doc = XDocument.Load(path);
            //doc.Elements().First()
            //    .Elements().First()
            //    .Elements().Where(o => o.Attribute("name").Value == "connection.connection_string").First()
            //    //
            //    .SetValue(string.Format(connectionFormat, defaultDB.ServerIP, defaultDB.DBName, defaultDB.UserId, defaultDB.Password));
            //doc.Save(path, SaveOptions.DisableFormatting);
            string myConnect = string.Format(connectionFormat, defaultDB.ServerIP, defaultDB.DBName, defaultDB.UserId, defaultDB.Password);
            XmlDocument myDoc = new XmlDocument();
            XmlElement myXmlElement;
            
            myDoc.Load(path);//加载启动目录下的App.config配置文件
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(myDoc.NameTable); nsMgr.AddNamespace("ns", "urn:nhibernate-configuration-2.2");
            XmlNode myNode = myDoc.SelectSingleNode("//ns:session-factory", nsMgr);//查找appSettings结点
            myXmlElement = (XmlElement)myNode.SelectSingleNode("//ns:property [@name='connection.connection_string']", nsMgr);//查找add结点中key=sql的结点

            myXmlElement.InnerText= myConnect;
         
            myDoc.Save(path);//保存到启动目录下的App.config配置文件
        }
        public static void Init(string baseApplicationPath = null, bool isStartService = false)
        {
            DapperHelp.AppPath = baseApplicationPath;
            SuspeSys.Dao.Nhibernate.SessionFactory.Init(baseApplicationPath, isStartService);
        }
        public void ClientInitNhibernateConfig()
        {

        }
        public void PowerSupplyInit(int mainTrackNumber, int statingNo, SusTCPServer1 server, TcpClient tcpClient=null)
        {
            #region//1.站点登录状态

            var sExist = StatingServiceImpl.Instance.CheckStatingExist(mainTrackNumber, statingNo);
            if (!sExist)
            {
                tcpLogInfo.InfoFormat("【上电初始化】站点不存在...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
                server.SendExcpetionOrPromptInfo(mainTrackNumber, statingNo, SuspeConstants.tag_StatingNoExist,null,tcpClient);
            }
            tcpLogInfo.InfoFormat("【上电初始化】站点登录状态 初始化开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            var other = string.Empty;
            var info = StatingServiceImpl.Instance.GetStatingLogInfo(mainTrackNumber, statingNo);
            if (!string.IsNullOrEmpty(info))
            {
                var emInfoEncoding = UnicodeUtils.CharacterToCoding(info);
                var emInfoBytes = HexHelper.StringToHexByte(emInfoEncoding);
                server.SendDataByEmployeeLoginInfo(new List<byte>(emInfoBytes), HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo),null,tcpClient);
            }
            else
            {
                tcpLogInfo.InfoFormat("【上电初始化】站点登录状态 无信息可推送...主轨:{0} 站点:{1} 推送内容:{2}", mainTrackNumber, statingNo, info);
            }
            tcpLogInfo.InfoFormat("【上电初始化】站点登录状态 初始化完成...主轨:{0} 站点:{1} 推送内容:{2}", mainTrackNumber, statingNo, info);
            #endregion

            #region//2.主轨运行状态
            var groupNo = StatingServiceImpl.Instance.GetGroupNo(mainTrackNumber);
            var sqlM = string.Format("select * from MainTrack where GroupNo=@GroupNo");
            var mainTrack = DapperHelp.QueryForObject<MainTrack>(sqlM, new { GroupNo = groupNo });
            var hexMainTrackNumber = HexHelper.TenToHexString2Len(mainTrackNumber);
            if (null != mainTrack)
            {
                tcpLogInfo.InfoFormat("【上电初始化】主轨状态初始化开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
                switch (mainTrack.Status.Value)
                {
                    case 0:
                        server.StartMainTrack(hexMainTrackNumber,null,tcpClient);
                        break;
                    case 1:
                        server.StopMainTrack(hexMainTrackNumber, null, tcpClient);
                        break;
                    case 2:
                        server.EmergencyStopMainTrack(hexMainTrackNumber, null, tcpClient);
                        break;

                }
                tcpLogInfo.InfoFormat("【上电初始化】主轨状态初始化结束...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            }
            #endregion

            #region//3.站点产量005b

            List<Byte> outData = null;
            var isSucess = CANProductsQueryService.Instance.GetPowerSupplyStatingYield(mainTrackNumber, statingNo, ref outData, ref info);
            if (isSucess)
            {
                tcpLogInfo.InfoFormat("【站点产量】 推送开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
                server.SendDataByCommonSiteOutSite(outData, HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo),tcpClient);
                tcpLogInfo.InfoFormat("【站点产量】 推送完成...主轨:{0} 站点:{1} 推送内容:{2}", mainTrackNumber, statingNo, info);
            }
            else
            {
                tcpLogInfo.InfoFormat("【站点产量】 无产量数据可推送...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            }
            #endregion

            #region//4.站点返工产量005c

            var infoRework = string.Empty;
            var reData = CANProductsQueryService.Instance.GetPowerSupplyStatingYieldRework(mainTrackNumber, statingNo, ref infoRework);

            tcpLogInfo.InfoFormat("【上电初始化】【站点返工信息】 推送开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            server.SendDataByReworkSiteOutSite(reData, HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo),tcpClient);
            tcpLogInfo.InfoFormat("【上电初始化】【站点返工信息】 推送完成...主轨:{0} 站点:{1} 推送内容:{2}", mainTrackNumber, statingNo, infoRework);

            //else
            //{
            //    tcpLogInfo.InfoFormat("【站点返工信息】 无产量数据可推送...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            //}
            #endregion

            #region //5.挂片站上线款式信息
            try {
                var isHangingPiece = CANProductsService.Instance.IsHangPieceStating(mainTrackNumber, statingNo);
                if (isHangingPiece)
                {
                    //tcpLogInfo.InfoFormat("挂片站上线款式异步推送开始...主轨:{0}", mainTrackNumber);
                    var pService = new ProductsServiceImpl();
                    var thread = new Thread(pService.BatchSendDataToHangingPiece2);
                    var sendProductsData = new ProductsData()
                    {
                        //ProductsList = productsList,
                        //MainTrackNumber = mainTrackNumber
                        TcpClient = tcpClient
                    };
                    thread.Start(sendProductsData);
                }
            } catch (Exception ex) {
                tcpLogError.Error(ex);
            }
            #endregion

            #region//6.站点容量//7.站点接收衣架状态
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, StatingModel>(SusRedisConst.STATING_TABLE);
            var key = string.Format("{0}:{1}", mainTrackNumber, statingNo);
            if (dic.ContainsKey(key))
            {
                tcpLogInfo.InfoFormat("【上电初始化】站点容量推送开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
                var statingModel = dic[key];
                server.ModifyStatingCapacity(mainTrackNumber, statingNo.ToString(), statingModel.Capacity.Value,null,tcpClient);
                tcpLogInfo.InfoFormat("【上电初始化】站点容量推送完成...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);


                server.SuspendOrReceiveHanger(mainTrackNumber, statingNo, (null != statingModel.IsReceivingHanger && statingModel.IsReceivingHanger.Value) ? 0 : 1,null,tcpClient);

                tcpLogInfo.InfoFormat("【上电初始化】站点类型推送开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
                var statingType = StatingType.GetStatingType(statingModel.StatingName?.Trim());
                server.SendStatingType(mainTrackNumber, statingNo.ToString(), 1, statingType,null,tcpClient);
                tcpLogInfo.InfoFormat("【上电初始化】站点类型推送完成...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            }

            #endregion

            #region//8.站内数
            //SendStatingNum
            var dicStatingNum = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);
            if (dicStatingNum.ContainsKey(key))
            {
                server.SendStatingNum(mainTrackNumber, statingNo, (int)dicStatingNum[key],null,tcpClient);
            }
            else
            {
                tcpLogInfo.InfoFormat("【上电初始化】【站内数】 无数据可推送...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            }
            #endregion

            #region 上线制品
            tcpLogInfo.InfoFormat("【上电初始化】上线制品推送开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            var onLineProduct = ProductsServiceImpl.Instance.GetOnLineProduct(mainTrackNumber);
            if (null == onLineProduct)
            {
                tcpLogInfo.InfoFormat("【上电初始化】无上线制品可推送...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            }
            else
            {
                string errMsg = null;
                ProductsServiceImpl.Instance.ClientMancheOnLine2(onLineProduct, mainTrackNumber, ref errMsg,tcpClient);
            }
            tcpLogInfo.InfoFormat("【上电初始化】上线制品推送完成...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            #endregion

            #region 满站处理
            tcpLogInfo.InfoFormat("【上电初始化】【满站查询】主轨:{0} 站点:{1}  ---开始", mainTrackNumber, statingNo);
            server.QueryFullSite(mainTrackNumber, statingNo,null,tcpClient);
            tcpLogInfo.InfoFormat("【上电初始化】【满站查询】主轨:{0} 站点:{1}  ---结束", mainTrackNumber, statingNo);
            #endregion
        }
        public void PowerSupplyInit(int mainTrackNumber, int statingNo, SusTCPClient client)
        {
            #region//1.站点登录状态

            var sExist = StatingServiceImpl.Instance.CheckStatingExist(mainTrackNumber, statingNo);
            if (!sExist)
            {
                tcpLogInfo.InfoFormat("【上电初始化】站点不存在...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
                client.SendExcpetionOrPromptInfo(mainTrackNumber, statingNo, SuspeConstants.tag_StatingNoExist);
            }
            tcpLogInfo.InfoFormat("【上电初始化】站点登录状态 初始化开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            var other = string.Empty;
            var info = StatingServiceImpl.Instance.GetStatingLogInfo(mainTrackNumber, statingNo);
            if (!string.IsNullOrEmpty(info))
            {
                var emInfoEncoding = UnicodeUtils.CharacterToCoding(info);
                var emInfoBytes = HexHelper.StringToHexByte(emInfoEncoding);
                client.SendDataByEmployeeLoginInfo(new List<byte>(emInfoBytes), HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo));
            }
            else
            {
                tcpLogInfo.InfoFormat("站点登录状态 无信息可推送...主轨:{0} 站点:{1} 推送内容:{2}", mainTrackNumber, statingNo, info);
            }
            tcpLogInfo.InfoFormat("站点登录状态 初始化完成...主轨:{0} 站点:{1} 推送内容:{2}", mainTrackNumber, statingNo, info);
            #endregion

            #region//2.主轨运行状态
            var groupNo = StatingServiceImpl.Instance.GetGroupNo(mainTrackNumber);
            var sqlM = string.Format("select * from MainTrack where GroupNo=@GroupNo");
            var mainTrack = DapperHelp.QueryForObject<MainTrack>(sqlM, new { GroupNo = groupNo });
            var hexMainTrackNumber = HexHelper.TenToHexString2Len(mainTrackNumber);
            if (null != mainTrack)
            {
                tcpLogInfo.InfoFormat("主轨状态初始化开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
                switch (mainTrack.Status.Value)
                {
                    case 0:
                        CANTcp.client.StartMainTrack(hexMainTrackNumber);
                        break;
                    case 1:
                        CANTcp.client.StopMainTrack(hexMainTrackNumber);
                        break;
                    case 2:
                        CANTcp.client.EmergencyStopMainTrack(hexMainTrackNumber);
                        break;

                }
                tcpLogInfo.InfoFormat("主轨状态初始化结束...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            }
            #endregion

            #region//3.站点产量005b

            List<Byte> outData = null;
            var isSucess = CANProductsQueryService.Instance.GetPowerSupplyStatingYield(mainTrackNumber, statingNo, ref outData, ref info);
            if (isSucess)
            {
                tcpLogInfo.InfoFormat("【站点产量】 推送开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
                client.SendDataByCommonSiteOutSite(outData, HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo));
                tcpLogInfo.InfoFormat("【站点产量】 推送完成...主轨:{0} 站点:{1} 推送内容:{2}", mainTrackNumber, statingNo, info);
            }
            else
            {
                tcpLogInfo.InfoFormat("【站点产量】 无产量数据可推送...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            }
            #endregion

            #region//4.站点返工产量005c

            var infoRework = string.Empty;
            var reData = CANProductsQueryService.Instance.GetPowerSupplyStatingYieldRework(mainTrackNumber, statingNo, ref infoRework);

            tcpLogInfo.InfoFormat("【站点返工信息】 推送开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            client.SendDataByReworkSiteOutSite(reData, HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo));
            tcpLogInfo.InfoFormat("【站点返工信息】 推送完成...主轨:{0} 站点:{1} 推送内容:{2}", mainTrackNumber, statingNo, infoRework);

            //else
            //{
            //    tcpLogInfo.InfoFormat("【站点返工信息】 无产量数据可推送...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            //}
            #endregion

            #region //5.挂片站上线款式信息
            var isHangingPiece = CANProductsService.Instance.IsHangPieceStating(mainTrackNumber, statingNo);
            if (isHangingPiece)
            {
                //tcpLogInfo.InfoFormat("挂片站上线款式异步推送开始...主轨:{0}", mainTrackNumber);
                var pService = new ProductsServiceImpl();
                var thread = new Thread(pService.BatchSendDataToHangingPiece);
                var sendProductsData = new ProductsData()
                {
                    //ProductsList = productsList,
                    //MainTrackNumber = mainTrackNumber
                };
                thread.Start(sendProductsData);
            }
            #endregion

            #region//6.站点容量//7.站点接收衣架状态
            var dic = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, StatingModel>(SusRedisConst.STATING_TABLE);
            var key = string.Format("{0}:{1}", mainTrackNumber, statingNo);
            if (dic.ContainsKey(key))
            {
                tcpLogInfo.InfoFormat("站点容量推送开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
                var statingModel = dic[key];
                client.ModifyStatingCapacity(mainTrackNumber, statingNo.ToString(), statingModel.Capacity.Value);
                tcpLogInfo.InfoFormat("站点容量推送完成...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);


                client.SuspendOrReceiveHanger(mainTrackNumber, statingNo, (null != statingModel.IsReceivingHanger && statingModel.IsReceivingHanger.Value) ? 0 : 1);

                tcpLogInfo.InfoFormat("站点类型推送开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
                var statingType = StatingType.GetStatingType(statingModel.StatingName?.Trim());
                client.SendStatingType(mainTrackNumber, statingNo.ToString(), 1, statingType);
                tcpLogInfo.InfoFormat("站点类型推送完成...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            }

            #endregion

            #region//8.站内数
            //SendStatingNum
            var dicStatingNum = NewSusRedisClient.RedisTypeFactory.GetDictionary<string, long>(SusRedisConst.MAINTRACK_STATING_IN_NUM);
            if (dicStatingNum.ContainsKey(key))
            {
                client.SendStatingNum(mainTrackNumber, statingNo, (int)dicStatingNum[key]);
            }
            else
            {
                tcpLogInfo.InfoFormat("【站内数】 无数据可推送...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            }
            #endregion

            #region 上线制品
            tcpLogInfo.InfoFormat("上线制品推送开始...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            var onLineProduct = ProductsServiceImpl.Instance.GetOnLineProduct(mainTrackNumber);
            if (null == onLineProduct)
            {
                tcpLogInfo.InfoFormat("无上线制品可推送...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            }
            else
            {
                string errMsg = null;
                ProductsServiceImpl.Instance.ClientMancheOnLine(onLineProduct, mainTrackNumber, ref errMsg);
            }
            tcpLogInfo.InfoFormat("上线制品推送完成...主轨:{0} 站点:{1}", mainTrackNumber, statingNo);
            #endregion

            #region 满站处理
            tcpLogInfo.InfoFormat("【满站查询】主轨:{0} 站点:{1}  ---开始", mainTrackNumber, statingNo);
            client.QueryFullSite(mainTrackNumber, statingNo);
            tcpLogInfo.InfoFormat("【满站查询】主轨:{0} 站点:{1}  ---结束", mainTrackNumber, statingNo);
            #endregion
        }
        /// <summary>
        /// 采集
        /// </summary>
        /// <param name="defaultDB"></param>
        public static void ResetConfig(SuspeSys.CustomerControls.Sqlite.Entity.DatabaseConnection defaultDB)
        {
            string path = new FileInfo("Config/hibernate.cfg.xml").FullName;

            //XDocument doc = XDocument.Load(path);
            //doc.Elements().First()
            //    .Elements().First()
            //    .Elements().Where(o => o.Attribute("name").Value == "connection.connection_string").First()
            //    //
            //    .SetValue(string.Format(connectionFormat, defaultDB.ServerIP, defaultDB.DBName, defaultDB.UserId, defaultDB.Password));
            //doc.Save(path, SaveOptions.DisableFormatting);
            string myConnect = string.Format(connectionFormat, defaultDB.ServerIP, defaultDB.DBName, defaultDB.UserId, defaultDB.Password);
            XmlDocument myDoc = new XmlDocument();
            XmlElement myXmlElement;

            myDoc.Load(path);//加载启动目录下的App.config配置文件
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(myDoc.NameTable); nsMgr.AddNamespace("ns", "urn:nhibernate-configuration-2.2");
            XmlNode myNode = myDoc.SelectSingleNode("//ns:session-factory", nsMgr);//查找appSettings结点
            myXmlElement = (XmlElement)myNode.SelectSingleNode("//ns:property [@name='connection.connection_string']", nsMgr);//查找add结点中key=sql的结点

            myXmlElement.InnerText = myConnect;

            myDoc.Save(path);//保存到启动目录下的App.config配置文件
        }
    }
}
