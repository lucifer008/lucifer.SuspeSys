using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuspeSys.Client.Action.Common;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Client.Action.Permission;
using DevExpress.XtraEditors;
using SuspeSys.Domain;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using SuspeSys.Client.Action;

namespace SuspeSys.Client.Modules.Permission
{
    public partial class UserAdd : Ext.SusXtraUserControl
    {
        private UsersAddAction _action = new UsersAddAction();
        private Domain.Users _user;


        private UserAdd()
        {
            InitializeComponent();
        }

        public UserAdd(XtraUserControl1 ucMain, string userId) : base(ucMain)
        {
            InitializeComponent();
            this.gridView1.OptionsDetail.EnableMasterViewMode = true;
            this.gridView1.DataController.AllowIEnumerableDetails = true;
            this.gridView2.DataController.AllowIEnumerableDetails = true;

            #region 设置表头

            gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="Id",FieldName= "Id",Visible=false},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="选择",FieldName="Checked",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="流水线号",FieldName="PipeliNo",Visible=true,Name="PipeliNo"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="用户卡号",FieldName="CardNo",Visible=true},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="推杆数量",FieldName="PushRodNum",Visible=true,Name="PuttQuantity"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="组编码",FieldName="GroupNO",Visible=true,Name="GroupNO"},
                new DevExpress.XtraGrid.Columns.GridColumn() { Caption="主轨号",FieldName="MainTrackNumber",Visible=true,Name="MainTrackNumber"},
                //new DevExpress.XtraGrid.Columns.GridColumn() { Caption="推杆数量",FieldName="PushRodNum",Visible=true},
            });

            gridView2.Columns["PipeliNo"].OptionsColumn.AllowFocus = false;
            gridView2.Columns["PushRodNum"].OptionsColumn.AllowFocus = false;

            gridView2.Columns["GroupNO"].OptionsColumn.AllowFocus = false;
            //gridView.Columns["Deleted"].
            gridView2.Columns["MainTrackNumber"].OptionsColumn.AllowFocus = false;
            #endregion

            susToolBar1.OnButtonClick += SusToolBar1_OnButtonClick;
            //base.SusToolBar.ShowAddButton = false;
            this.InitFormData(userId);
            susToolBar1.ShowCancelButton = false;
            susToolBar1.ShowAddButton = false;
            susToolBar1.ShowDeleteButton = false;
            susToolBar1.ShowExportButton = false;
            susToolBar1.ShowSaveAndAddButton = false;
        }

        private void ExpandAll()
        {
            MainView.BeginUpdate();
            try
            {
                for (int i = 0; i < MainView.RowCount; i++)
                    MainView.SetMasterRowExpanded(i, true);
            }
            finally
            {
                MainView.EndUpdate();
            }
        }
        GridView MainView { get { return gridView1; } }
        private void InitFormData(string userId)
        {

            //获取员工列表
            var employee = CommonAction.GetList<Domain.EmployeeModel>(true).Where(o => o.Deleted == 0);
            gdEmployee.Properties.DataSource = employee;

            gdEmployee.Properties.Columns.Add(new LookUpColumnInfo("Code", "编码"));
            gdEmployee.Properties.Columns.Add(new LookUpColumnInfo("RealName", "姓名"));
            gdEmployee.Properties.Columns.Add(new LookUpColumnInfo("SexName", "性别") );

            gdEmployee.Properties.ValueMember = "Id";
            gdEmployee.Properties.DisplayMember = "RealName";
            gdEmployee.Properties.ShowHeader = false;


            var userDto = _action.GetUserInfoByUserId(userId);
            _user = userDto.Users;

            if (_user == null || string.IsNullOrEmpty(_user.Password))
                btnResetPassword.Visible = false;
            else
                btnResetPassword.Visible = true;

            //绑定角色
            chcList_Roles.DataSource = userDto.Roles;
            chcList_Roles.CheckOnClick = true;
            chcList_Roles.DisplayMember = "ActionName";
            chcList_Roles.ValueMember = "Id";
            chcList_Roles.CheckMember = "Checked";


            #region 客户机绑定


            this.gridControl.DataSource = userDto.UserClientMachines;

            this.gridControl.ShowOnlyPredefinedDetails = true;
            MainView.OptionsView.ShowGroupPanel = false;
            gridView2.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowViewCaption = false;
            gridView2.OptionsView.ShowViewCaption = false;
            #endregion

            if (!string.IsNullOrEmpty(_user.Id))
            {
                txtCardNo.EditValue = _user.CardNo;
                txtUserName.EditValue = _user.UserName;
                gdEmployee.EditValue = _user.Employee.Id;

                txtUserName.ReadOnly = true;
            }

            ExpandAll();
        }

        private void SusToolBar1_OnButtonClick(ButtonName ButtonName)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                switch (ButtonName)
                {
                    case ButtonName.Save:
                        Save();
                        break;
                    case ButtonName.Close:
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                _user.Id = string.Empty;
                //XtraMessageBox.Show(ex.Message, "错误");
                XtraMessageBox.Show(ex.Message, Client.Action.LanguageAction.Instance.BindLanguageTxt("errorInfo"));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        protected void Save()
        {
            if (!Valid())
                return;

            UserDto dto = new UserDto();
            dto.Users = _user;


            dto.Roles = chcList_Roles.DataSource as IList<RolesModel>;
            dto.UserClientMachines = gridControl.DataSource as IList<ClientMachinesModel>;

            _action.SaveUserInfo(dto);

            //XtraMessageBox.Show("保存成功!", "提示");
            XtraMessageBox.Show(Client.Action.LanguageAction.Instance.BindLanguageTxt("promptSaveSuccess"), Client.Action.LanguageAction.Instance.BindLanguageTxt("promptTips"));

        }

        protected bool Valid()
        {
            if (txtUserName.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("用户名不能为空", "提示");
                return false;
            }


            if (txtPassword.Text.Trim().Length > 0 &&
                txtConfirmPassword.Text.Trim().Length > 0 )
            {

                if (txtPassword.Text.Trim().Length == 0)
                {
                    XtraMessageBox.Show("密码不能为空", "提示");
                    return false;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    XtraMessageBox.Show("密码输入不一致", "提示");
                    return false;
                }
                _user.Password = SuspeSys.Utils.Security.MD5.Encrypt(txtPassword.Text.Trim());

            }

            if (gdEmployee.EditValue == null || gdEmployee.EditValue.ToString().Trim().Length == 0 ||
                gdEmployee.Properties.NullText == gdEmployee.EditValue.ToString())
            {
                XtraMessageBox.Show("请选择员工信息", "提示");
                return false;
            }
            _user.UserName = txtUserName.Text.Trim();
            _user.Employee = new Employee();
            _user.CardNo = txtCardNo.Text.Trim();
            _user.Employee.Id = gdEmployee.EditValue.ToString();

            return true;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_user.Id))
                return;

            frmLoginAction _action = new frmLoginAction();

            string password = "123";
            try
            {
                _action.ResetPassword(_user.UserName, password);
                XtraMessageBox.Show($"密码已经重置为 {password}", "提示信息");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}
