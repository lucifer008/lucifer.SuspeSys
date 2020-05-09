using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace SuspeSys.Client.Modules.Ext
{
    public partial class CRUDControl  : Ext.SusXtraUserControl
    {
        #region 构造函数
        public CRUDControl()
        {
            InitializeComponent();
        }

        public CRUDControl(XtraUserControl1 _ucMain) : base(_ucMain)
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 修改数据SourceRowHandle
        /// </summary>
        protected List<int> modifiedRows = null;

        #region 属性
        /// <summary>
        /// 工具菜单
        /// </summary>
        protected SusToolBar SusToolBar
        {
            get
            {
                return this.susToolBar1;
            }
        }
        #region 工具栏暴漏按钮

        [Description("新增"), Category("导航按钮")]
        public bool ShowToolBarAddButton {
            set { this.SusToolBar.ShowAddButton = value; }
            get { return this.SusToolBar.ShowAddButton; }
        }
        [Description("编辑"), Category("导航按钮")]
        public bool ShowToolBarModifyButton
        {
            set { this.SusToolBar.ShowModifyButton = value; }
            get { return this.SusToolBar.ShowModifyButton; }
        }
        [Description("删除"), Category("导航按钮")]
        public bool ShowToolBarDeleteButton
        {
            set { this.SusToolBar.ShowDeleteButton = value; }
            get { return this.SusToolBar.ShowDeleteButton; }
        }
        #endregion

        /// <summary>
        /// DataGrid
        /// </summary>
        protected SusGrid SusGrid
        {
            get
            {
                return this.susGrid1;
            }
        }

        /// <summary>
        /// 查询字符串   
        /// </summary>
        protected string SearchText
        {
            get
            {
                return this.searchControl1.Text.Trim();
            }
        }

        protected DevExpress.XtraEditors.SearchControl SearchControl
        {
            get
            {
                return searchControl1;
            }
        }
        #endregion

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            
        }

        /// <summary>
        /// Control Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CRUDControl_Load(object sender, EventArgs e)
        {
            this.RegisterGridContextMenuStrip(susGrid1);

            this.BindGridHeader(susGrid1.DataGrid);

            this.InitToolBarButton(this.susToolBar1);

            this.InitEvent();

            this.Query(1);

            //this.susGrid1.DataGrid.MainView.cell
        }

        protected void Query(int currentPageIndex)
        {
            this.Query(currentPageIndex, susGrid1, searchControl1);
        }

        protected virtual void InitToolBarButton(SusToolBar susToolBar)
        {
            SusToolBar.ShowModifyButton = false;
        }

        private void InitEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPageIndex"></param>
        protected virtual void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            this.Query(currentPageIndex);
        }

        /// <summary>
        /// 初始化GridHeader
        /// </summary>
        protected virtual void BindGridHeader(GridControl dataGrid)
        {
            SusGridView gridView = (SusGridView)dataGrid.MainView;
            gridView.CellValueChanged += GridView_CellValueChanged;
            gridView.MouseDown += GridView_MouseDown;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            
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

        /// <summary>
        ///  绑定Grid右键菜单
        /// </summary>
        protected virtual void RegisterGridContextMenuStrip(SusGrid susGrid1)
        {
            susGrid1.gridContextMenuStrip.Items.Clear();
        }

        /// <summary>
        /// 按钮事件
        /// </summary>
        /// <param name="ButtonName"></param>
        protected virtual void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        Save(this.susGrid1.DataGrid);
                        break;
                    case ButtonName.Add:
                        AddItem(this.susGrid1.DataGrid);
                        break;
                    case ButtonName.Modify:
                        var selectRow = ((ColumnView)susGrid1.DataGrid.MainView).GetFocusedRow();
        
                        Modify(this.susGrid1.DataGrid, selectRow);
                        break;
                    case ButtonName.Refresh:
                        this.Query(1);
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.SaveAndAdd:
                        Save(this.susGrid1.DataGrid);
                        break;
                    case ButtonName.SaveAndClose:
                        Save(this.susGrid1.DataGrid);
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Delete:
                        Delete(this.susGrid1.DataGrid);
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
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

        protected virtual void Modify(GridControl dataGrid, object selectRow)
        {
            
        }



        /// <summary>
        /// 查询回车事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            this.Query(1, susGrid1, this.searchControl1);
        }

        protected virtual void Save(GridControl dataGrid)
        {
            this.Query(1);
        }

        protected virtual void AddItem(GridControl dataGrid)
        {
            dataGrid.MainView.RefreshData();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="currentPageIndex"></param>
        protected virtual void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            modifiedRows = new List<int>();
        }


        protected virtual void Delete(GridControl dataGrid)
        {
            //dataGrid.MainView.RefreshData();
        }
    }
}
