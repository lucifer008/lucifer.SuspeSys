using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SuspeSys.Client.Modules.TcpTest
{
    public partial class TcpConected : DevExpress.XtraEditors.XtraForm
    {
        public TcpConected()
        {
            InitializeComponent();
        }
        public bool IsConnect { private set; get; }
        bool isTcpConnected;
       // SuspeTcpClient tcpClient;
        private void btnClientConected_Click(object sender, EventArgs e)
        {
            try
            {
                btnClientConected.Cursor = Cursors.WaitCursor;
                var port = Convert.ToInt32(txtTcpPort.Text);
                //tcpClient = SuspeTcpClient.Intance;
                //tcpClient.ConnectToServer("127.0.0.1", port);
                //TestTcpClient.StartTcpClient(port);
                //tcpClient = TestTcpClient.SuspeTcpClient;
               // tcpClient.ServerDataReceived += TcpClient_ServerDataReceived;
                isTcpConnected = true;
                IsConnect = true;
                btnClientConected.Enabled = false;
                this.Close();
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.Message);
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                btnClientConected.Cursor = Cursors.Default;
            }
        }
    }
}