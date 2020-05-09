using SuspeSys.Service.Customer;
using SuspeSys.Service.Impl.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Service.Impl.Customer
{
    public class CustomerServiceImpl : ServiceBase, ICustomerService
    {

        public IList<SuspeSys.Domain.Customer> GetAllCustomerList() {
            return SuspeSys.Dao.CustomerDao.Instance.GetAll();
        }
        public IList<SuspeSys.Domain.Customer> Query(Hashtable condition) {
            //Query<>()
            return null;
        }

        public IList<DaoModel.Customer> SearchCustomer(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey)
        {
            string queryString = "select * from Customer where deleted=0 ";
            string[] paramValues =null;
            if (!string.IsNullOrEmpty(searchKey)) {
                queryString += string.Format(@" AND (CusNo like ? OR CusName like ? OR Tel like ?)");
                paramValues = new string[] { string.Format("%{0}%", searchKey) , string.Format("%{0}%", searchKey) , string.Format("%{0}%", searchKey) };
            }
            var rslt1 = Query<DaoModel.Customer>(new System.Text.StringBuilder(queryString), currentPageIndex, pageSize, out totalCount, ordercondition, false, paramValues);
            return rslt1;
        }
    }
}
