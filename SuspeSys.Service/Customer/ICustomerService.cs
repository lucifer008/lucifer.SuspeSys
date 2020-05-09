using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;

namespace SuspeSys.Service.Customer
{
    public interface ICustomerService
    {
        /// <summary>
        /// 查询所有客户
        /// </summary>
        /// <returns></returns>
        IList<SuspeSys.Domain.Customer> GetAllCustomerList();

        IList<SuspeSys.Domain.Customer> Query(Hashtable condition);

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="ordercondition"></param>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        IList<DaoModel.Customer> SearchCustomer(int currentPageIndex, int pageSize, out long totalCount, IDictionary<string, string> ordercondition, string searchKey);
    }
}
