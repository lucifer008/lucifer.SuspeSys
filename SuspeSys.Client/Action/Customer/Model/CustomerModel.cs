using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.Customer.Model
{
    public class CustomerModel
    {
        public SuspeSys.Domain.Customer Model = new Domain.Customer();
        public string SearchKey { set; get; }
    }
}
