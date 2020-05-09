using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SuspeSys.Client.Modules.Ext
{
    public class SusComboBoxItem
    {
        public SusComboBoxItem()
        { }
        public SusComboBoxItem(object value, object text)
        {
            this.Text = text;
            this.Value = value;
        }
        public object Text { set; get; }
        public object Value { set; get; }
        public object Tag { set; get; }
        public override string ToString()
        {
            return Text.ToString();
        }

        /// <summary>
        /// 根据SusComboBoxItem中的Value找到特定的SusComboBoxItem(仅在ComboBox的Item都为SusComboBoxItem时有效)
        /// </summary>
        /// <param name="cmb">要查找的ComboBox</param>
        /// <param name="strValue">要查找SusComboBoxItem的Value</param>
        /// <returns>返回传入的ComboBox中符合条件的第一个SusComboBoxItem，如果没有找到则返回null.</returns>
        public static SusComboBoxItem FindByValue(ComboBoxEdit cmb, string strValue)
        {
            return cmb.Properties.Items.Cast<SusComboBoxItem>().FirstOrDefault(li => li.Value.ToString() == strValue);
        }

        /// <summary>
        /// 根据SusComboBoxItem中的Key找到特定的SusComboBoxItem(仅在ComboBox的Item都为SusComboBoxItem时有效)
        /// </summary>
        /// <param name="cmb">要查找的ComboBox</param>
        /// <param name="strValue">要查找SusComboBoxItem的Key</param>
        /// <returns>返回传入的ComboBox中符合条件的第一个SusComboBoxItem，如果没有找到则返回null.</returns>
        public static SusComboBoxItem FindByText(ComboBoxEdit cmb, string strText)
        {
            return cmb.Properties.Items.Cast<SusComboBoxItem>().FirstOrDefault(li => li.Value.ToString() == strText);
        }

    }
}
