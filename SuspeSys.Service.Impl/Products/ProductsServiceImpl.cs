using SusNet.Common.Utils;
using Suspe.CAN.Action.CAN;
using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.Products.SusCache.Service;
using SuspeSys.Service.Impl.Support;
using SuspeSys.Service.Impl.SusTcp;
using SuspeSys.Service.Products;
using SuspeSys.Utils;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Service.Impl.Products
{
    public class ProductsServiceImpl : ServiceBase, IProductsService
    {
        public static ProductsServiceImpl Instance = new ProductsServiceImpl();

        public long GetCurrentProductNumber()
        {
            return IdGeneratorSupport.Instance.GetCurrentValue("Products");
        }
        private Object thisLock = new Object();
        /// <summary>
        /// 排产号
        /// </summary>
        /// <returns></returns>
        public int GetCurrentProductNumber(string groupNo)
        {
            var currentProductNumber = 0;
            lock (thisLock)
            {
                var sql = string.Format("select isnull(max(ProductionNumber),0) ProductionNumber from Products where groupNo=@groupNo");
                currentProductNumber = DapperHelp.QueryForObject<int>(sql, new { groupNo = groupNo }) + 1;
            }
            return currentProductNumber;
        }
        public bool AddProducts(IList<DaoModel.Products> productsList, string mainTrackNumber = null, int currentProductNumber = 0)
        {
            bool sucess = false;
            try
            {
                using (var session = SessionFactory.OpenSession())
                {
                    using (var trans = session.BeginTransaction())
                    {
                        foreach (var p in productsList)
                        {
                            p.ProductionNumber = currentProductNumber++; //(int)IdGeneratorSupport.Instance.NextId(typeof(SuspeSys.Domain.Products));
                            if (!string.IsNullOrEmpty(p.HangingPieceSiteNo))
                            {
                                p.Status = ProductsStatusType.Allocationed.Value;
                            }
                            ProductsDao.Instance.Save(p, true);
                        }
                        session.Flush();
                        trans.Commit();
                        return sucess = true;
                    }
                }
            }
            finally
            {
                if (sucess)
                {
                    var pService = new ProductsServiceImpl();
                    var thread = new Thread(pService.BatchSendDataToHangingPiece);
                    var sendProductsData = new ProductsData()
                    {
                        ProductsList = productsList,
                        MainTrackNumber = mainTrackNumber
                    };
                    thread.Start(sendProductsData);
                }
            }
        }
        public void BatchSendDataToHangingPiece(object data)
        {
            var pData = data as ProductsData;

            var hangingPieceList = new Dictionary<string, List<string>>(); //new List<HangingPieceStatingProductRelation>();

            IList<DaoModel.Products> pList = DapperHelp.Query<DaoModel.Products>("select * from Products", null).OrderBy((f => f.ProductionNumber)).ToList<DaoModel.Products>(); //ProductsDao.Instance.GetAll().OrderBy(f => f.ProductionNumber).ToList();
            //var index = 0;
            ////List<List<byte>> productsList = new List<List<byte>>();
            //var address = 24576;
            //var maxAddress = 26623;

            tcpLogInfo.InfoFormat("【挂片站上线款式推送】挂片站上线款式异步推送开始...");
            string errMsg = null;
            foreach (var p in pList)
            {
                var tenMainTrackNumber = StatingServiceImpl.Instance.GetMainTrackNumber(p.GroupNo?.Trim(), p.HangingPieceSiteNo?.Trim());
                string mainTrackNo = HexHelper.TenToHexString2Len(tenMainTrackNumber);//pData?.MainTrackNumber); 
                //制单号，颜色，尺码
                // var dataText = string.Format("{0}.{1},{2},{3}", (index + 1), p.ProcessOrderNo?.Trim(), p.PColor?.Trim(), p.PSize?.Trim());
                var dataText = string.Format("{0},{1},{2},{3}", p.ProcessOrderNo?.Trim(), p.PColor?.Trim(), p.PSize?.Trim(), p.Unit);
                // var hexData = HexHelper.ToHex(data, "utf-8", false);
                //var uninodeBytes = SusNet.Common.Utils.UnicodeUtils.GetBytesByUnicode(dataText); //SusNet.Common.Utils.UnicodeUtils.GetHexFromChs(dataText); //SusNet.Common.Utils.UnicodeUtils.GetBytesByUnicode(dataText);
                //Array.Reverse(uninodeBytes);

                var hexDataText = UnicodeUtils.CharacterToCoding(dataText);
                var dataBytes = HexHelper.StringToHexByte(hexDataText);
                // var hex = HexHelper.BytesToHexString(uninodeBytes);
                //var hexBytes = SusNet.Common.Utils.HexHelper.StringToHexByte(hex);
                var hexStatingNo = SusNet.Common.Utils.HexHelper.TenToHexString2Len(!string.IsNullOrEmpty(p.HangingPieceSiteNo?.Trim()) ? p.HangingPieceSiteNo?.Trim() : "04");
                BindProudctsToHangingPiece(new List<byte>(dataBytes), mainTrackNo, hexStatingNo, p.ProductionNumber.Value, ref errMsg);
                /*
                //productsList.Add(new List<byte>(hexBytes));
                //if (!hangingPieceList.ContainsKey(p.HangingPieceSiteNo?.Trim()))
                //{
                //    hangingPieceList.Add(p.HangingPieceSiteNo?.Trim(), new List<string>() { p.ProductionNumber.ToString() });
                //}
                //else {
                //    hangingPieceList[p.HangingPieceSiteNo?.Trim()].Add(p.ProductionNumber.ToString());
                //}
                //log.Info(string.Format(SusNet.Common.Utils.HexHelper.t));
                var hexBytes = SusNet.Common.Utils.HexHelper.StringToHexByte(hexStr);
                string mainTrackNo = "01";
                string errMsg = null;
                var hexStatingNo = SusNet.Common.Utils.HexHelper.TenToHexString(!string.IsNullOrEmpty(p.HangingPieceSiteNo?.Trim()) ? p.HangingPieceSiteNo?.Trim() : "04");
                bool isEnd = false;
                if (index == pList.Count - 1) isEnd = true;
                BindProudctsToHangingPiece(new List<byte>(hexBytes), mainTrackNo, hexStatingNo, p.ProductionNumber.Value, ref address, isEnd, ref errMsg);

                if (!string.IsNullOrEmpty(errMsg))
                {
                    log.Error(string.Format("【挂片站批量发送数据】 出现错误:{0}", errMsg));
                }
                index++;
                //address++;
                if (address > maxAddress)
                {
                    var ex = new ApplicationException("地址超过最大值:" + SusNet.Common.Utils.HexHelper.TenToHexString(address));
                    log.Error(ex);
                    throw ex;
                }
                */
            }
            tcpLogInfo.InfoFormat("【挂片站上线款式推送】挂片站上线款式异步推送完成...");
            //foreach (var statingno in hangingPieceList.Keys) {
            //    string mainTrackNo = "01";
            //    string errMsg = null;
            //    var hexStatingNo = SusNet.Common.Utils.HexHelper.tenToHexString(statingno);
            //    var productNumber=hangingPieceList[]
            //    BindProudctsToHangingPiece(productsList, mainTrackNo, hexStatingNo, p.ProductionNumber, ref errMsg);

            //    if (!string.IsNullOrEmpty(errMsg))
            //    {
            //        log.Error(string.Format("【挂片站批量发送数据】 出现错误:{0}", errMsg));
            //    }
            //}


            //重新加载制品信息缓存
            redisLog.InfoFormat("【redis缓存】重新加载制品信息开始...");
            SusCacheProductService.Instance.LoadProductsToCache();
            redisLog.InfoFormat("【redis缓存】重新加载制品信息完成...");

        }

        public void BatchSendDataToHangingPiece2(object data)
        {
            var pData = data as ProductsData;

            var hangingPieceList = new Dictionary<string, List<string>>(); //new List<HangingPieceStatingProductRelation>();

            IList<DaoModel.Products> pList = DapperHelp.Query<DaoModel.Products>("select * from Products", null).OrderBy((f => f.ProductionNumber)).ToList<DaoModel.Products>(); //ProductsDao.Instance.GetAll().OrderBy(f => f.ProductionNumber).ToList();
            //var index = 0;
            ////List<List<byte>> productsList = new List<List<byte>>();
            //var address = 24576;
            //var maxAddress = 26623;

            tcpLogInfo.InfoFormat("【挂片站上线款式推送】挂片站上线款式异步推送开始...");
            string errMsg = null;
            foreach (var p in pList)
            {
                if (string.IsNullOrEmpty(p.HangingPieceSiteNo)) {
                    var info = string.Format($"制单号{p.ProcessOrderNo?.Trim()} 挂片为空!");
                    tcpLogInfo.Info(info);
                    continue;
                }
                var tenMainTrackNumber = StatingServiceImpl.Instance.GetMainTrackNumber(p.GroupNo?.Trim(), p.HangingPieceSiteNo?.Trim());
                string mainTrackNo = HexHelper.TenToHexString2Len(tenMainTrackNumber);//pData?.MainTrackNumber); 
                //制单号，颜色，尺码
                // var dataText = string.Format("{0}.{1},{2},{3}", (index + 1), p.ProcessOrderNo?.Trim(), p.PColor?.Trim(), p.PSize?.Trim());
                var dataText = string.Format("{0},{1},{2},{3}", p.ProcessOrderNo?.Trim(), p.PColor?.Trim(), p.PSize?.Trim(), p.Unit);
                // var hexData = HexHelper.ToHex(data, "utf-8", false);
                //var uninodeBytes = SusNet.Common.Utils.UnicodeUtils.GetBytesByUnicode(dataText); //SusNet.Common.Utils.UnicodeUtils.GetHexFromChs(dataText); //SusNet.Common.Utils.UnicodeUtils.GetBytesByUnicode(dataText);
                //Array.Reverse(uninodeBytes);

                var hexDataText = UnicodeUtils.CharacterToCoding(dataText);
                var dataBytes = HexHelper.StringToHexByte(hexDataText);
                // var hex = HexHelper.BytesToHexString(uninodeBytes);
                //var hexBytes = SusNet.Common.Utils.HexHelper.StringToHexByte(hex);
                var hexStatingNo = SusNet.Common.Utils.HexHelper.TenToHexString2Len(!string.IsNullOrEmpty(p.HangingPieceSiteNo?.Trim()) ? p.HangingPieceSiteNo?.Trim() : "04");
                BindProudctsToHangingPiece2(new List<byte>(dataBytes), mainTrackNo, hexStatingNo, p.ProductionNumber.Value, ref errMsg, pData.TcpClient);
                /*
                //productsList.Add(new List<byte>(hexBytes));
                //if (!hangingPieceList.ContainsKey(p.HangingPieceSiteNo?.Trim()))
                //{
                //    hangingPieceList.Add(p.HangingPieceSiteNo?.Trim(), new List<string>() { p.ProductionNumber.ToString() });
                //}
                //else {
                //    hangingPieceList[p.HangingPieceSiteNo?.Trim()].Add(p.ProductionNumber.ToString());
                //}
                //log.Info(string.Format(SusNet.Common.Utils.HexHelper.t));
                var hexBytes = SusNet.Common.Utils.HexHelper.StringToHexByte(hexStr);
                string mainTrackNo = "01";
                string errMsg = null;
                var hexStatingNo = SusNet.Common.Utils.HexHelper.TenToHexString(!string.IsNullOrEmpty(p.HangingPieceSiteNo?.Trim()) ? p.HangingPieceSiteNo?.Trim() : "04");
                bool isEnd = false;
                if (index == pList.Count - 1) isEnd = true;
                BindProudctsToHangingPiece(new List<byte>(hexBytes), mainTrackNo, hexStatingNo, p.ProductionNumber.Value, ref address, isEnd, ref errMsg);

                if (!string.IsNullOrEmpty(errMsg))
                {
                    log.Error(string.Format("【挂片站批量发送数据】 出现错误:{0}", errMsg));
                }
                index++;
                //address++;
                if (address > maxAddress)
                {
                    var ex = new ApplicationException("地址超过最大值:" + SusNet.Common.Utils.HexHelper.TenToHexString(address));
                    log.Error(ex);
                    throw ex;
                }
                */
            }
            tcpLogInfo.InfoFormat("【挂片站上线款式推送】挂片站上线款式异步推送完成...");
            //foreach (var statingno in hangingPieceList.Keys) {
            //    string mainTrackNo = "01";
            //    string errMsg = null;
            //    var hexStatingNo = SusNet.Common.Utils.HexHelper.tenToHexString(statingno);
            //    var productNumber=hangingPieceList[]
            //    BindProudctsToHangingPiece(productsList, mainTrackNo, hexStatingNo, p.ProductionNumber, ref errMsg);

            //    if (!string.IsNullOrEmpty(errMsg))
            //    {
            //        log.Error(string.Format("【挂片站批量发送数据】 出现错误:{0}", errMsg));
            //    }
            //}


            //重新加载制品信息缓存
            redisLog.InfoFormat("【redis缓存】重新加载制品信息开始...");
            SusCacheProductService.Instance.LoadProductsToCache();
            redisLog.InfoFormat("【redis缓存】重新加载制品信息完成...");

        }
        public void ProductsHangingPiece(string productsId)
        {
            var products = ProductsDao.Instance.GetById(productsId);
            products.Status = ProductsStatusType.Allocationed.Value;
            ProductsDao.Instance.Update(products);
        }

        /// <summary>
        /// 清除挂片站制品
        /// </summary>
        /// <param name="mainTrackNo">主轨号(10进制)</param>
        /// <param name="statingNo">挂片站号(10进制)</param>
        /// <param name="productNumber">排产号(10进制)</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool ClearHangingPieceProducts(string mainTrackNo, string statingNo, int productNumber, ref string errMsg)
        {
            try
            {
                if (null == CANTcp.client && CANTcpServer.server != null)
                {
                    throw new ApplicationException("请检查CAN是否连接正常!");
                }
                if (CANTcp.client != null)
                    CANTcp.client.ClearHangingPieceStatingInfo(mainTrackNo, statingNo, productNumber);//BindProudctsToHangingPieceExt(productsList, mainTrackNo.TenToHexString(), statingNo.TenToHexString(), productNumber, ref address, isEnd);
                if (CANTcpServer.server != null)
                    CANTcpServer.server.ClearHangingPieceStatingInfo(mainTrackNo, statingNo, productNumber);

                log.Info(string.Format("【清除制品指令已发送到:{0} 站点】", statingNo));
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                log.Error("【BindProudctsToHangingPiece】", ex);
                return false;
            }
        }

        internal DaoModel.Products GetOnLineProduct(int mainTrackNumber)
        {
            var groupNo = StatingServiceImpl.Instance.GetGroupNo(mainTrackNumber);
            var pSql = "select top 1 HangingPieceSiteNo,ProductionNumber from Products where GroupNo=@GroupNo";
            var onLineP = DapperHelp.QueryForObject<DaoModel.Products>(pSql, new { GroupNo = groupNo?.Trim() });
            return onLineP;
        }

        /// <summary>
        /// 【协议2.0】制品界面发送消息到挂片站
        /// </summary>
        /// <param name="productsList">产品列表</param>
        /// <param name="mainTrackNo">主轨号(10进制)</param>
        /// <param name="statingNo">挂片站号(10进制)</param>
        /// <param name="productNumber">排产号(10进制)</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool BindProudctsToHangingPiece(List<byte> productsList, string mainTrackNo, string statingNo, int productNumber, ref string errMsg)
        {
            try
            {
                if (null == CANTcp.client && null == CANTcpServer.server)
                {
                    throw new ApplicationException("请检查CAN是否连接正常!");
                }
                if (CANTcp.client != null)
                    CANTcp.client.BindProudctsToHangingPieceNew(productsList, mainTrackNo, statingNo, productNumber, "FF");//BindProudctsToHangingPieceExt(productsList, mainTrackNo.TenToHexString(), statingNo.TenToHexString(), productNumber, ref address, isEnd);
                if (CANTcpServer.server != null)
                    CANTcpServer.server.BindProudctsToHangingPieceNew(productsList, mainTrackNo, statingNo, productNumber, "FF");
                log.Info(string.Format("【制品信息已发往挂片站:{0}】", statingNo));
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                log.Error("【BindProudctsToHangingPiece】", ex);
                return false;
            }
        }
        /// <summary>
        /// 【协议2.0】制品界面发送消息到挂片站
        /// </summary>
        /// <param name="productsList">产品列表</param>
        /// <param name="mainTrackNo">主轨号(10进制)</param>
        /// <param name="statingNo">挂片站号(10进制)</param>
        /// <param name="productNumber">排产号(10进制)</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool BindProudctsToHangingPiece2(List<byte> productsList, string mainTrackNo, string statingNo, int productNumber, ref string errMsg, TcpClient tcpClient)
        {
            try
            {
                if (null == CANTcpServer.server)
                {
                    var ex = new ApplicationException("请检查CAN是否连接正常!");
                    tcpLogError.Error(ex);
                    throw ex;
                }
                CANTcpServer.server.BindProudctsToHangingPieceNew(productsList, mainTrackNo, statingNo, productNumber, "FF",tcpClient);//BindProudctsToHangingPieceExt(productsList, mainTrackNo.TenToHexString(), statingNo.TenToHexString(), productNumber, ref address, isEnd);

                log.Info(string.Format("【制品信息已发往挂片站:{0}】", statingNo));
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                log.Error("【BindProudctsToHangingPiece】", ex);
                return false;
            }
        }
        public bool ProductsOnline(DaoModel.Products p)
        {
            //var sql = new StringBuilder("Update Products set Status=:Status1 where Status=:Status2");
            //var session = SessionFactory.OpenSession();
            //var r = session.CreateQuery(sql.ToString()).SetInt32("Status1", ProductsStatusType.Allocationed.Value).SetInt32("Status2", ProductsStatusType.Onlineed.Value).ExecuteUpdate();
            ////IList<DaoModel.Products> pList=Query<DaoModel.Products>(sql, null, true, ProductsStatusType.Successed.Value);
            ////foreach () {

            ////}
            //var pp = ProductsDao.Instance.GetById(p.Id);
            //pp.Status = ProductsStatusType.Onlineed.Value;
            //ProductsDao.Instance.Update(pp);

            //改为Dapper

            //先刷新其他已上线的产品为已分配
            var sqlRes = "Update Products set Status=@Status1 where Status=@Status2 and GroupNo = @GroupNo";
            DapperHelp.Execute(sqlRes, new
            {
                Status1 = ProductsStatusType.Allocationed.Value,
                Status2 = ProductsStatusType.Onlineed.Value,
                GroupNo = p.GroupNo
            });
            //再上线
            var sql = "Update Products set Status=@Status2 where Id = @Id";
            DapperHelp.Execute(sql, new { Status2 = ProductsStatusType.Onlineed.Value, Id = p.Id });
            return true;
        }
        public bool ClientMancheOnLine(DaoModel.Products p, int mainTrackNumber, ref string errMsg)
        {
            try
            {
                if (null == CANTcp.client && CANTcpServer.server == null)
                {
                    throw new ApplicationException("请检查CAN是否连接正常!");
                }
                //var mainTrackNo = "01";
                mainTrackNumber = StatingServiceImpl.Instance.GetMainTrackNumber(p.GroupNo?.Trim(), p.HangingPieceSiteNo?.Trim());
                var hangingPieceSiteNo = string.IsNullOrEmpty(p.HangingPieceSiteNo.Trim()) ? "02" : p.HangingPieceSiteNo.TenToHexString();
                //排产号消息层衣架转换为16进制
                if (null != CANTcp.client)
                    CANTcp.client.ClientMancheOnLine(mainTrackNumber.TenToHexString(), hangingPieceSiteNo.TenToHexString(), p.ProductionNumber.Value.TenToHexString());
                if (null != CANTcpServer.server)
                    CANTcpServer.server.ClientMancheOnLine(mainTrackNumber.TenToHexString(), hangingPieceSiteNo.TenToHexString(), p.ProductionNumber.Value.TenToHexString());

                log.Info(string.Format("【制品信息已发往挂片站:{0}】", p.HangingPieceSiteNo));
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                log.Error("【BindProudctsToHangingPiece】", ex);
                return false;
            }
        }
        public bool ClientMancheOnLine2(DaoModel.Products p, int mainTrackNumber, ref string errMsg, TcpClient tcpClient = null)
        {
            try
            {
                //if (null == CANTcp.client)
                //{
                //    throw new ApplicationException("请检查CAN是否连接正常!");
                //}
                //var mainTrackNo = "01";
                mainTrackNumber = StatingServiceImpl.Instance.GetMainTrackNumber(p.GroupNo?.Trim(), p.HangingPieceSiteNo?.Trim());
                var hangingPieceSiteNo = string.IsNullOrEmpty(p.HangingPieceSiteNo.Trim()) ? "02" : p.HangingPieceSiteNo.TenToHexString();
                //排产号消息层衣架转换为16进制
                CANTcpServer.server.ClientMancheOnLine(mainTrackNumber.TenToHexString(), hangingPieceSiteNo.TenToHexString(), p.ProductionNumber.Value.TenToHexString(), null,tcpClient);
                log.Info(string.Format("【制品信息已发往挂片站:{0}】", p.HangingPieceSiteNo));
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                log.Error("【BindProudctsToHangingPiece】", ex);
                return false;
            }
        }
        public bool ClearHangingPiece(string productId)
        {
            var p = ProductsDao.Instance.GetById(productId);
            p.HangingPieceSiteNo = null;
            p.Status = ProductsStatusType.NonAllocation.Value;
            ProductsDao.Instance.Update(p);
            return true;
        }
        public bool AllocationHangerPiece(string productId, string hangingPieceNo)
        {
            var p = ProductsDao.Instance.GetById(productId);
            p.HangingPieceSiteNo = hangingPieceNo;
            p.Status = ProductsStatusType.Allocationed.Value;
            ProductsDao.Instance.Update(p);
            var mainTrackNo = "01";
            var hangingPieceSiteNo = string.IsNullOrEmpty(p.HangingPieceSiteNo.Trim()) ? "02" : p.HangingPieceSiteNo.TenToHexString();
            //排产号消息层衣架转换为16进制
            if (null != CANTcp.client)
                CANTcp.client.ClientMancheOnLine(mainTrackNo.TenToHexString(), hangingPieceSiteNo.TenToHexString(), p.ProductionNumber.Value.TenToHexString());
            if (null != CANTcpServer.server)
                CANTcpServer.server.ClientMancheOnLine(mainTrackNo.TenToHexString(), hangingPieceSiteNo.TenToHexString(), p.ProductionNumber.Value.TenToHexString());
            log.Info(string.Format("【制品信息已发往挂片站:{0}】", p.HangingPieceSiteNo));
            return true;
        }
        public bool MarkSuccessProducts(DaoModel.Products row, ref string errMsg)
        {
            try
            {
                var p = ProductsDao.Instance.GetById(row.Id);

                var sp = BeanUitls<DaoModel.SucessProducts, DaoModel.Products>.Mapper(p);
                sp.Memo = "手动标记完成制品";
                sp.ProductsId = row.Id;
                SucessProductsDao.Instance.Save(sp);
                var list = WaitProcessOrderHangerDao.Instance.GetAll().Where(f => f.ProductsId.Equals(p.Id)).ToList<DaoModel.WaitProcessOrderHanger>();
                p.Status = ProductsStatusType.Successed.Value;
                ProductsDao.Instance.Update(p);

                foreach (var wpHangerData in list)
                {
                    var sucessProcessOrderHanger = BeanUitls<DaoModel.SucessProcessOrderHanger, DaoModel.WaitProcessOrderHanger>.Mapper(wpHangerData);
                    SucessProcessOrderHangerDao.Instance.Save(sucessProcessOrderHanger);
                    WaitProcessOrderHangerDao.Instance.Delete(wpHangerData.Id);

                    OtherUndeterminedData(wpHangerData);
                    //TransferProductData(wpHangerData);
                }

                return true;

                //BeanTransformerAdapter<DaoModel.SuccessStatingHangerProductItem,DaoModel.>.ma
            }
            catch (Exception ex)
            {
                tcpLogError.Error("MarkSuccessProducts", ex);
                throw ex;
            }
            finally
            {
                var sql = string.Format(@"
	select distinct Sg.GroupNO,St.MainTrackNumber from SiteGroup SG
						left join Stating ST ON SG.Id=ST.SITEGROUP_Id
						where Sg.GroupNo=@GroupNo And St.StatingNo=@StatingNo
");
                var siteGroup = DapperHelp.QueryForObject<DaoModel.SiteGroup>(sql, new { GroupNo = row.GroupNo?.Trim(), StatingNo = row.HangingPieceSiteNo?.Trim() });
                //"select top 1 * from SiteGroup where GroupNo=@GroupNo", new { GroupNo = row.GroupNo?.Trim() });
                var mainTrackNumber = siteGroup.MainTrackNumber.Value;
                ClearHangingPieceProducts(HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(row.HangingPieceSiteNo?.Trim()), row.ProductionNumber.Value, ref errMsg);
            }
        }
        /// <summary>
        /// 转移生产数据
        /// </summary>
        /// <param name="wpHangerData"></param>
        private void TransferProductData(DaoModel.WaitProcessOrderHanger wpHangerData)
        {
            var hangerStatingAllocationItemList = Query<DaoModel.HangerStatingAllocationItem>(new StringBuilder("select * from HangerStatingAllocationItem where HangerNo=?"), null, false, wpHangerData.HangerNo);
            foreach (var pi in hangerStatingAllocationItemList)
            {
                var hpi = BeanUitls<DaoModel.SuccessHangerStatingAllocationItem, DaoModel.HangerStatingAllocationItem>.Mapper(pi);
                hpi.Id = null;
                SuccessHangerStatingAllocationItemDao.Instance.Save(hpi);
                HangerStatingAllocationItemDao.Instance.Delete(pi.Id);
            }
            var hangerProductFlowChartList = Query<DaoModel.HangerProductFlowChart>(new StringBuilder("select * from HangerProductFlowChart where HangerNo=?"), null, false, wpHangerData.HangerNo);
            foreach (var pi in hangerProductFlowChartList)
            {
                var hpi = BeanUitls<DaoModel.SuccessHangerProductFlowChart, DaoModel.HangerProductFlowChart>.Mapper(pi);
                hpi.Id = null;
                SuccessHangerProductFlowChartDao.Instance.Save(hpi);
                HangerProductFlowChartDao.Instance.Delete(pi.Id);
            }
        }

        /// <summary>
        /// 其他未确定的数据
        /// </summary>
        /// <param name="wpHangerData"></param>
        private void OtherUndeterminedData(DaoModel.WaitProcessOrderHanger wpHangerData)
        {
            var hangerProductItemList = Query<DaoModel.HangerProductItem>(new StringBuilder("select * from HangerProductItem where HangerNo=?"), null, false, wpHangerData.HangerNo);
            foreach (var pi in hangerProductItemList)
            {
                var hpi = BeanUitls<DaoModel.SucessHangerProductItem, DaoModel.HangerProductItem>.Mapper(pi);
                hpi.Id = null;
                SucessHangerProductItemDao.Instance.Save(hpi);
                HangerProductItemDao.Instance.Delete(pi.Id);
            }
            var hangerReworkRecordList = Query<DaoModel.HangerReworkRecord>(new StringBuilder("select * from HangerReworkRecord where HangerNo=?"), null, false, wpHangerData.HangerNo);
            foreach (var pi in hangerReworkRecordList)
            {
                var hpi = BeanUitls<DaoModel.SucessHangerReworkRecord, DaoModel.HangerReworkRecord>.Mapper(pi);
                hpi.Id = null;
                SucessHangerReworkRecordDao.Instance.Save(hpi);
                HangerReworkRecordDao.Instance.Delete(pi.Id);
            }
            var statingHangerProductItemList = Query<DaoModel.StatingHangerProductItem>(new StringBuilder("select * from StatingHangerProductItem where HangerNo=?"), null, false, wpHangerData.HangerNo);
            foreach (var pi in statingHangerProductItemList)
            {
                var hpi = BeanUitls<DaoModel.SuccessStatingHangerProductItem, DaoModel.StatingHangerProductItem>.Mapper(pi);
                hpi.Id = null;
                SuccessStatingHangerProductItemDao.Instance.Save(hpi);
                StatingHangerProductItemDao.Instance.Delete(pi.Id);
            }
            var hangerStatingAllocationItemList = Query<DaoModel.HangerStatingAllocationItem>(new StringBuilder("select * from StatingHangerProductItem where HangerNo=?"), null, false, wpHangerData.HangerNo);
            foreach (var pi in hangerStatingAllocationItemList)
            {
                var hpi = BeanUitls<DaoModel.SuccessHangerStatingAllocationItem, DaoModel.HangerStatingAllocationItem>.Mapper(pi);
                hpi.Id = null;
                // SuccessHangerStatingAllocationItemDao.Instance.Save(hpi);
                // HangerStatingAllocationItemDao.Instance.Delete(pi.Id);
            }
            var employeeFlowProductionList = Query<DaoModel.EmployeeFlowProduction>(new StringBuilder("select * from EmployeeFlowProduction where HangerNo=?"), null, false, wpHangerData.HangerNo);
            foreach (var pi in employeeFlowProductionList)
            {
                var hpi = BeanUitls<DaoModel.SucessEmployeeFlowProduction, DaoModel.EmployeeFlowProduction>.Mapper(pi);
                hpi.Id = null;
                SucessEmployeeFlowProductionDao.Instance.Save(hpi);
                EmployeeFlowProductionDao.Instance.Delete(pi.Id);
            }
        }
    }
    class ProductsData
    {
        public IList<DaoModel.Products> ProductsList { set; get; }
        public string MainTrackNumber { set; get; }
        public TcpClient TcpClient { set; get; }
    }
}
