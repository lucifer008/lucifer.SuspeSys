using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils
{
    public static class Eextentions
    {
        public static List<Variance> Diff<T>(this T original, T newObj)
        {
            List<Variance> variances = new List<Variance>();
            var propertyInfo = original.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var item in propertyInfo)
            {
                var attr = item.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

                ///不设置描述的不进行对比记录
                if (attr != null)
                {
                    string description = ((DescriptionAttribute)attr).Description;

                    string propCode = item.Name;
                    object originalValue = item.GetValue(original, null);
                    object newValue = item.GetValue(newObj, null);

                    if (Convert.ToString( originalValue) != Convert.ToString(newValue))
                    {

                        variances.Add
                        (
                            new Variance()
                            {
                                PropCode = propCode,
                                NewValue = Convert.ToString(newValue),
                                OriginalValue = Convert.ToString(originalValue),
                                PropDescription = description
                            }
                        );
                    }

                }
            }

            return variances;

        }

    }

    public class Variance
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string PropCode { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string PropDescription { get; set; }
        /// <summary>
        /// 修改之前
        /// </summary>
        public string OriginalValue { get; set; }
        /// <summary>
        /// 修改之后
        /// </summary>
        public string NewValue { get; set; }
    }
}
