using System;
using System.Collections.Generic;
using System.Reflection;

namespace AAA.Extensions
{
    public static class ReflectionExtensions
    {
        public static List<Type> GetInheritedTypes(Type baseType, bool includeAbstractsAndInterfaces = false)
        {
            var ret = new List<Type>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach(var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                foreach(var type in types)
                {
                    if (!baseType.IsAssignableFrom(type))
                    {
                        continue;
                    }

                    if (baseType == type)
                    {
                        continue;
                    }

                    var isAbstractOrInterface = type.IsAbstract || type.IsInterface;

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
