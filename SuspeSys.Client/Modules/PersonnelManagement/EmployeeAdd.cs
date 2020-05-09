
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Client.Action.Common;
using SuspeSys.Client.Action.PersonnelManagement;
using SuspeSys.Domain;
using SuspeSys.Domain.SusEnum;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Modules.PersonnelManagement
{
    public partial class EmployeeAdd : Ext.SusXtraUserControl
    {
        EmployeeAddAction _action = new EmployeeAddAction();

        public EmployeeAdd()
        {
            InitializeComponent();

            this.susToolBar1.ShowRefreshButton = false;
            this.susToolBar1.ShowModifyButton = false;
            this.susToolBar1.ShowSaveAndAddButton = true;
            this.susToolBar1.ShowSaveAndCloseButton = true;
            this.susToolBar1.ShowAddButton = false;
            this.susToolBar1.ShowDeleteButton = false;
            this.susToolBar1.ShowExportButton = false;
            this.susToolBar1.ShowCancelButton = false;

            //base.SusToolBar.ShowSaveAndAddButton = true;
            //base.SusToolBar.ShowSaveAndCloseButton = true;
        }

        private Domain.Employee _employee;


        public EmployeeAdd(XtraUserControl1 ucMain, string moduleId, Domain.Employee employee) : this()
        {
            //processFlowMain1.ProcessFlowSusToolBar = susToolBar1;
            this.ucMain = ucMain;
            //processFlowMain1.ucMain = ucMain;

            if (employee == null)
            {
                _employee = new Domain.Employee();
            }
            else
            {
                _employee = new CommonAction().Get<Domain.Employee>(employee.Id);
            }

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
                        Save();
                        break;
                    //case ButtonName.Add:
                    //    AddItem(this.susGrid1.DataGrid);

                    //    break;
                    //case ButtonName.Refresh:
                    //    this.Query(1);
                    //    break;
                    case ButtonName.Close:

                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    case ButtonName.SaveAndAdd:
                        Save();
                        _employee = new Employee();
                        BindModel();
                        txtEmployeeCode.ReadOnly = false;
                        break;
                    case ButtonName.SaveAndClose:
                        Save();
                        ucMain.MainTabControl.TabPages.RemoveAt(ucMain.MainTabControl.SelectedTabPageIndex);
                        break;
                    //case ButtonName.Delete:
                    //    Delete(this.susGrid1.DataGrid);
                    //    break;
                    default:
                        XtraMessageBox.Show("开发中....值得期待!", "提示");
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
                Cursor = Cursors.Default;
            }
        }

        private void Save()
        {
            this.SetModel();
            var action = new CommonAction();

            //Update
            if (!string.IsNullOrEmpty(_employee.Id))
            {
                var employees = CommonAction.GetList<Employee>();

                if (employees != null && employees.Any(o => !string.IsNullOrEmpty(o.Code) &&
                                                            o.Code.Trim() == _employee.Code.Trim() &&
                                                            o.Id != _employee.Id))
                {
                    XtraMessageBox.Show("工号已存在", "提示");
                    return;
                }
                if (!string.IsNullOrEmpty(_employee.CardNo.Trim()))
                {
                    if (employees != null && employees.Any(o => !string.IsNullOrEmpty(o.CardNo) &&
                                                                o.CardNo.Trim() == _employee.CardNo.Trim() &&
                                                                o.Id != _employee.Id))
                    {
                        XtraMessageBox.Show("工卡卡号已经存在", "提示");
                        return;
                    }
                }

                action.UpdateLog(_employee.Id, _employee, "员工管理", "EmployeeAdd", _employee.GetType().Name);

                action.Update<Domain.Employee>(_employee);
                //先删除 ，在添加
                _action.RemoveEmployeePosition(_employee.Id);

                if (ddlPosition.EditValue != null)
                {
                    Domain.EmployeePositions model = new Domain.EmployeePositions();
                    model.Employee = _employee;
                    model.Position = new Domain.Position() { Id = ddlPosition.EditValue.ToString() };
                    action.Save<Domain.EmployeePositions>(model);

                    action.InsertLog<Domain.Department>(model.Id, "员工管理", "EmployeeAdd", model.GetType().Name);

                }
                if (string.IsNullOrEmpty(_employee.CardNo.Trim()))
                {
                    var cardRelationList = CommonAction.GetList<Domain.EmployeeCardRelation>();
                    var emRleation = cardRelationList.Where(f => f.Employee != null && f.Employee.Id.Equals(_employee.Id));
                    if (emRleation.Count() == 0)
                    {
                        var cardNo = _employee.CardNo;
                        short cardType = 4;
                        if (string.IsNullOrEmpty(_employee.CardNo))
                        {
                            cardNo = "-1";
                            cardType = 5;//临时人员分配一个临时卡
                        }
                        //添加CardInfo
                        CardInfo cardInfo = new CardInfo()
                        {
                            CardNo = cardNo,
                            IsEnabled = true,
                            IsMultiLogin = false,
                            CardType = cardType
                        };

                        action.Save<Domain.CardInfo>(cardInfo);

                        EmployeeCardRelation relation = new EmployeeCardRelation()
                        {
                            CardInfo = cardInfo,
                            Employee = _employee,
                        };
                        action.Save<Domain.EmployeeCardRelation>(relation);
                    }
                    else
                    {
                        var emRelationModel = emRleation.ToList()[0];
                        var cInfo = emRelationModel.CardInfo;
                        cInfo.CardNo = "-1";
                        cInfo.CardType = 5;
                        action.Update<Domain.CardInfo>(cInfo);

                    }
                }
                
            }
            else
            {
                //Add
                var employees = CommonAction.GetList<Employee>();
                var cardInfos = CommonAction.GetList<CardInfo>();

                if (employees != null && employees.Any(o => !string.IsNullOrEmpty(o.Code) && o.Code.Trim() == _employee.Code.Trim() && (!o.Deleted.HasValue || o.Deleted == 0)))
                {
                    XtraMessageBox.Show("工号已存在", "提示");
                    return;
                }
                if (!string.IsNullOrEmpty(_employee.CardNo.Trim()))
                {
                    if (employees != null && employees.Any(o => !string.IsNullOrEmpty(o.CardNo) &&
                                            o.CardNo.Trim() == _employee.CardNo.Trim()))
                    {
                        XtraMessageBox.Show("工卡卡号已经存在", "提示");
                        return;
                    }

                    if (cardInfos != null && cardInfos.Any(o => o.CardNo != null && o.CardNo.Trim() == _employee.CardNo.Trim()))
                    {
                        XtraMessageBox.Show("工卡卡号已经存在", "提示");
                        return;
                    }
                }

                action.Save<Domain.Employee>(_employee);
                var cardNo = _employee.CardNo;
                short cardType = 4;
                if (string.IsNullOrEmpty(_employee.CardNo))
                {
                    cardNo = "-1";
                    cardType = 5;//临时人员分配一个临时卡
                }
                //添加CardInfo
                CardInfo cardInfo = new CardInfo()
                {
                    CardNo = cardNo,
                    IsEnabled = true,
                    IsMultiLogin = false,
                    CardType = cardType
                };

                action.Save<Domain.CardInfo>(cardInfo);

                EmployeeCardRelation relation = new EmployeeCardRelation()
                {
                    CardInfo = cardInfo,
                    Employee = _employee,
                };
                action.Save<Domain.EmployeeCardRelation>(relation);



                action.InsertLog<Domain.Employee>(_employee.Id, "员工管理", "EmployeeAdd", _employee.GetType().Name);

                if (ddlPosition.EditValue != null)
                {
                    Domain.EmployeePositions model = new Domain.EmployeePositions();
                    model.Employee = _employee;
                    model.Position = new Domain.Position() { Id = ddlPosition.EditValue.ToString() };
                    action.Save<Domain.EmployeePositions>(model);

                    action.InsertLog<Domain.Department>(model.Id, "员工管理", "EmployeeAdd", model.GetType().Name);

                }

            }

            XtraMessageBox.Show("保存完成", "提示");
        }

        private void EmployeeAdd_Load(object sender, EventArgs e)
        {
            BindControlData();

            if (!string.IsNullOrEmpty(this._employee.Id))
                BindModel();
        }

        /// <summary>
        /// 绑定控件数据
        /// </summary>
        private void BindControlData()
        {
            //绑定性别
            var dic = EnumHelper.GetDictionary(typeof(Support.Enums.Sex));
            foreach (var item in dic)
            {
                radioSex.Properties.Items.Add(new RadioGroupItem(item.Key, item.Value));
            }

            radioSex.SelectedIndex = 0;

            //绑定部门
            ddlDeptment.Properties.DataSource = Action.Common.CommonAction.GetList<Domain.DepartmentModel>(true);
            ddlDeptment.Properties.Columns.Add(new LookUpColumnInfo("DepName"));
            ddlDeptment.Properties.ValueMember = "Id";
            ddlDeptment.Properties.DisplayMember = "DepName";
            ddlDeptment.Properties.ShowHeader = false;
            ddlDeptment.Properties.NullText = "请选择部门";

            //ddlDeptment.EditValue = "5b45404e1ae5469890f768d18ecee6ca";

            //绑定职位
            ddlPosition.Properties.DataSource = Action.Common.CommonAction.GetList<Domain.PositionModel>(true);
            ddlPosition.Properties.Columns.Add(new LookUpColumnInfo("PosName"));
            ddlPosition.Properties.ValueMember = "Id";
            ddlPosition.Properties.DisplayMember = "PosName";
            ddlPosition.Properties.ShowHeader = false;
            ddlPosition.Properties.NullText = "请选择职位";

            ddlWorkType.Properties.DataSource = Action.Common.CommonAction.GetList<Domain.WorkTypeModel>(true);
            ddlWorkType.Properties.Columns.Add(new LookUpColumnInfo("WTypeName"));
            ddlWorkType.Properties.ValueMember = "Id";
            ddlWorkType.Properties.DisplayMember = "WTypeName";
            ddlWorkType.Properties.ShowHeader = false;
            ddlWorkType.Properties.NullText = "请选择工种";

            //生产组别
            ddlSiteGroup.Properties.DataSource = Action.Common.CommonAction.GetList<Domain.SiteGroup>(true);
            ddlSiteGroup.Properties.ValueMember = "Id";
            ddlSiteGroup.Properties.NullText = "请选择组别";
            ddlSiteGroup.Properties.DisplayMember = "GroupName";
            //ddlSiteGroup.Properties.DisplayMember = "";

            //获取省信息
            ddlProvince.Properties.DataSource = Action.Common.CommonAction.GetList<Domain.Province>(true);
            ddlProvince.Properties.Columns.Add(new LookUpColumnInfo("ProvinceName"));
            ddlProvince.Properties.ValueMember = "Id";
            ddlProvince.Properties.NullText = "请选择省份";
            ddlProvince.Properties.ShowHeader = false;
            ddlProvince.Properties.DisplayMember = "ProvinceName";


            //获取地区
            ddlCity.Properties.DataSource = null;
            ddlCity.Properties.Columns.Add(new LookUpColumnInfo("CityName"));
            ddlCity.Properties.ValueMember = "Id";
            ddlCity.Properties.NullText = "请选择地区";
            ddlCity.Properties.ShowHeader = false;
            ddlCity.Properties.DisplayMember = "CityName";

            //获取地区
            ddlArea.Properties.DataSource = null;
            ddlArea.Properties.Columns.Add(new LookUpColumnInfo("AreaName"));
            ddlArea.Properties.ValueMember = "Id";
            ddlArea.Properties.NullText = "请选择区县";
            ddlArea.Properties.ShowHeader = false;
            ddlArea.Properties.DisplayMember = "AreaName";

            string[] filters = {"All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff|"
                                ,"Windows Bitmap(*.bmp)|*.bmp|"
                                ,"Windows Icon(*.ico)|*.ico|"
                                ,"Graphics Interchange Format (*.gif)|(*.gif)|"
                                ,"JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|"
                                ,"Portable Network Graphics (*.png)|*.png|"
                                ,"Tag Image File Format (*.tif)|*.tif;*.tiff" };

            openFileDialog.Filter = string.Join("", filters);
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }


        #region 照片相关
        private void btnSelectImg_Click(object sender, EventArgs e)
        {
            DialogResult dialog = openFileDialog.ShowDialog();

            if (DialogResult.OK == dialog && !string.IsNullOrEmpty(openFileDialog.FileName))
            {
                Image img = Image.FromFile(openFileDialog.FileName);
                this.pictureUser.Image = img;

                _employee.HeadImage = imageToByteArray(img);
            }
        }



        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;

        }

        private void btnDeleteImg_Click(object sender, EventArgs e)
        {
            _employee.HeadImage = null;
            this.pictureUser.Image = null;
        }
        #endregion

        #region 区划相关
        private void ddlProvince_EditValueChanged(object sender, EventArgs e)
        {
            if (ddlProvince.EditValue == null || string.IsNullOrEmpty(ddlProvince.EditValue.ToString()))
                return;


            ddlCity.Properties.DataSource = _action.GetCityListByProvinceId(ddlProvince.EditValue.ToString());

            //if (_employee.Area != null && !string.IsNullOrEmpty(_employee.Area.Id))
            //    ddlCity.EditValue = _employee.Area.City.Id;

            ddlArea.Properties.DataSource = null;
        }

        private void ddlCity_EditValueChanged(object sender, EventArgs e)
        {
            if (ddlCity.EditValue == null || string.IsNullOrEmpty(ddlCity.EditValue.ToString()))
                return;


            ddlArea.Properties.DataSource = _action.GetAreaListByCityId(ddlCity.EditValue.ToString());

            //if (_employee.Area != null && !string.IsNullOrEmpty(_employee.Area.Id))
            //    ddlArea.EditValue = _employee.Area.Id;

        }
        #endregion

        #region 绑定Model
        private void BindModel()
        {
            txtEmployeeCode.ReadOnly = true;
            txtEmployeeCode.Text = _employee.Code;
            txtCardNO.Text = _employee.CardNo;
            txtRealName.Text = _employee.RealName;

            int sex = _employee.Sex.HasValue ? (int)_employee.Sex : 1;

            txtEmail.Text = _employee.Email;
            txtMobile.Text = _employee.Mobile;
            txtPhone.Text = _employee.Phone;
            txtBirthday.EditValue = _employee.Birthday?.ToString();

            txtEmploymentDate.EditValue = _employee.EmploymentDate;
            //工卡卡号
            //_employee.EmploymentDate = txtemp

            ddlDeptment.EditValue = _employee.Department?.Id;


            ddlWorkType.EditValue = _employee.WorkType?.Id;

            txtStartingDate.EditValue = _employee.StartingDate;
            txtLeaveDate.EditValue = _employee.LeaveDate;

            ddlSiteGroup.EditValue = _employee.SiteGroup?.Id;


            txtAddress.Text = _employee.Address;
            txtBankNO.Text = _employee.BankCardNo;

            if (_employee.HeadImage != null)
                this.pictureUser.Image = this.byteArrayToImage(_employee.HeadImage);

            if (_employee.Area != null)
            {
                this.ddlProvince.EditValue = _employee.Area.City.Province.Id;
                this.ddlCity.EditValue = _employee.Area.City.Id;
                this.ddlArea.EditValue = _employee.Area.Id;
            }

            if (!string.IsNullOrWhiteSpace(_employee.Id))
            {
                //获取职位信息
                var dbPositions = _action.GetPositionsByEmployeeId(_employee.Id);
                if (dbPositions != null && dbPositions.Count > 0)
                    ddlPosition.EditValue = dbPositions.First().Position.Id;
            }
        }

        private void SetModel()
        {
            _employee.Code = txtEmployeeCode.Text.Trim();
            if (string.IsNullOrEmpty(txtCardNO.Text))
                _employee.CardNo = string.Empty;
            else
                _employee.CardNo = int.Parse(txtCardNO.Text.Trim()).ToString();

            _employee.RealName = txtRealName.Text.Trim();
            _employee.Sex = Convert.ToByte(radioSex.EditValue);
            _employee.Email = txtEmail.Text.Trim();
            _employee.Mobile = txtMobile.Text.Trim();
            _employee.Phone = txtPhone.Text.Trim();
            _employee.Birthday = ExtUtils.ToDateTime(txtBirthday.EditValue);

            _employee.EmploymentDate = ExtUtils.ToDateTime(txtEmploymentDate.EditValue);
            //工卡卡号
            //_employee.EmploymentDate = txtemp

            if (_employee.Department == null && ddlDeptment.EditValue != null)
                _employee.Department = new Domain.Department();

            if (ddlDeptment.EditValue != null)
                _employee.Department.Id = Convert.ToString(ddlDeptment.EditValue);
            //_employee 职务
            //生产组别
            if (_employee.WorkType == null && ddlWorkType.EditValue != null)
                _employee.WorkType = new Domain.WorkType();

            if (ddlWorkType.EditValue != null)
                _employee.WorkType.Id = ddlWorkType.EditValue?.ToString();

            _employee.StartingDate = ExtUtils.ToDateTime(txtStartingDate.EditValue);

            _employee.LeaveDate = ExtUtils.ToDateTime(txtLeaveDate.EditValue);


            //家庭住址
            if (_employee.Area == null && ddlArea.EditValue != null)
                _employee.Area = new Domain.Area();

            if (ddlArea.EditValue != null)
                _employee.Area.Id = ddlArea.EditValue?.ToString();

            if (_employee.SiteGroup == null && ddlSiteGroup.EditValue != null)
                _employee.SiteGroup = new Domain.SiteGroup();
            if (ddlSiteGroup.EditValue != null)
                _employee.SiteGroup.Id = ddlSiteGroup.EditValue?.ToString();


            _employee.Address = txtAddress.Text.Trim();
            _employee.BankCardNo = txtBankNO.Text.Trim();

            if (_employee.LeaveDate.HasValue)
                _employee.Valid = false;
            else
                _employee.Valid = true;

        }
        #endregion

    }
}
