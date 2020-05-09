using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Domain.SusEnum
{
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举的Description
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string Description(this System.Enum e)
        {
            Type type = e.GetType();
            string name = System.Enum.GetName(type, e);
            if (name == null)
            {
                return null;
            }
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }

        /// <summary>
        /// 获取字典类型的枚举所有元素
        /// </summary>
        /// <param name="t">枚举</param>
        /// <returns></returns>
        public static Dictionary<int, string> GetDictionary(Type t)
        {
            Dictionary<int, string> ar = new Dictionary<int, string>();
            Array _enumValues = System.Enum.GetValues(t);
            foreach (System.Enum value in _enumValues)
            {
        
                ar.Add((int)System.Enum.Parse(t, value.ToString()), value.Description());
            }
            return ar;
        }

        public static Dictionary<string, string> GetDictionary2(Type t)
        {
            Dictionary<string, string> ar = new Dictionary<string, string>();
            Array _enumValues = System.Enum.GetValues(t);
            foreach (System.Enum value in _enumValues)
            {

                ar.Add(value.ToString(), value.Description());
            }
            return ar;
        }
    }
}
