﻿using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Yu.Core.Extensions
{
    /// <summary>
    /// 反射类型扩展
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 判断类型是否是某泛型类的实现
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="generic">父类型</param>
        /// <returns>是否继承于父类型</returns>
        public static bool HasImplementedRawGeneric(this Type type, Type genericType)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (genericType == null) throw new ArgumentNullException(nameof(genericType));

            // 在类型的基类内进行判断
            type = type.BaseType;
            while (type != null && type != typeof(object))
            {
                if (IsTheRawGenericType(type)) return true;
                type = type.BaseType;
            }

            // 没有找到任何匹配的接口或类型。
            return false;

            // 测试类型是否是泛型,并且获取泛型类型
            bool IsTheRawGenericType(Type test)
                => genericType == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
        }

        /// <summary>
        /// 获取当前类型的泛型参数类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>泛型参数的类型</returns>
        public static Type[] GetBaseGenericArguments(this Type type)
        {
            type = type.BaseType;
            while (type != null && type != typeof(object))
            {
                if (type.GetGenericArguments().Count() > 0) return type.GetGenericArguments();
                type = type.BaseType;
            }
            return null;
        }

        /// <summary>
        /// 在全部程序集范围内查找当前类的子类
        /// </summary>
        /// <param name="type">父类型</param>
        /// <returns>子类型</returns>
        public static List<Type> GetAllChildType(this Type type)
        {
            // 过滤系统包和nuget包
            var libs = DependencyContext.Default.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package").ToList();

            // 指定程序集的包
            var typeList = new List<Type>();

            // 获取全部继承type的类型
            foreach (var lib in libs)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                typeList.AddRange(assembly.GetTypes().Where(x => x.HasImplementedRawGeneric(type)).ToList());
            }

            return typeList;
        }

        /// <summary>
        /// 在指定程序集范围内查找当前类的子类
        /// </summary>
        /// <param name="type">父类型</param>
        /// <returns>子类型</returns>
        public static List<Type> GetAllChildType(this Type type, string assemblyName)
        {
            // 过滤系统包和nuget包
            var libs = DependencyContext.Default.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package").ToList();

            // 指定程序集的包
            var serviceLib = libs.Where(c => c.Assemblies.Contains(assemblyName)).FirstOrDefault();

            // 获取全部继承type的类型
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(serviceLib.Name));

            return assembly.GetTypes().Where(x => x.HasImplementedRawGeneric(type)).ToList();
        }

        /// <summary>
        /// 获取全部接口类型
        /// </summary>
        /// <param name="assemblyName">指定程序集</param>
        /// <returns>接口类型</returns>
        public static List<Type> GetInterfaces(string assemblyName)
        {
            // 过滤系统包和nuget包
            var libs = DependencyContext.Default.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");

            // 指定程序集的包
            var serviceLib = libs.Where(c => c.Assemblies.Contains(assemblyName)).FirstOrDefault();

            // 获取全部接口类型
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(serviceLib.Name));
            return assembly.GetTypes().Where(x => x.IsInterface).ToList();
        }


        /// <summary>
        /// 获取全部类类型
        /// </summary>
        /// <param name="assemblyName">指定程序集</param>
        /// <returns>类类型</returns>
        public static List<Type> GetClasses(string assemblyName)
        {
            // 过滤系统包和nuget包
            var libs = DependencyContext.Default.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");

            // 指定程序集的包
            var serviceLib = libs.Where(c => c.Assemblies.Contains(assemblyName)).FirstOrDefault();

            // 获取全部接口类型
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(serviceLib.Name));
            return assembly.GetTypes().Where(x => x.IsClass).ToList();
        }

        /// <summary>
        /// 获取当前程序全部的自定义程序集
        /// </summary>
        /// <param name="assemblyName">指定程序集</param>
        /// <returns>程序集列表</returns>
        public static List<Assembly> GetAssemblies()
        {
            // 过滤系统包和nuget包
            var libs = DependencyContext.Default.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");

            // 结果集合
            var result = new List<Assembly>();

            // 循环包数据
            foreach (var lib in libs)
            {
                result.Add( AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name)));
            }

            return result;
        }
    }
}