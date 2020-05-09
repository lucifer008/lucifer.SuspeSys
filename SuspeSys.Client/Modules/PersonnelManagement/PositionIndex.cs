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
    public partial class PositionIndex : Ext.CRUDControl
    {
        PositionIndexAction _action = new PositionIndexAction();

        public PositionIndex()
        {
            InitializeComponent();
        }

        public PositionIndex(XtraUserControl1 ucMain) : base(ucMain)
        {
            InitializeComponent();
        }

        protected override void BindGridHeader(GridControl dataGrid)
        {
            base.SearchControl.Properties.NullValuePrompt = "请输入职务名称";
            var gridView = new Ext.SusGridView();


            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="职务编号",FieldName="PosCode",Visible=true,Name="PosCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="职务名称",FieldName="PosName",Visible=true,Name="PosName"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="备注",FieldName="Memo",Visible=true},
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
            var dt = dataGrid.DataSource as IList<Domain.PositionModel>;
            if (null != dt)
            {
                dt.Add(new Domain.PositionModel());
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

            var list = base.GetModifiedRow<Domain.PositionModel>();

            if (null != list)
            {
                    

                foreach (var m in list)
                {
                    var t = m.TransformTo<Domain.Position>();

                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        //var dbModel = action.Get<com>
                        //if (dbList.Any(o => o.DepName == m.DepName.Trim() && o.Id != m.Id && o.Deleted == 0))
                        //{
                        //XtraMessageBox.Show(m.DepName + "已经存在", "提示");
                        //return;
                        //}
                        action.UpdateLog<Domain.Position>(t.Id, t, "职位信息", "PositionIndex", t.GetType().Name);
                        action.Update<Domain.Position>(t);

                    }
                    else
                    {
                        //if (dbList.Any(o => o.DepName == m.DepName.Trim()  && o.Deleted == 0))
                        //{
                        //XtraMessageBox.Show(m.DepName + "已经存在", "提示");
                        //return;
                        //}

                        Hashtable table = new Hashtable();
                        table.Add("PosName", t);

                        if (action.CheckIsExist<Domain.Position>(table))
                        {
                            XtraMessageBox.Show(m.PosName + "已经存在", "提示");
                            return;
                        }

                        table.Clear();
                        table.Add("PosCode", t.PosCode);

                        if (action.CheckIsExist<Domain.Position>(table))
                        {
                            XtraMessageBox.Show(m.PosCode + "已经存在", "提示");
                            return;
                        }

                        action.Save<Domain.Position>(t);
                        action.InsertLog<Domain.Position>(t.Id,  "职位信息", "PositionIndex", t.GetType().Name);

                    }
                }

                //XtraMessageBox.Show("保存成功!", "提示");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            }

            base.Save(dataGrid);
        }

        protected override void Delete(GridControl dataGrid)
        {
            var selectData = (((ColumnView)dataGrid.MainView).GetFocusedRow() as Domain.PositionModel).TransformTo<Domain.Position>(); 

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
                action.Update<Domain.Position>(selectData.TransformTo<Domain.Position>());
                action.DeleteLog<Domain.Position>(selectData.Id, "职位信息", "PositionIndex", selectData.GetType().Name);

                base.Query(1);
            }

            base.Delete(dataGrid);
        }
    }
}
