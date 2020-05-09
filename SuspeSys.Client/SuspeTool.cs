using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client
{
    public class SuspeTool
    {
        public static DialogResult ShowMessageBox(string content)
        {
            return ShowMessageBox("温馨提示",content); ;
        }

        public static DialogResult ShowMessageBox(string title, string content)
        {
            return XtraMessageBox.Show(content, title);
        }
    }
}
