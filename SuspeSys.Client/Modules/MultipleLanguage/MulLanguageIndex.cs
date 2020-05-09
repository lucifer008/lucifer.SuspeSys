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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Action.Common;
using SuspeSys.Domain.Base;
using SuspeSys.Client.Modules.Ext;

namespace SuspeSys.Client.Modules.MultipleLanguage
{
    public partial class MulLanguageIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public MulLanguageIndex()
        {
            InitializeComponent();
        }
        public MulLanguageIndex(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
        }
        private void CustomerInfoIndex_Load(object sender, EventArgs e)
        {

        }

        private void RegisteEvent()
        {
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        SaveMulLanguage();
                        break;
                    case ButtonName.SaveAndClose:
                        SaveMulLanguage();
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Refresh:
                        Query(1);
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    //case ButtonName.Delete:
                    //    var rs = MessageBox.Show("确认要删除该客户吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //    if (rs == DialogResult.Yes)
                    //    {
                    //        var gv = susGrid1.DataGrid.MainView as GridView;
                    //        if (null == gv) return;
                    //        var delCustomer = gv.GetRow(gv.FocusedRowHandle) as MulLanguage;
                    //        if (!string.IsNullOrEmpty(delCustomer.Id))
                    //        {
                    //            CommonAction.Instance.LogicDelete<MulLanguage>(delCustomer.Id);
                    //            XtraMessageBox.Show("删除成功!", "提示");
                    //            Query(1);
                    //        }
                    //    }
                    //    break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    case ButtonName.Fix:
                        var fixFlag = this.FixFlag;
                        ucMain.FixOrNonFix(ref fixFlag, susGrid1.DataGrid);
                        this.FixFlag = fixFlag;
                        break;
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
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
                Cursor = Cursors.Default;
            }
        }

        private void SaveMulLanguage()
        {
            var action = new CommonAction();

            var list = GetModifiedRow<MulLanguage>();

            // throw new NotImplementedException();
            if (list != null)
            {
                var rows = 0;
                var commonInstance = new CommonAction();
                foreach (var m in list)
                {
                    rows = commonInstance.UpdateByDapper<MulLanguage>(m);
                }
                if (rows > 0)
                {
                    //XtraMessageBox.Show("保存成功!", "提示");
                    XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
                }
            }

        }

        /// <summary>
        /// 单元格值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            SusGridView view = sender as SusGridView;

            int sourceHandle = view.GetDataSourceRowIndex(e.RowHandle);
            if (!modifiedRows.Contains(e.RowHandle))
                modifiedRows.Add(sourceHandle);
        }
        /// <summary>
        /// 获取修改的数据，包含新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected List<T> GetModifiedRow<T>()
        {
            SusGridView view = this.susGrid1.DataGrid.MainView as SusGridView;

            List<T> list = new List<T>();
            List<object> test = new List<object>();
            foreach (var rowHandle in modifiedRows)
            {
                var obj = (T)view.GetRow(rowHandle);



                list.Add(obj);
            }

            var temp = test.Cast<T>().ToList(); //as (List<Domain.DepartmentModel>);


            return list;
        }
        void AddItem()
        {
            var dt = susGrid1.DataGrid.DataSource as IList<MulLanguage>;
            if (null != dt)
            {
                dt.Add(new MulLanguage());
            }
            susGrid1.DataGrid.MainView.RefreshData();
        }
        /// <summary>
        /// 修改数据SourceRowHandle
        /// </summary>
        protected List<int> modifiedRows = null;
        private void Query(int currentPageIndex)
        {
            modifiedRows = new List<int>();
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var commonInstance = CommonAction.Instance;
            var sql = string.Format("select * from MulLanguage t where 1=1");
            var list = commonInstance.QueryForList<MulLanguage>(sql, currentPageIndex, pageSize, out totalCount);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
        }



        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }

        private void BindGridHeader(GridControl gc)
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = susGrid1.DataGrid.MainView as SusGridView;
            gridView.CellValueChanged += GridView_CellValueChanged;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="ResKey",FieldName= "ResKey",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="资源类型",FieldName="ResType",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="资源项",FieldName="ResItem",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="简体中文",FieldName="SimplifiedChinese",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="繁体中文",FieldName="TraditionalChinese",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="English",FieldName="English",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Cambodia",FieldName="Cambodia",Visible=true},//柬埔寨
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Vietnamese",FieldName="Vietnamese",Visible=true}//越南語

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
            gridView.OptionsBehavior.Editable = true;
        }

        private void MulLanguageIndex_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            //BindData();
            Query(1);
            //RegisterGridContextMenuStrip();

            RegisteEvent();
        }
    }
}
