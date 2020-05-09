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
using SuspeSys.Client.Action.Common;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Domain.SusEnum;
using System.Windows.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Action.Attendance;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;
using DevExpress.XtraTab;
using SuspeSys.Client.Action;
using SuspeSys.Domain.Dto;
using SuspeSys.Utils.Exceptions;

namespace SuspeSys.Client.Modules.Attendance
{
    public partial class ClassesEmployeesIndex : Ext.SusXtraUserControl
    {



        public ClassesEmployeesIndex()
        {
            InitializeComponent();
        }
        public ClassesEmployeesIndex(XtraUserControl1 ucMain) : this()
        {
            this.ucMain = ucMain;
        }
        private void rgDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDateRange();
        }

        private void BindDateRange()
        {
            var sValue = rgDateRange.Properties.Items[rgDateRange.SelectedIndex].Value?.ToString();
            switch (sValue)
            {
                case "-1"://全部
                    dateBegin.Text = string.Empty;
                    dateEnd.Text = string.Empty;
                    break;
                case "0"://今天
                    dateBegin.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "Yesterday":
                    dateBegin.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    dateEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "LatelyThreeDay":
                    dateBegin.Text = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                    dateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "CurrentWeek":
                    dateBegin.Text = GetWeekFirstDayMon(DateTime.Now).ToString("yyyy-MM-dd");
                    dateEnd.Text = GetWeekLastDaySun(DateTime.Now).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "CurrentMonth":
                    dateBegin.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-01");
                    dateEnd.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "LastWeek":
                    dateBegin.Text = DateTime.Now.AddDays(Convert.ToInt32(1 - Convert.ToInt32(DateTime.Now.DayOfWeek)) - 7).ToString("yyyy-MM-dd");
                    dateEnd.Text = DateTime.Now.AddDays(Convert.ToInt32(1 - Convert.ToInt32(DateTime.Now.DayOfWeek)) - 7).AddDays(6).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
                case "LastMonth":
                    dateBegin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
                    dateEnd.Text = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";
                    break;
            }
        }
        public DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }
        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        private void ClassesEmployeesIndex_Load(object sender, EventArgs e)
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            susGrid1.OnPageChanged += SusGrid1_OnPageChanged;

            this.InitPageControl();

            BindGridHeader();
        }

        private void SusGrid1_OnPageChanged(int currentPageIndex)
        {
            Query(currentPageIndex);
        }

        private void Query(int currentPageIndex)
        {
            var siteGroupId = Convert.ToString(this.ddl_group.EditValue);
            var week = Convert.ToInt32(this.ddl_Week.EditValue);
            var classInfoId = Convert.ToString(this.ddl_Classesinfo.EditValue);
            var employeeCode = this.txtEmployeeName.Text;
            var begin = this.dateBegin.DateTime;
            var end = this.dateEnd.DateTime;


            int pageSize = susGrid1.PageSize;
            var list = AttendanceAction.Instance.SearchClassEmployeeInfo(siteGroupId, week,classInfoId,employeeCode, begin, end, currentPageIndex,pageSize);
            susGrid1.SetGridControlData(list.Data, currentPageIndex, pageSize, list.Total);
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
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    case ButtonName.Fix:
                        var fixFlag = this.FixFlag;
                        ucMain.FixOrNonFix(ref fixFlag, susGrid1.DataGrid);
                        this.FixFlag = fixFlag;
                        break;
                    case ButtonName.Add:
                        this.AddClassEmployee();
                        break;
                    case ButtonName.Delete:
                        this.RemoveClassesEmployee();
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

        private void AddClassEmployee()
        {
            ClassesEmployeesInput input = new ClassesEmployeesInput(ucMain);
            input.DoClose += Input_DoClose;

            //TODO  需要多语言
            base.AddMainTabControl(input, "ClassesEmployeesInput", "添加排班");
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void RemoveClassesEmployee()
        {
            var item = GetClassesEmployeeInfo();

            if (item == null)
            {
                //TODO  需要多语言
                MessageBox.Show("请选择排班记录", "提示");
                return;
            }

            var confirm = MessageBox.Show("确定要删除当前选中的排班记录吗？", "提示信息", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.No)
                return;

            var sql = "update ClassesEmployee set Deleted = 1 where id = @id";

            int eff =  Action.Common.CommonAction.UpdateBySql<Domain.ClassesInfo>(sql, new { id = item.ClassesEmployeeId });

            if (eff > 0)
            {
                this.Query(1);
                //TODO  需要多语言
                MessageBox.Show("删除成功", "提示");
            }
        }

        private void Input_DoClose(object sender, EventArgs e)
        {
            this.Query(1);
        }


        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                EditClassEmployee(sender as GridView);
            }
        }

        #region 编辑

        private ClassesEmployeeDto GetClassesEmployeeInfo()
        {
            var gv = susGrid1.DataGrid.MainView as GridView;
            if (null == gv) return null;
            var editClassInfo = gv.GetRow(gv.FocusedRowHandle) as ClassesEmployeeDto;
            
            return editClassInfo;
        }

        private void EditClassEmployee(GridView sender)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (null != sender)
                {


                    var classesEmployee = GetClassesEmployeeInfo(); //gv.GetRow(gv.FocusedRowHandle) as DaoModel.ProcessOrder;

                    var dtCurrent = DateTime.Now;

                    if (classesEmployee.AttendanceDate < new DateTime(dtCurrent.Year, dtCurrent.Month, dtCurrent.Day, classesEmployee.Time1GoToWorkDate.Value.Hour, classesEmployee.Time1GoToWorkDate.Value.Minute, classesEmployee.Time1GoToWorkDate.Value.Second))
                    {
                        //TODO
                        throw new BusinessException("不能修改小于当前时间的排班");
                    }
                    if (null == classesEmployee) return;
                    var classesInfoInput = new ClassesEmployeesInput()
                    {
                        Dock = DockStyle.Fill ,
                        ucMain = ucMain,
                        ClassesEmployee = classesEmployee,
                    };
                    classesInfoInput.DoClose += Input_DoClose;

                    //base.AddMainTabControl(ucMain.MainTabControl, classesInfoInput.Name, string.Format("正在编辑班次[{0}]", classesEmployee?.Num?.Trim()));
                    var tab = new XtraTabPage();
                    tab.Name = classesInfoInput.Name;
                    tab.Text = string.Format("正在编辑排班信息{0}", classesEmployee.EmployeeCode);
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
        #endregion

        #region 初始化Grid

        private void BindGridHeader()
        {
            var gc = susGrid1.DataGrid; 
            var gridView = (DevExpress.XtraGrid.Views.Grid.GridView)this.susGrid1.DataGrid.MainView;
            //gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;

            var time1GoToWorkDate = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段1上班时间", FieldName = "Time1GoToWorkDate", Visible = true, Name = "Time1GoToWorkDate" };
            var time1GoIffWorkDate = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段1下班时间", FieldName = "Time1GoOffWorkDate", Visible = true, Name = "Time1GoOffWorkDate" };
            var time2GoToWorkDate = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段2上班时间", FieldName = "Time2GoToWorkDate", Visible = true, Name = "Time2GoToWorkDate" };
            var time2GoOffWorkDate = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段2下班时间", FieldName = "Time2GoOffWorkDate", Visible = true, Name = "Time2GoOffWorkDate" };
            var time3GoToWorkDate = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段3上班时间", FieldName = "Time3GoToWorkDate", Visible = true, Name = "Time3GoToWorkDate" };
            var time3GoOffWorkDate = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "时段3下班时间", FieldName = "Time3GoOffWorkDate", Visible = true, Name = "Time3GoOffWorkDate" };

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

            //排班日期
            var schedulingDate = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "排班日期", FieldName = "AttendanceDate", Visible = true ,Name = "AttendanceDate" };
            schedulingDate.DisplayFormat.FormatString = "YYYY:MM:dd";
            //组别
            var GroupName = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "组别", FieldName = "GroupName", Visible = true, Name = "GroupName" };

            //工号
            var EmployeeCode = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "工号", FieldName = "EmployeeCode", Visible = true, Name = "EmployeeCode" };

            //姓名
            var RealName = new DevExpress.XtraGrid.Columns.GridColumn() { Caption = "姓名", FieldName = "RealName", Visible = true, Name = "RealName" };

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                schedulingDate,
                GroupName,
                EmployeeCode,
                RealName,
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
            gridView.CustomColumnDisplayText += GridView_CustomColumnDisplayText;
            gridView.MouseDown += GridView_MouseDown;
            gridView.Appearance.FocusedRow.BackColor = Color.IndianRed;
        }

        private void GridView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            //throw new NotImplementedException();
        }
        #endregion


        #region 初始化查询选择
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitPageControl()
        {
            //初始化组别
            var groupList = CommonAction.GetList<Domain.SiteGroup>();
            groupList.Add(new Domain.SiteGroup() { GroupName = "请选择", Id = string.Empty });
            ddl_group.Properties.DataSource = groupList;
            ddl_group.Properties.Columns.Add(new LookUpColumnInfo("GroupName"));
            ddl_group.Properties.ValueMember = "Id";
            ddl_group.Properties.DisplayMember = "GroupName";
            ddl_group.Properties.ShowHeader = false;
            ddl_group.Properties.NullText = "请选择组别";

            //初始化班次
            var classInfo = CommonAction.GetList<Domain.ClassesInfo>();
            classInfo.Add(new Domain.ClassesInfo() { Num = "请选择", Id= string.Empty });
            ddl_Classesinfo.Properties.DataSource = classInfo;
            ddl_Classesinfo.Properties.Columns.Add(new LookUpColumnInfo("Num"));
            ddl_Classesinfo.Properties.ValueMember = "Id";
            ddl_Classesinfo.Properties.DisplayMember = "Num";
            ddl_Classesinfo.Properties.ShowHeader = false;
            ddl_Classesinfo.Properties.NullText = "请选择班次";

            //初始化仅次于
            var dic = EnumHelper.GetDictionary(typeof(Weeks));
            ddl_Week.Properties.DataSource = dic.Select(o => new SusComboBoxItem(o.Key, o.Value)).ToList();
            ddl_Week.Properties.Columns.Add(new LookUpColumnInfo("Text"));
            ddl_Week.Properties.ValueMember = "Value";
            ddl_Week.Properties.DisplayMember = "Text";
            ddl_Week.Properties.ShowHeader = false;
            //ddl_Week.Properties.NullText = "仅次于";
        }
        #endregion

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            this.Query(1);
        }
    }
}
