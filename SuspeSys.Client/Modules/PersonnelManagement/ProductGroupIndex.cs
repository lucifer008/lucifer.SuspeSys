using SuspeSys.Client.Action.PersonnelManagement;
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
using System.Collections;

namespace SuspeSys.Client.Modules.PersonnelManagement
{
    public partial class ProductGroupIndex : Ext.CRUDControl
    {
        ProductGroupIndexAction _action = new ProductGroupIndexAction();

        public ProductGroupIndex()
        {
            InitializeComponent();
        }

        public ProductGroupIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();
        }

        protected override void BindGridHeader(GridControl dataGrid)
        {
            var gridView = new Ext.SusGridView();


            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别编号",FieldName="ProductGroupCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="生产组别",FieldName="ProductGroupName",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="备注",FieldName="Memo",Visible=true},
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
            var dt = dataGrid.DataSource as IList<Domain.ProductGroupModel>;
            if (null != dt)
            {
                dt.Add(new Domain.ProductGroupModel());
            }
            

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

            var list = base.GetModifiedRow<Domain.ProductGroupModel>();
            //var list = dataGrid.DataSource as List<Domain.ProductGroupModel>;
            
            if (null != list)
            {
                    

                foreach (var m in list)
                {
                    var t = m.TransformTo<Domain.ProductGroup>();

                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        //var dbModel = action.Get<com>
                        //if (dbList.Any(o => o.DepName == m.DepName.Trim() && o.Id != m.Id && o.Deleted == 0))
                        //{
                        //XtraMessageBox.Show(m.DepName + "已经存在", "提示");
                        //return;
                        //}
                        action.UpdateLog<Domain.ProductGroup>(t.Id, t, "生产组别", "ProductGroupIndex", t.GetType().Name);
                        action.Update<Domain.ProductGroup>(t);


                    }
                    else
                    {
                        //if (dbList.Any(o => o.DepName == m.DepName.Trim()  && o.Deleted == 0))
                        //{
                        //XtraMessageBox.Show(m.DepName + "已经存在", "提示");
                        //return;
                        //}

                        Hashtable table = new Hashtable();
                        table.Add("ProductGroupName", t.ProductGroupName);

                        if (action.CheckIsExist<Domain.ProductGroup>(table))
                        {
                            XtraMessageBox.Show(m.ProductGroupName + "已经存在", "提示");
                            return;
                        }

                        table.Clear();
                        table.Add("ProductGroupCode", t.ProductGroupCode);

                        if (action.CheckIsExist<Domain.ProductGroup>(table))
                        {
                            XtraMessageBox.Show(m.ProductGroupCode + "已经存在", "提示");
                            return;
                        }

                        action.Save<Domain.ProductGroup>(t);
                        action.InsertLog<Domain.ProductGroup>(t.Id, "生产组别", "ProductGroupIndex", t.GetType().Name);

                    }
                }

                //XtraMessageBox.Show("保存成功!", "提示");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            }

            base.Save(dataGrid);
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = (((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.ProductGroupModel).TransformTo<Domain.ProductGroup>(); 

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
                action.Update<Domain.ProductGroup>(selectData.TransformTo<Domain.ProductGroup>());
                action.DeleteLog<Domain.ProductGroup>(selectData.Id, "生产组别", "ProductGroupIndex", selectData.GetType().Name);

                base.Query(1);
            }

            base.Delete(dataGrid);
        }
    }
}
