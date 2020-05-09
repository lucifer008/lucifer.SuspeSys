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
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Utils;
using SuspeSys.Domain;

namespace SuspeSys.Client.Modules.ProductionLineSet
{
    public partial class UserParamterSet : SusXtraUserControl
    {
        Action.Common.CommonAction _action = new Action.Common.CommonAction();
        public UserParamterSet()
        {
            InitializeComponent();

            this.txtCompany.Enabled = false;
        }
        public UserParamterSet(XtraUserControl1 xuc) : this() { }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = ("图像文件") + "(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imageString = ImageHelp.ImageToString(openFileDialog.FileName);

                    ApplicationProfile profile = new ApplicationProfile() {
                        Name = ApplicationProfileEnum.CustomerLogo.ToString(),
                        ParaValue = imageString,
                        Memo = "字符串保存图片"
                    };

                    _action.AddApplicationProfile(profile);
                    ShowImage(imageString);
                    XtraMessageBox.Show("修改成功");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserParamterSet_Load(object sender, EventArgs e)
        {

            this.InitData();
        }

        private void InitData()
        {
            string customerLogo = _action.GetApplicationProfileByName(ApplicationProfileEnum.CustomerLogo);
            string customerInfo = _action.GetApplicationProfileByName(ApplicationProfileEnum.CustomerName);

            txtCompany.Text = customerInfo;
            if (!string.IsNullOrEmpty(customerLogo))
                ShowImage(customerLogo);    
        }

        private void ShowImage(string imgString)
        {
            Image img = ImageHelp.StringToImage(imgString);
            pictureLogo.Image = img;
        }


    }
}
