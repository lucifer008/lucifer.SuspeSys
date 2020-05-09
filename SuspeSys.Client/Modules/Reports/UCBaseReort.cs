using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SuspeSys.Client.Modules.Reports
{
    public partial class UCBaseReort : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public UCBaseReort()
        {
            InitializeComponent();
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
        }
        public UCBaseReort(XtraUserControl1 ucMain) : this()
        {
            this.ucMain = ucMain;
        }
        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            try
            {
                //  MessageBox.Show(ButtonName.ToString());
                switch (ButtonName)
                {

                    case ButtonName.Query:
                        pcContentTop.Visible =!pcContentTop.Visible;
                        panelControl2.Visible = pcContentTop.Visible;
                        break;
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
                return;
                //Console.WriteLine("异常:"+ex.Message);
            }
            finally
            {
                Common.Utils.SusTransitionManager.EndTransition();
            }
        }
    }
}
