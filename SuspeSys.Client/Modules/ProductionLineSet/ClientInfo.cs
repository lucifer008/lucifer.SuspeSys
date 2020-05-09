using DevExpress.ExpressApp.Win.Core.ModelEditor;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action.ProductionLineSet;
using SuspeSys.Support.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Modules.ProductionLineSet
{
    public partial class ClientInfo : SuspeSys.Client.Modules.Ext.CRUDControl
    {
        ClientInfoAction _action = new ClientInfoAction();

        public ClientInfo()
        {
            InitializeComponent();
            
        }

        public ClientInfo(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();
        }

        protected override void BindGridHeader(GridControl dataGrid)
        {
            var gridView = new Ext.SusGridView();


            this.SearchControl.Properties.NullValuePrompt = "请输入客户机名";

            var comboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            comboBox.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("查询机器", 1,0));
            comboBox.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("管理机器", 2,0));

            GridColumn column = new GridColumn();
            column.Caption = "客户机类型";
            column.FieldName = "ClientMachineType";
            column.Visible = true;
            column.ColumnEdit = comboBox;
            //column.edit


            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="客户机名",FieldName="ClientMachineName",Visible=true,Name="gcClientName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="描述",FieldName="Description",Visible=true,Name="Description"},
                column,
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="添加时间",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="最后修改时间",FieldName="UpdateDateTime",Visible=true,Name="UpdateDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="删除标识",FieldName="Deleted",Visible=false},
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
            var dt = dataGrid.DataSource as IList<Domain.ClientMachinesModel>;
            if (null != dt)
            {
                dt.Add(new Domain.ClientMachinesModel());
            }


            base.AddItem(dataGrid);
        }


        protected override void Query(int currentPageIndex, SusGrid susGrid1, DevExpress.XtraEditors.SearchControl searchControl)
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

            //MessageBox.Show(string.Join(",",modifiedRows));
            var action = new CommonAction();

            var list = base.GetModifiedRow<Domain.ClientMachinesModel>();

            //var list = dataGrid.DataSource as List<Domain.ClientMachinesModel>;

            if (null != list)
            {


                foreach (var m in list)
                {
                    var t = m.TransformTo<Domain.ClientMachines>();

                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        //var dbModel = action.Get<com>
                        //if (dbList.Any(o => o.DepName == m.DepName.Trim() && o.Id != m.Id && o.Deleted == 0))
                        //{
                        //XtraMessageBox.Show(m.DepName + "已经存在", "提示");
                        //return;
                        //}

                        action.Update<Domain.ClientMachines>(t);
                    }
                    else
                    {
                        //if (dbList.Any(o => o.DepName == m.DepName.Trim()  && o.Deleted == 0))
                        //{
                        //XtraMessageBox.Show(m.DepName + "已经存在", "提示");
                        //return;
                        //}

                        //Hashtable table = new Hashtable();
                        //table.Add("DepName", t.DepName);

                        //if (action.CheckIsExist<Domain.ClientMachines>(table))
                        //{
                        //    XtraMessageBox.Show(m.DepName + "已经存在", "提示");
                        //    return;
                        //}

                        //table.Clear();
                        //table.Add("DepNo", t.DepNo);

                        //if (action.CheckIsExist<Domain.ClientMachines>(table))
                        //{
                        //    XtraMessageBox.Show(m.DepName + "已经存在", "提示");
                        //    return;
                        //}
                        t.AuthorizationInformation = string.Empty;
                        action.Save<Domain.ClientMachines>(t);
                    }
                }

                //XtraMessageBox.Show("保存成功!", "提示");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            }

            base.Save(dataGrid);
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = (((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.ClientMachinesModel).TransformTo<Domain.ClientMachines>();

            if (selectData == null)
            {
                //XtraMessageBox.Show("请选中要删除的数据");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompPleaseSelectedData"));
                return;
            }
            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            var diag = XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompDeleteConfirm"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"), MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                var action = new CommonAction();
                selectData.Deleted = 1;
                action.Update<Domain.ClientMachines>(selectData.TransformTo<Domain.ClientMachines>());

                base.Query(1);
            }

            base.Delete(dataGrid);
        }

        private void ClientInfo_Load(object sender, EventArgs e)
        {
            base.SusToolBar.ShowAddButton = false;
        }
    }
}
