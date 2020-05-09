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
using System.Diagnostics;

namespace SuspeSys.Client.Modules.ProductionLineSet
{
    public partial class ControlConfiguration : DevExpress.XtraEditors.XtraForm
    {
        public ControlConfiguration()
        {
            InitializeComponent();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Cursor = Cursors.WaitCursor;
                var ip = txtRemoteServiceIP.Text.Trim();
                var port = txtRemotePort.Text.Trim();
                if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(port)) {
                    XtraMessageBox.Show("配置项不能为空！");
                    return;
                }
                modifyItem("SusRepmotingIP", ip);
                modifyItem("SussRepmotingPort", port);

               XtraMessageBox.Show("端口修改成功！端口修改后必须重启软件才生效!");
                //DialogResult dr = XtraMessageBox.Show("端口修改成功！端口修改后必须重启软件才生效! 确认重启吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dr == DialogResult.Yes)
                //{
                //    //Application.ExitThread();
                //    //Application.Exit();
                //    //Application.Restart();
                //    //Process.GetCurrentProcess().Kill();
                //    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                //    return;
                //}
                this.Close();
            }
            finally
            {
                btnSave.Cursor = Cursors.Default;
            }
        }

        private void ControlConfiguration_Load(object sender, EventArgs e)
        {
            InitSet();
        }

        private void InitSet()
        {
            txtRemoteServiceIP.Text = valueItem("SusRepmotingIP");
            txtRemotePort.Text = valueItem("SussRepmotingPort");

        }
        public void modifyItem(string keyName, string newKeyValue)
        {
            //修改配置文件中键为keyName的项的值
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            config.AppSettings.Settings[keyName].Value = newKeyValue;
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
        public string valueItem(string keyName)
        {
            //返回配置文件中键为keyName的项的值
            return System.Configuration.ConfigurationManager.AppSettings[keyName];
        }
    }
}