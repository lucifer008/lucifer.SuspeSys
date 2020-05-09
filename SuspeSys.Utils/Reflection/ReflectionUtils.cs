using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils.Reflection
{
    public class ReflectionUtils<T>
    {
        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <param name="proptertyName"></param>
        /// <returns></returns>
        public static String GetPropertyValue(object instance, string proptertyName)
        {
            Type type = typeof(T);
            PropertyInfo propertyInfo = type.GetProperty(proptertyName); //获取指定名称的属性
            return propertyInfo.GetValue(instance, null) as string;
        }
        /// <summary>
        /// 給属性賦值
        /// </summary>
        /// <param name="proptertyName"></param>
        /// <returns></returns>
        public static void SetPropertyValue(object instance, string proptertyName, object val)
        {
            Type type = typeof(T);
            PropertyInfo propertyInfo = type.GetProperty(proptertyName); //获取指定名称的属性
            propertyInfo.SetValue(instance, val);
        }
        public static Object GetInstance(string classFullName) {
            Type type = Type.GetType(classFullName);
            dynamic obj = type.Assembly.CreateInstance(classFullName);
            return obj;
        }
    }
}
