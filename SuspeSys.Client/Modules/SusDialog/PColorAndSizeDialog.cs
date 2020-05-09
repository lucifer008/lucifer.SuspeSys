using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuspeSys.Client.Action.Common;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Domain;
using DevExpress.XtraGrid;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Client.Action.ProcessOrder.Model;
using SuspeSys.Client.Modules.Customer;

using DaoModel = SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Client.Modules.Ext;
using System.Linq;

namespace SuspeSys.Client.Modules.ProduceData
{
    public partial class PColorAndSizeDialog : DevExpress.XtraEditors.XtraForm
    {
        public PColorAndSizeDialog()
        {
            InitializeComponent();

        }
        //public PColorAndSizeDialog(CustomerOrderInput cOrderInput):this()
        //{


        //}
        CustomerOrderInput customerOrderInput;
        public PColorAndSizeDialog(CustomerOrderInput _customerOrderInput) : this()
        {
            customerOrderInput = _customerOrderInput;
            // selectedColorList = _customerOrderInput.ProcessOrderColorList ?? new List<PoColor>();
            //selectedSizeList = _customerOrderInput.ProcessOrderSizeList ?? new List<PSize>();

            selectedColorList = _customerOrderInput.CustomerOrderColorList ?? new List<PoColor>();
            selectedSizeList = _customerOrderInput.CustomerOrderSizeList ?? new List<PSize>();
            customerOrderColorSizeList = _customerOrderInput.CustomerOrderColorSizeList ?? new List<ColorAndSizeModel>();
        }
        ProcessOrdersInputNew ProcessOrdersInputNew;
        public PColorAndSizeDialog(ProcessOrdersInputNew _processOrdersInputNew) : this()
        {
            ProcessOrdersInputNew = _processOrdersInputNew;
            // selectedColorList = _customerOrderInput.ProcessOrderColorList ?? new List<PoColor>();
            //selectedSizeList = _customerOrderInput.ProcessOrderSizeList ?? new List<PSize>();

            selectedColorList = _processOrdersInputNew.CustomerOrderColorList ?? new List<PoColor>();
            selectedSizeList = _processOrdersInputNew.CustomerOrderSizeList ?? new List<PSize>();
            customerOrderColorSizeList = _processOrdersInputNew.CustomerOrderColorSizeList ?? new List<ColorAndSizeModel>();
        }
        private void ColorAndSizeDialog_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            radioGroup1.Properties.Items.Clear();
            radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
                new DevExpress.XtraEditors.Controls.RadioGroupItem(ProcessOrderItemType.Color.Value,ProcessOrderItemType.Color.Desption),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(ProcessOrderItemType.Size.Value,ProcessOrderItemType.Size.Desption)
            });
            radioGroup1.SelectedIndex = 0;
        }
        //private void BindSelectedColorSizesList()
        //{
        //    var gv = new GridView();
        //    gv.Columns.Clear();
        //    gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
        //        new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
        //       // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName= "No",Visible=true},
        //        new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true},
        //        new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true},
        //    });
        //    foreach (var size in selectedSizeList)
        //    {
        //        gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = size.Size, FieldName = "Size" + size.Id, Visible = true });
        //    }
        //    gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "数量合计", FieldName = "Total", Visible = true });
        //    this.GridControlColorSizes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
        //    gv});
        //    this.GridControlColorSizes.MainView = gv;
        //    //gridControl1.DataSource = commonAction.GetAllColorList();
        //    gv.BestFitColumns();//按照列宽度自动适配
        //    gv.GridControl = GridControlColorSizes;
        //    gv.OptionsView.ShowFooter = true;
        //    gv.OptionsView.ShowGroupPanel = false;
        //    gv.OptionsBehavior.Editable = false;
        //    var processOrderColorSizeList = new List<ColorAndSizeModel>();
        //    for (var i = 0; i < selectedColorList.Count; i++)
        //    {
        //        var index = 1;
        //        var cs = new ColorAndSizeModel()
        //        {
        //            No = index.ToString(),
        //            ColorId = selectedColorList[i].Id,
        //            ColorValue = selectedColorList[i].ColorValue,
        //            ColorDescption = selectedColorList[i].ColorDescption
        //        };
        //        index++;
        //        processOrderColorSizeList = GridControlColorSizes.MainView.DataSource as List<ColorAndSizeModel>;
        //        if (null == processOrderColorSizeList)
        //        {
        //            processOrderColorSizeList = new List<ColorAndSizeModel>();
        //        }
        //        processOrderColorSizeList.Add(cs);
        //    }
        //    GridControlColorSizes.DataSource = processOrderColorSizeList;
        //    GridControlColorSizes.MainView.RefreshData();

        //    //保存选择颜色尺码明细
        //    customerOrderInput.CustomerOrderColorSizeList = processOrderColorSizeList;
        //    customerOrderInput.CustomerOrderColorList = null == selectedColorList ? new List<PoColor>() : selectedColorList;
        //    customerOrderInput.CustomerOrderSizeList = null == selectedSizeList ? new List<PSize>() : selectedSizeList;

        //}

        CommonAction commonAction = new CommonAction();
        /// <summary>
        /// 制单颜色尺码GridControl
        /// </summary>
        public GridControl GridControlColorSizes { get; internal set; }

        private void BindColorGridData(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            gc.MainView.PopulateColumns();
            var gridView = new GridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Identification",FieldName= "Identification",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName= "SNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true}
            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.IndicatorWidth = 40;
            gridView.OptionsView.ShowIndicator = true;
            gridView.OptionsBehavior.Editable = false;
            gridView.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
            gridView.MouseDown += GridView_MouseDown;

        }
        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = (sender as GridView).CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                var gName = ((sender as GridView)?.GridControl)?.Name;
                switch (gName)
                {
                    case "gridControl1":
                        AddItem();
                        break;
                    case "gridControl2":
                        RemoveItem();
                        break;
                }

            }
        }
        private void BindSizeGridData(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new GridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Identification",FieldName= "Identification",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName= "PsNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="Size",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码描述",FieldName="SizeDesption",Visible=true}
            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.IndicatorWidth = 40;
            gridView.OptionsView.ShowIndicator = true;
            gridView.OptionsBehavior.Editable = false;
            gridView.CustomDrawRowIndicator += gridView_CustomDrawRowIndicator;
            gridView.MouseDown += GridView_MouseDown;

        }
        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var seKey = radioGroup1.EditValue?.ToString();
            if (ProcessOrderItemType.Color.Value == seKey)
            {
                BindColorGridData(gridControl1);
                BindColorGridData(gridControl2);
                (gridControl2.MainView as GridView)?.ClearRows();
                gridControl1.DataSource = commonAction.GetAllColorList();
                if (selectedColorList?.Count > 0)
                {
                    PoColor[] poColors = new PoColor[selectedColorList.Count];
                    selectedColorList.CopyTo(poColors);
                    gridControl2.DataSource = new List<PoColor>(poColors);
                    gridControl2.Refresh();
                }

                Cursor = Cursors.Default;
                return;
            }
            BindSizeGridData(gridControl1);
            BindSizeGridData(gridControl2);
            (gridControl2.MainView as GridView)?.ClearRows();
            gridControl1.DataSource = commonAction.GetSizeList();
            if (selectedSizeList?.Count > 0)
            {
                PSize[] pSizes = new PSize[selectedSizeList.Count];
                selectedSizeList.CopyTo(pSizes);
                gridControl2.DataSource = new List<PSize>(pSizes); ;
                gridControl2.Refresh();
            }
            Cursor = Cursors.Default;
        }
        IList<PoColor> cList = new CommonAction().GetAllColorList().ToArray();
        /// <summary>
        /// 绘制选择的颜色及尺码
        /// </summary>
        private void BindColorSizeList()
        {
            var colorResComboBox = new RepositoryItemComboBox();
            colorResComboBox.NullValuePrompt = "请选择颜色";
            colorResComboBox.SelectedValueChanged += ColorResComboBox_SelectedValueChanged;
            colorResComboBox.ParseEditValue += new ConvertEditValueEventHandler(colorResComboBox_ParseEditValue);
            foreach (var cc in cList)
            {
                colorResComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc.ColorValue,
                    Value = cc.ColorValue?.Trim(),
                    Tag = cc
                });
            }

            var list = StatColorSizeItem(selectedColorList, selectedSizeList);

            var gv = GridControlColorSizes.MainView as GridView;
            gv.Columns.Clear();
            gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
               new DevExpress.XtraGrid.Columns.GridColumn() { Caption="PO",FieldName= "PO",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true, ColumnEdit=colorResComboBox},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true},
            });
            int j = 1;
            foreach (var size in selectedSizeList)
            {
                gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = size.Size, Tag = size, FieldName = "SizeValue" + (j++), Visible = true });
            }
            gv.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "数量合计", FieldName = "Total", Visible = true });
            this.GridControlColorSizes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gv});
            this.GridControlColorSizes.MainView = gv;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gv.BestFitColumns();//按照列宽度自动适配
            gv.GridControl = GridControlColorSizes;
            gv.OptionsView.ShowFooter = true;
            gv.OptionsView.ShowGroupPanel = false;

            gv.ClearRows();
            //var processOrderColorSizeList = new List<ColorAndSizeModel>();
            //for (var i = 0; i < selectedColorList.Count; i++)
            //{
            //    var index = 1;
            //    var cs = new ColorAndSizeModel()
            //    {
            //        No = index.ToString(),
            //        ColorId = selectedColorList[i].Id,
            //        ColorValue = selectedColorList[i].ColorValue,
            //        ColorDescption = selectedColorList[i].ColorDescption,
            //        SizeColumnCount = selectedSizeList.Count
            //    };
            //    index++;
            //    //processOrderColorSizeList = GridControlColorSizes.MainView.DataSource as List<ColorAndSizeModel>;
            //    //if (null == processOrderColorSizeList)
            //    //{
            //    //    processOrderColorSizeList = new List<ColorAndSizeModel>();
            //    //}
            //    processOrderColorSizeList.Add(cs);
            //}



            GridControlColorSizes.DataSource = list;
            GridControlColorSizes.MainView.RefreshData();

            //保存选择颜色尺码明细
            if (customerOrderInput != null)
            {
                customerOrderInput.CustomerOrderColorSizeList = list;
                customerOrderInput.CustomerOrderColorList = selectedColorList;
                customerOrderInput.CustomerOrderSizeList = selectedSizeList;
                return;
            }
            //保存选择颜色尺码明细
            if (ProcessOrdersInputNew != null)
            {
                ProcessOrdersInputNew.CustomerOrderColorSizeList = list;
                ProcessOrdersInputNew.CustomerOrderColorList = selectedColorList;
                ProcessOrdersInputNew.CustomerOrderSizeList = selectedSizeList;
            }


        }

        private void colorResComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void ColorResComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private List<ColorAndSizeModel> StatColorSizeItem(List<PoColor> selectedColorList, List<PSize> selectedSizeList)
        {
            if (null != customerOrderInput)
            {
                if (customerOrderInput.CustomerOrderColorSizeList == null)
                {
                    customerOrderInput.CustomerOrderColorSizeList = new List<ColorAndSizeModel>();
                }
                var arrs = new ColorAndSizeModel[customerOrderInput.CustomerOrderColorSizeList.Count];
                customerOrderInput.CustomerOrderColorSizeList.CopyTo(arrs);
                var tempList = new List<ColorAndSizeModel>(arrs);

                for (var index = 0; index < tempList.Count; index++)
                {
                    if (!selectedColorList.Exists(q => q.Id.Equals(tempList[index].ColorId.ToString())))
                    {
                        tempList.Remove(tempList[index]);
                        continue;
                    }

                }
                //  var processOrderColorSizeList = new List<ColorAndSizeModel>();
                for (var i = 0; i < selectedColorList.Count; i++)
                {
                    if (!tempList.Exists(f => f.ColorId.ToString().Equals(selectedColorList[i].Id)))
                    {
                        var cs = new ColorAndSizeModel()
                        {

                            ColorId = selectedColorList[i].Id,
                            ColorValue = selectedColorList[i].ColorValue,
                            ColorDescption = selectedColorList[i].ColorDescption,
                            SizeColumnCount = selectedSizeList.Count
                        };
                        tempList.Add(cs);
                    }
                }
                tempList.ForEach(f => f.SizeColumnCount = selectedSizeList.Count);
                return tempList;
            }
            if (null != ProcessOrdersInputNew)
            {
                if (ProcessOrdersInputNew.CustomerOrderColorSizeList == null)
                {
                    ProcessOrdersInputNew.CustomerOrderColorSizeList = new List<ColorAndSizeModel>();
                }
                var arrs = new ColorAndSizeModel[ProcessOrdersInputNew.CustomerOrderColorSizeList.Count];
                ProcessOrdersInputNew.CustomerOrderColorSizeList.CopyTo(arrs);
                var tempList = new List<ColorAndSizeModel>(arrs);

                for (var index = 0; index < tempList.Count; index++)
                {
                    if (!selectedColorList.Exists(q => q.Id.Equals(tempList[index].ColorId.ToString())))
                    {
                        tempList.Remove(tempList[index]);
                        continue;
                    }

                }
                //  var processOrderColorSizeList = new List<ColorAndSizeModel>();
                for (var i = 0; i < selectedColorList.Count; i++)
                {
                    if (!tempList.Exists(f => f.ColorId.ToString().Equals(selectedColorList[i].Id)))
                    {
                        var cs = new ColorAndSizeModel()
                        {

                            ColorId = selectedColorList[i].Id,
                            ColorValue = selectedColorList[i].ColorValue,
                            ColorDescption = selectedColorList[i].ColorDescption,
                            SizeColumnCount = selectedSizeList.Count
                        };
                        tempList.Add(cs);
                    }
                }
                tempList.ForEach(f => f.SizeColumnCount = selectedSizeList.Count);
                return tempList;
            }
            return null;
        }


        /// <summary>
        /// 获取已经有的颜色
        /// </summary>
        /// <returns></returns>
        bool CheckColorExist(string colorId)
        {
            foreach (var cs in customerOrderInput.CustomerOrderColorList)
            {
                if (cs.Id.Equals(colorId))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取已经有的尺码
        /// </summary>
        /// <returns></returns>
        DaoModel.PSize GetAlreadySize(string sizeId)
        {
            foreach (var cs in customerOrderInput.CustomerOrderSizeList)
            {
                if (cs.Id.Equals(sizeId))
                {
                    return cs;
                }
            }
            return null;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            BindColorSizeList();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }
        //  int identificationIndex = 1;
        void AddItem()
        {
            if (ProcessOrderItemType.Color.Value == radioGroup1.EditValue?.ToString())
            {
                var gvSource = gridControl1.MainView as GridView;
                var selectedRow = gvSource.GetRow(gvSource.FocusedRowHandle) as PoColor;

                var gvTarget = gridControl2.MainView as GridView;
                if (null != selectedRow)
                {
                    var dt = gvTarget.DataSource as List<PoColor>;
                    if (null == dt)
                    {
                        gridControl2.DataSource = new List<PoColor>() { selectedRow };
                        selectedColorList.Add(selectedRow);
                        return;
                    }
                    if (!CheckColorIsExist(selectedRow))
                    {
                        dt.Add(selectedRow);
                        gvTarget.RefreshData();
                        selectedColorList.Add(selectedRow);
                    }
                }
                return;
            }
            if (ProcessOrderItemType.Size.Value == radioGroup1.EditValue?.ToString())
            {
                var gvSource = gridControl1.MainView as GridView;
                var selectedRow = gvSource.GetRow(gvSource.FocusedRowHandle) as PSize;
                var gvTarget = gridControl2.MainView as GridView;
                if (null != selectedRow)
                {
                    var dt = gvTarget.DataSource as List<PSize>;
                    if (null == dt)
                    {
                        gridControl2.DataSource = new List<PSize>() { selectedRow };
                        selectedSizeList.Add(selectedRow);
                        return;
                    }
                    if (!CheckPoSizeIsExist(selectedRow))
                    {
                        dt.Add(selectedRow);
                        gvTarget.RefreshData();
                        selectedSizeList.Add(selectedRow);
                    }
                }
            }

        }

        /// <summary>
        /// 选择的颜色
        /// </summary>
        List<PoColor> selectedColorList = new List<PoColor>();
        /// <summary>
        /// 选择的尺码
        /// </summary>
        List<PSize> selectedSizeList = new List<PSize>();
        /// <summary>
        /// 颜色尺码明细
        /// </summary>
        List<ColorAndSizeModel> customerOrderColorSizeList = new List<ColorAndSizeModel>();
        void RemoveItem()
        {
            if (ProcessOrderItemType.Color.Value == radioGroup1.EditValue?.ToString())
            {
                var gvTarget = gridControl2.MainView as GridView;
                var selectedRow = gvTarget.GetRow(gvTarget.FocusedRowHandle) as PoColor;
                gvTarget.DeleteRow(gvTarget.FocusedRowHandle);
                gvTarget.RefreshData();
                selectedColorList.Remove(selectedRow);
                return;
            }
            if (ProcessOrderItemType.Size.Value == radioGroup1.EditValue?.ToString())
            {
                var gvTarget = gridControl2.MainView as GridView;
                var selectedRow = gvTarget.GetRow(gvTarget.FocusedRowHandle) as PSize;
                gvTarget.DeleteRow(gvTarget.FocusedRowHandle);
                gvTarget.RefreshData();
                selectedSizeList.Remove(selectedRow);
                return;
            }
        }
        /// <summary>
        /// 检查尺码是否存在
        /// </summary>
        /// <param name="pColor"></param>
        /// <returns></returns>
        bool CheckPoSizeIsExist(PSize pSize)
        {
            foreach (var pc in selectedSizeList)
            {
                if (pSize.Size.Trim().Equals(pc.Size.Trim()))
                {
                    XtraMessageBox.Show(string.Format("尺码【{0}】已存在!", pSize.Size), "温馨提示");
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检查颜色是否存在
        /// </summary>
        /// <param name="pSize"></param>
        /// <returns></returns>
        bool CheckColorIsExist(PoColor pColor)
        {
            foreach (var pc in selectedColorList)
            {
                if (pColor.ColorValue.Trim().Equals(pc.ColorValue.Trim()))
                {
                    XtraMessageBox.Show(string.Format("颜色【{0}】已存在!", pColor.ColorValue), "温馨提示");
                    return true;
                }
            }
            return false;
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveItem();
        }
        //移除所有
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            (gridControl2.MainView as GridView)?.ClearRows();
            if (ProcessOrderItemType.Color.Value == radioGroup1.EditValue?.ToString())
            {
                //var gvTarget = gridControl2.MainView as GridView;
                //if (null == gvTarget) return;
                //bool _mutilSelected = gvTarget.OptionsSelection.MultiSelect;//获取当前是否可以多选
                //if (!_mutilSelected)
                //    gvTarget.OptionsSelection.MultiSelect = true;
                //gvTarget.SelectAll();
                //gvTarget.DeleteSelectedRows();
                //gvTarget.OptionsSelection.MultiSelect = _mutilSelected;
                //gvTarget.RefreshData();
                selectedColorList.RemoveAll(j => !string.IsNullOrEmpty(j.Id));
            }
            if (ProcessOrderItemType.Size.Value == radioGroup1.EditValue?.ToString())
            {
                //var gvTarget = gridControl2.MainView as GridView;
                //if (null == gvTarget) return;
                //bool _mutilSelected = gvTarget.OptionsSelection.MultiSelect;//获取当前是否可以多选
                //if (!_mutilSelected)
                //    gvTarget.OptionsSelection.MultiSelect = true;
                //gvTarget.SelectAll();
                //gvTarget.DeleteSelectedRows();
                //gvTarget.OptionsSelection.MultiSelect = _mutilSelected;
                //gvTarget.RefreshData();

                selectedSizeList.RemoveAll(j => !string.IsNullOrEmpty(j.Id));
            }
        }

        private void btnUpper_Click(object sender, EventArgs e)
        {
            btnUpper.Cursor = Cursors.WaitCursor;
            var gv = (gridControl1.MainView as GridView);
            gv.SelectRow(gv.FocusedRowHandle--);
            btnUpper.Cursor = Cursors.Default;
        }

        private void btnLower_Click(object sender, EventArgs e)
        {
            btnLower.Cursor = Cursors.WaitCursor;
            var gv = (gridControl1.MainView as GridView);
            gv.SelectRow(gv.FocusedRowHandle++);
            btnLower.Cursor = Cursors.Default;
        }
    }
}