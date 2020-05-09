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
using SuspeSys.Client.Action;
using SuspeSys.Utils.Exceptions;

namespace SuspeSys.Client
{
    public partial class LockScreen : DevExpress.XtraEditors.XtraForm
    {
        frmLoginAction _action = new frmLoginAction();
        private Index _index;
        public LockScreen(Index index)
        {
            InitializeComponent();
            _index = index;
            //_index.Enabled = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(userName))
            {
                XtraMessageBox.Show("请输入用户名","提示信息");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                XtraMessageBox.Show("请输入密码", "提示信息");
                return;
            }

            try
            {
                _action.UnLockScreem(userName, password);
                this.Close();
            }
            catch (BusinessException ex)
            {

                XtraMessageBox.Show(ex.Message);
            }

            
            //_index.Enabled = false;
        }

        private void LockScreen_Load(object sender, EventArgs e)
        {
          
            panelControl1.Location = new Point((this.Width/2)-panelControl1.Width/2, this.Height/2- panelControl1.Height / 2);
        }
    }
}