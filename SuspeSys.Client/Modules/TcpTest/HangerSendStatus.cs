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
using DevExpress.XtraGrid.Views.Grid;
using System.Threading;
using SusNet.Common.Message;
using SusNet.Common.Utils;

namespace SuspeSys.Client.Modules.TcpTest
{
    public partial class HangerSendStatus : DevExpress.XtraEditors.XtraForm
    {
        public HangerSendStatus()
        {
            InitializeComponent();
        }
        HangerToMainTrack HangerToMainTrack;
        public HangerSendStatus(HangerToMainTrack _hMainTrack) : this()
        {
            HangerToMainTrack = _hMainTrack;
            TcpMessageList = new List<TcpMessage>();
        }
        IList<TcpMessage> TcpMessageList = null;
        private void BindGridHeader()
        {
            var gridView = gridControl1.MainView as GridView;
            gridView.Columns.Clear();
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="挂片站",FieldName="HangingPieceSiteNo",Visible=true},
                    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣架",FieldName="HangerNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="发送内容",FieldName="DataContent",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="发送时间",FieldName="SendDate",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true}
            });
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gridControl1.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gridControl1;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }

        private void HangerSendStatus_Load(object sender, EventArgs e)
        {
            BindGridHeader();

            new Thread(new ThreadStart(HangerPieceSendRequest)).Start();
        }
        void HangerPieceSendRequest()
        {
            var controls = HangerToMainTrack.FlowLayoutPanel.Controls;
            var total = 100 / controls.Count;
            foreach (var ctr in controls)
            {
                if (ctr is TextEdit)
                {
                    var tt = ctr as TextEdit;
                    var hangerNo = tt.Text.Trim();
                    var data = new MessageBody()
                    {
                        DATA1 = hangerNo.Trim(),
                        ID = HangerToMainTrack.Products.HangingPieceSiteNo.Trim()
                    }.GetMessage();
                    TcpMessageList.Add(new TcpMessage()
                    {
                        HangingPieceSiteNo = HangerToMainTrack.Products.HangingPieceSiteNo.Trim(),
                        HangerNo = hangerNo,
                        DataContent = HexHelper.BytesToHexString(data),
                        SendDate = DateTime.Now
                    });
                    //TestTcpClient.SuspeTcpClient.Send(data);
                    this.Invoke(new EventHandler(this.UpdateProgessBar), total);
                    if(total<100)
                        total += total;
                    if (total>100) {
                        total = 100;
                    }
                }
            }
            this.Invoke(new EventHandler(this.UpdateProgessBar), 100);
        }
        void UpdateProgessBar(object state, EventArgs e)
        {
            var p = Convert.ToInt16(state);
            progressBarControl1.EditValue = p;
            gridControl1.DataSource = TcpMessageList;
            gridControl1.Refresh();
            gridControl1.RefreshDataSource();
        }
    }
    class TcpMessage
    {
        public string DataContent { set; get; }
        public string HangerNo { set; get; }
        public string HangingPieceSiteNo { set; get; }
        public DateTime SendDate { set; get; }
    }
}