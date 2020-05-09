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
using SuspeSys.Client.Action.Common;
using DevExpress.XtraEditors.Controls;
using SuspeSys.Domain;
using DevExpress.XtraEditors.Mask;

namespace SuspeSys.Client.Modules.ProductionLineSet
{
    public partial class HangUpLineSet : SusXtraUserControl
    {
        SystemParameterAction _action = new SystemParameterAction();

        List<Domain.SystemModuleParameterModel> ParameterModel;
        public HangUpLineSet()
        {
            InitializeComponent();
            lookUpHangUp.EditValueChanged += LookUpHangUp_EditValueChanged;
            lookUpProduct.EditValueChanged += ProductLine_EditValueChanged;
            lookUpOther.EditValueChanged += Other_EditValueChanged;
            this.lblMessage1.Hide();
            this.lblMessage2.Hide();
            this.lblMessage3.Hide();
        }

        public HangUpLineSet(XtraUserControl1 xuc) : this() { }

        

        private void HangUpLineSet_Load(object sender, EventArgs e)
        {
            this.InitPageData();
        }

        bool _isPageInit = true;

        private void InitPageData()
        {
            _isPageInit = true;
            var lines = _action.GetPipelining();

            if (lines == null || lines.Count() == 0)
            {
                XtraMessageBox.Show("未获取到生产线数据，请联系管理员");
                return;
            }

            var sets = _action.GetAllSystemModuleParameter();
            ParameterModel = sets;

            #region 生产线下拉列表初始化
            lookUpHangUp.Properties.DataSource = lines;
            lookUpHangUp.Properties.NullText = "请选择生产线";
            lookUpHangUp.Properties.Columns.Add(new LookUpColumnInfo("GroupNO", "组编码"));
            lookUpHangUp.Properties.Columns.Add(new LookUpColumnInfo("GroupName", "组名"));
            lookUpHangUp.Properties.Columns.Add(new LookUpColumnInfo("PipeliNo", "生产线编码"));


            lookUpHangUp.Properties.ValueMember = "Id";
            lookUpHangUp.Properties.DisplayMember = "PipeliNo";
            

            lookUpProduct.Properties.DataSource = lines;
            lookUpProduct.Properties.NullText = "请选择生产线";
            lookUpProduct.Properties.Columns.Add(new LookUpColumnInfo("GroupNO", "组编码"));
            lookUpProduct.Properties.Columns.Add(new LookUpColumnInfo("GroupName", "组名"));
            lookUpProduct.Properties.Columns.Add(new LookUpColumnInfo("PipeliNo", "生产线编码"));

            lookUpProduct.Properties.ValueMember = "Id";
            lookUpProduct.Properties.DisplayMember = "PipeliNo";
            //lookUpHangUp.Properties.ShowHeader = false;
            lookUpProduct.ItemIndex = 0;
            


            lookUpOther.Properties.DataSource = lines;
            lookUpOther.Properties.NullText = "请选择生产线";
            lookUpOther.Properties.Columns.Add(new LookUpColumnInfo("GroupNO", "组编码"));
            lookUpOther.Properties.Columns.Add(new LookUpColumnInfo("GroupName", "组名"));
            lookUpOther.Properties.Columns.Add(new LookUpColumnInfo("PipeliNo", "生产线编码"));

            lookUpOther.Properties.ValueMember = "Id";
            lookUpOther.Properties.DisplayMember = "PipeliNo";
            //lookUpHangUp.Properties.ShowHeader = false;
            lookUpOther.ItemIndex = 0;

            #endregion



            #region 挂片站
            {
                var sysParaId = sets.Where(o => o.SecondModuleType == 2).First().Id;
                var setsKaoqing = sets.Where(o => o.SystemModuleParameterId == sysParaId).ToList();

                SystemParameterHelp.InitControl(this.panelHangUp, setsKaoqing,
                    checkBox_CheckedChange,
                    dropDown_SelectedChange,
                    Number_Validated,
                    GetSystemModuleParameterValueHangUp);
            }
            #endregion

            #region 生产
            {
                var sysParaId = sets.Where(o => o.SecondModuleType == 3).First().Id;
                var setsKaoqing = sets.Where(o => o.SystemModuleParameterId == sysParaId).ToList();

                SystemParameterHelp.InitControl(this.panelProduct, setsKaoqing,
                    checkBox_CheckedChange,
                    dropDown_SelectedChange,
                    Number_Validated,
                    GetSystemModuleParameterValueProduct);
            }
            #endregion

            #region 其他
            {
                var sysParaId = sets.Where(o => o.SecondModuleType == 4).First().Id;
                var setsKaoqing = sets.Where(o => o.SystemModuleParameterId == sysParaId).ToList();

                SystemParameterHelp.InitControl(this.panelOther, setsKaoqing,
                    checkBox_CheckedChange,
                    dropDown_SelectedChange,
                    Number_Validated,
                    GetSystemModuleParameterValueOther);
            }
            #endregion

            lookUpHangUp.EditValue = lines.First().Id;
            lookUpProduct.EditValue = lines.First().Id;
            lookUpOther.EditValue = lines.First().Id;

            _isPageInit = false;
        }

        #region 事件
        private string GetSystemModuleParameterValueOther(SystemModuleParameterModel model)
        {
            string productId = lookUpOther.EditValue?.ToString();
            return model.SystemModuleParameterValueList.FirstOrDefault(o => o.ProductLineId == productId)?.ParameterValue;
        }

        private string GetSystemModuleParameterValueProduct(SystemModuleParameterModel model)
        {
            string productId = lookUpProduct.EditValue?.ToString();
            return model.SystemModuleParameterValueList.FirstOrDefault(o => o.ProductLineId == productId)?.ParameterValue;
        }

        /// <summary>
        /// 挂片站
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetSystemModuleParameterValueHangUp(SystemModuleParameterModel model)
        {
            string productId = lookUpHangUp.EditValue?.ToString();
            return model.SystemModuleParameterValueList.FirstOrDefault(o => o.ProductLineId == productId)?.ParameterValue;
        }

        private void Number_Validated(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Control control = sender as Control;
                if (sender is TextEdit)
                {
                    var systemPara = control.Tag as SystemModuleParameterModel;

                    TextEdit text = sender as TextEdit;

                    SystemModuleParameterValue paraValue = new SystemModuleParameterValue()
                    {
                        SystemModuleParameter = systemPara,
                        ParameterValue = text.Text
                    };

                    SaveParameterValue(paraValue);
                }
            }
        }

        private void dropDown_SelectedChange(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (sender is ComboBoxEdit)
            {
                var systemPara = control.Tag as SystemModuleParameterModel;

                ComboBoxEdit text = sender as ComboBoxEdit;
                var selected = text.SelectedItem as SusComboBoxItem;

                if (selected == null)
                    return;

                SystemModuleParameterValue paraValue = new SystemModuleParameterValue()
                {
                    SystemModuleParameter = systemPara,
                    ParameterValue = selected.Value.ToString()
                };

                SaveParameterValue(paraValue);
            }
        }

        private void checkBox_CheckedChange(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (sender is CheckEdit)
            {
                var systemPara = control.Tag as SystemModuleParameterModel;

                CheckEdit check = sender as CheckEdit;

                SystemModuleParameterValue paraValue = new SystemModuleParameterValue()
                {
                    SystemModuleParameter = systemPara,
                    ParameterValue = check.Checked == true ? "1" : "0"
                };

                SaveParameterValue(paraValue);
            }
        }
        #endregion



        #region 吊挂线 生产先选择初始化
        bool isInit = false;
        private void LookUpHangUp_EditValueChanged(object sender, EventArgs e)
        {
            isInit = true;
            if (this.lookUpHangUp.EditValue == null)
                return;

            string lineId = this.lookUpHangUp.EditValue.ToString();
            this.DropChangedInitProductData(lineId, panelHangUp);
            //初始化数据
            //throw new NotImplementedException();

            isInit = false;
        }

        #endregion

        #region 生产线 生产先选择初始化
        bool isInitProduct = false;
        private void ProductLine_EditValueChanged(object sender, EventArgs e)
        {
            isInitProduct = true;
            if (this.lookUpProduct.EditValue == null)
                return;

            string lineId = this.lookUpProduct.EditValue.ToString();
            this.DropChangedInitProductData(lineId, panelProduct);
            //初始化数据
            //throw new NotImplementedException();

            isInitProduct = false;
        }

        private void DropChangedInitProductData(string lineId, Control control)
        {
            if (ParameterModel != null)
            {
                //获取panel内的控件
                var controls = control.Controls;

                foreach (Control item in controls)
                {
                    //判断是否是系统参数
                    if (!(item.Tag != null && item.Tag is SystemModuleParameterModel))
                        continue;

                    //处理业务
                    var systemPara = item.Tag as SystemModuleParameterModel;
                    if (systemPara == null)
                        continue;

                    //获取参数值 
                    systemPara = ParameterModel.FirstOrDefault(o => o.Id == systemPara.Id);
                    if (systemPara == null)
                        continue;

                    var paraValue = systemPara.SystemModuleParameterValueList.FirstOrDefault(o => o.ProductLineId == lineId);
                    //没有设置继续
                    //if (paraValue == null || string.IsNullOrEmpty( paraValue.ParameterValue))
                    //    continue;

                    if (item is CheckEdit)
                    {
                        var check = item as CheckEdit;
                        if (paraValue == null || string.IsNullOrEmpty(paraValue.ParameterValue))
                            check.Checked = false;
                        else
                            check.Checked = paraValue.ParameterValue.Equals("1");
                    }
                    else if (item is ComboBoxEdit)
                    {
                        var dropdown = item as ComboBoxEdit;
                        if (paraValue == null || string.IsNullOrEmpty(paraValue.ParameterValue))
                            dropdown.SelectedIndex = -1;
                        else
                        {
                            var items = dropdown.Properties.Items;
                            foreach (var dpItem in items)
                            {
                                var susItem = dpItem as SusComboBoxItem;
                                if (susItem == null)
                                    return;

                                if (susItem.Value.ToString() == paraValue.ParameterValue)
                                {
                                    dropdown.SelectedItem = dpItem;
                                    break;
                                }
                            }
                            //dropdown.EditValue =;
                        }

                    }
                    else if (item is TextEdit)
                    {
                        var textEdit = item as TextEdit;

                        if (paraValue == null || string.IsNullOrEmpty(paraValue.ParameterValue))
                            textEdit.Text = string.Empty;
                        else
                            textEdit.Text = paraValue.ParameterValue;
                    }

                }
            }
        }
        #endregion

        #region 其他 生产先选择初始化
        bool isInitOther = false;
        private void Other_EditValueChanged(object sender, EventArgs e)
        {
            isInitOther = true; 
            if (this.lookUpOther.EditValue == null)
                return;

            string lineId = this.lookUpOther.EditValue.ToString();
            this.DropChangedInitProductData(lineId, panelOther);
            //初始化数据
            //throw new NotImplementedException();

            isInitOther = false;
        }

        #endregion


        private void SaveParameterValue(SystemModuleParameterValue parameterValue)
        {
            if (_isPageInit)
                return;

            string productLineId = GetCurrentProductLineId();

            parameterValue.ProductLineId = productLineId;

            _action.SaveOrUpdate(parameterValue);




            this.ShowMessage("设置成功");
            ParameterModel = _action.GetAllSystemModuleParameter();
        }

        private string GetCurrentProductLineId()
        {
            string productLineId = string.Empty;

            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                productLineId = lookUpHangUp.EditValue.ToString();
            }
            else if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                productLineId = lookUpProduct.EditValue.ToString();
            }
            else if (xtraTabControl1.SelectedTabPage == xtraTabPage3)
            {
                productLineId = lookUpOther.EditValue.ToString();
            }

            return productLineId;
        }

       

        #region 页面消息
        private void ShowMessage(string msg)
        {
            this.lblMessage1.Show();
            this.lblMessage2.Show();
            this.lblMessage3.Show();

            lblMessage1.Text = msg;
            lblMessage2.Text = msg;
            lblMessage3.Text = msg;

            this.timer1.Interval = 1000;
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.lblMessage1.Hide();
            this.lblMessage2.Hide();
            this.lblMessage3.Hide();
        }
        #endregion

        private void xtraTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            //this.InitPageControl();
        }

        private void xtraTabControl1_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            //this.InitPageControl();
        }
    }




}
