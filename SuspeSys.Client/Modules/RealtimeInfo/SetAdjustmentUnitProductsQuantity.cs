using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Action.Common;

namespace SuspeSys.Client.Modules.RealtimeInfo
{
    public partial class SetAdjustmentUnitProductsQuantity : DevExpress.XtraEditors.XtraForm
    {
        public SetAdjustmentUnitProductsQuantity()
        {
            InitializeComponent();
        }
        Domain.Products products;
        public bool IsSaveSucess { set; get; }
        public SetAdjustmentUnitProductsQuantity(SusXtraUserControl parentBase, object productsModel = null) : this() {
            this.Text = "调整单位产品数量";
            var p = productsModel as Domain.Products;
            products = p;
            lblProductNumber.Text = p.ProductionNumber.ToString();
        }
        void Init()
        {
            this.Text = "调整单位产品数量";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductsQuantity.Text?.Trim()))
            {
                XtraMessageBox.Show(string.Format("数量不能为空!"), "温馨提示");
                return;
            }
            var pEdit = CommonAction.Instance.Get<Domain.Products>(products.Id);
            pEdit.Unit = txtProductsQuantity.Text;
            CommonAction.Instance.Save<Domain.Products>(pEdit);
            XtraMessageBox.Show(string.Format("保存成功!"), "温馨提示");
            this.Close();
            IsSaveSucess = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}