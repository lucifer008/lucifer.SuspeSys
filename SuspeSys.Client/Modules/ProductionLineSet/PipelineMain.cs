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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SuspeSys.Client.Action.Common;
using DaoModel = SuspeSys.Domain;
using DevExpress.XtraEditors.Repository;
using SuspeSys.Client.Action.ProductionLineSet.Model;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Client.Action.ProductionLineSet;
using System.Collections;
using SuspeSys.Client.Common.Utils;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Domain.SusEnum;

namespace SuspeSys.Client.Modules.ProductionLineSet
{
    public partial class PipelineMain : SusXtraUserControl
    {
        private IList<DaoModel.StatingRoles> _statingRoles = null;
        private IList<DaoModel.StatingDirection> _statingDirectionList = CommonAction.GetList<DaoModel.StatingDirection>();

        public PipelineMain()
        {
            InitializeComponent();

            var statingRoles = CommonAction.GetList<DaoModel.StatingRoles>();

            _statingRoles = statingRoles?.Where(o => o.RoleCode != StatingType.StatingMultiFunction.Code && o.RoleCode != StatingType.StatingRework.Code &&  o.RoleCode != StatingType.StatingStorage.Code).ToList();

        }
        private PipelineIndex pipelineIndex;
        public PipelineMain(PipelineIndex _pipelineIndex) : this()
        {
            pipelineIndex = _pipelineIndex;
            BindSiteInfoGridHeader();
            _pipelineIndex.SusToolBar.OnButtonClick += SusToolBar_OnButtonClick;
        }

        private void SusToolBar_OnButtonClick(ButtonName ButtonName)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (ButtonName)
                {
                    case ButtonName.Add:
                        pipelineIndex.PPCPipelining.Enabled = false;
                        txtPipelineNo.Text = string.Empty;
                        txtPipelineNo.Enabled = true;
                        var gv = (gridControl1.MainView as GridView);
                        gv?.ClearRows();
                        pipelineIndex.ComboPipelining.Text = string.Empty;

                        pipelining = null;
                        break;
                    case ButtonName.Save:
                    case ButtonName.SaveAndAdd:
                        Save();
                        txtPipelineNo.Enabled = true;
                        pipelineIndex.ComboPipelining.Enabled = true;
                        break;
                    case ButtonName.SaveAndClose:
                        Save();
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Delete:
                        Delete();
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    default:
                        break;
                }

                //处理后续动作
                switch (ButtonName)
                {
                    case ButtonName.Save:

                        break;
                    case ButtonName.SaveAndAdd:
                        break;
                    default:
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

                this.Cursor = Cursors.Default;
            }
        }

        private void Delete()
        {
            var selectData = (((ColumnView)this.gridControl1.MainView).GetFocusedRow() as Domain.Stating);
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

                if (!action.CanDeleteStating(selectData.Id))
                {
                    XtraMessageBox.Show("站点已经使用，不能删除！", "提示信息");
                    return;
                }

                selectData.Deleted = 1;
                action.Update<Domain.Stating>(selectData);
                action.DeleteLog<Domain.Stating>(selectData.Id, "流水线管理", "PipelineIndex", selectData.GetType().Name);

                //重新绑定数据
                BindData(pipelining);
            }

            //base.Delete(dataGrid);

        }

        void Save()
        {
            if (string.IsNullOrEmpty(txtPipelineNo.Text?.Trim()))
            {
                XtraMessageBox.Show("流水线编号不能为空!", "温馨提示");
                return;
            }
            var statingList = new List<DaoModel.Stating>();
            foreach (var rowHandle in modifiedRows)
            {
                var mStating = gridControl1.MainView.GetRow(rowHandle) as DaoModel.Stating;
                if(null!=mStating)
                    statingList.Add(mStating);
            }
            if (statingList.Count == 0)
            {
                XtraMessageBox.Show("无数据变更!", "温馨提示");
                return;
            }
            if (null == pipelining)
            {

                var commAction = new CommonAction();
                var ht = new Hashtable();
                ht.Add("PipeliNo", txtPipelineNo.Text?.Trim());
                var isExist = commAction.CheckIsExist<DaoModel.Pipelining>(ht);
                if (isExist)
                {
                    XtraMessageBox.Show("流水线编号已存在!", "温馨提示");
                    return;
                }
                var pipeline = new DaoModel.Pipelining();
                pipeline.PipeliNo = txtPipelineNo.Text?.Trim();
                pipeline.ProdType = comboProdType.Tag as DaoModel.ProdType;

                // var statingList = gridControl1.DataSource as List<DaoModel.Stating> ?? new List<DaoModel.Stating>();

                var model = new ProductionLineSetModel();
                model.StatingList = statingList;
                model.Pipelining = pipeline;
                model.SiteGroup = comboSiteGroup.Tag as DaoModel.SiteGroup;

                var pAction = new ProductionLineSetAction();
                pAction.Model = model;
                pAction.AddPipelining();
            }
            else
            {
                pipelining.PipeliNo = txtPipelineNo.Text?.Trim();
                pipelining.ProdType = comboProdType.Tag as DaoModel.ProdType;

                // var statingList = gridControl1.DataSource as List<DaoModel.Stating>;
                var model = new ProductionLineSetModel();
                model.StatingList = statingList;
                model.Pipelining = pipelining;
                model.SiteGroup = comboSiteGroup.Tag as DaoModel.SiteGroup;

                var pAction = new ProductionLineSetAction();
                pAction.Model = model;
                pAction.UpdatePipelining();
            }
            // XtraMessageBox.Show("保存成功!", "温馨提示");
            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            pipelineIndex?.BindPipelining();
        }


        private void BindSiteInfoGridHeader()
        {
            var gridView = gridControl1.MainView as GridView;

            var statingRoleComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            statingRoleComboBox.NullValuePrompt = "请选择站点角色";
            statingRoleComboBox.SelectedValueChanged += statingRoleComboBox_SelectedValueChanged;
            statingRoleComboBox.ParseEditValue += new ConvertEditValueEventHandler(statingRoleComboBox_ParseEditValue);
            var statingRoleList = _statingRoles;
            foreach (var cc in statingRoleList)
            {
                statingRoleComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc.RoleName,
                    Value = cc.Id,
                    Tag = cc
                });
            }

            var statingDirectionComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            // statingDirectionComboBox.NullValuePrompt = "请选择站点角色";
            statingDirectionComboBox.SelectedValueChanged += statingDirectionComboBox_SelectedValueChanged;
            statingDirectionComboBox.ParseEditValue += new ConvertEditValueEventHandler(statingDirectionComboBox_ParseEditValue);
            var statingDirectionList = _statingDirectionList;
            foreach (var cc in statingDirectionList)
            {
                statingDirectionComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc.DirectionDesc,
                    Value = cc.Id,
                    Tag = cc
                });
            }
            var statingLanguageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            // statingDirectionComboBox.NullValuePrompt = "请选择站点角色";
            statingLanguageComboBox.SelectedValueChanged += statingLanguageComboBox_SelectedValueChanged;
            statingLanguageComboBox.ParseEditValue += new ConvertEditValueEventHandler(statingLanguageComboBox_ParseEditValue);
            var statingLanguageList = CommonAction.GetList<DaoModel.SusLanguage>();
            foreach (var cc in statingLanguageList)
            {
                statingLanguageComboBox.Items.Add(new SusComboBoxItem()
                {
                    Text = cc.LanguageValue,
                    Value = cc.Id,
                    Tag = cc
                });
            }
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="主轨号",FieldName="MainTrackNumber",Visible=true,Name="MainTrackNumber"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站位号",FieldName="StatingNo",Visible=true,Name="StatingNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="启用",FieldName="IsEnabled",Visible=true,ColumnEdit=new RepositoryItemCheckEdit(),Name="Enabled"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="方向",FieldName="Direction",Visible=false,Name=""},
                    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="方向",FieldName="DirectionTxt",ColumnEdit=statingDirectionComboBox,Visible=true,Name="DirectionTxt"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="容量",FieldName="Capacity",Visible=true,Name="Capacity"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点类型",FieldName="StatingName",Visible=true,ColumnEdit=statingRoleComboBox,Name="StatingType"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="负载监测",FieldName="IsLoadMonitor",Visible=true,ColumnEdit=new RepositoryItemCheckEdit(),Name="IsLoadMonitor"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="链式提升",FieldName="IsChainHoist",Visible=true,ColumnEdit=new RepositoryItemCheckEdit(),Name="IsChainHoist"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="提升行程缓存满",FieldName="IsPromoteTripCachingFull",Visible=true,ColumnEdit=new RepositoryItemCheckEdit(),Name="IsPromoteTripCachingFull"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点语言",FieldName="Language",ColumnEdit=statingLanguageComboBox,Visible=true,Name="Language"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="备注",FieldName="Memo",Visible=false,Name="Memo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点条码",FieldName="SiteBarCode",Visible=true,Name=""}
            });
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gridControl1.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gridControl1;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;

            //列不可编辑
            gridView.Columns["StatingNo"].OptionsColumn.AllowEdit = false;
            //gridView.Columns["MainTrackNumber"].OptionsColumn.AllowEdit = false;

            gridView.ValidatingEditor += GridView_ValidatingEditor;
            gridView.CellValueChanged += GridView_CellValueChanged;
            gridView.CustomDrawCell += GridView_CustomDrawCell;

        }



        private void GridView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "StatingName")
            {
                if (e.CellValue != null)
                {
                    var statingType = StatingType.StatingTypeList.Where(o => o.Desption.Equals(e.CellValue?.ToString().Trim())).FirstOrDefault();

                    if (statingType != null)
                    {
                        unchecked
                        { 
                            e.Appearance.BackColor = Color.FromArgb((int)statingType.EndARGB);
                            e.Appearance.BackColor2 = Color.FromArgb((int)statingType.BeginARGB) ;
                        }
                        //e.Appearance.ForeColor = Color.Red;
                    }


                    
                }
            }
        }

        private void GridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as GridView;

            int sourceHandle = view.GetDataSourceRowIndex(e.RowHandle);
            if (!modifiedRows.Contains(sourceHandle))
                modifiedRows.Add(sourceHandle);
        }
        /// <summary>
        /// 修改数据SourceRowHandle
        /// </summary>
        protected List<int> modifiedRows = new List<int>();

        private void GridView_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            if (gridView1.FocusedColumn.FieldName.ToLower() == "Capacity".ToLower()) //设置校验列
            {
                if (e.Value != null && string.IsNullOrEmpty(e.Value.ToString()))
                    return;

                int capacity = Convert.ToInt32(e.Value);
                if (capacity > 65536)
                {
                    e.ErrorText = "请输入一个正确的容量";
                    e.Valid = false;
                    e.Value = string.Empty;
                    return;
                }
            }
        }

        private void statingLanguageComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void statingLanguageComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();

                GridView gv = (gridControl1.MainView as GridView);
                var list = gridControl1.DataSource as List<DaoModel.Stating>; ;
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                list[dataIndex].SusLanguage = (item.Tag as DaoModel.SusLanguage);
                gv.SetFocusedRowCellValue("Language", (item.Tag as DaoModel.SusLanguage)?.LanguageValue);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void statingDirectionComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void statingDirectionComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();
                GridView gv = (gridControl1.MainView as GridView);
                var list = gridControl1.DataSource as List<DaoModel.Stating>; ;
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                list[dataIndex].StatingDirection = (item.Tag as DaoModel.StatingDirection);
                list[dataIndex].Direction=short.Parse((item.Tag as DaoModel.StatingDirection)?.DirectionKey);
                gv.SetFocusedRowCellValue("DirectionTxt", (item.Tag as DaoModel.StatingDirection)?.DirectionDesc);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void statingRoleComboBox_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = e.Value?.ToString(); e.Handled = true;
        }

        private void statingRoleComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //var gv = (gridControl1.MainView as GridView);
            //var crCom = sender as RepositoryItemComboBox;
            //var colorKey = (gridControl1.MainView as GridView).FocusedColumn.FieldName;
            //BaseEdit edit = gv.ActiveEditor;
            //gv.SetFocusedRowCellValue("ColorDescption", "ssss");
            SusComboBoxItem item = new SusComboBoxItem();
            try
            {
                var list = gridControl1.DataSource as List<DaoModel.Stating>;
                //1.获取下拉框选中值
                item = (SusComboBoxItem)(sender as ComboBoxEdit).SelectedItem;
                string text = item.Text.ToString();
                //var value = (string)item.Value;
                //2.获取gridview选中的行
                GridView gv = (gridControl1.MainView as GridView);
                int dataIndex = gv.GetDataSourceRowIndex(gv.FocusedRowHandle);
                list[dataIndex].StatingRoles = (item.Tag as DaoModel.StatingRoles);
                //3.保存选中值到datatable
                //dt.Rows[dataIndex]["value"] = value;
                //dt.Rows[dataIndex]["text"] = text;
                gv.SetFocusedRowCellValue("StatingName", (item.Tag as DaoModel.StatingRoles)?.RoleName);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "提示");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void btnAddSite_Click(object sender, EventArgs e)
        {
            try
            {
                btnAddSite.Cursor = Cursors.WaitCursor;
                var list = gridControl1.DataSource as List<DaoModel.Stating>;
                var statingInput = new StatingInput(list, 0);
                statingInput.ShowDialog();
                if (null == statingInput.StatingInfoModel) return;
                BatchGeneratorStatingList(statingInput.StatingInfoModel);
            }
            finally
            {
                btnAddSite.Cursor = Cursors.Default;
            }
        }
        void BatchGeneratorStatingList(StatingInfoModel statingInfo)
        {
            var defaultRole = _statingRoles?.Where(o => o.RoleCode.Trim().Equals(StatingType.StatingSeamingCode)).FirstOrDefault();
            var defaultDirection = _statingDirectionList?.Where(o => o.DirectionKey.Trim().Equals("1")).FirstOrDefault();

            var list = gridControl1.DataSource as List<DaoModel.Stating> ?? new List<DaoModel.Stating>();
            for (var index = statingInfo.BeginNum; index <= statingInfo.EndNum; index++)
            {
                var stating = new DaoModel.Stating();
                stating.StatingNo = index.ToString();
                stating.MainTrackNumber = (short)statingInfo.MainTrackNumber;
                stating.IsChainHoist = false;
                stating.IsEnabled = true;
                stating.IsLoadMonitor = false;
                stating.IsPromoteTripCachingFull = false;
                stating.Direction = 1; // 出站，入站
                stating.Capacity = 20;
                stating.StatingRoles = defaultRole;
                stating.StatingName = defaultRole?.RoleName;

                stating.StatingDirection = defaultDirection;
                stating.DirectionTxt = defaultDirection?.DirectionDesc;

                //stating.
                list.Add(stating);
            }
            gridControl1.DataSource = list.Where(o =>null == o.Deleted || o.Deleted == 0).ToList<DaoModel.Stating>();
            gridControl1.RefreshDataSource();


            var mainView = gridControl1.MainView as DevExpress.XtraGrid.Views.Base.ColumnView;
            //for (int i = 0; i < mainView.RowCount; i++)
            //{
            //    mainView.getrow
            //}
            var source = gridControl1.DataSource as List<DaoModel.Stating>;
            if (source != null)
            {
                for (int i = 0; i < source.Count; i++)
                {
                    if (string.IsNullOrEmpty(source[i].Id) &&
                        !modifiedRows.Contains(i))
                    {
                        modifiedRows.Add(i);
                    }
                }
            }

        }
        private void PipelineMain_Load(object sender, EventArgs e)
        {
            BindInfo();

        }

        DaoModel.Pipelining pipelining;
        //PipelineIndex pipelineIndex;
        public void BindData(DaoModel.Pipelining _pipelining)
        {
            // pipelineIndex = _pIndex;
            (gridControl1.MainView as GridView)?.ClearRows();

            pipelining = _pipelining;
            txtPipelineNo.Text = _pipelining.PipeliNo?.Trim();
            txtPushRodNum.Text = _pipelining.PushRodNum?.ToString();
            txtMemo.Text = _pipelining.Memo?.Trim();
            txtPipelineNo.Enabled = false;

            comboProdType.EditValue = _pipelining.ProdType?.Id;
            comboProdType.Text = _pipelining.ProdType?.PorTypeName;
            comboProdType.Tag = _pipelining.ProdType;

            var pAction = new ProductionLineSetAction();
            var statingList = pAction.GetStatingList(_pipelining?.SiteGroup.Id);
            statingList.ToList<DaoModel.Stating>().ForEach(f=>f.DirectionTxt=f.StatingDirection?.DirectionDesc?.Trim());
            gridControl1.DataSource = statingList;
            var siteGroupList = pAction.GetPipeliningSiteGroupList(_pipelining?.SiteGroup.Id);
            if (siteGroupList.Count > 0)
            {
                var siteGroup = siteGroupList.Single();
                comboSiteGroup.Tag = siteGroup;
                // comboSiteGroup.EditValue = siteGroup?.Id;
                comboSiteGroup.Text = siteGroup?.GroupNo.Trim();

               // mainTrackNumber = siteGroup.MainTrackNumber.Value;
            }
            //comboSiteGroup.EditValue=pipelining.si
        }

        private void BindInfo()
        {
            try
            {
                //生产组别
                var poupcSiteGroup = new DevExpress.XtraEditors.PopupContainerControl();
                poupcSiteGroup.Width = 400;
                poupcSiteGroup.AutoSize = true;
                poupcSiteGroup.AutoSizeMode = AutoSizeMode.GrowOnly;
                poupcSiteGroup.Height = 200;
                comboSiteGroup.Properties.PopupControl = poupcSiteGroup;

                var gdSiteGroup = new GridControl() { Dock = DockStyle.Fill };
                var gvSiteGroup = new GridView();
                gdSiteGroup.MainView = gvSiteGroup;
                gvSiteGroup.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工厂",FieldName="FactoryCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="车间",FieldName="WorkshopCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="主轨号",FieldName="MainTrackNumber",Visible=true}
            });
                gvSiteGroup.Columns["FactoryCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;//据左对齐
                gvSiteGroup.Columns["WorkshopCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;//据左对齐
                gvSiteGroup.Columns["GroupNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;//据左对齐
                gvSiteGroup.Columns["MainTrackNumber"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;//据左对齐

                gvSiteGroup.OptionsBehavior.Editable = false;
                gdSiteGroup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gvSiteGroup});
                gdSiteGroup.MainView = gvSiteGroup;
                //gridControl1.DataSource = commonAction.GetAllColorList();
                gvSiteGroup.BestFitColumns();//按照列宽度自动适配
                gvSiteGroup.GridControl = gdSiteGroup;
                
                gvSiteGroup.OptionsView.ShowFooter = true;
                gvSiteGroup.OptionsView.ShowGroupPanel = false;
                poupcSiteGroup.Controls.Add(gdSiteGroup);
                
                gdSiteGroup.DataSource = CommonAction.GetList<DaoModel.SiteGroup>();
                gdSiteGroup.MainView.MouseDown += SiteGroup_MouseDown;

                //产线类别
                var poupcProType = new DevExpress.XtraEditors.PopupContainerControl();
                comboProdType.Properties.PopupControl = poupcProType;
                var gdProType = new GridControl() { Dock = DockStyle.Fill };
                var gvProType = new GridView();
                gdProType.MainView = gvProType;
                gvProType.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="类型代码",FieldName="PorTypeCode",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="类型名称",FieldName="PorTypeName",Visible=true}
            });
                gvProType.Columns["PorTypeCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;//据左对齐
                gvProType.Columns["PorTypeName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;//据左对齐

                gvProType.OptionsBehavior.Editable = false;
                gdProType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gvProType});
                gdProType.MainView = gvProType;
                //gridControl1.DataSource = commonAction.GetAllColorList();
                gvProType.BestFitColumns();//按照列宽度自动适配
                gvProType.GridControl = gdProType;
                gvProType.OptionsView.ShowFooter = true;
                gvProType.OptionsView.ShowGroupPanel = false;
                poupcProType.Controls.Add(gdProType);
                gdProType.DataSource = CommonAction.GetList<DaoModel.ProdType>();
                gdProType.MainView.MouseDown += ProType_MouseDown1;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message);
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
        }

        private void ProType_MouseDown1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                comboProdType.Properties.PopupControl.OwnerEdit.ClosePopup();
                var csl = comboProdType.Properties.PopupControl.Controls;
                if (csl.Count > 0)
                {
                    var gv = (csl[0] as GridControl)?.MainView as GridView;
                    var downHitInfo = gv.CalcHitInfo(new Point(e.X, e.Y));
                    var selecRow = gv.GetRow(downHitInfo.RowHandle) as DaoModel.ProdType;
                    if (null == selecRow) return;
                    comboProdType.Text = selecRow.PorTypeName;
                    comboProdType.Tag = selecRow;
                }
            }
        }
       // private short mainTrackNumber = 0;
        private void SiteGroup_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                comboSiteGroup.Properties.PopupControl.OwnerEdit.ClosePopup();
                var csl = comboSiteGroup.Properties.PopupControl.Controls;
                if (csl.Count > 0)
                {
                    var gv = (csl[0] as GridControl)?.MainView as GridView;
                    var downHitInfo = gv.CalcHitInfo(new Point(e.X, e.Y));
                    var selecRow = gv.GetRow(downHitInfo.RowHandle) as DaoModel.SiteGroup;
                    if (null == selecRow) return;
                    comboSiteGroup.EditValue = selecRow.GroupNo;
                    comboSiteGroup.Text = selecRow.GroupNo;
                    comboSiteGroup.Tag = selecRow;
                    (gridControl1.MainView as GridView)?.ClearRows();

                    var pAction = new ProductionLineSetAction();
                    var statingList = pAction.GetStatingList((comboSiteGroup.Tag as DaoModel.SiteGroup).Id);
                    statingList.ToList<DaoModel.Stating>().ForEach(f => f.DirectionTxt = f.StatingDirection?.DirectionDesc?.Trim());
                    gridControl1.DataSource = statingList;
                    //mainTrackNumber = selecRow.MainTrackNumber.Value;
                    //var pAction = new ProductionLineSetAction();
                    //var statingList = pAction.GetStatingList(selecRow.Id);
                    //gridControl1.DataSource = statingList;
                }
            }
        }

        private void comboSiteGroup_EditValueChanged(object sender, EventArgs e)
        {
            //if (null != comboSiteGroup.Tag)
            //{
            //    (gridControl1.MainView as GridView)?.ClearRows();

            //    var pAction = new ProductionLineSetAction();
            //    var statingList = pAction.GetStatingList((comboSiteGroup.Tag as DaoModel.SiteGroup).Id);
            //    statingList.ToList<DaoModel.Stating>().ForEach(f => f.DirectionTxt = f.StatingDirection?.DirectionDesc?.Trim());
            //    gridControl1.DataSource = statingList;
            //    //mainTrackNumber = (comboSiteGroup.Tag as DaoModel.SiteGroup).MainTrackNumber.Value;
            //}
        }
    }
}
