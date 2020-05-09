﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SuspeSys.Client.Action.Query;
using DaoModel = SuspeSys.Domain;
using SuspeSys.Client.Action.Common;
using System.Collections;
using SuspeSys.Client.Action.BasicInfo;
using DevExpress.XtraGrid.Views.Base;

namespace SuspeSys.Client.Modules.BasicInfo
{
    public partial class BasicColorTableIndex : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        public BasicColorTableIndex()
        {
            InitializeComponent();
        }
        public BasicColorTableIndex(XtraUserControl1 _ucMain) : this() {
            ucMain = _ucMain;
        }
        private void susGrid1_Load(object sender, EventArgs e)
        {
            BindGridHeader(susGrid1.DataGrid);
            Query(1);
            RegisterGridContextMenuStrip();
            RegisterTooButtonEvent();
            RegisterSusGridEvent();
            RegisterCommconEvent();
            DefaultSet();
        }
        void DefaultSet() {
            this.searchControl1.Properties.NullValuePrompt = "输入颜色及颜色描述搜索";
        }
        private void RegisterCommconEvent()
        {
            searchControl1.KeyDown += searchControl1_KeyDown;
        }
        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query(1);
            }
        }
        private void RegisterSusGridEvent()
        {
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
        }
      
        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }
      
        BasicInfoAction basicInfoAction=new BasicInfoAction();
        private void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            var searchKey = searchControl1.Text.Trim();
            IDictionary<string, string> ordercondition = new Dictionary<string, string>();
            //ordercondition.Add("StyleNo", "Asc");
            var list = basicInfoAction.SearchBasicColorTable(currentPageIndex, pageSize, out totalCount, ordercondition, searchKey);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
        }

        private void BindGridHeader(DevExpress.XtraGrid.GridControl gc)
        {
            var gridView = new Ext.SusGridView();

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="序号",FieldName="SNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色",FieldName="ColorValue",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="颜色描述",FieldName="ColorDescption",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true}

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }
        //private void BindData()
        //{
        //    var action = new CommonQueryAction<SuspeSys.Domain.PoColor>();
        //    action.GetList();
        //    susGrid1.DataGrid.DataSource = action.Model.List;
        //    //var p=new CommonQueryAction<SuspeSys.Domain.ProcessOrder>();
        //    //p.GetList();

        //}
        void RegisterGridContextMenuStrip()
        {
            susGrid1.gridContextMenuStrip.Items.Clear();
            var tsmItemCopyAndNew = new ToolStripMenuItem() { Text = "复制(新增)" };
            tsmItemCopyAndNew.Click += TsmItemCopyAndNew_Click;
            susGrid1.gridContextMenuStrip.Items.AddRange(new ToolStripMenuItem[] { tsmItemCopyAndNew });
        }
        void RegisterTooButtonEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        var sucess=Save();
                        if(sucess)
                            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));;
                        break;
                    case ButtonName.Add:
                        AddItem();
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Refresh:
                        Query(1);
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    case ButtonName.Fix:
                        var fixFlag = this.FixFlag;
                        ucMain.FixOrNonFix(ref fixFlag, susGrid1.DataGrid);
                        this.FixFlag = fixFlag;
                        break;
                    case ButtonName.Delete:
                        DeleteItem();
                        break;
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMessageBox.Show(ex.Message, "错误");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        bool Save()
        {
            var action = new CommonAction();
            var list = susGrid1.DataGrid.DataSource as List<DaoModel.PoColor>;
            if (null != list)
            {
                foreach (var m in list)
                {
                    if (string.IsNullOrEmpty(m.ColorValue?.Trim()))
                    {
                        //XtraMessageBox.Show("颜色值不能为空!");
                        XtraMessageBox.Show(string.Format(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptNotBank"), Client.Action.LanguageAction.Instance.BindLanguageTxt("ColorValue")));
                        return false;
                    }
                    if (string.IsNullOrEmpty(m.Id))
                    {
                        var ht = new Hashtable();
                        ht.Add("ColorValue", m.ColorValue.Trim());
                        var ex = action.CheckIsExist<Domain.PoColor>(ht);
                        if (ex)
                        {
                            XtraMessageBox.Show(string.Format("颜色值【{0}】已存在!", m.ColorValue.Trim()));
                            return false;
                        }
                    }
                }
                foreach (var m in list)
                {
                    
                    if (!string.IsNullOrEmpty(m.Id))
                    {
                        action.Update<DaoModel.PoColor>(m);
                    }
                    else
                    {
                        action.Save<DaoModel.PoColor>(m);
                    }
                }
            }
            return true;
        }
        private void TsmItemCopyAndNew_Click(object sender, EventArgs e)
        {
            AddItem();
        }
        void AddItem()
        {
            var dt = susGrid1.DataGrid.DataSource as IList<DaoModel.PoColor>;
            if (null != dt)
            {
                dt.Add(new DaoModel.PoColor());
            }
            susGrid1.DataGrid.MainView.RefreshData();
        }

        void DeleteItem()
        {
            var selectData = (((ColumnView)susGrid1.DataGrid.MainView).GetFocusedRow() as Domain.PoColor);

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
                action.Update<Domain.PoColor>(selectData);
                action.DeleteLog<Domain.PoColor>(selectData.Id, "基本颜色表", "BasicColorTableIndex", selectData.GetType().Name);
                Query(1);
            }
        }
    }
}