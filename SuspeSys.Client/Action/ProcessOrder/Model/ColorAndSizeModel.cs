using SuspeSys.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Client.Action.ProcessOrder.Model
{
    public class ColorAndSizeModel
    {
        public string Id { set; get; }
        public string No { set; get; }
        public string ColorValue { set; get; }
        public string ColorDescption { set; get; }
        public string SizeValue1 { set; get; }
        public string SizeDescption { set; get; }
        public int? Total { set; get; }
        public string SizeValue2 { set; get; }
        public string SizeValue3 { set; get; }
        public string SizeValue4 { set; get; }
        public string SizeValue5 { set; get; }
        public string SizeValue6 { set; get; }
        public string SizeValue7 { set; get; }
        public string SizeValue8 { set; get; }
        public string SizeValue9 { set; get; }
        public string SizeValue10 { set; get; }
        public string SizeValue11 { set; get; }
        public string SizeValue12 { set; get; }
        public string SizeValue13 { set; get; }
        public string SizeValue14 { set; get; }
        public string SizeValue15 { set; get; }
        public string SizeValue16 { set; get; }
        public string SizeValue17 { set; get; }
        public string SizeValue18 { set; get; }
        public string SizeValue19 { set; get; }
        public string SizeValue20 { set; get; }
        public int SizeColumnCount { set; get; }
        public object ColorId { get; internal set; }
        public string PO { set; get; }
        public CustomerPurchaseOrder CustomerPurchaseOrder { set; get; }

        public IList<SuspeSys.Domain.CustomerPurchaseOrderColorSizeItem> CustomerPurchaseOrderColorSizeItemList = new List<SuspeSys.Domain.CustomerPurchaseOrderColorSizeItem >();
    }
}
