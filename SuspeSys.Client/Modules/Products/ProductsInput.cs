using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuspeSys.Client.Modules.RealtimeInfo;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Action.Products;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Domain.Common;
using System.Linq;

using log4net;

namespace SuspeSys.Client.Modules.Products
{
    public partial class ProductsInput : DevExpress.XtraEditors.XtraForm
    {
        protected ILog log = LogManager.GetLogger(typeof(ProductsInput));
        ProductsAction productAction = new ProductsAction();
        public ProductsInput()
        {
            InitializeComponent();
        }
        public ProductsInput(ProductsingInfoIndex pInfoIndex) : this()
        {
            RegisterEvent();
            BindProductsGridHeader();
            BindFlowChartDetailGridHeader();
            BindProcessOrderGridHeader();
            BindFlowSelectionGridHeader();
            BindFlowChartGridHeader();
            BindHangerPieceSiteGridHeader();
        }
        void RegisterEvent()
        {
            gdProcessOrder.MainView.MouseDown += GridViewProcessOrder_MouseDown;
            gdFlowSelection.MainView.MouseDown += GridViewFlowSelection_MouseDown;
            gdFlowChart.MainView.MouseDown += GridViewFlowChart_MouseDown;
            gdHangerPiece.MainView.MouseDown += GridViewHangerPiece_MouseDown;
        }

        private void GridViewHangerPiece_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                popupccHangerPiece.OwnerEdit.ClosePopup();
                var gv = gdHangerPiece.MainView as GridView;
                //var selRows = gv.GetSelectedRows();
                //if (0 == selRows.Length) return;

                var downHitInfo = (sender as GridView).CalcHitInfo(new Point(e.X, e.Y));
                var selecRow = gv.GetRow(downHitInfo.RowHandle) as DaoModel.StatingModel;
                if (null == selecRow) return;

                comboHangerPieceSite.Text = string.Format("{0}-{1}", selecRow.GroupNO?.Trim(), selecRow.StatingNo?.Trim()).Trim();
                comboHangerPieceSite.Tag = selecRow;
            }
        }

        private void GridViewFlowChart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                popupccFlowChart.OwnerEdit.ClosePopup();
                var gv = gdFlowChart.MainView as GridView;
                //var selRows = gv.GetSelectedRows();
                //if (0 == selRows.Length) return;

                var downHitInfo = (sender as GridView).CalcHitInfo(new Point(e.X, e.Y));
                var selecRow = gv.GetRow(downHitInfo.RowHandle) as DaoModel.ProcessFlowChart;
                if (null == selecRow) return;
                productAction.SearchProcessFlowChartFlowModelList(selecRow.Id);
                gdFlowChartDetail.DataSource = productAction.Model.ProcessFlowChartFlowModelList;
                comboFlowChart.Text = selecRow.LinkName;
                comboFlowChart.Tag = selecRow;
            }

            //throw new NotImplementedException();
        }

        private void GridViewFlowSelection_MouseDown(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            popupccFlowSelection.OwnerEdit.ClosePopup();
            var gv = gdFlowSelection.MainView as GridView;
            //var selRows = gv.GetSelectedRows();
            //if (0 == selRows.Length) return;

            var downHitInfo = (sender as GridView).CalcHitInfo(new Point(e.X, e.Y));
            var selecRow = gv.GetRow(downHitInfo.RowHandle) as DaoModel.ProcessFlowSection;
            if (null == selecRow) return;
            comboFlowSelection.Text = selecRow.ProSectionName;
            comboFlowSelection.Tag = selecRow;
        }

        private void GridViewProcessOrder_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                popupccProcessOrder.OwnerEdit.ClosePopup();
                var gv = gdProcessOrder.MainView as GridView;
                //var selRows = gv.GetSelectedRows();
                //if (0 == selRows.Length) return;

                var downHitInfo = (sender as GridView).CalcHitInfo(new Point(e.X, e.Y));
                var selecRow = gv.GetRow(downHitInfo.RowHandle) as DaoModel.ProcessOrder;
                if (null == selecRow) return;
                productAction.SearchFlowChart(selecRow.Id);
                gdFlowChart.DataSource = productAction.Model.ProcessFlowChartList;
                comboProcessOrderNo.Text = string.Format("{0}", selecRow.POrderNo?.Trim()).Trim();
                comboProcessOrderNo.Tag = selecRow;
                productAction.SearchProcessOrderItemList(selecRow.Id);
                gdProducts.DataSource = productAction.Model.ProcessOrderExtModelList;

            }
        }
        #region 绑定GridHeader
        private void BindProductsGridHeader()
        {
            var gridView = gdProducts.MainView as GridView;

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="POrderNo",Visible=true,Name="ProcessOrderNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO号",FieldName="PurchaseOrderNo",Visible=true,Name="PO"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleCode",Visible=true,Name="StyleNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="Color",Visible=true,Name="ColorValue"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="SizeDesption",Visible=true,Name="SizeDesption"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="订单数量",FieldName="Total",Visible=true,Name="OrdersTotal"},
              new DevExpress.XtraGrid.Columns.GridColumn() { Caption="已分配数量",FieldName="AllocationedTotal",Visible=true,Name="AllocationedTotal"},
              new DevExpress.XtraGrid.Columns.GridColumn() { Caption="未分配数量",FieldName="NonAllocationTotal",Visible=true,Name="NonAllocationTotal"},
              new DevExpress.XtraGrid.Columns.GridColumn() { Caption="任务数量",FieldName="TaskTotal",Visible=true,Name="TaskTotal"},
              new DevExpress.XtraGrid.Columns.GridColumn() { Caption="单位",FieldName="Unit",Visible=true,Name="Unit"}
            });
            gdProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gdProducts.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gdProducts;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            //列不可编辑
            gridView.Columns["POrderNo"].OptionsColumn.AllowEdit = false;
            gridView.Columns["PurchaseOrderNo"].OptionsColumn.AllowEdit = false;
            gridView.Columns["StyleCode"].OptionsColumn.AllowEdit = false;
            gridView.Columns["Color"].OptionsColumn.AllowEdit = false;
            gridView.Columns["Total"].OptionsColumn.AllowEdit = false;
            gridView.Columns["AllocationedTotal"].OptionsColumn.AllowEdit = false;
            gridView.Columns["NonAllocationTotal"].OptionsColumn.AllowEdit = false;
        }
        private void BindFlowChartDetailGridHeader()
        {
            var gridView = gdFlowChartDetail.MainView as GridView;

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="加工顺序",FieldName="CraftFlowNo",Visible=true,Name="CraftFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowNo",Visible=true,Name="FlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="FlowCode",Visible=true,Name="FlowCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="FlowName",Visible=true,Name="FlowName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点数量",FieldName="StatingTotal",Visible=true,Name="StatingTotal"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点列表",FieldName="Statings",Visible=true,Name="Statings"}
            });
            gdFlowChartDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gdFlowChartDetail.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gdFlowChartDetail;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsBehavior.Editable = false;
        }
        private void BindProcessOrderGridHeader()
        {
            var gridView = gdProcessOrder.MainView as GridView;

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="制单号",FieldName="POrderNo",Visible=true,Name="ProcessOrderNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款号",FieldName="StyleCode",Visible=true,Name="StyleNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="款式描述",FieldName="StyleName",Visible=true,Name="StyleName"} });
            gdProcessOrder.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gdProcessOrder.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gdProcessOrder;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsBehavior.Editable = false;
        }
        private void BindFlowSelectionGridHeader()
        {
            var gridView = gdFlowSelection.MainView as GridView;

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工段代码",FieldName="ProSectionCode",Visible=true,Name="ProSectionCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工段名称",FieldName="ProSectionName",Visible=true,Name="ProSectionName" }
            });
            gdFlowSelection.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gdFlowSelection.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gdFlowSelection;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsBehavior.Editable = false;
        }
        private void BindFlowChartGridHeader()
        {
            var gridView = gdFlowChart.MainView as GridView;

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="路线图名称",FieldName="LinkName",Visible=true,Name="FlowChartName"}
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="路线图版本",FieldName="CusName",Visible=true }
            });
            gdFlowChart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gdFlowChart.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gdFlowChart;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsBehavior.Editable = false;
        }
        private void BindHangerPieceSiteGridHeader()
        {
            var gridView = gdHangerPiece.MainView as GridView;

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNO",Visible=true,Name="GroupNO"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点",FieldName="StatingNo",Visible=true,Name="StatingName" }
            });
            gdFlowChart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gdHangerPiece.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gdHangerPiece;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsBehavior.Editable = false;
            var currentGroupNo = string.Empty;
            var currentGroup = CurrentUser.Instance?.CurrentSiteGroup;
            if (null != currentGroup)
            {
                currentGroupNo = currentGroup.GroupNo?.Trim();
            }
            gdHangerPiece.DataSource = productAction.GetHangerPieceStatingList(currentGroupNo);
        }

        #endregion

        #region 绑定数据
        private void BindData()
        {
            productAction.SearchProcessOrderList();
            gdProcessOrder.DataSource = productAction.Model.ProderOrderModelList;
            productAction.SearchProcessFlowSection();
            gdFlowSelection.DataSource = productAction.Model.ProcessFlowSectionList;
        }
        #endregion
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string statingNo = (comboHangerPieceSite.Tag as DaoModel.StatingModel)?.StatingNo?.Trim();
                if (string.IsNullOrEmpty(statingNo))
                {
                    XtraMessageBox.Show("请选择挂片站", "温馨提示");
                    return;
                }

                btnOk.Cursor = Cursors.WaitCursor;
                var productsList = GetProductsModelList();
                var numIsEmpty = false;
                foreach (var item in productsList)
                {
                    if (null != item.TaskNum && item.TaskNum.Value != 0)
                    {
                        numIsEmpty = true;
                        break;
                    }
                }
                if (!numIsEmpty)
                {
                    XtraMessageBox.Show(string.Format("至少设置一个产品的任务数量!"), "温馨提示");
                    return;
                }
                
                //productsList = productsList.ToList<DaoModel.Products>();
                productAction.Model.ProductsList = productsList.ToList<DaoModel.Products>().Where(f => null != f.TaskNum && f.TaskNum.Value > 0).ToList<DaoModel.Products>();
                var valid = CheckData(productAction.Model.ProductsList);
                if (!valid)
                {
                    XtraMessageBox.Show(string.Format("该产品已添加至在制品信息中，请重新查验！!"), "温馨提示");
                    return;
                }
                var currentTrackNumber = CurrentUser.Instance.CurrentSiteGroup?.MainTrackNumber;
                var currentNumber = 0;
                productAction.AddProducts(ref currentNumber, currentTrackNumber.ToString());
                if (currentNumber > 255)
                {
                    XtraMessageBox.Show(string.Format("主轨:{0} 排产号已经用完! 请重新初始化!", currentTrackNumber), "温馨提示");
                    return;
                }
                XtraMessageBox.Show(string.Format("添加成功!"), "温馨提示");
                this.Close();

            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                btnOk.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// 同制单，同款号，同Po，同颜色，同尺码
        /// </summary>
        /// <returns></returns>
        bool CheckData(IList<DaoModel.Products> productsList)
        {
            var sql = string.Format("select * from Products where Status!=4");
            var pList = CommonAction.GetList<DaoModel.Products>(sql);
            foreach (var p in productsList)
            {
                if (pList.Where(k => 
                null!= k.Po && k.Po.Equals(p.Po)
                && null != k.StyleNo && k.StyleNo.Equals(p.StyleNo)
                && k.ProcessOrderNo.Equals(p.ProcessOrderNo)
                && k.PColor.Equals(p.PColor) && k.PSize.Equals(p.PSize)
                ).Count() > 0)
                {
                    return false;
                }
            }
            return true;
        }
        IList<DaoModel.Products> GetProductsModelList()
        {
            var commactionAction = new CommonAction();
            var productsList = new List<DaoModel.Products>();
            var processOrderItemList = gdProducts.DataSource as List<DaoModel.ProcessOrderExtModel>;
            var num = productAction.GetCurrentMaxProductionNumber();
            foreach (var po in processOrderItemList)
            {
                var products = new DaoModel.Products();
                products.GroupNo = (comboHangerPieceSite.Tag as DaoModel.StatingModel)?.GroupNO?.Trim();
                products.ImplementDate = dDateImplementDate.DateTime.FormatDate();
                products.FlowSection = comboFlowSelection.Text?.Trim();
                products.HangingPieceSiteNo = (comboHangerPieceSite.Tag as DaoModel.StatingModel)?.StatingNo?.Trim();
                products.ProcessOrderNo = po.POrderNo?.Trim();
                products.ProcessOrder = comboProcessOrderNo.Tag as DaoModel.ProcessOrder; //commactionAction.Get<DaoModel.ProcessOrder>(po.Id);
                products.StyleNo = po.StyleCode;
                products.OrderNo = po.PurchaseOrderNo?.Trim();
                products.PColor = po.Color;
                products.PSize = po.SizeDesption;
                products.Po = po.PurchaseOrderNo;
                products.TaskNum = po.TaskTotal;
                products.Unit = po.Unit.ToString();
                products.ProcessFlowChart = comboFlowChart.Tag as DaoModel.ProcessFlowChart;
                products.LineName = comboFlowChart.Text?.Trim();
                //products.ProductionNumber = (num++).ToString();
                products.Status = ProductsStatusType.NonAllocation.Value;
                productsList.Add(products);
            }
            return productsList;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProductsInput_Load(object sender, EventArgs e)
        {
            BindData();
        }
    }
}