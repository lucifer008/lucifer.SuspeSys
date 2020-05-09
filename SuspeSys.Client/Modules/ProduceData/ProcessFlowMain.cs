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
using DevExpress.XtraGrid;
using SuspeSys.Client.Action.Query;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Action.ProcessOrder;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SuspeSys.Utils.Reflection;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.ProcessOrder.Model;
using SuspeSys.Client.Common.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using SuspeSys.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;

namespace SuspeSys.Client.Modules.ProduceData
{
    /// <summary>
    /// 制单工序
    /// </summary>
    public partial class ProcessFlowMain : SusXtraUserControl
    {
        public ProcessFlowMain()
        {
            InitializeComponent();

            BindBasicProcessFlowGridHeader(gridControl3);
            BindStyleFlowGridHeader(gridControl2);
            BindUsedProcessFlowGridHeader(gridControl1);
            BindBasicProcessFlowData();

        }
        private DaoModel.ProcessOrder processOrder;
        // public XtraUserControl1 ucMain { get; internal set; }
        /// <summary>
        /// 绑定制单信息
        /// </summary>
        /// <param name="pOrder"></param>
        public void BindProcessOrder(SuspeSys.Domain.ProcessOrder pOrder)
        {
            if (null != pOrder)
            {
                processOrder = pOrder;
                txtProcessOrderNo.Text = pOrder?.POrderNo?.Trim();
                txtProcessOrderStyleCode.Text = pOrder?.StyleCode?.Trim();
                txtProcessOrderStyleDesption.Text = pOrder?.StyleName?.Trim();
                txtProcessOrderNo.Enabled = false;
                txtProcessOrderStyleCode.Enabled = false;
                txtProcessOrderStyleDesption.Enabled = false;
                var pOrderQueryAction = new ProcessOrderQueryAction();
                var versionNum = pOrderQueryAction.GetCurrentMaxProcessFlowVersionNo(pOrder?.Id);
                txtProcessFlowVersion.Text = string.Format("{0}_{1}-{2}", pOrder?.POrderNo?.Trim(), DateTime.Now.ToString("yyMMdd"), versionNum);
                txtProcessFlowVersion.Enabled = false;
                txtEffectiveDate.DateTime = DateTime.Now;

                (gridControl1.MainView as GridView)?.ClearRows();
                BindProcessOrderStyleFlowData(pOrder?.StyleCode);

                comboxProcessVersion.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                BindProcessFlowVersion(pOrder.Id);

                
            }
        }
        /// <summary>
        /// 已有绑定制单工序版本，并根据需要设置默认工序
        /// </summary>
        /// <param name="pOrderId"></param>
        /// <param name="isEarlist"></param>
        public void BindProcessFlowVerson(string pOrderId, bool isEarlist)
        {
            BindProcessFlowVersion(pOrderId);
            if (isEarlist)
            {
                if (comboxProcessVersion.Properties.Items.Count > 0)
                    comboxProcessVersion.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定已有工序
        /// </summary>
        /// <param name="pOrderId"></param>
        void BindProcessFlowVersion(string pOrderId)
        {
            var pAction = new ProcessOrderQueryAction();
            var list = pAction.GetProcessOrderFlowVersionList(pOrderId);
            comboxProcessVersion.Properties.Items.Clear();
            foreach (var item in list)
            {
                var cb = new SusComboBoxItem() { Text = item.ProVersionNo, Value = item.Id, Tag = item };
                comboxProcessVersion.Properties.Items.Add(cb);
                //if (comboxProcessVersion.Properties.Items.Count == 1) {
                //    comboxProcessVersion.SelectedItem =cb;
                //}
            }
            comboxProcessVersion.SelectedIndex = 0;

        }
        /// <summary>
        /// 制单工序明细
        /// </summary>
        /// <param name="gc"></param>
        private void BindUsedProcessFlowGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new Ext.SusGridView();

            RepositoryItemTextEdit textEdit = new RepositoryItemTextEdit();
            textEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            textEdit.Mask.EditMask = "([0-9]{1,}[.][0-9]*)";

            RepositoryItemTextEdit textEditStanardSecond = new RepositoryItemTextEdit();
            textEditStanardSecond.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            
            //textEditStanardSecond.Mask.EditMask = "[0-9]";
            //textEdit

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName="Id",Visible=false},
                  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="基本工序Id",FieldName="FlowId",Visible=false},
                  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序段",FieldName="ProSectionName",Visible=true,Name="ProSectionName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="排序字段",FieldName="ProcessOrderField",Visible=true,Name="ProcessOrderField"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="DefaultFlowNo",Visible=true,Name="DefaultFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessCode",Visible=true,Name="ProcessCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessName",Visible=true,Name="ProcessName"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(分钟)",FieldName="StanardMinute",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(秒)",FieldName="StanardSecond",Visible=true, ColumnEdit = textEditStanardSecond,Name="StanardSecond"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(SAM)",FieldName="SAM",Visible=true, ColumnEdit = textEdit,Name="SAM"},

                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工价(元)",FieldName="StandardPrice",Visible=true,Name="StandardPrice"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工艺指导说明",FieldName="PrcocessRmark",Visible=true,Name="PrcocessRmark"}
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true}
            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            //  gridView.BestFitColumns();//按照列宽度自动适配
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.GridControl = gc;
            for (var index=0;index<gridView.Columns.Count;index++) {
                gridView.Columns[index].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;//内容据左对齐
            }

            


            //gridView.Columns["SAM"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            //gridView.Columns["SAM"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            //GridColumn gridColumn = gridView.Columns["StanardMinute"];
            //gridColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            //gridColumn.UnboundExpression = "[StanardSecond] / 60";
            gridView.CellValueChanged += GridView1_CellValueChanged;


            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            RegisterGridControlEvent();
            gc.DataSource = bList;
            gc.AllowDrop = true;
        }


        /// <summary>
        /// 行着色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            DataRow dr = (gridControl1.MainView as GridView).GetDataRow(e.RowHandle);
            if (dr != null)
            {

            }
        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Value == null)
            //    return;

            var list = gridControl1.DataSource as List<DaoModel.BasicProcessFlowModel>;

            var gv = (gridControl1.MainView as SusGridView);
            int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);

            if (e.Column.FieldName.Equals("StanardSecond", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value != null)
                {
                    decimal statnardS = Convert.ToDecimal(e.Value);
                    list[dataIndex].SAM = Math.Round(statnardS / 60, 4).ToString();
                }
                else {
                    list[dataIndex].SAM = string.Empty;
                }
                //list[dataIndex].SAM = Math.Round(statnardS / 3600, 2).ToString();
            }

            //if (e.Column.FieldName.Equals("StanardMinute", StringComparison.OrdinalIgnoreCase))
            //{
            //    decimal statnardM = Convert.ToDecimal(e.Value);
            //    list[dataIndex].StanardSecond = Math.Round(statnardM * 60, 2);
            //    list[dataIndex].SAM = Math.Round(statnardM / 60, 2).ToString();
            //}
            if (e.Column.FieldName.Equals("SAM", StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value+""))
                {
                    decimal statnardM = Convert.ToDecimal(e.Value);
                    //list[dataIndex].StanardMinute = Math.Round(statnardH * 60, 4);
                    list[dataIndex].StanardSecond = Convert.ToInt32( Math.Round(statnardM * 60, 0));
                }
                else
                    list[dataIndex].StanardSecond = 0;
            }

            gv.RefreshData();
            
            //list[dataIndex].EmployeeName = text;
            //list[dataIndex].EmployeeCode = item.Value?.ToString();

            //gv.SetFocusedRowCellValue("EmployeeText", item.Text?.ToString()?.Trim());
        }

        /// <summary>
        /// 绑定基本工序
        /// </summary>
        /// <param name="gc"></param>
        private void BindBasicProcessFlowGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="DefaultFlowNo",Visible=true,Name="DefaultFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessCode",Visible=true,Name="ProcessCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessName",Visible=true,Name="ProcessName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(SAM)",FieldName="Sam",Visible=true,Name="Sam"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工价(元)",FieldName="StandardPrice",Visible=true,Name="StandardPrice"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工艺指导说明",FieldName="PrcocessRmark",Visible=true,Name="PrcocessRmark"}
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            //  gridView.BestFitColumns();//按照列宽度自动适配
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsBehavior.Editable = false;
            gc.AllowDrop = true; // 确保能够拖拽
            RegisterGridViewEvent(gridView);
        }

        /// <summary>
        /// 绑定款式工序
        /// </summary>
        /// <param name="gc"></param>
        private void BindStyleFlowGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序段",FieldName="ProSectionName",Visible=true,Name="ProSectionName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="排序字段",FieldName="ProcessName",Visible=true,Name="ProcessOrderField"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="DefaultFlowNo",Visible=true,Name="DefaultFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessCode",Visible=true,Name="ProcessCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessName",Visible=true,Name="ProcessName"},
                  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(SAM)",FieldName="SAM",Visible=true,Name="SAM"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工价(元)",FieldName="StandardPrice",Visible=true,Name="StandardPrice"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工艺指导说明",FieldName="PrcocessRmark",Visible=true,Name="PrcocessRmark"}
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            //  gridView.BestFitColumns();//按照列宽度自动适配
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;

            gridView.OptionsBehavior.Editable = false;
            gc.AllowDrop = true; // 确保能够拖拽
            RegisterGridViewEvent(gridView);
        }

        /// <summary>
        /// 绑定基本工序数据
        /// </summary>
        private void BindBasicProcessFlowData()
        {
            var action = new CommonQueryAction<SuspeSys.Domain.BasicProcessFlow>();
            action.GetList();
            gridControl3.DataSource = action.Model.List;
            //var p=new CommonQueryAction<SuspeSys.Domain.ProcessOrder>();
            //p.GetList();
        }

        /// <summary>
        /// 绑定制单款式工序数据
        /// </summary>
        /// <param name="styleNo"></param>
        private void BindProcessOrderStyleFlowData(string styleNo)
        {
            // var styleProcessFlowList = new List<SuspeSys.Domain.BasicProcessFlowModel>();
            var action = new ProcessOrderQueryAction();
            action.GetProcessOrderStyleFlow(styleNo);
            var list = action.Model.BasicProcessFlowModelList;///.Where(l => l.BasicProcessFlow.StyleNo.Trim().ToLower().Equals(styleNo.Trim().ToLower()));
            gridControl2.DataSource = list;
            //var p=new CommonQueryAction<SuspeSys.Domain.ProcessOrder>();
            //p.GetList();
        }

        private void gridControl1_LocationChanged(object sender, EventArgs e)
        {

        }

        #region 制单工序制作拖动相关
        void RegisterGridViewEvent(GridView gv)
        {
            gv.MouseDown += Gv_MouseDown; ;
            gv.MouseMove += Gv_MouseMove; ;

        }
        IList<SuspeSys.Domain.BasicProcessFlowModel> bList = new List<SuspeSys.Domain.BasicProcessFlowModel>();

        private void Gv_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;        //不是左键则无效
            if (downHitInfo == null || downHitInfo.RowHandle < 0) return;   //判断鼠标的位置是否有效

            IList<SuspeSys.Domain.BasicProcessFlowModel> bListTemp = new List<SuspeSys.Domain.BasicProcessFlowModel>();
            var rows = (sender as GridView).GetSelectedRows();  //获取所选行的index
            var startRow = rows.Length == 0 ? -1 : rows[0];
            var keyName = (sender as GridView).GridControl.Name;
            //bList.Clear();
            foreach (var r in rows)   // 根据所选行的index进行取值，去除所选的行数据，可能是选取的多行
            {
                int dataSourcerows = (sender as GridView).GetDataSourceRowIndex(r); //获取行数据

                if (keyName.Equals("gridControl3"))//基本工序
                {
                    var lst = (sender as GridView).DataSource as List<SuspeSys.Domain.BasicProcessFlow>;
                    //var isExBasic = bList.Where(q => q.ProcessCode.ToLower().Trim().Equals(lst[r].ProcessCode.ToLower().Trim())
                    //).ToList<DaoModel.BasicProcessFlowModel>().Count > 0;
                    //if (isExBasic)
                    //{
                    //    XtraMessageBox.Show(string.Format("工序[{0}]已经添加，不能重添加!", lst[r].ProcessCode.Trim()), "温馨提示");
                    //    return;
                    //}
                    var m = BeanUitls<SuspeSys.Domain.BasicProcessFlowModel, SuspeSys.Domain.BasicProcessFlow>.Mapper(lst[r]);
                    m.FlowSourceGrid = keyName;
                    bListTemp.Add(m);
                }
                else
                {
                    var lst = (sender as GridView).DataSource as List<SuspeSys.Domain.BasicProcessFlowModel>;
                    //  var isExBasic = bList.Where(q => q.ProcessCode.ToLower().Trim().Equals(lst[r].ProcessCode.ToLower().Trim())
                    //).ToList<DaoModel.BasicProcessFlowModel>().Count > 0;
                    //  if (isExBasic)
                    //  {
                    //      XtraMessageBox.Show(string.Format("工序[{0}]已经添加，不能重添加!", lst[r].ProcessCode.Trim()), "温馨提示");
                    //      return;
                    //  }
                    var basicFlow = BeanUitls<SuspeSys.Domain.BasicProcessFlowModel, SuspeSys.Domain.BasicProcessFlow>.Mapper(lst[r]);
                    basicFlow.FlowSourceGrid = keyName;
                    bListTemp.Add(basicFlow);
                }
                // dt.ImportRow(dtSource.Rows[dataSourcerows]); //保存所选取的行数据
            }
            gridControl1.DoDragDrop(bListTemp, DragDropEffects.Move);//开始拖放操作，将拖拽的数据存储起来
        }
        GridHitInfo downHitInfo = null;
        private void Gv_MouseDown(object sender, MouseEventArgs e)
        {
            downHitInfo = (sender as GridView).CalcHitInfo(new Point(e.X, e.Y));   //鼠标左键按下去时在GridView中的坐标
            //throw new NotImplementedException();
        }

        void RegisterGridControlEvent()
        {
            gridControl1.DragOver += GridControl1_DragOver;
            gridControl1.DragDrop += GridControl1_DragDrop;
            gridControl1.DragEnter += GridControl1_DragEnter;
        }
        //DragEnter是你拖动后首次在进入某个控件内发生
        private void GridControl1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //当你松开鼠标时发生
        private void GridControl1_DragDrop(object sender, DragEventArgs e)
        {
            //Point gridviewPoint = this.PointToScreen(this.gridControl1.Location);  //获取鼠标在屏幕上的位置。
            //upHitInfo = ((sender as GridControl).MainView as GridView).CalcHitInfo(new Point(e.X - gridviewPoint.X, e.Y - gridviewPoint.Y));   //鼠标左键弹起来时在GridView中的坐标（屏幕位置减去 gridView 开始位置）
            //if (upHitInfo == null || upHitInfo.RowHandle < 0) return;

            //int endRow = gridView1.GetDataSourceRowIndex(gridView1.GetDataSourceRowIndex(upHitInfo.RowHandle)); //获取拖拽的目标行index

            var dd = e.Data.GetData(typeof(List<SuspeSys.Domain.BasicProcessFlowModel>)) as List<SuspeSys.Domain.BasicProcessFlowModel>;

            foreach (var item in dd)
            {
                var isExBasic = bList.Where(q => q.ProcessCode.ToLower().Trim().Equals(item.ProcessCode.ToLower().Trim())).ToList<DaoModel.BasicProcessFlowModel>().Count > 0;
                if (isExBasic)
                {
                    XtraMessageBox.Show(string.Format("工序[{0}]已经添加，不能重添加!", item.ProcessCode.Trim()), "温馨提示");
                    return;
                }
                if (item.FlowSourceGrid.Equals("gridControl3"))
                {//来源基本工序
                    item.FlowId = item.Id;
                    item.SAM = item.Sam;
                    bList.Add(item);
                    continue;
                }
                item.FlowId = item.FlowId;
                bList.Add(item);
            }
            // var dt = e.Data.GetData(typeof(SuspeSys.Domain.BasicProcessFlow)) as SuspeSys.Domain.BasicProcessFlow;  //获取要移动的数据，从拖拽保存的地方：（gridControl1.DoDragDrop(dt, DragDropEffects.Move); ）

            gridControl1.DataSource = bList; //重新绑定
            gridControl1.MainView.RefreshData();
            CalculationSAMAndStandardPrice();
        }
        //DragOver发生在DragEnter之后，当你移动拖动对象（鼠标）时发生，类似于MouseMove。
        private void GridControl1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        //计算总SAM,总工价
        void CalculationSAMAndStandardPrice()
        {
            txtTotalSAM.Text = "";
            txtTotalStandardPrice.Text = "";
            decimal totalSAM = 0;
            decimal totalStandPrice = 0;
            var dt = gridControl1.DataSource as List<DaoModel.BasicProcessFlowModel>;
            if (null != dt)
            {
                foreach (var item in dt)
                {
                    totalSAM += string.IsNullOrEmpty(item.SAM) ? Convert.ToDecimal(0) : Convert.ToDecimal(item.SAM);
                    totalStandPrice += string.IsNullOrEmpty(item.StandardPrice.ToString()) ? Convert.ToDecimal(0) : Convert.ToDecimal(item.StandardPrice);
                }
            }
            txtTotalSAM.Text = totalSAM > 0 ? totalSAM.ToString() : "";
            txtTotalStandardPrice.Text = totalStandPrice > 0 ? totalStandPrice.ToString() : "";

        }
        #endregion

        public SusToolBar ProcessFlowSusToolBar { get; internal set; }

        void RegisterToolButtonEvent()
        {
            if (null != ProcessFlowSusToolBar)
            {
                ProcessFlowSusToolBar.OnButtonClick += SusToolBar1_OnButtonClick;
            }
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        if (!SaveProcessOrderFlow()) { return; };
                        break;
                    case ButtonName.SaveAndAdd:
                        if (!SaveProcessOrderFlow()) { return; };
                        break;
                    case ButtonName.SaveAndClose:
                        if (SaveProcessOrderFlow())
                        {
                            ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        }
                        break;
                    case ButtonName.Delete:
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Add:
                        AddItem();
                        break;
                    case ButtonName.Cancel:
                        AddItem();
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    default:
                        break;
                }
                //处理后续动作
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        BindProcessFlowVersion(processOrder?.Id);
                        for (var item = 0; item < comboxProcessVersion.Properties.Items.Count; item++)
                        {
                            if ((comboxProcessVersion.Properties.Items[item] as SusComboBoxItem).Value.Equals(action.Model.ProcessFlowVersion.Id))
                            {
                                comboxProcessVersion.SelectedIndex = item;
                                break;
                            }
                        }
                        break;
                    case ButtonName.SaveAndAdd:

                        var processFlowIndex = new ProcessFlowIndex(ucMain) { Dock = DockStyle.Fill };
                        var tab = new XtraTabPage();
                        tab.Name = "processFlowIndex" + CommonUtils.Counter;
                        tab.Text = "新增制单";
                        if (!ucMain.MainTabControl.TabPages.Contains(tab))
                        {
                            tab.Controls.Add(processFlowIndex);
                            ucMain.MainTabControl.TabPages.Add(tab);
                        }
                        ucMain.MainTabControl.SelectedTabPage = tab;
                        break;
                    default:
                        break;
                }
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

        private void AddItem()
        {

            txtProcessFlowVersion.Enabled = true;
            comboxProcessVersion.SelectedItem = null;
            bList.Clear();
            gridControl1.DataSource = null;
            txtProcessFlowVersion.Text = string.Empty;
            //DateTime? t=default(DateTime);
            //txtEffectiveDate.cl.DateTime = t.Value;
            txtTotalSAM.Text = string.Empty;
            txtTotalStandardPrice.Text = string.Empty;
        }

        ProcessOrderFlowAction action = new ProcessOrderFlowAction();

        // 保存制单工序
        bool SaveProcessOrderFlow()
        {

            var model = GetProcessOrderFlow();
            if (null == model.ProcessOrder)
            {
                XtraMessageBox.Show("请选择制单", "温馨提示");
                return false;
            }
            var isFlowDistinct=model.ProcessFlowList.Where(f => model.ProcessFlowList.Where(kk => kk.DefaultFlowNo.Equals(f.DefaultFlowNo)).Count() > 1).Count()>0;
            if (isFlowDistinct) {
                XtraMessageBox.Show("工序号重复!", "温馨提示");
                return false;
            }
            ////标准工时不能为空
            //if (model.ProcessFlowList != null)
            //{
            //    if (model.ProcessFlowList.Any(o => o.StanardSecond == null || o.StanardSecond == 0))
            //    {
            //        XtraMessageBox.Show("标准工时不能为空", "温馨提示");
            //        return false;
            //    }

            //    if (model.ProcessFlowList.Any(o => o.StanardMinute == null || o.StanardMinute == 0))
            //    {
            //        XtraMessageBox.Show("标准工时不能为空", "温馨提示");
            //        return false;
            //    }
            //}

            var seItem = comboxProcessVersion.SelectedItem as SusComboBoxItem;
            if (null == seItem)
            {
                DialogResult dr = XtraMessageBox.Show("确认创建新的制单工序吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return false;
                }
            }
            if (null != seItem)
            {
                var processFlowVersionId = seItem.Value;
                var pVersion = seItem.Tag as DaoModel.ProcessFlowVersion;
                if (null != pVersion)
                {
                    model.ProcessFlowVersion = pVersion;
                    action.Model = model;
                    action.UpdateProcessOrderFlow();
                    ProcessFlowSusToolBar.ShowCancelButton = false;
                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));

                    return true;
                }
            }
            action.Model = model;
            action.AddProcessOrderFlow();
            // XtraMessageBox.Show("保存成功!", "温馨提示");
            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));

            return true;
        }

        /// <summary>
        /// 保存没有的基本工序
        /// </summary>

        /// <summary>
        /// 保存制单工序
        /// </summary>
        void EditProcessOrderFlow()
        {
            var model = GetProcessOrderFlow();
            if (null == model.ProcessOrder)
            {
                XtraMessageBox.Show("请选择制单", "温馨提示");
                return;
            }
            var seItem = comboxProcessVersion.SelectedItem as SusComboBoxItem;
            if (null != seItem)
            {
                var processFlowVersionId = seItem.Value;
                var pVersion = seItem.Tag as DaoModel.ProcessFlowVersion;
                if (null != pVersion)
                {
                    model.ProcessFlowVersion = pVersion;
                }
            }
        }
        decimal? d;
        ProcessFlowModel GetProcessOrderFlow()
        {
            var commonAction = new CommonQueryAction<DaoModel.BasicProcessFlow>();
            var processFlowVersion = new DaoModel.ProcessFlowVersion();

            var pOrderQueryAction = new ProcessOrderQueryAction();
            var versionNum = pOrderQueryAction.GetCurrentMaxProcessFlowVersionNo(processOrder?.Id)?.Trim();
            var flowVersionName = txtProcessFlowVersion.Text?.Trim();
            if (string.IsNullOrEmpty(flowVersionName))
            {
                txtProcessFlowVersion.Text = string.Format("{0}_{1}-{2}", processOrder?.POrderNo?.Trim(), DateTime.Now.ToString("yyMMdd"), versionNum)?.Trim();
            }
            processFlowVersion.ProVersionNo = txtProcessFlowVersion.Text.Trim();
            processFlowVersion.ProVersionNum = versionNum;
            processFlowVersion.EffectiveDate = txtEffectiveDate.DateTime.FormatDate();
            processFlowVersion.TotalSam = txtTotalSAM.Text.Trim();
            processFlowVersion.TotalStandardPrice = txtTotalStandardPrice.Text.Trim();
           
            var processFlowList = new List<DaoModel.ProcessFlow>();
            var dt = gridControl1.DataSource as List<DaoModel.BasicProcessFlowModel>;
            if (null != dt)
            {
                foreach (var bpFlow in dt)
                {
                    var pFlow = new DaoModel.ProcessFlow();
                    pFlow.ProcessOrderField = bpFlow.ProcessOrderField;
                    pFlow.ProcessCode = bpFlow.ProcessCode;
                    pFlow.ProcessName = bpFlow.ProcessName;
                    pFlow.StanardHours = bpFlow.StanardSecond.HasValue ? Math.Round(bpFlow.StanardSecond.Value/60M,4)
                                                    : d;//bpFlow.StanardHours;
                    pFlow.StanardSecond = bpFlow.StanardSecond;
                    pFlow.StanardMinute = string.IsNullOrEmpty( bpFlow.SAM) ? 0.00M: decimal.Parse(bpFlow.SAM);
                    pFlow.StandardPrice = bpFlow.StandardPrice;
                    pFlow.PrcocessRemark = bpFlow.PrcocessRmark;
                    pFlow.DefaultFlowNo = bpFlow.DefaultFlowNo;
                    pFlow.Id = bpFlow.IsProcessVersionFlow ? bpFlow.Id:null;
                    if (!string.IsNullOrEmpty(bpFlow.FlowId))
                    {
                        pFlow.BasicProcessFlow = commonAction.LoadById(bpFlow.FlowId);
                    }
                    processFlowList.Add(pFlow);
                }
            }
            var model = new ProcessFlowModel();
            model.ProcessOrder = processOrder;
            model.ProcessFlowVersion = processFlowVersion;
            model.ProcessFlowList = processFlowList;
            return model;
        }

        private void ProcessFlowMain_Load(object sender, EventArgs e)
        {
            RegisterToolButtonEvent();
        }

        private void comboxProcessVersion_SelectedValueChanged(object sender, EventArgs e)
        {
            var seItem = ((sender as ComboBoxEdit).SelectedItem) as SusComboBoxItem;
            if (null != seItem)
            {

                var processFlowVersionId = seItem.Value;
                var pVersion = seItem.Tag as DaoModel.ProcessFlowVersion;
                if (null != pVersion)
                {
                    txtEffectiveDate.DateTime = pVersion.EffectiveDate.Value;
                    txtProcessFlowVersion.Text = pVersion.ProVersionNo?.Trim();
                    txtTotalSAM.Text = pVersion.TotalSam?.Trim();
                    txtTotalStandardPrice.Text = pVersion.TotalStandardPrice?.Trim();
                }
                //获取已有工序
                var action = new ProcessOrderQueryAction();
                var list = action.GetProcessOrderFlowList(processFlowVersionId?.ToString());
                var rslt = new List<DaoModel.BasicProcessFlowModel>();
                foreach (var it in list)
                {
                    var dt = BeanUitls<DaoModel.BasicProcessFlowModel, DaoModel.ProcessFlow>.Mapper(it);
                    dt.PrcocessRmark = it.PrcocessRemark;
                    dt.SAM = it.StanardHours?.ToString();
                    rslt.Add(dt);
                }
                var gv = gridControl1.MainView as GridView;
                if (null != gv) gv.ClearRows();
                bList = rslt;
                gridControl1.DataSource = rslt;
                gridControl1.RefreshDataSource();
                //gv.Columns["ProcessCode"].OptionsColumn.AllowEdit = false;
                //gv.Columns["ProcessName"].OptionsColumn.AllowEdit = false;
                // BindProcessOrderFlow(pOrder);
            }
        }

        private void btnAddProcessFlow_Click(object sender, EventArgs e)
        {
            var indexCount = bList.Count;
            var gv = gridControl1.MainView as SusGridView;
            if (null != gv)
            {

                // gv.AddNewRow();
                bList.Add(new DaoModel.BasicProcessFlowModel()
                { ProcessOrderField=(indexCount+1).ToString()});
                gv.RefreshData();
            }
        }

        private void btnDeleteProcessFlow_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompDeleteConfirm"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var gv = gridControl1.MainView as SusGridView;
                if (null != gv)
                {
                    gv.ClearSelectRow();
                    gv.RefreshData();
                }
                CalculationSAMAndStandardPrice();
            }
        }
        CommonQueryAction<SuspeSys.Domain.BasicProcessFlow> actionBasicProcessFlow = new CommonQueryAction<SuspeSys.Domain.BasicProcessFlow>();
        private void searchControl2_TextChanged(object sender, EventArgs e)
        {
            if (null != processOrder)
            {
                var action = new ProcessOrderQueryAction();
                var searchContext = searchControl2.Text?.Trim();

                if (!string.IsNullOrEmpty(searchContext))
                {
                    action.GetProcessOrderStyleFlow(processOrder.StyleCode?.Trim());
                    gridControl2.DataSource = action.Model.BasicProcessFlowModelList.Where(f => f.ProcessCode.IndexOf(searchContext) == 0 || f.ProcessName.IndexOf(searchContext) == 0).ToList<Domain.BasicProcessFlowModel>();
                    return;
                }
                action.GetProcessOrderStyleFlow(processOrder.StyleCode?.Trim());
                var list = action.Model.BasicProcessFlowModelList;///.Where(l => l.BasicProcessFlow.StyleNo.Trim().ToLower().Equals(styleNo.Trim().ToLower()));
                gridControl2.DataSource = list;
            }

            //var action = new CommonQueryAction<SuspeSys.Domain.BasicProcessFlow>();

        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            List<Domain.BasicProcessFlow> sList = null;

            var searchContext = searchControl1.Text?.Trim();
            if (!string.IsNullOrEmpty(searchContext))
            {
                actionBasicProcessFlow.GetList();
                sList = actionBasicProcessFlow.Model.List.Where(f => f.ProcessCode.IndexOf(searchContext) == 0 || f.ProcessName.IndexOf(searchContext) == 0).ToList<Domain.BasicProcessFlow>();
                gridControl3.DataSource = sList;
                return;
            }
            actionBasicProcessFlow.GetList();
            gridControl3.DataSource = actionBasicProcessFlow.Model.List;
        }

        private void searchControl1_MouseClick(object sender, MouseEventArgs e)
        {
            searchControl1_TextChanged(sender, e);
        }

        private void searchControl2_MouseClick(object sender, MouseEventArgs e)
        {
            searchControl2_TextChanged(sender, e);
        }
    }
}
