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
using SuspeSys.Client.Action.ClothingVehicle;
using SuspeSys.Client.Action.Common;
using SuspeSys.Support.Utilities;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Modules.Ext;
using DevExpress.XtraEditors.Repository;
using SuspeSys.Domain;

namespace SuspeSys.Client.Modules.SewingMachine
{
    /// <summary>
    /// 衣车登录日志
    /// </summary>
    public partial class SewingMachineLoginLogIndex : Ext.CRUDControl
    {
        public SewingMachineLoginLogIndex()
        {
           // InitializeComponent();
          
        }
        public SewingMachineLoginLogIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();

        }


        protected override void BindGridHeader(GridControl dataGrid)
        {
            var gridView = new SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="日期",FieldName="InsertDateTime",Visible=true,Name="InsertDateTime"},
                 new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车间",FieldName="Workshop",Visible=true,Name="workshop"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNo",Visible=true,Name="GroupNO"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站号",FieldName="StatingNo",Visible=true,Name="StatingNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣车号",FieldName="sewingMachineNo",Visible=true,Name="sewingMachineNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="衣车卡号",FieldName="CardNo",Visible=true,Name="CardNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="打卡时间",FieldName="ReadCardDateTime",Visible=true,Name="readCardDateTime"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="登录",FieldName="LoginStatusV",ColumnEdit=new RepositoryItemCheckEdit(),Visible=true,Name="btnLogin"},

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
            var dt = dataGrid.DataSource as IList<Domain.SewingMachineLoginLogModel>;
            if (null != dt)
            {
                dt.Add(new Domain.SewingMachineLoginLogModel());
            }


            base.AddItem(dataGrid);
        }
        ClothingVehicleAction _action = new ClothingVehicleAction();
        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = _action.GetSewingMachineLoginLogAllList(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            list.ToList().ForEach(delegate(SewingMachineLoginLogModel sml) {
                sml.LoginStatusV = sml.LoginStatus == null ? false : sml.LoginStatus.Value==1;
            });
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }
        protected override void Save(GridControl dataGrid)
        {

            //MessageBox.Show(string.Join(",",modifiedRows));
            var action = new CommonAction();

            var list = base.GetModifiedRow<Domain.SewingMachineLoginLogModel>();

            //var list = dataGrid.DataSource as List<Domain.DepartmentModel>;

            if (null != list)
            {


                foreach (var m in list)
                {
                    var t = m.TransformTo<Domain.SewingMachineLoginLog>();
                    t.LoginStatus = m.LoginStatusV ? (short)1 : (short)0;
                    if (!string.IsNullOrEmpty(m.Id))
                    {

                        action.Update<Domain.SewingMachineLoginLog>(t);

                    }
                    else
                    {

                        action.Save<Domain.SewingMachineLoginLog>(t);

                    }
                }

                //XtraMessageBox.Show("保存成功!", "提示");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            }

            base.Save(dataGrid);
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = (((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.SewingMachineLoginLog).TransformTo<Domain.SewingMachineLoginLog>();

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
                action.PhysicsDelte<Domain.SewingMachineLoginLog>(selectData.Id);
                base.Query(1);
            }

            base.Delete(dataGrid);
        }

    }
}
