using SuspeSys.Domain;
using SuspeSys.Domain.Dto;
using SuspeSys.Domain.SusEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Service.Impl.SusTcp
{
    /// <summary>
    /// 
    /// </summary>
    public class KanbanInfoServiceImpl
    {
        private KanbanInfoServiceImpl()
        {

        }

        public static KanbanInfoServiceImpl Instance { get { return new KanbanInfoServiceImpl(); } }


        /// <summary>
        /// 添加lcd看板
        /// </summary>
        /// <param name="kanbanInfo"></param>
        public void Add(KanbanInfo kanbanInfo)
        {
            #region sql
            string sql = @"INSERT INTO [KanbanInfo]
           ([LogId]
           ,[WorkShop]
           ,[GroupNo]
           ,[StationNo]
           ,[CallTime]
           ,[Mechanic]
           ,[FaultCode]
           ,[Fault]
           ,[Status]
           ,[InsertDateTime]
           ,[UpdateDateTime])
     VALUES
           (@LogId
           ,@WorkShop
           ,@GroupNo
           ,@StationNo
           ,@CallTime
           ,@Mechanic
           ,@FaultCode 
           ,@Fault 
           ,@Status
           ,@InsertDateTime 
           ,@UpdateDateTime)";
            #endregion

            if (null == kanbanInfo)
                throw new ArgumentNullException("kanbanInfo 不能为空");

            Dao.DapperHelp.Execute(sql, kanbanInfo);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="mid">主轨</param>
        /// <param name="stationNO">站点</param>
        /// <param name="faultCode">故障代码</param>
        /// <param name="status">状态</param>
        public void Cancel(int mid, string stationNO, string faultCode)
        {
            #region sql
            string sql = $@"
update KanbanInfo  
set Status = {(int)KanbanInfoStatus.Canceled}
from SiteGroup b
where KanbanInfo.WorkShop = b.WorkshopCode and KanbanInfo.GroupNo = b.GroupNO
and KanbanInfo.StationNo = @StationNo
and b.MainTrackNumber = @MainTrackNumber 
and KanbanInfo.FaultCode = @FaultCode
and KanbanInfo.Status ={(int)KanbanInfoStatus.Pending}
";
            #endregion

            Dao.DapperHelp.Execute(sql, new { StationNo = stationNO, MainTrackNumber= mid, FaultCode = faultCode   });
        }

        /// <summary>
        /// 已经处理
        /// </summary>
        /// <param name="mid">主轨</param>
        /// <param name="stationNO">站点</param>
        /// <param name="faultCode">故障代码</param>
        /// <param name="status">状态</param>
        public void Done(int mid, string stationNO, string faultCode)
        {
            #region sql
            string sql = $@"
update KanbanInfo  
set Status = {(int)KanbanInfoStatus.Done}
from SiteGroup b
where KanbanInfo.WorkShop = b.WorkshopCode and KanbanInfo.GroupNo = b.GroupNO
and KanbanInfo.StationNo = @StationNo
and b.MainTrackNumber = @MainTrackNumber 
and KanbanInfo.FaultCode = @FaultCode
and KanbanInfo.Status ={(int)KanbanInfoStatus.Pending}
";
            #endregion

            Dao.DapperHelp.Execute(sql, new { StationNo = stationNO, MainTrackNumber = mid, FaultCode = faultCode });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainTrackNumber"></param>
        /// <param name="stationNo"></param>
        /// <returns></returns>
        public StatingDto GetStating(int mainTrackNumber, string stationNo)
        {
            #region sql
            string sql = @"select a.StatingNo, b.GroupNO,b.WorkshopCode 
From Stating a
left join SiteGroup b on a.SITEGROUP_Id = b.Id
where a.StatingNo = @stationNo and a.MainTrackNumber = @mainTrackNumber and a.deleted=0";
            #endregion

            return Dao.DapperHelp.FirstOrDefault<StatingDto>(sql, new { mainTrackNumber, stationNo });
        }

        internal void CorrectFaultRepairByRepairEmoyeeRepairing(int mainTrackNuber, int statingNo, string employeeName)
        {
            var groupNo = GetStating(mainTrackNuber, statingNo+"").GroupNO?.Trim();
            #region sql
            string sql = $@"
update KanbanInfo
set Mechanic = @Mechanic
where StationNo = @StationNo
and GroupNo = @GroupNo 
and Status ={(int)KanbanInfoStatus.Pending}
";
            #endregion

           var rows= Dao.DapperHelp.Execute(sql, new { StationNo = statingNo, GroupNo = groupNo, Mechanic = employeeName });
        }

        internal void CorrectFaultRepairByRepairEmoyeeRepaired(int mainTrackNuber, int statingNo)
        {
            var groupNo = GetStating(mainTrackNuber, statingNo + "").GroupNO?.Trim();
            #region sql
            string sql = $@"
update KanbanInfo
set Status ={(int)KanbanInfoStatus.Done}
where StationNo = @StationNo
and GroupNo = @GroupNo
";
            #endregion

            var rows = Dao.DapperHelp.Execute(sql, new { StationNo = statingNo, GroupNo = groupNo });
        }
    }
}
