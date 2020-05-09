using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuspeSys.Domain;
using SuspeSys.Service.Impl.Products;
using SuspeSys.Service.Impl.Products.PExcption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.Tests
{
    [TestClass()]
    public class OutSiteServiceTests
    {
        [TestMethod()]
        public void GetHangerNextSiteTest()
        {
            int mainTrackNumber = 0;
            string processFlowChartId = "a3fc87434ac54a41a6fcdbaadfcd7a80";
            int flowIndex = 1;
            string nextStatingNo = null;
            int outMainTrackNumber = 1;
            HangerProductFlowChart hpFlowChart = null;
            ProcessFlowChartFlowRelation processFlowChartFlowRelation = null;
            //OutSiteService.Instance.GetHangerNextSite(mainTrackNumber, processFlowChartId, flowIndex, ref nextStatingNo, ref processFlowChartFlowRelation, ref outMainTrackNumber);
            //Console.WriteLine(string.Format("maintrackNumber:{0} next stating:{1} outMainTrackNumber:{2}", mainTrackNumber, nextStatingNo,outMainTrackNumber));
            //flowIndex = 2;
            //OutSiteService.Instance.GetHangerNextSite(mainTrackNumber, processFlowChartId, flowIndex, ref nextStatingNo, ref processFlowChartFlowRelation, ref outMainTrackNumber);
            //Console.WriteLine(string.Format("maintrackNumber:{0} next stating:{1} outMainTrackNumber:{2}", mainTrackNumber, nextStatingNo, outMainTrackNumber));
            flowIndex = 3;
            //OutSiteService.Instance.GetHangerNextSite(mainTrackNumber,null, flowIndex, ref nextStatingNo, ref processFlowChartFlowRelation, ref outMainTrackNumber,ref hpFlowChart);
            //Console.WriteLine(string.Format("maintrackNumber:{0} next stating:{1} outMainTrackNumber:{2}", mainTrackNumber, nextStatingNo, outMainTrackNumber));
        }
        [TestMethod]
        public void TestCusExcpetion()
        {
            try
            {
                var index = 30;
                if (0 == index)
                {
                    throw new Exception("index==0");
                }
                else {
                    throw new FullStatingExcpetion("index 不等于0");
                }
            }
            catch (FullStatingExcpetion ex)
            {
                var mess = string.Format("捕获到异常:{0}",ex.GetType().ToString());
                Console.WriteLine(mess);
            }
            catch (Exception ex)
            {
                var mess = string.Format("捕获到异常:{0}", ex.GetType().ToString());
                Console.WriteLine(mess);
            }
        }
    }
}