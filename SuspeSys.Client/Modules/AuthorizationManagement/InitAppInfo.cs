using DevExpress.XtraEditors;
using log4net;
using SuspeSys.Domain.SusEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Modules.AuthorizationManagement
{
    /// <summary>
    /// 初始化授权系统参数
    /// </summary>
    public partial class InitAppInfo : DevExpress.XtraEditors.XtraForm
    {
        protected ILog log = LogManager.GetLogger(typeof(RegIndex));
        Action.Common.CommonAction _action = new Action.Common.CommonAction();
        public InitAppInfo()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            string config = txtConfig.Text.Trim();

            if (string.IsNullOrEmpty(config))
            {
                XtraMessageBox.Show("初始化信息不能为空");
                return;
            }

            try
            {
                CustomerInitInfo info = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerInitInfo>(config);

                List<Domain.ApplicationProfile> profiles = new List<Domain.ApplicationProfile>();

                #region 初始化信息
                //SuspeSysSetting.Default.ClientId = info.ClientId;
                profiles.Add(new Domain.ApplicationProfile
                {
                    ParaValue = info.ClientId,
                    Name = ApplicationProfileEnum.ClientId.ToString()
                });

                //SuspeSysSetting.Default.ClientPwd = info.ClientPwd;
                profiles.Add(new Domain.ApplicationProfile
                {
                    ParaValue = info.ClientPwd,
                    Name = ApplicationProfileEnum.ClientPwd.ToString()
                });

                //SuspeSysSetting.Default.UserName = info.UserName;
                profiles.Add(new Domain.ApplicationProfile
                {
                    ParaValue = info.UserName,
                    Name = ApplicationProfileEnum.UserName.ToString()
                });

                //SuspeSysSetting.Default.UserPwd = info.UserPwd;
                profiles.Add(new Domain.ApplicationProfile
                {
                    ParaValue = info.UserPwd,
                    Name = ApplicationProfileEnum.UserPwd.ToString()
                });

                profiles.Add(new Domain.ApplicationProfile
                {
                    ParaValue = info.CustomerCode,
                    Name = ApplicationProfileEnum.CustomerCode.ToString()
                });

                profiles.Add(new Domain.ApplicationProfile
                {
                    ParaValue = info.CustomerName,
                    Name = ApplicationProfileEnum.CustomerName.ToString()
                });
                #endregion

                
                _action.AddApplicationProfile(profiles);
                SuspeSysSetting.Default.Save();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMessageBox.Show("初始化信息不正确");
            }

        }

        public class CustomerInitInfo
        {
            public string ClientId { get; set; }

            public string ClientPwd { get; set; }

            public string UserName { get; set; }

            public string UserPwd { get; set; }

            public string CustomerName { get; set; }

            public string CustomerCode { get; set; }
        }
    }
}
