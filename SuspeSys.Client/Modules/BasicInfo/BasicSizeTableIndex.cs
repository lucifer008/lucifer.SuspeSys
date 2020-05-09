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
using SuspeSys.Client.Action.Query;
using System.Collections;
using SuspeSys.Client.Action.BasicInfo;
using SuspeSys.Client.Action.Common;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Action;

namespace SuspeSys.Client.Modules.BasicInfo
{
    public partial class BasicSizeTableIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public BasicSizeTableIndex()
        {
            InitializeComponent();
        }
        public BasicSizeTableIndex(XtraUserControl1 _ucMain) : this() {
            ucMain = _ucMain;
        }
        private void susGrid1_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            Query(1);
            RegisterGridContextMenuStrip();
            RegisterTooButtonEvent();
            RegisterSusGridEvent();
            RegisterCommconEvent();
            DefaultSet();
        }
        void DefaultSet()
        {
            this.searchControl1.Properties.NullValuePrompt = "输入尺码及尺码描述搜索";
        }

        private void RegisterCommconEvent()
        {
            searchControl1.KeyDown += searchControl1_KeyDown;
        }
        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query(1);
            }
        }

        private void RegisterSusGridEvent()
        {
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }
        BasicInfoAction basicInfoAction = new BasicInfoAction();
        private void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var searchKey = searchControl1.Text.Trim();
            IDictionary<string, string> ordercondition = new Dictionary<string, string>();
            //ordercondition.Add("StyleNo", "Asc");
            var list = basicInfoAction.SearchPSize(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
        }
        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName="PsNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码",FieldName="Size",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="尺码描述",FieldName="SizeDesption",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true}

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
        private void BindData()
        {
            var action = new CommonQueryAction<SuspeSys.Domain.PSize>();
            action.GetList();
            susGrid1.DataGrid.DataSource = action.Model.List;
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
                       var sucess= Save();
                        if(sucess)
                            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
                        break;
                    case ButtonName.Add:
                        AddItem();
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Refresh:
                        Query(1);
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    case ButtonName.Delete:
                        DeleteItem();
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
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DeleteItem()
        {
            var selectData = (((ColumnView)susGrid1.DataGrid.MainView).GetFocusedRow() as Domain.PSize);

            if (selectData == null)
            {
                //XtraMessageBox.Show("请选中要删除的数据");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompPleaseSelectedData"));
                return;
            }

            var diag = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompDeleteConfirm"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"), MessageBoxButtons.YesNo);// XtraMessageBox.Show("确定要删除吗？", "提示信息", MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                var action = new CommonAction();
                selectData.Deleted = 1;
                action.Update<Domain.PSize>(selectData);
                action.DeleteLog<Domain.PSize>(selectData.Id, "基本尺码表", "BasicSizeTableIndex", selectData.GetType().Name);
                Query(1);
            }
        }

        bool Save()
        {
            var action = new Client.Action.Common.CommonAction();
            var list = susGrid1.DataGrid.DataSource as List<Domain.PSize>;
            if (null != list)
            {
                foreach (var m in list)
                {
                    if (string.IsNullOrEmpty(m.Size?.Trim()))
                    {
                        //XtraMessageBox.Show("尺码不能为空!");
                        XtraMessageBox.Show(string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptNotBank"), Client.Action.LanguageAction.Instance.BindLanguageTxt("SizeDesption")));
                        return false;
                    }
                    if (string.IsNullOrEmpty(m.Id))
                    {
                        var ht = new Hashtable();
                        ht.Add("Size", m.Size.Trim());
                        var ex = action.CheckIsExist<Domain.PSize>(ht);
                        if (ex)
                        {
                            XtraMessageBox.Show(string.Format("尺码【{0}】已存在!", m.Size.Trim()));
                            return false;
                        }
                    }
                }
                foreach (var m in list)
                {
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.Update<Domain.PSize>(m);
                    }
                    else
                    {
                        action.Save<Domain.PSize>(m);
                    }
                }
            }
            return true;
        }
        private void TsmItemCopyAndNew_Click(object sender, EventArgs e)
        {
            AddItem();
        }
        void AddItem()
        {
            var dt = susGrid1.DataGrid.DataSource as IList<Domain.PSize>;
            if (null != dt)
            {
                dt.Add(new Domain.PSize());
            }
            susGrid1.DataGrid.MainView.RefreshData();
        }
    }
}
