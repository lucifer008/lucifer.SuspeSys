using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Client.Modules.Ext;
using System.Windows.Controls;
using SuspeSys.Client.Action.Attendance;
using SuspeSys.Utils.Exceptions;
using SuspeSys.Domain.Business;
using SuspeSys.Domain;
using DevExpress.XtraGrid.Views.Base;
using SuspeSys.Client.Action.Common;
using DevExpress.XtraSpreadsheet.Utils;

namespace SuspeSys.Client.Modules.Attendance
{
    public partial class ClassesEmployeesInput :  Ext.SusXtraUserControl
    {
        public Domain.Dto.ClassesEmployeeDto _ClassesEmployee;
        public Domain.Dto.ClassesEmployeeDto ClassesEmployee
        {
            get
            {
                return _ClassesEmployee;
            }
            set
            {
                _ClassesEmployee = value;

                
            }
        }


        /// <summary>
        /// 编辑模式
        /// </summary>
        protected bool IsEditModel { get { return this.ClassesEmployee != null; } }
        public ClassesEmployeesInput()
        {
            InitializeComponent();
        }

        public ClassesEmployeesInput(XtraUserControl1 ucMain) : this()
        {
            this.ucMain = ucMain;
        }




        private void InitPageData()
        {
            InitPageControl();
            this.InitEditData();
        }

        private void InitSiteGroup()
        {
            var dic = EnumHelper.GetDictionary(typeof(Weeks));

            var list = dic.Select(o => new SusComboBoxItem(o.Key, o.Value)).ToList();
            //list.Insert(0, new SusComboBoxItem(-1, "请选择"));
            cb_weeks.Properties.Items.AddRange(list);

            //生产组别
            this.ddlSiteGroup.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GroupNo", "组编码"),
                    new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GroupName", "组名"),
                    //new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Factory", "工场"),
                    //new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Workshop", "车间")
                 });

            var siteGroup = Action.Common.CommonAction.GetList<Domain.SiteGroup>(true);
            siteGroup.Insert(0, new SiteGroup() { Id = string.Empty, GroupName = "请选择组别s" });
            ddlSiteGroup.Properties.DataSource = siteGroup;
            ddlSiteGroup.Properties.ValueMember = "Id";
            ddlSiteGroup.Properties.NullText = "请选择组别";
            ddlSiteGroup.Properties.DisplayMember = "GroupName";

 
        }

        private void InitEditData()
        {
            if (this.IsEditModel)
            {
                var sqlEmployee = "select * from Employee where Code = @Code";

                var employee = Action.Common.CommonAction.GetList<Domain.Employee>(sqlEmployee, new { Code = this.ClassesEmployee.EmployeeCode });

                if (employee != null)
                {
                    var sche = employee.Select(o => new SchedulingEmployee()
                    {
                        Checked = true,
                        Code = o.Code,
                        Name = o.RealName,
                        Employee = o
                    }).ToList();

                    this.gd_Employee.DataSource = sche;
                }
                    

                

                var sqlClassInfo = "select * from ClassesInfo where id = @id";

                var classInfo = Action.Common.CommonAction.GetList<Domain.ClassesInfo>(sqlClassInfo, new { id = this.ClassesEmployee.Id });

                this.gd_Scheduling.DataSource = classInfo;

                this.dtEdit.EditValue = this.ClassesEmployee.AttendanceDate;

                this.cb_weeks.SelectedIndex = this.ClassesEmployee.Week;

                this.btnAdd.Text = "编 辑";
            }
        }

        private void InitPageControl()
        {
            this.SchedulingDataBind();
            this.InitEmployee();
            InitSiteGroup();
        }

        private void InitEmployee()
        {
            var gridView = this.gridView1;

            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Check",FieldName= "Checked",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工编号",FieldName="Code",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工姓名",FieldName="Name",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="性别",FieldName="Sex",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Email",FieldName="Email",Visible=true},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="手机号",FieldName="Phone",Visible=true}
            });

            gridView.Columns["Code"].OptionsColumn.ReadOnly = true;
            gridView.Columns["Name"].OptionsColumn.ReadOnly = true;

            //for (int i = 0; i < gridView.Columns.Count; i++)
            //{

            //}
            //foreach (var item in gridView.Columns)
            //{
            //    item.OptionsColumn
            //} 

            this.gd_Employee.MainView = gridView;

            //gridView.
        }

        #region 查询员工信息


        /// <summary>
        /// 查询员工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEmployeeSearch_Click(object sender, EventArgs e)
        {
            this.QueryEmployee();
        }

        void QueryEmployee()
        {
            int pageSize = int.MaxValue;
            long totalCount = 0;
            var searchKey = txtEmployeeKeyWord.EditValue?.ToString().Trim();
            IDictionary<string, string> ordercondition = new Dictionary<string,string>();


            //Checked
            if (!string.IsNullOrWhiteSpace(Convert.ToString(this.ddlSiteGroup.EditValue)))
            {
                ordercondition.Add("SITEGROUP_Id", this.ddlSiteGroup.EditValue.ToString());
            }

            if (!string.IsNullOrWhiteSpace(this.txtEmployeeKeyWord.Text.Trim()))
            {
                searchKey = this.txtEmployeeKeyWord.Text.Trim();
                //ordercondition.Add("RealName", this.ddlSiteGroup.EditValue.ToString());
                //ordercondition.Add("RealName", this.ddlSiteGroup.EditValue.ToString());
            }

            var list = AttendanceAction.Instance.SearchEmployee(1, pageSize, out totalCount, null, searchKey, ordercondition);

            var sche = list.Select(o => new SchedulingEmployee()
            {
                Checked = false,
                Code = o.Code,
                Name = o.RealName,
                Employee = o
            }).ToList();
 
            this.gd_Employee.DataSource = sche;
            // susGrid1.DataGrid.DataSource = processOrderQueryAction.Model.ProcessOrderList;

        }

        #endregion

        private void ClassesEmployeesInput_Load(object sender, EventArgs e)
        {

            this.InitPageData();

        }

        /// <summary>
        /// 添加排班记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SchedulingRule dto = this.InitSchedulingRule();

                AttendanceAction.Instance.AddClassesEmployee(dto);

                MessageBox.Show("保存成功", "提示信息");
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message, "提示信息");

            }
            finally
            {
                base.Closing(this, new EventArgs());
            }
        }

        private SchedulingRule InitSchedulingRule()
        {
            SchedulingRule dto = new SchedulingRule();

            //排班员工
            dto.SchedulingEmployees = ((ColumnView)this.gridView1).DataSource as List<SchedulingEmployee>;

            //班次
            dto.ClassesInfoModel =  (((ColumnView)this.gridView2).GetFocusedRow() as Domain.ClassesInfo); 

            //排班日期
            dto.SchedulingDateTime = dtEdit.DateTime;

            //仅用于
            SusComboBoxItem selectItem = cb_weeks.SelectedItem as SusComboBoxItem;
            dto.Week = selectItem == null ? 0 : (Weeks)selectItem.Value;

            //编辑模式
            dto.IsEditMode = this.IsEditModel;

            if (this.IsEditModel)
                //排班Id
                dto.ClassesEmployeeId = this.ClassesEmployee.ClassesEmployeeId;

            return dto;
        }


        #region 查询排班


        /// <summary>
        /// 查询排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSchedulingSearch_Click(object sender, EventArgs e)
        {
            QueryScheduling();
        }

        private void SchedulingDataBind()
        {
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

            gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
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
        }

        private void QueryScheduling()
        {
            int pageSize = int.MaxValue;
            long totalCount = 0;
            IDictionary<string, string> ordercondition = null;
            
            var list = AttendanceAction.Instance.SearchClassesInfo(1, pageSize, out totalCount, ordercondition, this.txt_SchedulingKey.Text);

            gd_Scheduling.DataSource = list;
            
        }
        #endregion

    }
}
