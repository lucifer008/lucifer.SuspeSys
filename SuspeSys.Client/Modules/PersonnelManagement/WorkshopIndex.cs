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
using SuspeSys.Client.Action.PersonnelManagement;
using DevExpress.XtraGrid;
using SuspeSys.Client.Action.Common;
using SuspeSys.Support.Utilities;
using SuspeSys.Domain;
using SuspeSys.Client.Modules.Ext;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Utils.Reflection;
using DevExpress.XtraGrid.Views.Base;

namespace SuspeSys.Client.Modules.PersonnelManagement
{
    public partial class WorkshopIndex : Ext.CRUDControl
    {
        SiteGroupIndexAction _action = new SiteGroupIndexAction();
        public WorkshopIndex()
        {
            InitializeComponent();
        }
        public WorkshopIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();
        }
        protected override void BindGridHeader(GridControl dataGrid)
        {
            base.SearchControl.Properties.NullValuePrompt = "请选择车间名称";
            var gridView = new Ext.SusGridView();

            var factoryComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

            factoryComboBox.SelectedValueChanged += factoryComboBox_SelectedValueChanged;
            factoryComboBox.ParseEditValue += new ConvertEditValueEventHandler(factoryComboBox_ParseEditValue);
            var factoryList = CommonAction.GetList<Factory>();
            foreach (var cc in factoryList)
            {
                factoryComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc.FacName,
                    Value = cc.Id,
                    Tag = cc
                });
            }
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工厂",FieldName="FacName",ColumnEdit=factoryComboBox,Visible=true,Name="FacName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车间编号",FieldName="WorCode",Visible=true,Name="WorCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车间名称",FieldName="WorName",Visible=true,Name="WorName"},
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

        private void factoryComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void factoryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();

                var gv = (SusGrid.DataGrid.MainView as GridView);
                var list = SusGrid.DataGrid.DataSource as List<WorkshopModel>; ;
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                list[dataIndex].Factory= (item.Tag as Factory);
                gv.SetFocusedRowCellValue("FacName", (item.Tag as Factory)?.FacName);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        /// <summary>
        /// 必须先绑定数据源
        /// </summary>
        /// <param name="dataGrid"></param>
        protected override void AddItem(GridControl dataGrid)
        {
            var dt = dataGrid.DataSource as IList<Domain.WorkshopModel>;
            if (null != dt)
            {
                dt.Add(new Domain.WorkshopModel());
            }
            dataGrid.MainView.RefreshData();

            base.AddItem(dataGrid);
        }
        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = _action.SearchWorkshop(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }
        protected override void Save(GridControl dataGrid)
        {
            var action = new CommonAction();
            var list = new List<Domain.Workshop>();
            var listExt = dataGrid.DataSource as List<Domain.WorkshopModel>;
            foreach (var m in listExt) {
                var w=Utils.Reflection.BeanUitls<Workshop, WorkshopModel>.Mapper(m);
                list.Add(w);
            }
            //var list = dataGrid.DataSource as List<Domain.WorkshopModel>;
            if (null != list)
            {
                foreach (var m in list)
                {
                    //var t = m.TransformTo<Domain.Workshop>();
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.UpdateLog<Domain.Workshop>(m.Id, m, "车间信息", "WorkshopIndex", m.GetType().Name);
                        action.Update<Domain.Workshop>(m);


                    }
                    else
                    {
                        action.Save<Domain.Workshop>(m);
                        action.InsertLog<Domain.Workshop>(m.Id, "车间信息", "WorkshopIndex", m.GetType().Name);

                    }
                }
            }

            base.Save(dataGrid);

            XtraMessageBox.Show("添加成功");
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = ((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.WorkshopModel;

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
                action.Update<Domain.Workshop>(selectData.TransformTo<Domain.Workshop>());
                action.DeleteLog<Domain.Workshop>(selectData.Id, "车间信息", "WorkshopIndex", "Workshop");

                base.Query(1);
            }

            base.Delete(dataGrid);
        }

    }
}
