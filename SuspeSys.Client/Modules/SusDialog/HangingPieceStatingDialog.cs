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
using SuspeSys.Client.Action.Products;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Modules.RealtimeInfo;

namespace SuspeSys.Client.Modules.SusDialog
{
    public partial class HangingPieceStatingDialog : DevExpress.XtraEditors.XtraForm
    {
        public HangingPieceStatingDialog()
        {
            InitializeComponent();
        }
        ProductsingInfoIndex pInfoIndex;
        public HangingPieceStatingDialog(ProductsingInfoIndex _pInfoIndex) : this()
        {
            pInfoIndex = _pInfoIndex;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
           var cItem= (comboHangingPiece.SelectedItem as SusComboBoxItem)?.Tag as Domain.StatingModel;
            if (null==cItem) {
                MessageBox.Show("挂片站不能为空!","温馨提示");
                return;
            }
            pInfoIndex.HangingPeiceNo = cItem?.StatingNo.Trim();
            this.Close();
        }

        private void HangingPieceStatingDialog_Load(object sender, EventArgs e)
        {
            var productAction = new ProductsAction();
            var list = productAction.GetHangerPieceStatingList();
            comboHangingPiece.Properties.Items.AddRange(list.Select(f => new SusComboBoxItem() { Tag = f, Text = f.GroupNO + "--" + f.StatingNo }).ToArray());
        }
    }
}