using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
using log4net.Repository.Hierarchy;
using SuspeSys.Utils.Reflection;
using NHibernate;
using log4net;

namespace SuspeSys.Dao.Nhibernate
{
    /// <summary>
    /// 解决数据库字段与类属性大小写不一致问题
    /// </summary>
    public class BeanTransformerAdapter<T> : IResultTransformer
    {
        ILog log = LogManager.GetLogger("LogLogger");
        public BeanTransformerAdapter(Type result)
        { }
        public BeanTransformerAdapter()
        {
            initialize(typeof(T));
        }
        public BeanTransformerAdapter(Type result, bool checkFullyPopulated) : this(result)
        {
            this.checkFullyPopulated = checkFullyPopulated;
        }
        // protected readonly static Log  Logger= LogFactory.getLog(typeof(BeanTransformerAdapter));

        /** The class we are mapping to */
        private Type mappedClass;

        /** Whether we're strictly validating */
        private bool checkFullyPopulated = false;

        /** Whether we're defaulting primitives when mapping a null value */
        private bool primitivesDefaultedForNullValue = false;

        /** Map of the fields we provide mapping for */
        private Dictionary<String, PropertyInfo> mappedFields;

        /** Set of bean properties we provide mapping for */
        private HashSet<String> mappedProperties;

        protected void initialize(Type mappedClass)
        {
            this.mappedClass = mappedClass;
            this.mappedFields = new Dictionary<String, PropertyInfo>();
            this.mappedProperties = new HashSet<String>();
            PropertyInfo[] pds = mappedClass.GetProperties().Where(p => p.GetMethod.IsVirtual).ToArray();
            foreach (PropertyInfo pd in pds)
            {
                //if (pd.getWriteMethod() != null)
                //{
                var name = pd.Name.ToLowerInvariant();
                if (!this.mappedFields.ContainsKey(name))
                    this.mappedFields.Add(name, pd);
                String underscoredName = UnderscoreName(pd.Name);
                if (!pd.Name.ToUpperInvariant().Equals(underscoredName) && !string.IsNullOrEmpty(underscoredName))
                {
                    if (!this.mappedFields.ContainsKey(underscoredName))
                        this.mappedFields.Add(underscoredName, pd);
                }
                if (!this.mappedFields.ContainsKey(pd.Name))
                    this.mappedProperties.Add(pd.Name);
                //}
            }

            //zxl.兼容基本类型,2018年1月30日 00:47:43
            if (typeof(T).IsValueType)
            {
                mappedProperties.Add(typeof(T).ToString());
            }
            if (typeof(T).FullName.Equals("System.String"))
            {
                mappedProperties.Add("System.String");
            }
            //mappedProperties.Add(typeof(int).ToString());
            //mappedProperties.Add(typeof(Int16).ToString());
            //mappedProperties.Add(typeof(Int32).ToString());
            //mappedProperties.Add(typeof(Int64).ToString());
            //mappedProperties.Add(typeof(bool).ToString());
            //mappedProperties.Add(typeof(Double).ToString());
            //mappedProperties.Add(typeof(float).ToString());
        }
        private String UnderscoreName(String name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return "";
            }
            StringBuilder result = new StringBuilder();
            result.Append(name.Substring(0, 1).ToUpperInvariant());
            for (int i = 1; i < name.Length; i++)
            {
                String s = name.Substring(i, i + 1);
                String slc = s.ToLowerInvariant();
                if (!s.Equals(slc))
                {
                    result.Append("_").Append(slc);
                }
                else
                {
                    result.Append(s);
                }
            }
            return result.ToString();
        }
        //public IList TransformList(IList collection)
        //{
        //    throw new NotImplementedException();
        //}

        //public object TransformTuple(object[] tuple, string[] aliases)
        //{
        //    throw new NotImplementedException();
        //}
        private Type result;
        private List<PropertyInfo> properties = new List<PropertyInfo>();

        public IList TransformList(IList collection)
        {
            return collection;
        }
        public BeanTransformerAdapter(Type result, params string[] names)
        {
            this.result = result;
            foreach (string name in names)
            {
                properties.Add(result.GetProperty(name));
            }
        }

        public object TransformTuple(object[] tuple, string[] aliases)
        {
            //object instance = Activator.CreateInstance(result);
            //for (int i = 0; i < tuple.Length; i++)
            //{
            //    properties[i].SetValue(instance, tuple[i], null);
            //}
            //return instance;
            if (null != tuple && tuple.Length == 1)
            {
                if (typeof(T).IsValueType)
                {
                    return (tuple[0]);
                }
                //if (tuple[0] is Int32)
                //{
                //    return tuple[0];
                //}
            }
            HashSet<String> populatedProperties = new HashSet<String>();
            Object mappedObject = Activator.CreateInstance<T>();
            for (int i = 0; i < aliases.Length; i++)
            {
                String column = aliases[i];
                var pName = column.Replace("_", "").Replace(" ", "").ToLowerInvariant();
                if (!this.mappedFields.Keys.Contains(pName)) continue;
                PropertyInfo pd = this.mappedFields[pName];
                if (pd != null)
                {
                    try
                    {
                        Object value = tuple[i];
                        try
                        {
                            pd.SetValue(mappedObject, tuple[i], null);
                        }
                        catch (TypeMismatchException e)
                        {
                            if (value == null && primitivesDefaultedForNullValue)
                            {

    //                            log.Debug("Intercepted TypeMismatchException for column " + column + " and column '"
    // + column + "' with value " + value + " when setting property '" + pd.Name + "' of type " + pd.GetType()
    //+ " on object: " + mappedObject);
                            }
                            else
                            {
                                throw e;
                            }
                        }
                        if (populatedProperties != null)
                        {
                            populatedProperties.Add(pd.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Unable to map column " + column
    + " to property " + pd.Name, ex);
                    }
                }
            }

            if (populatedProperties != null && !populatedProperties.Equals(this.mappedProperties))
            {
                //log.Info(@"Given ResultSet does not contain all fields "
                //+ "necessary to populate object of class [" + this.mappedClass + "]: " + this.mappedProperties);
                //匹配不上的暂时不处理
                //           throw new Exception("Given ResultSet does not contain all fields "
                //+ "necessary to populate object of class [" + this.mappedClass + "]: " + this.mappedProperties);
            }

            return mappedObject;
        }
    }
}
