using SuspeSys.Client.Action.ProcessOrder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.CustPurchaseOrder.Model
{
   public class CusPurchColorAndSizeModel: ColorAndSizeModel
    {
      //  new public int? Total { get; set; }
        public string CusNo { set; get; }
        public string CusName { set; get; }
        public string PurchaseOrderNo { set; get; }
        public string OrderNo { set; get; }
    }
}
