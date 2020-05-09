using DevExpress.XtraEditors;
using SuspeSys.Client.Action;
using SuspeSys.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client
{
    public partial class ChangePwd : XtraForm
    {
        frmLoginAction _action = new frmLoginAction();
        public ChangePwd()
        {
            InitializeComponent();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            string org = txtOrg.Text.Trim();
            string newPwd = txtNew.Text.Trim();
            string confirmPwd = txtConfirm.Text.Trim();

            if (string.IsNullOrWhiteSpace(org))
            {
                XtraMessageBox.Show(string.Format(LanguageAction.Instance.BindLanguageTxt("promptInput"), string.Format(LanguageAction.Instance.BindLanguageTxt("oldPassword"))));//"请输入原始密码");
                return;
            }

            if (string.IsNullOrWhiteSpace(newPwd))
            {

                XtraMessageBox.Show(string.Format(LanguageAction.Instance.BindLanguageTxt("promptInput"), string.Format(LanguageAction.Instance.BindLanguageTxt("newPassword"))));//"请输入新密码");
                return;
            }

            if (!newPwd.Equals(confirmPwd))
            {
                XtraMessageBox.Show(string.Format(LanguageAction.Instance.BindLanguageTxt("promptTwiceNotUnanimous"), string.Format(LanguageAction.Instance.BindLanguageTxt("Password"))));//"两次输入密码不一致");
                return;
            }


            try
            {
                _action.ChangePassword(CurrentUser.Instance.UserName, org, newPwd);
                XtraMessageBox.Show(string.Format(LanguageAction.Instance.BindLanguageTxt("promptUpdateSuccess")));//"密码修改成功");
                this.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
