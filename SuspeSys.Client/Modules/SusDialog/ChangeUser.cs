using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Client.Action;
using SuspeSys.Client.SusException;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Modules.SusDialog
{
   

    public partial class ChangeUser : XtraForm
    {
        frmLoginAction _action = new frmLoginAction();
        Index _index;
        private ChangeUser()
        {
            InitializeComponent();
        }
        public ChangeUser(Index index):this()
        {
            _index = index;
            this.CenterToScreen();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            string clientName = cmClient.EditValue?.ToString();

            if (string.IsNullOrWhiteSpace(userName))
            {
                XtraMessageBox.Show("用户名不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                XtraMessageBox.Show("密码不能为空");
                return;
            }

            bool loginResult =  _action.Login(userName, password, clientName);
            if (loginResult)
            {
                //_index.Close();
                DialogResult = DialogResult.OK;
            }

        }

        private void ChangeUser_Load(object sender, EventArgs e)
        {
            this.cmClient.Properties.DataSource = _action.GetAllClientMachines();
            cmClient.Properties.Columns.Add(new LookUpColumnInfo("ClientMachineName"));
            cmClient.Properties.ValueMember = "ClientMachineName";
            cmClient.Properties.DisplayMember = "ClientMachineName";
            cmClient.Properties.ShowHeader = false;
            cmClient.Properties.NullText = "请选择客户机";
        }
    }
}
