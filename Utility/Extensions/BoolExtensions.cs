using System;

namespace AAA.Utility.Extensions
{
    public static class BoolExtensions
    {
        public static int ToInt(this bool value)
        {
            return Convert.ToInt32(value);
        }
    }
}
