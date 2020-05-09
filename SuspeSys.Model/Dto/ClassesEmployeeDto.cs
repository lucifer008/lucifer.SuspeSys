using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Dto
{
    /// <summary>
    /// 排班Dto
    /// </summary>
    public class ClassesEmployeeDto: ClassesInfo
    {
        public string ClassesEmployeeId { get; set; }

        /// <summary>
        ///排班日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RealName { get; set; }

        

        /// <summary>
        /// 当前时段是否在Time1
        /// </summary>
        /// <returns></returns>
        public bool IsInTime1(TimeSpan dtCurent)
        {

            if (dtCurent > base.Time1GoToWorkDate.Value.TimeOfDay &&
                dtCurent < base.Time1GoOffWorkDate.Value.TimeOfDay)
                return true;

            return false;
        }

        /// <summary>
        /// 当前时段是否在Time1
        /// </summary>
        /// <returns></returns>
        public bool IsInTime2(TimeSpan dtCurent)
        {

            if (dtCurent > base.Time2GoToWorkDate.Value.TimeOfDay &&
                dtCurent < base.Time2GoOffWorkDate.Value.TimeOfDay)
                return true;

            return false;
        }

        /// <summary>
        /// 当前时段是否在Time1
        /// </summary>
        /// <returns></returns>
        public bool IsInTime3(TimeSpan dtCurent)
        {
            

            if (dtCurent > base.Time3GoToWorkDate.Value.TimeOfDay &&
                dtCurent < base.Time3GoOffWorkDate.Value.TimeOfDay)
                return true;

            return false;
        }

        public bool IsInTime()
        {
            var dtCurent = DateTime.Now.TimeOfDay;

            return IsInTime1(dtCurent) && IsInTime2(dtCurent) && IsInTime3(dtCurent);
        }
    }
}
