using System.Text.RegularExpressions;

namespace AAA.Utility.General
{
    public static class CamelCaseConverter
    {
        public static string FromCamelCase(this string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }
        public static string ToCamelCase(this string str)
        {
            return str.Replace(" ", string.Empty);
        }
    }
}