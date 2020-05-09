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
    public partial class SetAdjustmentPlanYield : DevExpress.XtraEditors.XtraForm
    {
        public SetAdjustmentPlanYield()
        {
            InitializeComponent();
        }
        public bool IsSaveSucess { set; get; }
        Domain.Products products;
        public SetAdjustmentPlanYield(SusXtraUserControl parentBase, object productsModel = null) : this()
        {
            this.Name = "adjustmentPlanYield";
            this.Text = "调整计划投入产量";
            var p = productsModel as Domain.Products;
            products = p;
            lblProductNumber.Text = p.ProductionNumber.ToString();

            //Init();
        }
        //void Init() {
        //    this.Text = "调整计划投入产量";
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtTaskNum.Text?.Trim())) {
                    XtraMessageBox.Show(string.Format("产量不能为空!"), "温馨提示");
                    return;
                }
                var pEdit=CommonAction.Instance.Get<Domain.Products>(products.Id);
                pEdit.TaskNum = Convert.ToInt32(txtTaskNum.Text);
                CommonAction.Instance.Save<Domain.Products>(pEdit);
                XtraMessageBox.Show(string.Format("保存成功!"), "温馨提示");
                this.Close();
                IsSaveSucess = true;
            }
            finally
            {
                btnSave.Cursor = Cursors.Default;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}