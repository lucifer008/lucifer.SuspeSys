using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Domain;
using SuspeSys.Utils;
using SuspeSys.Dao;
using SuspeSys.Dao.Nhibernate;
using System.Threading;
using SusNet.Common.Message;
using SusNet.Common.Utils;

namespace SuspeSys.CoreTest.FaultRepair
{
    [TestClass]
    public class FaultRepairTest : TestBase
    {
        [TestMethod]
        public void InitFaultData()
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var clothingVehicleType = new ClothingVehicleType();
                    clothingVehicleType.Code = "1";
                    clothingVehicleType.Name = "类别1";
                    clothingVehicleType.Id = GUIDHelper.GetGuidString();
                    ClothingVehicleTypeDao.Instance.Save(clothingVehicleType, true);
                    for (var fcIndex = 1; fcIndex < 6; fcIndex++)
                    {
                        var fct = new FaultCodeTable();
                        fct.SerialNumber = fcIndex + "";
                        fct.ClothingVehicleType = clothingVehicleType;
                        fct.FaultCode = $"{clothingVehicleType.Code?.Trim()}-" + fcIndex + "";
                        fct.FaultName = $"类别:{clothingVehicleType.Name?.Trim()}的故障代码:${fcIndex}";
                        FaultCodeTableDao.Instance.Save(fct, true);
                    }
                    session.Flush();
                    trans.Commit();
                }
            }
        }
        [TestMethod]
        public void InitClothingVehicleData()
        {
            FaultRepairTest.isOpenTcp = false;
            var clothingVehicleType = ClothingVehicleTypeDao.Instance.GetById("5b0931faaf294759a5f12530b8320c24");
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    for (var index = 1230; index < 1235; index++)
                    {
                        var cv = new ClothingVehicle();
                        cv.ClothingVehicleType = clothingVehicleType;
                        cv.CardNo = index + "";
                        cv.Brand = "brand1";
                        cv.Model = "model1";
                        cv.Model = "ProductsNo" + index;
                        cv.PurchaseDate = DateTime.Now;
                        ClothingVehicleDao.Instance.Save(cv, true);
                    }
                    session.Flush();
                    trans.Commit();
                }
                //isOpenTcp = false;
            }
            log.Info($"------------------------------ClothingVehicle初始化完成!-----------------------------------------------");
        }
        [TestMethod]
        public void InitMechanicEmployees()
        {
            FaultRepairTest.isOpenTcp = false;
            using (var session = SessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    for (var index = 2230; index < 2235; index++)
                    {
                        var me = new MechanicEmployees();
                        me.CardNo = index + "";
                        me.Code = "2230";
                        me.Status = "1";
                        me.RealName = "zsan"+index;
                        MechanicEmployeesDao.Instance.Save(me, true);
                    }
                    session.Flush();
                    trans.Commit();
                }
                //isOpenTcp = false;
            }
            log.Info($"------------------------------MechanicEmployees初始化完成!-----------------------------------------------");
        }
        
        [TestMethod]
        public void TestClothingVehicleLogin()
        {
            var cardNo = "1230";
            var mainTrackNumber = 1;
            var statingNo = 1;
            var hexMessage = string.Format("{0} {1} 06 FF 00 60 00 {2}", HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo), HexHelper.TenToHexString10Len(cardNo));

            log.InfoFormat("TestClothingVehicleLogin---->{0}", hexMessage);
            var data = HexHelper.StringToHexByte(hexMessage);
            MessageBody message = new MessageBody(data);
            client.SendData(message.GetBytes());
            log.InfoFormat("TestClothingVehicleLogin---->{0}", hexMessage);

            Thread.CurrentThread.Join(60000);
        }
        [TestMethod]
        public void TestFaultRepairUpload()
        {
            var mainTrackNumber = 1;
            var statingNo = 1;
            var hexMessage = string.Format("{0} {1} 06 FF 02 1B 00 00 00 00 00 00", HexHelper.TenToHexString2Len(mainTrackNumber), HexHelper.TenToHexString2Len(statingNo));

            log.InfoFormat("TestClothingVehicleLogin---->{0}", hexMessage);
            var data = HexHelper.StringToHexByte(hexMessage);
            MessageBody message = new MessageBody(data);
            client.SendData(message.GetBytes());
            log.InfoFormat("TestClothingVehicleLogin---->{0}", hexMessage);

            Thread.CurrentThread.Join(60000);
        }
        [TestMethod]
        public void TestFaultRepairRequest()
        {
            var mainTrackNumber = 1;
            var statingNo = 1;
            var clothingVehicleTypeCode = 1;
            var hexMessage = string.Format($"{HexHelper.TenToHexString2Len(mainTrackNumber)} {HexHelper.TenToHexString2Len(statingNo)} 06 FF 02 24 00 00 00 00 00 {HexHelper.TenToHexString2Len(clothingVehicleTypeCode)}");

            log.InfoFormat("TestFaultRepairRequest---->{0}", hexMessage);
            var data = HexHelper.StringToHexByte(hexMessage);
            MessageBody message = new MessageBody(data);
            client.SendData(message.GetBytes());
            log.InfoFormat("TestFaultRepairRequest---->{0}", hexMessage);

            Thread.CurrentThread.Join(60000);
        }
        [TestMethod]
        public void TestFaultRepairClothingTypeAndFaultCodeRequest()
        {
            var mainTrackNumber = 1;
            var statingNo = 1;
            var clothingVehicleTypeCode = 1;
            var faultCode = 1;
            var hexMessage = string.Format($"{HexHelper.TenToHexString2Len(mainTrackNumber)} {HexHelper.TenToHexString2Len(statingNo)} 06 FF 02 25 00 00 00 00 {HexHelper.TenToHexString2Len(clothingVehicleTypeCode)}{HexHelper.TenToHexString2Len(faultCode)}");

            log.InfoFormat("TestFaultRepairClothingTypeAndFaultCodeRequest---->{0}", hexMessage);
            var data = HexHelper.StringToHexByte(hexMessage);
            MessageBody message = new MessageBody(data);
            client.SendData(message.GetBytes());
            log.InfoFormat("TestFaultRepairClothingTypeAndFaultCodeRequest---->{0}", hexMessage);

            Thread.CurrentThread.Join(60000);
        }
        [TestMethod]
        public void TestFaultRepairStart()
        {
            var mainTrackNumber = 1;
            var statingNo = 1;
            var repairEmployeeCardNo = "2230";
            var hexMessage = string.Format($"{HexHelper.TenToHexString2Len(mainTrackNumber)} {HexHelper.TenToHexString2Len(statingNo)} 06 FF 03 1D 00  {HexHelper.TenToHexString10Len(repairEmployeeCardNo)}");

            log.InfoFormat("TestFaultRepairStart---->{0}", hexMessage);
            var data = HexHelper.StringToHexByte(hexMessage);
            MessageBody message = new MessageBody(data);
            client.SendData(message.GetBytes());
            log.InfoFormat("TestFaultRepairStart---->{0}", hexMessage);

            Thread.CurrentThread.Join(60000);
        }
        [TestMethod]
        public void TestFaultRepairSuccess()
        {
            var mainTrackNumber = 1;
            var statingNo = 1;
            var repairEmployeeCardNo = "2230";
            var hexMessage = string.Format($"{HexHelper.TenToHexString2Len(mainTrackNumber)} {HexHelper.TenToHexString2Len(statingNo)} 06 FF 03 1E 00  {HexHelper.TenToHexString10Len(repairEmployeeCardNo)}");

            log.InfoFormat("TestFaultRepairSuccess---->{0}", hexMessage);
            var data = HexHelper.StringToHexByte(hexMessage);
            MessageBody message = new MessageBody(data);
            client.SendData(message.GetBytes());
            log.InfoFormat("TestFaultRepairSuccess---->{0}", hexMessage);

            Thread.CurrentThread.Join(60000);
        }
        [TestMethod]
        public void TestFaultRepairStop()
        {
            var mainTrackNumber = 1;
            var statingNo = 1;
            var cardNo = "1003479933";
            var hexMessage = string.Format($"{HexHelper.TenToHexString2Len(mainTrackNumber)} {HexHelper.TenToHexString2Len(statingNo)} 06 FF 02 1C 00  {HexHelper.TenToHexString10Len(cardNo)}");

            log.InfoFormat("TestFaultRepairStop---->{0}", hexMessage);
            var data = HexHelper.StringToHexByte(hexMessage);
            MessageBody message = new MessageBody(data);
            client.SendData(message.GetBytes());
            log.InfoFormat("TestFaultRepairStop---->{0}", hexMessage);

            Thread.CurrentThread.Join(60000);
        }
    }
}
