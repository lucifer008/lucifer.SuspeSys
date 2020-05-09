using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using SuspeSys.Client.Action.Common;
using SuspeSys.Support.Utilities;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Action.PersonnelManagement;

namespace SuspeSys.Client.Modules.PersonnelManagement
{
    public partial class WorkTypeIndex : Ext.CRUDControl
    {
        WorkTypeIndexAction _action = new WorkTypeIndexAction();

        public WorkTypeIndex()
        {
            InitializeComponent();
        }

        public WorkTypeIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();
        }

        protected override void BindGridHeader(GridControl dataGrid)
        {
            base.SearchControl.Properties.NullValuePrompt = "请输入工种名称";
            var gridView = new Ext.SusGridView();


            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工种编码",FieldName="WTypeCode",Visible=true,Name="WTypeCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工种名称",FieldName="WTypeName",Visible=true,Name="WTypeName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="添加时间",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="最后修改时间",FieldName="UpdateDateTime",Visible=true,Name="UpdateDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="删除标识",FieldName="Deleted",Visible=true,Name="Deleted"},
            });
            gridView.Columns["Deleted"].OptionsColumn.AllowFocus = false;
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
            var dt = dataGrid.DataSource as IList<Domain.WorkTypeModel>;
            if (null != dt)
            {
                dt.Add(new Domain.WorkTypeModel());
            }
            dataGrid.MainView.RefreshData();

            base.AddItem(dataGrid);
        }

        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = _action.GetAllList(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }
        protected override void Save(GridControl dataGrid)
        {
            var action = new CommonAction();

            var list = dataGrid.DataSource as List<Domain.WorkTypeModel>;
            if (null != list)
            {
                foreach (var m in list)
                {
                    var t = m.TransformTo<Domain.WorkType>();
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.UpdateLog<Domain.WorkType>(m.Id, t, "车间信息", "WorkTypeIndex", m.GetType().Name);
                        action.Update<Domain.WorkType>(t);

                    }
                    else
                    {
                        action.Save<Domain.WorkType>(t);
                        action.InsertLog<Domain.WorkType>(m.Id, "车间信息", "WorkTypeIndex", m.GetType().Name);
                    }
                }
            }

            base.Save(dataGrid);
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = ((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.WorkTypeModel;

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
                action.Update<Domain.WorkType>(selectData.TransformTo<Domain.WorkType>());
                action.DeleteLog<Domain.WorkType>(selectData.Id, "车间信息", "WorkTypeIndex", selectData.GetType().Name);

                base.Query(1);
            }

            base.Delete(dataGrid);
        }
    }
}
