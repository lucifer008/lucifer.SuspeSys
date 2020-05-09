using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Modules.Ext
{
    /// <summary>
    /// 分配比例
    /// </summary>
    public partial class InputForm : XtraForm
    {
        public decimal Proportion
        {
            get; private set;
        }

        public InputForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtValue == null)
            {
                SuspeTool.ShowMessageBox("分配比例不能为空");
            }

            var proportion = decimal.Parse(this.txtValue.Text);
            if (proportion > 10)
            {
                SuspeTool.ShowMessageBox("分配比例不能大于10");
                return;
            }
            this.Proportion = proportion;

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
