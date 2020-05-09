using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using log4net;
//using Cowboy.Sockets;

namespace SuspeSys.Client.Modules.TcpTest
{
    public partial class HangerToMainTrack : DevExpress.XtraEditors.XtraForm
    {
        protected ILog log = LogManager.GetLogger(typeof(HangerToMainTrack));
        public HangerToMainTrack()
        {
            InitializeComponent();
        }
        public SuspeSys.Domain.Products Products;
        public FlowLayoutPanel FlowLayoutPanel
        {
            get { return flowLayoutPanel1; }
        }
        //public SuspeTcpClient SuspeTcpClient
        //{
        //    get
        //    {
        //        return tcpClient;
        //    }
        //}
        public HangerToMainTrack(SuspeSys.Domain.Products products) : this()
        {
            Products = products;
            groupControl1.Text = string.Format("本次能挂衣架数:" + products.TaskNum + "个 " + " 挂片站【{0}】", products.HangingPieceSiteNo);
            var index = 1;
            for (var p = 0; p < products.TaskNum.Value; p++)
            {
                flowLayoutPanel1.Controls.Add(new LabelControl() { Text = "衣架" + index });
                flowLayoutPanel1.Controls.Add(new TextEdit() { Text = "000" + index });
                index++;
            }
            txtHangerNum.Text = products.TaskNum.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //if (!isTcpConnected)
            //{
            //    XtraMessageBox.Show("tcp服务器未连接!请先连接服务器!");
            //    return;
            //}
            try
            {
                Cursor = Cursors.WaitCursor;
                //SusTransitionManager.StartTransition(Contr, "");
                var hangerSendStatus = new HangerSendStatus(this);
                hangerSendStatus.ShowDialog();
            }
            finally {
                Cursor = Cursors.Default;
            }
        }
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
                
                //TestTcpClient.SuspeTcpClient.ServerDataReceived += TcpClient_ServerDataReceived;
                isTcpConnected = true;
                btnClientConected.Enabled = false;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                btnClientConected.Cursor = Cursors.Default;
            }
        }

        //private void TcpClient_ServerDataReceived(object sender, TcpServerDataReceivedEventArgs e)
        //{
           
        //}
    }
}