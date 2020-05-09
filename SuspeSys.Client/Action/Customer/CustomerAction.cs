using SuspeSys.Service.Impl.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuspeSys.Client.Action.Customer.Model;
using System.Collections;

namespace SuspeSys.Client.Action.Customer
{
    public class CustomerAction: BaseAction
    {
       
        public IList<SuspeSys.Domain.Customer> GetAllCustomerList() {
            return customerService.GetAllCustomerList();
        }

        internal IList<SuspeSys.Domain.Customer> SearchCustomer(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, CustomerModel conModel)
        {
            return customerService.SearchCustomer(currentPageIndex,pageSize,out totalCount,ordercondition,conModel.Model.CusName=conModel.SearchKey);
        }
    }
}
