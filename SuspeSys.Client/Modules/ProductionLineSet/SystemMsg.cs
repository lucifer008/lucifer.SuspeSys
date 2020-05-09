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
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraBars.Navigation;
using SuspeSys.Client.Modules.Ext;
using SuspeSys.Domain;
using SuspeSys.Client.Action.Common;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.Utils;
using System.Windows.Input;

namespace SuspeSys.Client.Modules.ProductionLineSet
{
    public partial class SystemMsg : SuspeSys.Client.Modules.Ext.SusXtraUserControl
    {
        SystemParameterAction _action = new SystemParameterAction();

        List<Domain.SystemModuleParameterModel> ParameterModel;

        public SystemMsg()
        {
            InitializeComponent();
            this.lblMessage1.Hide();
            this.lblMessage2.Hide();
            this.lblMessage3.Hide();
        }
        public SystemMsg(XtraUserControl1 xuc) : this() {
            
        }


        private void SystemMsg_Load(object sender, EventArgs e)
        {
            InitPageData();
        }

        private void InitPageData()
        {
            var sets = _action.GetAllSystemModuleParameter();
            ParameterModel = sets;

            #region 初始化考勤
            {
                var sysParaId = sets.Where(o =>  o.SecondModuleType == 5).First().Id;
                var setsKaoqing= sets.Where(o => o.SystemModuleParameterId == sysParaId).ToList();

                SystemParameterHelp.InitControl(this.panelKaoQin, setsKaoqing,
                    checkBox_CheckedChange,
                    dropDown_SelectedChange,
                    Number_Validated,
                    GetSystemModuleParameterValue);
            }
            #endregion

            #region 初始化生产
            {
                var sysParaId = sets.Where(o => o.SecondModuleType == 6).First().Id;
                var setsKaoqing = sets.Where(o => o.SystemModuleParameterId == sysParaId).OrderBy(o => o.ParamterControlTitle).ToList();

                SystemParameterHelp.InitControl(this.panelProduct, setsKaoqing,
                    checkBox_CheckedChange,
                    dropDown_SelectedChange,
                    Number_Validated,
                    GetSystemModuleParameterValue);
            }
            #endregion

            #region 初始化其他
            {
                var sysParaId = sets.Where(o => o.SecondModuleType == 7).First().Id;
                var setsKaoqing = sets.Where(o => o.SystemModuleParameterId == sysParaId).ToList();

                SystemParameterHelp.InitControl(this.panelOther, setsKaoqing,
                    checkBox_CheckedChange,
                    dropDown_SelectedChange,
                    Number_Validated,
                    GetSystemModuleParameterValue);
            }
            #endregion
        }

        /// <summary>
        /// CheckBox事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void dropDown_SelectedChange(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (sender is ComboBoxEdit)
            {
                var systemPara = control.Tag as SystemModuleParameterModel;

                ComboBoxEdit text = sender as ComboBoxEdit;
                var selected = text.SelectedItem as SusComboBoxItem;
                SystemModuleParameterValue paraValue = new SystemModuleParameterValue()
                {
                    SystemModuleParameter = systemPara,
                    ParameterValue = selected.Value.ToString()
                };

                SaveParameterValue(paraValue);
            }
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

        private string GetSystemModuleParameterValue(SystemModuleParameterModel model)
        {
            return model.SystemModuleParameterValueList.FirstOrDefault()?.ParameterValue;
        }

        private void SaveParameterValue(SystemModuleParameterValue parameterValue)
        {

            _action.SaveOrUpdate(parameterValue);
 



            this.ShowMessage("设置成功");
            ParameterModel = _action.GetAllSystemModuleParameter();
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

    }

    public delegate string GetSystemModuleParameterValueHander(SystemModuleParameterModel model);
    public static class SystemParameterHelp
    {
        public static void InitControl(Control container, List<Domain.SystemModuleParameterModel> systemParameters,
                        EventHandler checkboxEvent, 
                        EventHandler dropdownEvent,
                        KeyPressEventHandler textBoxEvent,
                        GetSystemModuleParameterValueHander GetSystemModuleParameterValue)
        {
            Point point = new Point(3, 3);
            foreach (var item in systemParameters)
            {

                var paraValue = GetSystemModuleParameterValue(item);

                if (item.ParamterControlType == "checkbox")
                {
                    CheckEdit control;
                    control = new CheckEdit();
                    control.Text = item.ParamterControlTitle;
                    control.Tag = item;
                    //control.Dock = DockStyle.Top;
                    control.Location = point;
                    control.Width = 700;

                    container.Controls.Add(control);

                    control.CheckedChanged += checkboxEvent;

                    if (string.IsNullOrEmpty(paraValue))
                        control.Checked = false;
                    else
                        control.Checked = paraValue.Equals("1");

                }
                else if (item.ParamterControlType == "number")
                {
                    //添加ｌａｂｅｌ
                    LabelControl label = new LabelControl();
                    label.Text = item.ParamterControlTitle;
                    label.Location = new Point(point.X + 20, point.Y);

                    label.Width = 200;
                    container.Controls.Add(label);

                    var control = new TextEdit();
                    control.Tag = item;
                    control.Properties.Mask.EditMask = "n0";
                    control.Properties.Mask.MaskType = MaskType.Numeric;
                    control.Properties.Appearance.Options.UseTextOptions = true;
                    control.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                    control.Location = new Point(point.X + label.Width + 60, point.Y);

                    if (string.IsNullOrEmpty(paraValue))
                        control.Text = string.Empty;
                    else
                        control.Text = paraValue;

                    //control.ToolTip = "回车保存";
                    //control.Validated += textBoxEvent ;
                    control.KeyPress += textBoxEvent;
                    container.Controls.Add(control);
                }
                else if (item.ParamterControlType == "dropdown")
                {
                    //添加ｌａｂｅｌ
                    LabelControl label = new LabelControl();
                    label.Text = item.ParamterControlTitle;
                    label.Location = new Point(point.X + 20, point.Y);

                    label.Width = 200;
                    container.Controls.Add(label);

                    var control = new ComboBoxEdit();
                    control.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                    control.Tag = item;
                    control.Location = new Point(point.X + label.Width + 60, point.Y);

                    
                    foreach (var itemSub in item.SystemModuleParameterDomainList)
                    {
                        control.Properties.Items.Add(new SusComboBoxItem()
                        {
                            Value = itemSub.Code,
                            Text = itemSub.Name
                        });
                    }


                    if (string.IsNullOrEmpty(paraValue))
                        control.SelectedIndex = -1;
                    else
                    {
                        var items = control.Properties.Items;
                        foreach (var dpItem in items)
                        {
                            var susItem = dpItem as SusComboBoxItem;
                            if (susItem == null)
                                return;

                            if (susItem.Value.ToString() == paraValue)
                            {
                                control.SelectedItem = dpItem;
                                break;
                            }
                        }
                        //dropdown.EditValue =;
                    }

                    control.EditValueChanged += dropdownEvent;
                    //control.Click += HangUpLine_CheckedChanged;

                    container.Controls.Add(control);
                }
                point.Y += 35;
            }
        }
    }


}
