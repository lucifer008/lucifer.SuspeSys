using System;
using System.Text;
using System.Collections.Generic;
using SuspeSys.Domain.SusEnum;

namespace SuspeSys.Domain {

    /// <summary>
    /// 员工 扩展Model
    /// </summary>
    [Serializable]
    public class EmployeeModel : Employee {

        public EmployeeModel() { }
        public virtual string DeptmentName
        {
            get;set;
        }

        public virtual string WorkTypeName
        {
            get;set;
        }

        public virtual string SiteGroupName
        {
            get;set;
        }
        public virtual string SiteGroupNo
        {
            get; set;
        }
        public virtual string StatingNo
        {
            get; set;
        }
        //public string PositionName
        //{
        //    get 
        //    {
        //        return base.
        //    }
        //}

        public virtual string AreaName
        {
            get;set;
        }

        public string SexName
        {
            get
            {
                int sexInt = base.Sex.HasValue ? (int)base.Sex : 1;

                Support.Enums.Sex os = (Support.Enums.Sex)sexInt;
                return os.Description();
            }
        }
    }
}
