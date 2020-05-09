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
using SuspeSys.Client.Common.Utils;
using DevExpress.XtraTab;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Client.Action.Attendance;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Domain;
using SuspeSys.Client.Action;

namespace SuspeSys.Client.Modules.Attendance
{
    public partial class ClassesInfoIndex : Ext.SusXtraUserControl
    {
        public ClassesInfoIndex()
        {
            InitializeComponent();
        }
        public ClassesInfoIndex(XtraUserControl1 ucMain) : this()
        {
            this.ucMain = ucMain;
        }

        private void ClassesInfoIndex_Load(object sender, EventArgs e)
        {
            RegisterEvent();
            BindGridHeader();
            Query(1);
        }

        private void RegisterEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;
            searchControl1.KeyDown += SearchControl1_KeyDown;
        }

        private void SearchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query(1);
            }
        }
        private void BindGridHeader()
        {
            var gc = susGrid1.DataGrid;
            var gridView = susGrid1.DataGrid.MainView as GridView;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;

            var time1GoToWorkDate = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段1上班时间", FieldName = "Time1GoToWorkDate", Visible = true,Name= "Time1GoToWorkDate" };
            var time1GoIffWorkDate=new  DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段1下班时间", FieldName = "Time1GoOffWorkDate", Visible = true, Name = "Time1GoOffWorkDate" };
            var time2GoToWorkDate = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段2上班时间", FieldName = "Time2GoToWorkDate", Visible = true, Name = "Time2GoToWorkDate" };
            var time2GoOffWorkDate=new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段2下班时间", FieldName = "Time2GoOffWorkDate", Visible = true, Name = "Time2GoOffWorkDate" };
            var time3GoToWorkDate=new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段3上班时间", FieldName = "Time3GoToWorkDate", Visible = true, Name = "Time3GoToWorkDate" };
            var time3GoOffWorkDate=new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段3下班时间", FieldName = "Time3GoOffWorkDate", Visible = true, Name = "Time3GoOffWorkDate" };

            var time3IsOT = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段3为加班时段", FieldName = "Time3IsOverTime", Visible = true, Name = "Time3IsOverTime" };
            var OTIn = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "加班开始", FieldName = "OverTimeIn", Visible = true, Name = "OverTimeIn" };
            var OTOut = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "加班结束", FieldName = "OverTimeOut", Visible = true, Name = "OverTimeOut" };

            time1GoToWorkDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            time1GoToWorkDate.DisplayFormat.FormatString = "HH:mm";

            time1GoIffWorkDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            time1GoIffWorkDate.DisplayFormat.FormatString = "HH:mm";

            time2GoToWorkDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            time2GoToWorkDate.DisplayFormat.FormatString = "HH:mm";

            time2GoOffWorkDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            time2GoOffWorkDate.DisplayFormat.FormatString = "HH:mm";

            time3GoToWorkDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            time3GoToWorkDate.DisplayFormat.FormatString = "HH:mm";

            time3GoOffWorkDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            time3GoOffWorkDate.DisplayFormat.FormatString = "HH:mm";

            OTIn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            OTIn.DisplayFormat.FormatString = "HH:mm";

            OTOut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            OTOut.DisplayFormat.FormatString = "HH:mm";

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="班次",FieldName="Num",Visible=true,Name="classses"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="班次类型",FieldName="CType",Visible=true,Name="CType"},
                
               time1GoToWorkDate,
               time1GoIffWorkDate,
               time2GoToWorkDate,
               time2GoOffWorkDate,
               time3GoToWorkDate,
               time3GoOffWorkDate,
               time3IsOT,
               OTIn,
               OTOut,
               new DevExpress.XtraGrid.Columns.GridColumn() { Caption="是否启用",FieldName="IsEnabled",Visible=true,Name="IsEnabled"},

            });
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            gc.MainView = gridView;
            //gridControl1.DataSource = commonAction.GetAllColorList();
            gridView.BestFitColumns();//按照列宽度自动适配
            gridView.GridControl = gc;
            gridView.OptionsView.ShowFooter = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsBehavior.Editable = false;
            gridView.MouseDown += GridView_MouseDown;
            gridView.CustomColumnDisplayText += GridView_CustomColumnDisplayText; 
        }

        private void GridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column.FieldName == "CType")
            //{
            //    int cType = Convert.ToInt32(e.Value);
            //    if (cType == 0)
            //    {
            //        e.DisplayText = "正常班次";
            //    }
            //    else if (cType == 1)
            //    {
            //        e.DisplayText = "加班班次";
            //    }
            //    //else if (cType == 2)
            //    //{
            //    //    e.DisplayText = "假日班次";
            //    //}
            //}
        }

       

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                EditClassesInfo(sender as GridView);
            }
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }
        AttendanceAction attendanceAction = AttendanceAction.Instance;

        private void Query(int currentPageIndex)
        {
            int pageSize = susGrid1.PageSize;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            var list = attendanceAction.SearchClassesInfo(currentPageIndex, pageSize, out totalCount, ordercondition, searchControl1.Text);
            susGrid1.SetGridControlData(list, currentPageIndex, pageSize, totalCount);
        }
        void EditClassesInfo(GridView gv)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (null != gv)
                {


                    var classesInfo = GetClassesInfo(); //gv.GetRow(gv.FocusedRowHandle) as DaoModel.ProcessOrder;
                    if (null == classesInfo) return;
                    var classesInfoInput = new ClassesInfoInput() { Dock = DockStyle.Fill };
                    classesInfoInput.ucMain = ucMain;
                    classesInfoInput.ClassInfo = classesInfo;
                    classesInfoInput.DoClose += ClassesInfoInput_DoClose;

                    //base.AddMainTabControl(ucMain.MainTabControl, classesInfoInput.Name, string.Format("正在编辑班次[{0}]", classesInfo?.Num?.Trim()));
                    var tab = new XtraTabPage();
                    tab.Name = classesInfoInput.Name;
                    tab.Text = string.Format("正在编辑班次[{0}]", classesInfo?.Num?.Trim());
                    if (!ucMain.MainTabControl.TabPages.Contains(tab))
                    {
                        tab.Controls.Add(classesInfoInput);
                        ucMain.MainTabControl.TabPages.Add(tab);
                    }
                    ucMain.MainTabControl.SelectedTabPage = tab;


                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMessageBox.Show(ex.Message, LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ClassesInfoInput_DoClose(object sender, EventArgs e)
        {
            Query(1);
        }

        private ClassesInfo GetClassesInfo()
        {
            var gv = susGrid1.DataGrid.MainView as GridView;
            if (null == gv) return null;
            var editClassInfo = gv.GetRow(gv.FocusedRowHandle) as SuspeSys.Domain.ClassesInfo;
            if (!string.IsNullOrEmpty(editClassInfo.Id))
            {
                editClassInfo = Client.Action.Common.CommonAction.Instance.Get<ClassesInfo>(editClassInfo.Id);
            }
            return editClassInfo;
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            try
            {
                //  MessageBox.Show(ButtonName.ToString());
                switch (ButtonName)
                {
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.Refresh:
                        Query(1);
                        break;
                    case ButtonName.Add:
                        AddClassesEmployees();
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    case ButtonName.Fix:
                        var fixFlag = this.FixFlag;
                        ucMain.FixOrNonFix(ref fixFlag, susGrid1.DataGrid);
                        this.FixFlag = fixFlag;
                        break;
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
                return;
                //Console.WriteLine("异常:"+ex.Message);
            }
            finally
            {
                SusTransitionManager.EndTransition();
            }
        }
        private void AddClassesEmployees()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                XtraTabPage tab = new SusXtraTabPage();

                tab.Text = string.Format("新增班次[{0}]", "");
                tab.Name = string.Format("新增班次[{0}]", "");

                var input = new ClassesInfoInput(ucMain);
                input.DoClose += ClassesInfoInput_DoClose;

                XtraTabPageHelper.AddTabPage(ucMain.MainTabControl, tab, input);
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

    }
}
