using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Action.TcpTest;
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using SuspeSys.Client.Action.TcpTest.Model;
using SuspeSys.SusRedisService.SusRedis;
using SuspeSys.SusRedis.SusRedis.SusConst;

namespace SuspeSys.Client.Modules.TcpTest
{
    public partial class TcpTestMain : SusXtraUserControl
    {
        public TcpTestMain()
        {
            InitializeComponent();
        }
        public TcpTestMain(XtraUserControl1 _ucMain) : this()
        {
            this.ucMain = _ucMain;
        }

        private void TcpTestMain_Load(object sender, EventArgs e)
        {
            //BindData();
         //   TestTcpClient.SuspeTcpClient.ServerDataReceived += TcpClient_ServerDataReceived;
        }
        TcpTestAction tcpTestAction = new TcpTestAction();
        private void BindData()
        {
            string productionNumber = "1";
            string hangingPieceSiteNo = "1010";
            var product = tcpTestAction.GetProducts(productionNumber, hangingPieceSiteNo);
            var processChartList = tcpTestAction.GetProcessChartStatingList(productionNumber, hangingPieceSiteNo);
            foreach (var pc in processChartList)
            {
                var tab = new XtraTabPage();
                tab.Tag = pc;
                tab.Text = string.Format("站点【{0}】-->【{1}】", pc.No?.Trim(), pc.StatingRoleName?.Trim());
                tab.ShowCloseButton = DefaultBoolean.True;
                var gc = new GridControl();
                gc.Tag = pc;
                gc.Dock = DockStyle.Fill;
                BindGridHeader(gc);
                tab.Controls.Add(gc);
                xtraTabControl1.TabPages.Add(tab);
            }

        }
        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName="SNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣架号",FieldName="HangerNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessFlowCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessFlowName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="动作",FieldName="Content",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="时间",FieldName="InsertDateTime",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="操作",FieldName="InsertDateTime",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit() { TextEditStyle=DevExpress.XtraEditors.Controls.TextEditStyles.Standard},Visible=true},

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }

        bool isTcpConnected;
        //SuspeTcpClient tcpClient;
        private void btnClientConected_Click(object sender, EventArgs e)
        {
            try
            {
                btnClientConected.Cursor = Cursors.WaitCursor;
                //var port = Convert.ToInt32(txtTcpPort.Text);
                //tcpClient = SuspeTcpClient.Intance;
                //tcpClient.ConnectToServer("127.0.0.1", port);
                
                isTcpConnected = true;
                btnClientConected.Enabled = false;
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

        //private void TcpClient_ServerDataReceived(object sender, Cowboy.Sockets.TcpServerDataReceivedEventArgs e)
        //{
        //    byte[] dstArray = new byte[9];
        //    ////Buffer.BlockCopy(e.Data, 0, dstArray, e.DataOffset, 6);
        //    var d = e.DataOffset;
        //    for (var i = 0; i < 9; i++)
        //    {
        //        dstArray[i] = e.Data[d + i];
        //    }
        //    var data = HexHelper.byteToHexStr(dstArray);
        //    this.Invoke(new EventHandler(ReceivedHandler), data);
        //}
        IList<TcpMessageModel> tcpMessageList = new List<TcpMessageModel>();
        void ReceivedHandler(object sender, EventArgs e)
        {
            tcpMessageList.Add(new TcpMessageModel() { SNo=""+(tcpMessageList.Count+1),Content=sender.ToString()});
            var cl=xtraTabControl1.TabPages[0].Controls[0];
            var gc = cl as GridControl;
            if (null!=gc) {
                gc.DataSource = tcpMessageList;
                gc.RefreshDataSource();
            }
        }

        private void btnPublicMessage_Click(object sender, EventArgs e)
        {
            var st = SusRedisClientDecode.subcriber.Publish(SusRedisConst.PUBLIC_TEST,"helo");
        }
    }
}
