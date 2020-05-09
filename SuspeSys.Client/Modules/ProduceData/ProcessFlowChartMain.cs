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
using SuspeSys.Client.Action.ProcessOrder;
using SuspeSys.Client.Action.Query;

using DaoModel = SuspeSys.Domain;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList.Columns;
using DevExpress.Data;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Nodes;
using System.IO;
using DevExpress.XtraGrid;
using SuspeSys.Client.Modules.Ext.CustomEditors;
using DevExpress.Mvvm;
using System.Collections;
using SuspeSys.Client.Modules.CuttingBed;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing.Drawing2D;

using ActionModel = SuspeSys.Client.Action.ProcessFlowChart.Model;
using SuspeSys.Client.Action.ProcessFlowChart;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Common.Utils;
using DevExpress.XtraTab;
using SuspeSys.Domain.SusEnum;
using DevExpress.Utils.Drawing;
using SuspeSys.Utils.Reflection;

namespace SuspeSys.Client.Modules.ProduceData
{
    public partial class ProcessFlowChartMain : SusXtraUserControl
    {
        //站点角色
        private IList<DaoModel.StatingRoles> _StatingRoles;

        public ProcessFlowChartMain()
        {
            InitializeComponent();
            if (!this.DesignMode)
            {
                BindGridAndTreeListHeader();
            }
        }
        void BindGridAndTreeListHeader()
        {
            BindFlowChartTreeListHeader();
            BindProcessFlowGridHeader();
            BindSiteGridHeader();
            SetDefaultValue();
            ResiterSearchControl();

            InitStatingRole();
        }

        void InitStatingRole()
        {
            _StatingRoles = Action.Common.CommonAction.GetList<DaoModel.StatingRoles>()
            .Where(o => o.RoleCode != StatingType.StatingHanger.Code &&
                                     o.RoleCode != StatingType.StatingOverload.Code &&
                                     o.RoleCode != StatingType.StatingQC.Code &&
                                     o.RoleCode != StatingType.StatingBridging.Code
                                     ).ToList<DaoModel.StatingRoles>();
        }

        private void ResiterSearchControl()
        {
            searchControl1.TextChanged += SearchControl1_TextChanged;
            searchControl2.TextChanged += SearchControl2_TextChanged;
        }
        //制单工序过滤搜索
        private void SearchControl2_TextChanged(object sender, EventArgs e)
        {
            var list = gridControl2.DataSource as List<DaoModel.ProcessFlow>;
            if (null != list && list.Count > 0)
            {
                var searchContext = searchControl2.Text?.Trim();
                var action = new ProcessOrderQueryAction();
                var listSource = action.GetProcessOrderFlowList(pFlowVersion.Id);
                if (!string.IsNullOrEmpty(searchContext))
                {
                    var oList = listSource.Where(f => f.ProcessCode.IndexOf(searchContext) == 0 || f.ProcessName.IndexOf(searchContext) == 0).ToList<DaoModel.ProcessFlow>();
                    gridControl2.DataSource = oList;
                    return;
                }

                gridControl2.DataSource = listSource;
            }
        }
        //站点过滤
        private void SearchControl1_TextChanged(object sender, EventArgs e)
        {
            var list = gridControl3.DataSource as List<DaoModel.StatingModel>;
            if (null != list && list.Count > 0)
            {
                var cbs = comboProcessGroup.EditValue?.ToString();
                var listSource = new ProcessOrderQueryAction().GetGroupStatingList(cbs?.ToString());
                var searchContext = searchControl1.Text?.Trim();
                if (!string.IsNullOrEmpty(searchContext))
                {
                    var oList = listSource.Where(f => f.StatingNo.IndexOf(searchContext) == 0 || f.StatingName.IndexOf(searchContext) == 0 || f.GroupNO.IndexOf(searchContext) == 0).ToList<DaoModel.StatingModel>();
                    gridControl3.DataSource = oList;
                    return;
                }
                //var cbs = comboProcessGroup.EditValue?.ToString();
                if (!string.IsNullOrEmpty(cbs))
                {
                    //var listSource = new ProcessOrderQueryAction().GetGroupStatingList(cbs?.ToString());
                    gridControl3.DataSource = listSource;
                }
            }
        }

        void SetDefaultValue()
        {
            tcProcessFlowAndSite.SelectedTab = tpageSiteStore;
        }

        #region 【工艺路线图】 数据提交部分
        void RegisterToolButtonEvent()
        {
            if (null != ProcessFlowChartSusToolBar)
            {
                ProcessFlowChartSusToolBar.OnButtonClick += SusToolBar1_OnButtonClick;
            }
        }
        //数据提交部分
        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        SaveProcessFlowChart();
                        break;
                    case ButtonName.SaveAndAdd:
                        SaveProcessFlowChart();
                        break;
                    case ButtonName.SaveAndClose:
                        if (SaveProcessFlowChart())
                        {
                            ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        }
                        break;
                    case ButtonName.Delete:
                        break;
                    case ButtonName.Add:
                        AddItem();
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

                        var processFlowIndex = new ProcessFlowIndex(ucMain) { Dock = DockStyle.Fill };
                        var tab = new XtraTabPage();
                        tab.Name = "新增工艺图";
                        tab.Text = "新增工艺图";
                        tab.Tag = "NewFlowChart";
                        if (!ucMain.MainTabControl.TabPages.Contains(tab))
                        {
                            tab.Controls.Add(processFlowIndex);
                            ucMain.MainTabControl.TabPages.Add(tab);
                        }
                        ucMain.MainTabControl.SelectedTabPage = tab;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {

                this.Cursor = Cursors.Default;
            }
        }

        ProcessFlowChartAction chartAction = new Action.ProcessFlowChart.ProcessFlowChartAction();
        //保存工艺图
        bool SaveProcessFlowChart()
        {
            if (null == pFlowVersion)
            {
                XtraMessageBox.Show("请选择制单!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            var lineName = txtProcessChartName.Text?.Trim();
            if (string.IsNullOrEmpty(lineName))
            {
                XtraMessageBox.Show("路线图名称不能为空!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            var chInfo = string.Empty;
            var model = GetProcessFlowChartModel(ref chInfo);
            if (!string.IsNullOrEmpty(chInfo))
            {
                XtraMessageBox.Show(chInfo, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // var action = new Action.ProcessFlowChart.ProcessFlowChartAction();
            chartAction.Model = model;
            var seItem = comboChartList.SelectedItem as SusComboBoxItem;
            if (null == seItem)
            {
                DialogResult dr = XtraMessageBox.Show("确认创建新的工艺图吗?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return false;

                }
            }
            if (null != seItem)
            {
                var editFlowChart = seItem.Tag as DaoModel.ProcessFlowChart;
                model.ProcessFlowChart.Id = editFlowChart.Id;
                // model.ProcessFlowChart =;
                chartAction.UpdateProcessFlowChart();
                // XtraMessageBox.Show("保存成功!", "温馨提示");
                XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
                return true;
            }

            if (chartAction.CheckProcessFlowChartNameIsExist(lineName))
            {
                XtraMessageBox.Show("工艺路线图名称已经存在!", "温馨提示");
                txtProcessChartName.Focus();
                return false;
            }
            chartAction.AddProcessFlowChart();
            // XtraMessageBox.Show("保存成功!", "温馨提示");
            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
            BindFlowChartList(pFlowVersion.Id);
            return true;
        }
        ActionModel.ProcessFlowChartModel GetProcessFlowChartModel(ref string checkInfo)
        {
            DaoModel.ProcessFlowChart processFlowChart = new DaoModel.ProcessFlowChart();
            processFlowChart.LinkName = txtProcessChartName.Text.Trim();
            processFlowChart.Remark = txtProcessChartMemo.Text?.Trim();
            var flowVersion = (txtProcessChartName.Tag as DaoModel.ProcessFlowVersionModel);
            processFlowChart.ProcessFlowVersion = flowVersion == null ? pFlowVersion : flowVersion;
            var num = new ProcessFlowChartAction().GetCurrentMaxProcessFlowChartNo(flowVersion?.Id);
            processFlowChart.PFlowChartNum = Convert.ToInt64(num);
            if (!string.IsNullOrEmpty(txtProcessTargetNum.Text))
            {
                processFlowChart.TargetNum = Convert.ToInt32(txtProcessTargetNum.Text);
            }
            processFlowChart.OutputProcessFlowId = (comboProductOutFlow.SelectedItem as SuspeSys.Client.Modules.Ext.SusComboBoxItem)?.Value?.ToString();
            processFlowChart.BoltProcessFlowId = (comboHangingPieceFlowCode.SelectedItem as SuspeSys.Client.Modules.Ext.SusComboBoxItem)?.Value?.ToString();

            var fcGroupList = new List<DaoModel.ProcessFlowChartGrop>();
            foreach (CheckedListBoxItem gItem in comboProcessGroup.Properties.Items)
            {
                if (gItem.CheckState == CheckState.Checked)
                {
                    fcGroupList.Add(new Domain.ProcessFlowChartGrop()
                    {
                        SiteGroup = (DaoModel.SiteGroup)gItem.Tag,
                        GroupNo = gItem.Value?.ToString(),
                        GroupName = gItem.Description
                    });
                }
            }
            var dt = treeList1.DataSource as List<DaoModel.ProcessFlowChartFlowRelationModel>;
            //var pfcFlowList = new List<DaoModel.ProcessFlowChartFlowRelationModel>();
            var pfcFlowList = dt.Where(q => !q.IsChind && short.Parse(q.CraftFlowNo) > 1).ToList<DaoModel.ProcessFlowChartFlowRelationModel>();
            foreach (var flow in pfcFlowList)
            {
                flow.ProcessFlowChartFlowRelationModelList = dt.Where(q => q.ParentId.Equals(flow.Id)).ToList<DaoModel.ProcessFlowChartFlowRelationModel>();
                if (flow.ProcessFlowChartFlowRelationModelList.Count == 0 && !flow.IsMergeForw)
                {
                    checkInfo = string.Format("工序:【{0}】无站点不能创建工艺图!", flow.FlowName?.Trim());
                    return null;
                }
            }
            //var distinctStatingList = new List<string>();
            //foreach (var li in dt.Where(q => q.IsChind))
            //{

            //    if (distinctStatingList.Contains(li.CraftFlowNo?.Trim()))
            //    {
            //        checkInfo = string.Format("站点【{1}】重复!", li.FlowName?.Trim(), li.CraftFlowNo?.Trim());
            //        return null;
            //    }
            //    if (!distinctStatingList.Contains(li.CraftFlowNo?.Trim()))
            //    {
            //        distinctStatingList.Add(li.CraftFlowNo?.Trim());
            //    }
            //}
            pfcFlowList = dt.Where(q => !q.IsChind).ToList<DaoModel.ProcessFlowChartFlowRelationModel>();
            foreach (var flow in pfcFlowList)
            {
                flow.ProcessFlowChartFlowRelationModelList = dt.Where(q => q.ParentId.Equals(flow.Id)).ToList<DaoModel.ProcessFlowChartFlowRelationModel>();

            }
            ActionModel.ProcessFlowChartModel model = new ActionModel.ProcessFlowChartModel();
            model.ProcessFlowChart = processFlowChart;
            model.ProcessFlowChartFlowRelationList = pfcFlowList;
            model.ProcessFlowChartGropList = fcGroupList;
            return model;
        }
        public SusToolBar ProcessFlowChartSusToolBar { get; internal set; }
        void AddItem()
        {
            txtProcessChartName.Text = string.Empty;
            txtProcessChartMemo.Text = string.Empty;
            txtProcessChartName.Enabled = true;
            comboChartList.SelectedItem = null;
            chartList.Clear();
            treeList1.DataSource = chartList;
            treeList1.Nodes.Clear();
        }
        #endregion

        public ProcessFlowChartIndex ProcessFlowChartIndex { get; internal set; }

        //【工艺路线图】绑定树列头
        private void BindFlowChartTreeListHeader()
        {
            //var gridView = gridControl1.MainView as Ext.SusGridView;
            var assem = this.GetType().Assembly;
            var processStream = SuspeSys.Client.Properties.Resources.zhandian16;//"SuspeSys.Client.Resources.chart.zhandian16.png");
            var siteStream = SuspeSys.Client.Properties.Resources.gongrennan16;
            DevExpress.Utils.ImageCollection imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            imageCollection1.Images.Add(processStream);
            imageCollection1.Images.Add(siteStream);
            imageCollection1.Images.SetKeyName(0, "zhandian16.png");
            imageCollection1.Images.SetKeyName(1, "gongrennan16.png");
            var rItemEnable = new SusRepositoryItemCheckEdit()
            {
                CheckStyle = CheckStyles.Standard,
                GlyphAlignment = DevExpress.Utils.HorzAlignment.Default,//设置显示标题
                Caption = "生效",
                Name="Enable"
            };

            var rItemMergeForward = new SusRepositoryItemCheckEdit()
            {
                CheckStyle = CheckStyles.Standard,
                GlyphAlignment = DevExpress.Utils.HorzAlignment.Default,//设置显示标题
                Caption = "往前合并",
                Name= "MergeForward"
            };

            var columns = new TreeListColumn[]
            {
                new TreeListColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new TreeListColumn() { Caption="ProcessflowIdOrStatingId",FieldName= "ProcessflowIdOrStatingId",Visible=false},
                new TreeListColumn() { FieldName="IsStating",Visible=false},
                new TreeListColumn() { FieldName="IsChind",Visible=false},//区分子父节点
                new TreeListColumn() { Caption="顺序/站号",FieldName="CraftFlowNo",Visible=true,Name="OrderOrStating"},
                new TreeListColumn() { Caption = "生效/接收", FieldName = "IsEn", Visible = true, ColumnEdit = rItemEnable, Fixed = FixedStyle.None,Name="EnableOrReceive" },
                new TreeListColumn() { Caption="合并/顺序",FieldName="IsMergeForw",Visible=true,ColumnEdit=rItemMergeForward,Name="MergeOrOrder"},
                new TreeListColumn() { Caption="工序号/比例",FieldName="FlowNo",Visible=true,Name="FlowNoOrRate"},
                new TreeListColumn() { Caption="工序代码/站点角色",FieldName="FlowCode",Visible=true,Name="FlowCodeOrStatingRole"},
                //new TreeListColumn() { Caption="工序名称/指定颜色尺码",FieldName="FlowName",Visible=true}
                new TreeListColumn() { Caption="工序名称/指定颜色尺码",FieldName="FlowDisplayName",Visible=true,Name="FlowNameOrColorSize"}
            };
            treeList1.Columns.AddRange(columns);
            treeList1.StateImageList = imageCollection1;
            treeList1.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeList1_GetStateImage);
            treeList1.ParentFieldName = "ParentId";
            treeList1.KeyFieldName = "Id";
            treeList1.AllowDrop = true;
            treeList1.CustomDrawNodeCell += TreeList1_CustomDrawNodeCell;
            treeList1.CustomNodeCellEdit += TreeList1_CustomNodeCellEdit;
            //treeList1.CustomDrawNodeIndent
            //treeList1.CustomDrawNodeIndicator

            //treeList1.CustomDrawNodePreview += TreeList1_CustomDrawNodePreview;
            //treeList1.OptionsView.ShowIndentAsRowStyle = true;
            //treeList1.CustomDrawRow +=   
            treeList1.DragDrop += TreeList1_DragDrop;
            treeList1.DragLeave += TreeList1_DragLeave;
            treeList1.DragOver += TreeList1_DragOver;
            treeList1.DragEnter += TreeList1_DragEnter;
            treeList1.GiveFeedback += TreeList1_GiveFeedback;
            treeList1.CellValueChanging += TreeList1_CellValueChanging;
            // treeList1.MouseUp += TreeList1_MouseUp;

            //拖放相关属性
            treeList1.AllowDrop = true;
            treeList1.OptionsBehavior.DragNodes = true;
            treeList1.OptionsBehavior.ShowEditorOnMouseUp = true;


            treeList1.LookAndFeel.UseDefaultLookAndFeel = false;
            treeList1.LookAndFeel.UseWindowsXPTheme = true;
            treeList1.CustomDrawNodeCheckBox += TreeList1_CustomDrawNodeCheckBox;
            // treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.SteelBlue;
            // treeList1.Appearance.Row.BackColor = Color.Aqua;
            treeList1.OptionsView.ShowHorzLines = true;


            //列不可编辑
            var list = new List<string>()
            { "IsEn","IsMergeForw" };
            foreach (TreeListColumn c in treeList1.Columns)
            {
                if (!list.Contains(c.FieldName))
                {
                    c.OptionsColumn.AllowEdit = false;
                }
                //列不可编辑
                //treeList1.Columns["IsEn"].OptionsColumn.AllowEdit = false;
            }
        }

        private void TreeList1_CustomDrawNodePreview(object sender, CustomDrawNodePreviewEventArgs e)
        {
            Brush backBrush = e.Cache.GetGradientBrush(e.Bounds, Color.LightGoldenrodYellow, Color.Cornsilk, LinearGradientMode.Vertical);
            e.Cache.FillRectangle(backBrush, e.Bounds);
            // painting the preview string
            e.Appearance.FontStyleDelta = FontStyle.Italic;
            e.Cache.DrawString(e.PreviewText, e.Appearance.Font, Brushes.Maroon, e.Bounds, e.Appearance.GetStringFormat());
            // prohibiting default painting
            e.Handled = true;
        }

        private void TreeList1_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Equals("IsMergeForw"))
            {
                // MessageBox.Show(e.Value?.ToString());
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    var state = Convert.ToBoolean(e.Value);
                    if (state)
                    {
                        DialogResult dr = XtraMessageBox.Show("合并工序将删除分配给当前工序的站点,当前工序将由前道工序分配的站点加工!", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.No)
                        {
                            return;
                        }

                        var id = e.Node.GetValue("Id");
                        var chart = chartList.Where(o => o.Id == id.ToString()).First();

                        //找出下一个工序，获取id，FlowNo, 记录MergeProcessFlowChartFlowRelationId
                        var preFlow = (int.Parse(chart.CraftFlowNo) - 1).ToString();
                        var preChart = chartList.Where(o => o.CraftFlowNo.Equals(preFlow)).FirstOrDefault();
                        if (preChart == null)
                        {
                            SuspeTool.ShowMessageBox("第一道工序，不能向前合并");
                            return;
                        }

                        var removeChart = new List<DaoModel.ProcessFlowChartFlowRelationModel>();
                        removeChart.AddRange(chartList.Where(o => o.ParentId == id.ToString()));

                        //移除
                        foreach (var item in removeChart)
                        {
                            chartList.Remove(item);
                        }
                        //chartList = chartList.Except(removeChart).ToList();
                        chart.IsMergeForw = state;

                    }
                    else
                    {
                        var id = e.Node.GetValue("Id");
                        var chart = chartList.Where(o => o.Id == id.ToString()).First();
                        chart.IsMergeForw = state;
                        chart.MergeFlowNo = null;
                        chart.MergeProcessFlowChartFlowRelationId = null;
                    }


                    //e.Node.SetValue("IsMergeForw", state);

                    treeList1.RefreshDataSource();
                    treeList1.ExpandAll();

                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }



        private void TreeList1_CustomDrawNodeCheckBox(object sender, CustomDrawNodeCheckBoxEventArgs e)
        {
            //隐藏checkbox
            //if (e.Node.HasChildren)
            //{
            DevExpress.XtraTreeList.ViewInfo.IndentInfo ii = treeList1.ViewInfo.RowsInfo[e.Node].IndentInfo;
            int x2 = e.Bounds.Left + ii.LevelWidth / 2;
            int y2 = e.Bounds.Top + e.Bounds.Height / 2;
            int h2 = e.Bounds.Height / 2 + 1;
            Rectangle r1 = new Rectangle(e.Bounds.Left, y2, e.Bounds.Width, 1);
            Rectangle r2 = new Rectangle(x2, y2, 1, h2);
            e.Graphics.FillRectangle(treeList1.ViewInfo.RC.TreeLineBrush, r1);
            if (e.Node.Expanded)
            {
                e.Graphics.FillRectangle(treeList1.ViewInfo.RC.TreeLineBrush, r2);
            }
            e.Handled = true;
            //}
        }


        #region 【工艺路线图】 拖拽部分
        /*
         如果用户移出一个窗口，则引发 DragLeave 事件。
        如果鼠标进入另一个控件，则引发该控件的 DragEnter。
        如果鼠标移动但停留在同一个控件中，则引发 DragOver 事件
        https://msdn.microsoft.com/zh-cn/library/system.windows.forms.control.givefeedback.aspx
             */
        //private DragDropEffects GetDragDropEffect(TreeList tl, TreeListNode dragNode)
        //{
        //    TreeListNode targetNode;
        //    Point p = tl.PointToClient(MousePosition);
        //    targetNode = tl.CalcHitInfo(p).Node;

        //    if (dragNode != null && targetNode != null
        //        && dragNode != targetNode
        //        && dragNode.ParentNode == targetNode.ParentNode)
        //        return DragDropEffects.Move;
        //    else
        //        return DragDropEffects.None;
        //}
        private void TreeList1_DragEnter(object sender, DragEventArgs e)
        {
            //var d=e.Data.GetData(typeof(SuspeSys.Domain.ProcessFlow));
            //if (null != d)
            //{
            //    var drData = d as SuspeSys.Domain.ProcessFlow;
            //    SusTreeList list = (SusTreeList)sender;
            //    //if (list == node.TreeList) return;
            //    TreeListHitInfo info = list.CalcHitInfo(list.PointToClient(new Point(e.X, e.Y)));
            //    var ne = info.Node;
            //    if (null!=ne) {
            //        var isStating =Convert.ToBoolean(ne.GetValue("IsStating"));
            //        if (!isStating) {
            //            var data = new DaoModel.ProcessFlowChartFlowRelationModel() {
            //                Id=treeList1.Nodes.Count.ToString(),
            //                CraftFlowNo= drData.ProcessNo,
            //                FlowName=drData.ProcessName,
            //                FlowNo=drData.ProcessNo,
            //                FlowCode=drData.ProcessCode
            //            };
            //            treeList1.AppendNode(data, ne);
            //        }
            //    }
            //    e.Effect = DragDropEffects.Copy;
            //}
            e.Effect = DragDropEffects.Copy;
        }

        private void TreeList1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
        }

        private void TreeList1_DragOver(object sender, DragEventArgs e)
        {
            //TreeListNode dragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
            //e.Effect = GetDragDropEffect(sender as TreeList, dragNode);
            DXDragEventArgs args = treeList1.GetDXDragEventArgs(e);
            //if (args.Node == null)
            //{
            //    if (args.HitInfo.HitInfoType == HitInfoType.Empty || args.TargetNode != null)
            //        args.Effect = DragDropEffects.Copy;
            //    else
            //        args.Effect = DragDropEffects.None;
            //}
            e.Effect = DragDropEffects.Copy;
            SetDragCursor(args.Effect);
        }

        private TreeListNode GetDragNode(IDataObject data)
        {
            return data.GetData(typeof(TreeListNode)) as TreeListNode;
        }

        private void TreeList1_DragLeave(object sender, EventArgs e)
        {
            //SetDefaultCursor();
        }
        private void SetDefaultCursor()
        {
            Cursor = Cursors.Default;
        }
        private void SetDragCursor(DragDropEffects e)
        {
            if (e == DragDropEffects.Move)
                Cursor = new Cursor(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SuspeSys.Client.Images.move.ico"));
            if (e == DragDropEffects.Copy)
                Cursor = new Cursor(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SuspeSys.Client.Images.copy.ico"));
            if (e == DragDropEffects.None)
                Cursor = Cursors.No;
        }
        //private void InsertBrush(TreeList list, TreeListNode node, int parent)
        //{
        //    //ArrayList data = new ArrayList();
        //    //foreach (TreeListColumn column in node.TreeList.Columns)
        //    //{
        //    //    data.Add(node[column]);
        //    //}
        //    //parent = list.AppendNode(data.ToArray(), parent).Id;

        //    //if (node.HasChildren)
        //    //    foreach (TreeListNode n in node.Nodes)
        //    //        InsertBrush(list, n, parent);
        //}
        #region 拖动，处理工序
        /// <summary>
        /// 拖动，处理工序
        /// </summary>
        private bool TreeListDragDropProcessFlow(TreeListHitInfo info, DaoModel.ProcessFlow d, object sender, DragEventArgs e, bool isDownUpDrop = false)
        {
            //工序处理
            var ne = info.Node;
            if (null == ne)
            {
                ///拖入的节点
                var drData = d as SuspeSys.Domain.ProcessFlow;
                var isContains = chartList.Where(q => q.ProcessflowIdOrStatingId != null).Where(q => q.ProcessflowIdOrStatingId.Equals(drData.Id) && !q.IsChind).ToList<DaoModel.ProcessFlowChartFlowRelationModel>().Count > 0;
                if (isContains)
                {
                    XtraMessageBox.Show("工序已包含,不能重复添加！", "温馨提示");
                    return false;
                }
                if (chartList.Count == 0)
                {
                    DialogResult dr = XtraMessageBox.Show(string.Format("工艺图为空，挂片工序应当被最先加入到方案中,工序{0},{1}作为挂片工序吗?", drData.DefaultFlowNo, drData.ProcessCode?.Trim()),
                        "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                    {
                        return false;

                    }
                }
                var flowIndex = chartList.Where(f => !f.IsChind).ToList<DaoModel.ProcessFlowChartFlowRelationModel>().Count;
                var data = new DaoModel.ProcessFlowChartFlowRelationModel()
                {
                    Id = (chartList.Count + 1).ToString(),
                    ProcessflowIdOrStatingId = drData.Id,
                    ParentId = "0",
                    CraftFlowNo = (flowIndex + 1).ToString(),
                    FlowName = drData.ProcessName,
                    FlowNo = drData.DefaultFlowNo.ToString(),
                    FlowCode = drData.ProcessCode,
                    ProcessFlow = drData,
                    IsStating = false,
                    IsEn = true,
                    IsChind = false,
                    IsAcceHanger = true,
                };
                //var data = new DaoModel.ProcessFlowChartFlowRelationModel() { Id = (chartList.Count + 1).ToString(), ParentId = "0", CraftFlowNo = "1", IsEn = false, IsMergeForw = false, IsStating = false, FlowName = "工序1", FlowNo = "1", FlowCode = "工序1101" };
                //treeList1.AppendNode(data, 0);
                chartList.Add(data);
                treeList1.RefreshDataSource();
                treeList1.ExpandAll();
                //}
            }
            else
            {
                #region 插入工序
                //插入工序
                var drData = d as SuspeSys.Domain.ProcessFlow;
                var isContains = chartList.Where(q => q.ProcessflowIdOrStatingId != null).Where(q => q.ProcessflowIdOrStatingId.Equals(drData.Id) && !q.IsChind).ToList<DaoModel.ProcessFlowChartFlowRelationModel>().Count > 0;
                if (isContains)
                {
                    XtraMessageBox.Show("工序已包含,不能重复添加！", "温馨提示");
                    return false;
                }

                if (chartList.Count == 0)
                {
                    DialogResult dr = XtraMessageBox.Show(string.Format("工艺图为空，挂片工序应当被最先加入到方案中,工序{0},{1}作为挂片工序吗?", drData.DefaultFlowNo, drData.ProcessCode?.Trim()),
                        "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                    {
                        return false;

                    }
                }

                DXDragEventArgs dxEvent = e.GetDXDragEventArgs(sender as TreeList);
                //拖到哪个节点上，即:被影响节点
                var targetNode = dxEvent.TargetNode;
                var postion = dxEvent.DragInsertPosition;
                var drapNode = ne;

                ////控制拖入子节点不生效
                //if (postion == DragInsertPosition.AsChild || postion == DragInsertPosition.None)
                //    return false;

                //获取TargetNode 信息
                var isChidNodeArea = (bool)targetNode.GetValue("IsChind");
                //拖入站点区域
                if (isChidNodeArea)
                {
                    var parentId = targetNode.GetValue("ParentId");
                    int index1 = chartList.IndexOf(chartList.Where(o => o.IsChind == false && o.Id.Equals(parentId)).First());
                    var data1 = new DaoModel.ProcessFlowChartFlowRelationModel()
                    {
                        Id = (chartList.Count + 1).ToString(),
                        ProcessflowIdOrStatingId = drData.Id,
                        ParentId = "0",//ProcessFlowChartFlowRelationId
                        //CraftFlowNo = (flowIndex + 1).ToString(),
                        FlowName = drData.ProcessName,
                        FlowNo = drData.DefaultFlowNo.ToString(),
                        FlowCode = drData.ProcessCode,
                        ProcessFlow = drData,
                        IsStating = false,
                        IsEn = true,
                        IsChind = false,
                        IsAcceHanger = true,
                    };
                    chartList.Insert(index1 + 1, data1);
                    RepaidFlowChart();
                    return true;
                }
                var sFlowNo = targetNode.GetValue("CraftFlowNo");
                int index = chartList.IndexOf(chartList.Where(o => o.IsChind == false && o.CraftFlowNo.Equals(sFlowNo)).First());

                if (index == 0 && postion == DragInsertPosition.Before)
                    return false;

                #region 构建实体类
                var data = new DaoModel.ProcessFlowChartFlowRelationModel()
                {
                    Id = (chartList.Count + 1).ToString(),
                    ProcessflowIdOrStatingId = drData.Id,
                    ParentId = "0",
                    //CraftFlowNo = (flowIndex + 1).ToString(),
                    FlowName = drData.ProcessName,
                    FlowNo = drData.DefaultFlowNo.ToString(),
                    FlowCode = drData.ProcessCode,
                    ProcessFlow = drData,
                    IsStating = false,
                    IsEn = true,
                    IsChind = false,
                    IsAcceHanger = true,
                };
                #endregion

                ////if (postion == DragInsertPosition.After)
                ////    chartList.Insert(index + 1, data);
                ////else if (postion == DragInsertPosition.Before)
                //    chartList.Insert(index, data);
                chartList.Insert(index + 1, data);
                RepaidFlowChart();

                #endregion
            }

            return true;
        }
        void RepaidFlowChart()
        {
            //重新刷新序号
            int refreshIndex = 1;
            foreach (var item in chartList)
            {
                if (!item.IsChind)
                {

                    item.CraftFlowNo = refreshIndex.ToString();
                    refreshIndex++;
                }
            }

            treeList1.RefreshDataSource();
            treeList1.ExpandAll();
        }
        #endregion


        private void TreeList1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {

                SusTreeList list = (SusTreeList)sender;
                //if (list == node.TreeList) return;
                TreeListHitInfo info = list.CalcHitInfo(list.PointToClient(new Point(e.X, e.Y)));
                TreeListNode node = GetDragNode(e.Data);
                if (node == null)
                {
                    var d = e.Data.GetData(typeof(SuspeSys.Domain.ProcessFlow));
                    //数据来源于基本工序
                    if (null != d)
                    {
                        //处理工序

                        bool canDrag = this.TreeListDragDropProcessFlow(info, (SuspeSys.Domain.ProcessFlow)d, sender, e);
                        if (!canDrag)
                            return;
                    }
                    //来源于站点组
                    var dSite = e.Data.GetData(typeof(DaoModel.StatingModel)) as DaoModel.StatingModel;
                    if (null != dSite)
                    {
                        var ne = info.Node;
                        if (null == ne)
                        {
                            if (chartList.Count == 0)
                            {
                                XtraMessageBox.Show("请先分配工序,工序分完后才能给工序分配站点！", "温馨提示");
                                return;
                            }
                            //if (null!= editStating) {

                            //    editStating = null;
                            //}
                            XtraMessageBox.Show("请将站点拖动到要分配的目标工序节点上！", "温馨提示");
                            return;
                        }
                        var aId = ne.GetValue("Id");

                        if (null != ne)
                        {
                            //如果拖入节点的释放点是否是子节点
                            var isChildNode = Convert.ToBoolean(ne.GetValue("IsChind"));
                            if (isChildNode)
                            {
                                var parentId = ne?.ParentNode?.GetValue("Id");
                                var sizeCount = chartList.Where(f => f.ParentId.Equals(parentId)).Where(f => f.ProcessflowIdOrStatingId.Equals(dSite.Id)).Count();
                                if (0 < sizeCount)
                                {
                                    XtraMessageBox.Show("站位已包含,不能重复添加！", "温馨提示");
                                    return;
                                }
                            }
                            var IsMergeForw = ne.GetValue("IsMergeForw");
                            if (Convert.ToBoolean(IsMergeForw))
                            {
                                SuspeTool.ShowMessageBox("合并工序不能添加站点");
                                return;
                            }
                        }

                        var isContains = chartList.Where(q => q.ProcessflowIdOrStatingId != null).Where(q => q.ProcessflowIdOrStatingId.Equals(dSite.Id) && q.ParentId.Equals(aId)).ToList<DaoModel.ProcessFlowChartFlowRelationModel>().Count > 0;
                        if (isContains)
                        {
                            XtraMessageBox.Show("站位已包含,不能重复添加！", "温馨提示");
                            return;
                        }
                        if (null != ne)
                        {
                            var isStating = Convert.ToBoolean(ne.GetValue("IsStating"));
                            var isChind = Convert.ToBoolean(ne.GetValue("IsChind"));
                            DaoModel.ProcessFlowChartFlowRelationModel data = null;
                            //var isStating = Convert.ToBoolean(ne.GetValue("IsStating"));
                            if (isStating)//如果是鼠标释放区域是站点节点，则增加到该节点的父亲节点下
                            {
                                data = new DaoModel.ProcessFlowChartFlowRelationModel()
                                {
                                    Id = (chartList.Count + 1).ToString(),
                                    ProcessflowIdOrStatingId = dSite.Id,
                                    ParentId = ne.GetValue("ParentId")?.ToString(),
                                    CraftFlowNo = string.Format("{0}-{1}", dSite.GroupNO?.Trim(), dSite.StatingNo?.Trim()),
                                    StatingNo = dSite.StatingNo?.Trim(),
                                    GroupNo = dSite.GroupNO?.Trim(),
                                    // FlowName = dSite.RoleName,
                                    //FlowNo = dSite.StatingNo,
                                    FlowCode = dSite.RoleName,//站点角色
                                    IsStating = true,
                                    IsEn = true,
                                    IsChind = true
                                };
                                chartList.Add(data);
                                treeList1.RefreshDataSource();
                                treeList1.ExpandAll();
                                return;
                            }

                            data = new DaoModel.ProcessFlowChartFlowRelationModel()
                            {
                                Id = (chartList.Count + 1).ToString(),
                                ProcessflowIdOrStatingId = dSite.Id,
                                ParentId = ne.GetValue("Id")?.ToString(),
                                CraftFlowNo = string.Format("{0}-{1}", dSite.GroupNO?.Trim(), dSite.StatingNo?.Trim()),
                                StatingNo = dSite.StatingNo?.Trim(),
                                GroupNo = dSite.GroupNO?.Trim(),
                                // FlowName = dSite.RoleName,
                                // FlowNo = dSite.StatingNo,
                                FlowCode = dSite.RoleName,//站点角色
                                IsStating = true,
                                IsEn = true,
                                IsChind = true
                            };

                            chartList.Add(data);
                            treeList1.RefreshDataSource();
                            treeList1.ExpandAll();
                        }
                    }
                    return;
                }

                //内部拖动//上下拖动工序
                //如果是父节点
                if (!Convert.ToBoolean(node.GetValue("IsStating")))
                {
                    //上下拖动工序
                    bool canDrag = UpDownDragDropFlow(node, info.Node);
                    if (!canDrag)
                        return;
                    e.Effect = DragDropEffects.None;
                    return;
                }
                //同级不处理
                if (info.Node == null || Convert.ToBoolean(info.Node.GetValue("IsStating")) == Convert.ToBoolean(node.GetValue("IsStating")))
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
            }
            finally
            {
                SetDefaultCursor();
                BindHangingPieceFlowCode();
                BindProductOutFlowCode();
            }
        }

        private bool UpDownDragDropFlow(TreeListNode sourceNode, TreeListNode tagetNode)
        {
            var sourceProcessFlowChartFlowRelationId = sourceNode.GetValue("ProcessflowIdOrStatingId");
            var targetProcessFlowChartFlowRelationId = tagetNode.GetValue("ProcessflowIdOrStatingId");
            if (null == sourceProcessFlowChartFlowRelationId || null == targetProcessFlowChartFlowRelationId)
            {
                return false;
            }
            var sourceIndex = chartList.IndexOf(chartList.Where(f => f.ProcessflowIdOrStatingId.Equals(sourceProcessFlowChartFlowRelationId)).First());
            var targertIndex = chartList.IndexOf(chartList.Where(f => f.ProcessflowIdOrStatingId.Equals(targetProcessFlowChartFlowRelationId)).First());
            var cpSourceProcessFlowChartFlowRelation = BeanUitls<DaoModel.ProcessFlowChartFlowRelationModel, DaoModel.ProcessFlowChartFlowRelationModel>.Mapper(chartList[sourceIndex]);
            var cpTargetProcessFlowChartFlowRelation = BeanUitls<DaoModel.ProcessFlowChartFlowRelationModel, DaoModel.ProcessFlowChartFlowRelationModel>.Mapper(chartList[targertIndex]);
            chartList.RemoveAt(sourceIndex);
            if (chartList.Count == targertIndex)
            {
                chartList.Insert(targertIndex, cpSourceProcessFlowChartFlowRelation);
            }
            else
            {
                chartList.Insert(targertIndex + 1, cpSourceProcessFlowChartFlowRelation);
            }
            //  chartList[sourceIndex] = cpTargetProcessFlowChartFlowRelation;
            RepaidFlowChart();
            return false;
        }
        //鼠标进入 GridView时发生
        private void GridControl2GridView_MouseMove(object sender, MouseEventArgs e)
        {
            downHitInfo = (sender as SusGridView).CalcHitInfo(new Point(e.X, e.Y));
            //鼠标左键按下去时在GridView中的坐标

            // throw new NotImplementedException();
        }
        //鼠标进入GridView并按下时放生
        private void GridControl2GridView_MouseDown(object sender, MouseEventArgs e)
        {
            var gv = (sender as SusGridView);
            downHitInfo = gv.CalcHitInfo(new Point(e.X, e.Y));
            var d = gv.GetRow(downHitInfo.RowHandle);
            //gridControl2.DoDragDrop(d, DragDropEffects.Move);//开始拖放操作，将拖拽的数据存储起来
            if (d == null) return;
            treeList1.DoDragDrop(d, DragDropEffects.Copy);//开始拖放操作
        }

        //GridControl 的数据拖放到TreeList,只拖放数据两个事件就可以搞定
        //1. GridControl2GridView_MouseDown
        //2.TreeList1_DragDrop
        private void GridControl3GridView_MouseDown(object sender, MouseEventArgs e)
        {
            var gv = (sender as SusGridView);
            downHitInfo = gv.CalcHitInfo(new Point(e.X, e.Y));
            var d = gv.GetRow(downHitInfo.RowHandle);
            //gridControl2.DoDragDrop(d, DragDropEffects.Move);//开始拖放操作，将拖拽的数据存储起来
            if (d == null) return;
            treeList1.DoDragDrop(d, DragDropEffects.Copy);//开始拖放操作
        }
        #endregion


        //【工艺路线图】数据绑定时动态修改单元格内容
        private void TreeList1_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {
            if (e.Column.FieldName != "Id")
            {

                if (e.Column.FieldName.Equals("IsEn"))
                {
                    //object obj = e.Node.GetValue(0);
                    var tt = e.Node.GetValue("IsStating");
                    if (null != tt && Convert.ToBoolean(tt))//(tt.ToString().Equals("1") || tt.ToString().Equals("2")))
                    {

                        e.RepositoryItem = new SusRepositoryItemCheckEdit()
                        {
                            CheckStyle = CheckStyles.Standard,
                            GlyphAlignment = DevExpress.Utils.HorzAlignment.Default,//设置显示标题
                            Caption = "接收衣架",
                            Name= "ReceiveHanger"
                        };

                    }

                }
                if (e.Column.FieldName.Equals("IsMergeForw"))
                {
                    //object obj = e.Node.GetValue(0);
                    var tt = e.Node.GetValue("IsStating");
                    if (null != tt && Convert.ToBoolean(tt))//(tt.ToString().Equals("1") || tt.ToString().Equals("2")))
                    {
                        var txt = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
                        txt.TextEditStyle = TextEditStyles.HideTextEditor;
                        // txt.
                        e.RepositoryItem = txt;
                        // txt.OwnerEdit.Text = "111";
                        //e.RepositoryItem = txt;
                    }

                }
            }
        }

        private void TreeList1_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            //return;
            /*
            Brush backBrush, foreBrush;
            if (e.Node != (sender as TreeList).FocusedNode)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.PapayaWhip, Color.PeachPuff, LinearGradientMode.ForwardDiagonal);
                foreBrush = Brushes.Black;
            }
            else
            {
                backBrush = Brushes.DarkBlue;
                foreBrush = new SolidBrush(Color.PeachPuff);
            }
            // Fill the background.
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            // Paint the node value.
            e.Graphics.DrawString(e.CellText, e.Appearance.Font, foreBrush, e.Bounds,
              e.Appearance.GetStringFormat());
           

            // Prohibit default painting.
            e.Handled = true;

       */

            if (e.Node.Selected)//如果该节点被选中，不改变颜色
            {
                Graphics g = e.Graphics;   //实例化Graphics 对象g
                Color FColor = Color.White; //颜色1
                Color TColor = Color.Red;  //颜色2 
                Brush b = new LinearGradientBrush(e.Bounds, FColor, TColor, LinearGradientMode.Vertical);  //实例化刷子，第一个参数指示上色区域，第二个和第三个参数分别渐变颜色的开始和结束，第四个参数表示颜色的方向。
                g.FillRectangle(b, e.Bounds);  //进行上色

                e.Appearance.BackColor = Color.DarkViolet;
                e.Appearance.BorderColor = Color.DarkBlue;
                e.Appearance.Options.UseBorderColor = true;
                //Graphics graphics = new Graphics();
                //e.Appearance.DrawBackground(new GraphicsCache(g), e.Bounds);
                return;
            }
            // 判断条件
            if (e.Node.Level == 0)
            {
                bool isEnable = Convert.ToBoolean(e.Node.GetValue("IsEn"));
                bool IsMergeForw = Convert.ToBoolean(e.Node.GetValue("IsMergeForw"));

                if (isEnable && !IsMergeForw)
                {
                    //Graphics g = e.Graphics;   //实例化Graphics 对象g
                    //Color FColor = Color.White; //颜色1
                    //Color TColor = Color.Red;  //颜色2 
                    //Brush b = new LinearGradientBrush(e.Bounds, FColor, TColor, LinearGradientMode.Vertical);  //实例化刷子，第一个参数指示上色区域，第二个和第三个参数分别渐变颜色的开始和结束，第四个参数表示颜色的方向。
                    //g.FillRectangle(b, e.Bounds);  //进行上色

                    //e.Appearance.DrawBackground(new GraphicsCache(g), e.Bounds);
                    //e.Appearance.Options.UseBackColor = true;
                    //e.Handled = true;
                    e.Appearance.BackColor = Color.FromArgb(91, 155, 213);
                    e.Appearance.BorderColor = Color.DarkGray;
                    e.Appearance.Options.UseBorderColor = true;
                }
                else if (isEnable && IsMergeForw)
                {
                    e.Appearance.BackColor = Color.Chartreuse;
                    e.Appearance.BorderColor = Color.DarkGray;
                    e.Appearance.Options.UseBorderColor = true;
                    //e.Appearance.BackColor2 = Color.White;
                }
                else
                {
                    // 更改颜色
                    e.Appearance.BackColor = Color.Gray;
                    //e.Appearance.BackColor = Color.FromArgb(91,155,213);
                    //e.Appearance.BackColor2 = Color.White;
                    e.Appearance.BorderColor = Color.DarkGray;
                    e.Appearance.Options.UseBorderColor = true;
                }

            }
            else
            {
                bool isEnable = Convert.ToBoolean(e.Node.GetValue("IsEn"));
                if (isEnable == false)
                {
                    e.Appearance.BackColor = Color.Gray;
                    //e.Appearance.BackColor = Color.FromArgb(91,155,213);
                    //e.Appearance.BackColor2 = Color.White;
                    e.Appearance.BorderColor = Color.DarkGray;
                    e.Appearance.Options.UseBorderColor = true;
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(230, 150, 150);

                    e.Appearance.BorderColor = Color.DarkGray;
                }

                // var bCo = 0xFF3399cc;
                // var eCo = 0xFFEDEDED;
                //e.Appearance.BackColor = Color.FromArgb((int)bCo);
                // e.Appearance.BackColor2 = Color.FromArgb((int)eCo);
            }


            //if (Convert.ToBoolean(e.Node.GetValue("IsMergeForw")))
            //{
            //    e.Appearance.BackColor = Color.Chartreuse;
            //    e.Appearance.BorderColor = Color.DarkGray;
            //    //e.Appearance.BackColor2 = Color.White;
            //}

            //if (e.Column.FieldName == "IsMergeForw")
            //{

            //     bool IsMergeForw = Convert.ToBoolean(e.Node.GetValue("IsMergeForw"));
            //    if (IsMergeForw == true)
            //    {


            //    }
            //}
            //var id = e.Node.Id;
            //var t = e.Column.ColumnEdit as SusRepositoryItemCheckEdit;
            //if (null != t && id==3)
            //{
            //   // XtraMessageBox.Show(e.Node.Id.ToString());
            //    t.Caption = "接收衣架";
            //}
            ////if (e.Node) {

            ////}
            //e.Handled = true;
        }

        string IsStating;
        //【工艺路线图】修改节点图标
        private void treeList1_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            // string[] groupNames = new string[] { "zhandian16", "gongrennan16" };
            IsStating = e.Node.GetValue("IsStating")?.ToString();
            if (IsStating == null) return;
            if (!Convert.ToBoolean(IsStating))
            {
                e.NodeImageIndex = 0; //Array.FindIndex(groupNames, new Predicate<string>(IsCurrentGroupName));
            }
            else
            {
                e.NodeImageIndex = 1; //Array.FindIndex(groupNames, new Predicate<string>(IsCurrentGroupName));
            }
        }
        // private bool IsCurrentGroupName(string groupName) { return currentGroupName.Contains(groupName); }

        //制单工序明细
        private void BindProcessFlowGridHeader()
        {
            var gridView = gridControl2.MainView as Ext.SusGridView;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序段",FieldName="ProSectionName",Visible=true,Name="ProSectionName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="排序字段",FieldName="ProcessOrderField",Visible=true,Name="ProcessOrderField"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序号",FieldName="DefaultFlowNo",Visible=true,Name="DefaultFlowNo"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序代码",FieldName="ProcessCode",Visible=true,Name="ProcessCode"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工序名称",FieldName="ProcessName",Visible=true,Name="ProcessName"}//,
                //  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工时(SAM)",FieldName="StanardHours",Visible=true,Name="SAM"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="标准工价(元)",FieldName="StandardPrice",Visible=true,Name="StandardPrice"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="工艺指导说明",FieldName="PrcocessRmark",Visible=true,Name="PrcocessRmark"}
               // new DevExpress.XtraGrid.Columns.GridColumn() { Caption="录入时间",FieldName="InsertDateTime",Visible=true}
            });
            gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gridControl2.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            //  gridView.BestFitColumns();//按照列宽度自动适配
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.GridControl = gridControl2;

            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsBehavior.Editable = false;
            gridControl2.AllowDrop = true;
            gridView.MouseDown += GridControl2GridView_MouseDown;
            gridView.MouseMove += GridControl2GridView_MouseMove;
            gridView.OptionsBehavior.Editable = false;
            // RegisterGridViewEvent(gridView);
        }

        GridHitInfo downHitInfo = null;
        //站点组
        private void BindSiteGridHeader()
        {
            //var gv=gridControl1.MainView as GridView;
            //gv.Columns.Clear();
            //gridControl1.ViewCollection.Clear();

            //gridControl1.MainView.PopulateColumns();
            var gridView = gridControl3.MainView as Ext.SusGridView;

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                  new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站点",FieldName="StatingNo",Visible=true,Name="StatingName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="角色",FieldName="RoleName",Visible=true,Name="RoleName"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组别",FieldName="GroupNO",Visible=true,Name="GroupNO"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="站号",FieldName="StatingNo",Visible=true,Name="StatingNo"}
            });
            gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gridControl3.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            //  gridView.BestFitColumns();//按照列宽度自动适配
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.GridControl = gridControl3;

            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;

            gridControl3.AllowDrop = true;
            gridView.MouseDown += GridControl3GridView_MouseDown;
            gridView.OptionsBehavior.Editable = false;
        }
        private List<string> _POList;
        public void BindProductProcessFlowChartData(DaoModel.ProcessOrder pOrder, DaoModel.ProductsModel productModel, DaoModel.ProcessFlowVersionModel pfVersionModel, DaoModel.ProcessFlowChart pfChart)
        {

            processOrderId = pOrder.Id;
            //ProcessOrder = pOrder;
            txtProcessOrderNo.Text = pOrder?.POrderNo?.Trim();
            txtProcessOrderStyleNo.Text = pOrder?.StyleCode?.Trim();
            txtProcessOrderStyleDesption.Text = pOrder?.StyleName?.Trim();
            txtProcessOrderNo.Enabled = false;
            txtProcessOrderStyleNo.Enabled = false;
            txtProcessOrderStyleDesption.Enabled = false;

            _POList = new ProcessFlowChartAction().GetPoList(pfVersionModel.Id);
            colorList = chartAction.GetProcessOrderColorList(processOrderId, ref sizeList);

            var action = new ProcessOrderQueryAction();
            var list = action.GetProcessOrderFlowList(pfVersionModel.Id);
            gridControl2.DataSource = list;
            txtProcessChartName.Text = pfChart.LinkName?.Trim();
            BindFlowChartList(pfVersionModel.Id);
            BindProductsFlowChartList(productModel);
            //chartList.Clear();
            //treeList1.DataSource = chartList;
            //treeList1.Nodes.Clear();
            //treeList1.Refresh();
        }
        void BindProductsFlowChartList(DaoModel.ProductsModel productModel)
        {
            if (null != productModel)
            {
                var items = comboChartList.Properties.Items;
                foreach (SusComboBoxItem item in items)
                {
                    if (item.Value.Equals(productModel.ProcessFlowChartId))
                    {
                        comboChartList.SelectedItem = item;
                        break;
                    }
                }
                pFlowVersion = new ProcessFlowChartAction().GetProcessFlowVerson(productModel.ProcessFlowChartId);
            }
            else
            {
                comboChartList.SelectedIndex = 0;
            }
        }
        string processOrderId;
        IList<DaoModel.ProcessOrderColorItem> colorList;
        IList<string> sizeList = null;
        //选择工序版本绑定工序版本及工艺路线图数据
        DaoModel.ProcessFlowVersion pFlowVersion;
        public void BindProcessFlowData(SuspeSys.Domain.ProcessFlowVersionModel pFlowVersion)
        {

            if (null != pFlowVersion)
            {
                _POList = new ProcessFlowChartAction().GetPoList(pFlowVersion.Id);

                processOrderId = pFlowVersion.ProcessOrderId;
                colorList = chartAction.GetProcessOrderColorList(processOrderId, ref sizeList);
                this.pFlowVersion = pFlowVersion;
                txtProcessOrderNo.Text = pFlowVersion?.POrderNo?.Trim();
                txtProcessOrderStyleNo.Text = pFlowVersion?.StyleCode?.Trim();
                txtProcessOrderStyleDesption.Text = pFlowVersion?.StyleName?.Trim();
                txtProcessOrderNo.Enabled = false;
                txtProcessOrderStyleNo.Enabled = false;
                txtProcessOrderStyleDesption.Enabled = false;
                var action = new ProcessOrderQueryAction();
                var list = action.GetProcessOrderFlowList(pFlowVersion.Id);
                gridControl2.DataSource = list;
                txtProcessChartName.Enabled = true;
                txtProcessChartName.Text = string.Empty;

                //生成工艺图单号
                txtProcessChartName.Tag = pFlowVersion;
                var num = new ProcessFlowChartAction().GetCurrentMaxProcessFlowChartNo(pFlowVersion.Id);
                txtProcessChartName.Text = string.Format("{0}_{1}_{2}", pFlowVersion?.POrderNo?.Trim(), pFlowVersion.ProVersionNo.Trim(), DateTime.Now.ToString("yyMMdd") + "-" + num.ToString());
                // txtProcessChartName.Text = string.Format("{0}_{1}_{2}", pFlowVersion?.POrderNo?.Trim(), pFlowVersion.ProVersionNo.Trim(), DateTime.Now.ToString("yyMMdd"));

                chartList.Clear();
                treeList1.DataSource = chartList;
                treeList1.Nodes.Clear();
                treeList1.Refresh();

                //绑定已有工艺图列表
                BindFlowChartList(pFlowVersion.Id);


            }
        }
        //绑定站点组下拉框
        public void BindSiteGroup()
        {
            var queryAction = new CommonQueryAction<DaoModel.SiteGroup>();
            queryAction.GetList();
            var list = queryAction.Model.List;
            comboProcessGroup.Properties.Items.Clear();
            foreach (var m in list)
            {
                var cb = new CheckedListBoxItem(m.Id, m.GroupNo, m);
                comboProcessGroup.Properties.Items.Add(cb);
            }
            /*
             * 另一种实现办法
            comboProcessGroup.Properties.DataSource = list;
            comboProcessGroup.Properties.DisplayMember = "GroupNo";
            comboProcessGroup.Properties.ValueMember = "Id";
            comboProcessGroup.Properties.SeparatorChar = ',';
            */
            //var groupSerive = new SuspeSys.Client.Action.Common.CommonAction();
        }

        IList<DaoModel.StatingModel> statingGroupList = new List<DaoModel.StatingModel>();
        public void BindSiteData()
        {
            var cbs = comboProcessGroup.EditValue?.ToString();
            if (!string.IsNullOrEmpty(cbs))
            {
                var list = new ProcessOrderQueryAction().GetGroupStatingList(cbs?.ToString());
                gridControl3.DataSource = list;
                //foreach (var lt in list) {
                //    statingGroupList.Add(lt);
                //}
            }
            //if (null != cbs)
            //{
            //    var groupList = cbs.ToString().Split(',').ToList<string>();
            //    //var commonAction = new CommonQueryAction<DaoModel.Stating>();
            //    //commonAction.GetList();
            //    var listSite = new CommonQueryAction<DaoModel.Stating>().GetAllList();
            //    var roleList = new CommonQueryAction<DaoModel.StatingRoles>().GetAllList();
            //    var rslt = new List<DaoModel.StatingModel>();
            //    if (null == groupList)
            //    {
            //        foreach (var g in groupList)
            //        {
            //            var res = listSite.Where(q => q.SiteGroup.Id.Equals(g));
            //            foreach (var s in res)
            //            {
            //               var t= roleList.Where(qq=>qq.Stating.Id.Equals(s.Id));
            //                //t.Select();
            //            }
            //            //rslt.AddRange(res.ToArray<DaoModel.StatingModel>());
            //        }
            //    }
            //}
            //var list2=list.Select(s=>new {s.Id,    teacher_name=s.teacher.Name});    //转为匿名
        }
        //转byte[]
        public byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }

        IList<DaoModel.ProcessFlowChartFlowRelationModel> chartList = new List<DaoModel.ProcessFlowChartFlowRelationModel>();
        public void BindFlowChartData()
        {
            //var bp = SuspeSys.Client.Properties.Resources.gongrennan16;

            //var model = new DaoModel.ProcessFlowChartFlowRelationModel() { Id = "1", ParentId = "0", CraftFlowNo = "1", IsEn = false, IsMergeForw = false, IsStating = false, FlowName = "工序1", FlowNo = "1", FlowCode = "工序001", IsChind = false };
            //var model1 = new DaoModel.ProcessFlowChartFlowRelationModel() { Id = "2", ParentId = "0", CraftFlowNo = "2", IsEn = true, IsMergeForw = true, IsStating = false, IsEnabled = 2, FlowName = "工序2", FlowNo = "2", FlowCode = "工序002", IsChind = false };
            //var model2 = new DaoModel.ProcessFlowChartFlowRelationModel() { Id = "3", ParentId = "1", CraftFlowNo = "3", IsEn = true, IsStating = true, FlowNo = "1-1", FlowCode = "角色1", IsChind = true };
            //var model3 = new DaoModel.ProcessFlowChartFlowRelationModel() { Id = "4", ParentId = "1", CraftFlowNo = "4", IsEn = false, IsStating = true, FlowNo = "1-2", FlowCode = "角色2", IsChind = true };
            //var model4 = new DaoModel.ProcessFlowChartFlowRelationModel() { Id = "5", ParentId = "2", CraftFlowNo = "2-1", IsEn = false, IsStating = true, FlowNo = "2-1", FlowCode = "角色3", IsChind = true };
            //var model5 = new DaoModel.ProcessFlowChartFlowRelationModel() { Id = "6", ParentId = "2", CraftFlowNo = "2-2", IsEn = false, IsStating = true, FlowNo = "2-2", FlowCode = "角色4", IsChind = true };
            //chartList = new List<DaoModel.ProcessFlowChartFlowRelationModel>() { model, model1, model2, model3, model4, model5 };
            treeList1.DataSource = chartList;
            ////treeList1.ForceInitialize();
            //treeList1.ExpandAll();
            //treeList1.BestFitColumns();
            //treeList1.OptionsView.ShowCheckBoxes = true;
        }
        private void ProcessFlowChartMain_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                BindSiteGroup();
                BindFlowChartData();
                RegisterToolButtonEvent();
            }
        }

        //站点组下拉框数据绑定
        private void comboProcessGroup_EditValueChanged(object sender, EventArgs e)
        {
            BindSiteData();

        }

        //绑定工艺图版本列表
        private void BindFlowChartList(string flowVersionId)
        {
            var action = new ProcessFlowChartQueryAction();
            var list = action.GetProcessFlowChartList(flowVersionId);
            comboChartList.Properties.Items.Clear();
            foreach (var item in list)
            {
                comboChartList.Properties.Items.Add(new SusComboBoxItem() { Text = item.LinkName?.Trim(), Value = item.Id, Tag = item });
            }
            comboChartList.SelectedIndex = 0;
            //comboChartList.SelectedItem = null;
        }

        //绑定工艺图路线明细
        private void BindFlowChartLineItem(string flowChartId)
        {
            var action = new ProcessFlowChartQueryAction();
            chartList = action.GetProcessFlowChartLineItem(flowChartId);
            if(chartList.Count()>0)
                chartList=chartList.OrderBy(f=>f.IsStating? -1:int.Parse(f.CraftFlowNo)).ToList();
            treeList1.DataSource = chartList;
            treeList1.ExpandAll();

            var cbList = new List<string>();
            var cbTextList = new List<string>();
            var groupList = action.GetProcessFlowChartGroupList(flowChartId);
            (gridControl3.MainView as GridView).ClearRows();
            foreach (CheckedListBoxItem cb in comboProcessGroup.Properties.Items)
            {
                //var pfcG = cb.Tag as DaoModel.SiteGroup;
                //if (null != groupList.Where(q => q.SiteGroup.Id.Equals(pfcG.Id)).SingleOrDefault<DaoModel.ProcessFlowChartGrop>())
                //{
                //    //cb.CheckState = CheckState.Checked;
                //    //sbCheckValue.AppendFormat("{0}");
                //    cbList.Add(pfcG.Id);
                //    cbTextList.Add(pfcG.GroupNo);
                //}
                var g = groupList.Where(f => f.SiteGroup.Id.Equals(cb.Value.ToString())).ToList<DaoModel.ProcessFlowChartGrop>().Count > 0;
                if (g)
                {
                    cb.CheckState = CheckState.Checked;

                }
                else
                {
                    cb.CheckState = CheckState.Unchecked;
                }
            }
            //if (comboProcessGroup.OldEditValue != null && null != comboProcessGroup.EditValue
            //    && comboProcessGroup.OldEditValue.ToString().Equals(comboProcessGroup.EditValue.ToString()))
            //{
            //    comboProcessGroup_EditValueChanged(null, null);
            //}
            comboProcessGroup_EditValueChanged(null, null);
            // comboProcessGroup.EditValue = string.Join(",", cbList);

            //comboProcessGroup.Text= string.Join(",", cbTextList);
            //gridControl3.DataSource = statingGroupList;
        }

        //工艺路线图名称下拉框变更绑定已有工艺路线图
        private void comboChartList_SelectedValueChanged(object sender, EventArgs e)
        {
            var seItem = ((sender as ComboBoxEdit).SelectedItem) as SusComboBoxItem;
            if (null != seItem)
            {
                var pfChart = seItem.Tag as DaoModel.ProcessFlowChart;
                txtProcessChartMemo.Text = pfChart?.Remark?.Trim();
                txtProcessChartName.Text = pfChart.LinkName?.Trim();
                txtProcessTargetNum.Text = pfChart?.TargetNum.ToString();
                txtProcessChartName.Enabled = false;
                BindFlowChartLineItem(seItem.Value?.ToString());
                BindHangingPieceFlowCode(pfChart.BoltProcessFlowId);
                BindProductOutFlowCode(pfChart.OutputProcessFlowId);
            }
        }
        //【工艺路线图】删除工序
        private void btnDeleteFlow_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("prompDeleteConfirm"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                treeList1.Nodes.Remove(treeList1.FocusedNode);
                ReunificationTreeListIds();
                BindHangingPieceFlowCode();
                BindProductOutFlowCode();
                treeList1.RefreshDataSource();
            }

        }
        //【工艺路线图】对树Id重新编号
        void ReunificationTreeListIds()
        {
            var dt = treeList1.DataSource as List<DaoModel.ProcessFlowChartFlowRelationModel>;
            var index = 1;
            var idIndex = 1;
            foreach (var treeNodeObject in dt)
            {
                if (!treeNodeObject.IsChind)
                {
                    treeNodeObject.CraftFlowNo = index.ToString();
                    index++;
                }
                //对子节点parentId重新匹配
                var childrens = dt.Where(f => f.ParentId.Equals(treeNodeObject.Id) && !treeNodeObject.IsChind).ToList<DaoModel.ProcessFlowChartFlowRelationModel>();
                foreach (var cen in childrens)
                {
                    cen.ParentId = idIndex.ToString();
                }
                treeNodeObject.Id = idIndex.ToString();
                idIndex++;
            }
        }
        //【工艺路线图】添加工序
        private void btnAddFlow_Click(object sender, EventArgs e)
        {
            var data = new DaoModel.ProcessFlowChartFlowRelationModel()
            {
                Id = (chartList.Count + 1).ToString(),
                ParentId = "0",
                CraftFlowNo = (chartList.Count + 1).ToString(),
                IsStating = false,
                IsEn = true,
                IsChind = false
            };
            //var data = new DaoModel.ProcessFlowChartFlowRelationModel() { Id = (chartList.Count + 1).ToString(), ParentId = "0", CraftFlowNo = "1", IsEn = false, IsMergeForw = false, IsStating = false, FlowName = "工序1", FlowNo = "1", FlowCode = "工序1101" };
            //treeList1.AppendNode(data, 0);
            chartList.Add(data);
            //if (chartList.Count==1) {
            //    treeList1.DataSource = chartList;
            //}
            treeList1.RefreshDataSource();
            treeList1.ExpandAll();
        }
        //【挂片后开始生产的工序号码】根据工艺图绑定挂片列表
        void BindHangingPieceFlowCode(string flowId = null)
        {
            var processFlowList = chartList.Where(f => !f.IsChind);
            comboHangingPieceFlowCode.Properties.Items.Clear();
            foreach (var item in processFlowList)
            {
                comboHangingPieceFlowCode.Properties.Items.Add(new SusComboBoxItem()
                {
                    Text = string.Format("{0}-{1}-{2}", item.CraftFlowNo?.Trim(), item.FlowCode?.Trim(), item.FlowName?.Trim()),
                    Value = item.ProcessFlow?.Id,
                    Tag = item
                });
            }
            if (!string.IsNullOrEmpty(flowId))
            {
                foreach (var item in comboHangingPieceFlowCode.Properties.Items)
                {
                    var scb = item as SusComboBoxItem;
                    var df = scb?.Tag as DaoModel.ProcessFlowChartFlowRelationModel;
                    if (flowId.Equals(df?.ProcessFlow?.Id))
                    {
                        comboHangingPieceFlowCode.SelectedIndex = comboHangingPieceFlowCode.Properties.Items.IndexOf(item);
                        break;
                    }
                }

            }
        }
        //【产出工序(未选默认最后一道工序)】
        void BindProductOutFlowCode(string flowId = null)
        {
            var processFlowList = chartList.Where(f => !f.IsChind);
            comboProductOutFlow.Properties.Items.Clear();
            foreach (var item in processFlowList)
            {
                comboProductOutFlow.Properties.Items.Add(new SusComboBoxItem()
                {
                    Text = string.Format("{0}-{1}-{2}", item.CraftFlowNo?.Trim(), item.FlowCode?.Trim(), item.FlowName?.Trim()),
                    Value = item.ProcessFlow?.Id,
                    Tag = item
                });
            }
            if (!string.IsNullOrEmpty(flowId))
            {
                foreach (var item in comboProductOutFlow.Properties.Items)
                {
                    var scb = item as SusComboBoxItem;
                    var df = scb?.Tag as DaoModel.ProcessFlowChartFlowRelationModel;
                    if (flowId.Equals(df?.ProcessFlow?.Id))
                    {
                        comboProductOutFlow.SelectedIndex = comboProductOutFlow.Properties.Items.IndexOf(item);
                        break;
                    }
                }
            }
        }
        #region 上下文菜单

        private void RegisterProcessFlowContextMenu()
        {
            contextMenuStrip1.Items.Clear();
            var tsMenuItemCollapseAll = new ToolStripMenuItem() { Text = "全部折叠" };
            var tsMenuItemExpandAll = new ToolStripMenuItem() { Text = "全部展开" };
            tsMenuItemCollapseAll.Click += TsMenuItemCollapseAll_Click;
            tsMenuItemExpandAll.Click += TsMenuItemExpandAll_Click;
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { tsMenuItemCollapseAll, tsMenuItemExpandAll });
        }
        private void RegisterProcessFlowStatingContextMenu(string id)
        {
            contextMenuStrip1.Items.Clear();
            var tsMenuItemCollapseAll = new ToolStripMenuItem() { Text = "全部折叠",Name= "tsMenuItemCollapseAll" };
            var tsMenuItemExpandAll = new ToolStripMenuItem() { Text = "全部展开",Name= "tsMenuItemExpandAll" };
            var tsMenuItemAlltoColor = new ToolStripMenuItem() { Text = "指定颜色",Name= "tsMenuItemAlltoColor" };
            var tsMenuItemAlltoSize = new ToolStripMenuItem() { Text = "指定尺码",Name= "tsMenuItemAlltoSize" };

            var tsMenuItemAssignment = new ToolStripMenuItem() { Text = "调整分配比例",Name= "tsMenuItemAssignment" };
            var tsMenuItemEndStating = new ToolStripMenuItem() { Text = "设置为收尾站点", Name = "tsMenuItemEndStating" };
            var tsMenuItemChangeStatingRole = new ToolStripMenuItem() { Text = "修改站点角色", Name = "tsMenuItemChangeStatingRole" };
            var tsMenuItemSetPO = new ToolStripMenuItem() { Text = "设置PO", Name = "tsMenuItemSetPO" };

            if (!string.IsNullOrEmpty(processOrderId))
            {

                if (colorList.Count > 0)
                {
                    var colorAllRece = new ToolStripMenuItem("全部接收") { Name= "colorAllRece" };
                    colorAllRece.Tag = new FlowChartStatingItem(id, "-1");
                    colorAllRece.Click += ColorAllRece_Click;
                    tsMenuItemAlltoColor.DropDownItems.Add(colorAllRece);
                }
                foreach (var cl in colorList)
                {
                    var ts = new ToolStripMenuItem(cl.Color?.Trim(), null, ColorAllRece_Click);
                    ts.Tag = new FlowChartStatingItem(id, cl.Color?.Trim());
                    tsMenuItemAlltoColor.DropDownItems.Add(ts);
                }
                if (sizeList.Count > 0)
                {
                    var sizeAllRece = new ToolStripMenuItem("全部接收") { Name= "colorAllRece" };
                    sizeAllRece.Tag = new FlowChartStatingItem(id, "-1");
                    sizeAllRece.Click += SizeAllRece_Click;
                    tsMenuItemAlltoSize.DropDownItems.Add(sizeAllRece);
                }
                foreach (var size in sizeList)
                {
                    var ts = new ToolStripMenuItem(size?.Trim(), null, SizeAllRece_Click);
                    ts.Tag = new FlowChartStatingItem(id, size?.Trim());
                    tsMenuItemAlltoSize.DropDownItems.Add(ts);
                }

                tsMenuItemAssignment.Tag = new FlowChartStatingItem(id, "-1");
                tsMenuItemEndStating.Tag = new FlowChartStatingItem(id, "-1");
                tsMenuItemChangeStatingRole.Tag = new FlowChartStatingItem(id, "-1");

                //站点角色
                if (_StatingRoles != null)
                {
                    foreach (var item in _StatingRoles)
                    {
                        var ts = new ToolStripMenuItem(item.RoleName, null, TsMenuItemChangeStatingRole_Click);
                        var tmp = new FlowChartStatingItem(id, "");
                        tmp.StatingRoleId = item.Id;
                        ts.Tag = tmp;
                        tsMenuItemChangeStatingRole.DropDownItems.Add(ts);
                    }
                }

                //po列表
                if (_POList != null)
                {
                    foreach (var item in _POList)
                    {
                        var ts = new ToolStripMenuItem(item, null, TsMenuItemSetPO_Click);
                        ts.Tag = new FlowChartStatingItem(id, "");

                        tsMenuItemSetPO.DropDownItems.Add(ts);
                    }

                    if (_POList.Count > 0)
                    {
                        var ts = new ToolStripMenuItem("全部接收", null, TsMenuItemSetPO_Click) { Name= "colorAllRece" };
                        ts.Tag = new FlowChartStatingItem(id, "-1");
                        tsMenuItemSetPO.DropDownItems.Add(ts);
                    }
                }
            }

            tsMenuItemCollapseAll.Click += TsMenuItemCollapseAll_Click;
            tsMenuItemExpandAll.Click += TsMenuItemExpandAll_Click;

            tsMenuItemAssignment.Click += TsMenuItemAssignment_Click;
            tsMenuItemEndStating.Click += TsMenuItemEndStating_Click;
            //tsMenuItemSetPO.Click += TsMenuItemSetPO_Click;
            //tsMenuItemChangeStatingRole.Click += TsMenuItemChangeStatingRole_Click;

            contextMenuStrip1.Items.AddRange(new ToolStripItem[]
            {
                tsMenuItemCollapseAll,
                tsMenuItemExpandAll,
                tsMenuItemAlltoColor,
                tsMenuItemAlltoSize,
                tsMenuItemAssignment,
                tsMenuItemEndStating,
                tsMenuItemChangeStatingRole,
                tsMenuItemSetPO
            });
        }

        private void TsMenuItemSetPO_Click(object sender, EventArgs e)
        {
            var tsmItem = sender as ToolStripMenuItem;
            var flowChartStatingItem = tsmItem?.Tag as FlowChartStatingItem;
            var cItem = chartList.FirstOrDefault(f => f.Id.Equals(flowChartStatingItem.Id));

            if (cItem != null)
            {

                if (flowChartStatingItem.ColorOrSize.Equals("-1"))
                {
                    cItem.IsReceivingAllPoNumber = true;
                    cItem.ReceivingPoNumber = string.Empty;
                }
                else
                {
                    cItem.ReceivingPoNumber = tsmItem.Text;
                    cItem.IsReceivingAllPoNumber = false;
                }
                treeList1.RefreshDataSource();
                SuspeTool.ShowMessageBox("修改PO成功！");
            }
        }

        private void TsMenuItemChangeStatingRole_Click(object sender, EventArgs e)
        {
            var tsmItem = sender as ToolStripMenuItem;
            var flowChartStatingItem = tsmItem?.Tag as FlowChartStatingItem;
            var cItem = chartList.FirstOrDefault(f => f.Id.Equals(flowChartStatingItem.Id));

            if (!tsmItem.Text.Equals(cItem.FlowCode))
            {
                string statingId = cItem.ProcessflowIdOrStatingId;
                foreach (var item in chartList)
                {
                    if (item.ProcessflowIdOrStatingId.Equals(statingId))
                        item.FlowCode = tsmItem.Text;
                }

                //cItem.FlowCode = tsmItem.Text;
                treeList1.RefreshDataSource();
                SuspeTool.ShowMessageBox("修改站点角色成功！");
            }
        }

        private void TsMenuItemEndStating_Click(object sender, EventArgs e)
        {
            var tsmItem = sender as ToolStripMenuItem;
            var flowChartStatingItem = tsmItem?.Tag as FlowChartStatingItem;
            var cItem = chartList.FirstOrDefault(f => f.Id.Equals(flowChartStatingItem.Id));

            foreach (var item in chartList)
            {
                if (item.IsChind)
                    item.IsEndStating = false;
            }

            cItem.IsEndStating = true;

            SuspeTool.ShowMessageBox("设置收尾站点成功");
        }

        private void TsMenuItemAssignment_Click(object sender, EventArgs e)
        {
            InputForm form = new InputForm();
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                var tsmItem = sender as ToolStripMenuItem;
                var flowChartStatingItem = tsmItem?.Tag as FlowChartStatingItem;
                var cItem = chartList.FirstOrDefault(f => f.Id.Equals(flowChartStatingItem.Id));
                cItem.FlowNo = $"{form.Proportion}";
                //cItem.FlowName +=
                treeList1.RefreshDataSource();
            }
        }

        private void SizeAllRece_Click(object sender, EventArgs e)
        {
            var tsmItem = sender as ToolStripMenuItem;
            var flowChartStatingItem = tsmItem?.Tag as FlowChartStatingItem;
            var cItem = chartList.FirstOrDefault(f => f.Id.Equals(flowChartStatingItem.Id));
            if (!flowChartStatingItem.ColorOrSize.Equals("-1"))
            {
                cItem.IsReceivingAllSize = false;
                cItem.ReceivingSize = tsmItem.Text?.Trim();
                var coSize = string.Empty;
                if (!string.IsNullOrEmpty(cItem.ReceivingColor))
                {
                    coSize = "颜色:" + cItem.ReceivingColor?.Trim();
                }
                if (!string.IsNullOrEmpty(cItem.ReceivingSize))
                {
                    coSize += " 尺码:" + cItem.ReceivingSize?.Trim();
                }
                cItem.FlowName = coSize;
                treeList1.RefreshDataSource();
                return;
            }
            cItem.IsReceivingAllSize = true;
            cItem.FlowName = null;
            cItem.ReceivingSize = null;
            treeList1.RefreshDataSource();
        }

        private void ColorAllRece_Click(object sender, EventArgs e)
        {
            var tsmItem = sender as ToolStripMenuItem;
            var flowChartStatingItem = tsmItem?.Tag as FlowChartStatingItem;
            var cItem = chartList.FirstOrDefault(f => f.Id.Equals(flowChartStatingItem.Id));
            if (!flowChartStatingItem.ColorOrSize.Equals("-1"))
            {
                cItem.IsReceivingAllColor = false;
                cItem.ReceivingColor = tsmItem.Text?.Trim();
                var coSize = string.Empty;
                if (!string.IsNullOrEmpty(cItem.ReceivingColor))
                {
                    coSize = "颜色:" + cItem.ReceivingColor?.Trim();
                }
                if (!string.IsNullOrEmpty(cItem.ReceivingSize))
                {
                    coSize += " 尺码:" + cItem.ReceivingSize?.Trim();
                }
                cItem.FlowName = coSize;
                treeList1.RefreshDataSource();
                return;
            }
            cItem.IsReceivingAllSize = true;
            cItem.FlowName = null;
            cItem.ReceivingColor = null;
            treeList1.RefreshDataSource();
        }
        private void TsMenuItemExpandAll_Click(object sender, EventArgs e)
        {
            treeList1.ExpandAll();
        }

        private void TsMenuItemCollapseAll_Click(object sender, EventArgs e)
        {
            treeList1.CollapseAll();
        }
        #endregion

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            //int rowHandle = treeList1.ViewInfo.GetRowHandleByMouseEventArgs(e);
            if (e.Button == MouseButtons.Right)
            {
                TreeList treeList = sender as TreeList;
                TreeListHitInfo info = treeList.CalcHitInfo(e.Location);
                if (info.Node != null)
                {
                    treeList1.FocusedNode = info.Node;
                }
            }
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            SusTreeList list = (SusTreeList)sender;
            TreeListHitInfo info = list.CalcHitInfo(e.Location);
            var isStating = info?.Node?.GetValue("IsStating");
            var id = info?.Node?.GetValue("Id");
            if (null == isStating) return;
            if (e.Button == MouseButtons.Right)
            {
                if (!(bool)isStating)
                {
                    RegisterProcessFlowContextMenu();
                    return;
                }
                RegisterProcessFlowStatingContextMenu(id?.ToString());
            }
        }

       // DaoModel.StatingModel editStating = null;
        private void gridView3_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left && e.Clicks == 2)
            //{

            //    var stating = gridView3.GetRow(gridView3.FocusedRowHandle) as DaoModel.StatingModel;
            //    if (null!= stating)
            //    {
            //        editStating = stating;
            //    }
            //}
        }
    }
    class FlowChartStatingItem
    {
        public FlowChartStatingItem(string id, string colorOrSize)
        {
            this.Id = id;
            this.ColorOrSize = colorOrSize;
        }
        public string Id { set; get; }
        public string ColorOrSize { set; get; }

        public string StatingRoleId { get; set; }
    }
}
