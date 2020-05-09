using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.Ext.CANModel
{
    public class EmployeeLoginInfo: CardLoginInfo
    {
        public virtual string RealName { set; get; }
        public virtual string CardNo { set; get; }
        public virtual string EmployeeId { set; get; }
        /// <summary>
        /// 员工工号
        /// </summary>
        public virtual string Code { set; get; }
        // public string CardNo { set; get; }
    }
}
