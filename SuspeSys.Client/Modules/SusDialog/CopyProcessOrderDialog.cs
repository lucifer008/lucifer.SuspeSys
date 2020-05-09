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
using DevExpress.XtraEditors.Controls;
using log4net;

namespace SuspeSys.Client.Modules.SusDialog
{
    public partial class CopyProcessOrderDialog : DevExpress.XtraEditors.XtraForm
    {
        protected ILog log = LogManager.GetLogger(typeof(CopyProcessOrderDialog));
        public CopyProcessOrderDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            btnOk.Cursor = Cursors.WaitCursor;
            try
            {
                BindCopyProcessOrderItem();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMessageBox.Show(ex.Message);
            }
            finally {
                btnOk.Cursor = Cursors.Default;
            }
        }
        void BindCopyProcessOrderItem() {
            foreach (CheckedListBoxItem item in checkedListBoxControl1.Items) {
                if (item.CheckState==CheckState.Checked && item.Value.ToString().Equals("cpProcessOrder")) {
                    CopyProcessFlow = true;
                }
                if (item.CheckState == CheckState.Checked && item.Value.ToString().Equals("cpProcessItem"))
                {
                    CopyProcessOrderItem = true;
                }
                if (item.CheckState == CheckState.Checked && item.Value.ToString().Equals("cpProcessFlow"))
                {
                    CopyProcessFlow = true;
                }
                if (item.CheckState == CheckState.Checked && item.Value.ToString().Equals("cpProcessFlowChart"))
                {
                    CopyProcessFlowChart = true;
                }
            }
        }
        /// <summary>
        /// 是否复制制单
        /// </summary>
        public bool CopyProcessOrder { private set; get; }
        /// <summary>
        /// 是否复制制单明细
        /// </summary>
        public bool CopyProcessOrderItem { private set; get; }
        /// <summary>
        /// 是否复制工序
        /// </summary>
        public bool CopyProcessFlow { private set; get; }
        /// <summary>
        /// 复制工艺路线图
        /// </summary>
        public bool CopyProcessFlowChart { private set; get; }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}