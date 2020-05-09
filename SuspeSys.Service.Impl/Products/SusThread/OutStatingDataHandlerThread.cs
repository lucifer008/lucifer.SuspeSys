using log4net;
using SuspeSys.Dao;
using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.Base;
using SuspeSys.Service.Impl.Products.PExcption;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Products.SusThread
{
    public class OutStatingDataHandlerThread : ServiceBase
    {

        private OutStatingDataHandlerThread() { }
        public static OutStatingDataHandlerThread Instance { get { return new OutStatingDataHandlerThread(); } }
        public int MainTrackNumber { set; get; }
        public string HangerNo { set; get; }
        public string StatingNo { set; get; }
        public HangerProductFlowChart HangerProductFlowChart { set; get; }
        //public void OutSatingDataHandler()
        //{

        //    //更新衣架工序生产明细的衣架出站时间
        //    var sql = new StringBuilder("select top 1 * from HangerProductItem where HangerNo=? and SiteNo=? and MainTrackNumber=?");
        //    var obj = QueryForObject<HangerProductItem>(sql, null, false, HangerNo, StatingNo, MainTrackNumber);
        //    if (null == obj)
        //    {
        //        var ex = new NoFoundOnlineProductsException(string.Format("【更新衣架工序生产明细的衣架出站时间】 出错:找不到生产明细;主轨:{0} 站点:{1} 衣架号:{2}", MainTrackNumber, StatingNo, HangerNo));
        //        tcpLogError.Error(ex);
        //        throw ex;
        //    }
        //    obj.OutSiteDate = DateTime.Now;
        //    obj.IsSucessedFlow = true;
        //    HangerProductItemDao.Instance.Update(obj);
        //    log.Info("【更新衣架工序生产明细的衣架出站时间】");

        //    if (null == HangerProductFlowChart)
        //    {
        //        var ex = new ApplicationException(string.Format("找不着衣架生产工艺图信息! 主轨:{0} 衣架号:{1} 站点:{2}", MainTrackNumber, HangerNo, StatingNo));
        //        errorLog.Error("【衣架出站】", ex);
        //        return;
        //    }
        //    HangerProductFlowChart.OutSiteDate = DateTime.Now;
        //    HangerProductFlowChart.FlowRealyProductStatingNo = short.Parse(StatingNo);
        //    HangerProductFlowChart.IsFlowSucess = true;
        //    HangerProductFlowChart.Status = HangerProductFlowChartStaus.Successed.Value;//生产完成
        //    HangerProductFlowChartDao.Instance.Update(HangerProductFlowChart);

        //    //记录站点衣架生产记录
        //    var statingHangerProductItem = BeanUitls<StatingHangerProductItem, HangerProductItem>.Mapper(obj);
        //    statingHangerProductItem.Id = null;
        //    statingHangerProductItem.OutSiteDate = DateTime.Now;
        //    statingHangerProductItem.IsReturnWorkFlow = null == HangerProductFlowChart.FlowType ? false : (HangerProductFlowChart.FlowType == 1 ? true : false);
        //    statingHangerProductItem.IsSucessedFlow = true;
        //    statingHangerProductItem.IsReworkSourceStating = HangerProductFlowChart.IsReworkSourceStating;
        //    statingHangerProductItem.Id = StatingHangerProductItemDao.Instance.Insert(statingHangerProductItem);
        //    log.Info("【记录站点衣架生产记录】");

        //    //检查工序是否完成，若完成则转移数据
        //    var isFlowSucess = CANProductsService.Instance.CheckHangerProcessChartIsSuccessed(HangerNo);//CheckFlowIsSuccessed(obj.FlowChartd, (short)obj.FlowIndex);
        //    if (isFlowSucess)
        //    {

        //        var sucessMessage = string.Format("主轨:{0} 最后一站:{1} 衣架号:{2} 已生产完成!", MainTrackNumber, StatingNo, HangerNo);
        //        tcpLogInfo.Info(sucessMessage);

        //        var sqlWaitGen = new StringBuilder(@"select * from WaitProcessOrderHanger where HangerNo=?");
        //        var data = QueryForObject<WaitProcessOrderHanger>(sqlWaitGen, null, false, HangerNo);
        //        var thread = new Thread(CANProductsService.Instance.CopySucessData);
        //        thread.Start(data);

        //        //记录员工产量
        //        var refpModelSucess = new RecordEmployeeFlowProductionModel() { IsEndFlow = true, MainTrackNumber = MainTrackNumber, HangerNo = HangerNo, StatingNo = StatingNo, StatingHangerProductItemId = statingHangerProductItem.Id };
        //        var threadRecordEmployeeFlowProductionSucess = new Thread(new ThreadStart(refpModelSucess.RecordEmployeeFlowProduction));
        //        threadRecordEmployeeFlowProductionSucess.Start();
        //        return;
        //    }

        //    //更新站点出站时间
        //    var sqlHangerStatingAll = new StringBuilder("select * from HangerStatingAllocationItem where HangerNo=? and SiteNo=?");
        //    var hangerStatingAllocationItem = QueryForObject<HangerStatingAllocationItem>(sqlHangerStatingAll, null, false, HangerNo, StatingNo);
        //    if (null != hangerStatingAllocationItem)
        //    {
        //        hangerStatingAllocationItem.OutSiteDate = DateTime.Now;
        //        hangerStatingAllocationItem.Status = (byte)HangerStatingAllocationItemStatus.Successed.Value;//更新完成
        //        //HangerStatingAllocationItemDao.Instance.Update(hangerStatingAllocationItem);
        //    }

        //    //记录员工产量
        //    var refpModel = new RecordEmployeeFlowProductionModel() { MainTrackNumber = MainTrackNumber, HangerNo = HangerNo, StatingNo = StatingNo, StatingHangerProductItemId = statingHangerProductItem.Id };
        //    var threadRecordEmployeeFlowProduction = new Thread(new ThreadStart(refpModel.RecordEmployeeFlowProduction));
        //    threadRecordEmployeeFlowProduction.Start();
        //}
    }
}
