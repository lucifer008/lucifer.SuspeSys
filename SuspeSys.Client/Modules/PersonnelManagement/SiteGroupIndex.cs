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
using SuspeSys.Domain;

namespace SuspeSys.Client.Modules.PersonnelManagement
{
    public partial class SiteGroupIndex : Ext.CRUDControl
    {
        SiteGroupIndexAction _action = new SiteGroupIndexAction();

        public SiteGroupIndex()
        {
            InitializeComponent();
        }

        public SiteGroupIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();
        }

        protected override void BindGridHeader(GridControl dataGrid)
        {
            var gridView = new Ext.SusGridView();
            base.SearchControl.Properties.NullValuePrompt = "请输入组名称";

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组编号",FieldName="GroupNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组名称",FieldName="GroupName",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="主轨号",FieldName="MainTrackNumber",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工厂",FieldName="FactoryCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车间",FieldName="WorkshopCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="添加时间",FieldName="InsertDateTime",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="最后修改时间",FieldName="UpdateDateTime",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="删除标识",FieldName="Deleted",Visible=true},
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
            var dt = dataGrid.DataSource as IList<Domain.SiteGroupModel>;
            if (null != dt)
            {
                dt.Add(new Domain.SiteGroupModel());
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

            var allSiteGroup = CommonAction.GetList<SiteGroup>();

            if (allSiteGroup == null)
                allSiteGroup = new List<SiteGroup>();

            var list = dataGrid.DataSource as List<Domain.SiteGroupModel>;
            if (null != list)
            {
                foreach (var m in list)
                {
                    var t = m.TransformTo<Domain.SiteGroup>();
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        if (allSiteGroup.Count(o => o.GroupNo == m.GroupNo && o.MainTrackNumber == m.MainTrackNumber) > 1)
                        {
                            XtraMessageBox.Show($"已经存在,组：{m.GroupNo}, 主轨：{m.MainTrackNumber}","提示信息");
                            return;
                        }

                        action.UpdateLog<Domain.SiteGroup>(t.Id, t, "站点组", "SiteGroupIndex", t.GetType().Name);
                        action.Update<Domain.SiteGroup>(t);


                    }
                    else
                    {

                        if (allSiteGroup.Any(o => o.GroupNo == m.GroupNo && o.MainTrackNumber == m.MainTrackNumber))
                        {
                            XtraMessageBox.Show($"已经存在,组：{m.GroupNo}, 主轨：{m.MainTrackNumber}", "提示信息");
                            return;
                        }

                        action.Save<Domain.SiteGroup>(t);
                        action.InsertLog<Domain.SiteGroup>(t.Id,  "站点组", "SiteGroupIndex", t.GetType().Name);

                    }
                }
            }

            base.Save(dataGrid);

            XtraMessageBox.Show("添加成功");
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = ((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.SiteGroupModel;

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
                action.Update<Domain.SiteGroup>(selectData.TransformTo<Domain.SiteGroup>());
                action.DeleteLog<Domain.SiteGroup>(selectData.Id, "站点组", "SiteGroupIndex", selectData.GetType().Name);

                base.Query(1);
            }

            base.Delete(dataGrid);
        }
    }
}
