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
using SuspeSys.Client.Action.PersonnelManagement;
using SuspeSys.Client.Action.Common;
using SuspeSys.Support.Utilities;
using DevExpress.XtraGrid.Views.Base;

namespace SuspeSys.Client.Modules.PersonnelManagement
{
    public partial class FactoryIndex : Ext.CRUDControl
    {
        SiteGroupIndexAction _action = new SiteGroupIndexAction();

        public FactoryIndex()
        {
            InitializeComponent();
        }
        public FactoryIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();

        }

        protected override void InitToolBarButton(SusToolBar susToolBar)
        {
            
            base.InitToolBarButton(susToolBar);
        }

        protected override void BindGridHeader(GridControl dataGrid)
        {
           base.SearchControl.Properties.NullValuePrompt = "请选择工厂名称";

            var gridView = new Ext.SusGridView();


            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工厂编号",FieldName="FacCode",Visible=true,Name="FacCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工厂名称",FieldName="FacName",Visible=true,Name="FacName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="添加时间",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="最后修改时间",FieldName="UpdateDateTime",Visible=true,Name="UpdateDateTime"}
            });
           // gridView.Columns["Deleted"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["Deleted"].
            gridView.Columns["UpdateDateTime"].OptionsColumn.AllowFocus = false;
            gridView.Columns["InsertDateTime"].OptionsColumn.AllowFocus = false;

            dataGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            dataGrid.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = dataGrid;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;

            base.BindGridHeader(dataGrid);
        }

        /// <summary>
        /// 必须先绑定数据源
        /// </summary>
        /// <param name="dataGrid"></param>
        protected override void AddItem(GridControl dataGrid)
        {
            var dt = dataGrid.DataSource as IList<Domain.Factory>;
            if (null != dt)
            {
                dt.Add(new Domain.Factory());
            }
            dataGrid.MainView.RefreshData();

            base.AddItem(dataGrid);
        }
        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = _action.SearchFactory(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }
        protected override void Save(GridControl dataGrid)
        {
            var action = new CommonAction();

            var list = dataGrid.DataSource as List<Domain.Factory>;
            if (null != list)
            {
                foreach (var m in list)
                {
                    //var t = m.TransformTo<Domain.Factory>();
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.UpdateLog<Domain.Factory>(m.Id, m, "工厂信息", "FactoryIndex", m.GetType().Name);
                        action.Update<Domain.Factory>(m);

                    }
                    else
                    {
                        action.Save<Domain.Factory>(m);
                        action.InsertLog<Domain.Factory>(m.Id, "工厂信息", "FactoryIndex", m.GetType().Name);
                    }
                }
            }

            base.Save(dataGrid);

            XtraMessageBox.Show("保存成功");
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = (((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.Factory);

            if (selectData == null)
            {
                //XtraMessageBox.Show("请选中要删除的数据");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompPleaseSelectedData"));
                return;
            }

            var diag = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompDeleteConfirm"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"), MessageBoxButtons.YesNo);//XtraMessageBox.Show("确定要删除吗？", "提示信息", MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                var action = new CommonAction();
                selectData.Deleted = 1;
                action.Update<Domain.Factory>(selectData.TransformTo<Domain.Factory>());
                action.DeleteLog<Domain.Department>(selectData.Id, "工厂信息", "FactoryIndex", selectData.GetType().Name);
                base.Query(1);
            }

            base.Delete(dataGrid);
        }
    }
}
