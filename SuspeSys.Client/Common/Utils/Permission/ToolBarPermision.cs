using DevExpress.XtraEditors;
using SuspeSys.Client.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Common.Utils.Permission
{
    public class ToolBarPermision
    {
        public static void Process(Type type, SusToolBar toolBar)
        {
            
            //获取当前key
            string key = FormPermissionAttribute.GetFormKey(type);

            //获取页面按钮
            if (toolBar == null)
                return;

            foreach (Control item in toolBar.Controls[0].Controls)
            {
                if (item is SimpleButton) {
                    string resourceId = string.Concat(key, ".", item.Name);
                    //判断权限
                    if (!Domain.Common.CurrentUser.Instance.HasPermisson(resourceId))
                        item.Visible = false;

                }
            } 
        }
    }
}
