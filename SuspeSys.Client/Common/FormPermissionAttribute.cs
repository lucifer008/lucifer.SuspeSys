using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuspeSys.Client.Common
{
    /// <summary>
    /// 自定义权限特性
    /// </summary>
    public class FormPermissionAttribute : Attribute
    {

        private bool _hasPermission = false;
        public bool hasPermission
        {
            get
            {
                return _hasPermission;
            }
        }

        public string FormKey;

        public FormPermissionAttribute(string formKey)
        {
            //MessageBox.Show(formKey);
            _hasPermission = true;
            this.FormKey = formKey;
        }

        /// <summary>
        /// 获取自定义特性值
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetFormKey(System.Type t)
        {
            try
            {
                string formKey = string.Empty;
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(t);  //反射获得用户自定义属性  

                foreach (System.Attribute attr in attrs)
                {
                    if (!string.IsNullOrEmpty(formKey))
                        continue;

                    if (attr is FormPermissionAttribute)
                    {
                        FormPermissionAttribute a = (FormPermissionAttribute)attr;
                        formKey = a.FormKey;
                    }
                }
                return formKey;
            }
            catch (Exception ex)
            {

                //写日志
                return string.Empty;
            }
            

    
        }
    }
}
