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
using SuspeSys.Client.Modules.Ext;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraGrid;
using SuspeSys.Client.Action.PersonnelManagement;
using SuspeSys.Domain.Ext;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Client.Action.Common;
using SuspeSys.Support.Utilities;
using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;

namespace SuspeSys.Client.Modules.PersonnelManagement
{
    public partial class CardInfoIndex : Ext.CRUDControl
    {
        public CardInfoIndex()
        {
            InitializeComponent();
        }
        public CardInfoIndex(XtraUserControl1 _ucMain) : this()
        {
            this.ucMain = _ucMain;
        }
        //void BindTreeListHeader()
        //{

        //    var columns = new TreeListColumn[]
        //    {
        //        new TreeListColumn() { Caption="Id",FieldName= "Id",Visible=false},
        //        new TreeListColumn() { Caption="顺序",FieldName="CraftFlowNo",Visible=true},
        //        new TreeListColumn() { Caption = "员工编号", FieldName = "IsEn", Visible = true, Fixed = FixedStyle.None },
        //        new TreeListColumn() { Caption="员工姓名",FieldName="IsMergeForw",Visible=true}
        //    };
        //    susTreeView1.TLData.Columns.AddRange(columns);
        //    susTreeView1.TLData.ParentFieldName = "ParentId";
        //    susTreeView1.TLData.KeyFieldName = "Id";

        //}

        private void CardInfoIndex_Load(object sender, EventArgs e)
        {
           // BindTreeListHeader();
        }

        protected override void InitToolBarButton(SusToolBar susToolBar)
        {

            base.InitToolBarButton(susToolBar);
        }
        /// <summary>
        /// 必须先绑定数据源
        /// </summary>
        /// <param name="dataGrid"></param>
        protected override void AddItem(GridControl dataGrid)
        {
            var dt = dataGrid.DataSource as IList<CardInfoModel>;
            if (null != dt)
            {
                dt.Add(new CardInfoModel());
            }


            base.AddItem(dataGrid);
        }
        protected override void BindGridHeader(GridControl dataGrid)
        {
            base.SearchControl.Properties.NullValuePrompt = "请选择工厂名称";

            var gridView = new Ext.SusGridView();

            var cardInfoTypeComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            cardInfoTypeComboBox.NullValuePrompt = "请选择站点角色";
            cardInfoTypeComboBox.SelectedValueChanged += cardInfoTypeComboBox_SelectedValueChanged;
            cardInfoTypeComboBox.ParseEditValue += new ConvertEditValueEventHandler(cardInfoTypeComboBox_ParseEditValue);
            var cardInfoTypeList = new string[] { CardType.Hanger.Desption, CardType.Hanger.Desption, CardType.MachineRepair.Desption };//, "员工卡" };
            var index = 1;
            foreach (var cc in cardInfoTypeList)
            {
                cardInfoTypeComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc,
                    Value = CardType.GetValueByDesp(cc),
                    Tag = CardType.GetValueByDesp(cc)
                });
              //  index++;
            }

            var enabledComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            enabledComboBox.SelectedValueChanged += enabledComboBox_SelectedValueChanged;
            enabledComboBox.ParseEditValue += new ConvertEditValueEventHandler(enabledComboBox_ParseEditValue);
            var enList = new string[] { "否", "是" };
            index = 0;
            foreach (var cc in enList)
            {
                enabledComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc,
                    Value = index,
                    Tag = index
                });
                index++;
            }

            var multLoginComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            multLoginComboBox.SelectedValueChanged += multLoginComboBox_SelectedValueChanged;
            multLoginComboBox.ParseEditValue += new ConvertEditValueEventHandler(multLoginComboBox_ParseEditValue);
            //var enList = new string[] { "否", "是" };
            index = 0;
            foreach (var cc in enList)
            {
                multLoginComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc,
                    Value = index,
                    Tag = cc
                });
                index++;
            }

            var employeeComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            employeeComboBox.SelectedValueChanged += EmployeeComboBox_SelectedValueChanged;
            employeeComboBox.ParseEditValue += EmployeeComboBox_ParseEditValue;
            var employeeList = CommonAction.GetList<Employee>();

            index = 0;
            foreach (var employee in employeeList)
            {
                employeeComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = employee.RealName,
                    Value = employee.Code,
                    Tag = employee
                });
                index++;
            }


            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工编号",FieldName="EmployeeCode",Visible=true,Width=80,Name="EmployeeCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工名称",FieldName="EmployeeName",Visible=true, ColumnEdit = employeeComboBox,Name="RealName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="卡号",FieldName="CardNo",Visible=true,Name="CardNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="卡类型",FieldName="CardTypeTxt" ,ColumnEdit=cardInfoTypeComboBox,Visible=true,Name="CardTypeTxt"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="启用",FieldName="EnText" ,ColumnEdit=enabledComboBox,Visible=true,Name=""},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="多点登录",FieldName="MulLoginText" ,ColumnEdit=multLoginComboBox,Visible=true,Name=""},
            });
            // gridView.Columns["Deleted"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["Deleted"].
            gridView.Columns["EmployeeCode"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["EmployeeName"].OptionsColumn.AllowFocus = false;

            dataGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            dataGrid.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            //gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = dataGrid;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;

            base.BindGridHeader(dataGrid);
        }

        private void EmployeeComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void EmployeeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                var list = SusGrid.DataGrid.DataSource as List<CardInfoModel>;
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();
                var gv = (SusGrid.DataGrid.MainView as SusGridView);
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                list[dataIndex].EmployeeName = text;
                list[dataIndex].EmployeeCode = item.Value?.ToString();

                gv.SetFocusedRowCellValue("EmployeeText", item.Text?.ToString()?.Trim());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void multLoginComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void multLoginComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                var list = SusGrid.DataGrid.DataSource as List<CardInfoModel>;
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();
                var gv = (SusGrid.DataGrid.MainView as SusGridView);
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                list[dataIndex].IsMultiLogin = short.Parse(item.Value.ToString()) == 1 ? true : false;
                gv.SetFocusedRowCellValue("MulLoginText", item.Text?.ToString()?.Trim());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void enabledComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void enabledComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                var list = SusGrid.DataGrid.DataSource as List<CardInfoModel>;
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();
                var gv = (SusGrid.DataGrid.MainView as SusGridView);
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                list[dataIndex].IsEnabled = short.Parse(item.Tag.ToString())==1?true:false;
                gv.SetFocusedRowCellValue("EnText", item.Text?.ToString()?.Trim());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void cardInfoTypeComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void cardInfoTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                var list = SusGrid.DataGrid.DataSource as List<CardInfoModel>;
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();
                var gv = (SusGrid.DataGrid.MainView as SusGridView);
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                list[dataIndex].CardType = short.Parse(item.Tag.ToString());
                gv.SetFocusedRowCellValue("CardTypeTxt", item.Text?.ToString()?.Trim());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private EmployeeIndexAction employeeAction = new EmployeeIndexAction();
        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = new Dictionary<string,string>();
            ordercondition.Add("EmployeeCode","ASC");
            var list = employeeAction.SearchEmployeeCardInfo(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.EditValue?.ToString()?.Trim());
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }
        protected override void Save(GridControl dataGrid)
        {
            var action = new CommonAction();
            var list = base.GetModifiedRow<CardInfoModel>();
            if (null != list)
            {
                action.SaveEmployeeCardInfo(list);


                //XtraMessageBox.Show("保存成功!", "提示");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            }

            base.Save(dataGrid);
        }

    }
}
