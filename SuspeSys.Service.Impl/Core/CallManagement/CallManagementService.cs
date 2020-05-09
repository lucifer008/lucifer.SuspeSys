﻿using Sus.Net.Common.SusBusMessage;
using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Service.Impl.SusTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.Core.CallManagement
{
    /// <summary>
    /// 呼叫管理服务
    /// </summary>
    public class CallManagementService
    {
        private CallManagementService() { }
        public static readonly CallManagementService Instance = new CallManagementService();
        /// <summary>
        /// 呼叫管理开始
        /// </summary>
        /// <param name="request"></param>
        internal void CallManagementAction(CallManagementStartRequestMessage request)
        {
            //2、缺料写入站点，同步到产线实时信息（表Stating字段(FaultInfo)）及看板写入
            var kanbanInfo = this.BuildKanbanInfo(request.MainTrackNuber, request.StatingNo);
            KanbanInfoServiceImpl.Instance.Add(kanbanInfo);

        }
        /// <summary>
        /// 呼叫管理结束
        /// </summary>
        /// <param name="request"></param>
        internal void CallStopManagementAction(CallStopRequestMessage request)
        {
            //2.1【表Stating字段(FaultInfo)】--->清空
            StatingServiceImpl.Instance.UpdateFaultInfo(request.MainTrackNuber, request.StatingNo, string.Empty);

            //2.2 【KanbanInfo】只改状态
            KanbanInfoServiceImpl.Instance.Done(request.MainTrackNuber, request.StatingNo + "", "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private KanbanInfo BuildKanbanInfo(int mainTrackNumber, int statingNo)
        {
            var statingDto = KanbanInfoServiceImpl.Instance.GetStating(mainTrackNumber, statingNo.ToString());

            return new KanbanInfo()
            {
                CallTime = DateTime.Now,
                Fault = "呼叫管理",
                FaultCode = "",
                LogId = Guid.NewGuid().ToString().Replace("-", ""),
                InsertDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                GroupNo = statingDto.GroupNO,
                Mechanic = string.Empty,
                StationNo = statingNo.ToString(),
                Status = (int)KanbanInfoStatus.Pending,
                WorkShop = statingDto.WorkshopCode,
            };
        }
    }
}
