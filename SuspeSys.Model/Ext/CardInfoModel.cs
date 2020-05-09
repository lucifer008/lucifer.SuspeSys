using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext
{
    public class CardInfoModel:CardInfo
    {
        public virtual string ParentId { set; get; }
        public virtual string EmployeeCode { set; get; }
        public virtual string EmployeeName { set; get; }
        public virtual string CardTypeTxt { set; get; }
        public virtual string EnText { set; get; }
        public virtual string MulLoginText { set; get; }

    }
}
