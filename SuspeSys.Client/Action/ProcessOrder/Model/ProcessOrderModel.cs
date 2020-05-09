using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaoModel = SuspeSys.Domain;
namespace SuspeSys.Client.Action.ProcessOrder.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessOrderModel
    {
        /// <summary>
        /// 是否复制制单
        /// </summary>
        public bool CopyProcessOrder { set; get; }
        /// <summary>
        /// 是否复制制单明细
        /// </summary>
        public bool CopyProcessOrderItem { set; get; }
        /// <summary>
        /// 是否复制工序
        /// </summary>
        public bool CopyProcessFlow { set; get; }
        /// <summary>
        /// 复制工艺路线图
        /// </summary>
        public bool CopyProcessFlowChart { set; get; }
        public string ProcessOrderId { get;  set; }

        public DaoModel.ProcessOrder ProcessOrder = new DaoModel.ProcessOrder();
        public IList<DaoModel.ProcessOrderColorItemModel> ProcessOrderItemList = new List<DaoModel.ProcessOrderColorItemModel>();
        public DaoModel.ProcessFlowVersion ProcessFlowVersion = new DaoModel.ProcessFlowVersion();
        public IList<DaoModel.ProcessFlow> ProcessFlowList = new List<DaoModel.ProcessFlow>();
        //public IList<DaoModel.ProcessOrderColorItem> ProcessOrderColorItemList = new List<DaoModel.ProcessOrderColorItem>();
        //public IList<DaoModel.ProcessOrderColorSizeItem> ProcessOrderColorSizeItemList = new List<DaoModel.ProcessOrderColorSizeItem>();
    }
}
