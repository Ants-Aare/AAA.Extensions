using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AAA.Extensions
{
    public static class AppDomainExtension
    {
        public static IEnumerable<Type> GetAllTypes(this AppDomain appDomain) =>
            ((IEnumerable<Assembly>)appDomain.GetAssemblies()).GetAllTypes();

        public static IEnumerable<Type> GetAllTypes(this IEnumerable<Assembly> assemblies)
        {
            List<Type> allTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    allTypes.AddRange((IEnumerable<Type>)assembly.GetTypes());
                }
                catch (ReflectionTypeLoadException ex)
                {
                    allTypes.AddRange(
                        ((IEnumerable<Type>)ex.Types).Where<Type>((Func<Type, bool>)(type => type != (Type)null)));
                }
            }

            return (IEnumerable<Type>)allTypes;
        }

        public static IEnumerable<Type> GetAllDerivedTypes(this AppDomain appDomain, Type baseClassType)
        {
            var assemblies = appDomain.GetAssemblies();

            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(t => t.IsSubclassOf(baseClassType));
        }

        public static IEnumerable<Type> GetNonAbstractTypes<T>(this AppDomain appDomain) =>
            appDomain.GetAllTypes().GetNonAbstractTypes<T>();

        public static IEnumerable<Type> GetNonAbstractTypes<T>(
            this IEnumerable<Type> types)
        {
            return types.Where<Type>((Func<Type, bool>)(type => !type.IsAbstract))
                .Where<Type>((Func<Type, bool>)(type => type.ImplementsInterface<T>()));
        }

        public static IEnumerable<T> GetInstancesOf<T>(this AppDomain appDomain) =>
            appDomain.GetNonAbstractTypes<T>().GetInstancesOf<T>();

        public static IEnumerable<T> GetInstancesOf<T>(this IEnumerable<Type> types) => types.GetNonAbstractTypes<T>()
            .Select<Type, T>((Func<Type, T>)(type => (T)Activator.CreateInstance(type)));
    }
}