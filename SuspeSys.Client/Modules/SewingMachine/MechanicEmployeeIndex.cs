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
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Action.ClothingVehicle;
using SuspeSys.Client.Action.Common;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Support.Utilities;
using DevExpress.XtraEditors.Repository;
using SuspeSys.Domain;

namespace SuspeSys.Client.Modules.SewingMachine
{
    /// <summary>
    /// 机修人员表
    /// </summary>
    public partial class MechanicEmployeeIndex : Ext.CRUDControl
    {
        public MechanicEmployeeIndex()
        {
            
        }
        public MechanicEmployeeIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();

        }
    
    
        protected override void BindGridHeader(GridControl dataGrid)
        {
            var gridView = new SusGridView();
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工号",FieldName="Code",Visible=true,Name="Code"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="姓名",FieldName="RealName",Visible=true,Name="RealName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="卡号",FieldName="CardNo",Visible=true,Name="CardNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车间",FieldName="WorkShop",Visible=true,Name="workshop"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNo",Visible=true,Name="productGroup"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="启用",FieldName="StatusV",ColumnEdit=new RepositoryItemCheckEdit(),Visible=true,Name="Enabled"}
            });
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
            var dt = dataGrid.DataSource as IList<Domain.MechanicEmployeesModel>;
            if (null != dt)
            {
                dt.Add(new Domain.MechanicEmployeesModel());
            }


            base.AddItem(dataGrid);
        }
        ClothingVehicleAction _action = new ClothingVehicleAction();
        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = _action.GetMechanicEmployeesAllList(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            list.ToList().ForEach(delegate (MechanicEmployeesModel sml) {
                sml.StatusV = sml.Status == null ? false : "1".Equals(sml.Status);
            });
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }
        protected override void Save(GridControl dataGrid)
        {

            //MessageBox.Show(string.Join(",",modifiedRows));
            var action = new CommonAction();

            var list = base.GetModifiedRow<Domain.MechanicEmployeesModel>();

            //var list = dataGrid.DataSource as List<Domain.DepartmentModel>;

            if (null != list)
            {


                foreach (var m in list)
                {
                    var t = m.TransformTo<Domain.MechanicEmployees>();
                    t.Status = m.StatusV ? 1+"" :0+"";
                    if (!string.IsNullOrEmpty(m.Id))
                    {

                        action.Update<Domain.MechanicEmployees>(t);

                    }
                    else
                    {

                        action.Save<Domain.MechanicEmployees>(t);

                    }
                }

                //XtraMessageBox.Show("保存成功!", "提示");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            }

            base.Save(dataGrid);
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = (((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.MechanicEmployees).TransformTo<Domain.MechanicEmployees>();

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
                action.PhysicsDelte<Domain.MechanicEmployees>(selectData.Id);
                base.Query(1);
            }

            base.Delete(dataGrid);
        }

    }
}
