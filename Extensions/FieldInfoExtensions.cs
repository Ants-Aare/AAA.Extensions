using System;
using System.Reflection;

namespace AAA.Extensions
{
    public static class FieldInfoExtensions
    {
        public static bool TryGetCustomAttribute<T>(this FieldInfo fieldInfo, out T attribute) where T : Attribute
        {
            attribute = fieldInfo.GetCustomAttribute<T>();

            return attribute != null;
        }
    }
}
