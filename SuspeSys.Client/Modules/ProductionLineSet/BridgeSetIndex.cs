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
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Action.Common;
using DevExpress.XtraGrid;
using SuspeSys.Client.Action.ProductionLineSet;
using SuspeSys.Support.Utilities;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Domain;
using SuspeSys.Client.Action.SuspeRemotingClient;
using DevExpress.XtraEditors.Repository;

namespace SuspeSys.Client.Modules.ProductionLineSet
{
    public partial class BridgeSetIndex : Ext.CRUDControl
    {
        public BridgeSetIndex()
        {
            InitializeComponent();
        }
        public BridgeSetIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();

        }
        protected override void InitToolBarButton(SusToolBar susToolBar)
        {

            base.InitToolBarButton(susToolBar);
        }
        protected override void BindGridHeader(GridControl dataGrid)
        {
            base.SearchControl.Properties.NullValuePrompt = "请输入内容搜索";

            var gridView = new Ext.SusGridView();

            var DirectionComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            DirectionComboBox.NullValuePrompt = "请选择方向";
            DirectionComboBox.SelectedValueChanged += DirectionComboBox_SelectedValueChanged;
            DirectionComboBox.ParseEditValue += DirectionComboBox_ParseEditValue;

            DirectionComboBox.Items.Add(new SusComboBoxItem()
            {
                Text = "<<>>",
                Value = 0,
                Tag = "0-<<>>"
            });
            DirectionComboBox.Items.Add(new SusComboBoxItem()
            {
                Text = ">>>>",
                Value = 1,
                Tag = "1->>>>"
            });

            DirectionComboBox.Items.Add(new SusComboBoxItem()
            {
                Text = "<<<<",
                Value = 2,
                Tag = "2-<<<<"
            });
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
              //  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="顺序号",FieldName="BIndex",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="A线主轨",FieldName="AMainTrackNumber",Visible=true,Name="AMainTrackNumber"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="A线站号",FieldName="ASiteNo",Visible=true,Name="ASiteNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="方向",FieldName="DirectionTxt",ColumnEdit=DirectionComboBox,Visible=true,Name="DirectionTxt"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="B线主轨",FieldName="BMainTrackNumber",Visible=true,Name="BMainTrackNumber"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="B线主轨站号",FieldName="BSiteNo",Visible=true,Name="BSiteNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="是否启用",FieldName="Enabled",Visible=true,ColumnEdit=new RepositoryItemCheckEdit(),Name="Enabled"}
            });
            // gridView.Columns["Deleted"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["Deleted"].

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

        private void DirectionComboBox_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void DirectionComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;

                SusGridView gv = (SusGrid.DataGrid.MainView as SusGridView);
                var list = SusGrid.DataGrid.DataSource as List<BridgeSet>;
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                var ctArr = item.Tag.ToString().Split('-');
                list[dataIndex].Direction = short.Parse(ctArr[0]);
                list[dataIndex].DirectionTxt = ctArr[1];
                gv.SetFocusedRowCellValue("DirectionTxt", ctArr[1]);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMessageBox.Show(ex.Message, "提示");
            }
        }

        /// <summary>
        /// 必须先绑定数据源
        /// </summary>
        /// <param name="dataGrid"></param>
        protected override void AddItem(GridControl dataGrid)
        {
            var dt = dataGrid.DataSource as IList<Domain.BridgeSet>;
            if (null != dt)
            {
                dt.Add(new Domain.BridgeSet());
            }
            dataGrid.MainView.RefreshData();

            base.AddItem(dataGrid);
        }
        private ProductionLineSetAction action = new ProductionLineSetAction();
        protected override void Query(int currentPageIndex, SusGrid susGrid1, SearchControl searchControl)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = action.SearchBridgeSet(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl.Text.Trim());
            list.ToList().ForEach(delegate (BridgeSet bs)
            {
                if (bs.Enabled == null)
                {
                    bs.Enabled = false;
                }
            });
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);

            base.Query(currentPageIndex, susGrid1, searchControl);
        }
        protected override void Save(GridControl dataGrid)
        {
            var action = new CommonAction();

            var list = dataGrid.DataSource as List<Domain.BridgeSet>;
            if (null != list)
            {
                var listDis = new List<string>();
                foreach (var m in list)
                {
                    //var t = m.TransformTo<Domain.Factory>();
                    if (m.Enabled.Value)
                    {
                        var vv = m.AMainTrackNumber.Value + "" + m.BMainTrackNumber.Value;
                        if (listDis.Contains(vv)) {
                            XtraMessageBox.Show("同方向桥接不能同时启用!");
                            return;
                        }
                        listDis.Add(vv);
                    }
                    string info = string.Empty;
                    action.CheckStatingIsExist(m, ref info);
                    if (!string.IsNullOrEmpty(info)) {
                        XtraMessageBox.Show(info);
                        return;
                    }
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.UpdateLog<Domain.BridgeSet>(m.Id, m, "桥连配置信息", "BridgeSet", m.GetType().Name);
                        action.Update<Domain.BridgeSet>(m);

                    }
                    else
                    {
                        action.Save<Domain.BridgeSet>(m);
                        action.InsertLog<Domain.BridgeSet>(m.Id, "桥连配置信息", "BridgeSet", m.GetType().Name);
                    }
                }
                SuspeRemotingService.reloadCacheService.ReloadBridgeSet();
            }

            base.Save(dataGrid);

            XtraMessageBox.Show("保存成功");
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = (((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.BridgeSet);

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
                action.PhysicsDelte<Domain.BridgeSet>(selectData.Id);
                action.DeleteLog<Domain.BridgeSet>(selectData.Id, "桥连配置信息", "BridgeSet", selectData.GetType().Name);
                base.Query(1);
            }

            base.Delete(dataGrid);
        }

    }
}
