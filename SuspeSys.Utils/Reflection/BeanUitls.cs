using log4net;
using SuspeSys.Utils.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils.Reflection
{
    public class BeanUitls<D, S>
    {
        private static ILog log = LogManager.GetLogger("LogLogger");
        /// <summary>
        /// 拷贝非集合类，将S类的属性值拷贝到D
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// lucifer/2018年10月12日 13:18:24/修正烦类型拷贝及目标属性存储范围小于源头属性范围
        /// </remarks>
        public static D Mapper(S s)
        {
            Type typeD = typeof(D);
            if (typeD.IsGenericType)
            {
                throw new Exception("不支持集合类型拷贝!");
            }
            var errPropertyName = string.Empty;
            D d = Activator.CreateInstance<D>();
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        var dTypeString = dp.PropertyType.FullName;
                        var sTypeString = sp.PropertyType.FullName;
                        //switch (tString)
                        //{
                        //}
                        if (dp.Name == sp.Name && dTypeString.Equals(sTypeString))//判断属性名是否相同  
                        {
                            errPropertyName = dp.Name;
                            //try
                            //{
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                            //}
                            //catch (Exception ex)
                            //{
                            //    log.Error(ex);
                            //}
                        }
                        else if (dp.Name == sp.Name && !dTypeString.Equals(sTypeString))
                        {
                            errPropertyName = dp.Name;
                            var sTypeStr = string.Empty;
                            if (dp.PropertyType.IsGenericType && dp.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                sTypeStr = dp.PropertyType.GetGenericArguments()[0].FullName;
                            }
                            else {
                                sTypeStr = dTypeString;
                            }
                            switch (sTypeStr)
                                {
                                    case "System.Int16":
                                        dp.SetValue(d, Convert.ToInt16(sp.GetValue(s, null)), null);
                                        break;
                                    case "System.Int32":
                                        dp.SetValue(d, Convert.ToInt32(sp.GetValue(s, null)), null);
                                        break;
                                    case "System.Int64":
                                        dp.SetValue(d, Convert.ToInt64(sp.GetValue(s, null)), null);
                                        break;
                                    default:
                                        dp.SetValue(d, sp.GetValue(s, null), null);
                                        break;
                                }
                          
                           
                        }
                       
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }
    }
}
