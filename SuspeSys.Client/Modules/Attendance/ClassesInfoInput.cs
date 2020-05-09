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
using SuspeSys.Client.Modules.SusDialog;
using DevExpress.XtraGrid.Views.Grid;
using SuspeSys.Client.Common.Utils;
using SuspeSys.Client.Action.Attendance;
using SuspeSys.Domain;
using SuspeSys.Client.Modules.Ext;

namespace SuspeSys.Client.Modules.Attendance
{
    public partial class ClassesInfoInput : Ext.SusXtraUserControl
    {
        private AttendanceAction attendanceAction = AttendanceAction.Instance;

        public ClassesInfo ClassInfo { get; internal set; }

        public ClassesInfoInput()
        {
            InitializeComponent();
        }
        public ClassesInfoInput(XtraUserControl1 ucMain) : this()
        {
            this.ucMain = ucMain;
        }

        private void btnAddClassEmployees_Click(object sender, EventArgs e)
        {
            var eDialog = new EmployeesDialog();
            eDialog.ShowDialog();
        }

        private void ClassesInfoInput_Load(object sender, EventArgs e)
        {
            RegisterEvent();
           

            BindEditClassesInfo();

        }

        private void BindEditClassesInfo()
        {
            if (ClassInfo != null)
            {
                txtClassesNum.Text = ClassInfo.Num;
                teTime1GoToWorkDate.Time = ClassInfo.Time1GoToWorkDate.Value;
                teTime1GoOffWorkDate.Time = ClassInfo.Time1GoOffWorkDate.Value;
                teTime2GoToWorkDate.Time = ClassInfo.Time2GoToWorkDate.Value;
                teTime2GoOffWorkDate.Time = ClassInfo.Time2GoOffWorkDate.Value;
                teTime3GoToWorkDate.Time = ClassInfo.Time3GoToWorkDate.Value;
                teTime3GoOffWorkDate.Time = ClassInfo.Time3GoOffWorkDate.Value;

                TimeOTIn.Time = ClassInfo.OverTimeIn.HasValue ? ClassInfo.OverTimeIn.Value : new DateTime();
                TimeOTOut.Time = ClassInfo.OverTimeOut.HasValue? ClassInfo.OverTimeOut.Value : new DateTime();

                this.txtClassesType.EditValue = ClassInfo.CType;

                rgIsEnabled.SelectedIndex = (ClassInfo.IsEnabled.HasValue && ClassInfo.IsEnabled.Value) ? 1 :0;
                rg_time3IsOT.SelectedIndex = (ClassInfo.Time3IsOverTime.HasValue && ClassInfo.IsEnabled.Value) ? 1 : 0;
            }
        }

        ///// <summary>
        ///// 初始化数据
        ///// </summary>
        //private void BindData()
        //{
        //    cbClassesType.Properties.Items.Add(new SusComboBoxItem(0, "正常班次"));
        //    cbClassesType.Properties.Items.Add(new SusComboBoxItem(1, "加班班次"));
        //    //cbClassesType.Properties.Items.Add(new SusComboBoxItem(2, "假日班次"));
        //}

        private void RegisterEvent()
        {
            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
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
                    case ButtonName.Save:
                        var isSuccess = Save();
                        if (isSuccess)
                        {
                            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));
                            return;
                        }
                        break;
                    case ButtonName.Max:
                        ucMain.MaxOrMin();
                        break;
                    //case ButtonName.Fix:
                    //    var fixFlag = this.FixFlag;
                    //    ucMain.FixOrNonFix(ref fixFlag, susGrid1.DataGrid);
                    //    this.FixFlag = fixFlag;
                    //    break;
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Error(ex);
                return;
                //Console.WriteLine("异常:"+ex.Message);
            }
            finally
            {
                SusTransitionManager.EndTransition();
            }
        }

        /// <summary>
        /// 清空页面控件
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private void Clear(Control control)
        {
            foreach (Control item in control.Controls)
            {
                this.ClearByControlType(item);

                if (item.Controls == null || item.Controls.Count == 0)
                    continue;
                else
                {
                    this.Clear(item);
                }
            }
        }

        private void ClearByControlType(Control control)
        {
            if (control.GetType().Name.Equals( "TextEdit", StringComparison.OrdinalIgnoreCase))
            {
                var text = control as DevExpress.XtraEditors.TextEdit;
                text.EditValue = string.Empty;
            }
            else if (control.GetType().Name.Equals("TimeEdit", StringComparison.OrdinalIgnoreCase))
            {
                var time = control as DevExpress.XtraEditors.TimeEdit;
                time.EditValue = new DateTime(2018, 3, 12, 0, 0, 0);
            }
        }

        private bool Save()
        {
            if (string.IsNullOrWhiteSpace(this.txtClassesNum.Text))
            {
                MessageBox.Show("请填写班次信息","title");
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.txtClassesType.Text))
            {
                MessageBox.Show("请填写班次类型", "title");
                return false;
            }

            var isSucess = false;
            var classNum = txtClassesNum.Text.Trim();
            var time1GoToWorkDate = teTime1GoToWorkDate.Time;
            var time1GoOffWorkDate = teTime1GoOffWorkDate.Time;
            var time2GoToWorkDate = teTime2GoToWorkDate.Time;
            var time2GoOffWorkDate = teTime2GoOffWorkDate.Time;
            var time3GoToWorkDate = teTime3GoToWorkDate.Time;
            var time3GoOffWorkDate = teTime3GoOffWorkDate.Time;

            var OverTimeIn = this.TimeOTIn.Time;
            var OverTimeOut = this.TimeOTOut.Time;

            attendanceAction.Model = new Domain.ClassesInfo()
            {
                Num = classNum,
                CType = txtClassesType.Text,
                Time1GoToWorkDate = ToWorkDate(time1GoToWorkDate),
                Time1GoOffWorkDate = ToWorkDate(time1GoOffWorkDate),
                Time2GoToWorkDate = ToWorkDate(time2GoToWorkDate),
                Time2GoOffWorkDate = ToWorkDate(time2GoOffWorkDate),
                Time3GoToWorkDate = ToWorkDate(time3GoToWorkDate),
                Time3GoOffWorkDate = ToWorkDate(time3GoOffWorkDate),
                IsEnabled = Convert.ToInt32(rgIsEnabled.EditValue) == 1,
                Time3IsOverTime = Convert.ToInt32(rg_time3IsOT.EditValue) == 1,

                OverTimeIn = ToWorkDate(this.TimeOTIn.Time),
                OverTimeOut = ToWorkDate(this.TimeOTOut.Time),
                Id = this.ClassInfo == null ? string.Empty: this.ClassInfo.Id
            };
            attendanceAction.SaveClassesInfo();
            isSucess = true;

            this.Closing(this, new EventArgs());

            this.Clear(this);
            return isSucess;

        }

        /// <summary>
        /// 返回 2018-3-12 XX:XX:XX
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected static DateTime ToWorkDate(DateTime dt)
        {
            return new DateTime(2018, 3, 12, dt.Hour, dt.Minute, dt.Second);
        }

        private void BindGridHeader()
        {
            //this.deTime1GoOffWorkDate.Properties.EditMask = "G";
            //this.deTime1GoOffWorkDate.Properties.DisplayFormat.FormatString = "HH:mm";
            //this.deTime1GoOffWorkDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //this.deTime1GoOffWorkDate.Properties.EditFormat.FormatString = "HH:mm";
            //this.deTime1GoOffWorkDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //this.deTime1GoOffWorkDate.Properties.Mask.EditMask = "HH:mm";
            //this.deTime1GoOffWorkDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            //this.deTime1GoOffWorkDate.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;

            //var gc =gcClassEmployes;
            ////var gv=gridControl1.MainView as GridView;
            ////gv.Columns.Clear();
            ////gridControl1.ViewCollection.Clear();

            ////gridControl1.MainView.PopulateColumns();
            //var gridView = gcClassEmployes.MainView as GridView;
            //gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            //gridView.MouseDown += GridView_MouseDown;
            //gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工编号",FieldName="CusNo",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="员工姓名",FieldName="CusName",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="性别",FieldName="PurchaseOrderNo",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="上班时间",FieldName="OrderNo",Visible=true},
            //    new DevExpress.XtraGrid.Columns.GridColumn() { Caption="下班时间",FieldName="GeneratorDate",Visible=true}
            //});
            //gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            //gridView});
            //gc.MainView = gridView;
            ////gridControl1.DataSource = commonAction.GetAllColorList();
            //gridView.BestFitColumns();//按照列宽度自动适配
            //gridView.GridControl = gc;
            //gridView.OptionsView.ShowFooter = false;
            //gridView.OptionsView.ShowGroupPanel = false;
            //gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            //gridView.OptionsSelection.MultiSelect = true;
            //gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected override void Closing(object sender, EventArgs e)
        {
            base.Closing(sender, e);
        }
    }
}
