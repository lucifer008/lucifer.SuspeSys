using DevExpress.XtraEditors;
using log4net;
using SuspeSys.Client.Action;
using SuspeSys.Client.Action.Common;
using SuspeSys.Domain.Common;
using SuspeSys.Domain.SusEnum;
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
using static SuspeSys.Utils.OAuthHelp;

namespace SuspeSys.Client.Modules.AuthorizationManagement
{
    public partial class RegIndex : DevExpress.XtraEditors.XtraForm
    {
        protected ILog log = LogManager.GetLogger(typeof(RegIndex));
        Action.Common.CommonAction _action = new Action.Common.CommonAction();
        private RegIndex()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public RegIndex(string customer, string clientName):this()
        {
            this.txtCompanyName.Text = customer;
            this.txtClientName.Text = clientName;

            if (!string.IsNullOrEmpty(customer)) {
                txtCompanyName.Enabled = false;
            }

            if (!string.IsNullOrEmpty(clientName))
            {
                txtClientName.Enabled = false;
            }
        }

        private void RegIndex_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            //获取配置信息

            string customerInfo = _action.GetApplicationProfileByName(ApplicationProfileEnum.CustomerName);

            if (!string.IsNullOrEmpty(customerInfo))
            {
                this.txtCompanyName.Text = customerInfo;
                txtCompanyName.Enabled = false;
            }
            txtMessage.Enabled = false;
            txtMessage.Properties.AutoHeight = false;

            string clientName = this.txtClientName.Text;
            if (!string.IsNullOrEmpty(clientName))
            {
                var clientMachines = new frmLoginAction().GetAllClientMachines();
                if (clientMachines == null && clientMachines.Count() == 0)
                    XtraMessageBox.Show("客户机配置为空，请联系管理员");
                //var client = _action.GetApplicationProfileByName(Domain.SusEnum.ApplicationProfileEnum.UserPwd)
                var clientMachine = clientMachines.First(o => o.ClientMachineName.Trim().Equals(clientName, StringComparison.OrdinalIgnoreCase));

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
                    log.Error(ex);
                    XtraMessageBox.Show("读取授权文件失败，请联系管理员");
                }
            }

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

                string customer = txtCompanyName.Text.Trim();
                string clientInfo = txtClientName.Text.Trim();
                if (string.IsNullOrEmpty(customer))
                {
                    XtraMessageBox.Show("客户名称不能为空");
                    return;
                }

                if (string.IsNullOrEmpty(customer))
                {
                    XtraMessageBox.Show("客户端名称不能为空");
                    return;
                }


                GrantOnLine grantOnLine = new GrantOnLine();
                

                SuspeSys.Utils.Authorization.ComputeInfo computeInfo = new Utils.Authorization.ComputeInfo();
                string mac = computeInfo.MacAddress.FirstOrDefault();
                if (string.IsNullOrEmpty(mac) || mac.Equals("syspesys-maxaddress-unknown"))
                    throw new BusinessException("没有获取到Mac地址");

                grantOnLine.CustomerName = customer;
                grantOnLine.ClientName = clientInfo;
                grantOnLine.Mac = SuspeSys.Utils.Security.AESCrypto.EncryptStringAES(mac, ReadOnlyData.AESShareKey);

                ViewModels.OAuthData authData = _action.GetOAuthData();

                SuspeSys.Utils.OAuthHelp oAuth = new OAuthHelp(authData.ClientId,
                                                               authData.ClientPwd,
                                                               authData.UserName,
                                                               authData.UserPwd,
                                                               SuspeSysSetting.Default.Url);

                //在线授权
                //string id = oAuth.GetPostResponse("OnlineGrant", Newtonsoft.Json.JsonConvert.SerializeObject(grantOnLine));
                string id;
                var responseId = oAuth.GetPostResponse(oAuth.GetAccessToken(), "OnlineGrant", Newtonsoft.Json.JsonConvert.SerializeObject(grantOnLine));

                if (responseId.StatusCode != System.Net.HttpStatusCode.OK)
                {
                   var messageObj =   Newtonsoft.Json.JsonConvert.DeserializeObject<MessageObj>(responseId.Content.ReadAsStringAsync().Result);
                    if (messageObj != null)
                        XtraMessageBox.Show(messageObj.Message);
                    else
                        XtraMessageBox.Show("授权失败，请练习管理员");
                    return;
                }
                else
                {
                    id = responseId.Content.ReadAsStringAsync().Result;
                }


                try
                {
                    

                    //下载授权文件
                       HttpResponseMessage response = oAuth.Get("CoustmerGrant/" + id.Trim('"'), null);
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        //SuspeSys.Core.Logging.Log.LogInfo(response.Content.ReadAsStringAsync().Result);
                        XtraMessageBox.Show("授权失败，请联系管理员！" + response.RequestMessage.ToString());
                        return;
                    }


                    
                    var content = response.Content.ReadAsStringAsync().Result;

                    var grant = AuthorizationDetection.Instance.GetGrantInfo(content);

                    //授权客户端写入数据库
                    var action = new CommonAction();
                    action.OnLineGrantProcess(clientInfo, content);

                    //授权客户信息写入系统基本配置信息表
                    Domain.ApplicationProfile profile = new Domain.ApplicationProfile();
                    profile.Name = ApplicationProfileEnum.CustomerName.ToString();
                    profile.ParaValue = customer;
                    action.AddApplicationProfile(profile);

                    Domain.ApplicationProfile profileCustomerCode = new Domain.ApplicationProfile()
                    {
                        Name = ApplicationProfileEnum.CustomerCode.ToString(),
                        ParaValue = grant.CompanyId,
                    };
                    action.AddApplicationProfile(profileCustomerCode);

                    //客户端名称写入本地数据库
                    Sqlite.Entity.BasicInfo basicInfo = new Sqlite.Entity.BasicInfo()
                    {
                        Name = Sqlite.Entity.BasicInfoEnum.DefaultClient.ToString(),
                        Value = clientInfo
                    };
                    SuspeSys.Client.Sqlite.Repository.BasicInfoRepository.Instance.Save(basicInfo);

                    lblEndDate.Text = grant.End.ToString("yyyy-MM-dd");

                    //if (File.Exists(pathGrant))
                    //{
                    //    File.Move("Grant.Grant",string.Format("Grant{0}.Grant", DateTime.Now.ToString("yyyyMMddhhmmss")));
                    //}
                    //using (System.IO.StreamWriter stream = new System.IO.StreamWriter(pathGrant, false))
                    //{
                    //    stream.Write(bytes);
                    //    stream.Flush();
                    //}


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

                    _action.OnLineGrantProcess(CustomSysInfo.Instance.ClientName, content);

                    ////文件存在，重命名
                    //if (File.Exists(path))
                    //{
                    //    File.Move("Grant.Grant", string.Format("Grant{0}.Grant", DateTime.Now.ToString("yyyyMMddhhmmss")));
                    //}

                    ////File.Create(path);

                    //string savePath = Path.Combine( System.Environment.CurrentDirectory, "Grant.Grant") ;

                    ////var bytes = System.Text.Encoding.Default.GetBytes(content);
                    //using (System.IO.StreamWriter stream = new System.IO.StreamWriter(savePath, false))
                    //{
                    //    stream.Write(content);
                    //    stream.Flush();
                    //}

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
