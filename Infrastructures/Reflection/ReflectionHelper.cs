﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace Infrastructures
{
    /// <summary>
    /// 加载程序所有引用的非systemdll文件，
    /// </summary>
    public static class ReflectionHelper
    {
        private static bool IsSystemAssembly(Assembly asm)
        {
            var asmCompanyAttr = asm.GetCustomAttribute<AssemblyCompanyAttribute>();
            if (asmCompanyAttr == null)
            {
                return false;
            }
            else
            {
                string companyName = asmCompanyAttr.Company;
                return companyName.Contains("Microsoft");
            }
        }

        private static Assembly? TryLoadAssembly(string pathName)
        {
            var assembly = AssemblyName.GetAssemblyName(pathName);
            Assembly? asm = null;
            try
            {
                asm = Assembly.Load(assembly);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            if (asm == null)
            {
                try
                {
                    asm = Assembly.Load(pathName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return asm;
        }

        private static bool IsValid(Assembly asm)
        {
            try
            {
                asm.GetTypes();
                asm.DefinedTypes.ToList();
                return true;
            }
            catch (ReflectionTypeLoadException)
            {
                return false;
            }
        }
        /// <summary>
        /// 判断file这个文件是否是程序集
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static bool IsManagedAssembly(string file)
        {
            using var fs = File.OpenRead(file);
            using PEReader peReader = new PEReader(fs);
            return peReader.HasMetadata && peReader.GetMetadataReader().IsAssembly;
        }
        public static IEnumerable<Assembly> GetAllReferencedAssemblies(bool skipSystemAssemblies=true,string content="")
        {
            bool skipNotContent = content==""?false:true;
            //程序集运行的dll
            var rootAsm = Assembly.GetEntryAssembly();
            if (rootAsm == null)
                rootAsm = Assembly.GetCallingAssembly();
            var returnAsms = new HashSet<Assembly>(new AssemblyEquality());
            var pathAsms = new HashSet<string>();
            //获取程序运行的文件下所有的dll文件
            var asmPath = Directory.EnumerateFiles(AppContext.BaseDirectory, "*.dll", new EnumerationOptions { RecurseSubdirectories = true });
            foreach (var path in asmPath)
            {
                if (!IsManagedAssembly(path))
                    continue;
                var asmName = AssemblyName.GetAssemblyName(path);
                if (pathAsms.Any(p => p.Contains(asmName.FullName)))
                    continue;
                var asm = TryLoadAssembly(path);
                if (skipSystemAssemblies && IsSystemAssembly(asm))
                    continue;
                if (skipNotContent && !asm.FullName.Contains(content))
                    continue;
                if (!IsValid(asm))
                    continue;
                returnAsms.Add(asm);
                pathAsms.Add(asmName.FullName);
            }
            return returnAsms.ToArray();
        }
    }
    class AssemblyEquality : EqualityComparer<Assembly>
    {
        public override bool Equals(Assembly? x, Assembly? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return AssemblyName.ReferenceMatchesDefinition(x.GetName(), y.GetName());
        }

        public override int GetHashCode([DisallowNull] Assembly obj)
        {
            return obj.GetName().FullName.GetHashCode();
        }
    }
}
