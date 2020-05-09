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
using SuspeSys.Client.Action.Common;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Common.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Utils.Reflection;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Action.BasicInfo;
using SuspeSys.Client.Action;

namespace SuspeSys.Client.Modules.BasicInfo
{
    public partial class StyleProcessLirbaryInputMain : SusXtraUserControl
    {
        public StyleProcessLirbaryInputMain()
        {
            InitializeComponent();
        }
        private void BindGridBasicFlowHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new GridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                 new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="DefaultFlowNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(SAM)",FieldName="Sam",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工价(元)",FieldName="StandardPrice",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工艺指导说明",FieldName="PrcocessRmark",Visible=true}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.OptionsBehavior.Editable = false;
            gc.AllowDrop = true;
            gridView.MouseDown += GridView_MouseDown;
        }
        private void BindGridStyleFlowHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new GridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
               new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                   new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="FlowNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(SAM)",FieldName="Sam",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工价(元)",FieldName="StandardPrice",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工艺指导说明",FieldName="PrcocessRmark",Visible=true}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.OptionsBehavior.Editable = false;
            gc.AllowDrop = true;
            gc.DragEnter += Gc_DragEnter;
            gc.DragDrop += Gc_DragDrop;
        }
        public SusToolBar SusToolBar { get; internal set; }

        #region 拖拽相关
        private void Gc_DragDrop(object sender, DragEventArgs e)
        {
            var dd = e.Data.GetData(typeof(DaoModel.BasicProcessFlow)) as DaoModel.BasicProcessFlow;
            if (dd == null) return;
            GeneratorStyleProcessFlowSectionItem(dd);
        }
        void GeneratorStyleProcessFlowSectionItem(DaoModel.BasicProcessFlow d)
        {
            var list = StyleProcessFlowSectionItemList.Where(f => f.BasicProcessFlow.Id.Equals(d.Id)).ToList<DaoModel.StyleProcessFlowSectionItem>();
            if (list.Count > 0)
            {
                XtraMessageBox.Show(string.Format("工序【{0}】已存在!", d.ProcessCode?.Trim()), "温馨提示");
                return;
            }
            var t = BeanUitls<DaoModel.StyleProcessFlowSectionItem, DaoModel.BasicProcessFlow>.Mapper(d);
            t.FlowNo = d.DefaultFlowNo;
            t.BasicProcessFlow = d;
            StyleProcessFlowSectionItemList.Add(t);
            gridControl1.DataSource = StyleProcessFlowSectionItemList;
            gridControl1.MainView.RefreshData();
        }
        private void Gc_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        GridHitInfo downHitInfo = null;
        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            var gv = (sender as GridView);
            downHitInfo = gv.CalcHitInfo(new Point(e.X, e.Y));
            var d = gv.GetRow(downHitInfo.RowHandle);
            if (null == d) return;
            gridControl1.DoDragDrop(d, DragDropEffects.Copy);//开始拖放操作
        }

        #endregion
        public DevExpress.XtraEditors.PopupContainerEdit comStyleList;
        private DaoModel.Style EditStyle;
        public DevExpress.XtraGrid.GridControl gcStyleSelectDialog;
        IList<DaoModel.StyleProcessFlowSectionItem> StyleProcessFlowSectionItemList = new List<DaoModel.StyleProcessFlowSectionItem>();
        public void BindStyleProcessFlowData(SuspeSys.Domain.Style style)
        {
            EditStyle = style;
            txtStyleNo.Text = style.StyleNo?.Trim();
            txtStyleName.Text = style.StyleName?.Trim();
            txtMemo.Text = style.Rmark?.Trim();
            var styleProcessFlowList = CommonAction.GetList<SuspeSys.Domain.StyleProcessFlowSectionItem>().Where(f => f.Style.Id.Equals(style.Id)).ToList<SuspeSys.Domain.StyleProcessFlowSectionItem>();
            gridControl1.DataSource = styleProcessFlowList;
            StyleProcessFlowSectionItemList = styleProcessFlowList;
        }
        private void StyleProcessLirbaryInputMain_Load(object sender, EventArgs e)
        {
            if (null != SusToolBar) SusToolBar.OnButtonClick += SusToolBar1_OnButtonClick;
            BindGridBasicFlowHeader(gcBasicProcessFlow);
            BindGridStyleFlowHeader(gridControl1);
            BindBasicProcessFlowData();
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        Save();
                        break;
                    case ButtonName.SaveAndAdd:
                        Save();
                        break;
                    case ButtonName.SaveAndClose:
                        Save();
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Add:
                        EditStyle = null;
                        (gridControl1.MainView as GridView).ClearRows();
                        comStyleList.Text = string.Empty;
                        txtMemo.Text = string.Empty;
                        txtStyleName.Text = string.Empty;
                        txtStyleNo.Text = string.Empty;
                        break;

                    case ButtonName.Delete:
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
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

                        break;
                    case ButtonName.SaveAndAdd:
                        EditStyle = null;
                        (gridControl1.MainView as GridView).ClearRows();
                        comStyleList.Text = string.Empty;
                        txtMemo.Text = string.Empty;
                        txtStyleName.Text = string.Empty;
                        txtStyleNo.Text = string.Empty;
                        //var processFlowIndex = new ProcessFlowIndex(ucMain) { Dock = DockStyle.Fill };
                        //var tab = new XtraTabPage();
                        //tab.Name = "processFlowIndex" + CommonUtils.Counter;
                        //tab.Text = "新增制单";
                        //if (!ucMain.MainTabControl.TabPages.Contains(tab))
                        //{
                        //    tab.Controls.Add(processFlowIndex);
                        //    ucMain.MainTabControl.TabPages.Add(tab);
                        //}
                        //ucMain.MainTabControl.SelectedTabPage = tab;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {

                this.Cursor = Cursors.Default;
            }
        }
        void Save()
        {
            //EditStyle
            var basicInfoAction = new BasicInfoAction();
            var styleProcessFlowList = gridControl1.DataSource as List<DaoModel.StyleProcessFlowSectionItem> ?? new List<DaoModel.StyleProcessFlowSectionItem>();

            if (null == EditStyle)
            {
                // XtraMessageBox.Show("请选择款式!", "温馨提示");
                var styleList = CommonAction.GetList<SuspeSys.Domain.Style>();
                styleList.ToList().ForEach(f => f.StyleNo = f.StyleNo?.Trim());
                var isExist = styleList.Where(f => f.StyleNo.Equals(txtStyleNo.Text?.Trim())).Count() > 0;
                if (isExist) {
                    XtraMessageBox.Show("款号已存在!", "温馨提示");
                    return;
                }
                basicInfoAction.Model.Style = new DaoModel.Style()
                {
                    StyleName = txtStyleName.Text?.Trim(),
                    StyleNo = txtStyleNo.Text?.Trim(),
                    Rmark = txtMemo.Text?.Trim()
                };
               
                basicInfoAction.Model.StyleProcessFlowSectionItemList = styleProcessFlowList;
                basicInfoAction.AddStyleProcessFlow();
                // XtraMessageBox.Show("保存成功!", "温馨提示");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));

                comStyleList.Text = basicInfoAction.Model.Style.StyleName?.Trim();
                BindStyleProcessFlowData(basicInfoAction.Model.Style);
                gcStyleSelectDialog.DataSource = CommonAction.GetList<SuspeSys.Domain.Style>();
                return;
            }

            //if (null == EditStyle) {
            //    basicInfoAction.Model.StyleProcessFlowSectionItemList = styleProcessFlowList;
            //    basicInfoAction.UpdateStyleProcessFlow();
            //    return;
            //}
            var styleListUpdate = CommonAction.GetList<SuspeSys.Domain.Style>();
            styleListUpdate.ToList().ForEach(f => f.StyleNo = f.StyleNo?.Trim());
            var isExistUpdate = styleListUpdate.Where(f => f.StyleNo.Equals(txtStyleNo.Text?.Trim()) && !f.Id.Equals(EditStyle.Id)).Count() > 0;
            if (isExistUpdate)
            {
                XtraMessageBox.Show("款号已存在!", "温馨提示");
                return;
            }
            EditStyle.StyleNo = txtStyleNo.Text?.Trim();
            EditStyle.StyleName = txtStyleName.Text?.Trim();
            EditStyle.Rmark = txtMemo.Text?.Trim();

            styleProcessFlowList.ForEach(f => f.Style.Id = EditStyle.Id);
            basicInfoAction.Model.Style = EditStyle;
            basicInfoAction.Model.StyleProcessFlowSectionItemList = styleProcessFlowList;
            basicInfoAction.UpdateStyleProcessFlow();
            XtraMessageBox.Show("保存成功!", "温馨提示");
            comStyleList.Text = EditStyle.StyleName?.Trim();
            comStyleList.Refresh();
            gcStyleSelectDialog.DataSource = CommonAction.GetList<SuspeSys.Domain.Style>();
        }
        private void BindBasicProcessFlowData()
        {
            var styleProcessFlowList = CommonAction.GetList<SuspeSys.Domain.BasicProcessFlow>();
            gcBasicProcessFlow.DataSource = styleProcessFlowList;
        }

        private void btnClearAllFlow_Click(object sender, EventArgs e)
        {
            (gridControl1.MainView as GridView).ClearRows();
        }

        private void btnDeleteFlow_Click(object sender, EventArgs e)
        {
            var gv = gridControl1.MainView as GridView ?? new GridView();
            gv.ClearSelectRow();
            //var gv = (gridControl1.MainView as GridView);
            //var deleteRows = gv.GetSelectedRows();
            //foreach (var inx in deleteRows)
            //{
            //    var sm = gv.GetRow(inx) as CusPurchColorAndSizeModel;
            //    _cusPurchColorSizeSourceList.RemoveAll(f => f.Id.Equals(sm?.Id) && f.ColorId.Equals(sm?.ColorId));
            //}
            //gridView2.ClearSelectRow();
        }
    }
}
