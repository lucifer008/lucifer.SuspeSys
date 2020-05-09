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
using SuspeSys.Client.Modules.Ext;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SuspeSys.Client.Action.Query;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Client.Action.CustPurchaseOrder;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SuspeSys.Client.Action.ProcessOrder.Model;
using SuspeSys.Utils.Reflection;
using SuspeSys.Client.Action.CustPurchaseOrder.Model;

namespace SuspeSys.Client.Modules.ProduceData
{
    public partial class PurchaseProcessOrdersInputMain : DevExpress.XtraEditors.XtraUserControl
    {
        public PurchaseProcessOrdersInputMain()
        {
            InitializeComponent();
        }

        private void BindGridHeader()
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            GridControl gc = gridControl2;
            var gridView = gc.MainView as GridView;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            //  gridView.MouseDown += GridView_MouseDown;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户编号",FieldName="CusNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户名称",FieldName="CusName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO编号",FieldName="PurchaseOrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="订单号",FieldName="OrderNo",Visible=true}//,
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="下单日期",FieldName="GeneratorDate",Visible=true}
            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridView.MouseDown += Gv_MouseDown;
            gridView.OptionsBehavior.Editable = false;
            gridControl2.AllowDrop = true;

        }
        public void BindCustomerOrderInfo()
        {
            var cmmonAction = new CommonQueryAction<DaoModel.CustomerPurchaseOrder>();
            gridControl2.DataSource = cmmonAction.GetAllList();

        }
        void BindCustomerOrderInfo(string purOrderId) {

            CustPurchaseOrderAction action = new CustPurchaseOrderAction();
            action.GetCustomerOrderItem(purOrderId);
            var pOrderColorList = action.Model.CustomerPurchaseOrderColorItemModelList;
        }
        void BindColorSizeGridHeader()
        {
            var gv = gridView1;
            gv.Columns.Clear();
            gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName= "No",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true},
            });
            gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "数量合计", FieldName = "Total", Visible = true });
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gv});
            gv.BestFitColumns();//按照列宽度自动适配
            gv.ClearRows();
            gridControl1.MainView = gv;
            gv.GridControl = gridControl1;
            gridControl1.DragDrop += GridControl1_DragDrop;
            gridControl1.DragEnter += GridControl1_DragEnter;
           // gridControl2.MouseMove += GridControl1GridView_MouseMove;
            gridControl1.AllowDrop = true;
        }
        List<CusPurchColorAndSizeModel> CpOrderSizeList = new List<CusPurchColorAndSizeModel>();
        List<DaoModel.PSize> MaxSizeList = new List<DaoModel.PSize>();
      

        void GeneratorProcessOrderItemToGrid(DaoModel.CustomerPurchaseOrder cpOrder) {
            CustPurchaseOrderAction action = new CustPurchaseOrderAction();
            action.GetCustomerOrderItem(cpOrder.Id);
            var pOrderColorList = action.Model.CustomerPurchaseOrderColorItemModelList;
            
            var pSizeList = new List<DaoModel.PSize>();
            var pColorList = new List<DaoModel.PoColor>();
            var pColorSizeDataList = new List<ColorAndSizeModel>();
            var tag = false;
            foreach (var item in pOrderColorList)
            {
                var m = new ColorAndSizeModel();
                m.ColorId = item.PoColor.Id;
                m.SizeColumnCount = item.CustomerPurchaseOrderColorSizeItemList.Count;
                m.ColorValue = item.PoColor.ColorValue;
                m.ColorDescption = item.ColorDescription;
                m.Total = Convert.ToInt32(item.Total);

                pColorList.Add(item.PoColor);
                var index = 1;
                foreach (var size in item.CustomerPurchaseOrderColorSizeItemList)
                {
                    if (!tag)
                    {
                        pSizeList.Add(size.PSize);
                    }
                    ReflectionUtils<ColorAndSizeModel>.SetPropertyValue(m, string.Format("SizeValue{0}", index), size.Total);
                    index++;
                }
                if (pSizeList.Count > 0)
                {
                    tag = true;
                }
                pColorSizeDataList.Add(m);
            }
            var cpOrderSizeList = new List<CusPurchColorAndSizeModel>();
            foreach (var d in pColorSizeDataList) {
                var ta=BeanUitls<CusPurchColorAndSizeModel, ColorAndSizeModel>.Mapper(d);
                ta.Id = cpOrder.Id;
                ta.CusName = cpOrder.CusName;
                ta.CusNo = cpOrder.CusNo;
                ta.PurchaseOrderNo = cpOrder.PurchaseOrderNo;
                ta.OrderNo = cpOrder.OrderNo;
                cpOrderSizeList.Add(ta);
            }
            if (CpOrderSizeList.Count == 0)
            {
                CpOrderSizeList = cpOrderSizeList;
            }
            else {
                if (CpOrderSizeList.Exists(f=>f.Id.Equals(cpOrder.Id))) {
                    XtraMessageBox.Show("客户已经添加到明细，不能重复添加!","温馨提示");
                    return;
                }
                
                cpOrderSizeList.ForEach(a => CpOrderSizeList.Add(a));

            }

            //记录原始客户订单
            var arrays = new CusPurchColorAndSizeModel[CpOrderSizeList.Count];
            CpOrderSizeList.CopyTo(arrays);
            _cusPurchColorSizeSourceList = arrays.ToList<CusPurchColorAndSizeModel>();

            //分组合计
            var lt = GroupBySumCustOrderColorSizeItem(CpOrderSizeList);
            if (pSizeList.Count > MaxSizeList.Count) {
                MaxSizeList = pSizeList;
            }
            BindProcessOrderColorSizeList(pColorList, MaxSizeList, lt);
        }
        List<CusPurchColorAndSizeModel> _cusPurchColorSizeSourceList = new List<CusPurchColorAndSizeModel>();
        public List<CusPurchColorAndSizeModel> CusPurchColorSizeSourceList { get { return _cusPurchColorSizeSourceList; } }
      //  public GridControl GCCustomerPurch

       List<CusPurchColorAndSizeModel> GroupBySumCustOrderColorSizeItem(List<CusPurchColorAndSizeModel> cpOrderSizeList) {
            var query = from l in cpOrderSizeList
                        group l by new { l.CusNo, l.CusName, l.OrderNo, l.PurchaseOrderNo,l.ColorId,l.ColorValue } into g
                        select new CusPurchColorAndSizeModel() {
                            CusNo=g.Key.CusNo,
                            CusName=g.Key.CusName,
                            OrderNo=g.Key.OrderNo,
                            PurchaseOrderNo=g.Key.PurchaseOrderNo,
                            ColorId=g.Key.ColorId,
                            ColorValue=g.Key.ColorValue,
                            SizeValue1=g.Sum(f=>Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue1) ? "0" : f.SizeValue1))).ToString(),
                            SizeValue2 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue2) ? "0" : f.SizeValue2))).ToString(),
                            SizeValue3 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue3) ? "0" : f.SizeValue3))).ToString(),
                            SizeValue4= g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue4) ? "0" : f.SizeValue4))).ToString(),
                            SizeValue5 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue5) ? "0" : f.SizeValue5))).ToString(),
                            SizeValue6 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue6) ? "0" : f.SizeValue6))).ToString(),
                            SizeValue7 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue7) ? "0" : f.SizeValue7))).ToString(),
                            SizeValue8 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue8) ? "0" : f.SizeValue8))).ToString(),
                            SizeValue9 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue9) ? "0" : f.SizeValue9))).ToString(),
                            SizeValue10 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue10) ? "0" : f.SizeValue10))).ToString(),
                            SizeValue11 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue11) ? "0" : f.SizeValue11))).ToString(),
                            SizeValue12= g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue12) ? "0" : f.SizeValue12))).ToString(),
                            SizeValue13 = g.Sum(f => Convert.ToInt32((string.IsNullOrEmpty(f.SizeValue13) ? "0" : f.SizeValue13))).ToString(),
                        };
            var resList=query.ToList<CusPurchColorAndSizeModel>();
            return resList;
        }
        private void BindProcessOrderColorSizeList(List<DaoModel.PoColor> selectedColorList, List<DaoModel.PSize> selectedSizeList, List<CusPurchColorAndSizeModel> cpOrderSizeList)
        {
            var gv = gridView1;// gridControl1.MainView as GridView;
            gv.Columns.Clear();
            gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
               new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户编号",FieldName="CusNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户名称",FieldName="CusName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO编号",FieldName="PurchaseOrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="订单号",FieldName="OrderNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true},
            });
            int j = 1;
            foreach (var size in selectedSizeList)
            {
                gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = size.Size, Tag = size, FieldName = "SizeValue" + (j++), Visible = true });
            }
            gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "数量合计", FieldName = "Total", Visible = true });
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gv});
            gridControl1.MainView = gv;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gv.BestFitColumns();//按照列宽度自动适配
            gv.GridControl = gridControl1;
            gv.OptionsView.ShowFooter = true;
            gv.OptionsView.ShowGroupPanel = false;

            //gv.ClearRows();

            gridControl1.DataSource = cpOrderSizeList;
            gridControl1.MainView.RefreshData();
            gv.IndicatorWidth = 40;
            //gv.OptionsView.ShowIndicator = true;
            //  gv.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
            //保存选择颜色尺码明细
            //this.ProcessOrderColorSizeList = processOrderColorSizeList;
            //this.CustomerOrderColorList = selectedColorList;
            //this.CustomerOrderSizeList = selectedSizeList;
        }

        #region 拖拽相关
        GridHitInfo downHitInfo = null;
        private void Gv_MouseDown(object sender, MouseEventArgs e)
        {
            var gv = (sender as GridView);
            downHitInfo = gv.CalcHitInfo(new Point(e.X, e.Y));
            var d = gv.GetRow(downHitInfo.RowHandle);
            if (null == d) return;
            gridControl1.DoDragDrop(d, DragDropEffects.Copy);//开始拖放操作
        }
        private void GridControl1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void GridControl1_DragDrop(object sender, DragEventArgs e)
        {
            var dd = e.Data.GetData(typeof(DaoModel.CustomerPurchaseOrder)) as DaoModel.CustomerPurchaseOrder;
            GeneratorProcessOrderItemToGrid(dd);
        }

        //鼠标进入 GridView时发生
        private void GridControl1GridView_MouseMove(object sender, MouseEventArgs e)
        {
            downHitInfo = (sender as GridView).CalcHitInfo(new Point(e.X, e.Y));
            //鼠标左键按下去时在GridView中的坐标

            // throw new NotImplementedException();
        }
        #endregion

        private void PurchaseProcessOrdersInputMain_Load(object sender, EventArgs e)
        {
            BindGridHeader();
            BindColorSizeGridHeader();
            BindCustomerOrderInfo();
        }

        private void searchControl2_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            searchControl2.Cursor = Cursors.WaitCursor;
            BindCustomerOrderInfo();
            searchControl2.Cursor = Cursors.Default;
        }
    }
}
