using DevExpress.XtraEditors;
using log4net;
using SuspeSys.Client.Action;
using SuspeSys.Domain.Common;
using SuspeSys.Utils;
using SuspeSys.Utils.Authorization;
using SuspeSys.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Modules.AuthorizationManagement
{
    public partial class AuthorizationIndex : DevExpress.XtraEditors.XtraForm
    {
        protected ILog log = LogManager.GetLogger(typeof(AuthorizationIndex));
        frmLoginAction _action = new frmLoginAction();
        Action.Common.CommonAction _commonAction = new Action.Common.CommonAction();

        public AuthorizationIndex()
        {
            InitializeComponent();
        }

        private void AuthorizationIndex_Load(object sender, EventArgs e)
        {
            //获取授权文件

            string clientName = CustomSysInfo.Instance.ClientName;
            var clientMachines = _action.GetAllClientMachines();
            if (clientMachines == null && clientMachines.Count() == 0)
                XtraMessageBox.Show("客户机配置为空，请联系管理员");
            //var client = _action.GetApplicationProfileByName(Domain.SusEnum.ApplicationProfileEnum.UserPwd)
            var clientMachine = clientMachines.First(o => o.ClientMachineName.Trim().Equals(clientName, StringComparison.OrdinalIgnoreCase));

            


            this.txtCompanyName.ReadOnly = true;
            this.txtClientName.ReadOnly = true;
            this.txtCompanyName.Text = SuspeSysSetting.Default.CompanyName;

            //string path = System.Environment.CurrentDirectory + "\\Grant.Grant";

            if (string.IsNullOrEmpty(clientMachine.AuthorizationInformation))
            {
                XtraMessageBox.Show("授权文件不存在");
                return;
            }

            try
            {
                string content = clientMachine.AuthorizationInformation;

                if (string.IsNullOrEmpty(content))
                {
                    XtraMessageBox.Show("授权数据不存在");
                    return;
                }
                    

                Grant grant = null;
                try
                {
                    string plainText = SuspeSys.Utils.Security.RSACrypto.RSADecrypt(content, ReadOnlyData.privateKey);
                    grant = Newtonsoft.Json.JsonConvert.DeserializeObject<Grant>(plainText);

                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    XtraMessageBox.Show("授权文件不正确");
                }

                this.txtClientName.Text = grant == null ? string.Empty : grant.ServerName;
                this.lblEndDate.Text = grant?.End.ToString("yyyy年MM月dd日");
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("读取授权文件失败，请联系管理员");
            }
            
        }

        private void InitData()
        {
            txtMessage.Enabled = false;
            txtMessage.Properties.AutoHeight = false;
            
        }

        /// <summary>
        /// 在线授权
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOnLine_Click(object sender, EventArgs e)
        {

            GrantOnLine();
        }

        private void GrantOnLine()
        {
            try
            {
                GrantOnLine grantOnLine = new GrantOnLine();
                //1、读取本地授权文件信息
                string path = System.Environment.CurrentDirectory + "\\Grant.Grant";

                if (!System.IO.File.Exists(path))
                {
                    throw new BusinessException("授权文件不存在");
                }

                string content = System.IO.File.ReadAllText(path);

                if (string.IsNullOrEmpty(content))
                    throw new BusinessException("授权数据不存在");

                SuspeSys.Utils.Authorization.ComputeInfo computeInfo = new Utils.Authorization.ComputeInfo();
                string mac = computeInfo.MacAddress.FirstOrDefault();
                if (string.IsNullOrEmpty(mac) || mac.Equals("syspesys-maxaddress-unknown"))
                    throw new BusinessException("没有获取到Mac地址");

                grantOnLine.GrantLocal = content;
                grantOnLine.Mac = SuspeSys.Utils.Security.AESCrypto.EncryptStringAES(mac, ReadOnlyData.AESShareKey);

                ViewModels.OAuthData authData = _commonAction.GetOAuthData();

                SuspeSys.Utils.OAuthHelp oAuth = new OAuthHelp(authData.ClientId,
                                                               authData.ClientPwd,
                                                               authData.UserName,
                                                               authData.UserPwd,
                                                               SuspeSysSetting.Default.Url);
                string id = oAuth.GetPostResponse("CoustmerGrant", Newtonsoft.Json.JsonConvert.SerializeObject(grantOnLine));


                try
                {
                    

                       HttpResponseMessage response = oAuth.Get("CoustmerGrant/" + id.Trim('"'), null);
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        //SuspeSys.Core.Logging.Log.LogInfo(response.Content.ReadAsStringAsync().Result);
                        XtraMessageBox.Show("授权失败，请联系管理员！");
                        return;
                    }

                    string pathGrant = System.Environment.CurrentDirectory + "\\Grant.Grant";

                    
                    var bytes = response.Content.ReadAsStringAsync().Result;
                    if (File.Exists(pathGrant))
                    {
                        File.Move("Grant.Grant",string.Format("Grant{0}.Grant", DateTime.Now.ToString("yyyyMMddhhmmss")));
                    }
                    using (System.IO.StreamWriter stream = new System.IO.StreamWriter(pathGrant, false))
                    {
                        stream.Write(bytes);
                        stream.Flush();
                    }
                    
                  
                    //if ((myStream = saveFileDialog1.OpenFile()) != null)
                    //{
                    //    //byte[] bytes = System.Text.Encoding.Default.GetBytes(server.Credentials);
                    //    myStream.Write(bytes, 0, bytes.Length);
                    //    // Code to write the stream goes here.
                    //    myStream.Close();

                    //    //记录授权日志
                    //}
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    XtraMessageBox.Show(ex.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMessageBox.Show(ex.Message);
                return;
            }

            XtraMessageBox.Show("授权成功，请重启客户端");
        }

        /// <summary>
        /// 生成授权文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                GrantOnLine grantOnLine = new GrantOnLine();
                //1、读取本地授权文件信息
                string path = System.Environment.CurrentDirectory + "\\Grant.Grant";

                if (!System.IO.File.Exists(path))
                {
                    throw new BusinessException("授权文件不存在");
                }

                string content = System.IO.File.ReadAllText(path);

                if (string.IsNullOrEmpty(content))
                    throw new BusinessException("授权数据不存在");

                SuspeSys.Utils.Authorization.ComputeInfo computeInfo = new Utils.Authorization.ComputeInfo();
                string mac = computeInfo.MacAddress.FirstOrDefault();
                if (string.IsNullOrEmpty(mac) || mac.Equals("syspesys-maxaddress-unknown"))
                    throw new BusinessException("没有获取到Mac地址");

                grantOnLine.GrantLocal = content;
                grantOnLine.Mac = SuspeSys.Utils.Security.AESCrypto.EncryptStringAES(mac, ReadOnlyData.AESShareKey);

                string offlineGrant = Newtonsoft.Json.JsonConvert.SerializeObject(grantOnLine);

                try
                {

                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "All files (*.*)|*.*";
                    saveFileDialog1.FileName = "GrantOffline.Grant";

                    DialogResult result = saveFileDialog1.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        try
                        {
                            Stream myStream;
                            var bytes = System.Text.Encoding.Default.GetBytes(offlineGrant);
                            if ((myStream = saveFileDialog1.OpenFile()) != null)
                            {
                                //byte[] bytes = System.Text.Encoding.Default.GetBytes(server.Credentials);
                                myStream.Write(bytes, 0, bytes.Length);
                                // Code to write the stream goes here.
                                myStream.Close();

                                //记录授权日志
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex);
                            XtraMessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);

                    XtraMessageBox.Show(ex.Message);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 使用离线授权文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All files (*.*)|*.*";
            fileDialog.FileName = "Grant.Grant";

            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                                        string path = fileDialog.FileName;

                    if (!System.IO.File.Exists(path))
                    {
                        throw new BusinessException("授权文件不存在");
                    }

                    string content = System.IO.File.ReadAllText(path);

                    if (string.IsNullOrEmpty(content))
                        throw new BusinessException("授权数据不存在");

                    _commonAction.OnLineGrantProcess(CustomSysInfo.Instance.ClientName, content);

                    XtraMessageBox.Show("完成离线续期,授权成功，请重启客户端");

                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    XtraMessageBox.Show(ex.Message);
                }
            }
        }
    }
}
