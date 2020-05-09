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
using SuspeSys.Client.Action.Report;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Modules.ProduceData;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Domain.Ext.ReportModel;
using SuspeSys.Client.Action.Common;
using SusNet.Common.BusModel;
using SuspeSys.Utils.Reflection;
using SusNet.Common.Model;
using SuspeSys.Domain;

namespace SuspeSys.Client.Modules.RealtimeInfo
{
    public partial class CoatHangerIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        private bool isQuery = false;

        public CoatHangerIndex(XtraUserControl1 _ucMain, bool _isQuery)
        {
            InitializeComponent();
            RegisterEvent();
            ucMain = _ucMain;
            isQuery = _isQuery;
            BindGridHeader(susGrid1.DataGrid);
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
            //Query(1);
        }
        private string cardNo;
        public CoatHangerIndex(XtraUserControl1 _ucMain, string _cardNo)
        {
            InitializeComponent();
            RegisterEvent();
            ucMain = _ucMain;
            if (!string.IsNullOrEmpty(_cardNo))
                cardNo = int.Parse(_cardNo).ToString();
            txtHangerNo.Text = cardNo;
            isQuery = false;
            BindGridHeader(susGrid1.DataGrid);
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
            Query(1);
        }

        void RegisterEvent()
        {
            this.Load += CoatHangerIndex_Load;
            susGrid1.gridContextMenuStrip.Items.Clear();
            var lookProcessFlowChart = new ToolStripMenuItem() { Text = "查看工艺图" };


            lookProcessFlowChart.Click += LookProcessFlowChart_Click; ;
            susGrid1.gridContextMenuStrip.Items.AddRange(new ToolStripMenuItem[] { lookProcessFlowChart });
        }

        private void LookProcessFlowChart_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var gv = susGrid1.DataGrid.MainView as GridView;
                if (null == gv) return;
                var coatHangerIndexModel = gv.GetRow(gv.FocusedRowHandle) as CoatHangerIndexModel;
                var tab = new SusXtraTabPage();
                var products = CommonAction.Instance.Get<SuspeSys.Domain.Products>(coatHangerIndexModel.ProductsId);
                tab.Text = string.Format("工艺路线图[{0}]", products.LineName?.Trim());
                tab.Name = string.Format("工艺路线图[{0}]", products.LineName?.Trim());
                var productsModel = Utils.Reflection.BeanUitls<SuspeSys.Domain.ProductsModel, SuspeSys.Domain.Products>.Mapper(products);
                productsModel.ProcessFlowChartId = products.ProcessFlowChart?.Id;
                XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new ProcessFlowChartIndex(ucMain, productsModel));
                //SusTransitionManager.EndTransition();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void CoatHangerIndex_Load(object sender, EventArgs e)
        {

        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }

        void Query(int currentPageIndex)
        {

            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var conModel = string.Empty;
            IDictionary<string, string> ordercondition = new Dictionary<string, string>();
            cardNo = txtHangerNo.Text?.Trim();
            //var processOrderNo = txtProcessOrderNo.Text?.Trim();
            //var styleNo = txtStyleNo.Text?.Trim();
            //var flowNo = txtFlowNo.Text?.Trim();
            //var flowName = txtFlowName.Text?.Trim();
            //var pieceNum = txtPiece.Text?.Trim();
            //var colorCode = txtColorCode.Text?.Trim();
            //var colorName = txtColorName.Text?.Trim();
            //var flowCode = txtFlowCode.Text?.Trim();
            //var sizeCode = txtSizeCode.Text?.Trim();
            //var sizeName = txtSizeName.Text?.Trim();
            //var statingNo = txtSiteNo.Text?.Trim();
            if (string.IsNullOrEmpty(cardNo))
            {
                MessageBox.Show("请输入衣架号后再查询!");
                return;
            }
            if (!string.IsNullOrEmpty(cardNo))
            {
                try
                {
                    var listHangerNo = CommonAction.Instance.QueryBySql<SuspeSys.Domain.Hanger>("select HangerNo from Hanger where HangerNo=@HangerNo", new { HangerNo = cardNo });
                    if (listHangerNo.Count() == 0)
                    {
                        MessageBox.Show(string.Format("卡【{0}】未注册!不是衣架卡!", cardNo));
                        return;
                    }

                    conModel += string.Format(" AND HangerNo='{0}'", int.Parse(cardNo));
                }
                catch
                {
                    MessageBox.Show("衣架格式错误!");
                    return;
                }
            }
            //if (!string.IsNullOrEmpty(processOrderNo))
            //{
            //    conModel += string.Format(" AND ProcessOrderNo like '%{0}%'", processOrderNo);
            //}
            //if (!string.IsNullOrEmpty(styleNo))
            //{
            //    conModel += string.Format(" AND ProductsId in(select id from Products where StyleNo like '%{0}%')", styleNo);
            //}
            //if (!string.IsNullOrEmpty(flowNo))
            //{
            //    conModel += string.Format(" AND FlowNo like '%{0}%'", flowNo);
            //}
            //if (!string.IsNullOrEmpty(flowName))
            //{
            //    conModel += string.Format(" AND FlowName like '%{0}%'", flowName);
            //}
            //if (!string.IsNullOrEmpty(flowCode))
            //{
            //    conModel += string.Format(" AND FlowCode like '%{0}%'", flowCode);
            //}
            //if (!string.IsNullOrEmpty(colorCode))
            //{
            //    conModel += string.Format(" AND PColor like '%{0}%'", colorCode);
            //}
            //if (!string.IsNullOrEmpty(colorName))
            //{
            //    conModel += string.Format(" PColor in(select ColorValue from PoColor where ColorDescption='{0}')", colorCode);
            //}
            //if (!string.IsNullOrEmpty(sizeCode))
            //{
            //    conModel += string.Format(" AND PSize like '%{0}%'", sizeCode);
            //}
            //if (!string.IsNullOrEmpty(sizeName))
            //{
            //    conModel += string.Format(" AND PSize in(select Size from PSize where SizeDesption='{0}')", sizeName);
            //}
            //if (!string.IsNullOrEmpty(statingNo))
            //{
            //    conModel += string.Format(" AND StatingNo='{0}'", int.Parse(statingNo));
            //}
            conModel += string.Format(" AND FlowIndex>0");
            ordercondition.Add("OutSiteDate", "Asc");
            ordercondition.Add("FlowIndex", "Asc");
            SuspeSys.Domain.Ext.ReportModel.CoatHangerIndexModel endFlow = null;
            var list = ReportQueryAction.Instance.SearchCoatHangerInfo(currentPageIndex, pageSize, out totalCount, ordercondition, conModel,ref endFlow, cardNo);
            list.ToList().ForEach(delegate (CoatHangerIndexModel hanger) {
                hanger.GroupStatingName = hanger.GroupNo?.Trim() + "-" + hanger.StatingNo;
            });
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
            txtFlowChart.Text = string.Empty;
            txtFlowChart.ToolTip = string.Empty;
            if (list.Count > 0)
            {
                var flowChartId = list[0].ProcessChartId;
                if (!string.IsNullOrEmpty(flowChartId))
                {
                    var flowChartName = CommonAction.Instance.QueryObjectBySql<ProcessFlowChart>("select LinkName from ProcessFlowChart where Id=@id", new { id = flowChartId })?.LinkName?.Trim();
                    txtFlowChart.Text = flowChartName;
                    txtFlowChart.ToolTip = flowChartName;
                }
            }
           
            txtColorCode.Text = endFlow?.PColor?.Trim();
            txtColorName.Text = endFlow?.ColorName?.Trim();
            txtSizeCode.Text = endFlow?.PSize?.Trim();
            txtSizeName.Text = endFlow?.SizeName?.Trim();
            txtStyleNo.Text = endFlow?.StyleNo?.Trim();
            txtPiece.Text = endFlow?.PieceNum?.Trim();
            txtSiteNo.Text = endFlow?.GroupNo?.Trim()+ "-" +endFlow?.StatingNo?.ToString();
            cbSiteIn.Checked = endFlow == null ? false : endFlow.IsInStating;
            cbSucessPiece.Checked = endFlow == null ? false : endFlow.IsSuccess;
            txtProcessOrderNo.Text = endFlow?.ProcessOrderNo?.Trim();
            txtFlowCode.Text = endFlow?.FlowCode?.Trim();
            txtFlowNo.Text = endFlow?.FlowNo?.Trim();
            txtFlowName.Text = endFlow?.FlowName?.Trim();
        }
        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new DevExpress.XtraGrid.Views.Grid.GridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="ProductsId",FieldName= "ProductsId",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣架号",FieldName="HangerNo",Visible=true,Name="HangerNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序顺序",FieldName="FlowIndex",Visible=true,Name="FlowIndex"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowNo",Visible=true,Name="DefaultFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="FlowCode",Visible=true,Name="ProcessCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="FlowName",Visible=true,Name="ProcessName"},

                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="加工时间",FieldName="CompareDate",ColumnEdit=new RepositoryItemTextEdit(),Visible=true,Name="CompareDate"},
                 /// new DevExpress.XtraGrid.Columns.GridColumn() { Caption="主轨",FieldName="MainTrackNumber",Visible=true,Name="MainTrackNumber"},
              //  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="加工站点",FieldName="StatingNo",Visible=true,Name="StatingName"},
               new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别+站点",FieldName="GroupStatingName",Visible=true,Name="GroupStatingName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工号",FieldName="CardNo",Visible=true,Name="EmployeeCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工姓名",FieldName="EmployeeName",Visible=true,Name="RealName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="检验结果",FieldName="CheckResult",Visible=true,Name="CheckResult"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="检验信息",FieldName="CheckInfo",Visible=true,Name="CheckInfo"}//,
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="检验时间",FieldName="ReworkDate1",Visible=true,Name="ReworkDate1"}

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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                btnQuery.Cursor = Cursors.WaitCursor;
                Query(1);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
            }
            finally {
                btnQuery.Cursor = Cursors.Default;
            }
        }

        private void btnFlowChartSelect_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtFlowChart.Text?.Trim()))
                {
                    return;
                }
                var gv = susGrid1.DataGrid.MainView as GridView;
                if (null == gv) return;
                var coatHangerIndexModel = gv.GetRow(gv.FocusedRowHandle) as CoatHangerIndexModel;
                var tab = new SusXtraTabPage();
                var products = CommonAction.Instance.Get<SuspeSys.Domain.Products>(coatHangerIndexModel.ProductsId);
                tab.Text = string.Format("工艺路线图[{0}]", products.LineName?.Trim());
                tab.Name = string.Format("工艺路线图[{0}]", products.LineName?.Trim());
                var productsModel = Utils.Reflection.BeanUitls<SuspeSys.Domain.ProductsModel, SuspeSys.Domain.Products>.Mapper(products);
                productsModel.ProcessFlowChartId = products.ProcessFlowChart?.Id;
                XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, new ProcessFlowChartIndex(ucMain, productsModel));
                //SusTransitionManager.EndTransition();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
