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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.Query;
using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action.Customer;
using System.Collections;
using SuspeSys.Client.Action.Customer.Model;

namespace SuspeSys.Client.Modules
{
    /// <summary>
    /// 客户信息
    /// </summary>
    public partial class CustomerInfoIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public CustomerInfoIndex()
        {
            InitializeComponent();
        }
        public CustomerInfoIndex(XtraUserControl1 _ucMain) : this()
        {
            ucMain = _ucMain;
        }
        private void CustomerInfoIndex_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            //BindData();
            Query(1);
            //RegisterGridContextMenuStrip();
            RegisterTooButtonEvent();
            RegisterGridEvent();
        }

        private void RegisterGridEvent()
        {
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
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
            var gridView = susGrid1.DataGrid.MainView as GridView;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户编号",FieldName="CusNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户名称",FieldName="CusName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="地址",FieldName="Address",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="联系人",FieldName="LinkMan",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="电话",FieldName="Tel",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",ColumnEdit=new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit(),Visible=true}

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

        private void BindData()
        {
            var action = new CommonQueryAction<DaoModel.Customer>();
            action.GetList();
            //var list = SuspeSys.Utils.Reflection.BeanUitls<List<DaoModel.CustomerModel>, List<DaoModel.Customer>>.Mapper(action.Model.List.ToList());
            susGrid1.DataGrid.DataSource = action.Model.List;
            //var p=new CommonQueryAction<SuspeSys.Domain.ProcessOrder>();
            //p.GetList();
        }
        void RegisterGridContextMenuStrip()
        {
            susGrid1.gridContextMenuStrip.Items.Clear();
            var tsmItemCopyAndNew = new ToolStripMenuItem() { Text = "复制(新增)" };
            tsmItemCopyAndNew.Click += TsmItemCopyAndNew_Click;
            susGrid1.gridContextMenuStrip.Items.AddRange(new ToolStripMenuItem[] { tsmItemCopyAndNew });
        }
        void RegisterTooButtonEvent()
        {
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
                        SaveCustomer();
                        //XtraMessageBox.Show("保存成功!", "提示");
                        XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
                        break;
                    case ButtonName.Add:
                        AddItem();
                        break;
                    case ButtonName.Refresh:
                        Query(1);
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Delete:
                        var rs = MessageBox.Show("确认要删除该客户吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rs == DialogResult.Yes)
                        {
                            var gv = susGrid1.DataGrid.MainView as GridView;
                            if (null == gv) return;
                            var delCustomer = gv.GetRow(gv.FocusedRowHandle) as DaoModel.Customer;
                            if (!string.IsNullOrEmpty(delCustomer.Id))
                            {
                                CommonAction.Instance.LogicDelete<DaoModel.Customer>(delCustomer.Id);
                                XtraMessageBox.Show("删除成功!", "提示");
                                Query(1);
                            }
                        }
                        break;
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
                XtraMessageBox.Show(ex.Message, "错误");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        void SaveCustomer()
        {
            var action = new CommonAction();
            var list = susGrid1.DataGrid.DataSource as List<DaoModel.Customer>;
            if (null != list)
            {
                foreach (var m in list)
                {
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.Update<DaoModel.Customer>(m);
                    }
                    else
                    {
                        action.Save<DaoModel.Customer>(m);
                    }
                }
            }
        }
        private void TsmItemCopyAndNew_Click(object sender, EventArgs e)
        {
            AddItem();
        }
        void AddItem()
        {
            var dt = susGrid1.DataGrid.DataSource as IList<DaoModel.Customer>;
            if (null != dt)
            {
                dt.Add(new DaoModel.Customer());
            }
            susGrid1.DataGrid.MainView.RefreshData();
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Query(1);
        }
        CustomerAction cutomerAction = new CustomerAction();
        void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var conModel = new CustomerModel();
            conModel.SearchKey = searchControl1.Text.Trim();
            IDictionary<string, string> ordercondition = null;
            var list = cutomerAction.SearchCustomer(currentPageIndex, pageSize, out totalCount, ordercondition, conModel);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
            // susGrid1.DataGrid.DataSource = processOrderQueryAction.Model.ProcessOrderList;

        }

        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query(1);
            }
        }
    }
}
