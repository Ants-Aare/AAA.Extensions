using System;
using System.Collections.Generic;
using System.Reflection;

namespace AAA.Utility.Extensions
{
    public static class ReflectionExtensions
    {
        public static List<Type> GetInheritedTypes(Type baseType, bool includeAbstractsAndInterfaces = false)
        {
            List<Type> ret = new List<Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach(Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach(Type type in types)
                {
                    if (!baseType.IsAssignableFrom(type))
                    {
                        continue;
                    }

                    if (baseType == type)
                    {
                        continue;
                    }

                    bool isAbstractOrInterface = type.IsAbstract || type.IsInterface;

                    if(!includeAbstractsAndInterfaces && isAbstractOrInterface)
                    {
                        continue;
                    }

                    ret.Add(type);
                }
            }

            return ret;
        }
    }
}
