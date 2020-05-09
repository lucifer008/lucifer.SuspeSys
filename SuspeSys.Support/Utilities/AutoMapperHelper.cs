using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Support.Utilities
{
    public static class AutoMapperHelper
    {
        /// <summary>
        /// 自动建立sourceType-->destinationType 的映射 [包括内部类型的映射]
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        private static void JAutoCreateMap(Type sourceType, Type destinationType)
        {
            foreach (var perProperty in sourceType.GetProperties())
            {
                var dProperty = destinationType.GetProperty(perProperty.Name);
                if (dProperty != null)
                {
                    if (!perProperty.PropertyType.IsValueType && perProperty.PropertyType != typeof(string)) //非string的引用类型
                    {
                        if (perProperty.PropertyType.IsGenericType && perProperty.PropertyType.GenericTypeArguments.First().IsClass && perProperty.PropertyType.GenericTypeArguments.First() != typeof(string))
                        {
                            if (dProperty.PropertyType.IsGenericType && dProperty.PropertyType.GenericTypeArguments.First().IsClass && dProperty.PropertyType.GenericTypeArguments.First() != typeof(string))
                            {
                                JAutoCreateMap(perProperty.PropertyType.GenericTypeArguments.First(), dProperty.PropertyType.GenericTypeArguments.First());
                            }
                        }
                        else if (perProperty.PropertyType.IsClass && dProperty.PropertyType.IsClass) // 非泛型的属性,比如 MyType P3 {get;set;}
                        {
                            JAutoCreateMap(perProperty.PropertyType, dProperty.PropertyType);
                        }
                    }
                }
            }
            //Mapper.CreateMap(sourceType, destinationType);
            Mapper.Initialize(cfg => cfg.CreateMap(sourceType, destinationType));
        }
        /// <summary>
        /// 对象扩展方法
        /// </summary>
        /// <typeparam name="TEntity">需转换为的对象</typeparam>
        /// <param name="o">源对象</param>
        /// <returns>转换后的对象</returns>
        public static TEntity TransformTo<TEntity>(this object o)
        {
            if (o != null)
            {
                Mapper.Reset();
                Mapper.Initialize(cfg => cfg.CreateMap(o.GetType(), typeof(TEntity)));

                return Mapper.Map<TEntity>(o);
            }
            else
                return default(TEntity);
        }
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <typeparam name="TSource">元对象类型</typeparam>
        /// <typeparam name="TEntity">目标对象类型</typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static TEntity Transform<TSource, TEntity>(this TSource o)
        {
            if (o != null)
            {
                Mapper.Reset();
                Mapper.Initialize(x => x.CreateMap<TSource, TEntity>());
                return Mapper.Map<TSource, TEntity>(o);
            }
            else
                return default(TEntity);
        }
        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static IEnumerable<TDestination> TransformTo<TDestination>(this IEnumerable source)
        {
            if (source != null)
            {
                IEnumerable<TDestination> ar = default(IEnumerable<TDestination>);
                //IEnumerable<T> 类型需要创建元素的映射
                foreach (var first in source)
                {
                    var type = first.GetType();
                    //Mapper.CreateMap(type, typeof(TDestination));
                    JAutoCreateMap(type, typeof(TDestination));
                    ar = Mapper.Map<IEnumerable<TDestination>>(source);
                    break;
                }
                return ar;
            }
            else
                return default(IEnumerable<TDestination>);
        }
        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TDestination> TransformTo<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            //if (source != null && source.Count() > 0)
            if (source != null)
            {
                Mapper.Reset();
                Mapper.Initialize(x => x.CreateMap<TSource, TDestination>());
                return Mapper.Map<IEnumerable<TDestination>>(source);
            }
            else
                return default(IEnumerable<TDestination>);
        }
        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return MapTo<TDestination>(source, destination);
        }

        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source) where TDestination : new()
        {
            return MapTo(source, new TDestination());
        }

        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns></returns>
        private static TDestination MapTo<TDestination>(object source, TDestination destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            var sourceType = GetObjectType(source.GetType());
            var destinationType = GetObjectType(typeof(TDestination));
            try
            {
                var map = Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
                if (map != null)
                {
                    return Mapper.Map(source, destination);
                }
                var maps = Mapper.Configuration.GetAllTypeMaps();
                Mapper.Initialize(config =>
                {
                    foreach (var item in maps)
                    {
                        config.CreateMap(item.SourceType, item.DestinationType);
                    }
                    config.CreateMap(sourceType, destinationType);
                });

            }
            catch (InvalidOperationException)
            {
                Mapper.Initialize(config =>
                {
                    config.CreateMap(sourceType, destinationType);
                });
            }
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// 获取对象类型
        /// </summary>
        /// <param name="source">类型</param>
        /// <returns></returns>
        private static Type GetObjectType(Type source)
        {
            if (source.IsGenericType && typeof(IEnumerable).IsAssignableFrom(source))
            {
                var type = source.GetGenericArguments()[0];
                return GetObjectType(type);
            }
            return source;
        }
    }
}
