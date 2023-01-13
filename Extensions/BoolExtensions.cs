using System;

namespace AAA.Extensions
{
    public static class BoolExtensions
    {
        public static int ToInt(this bool value)
        {
            return Convert.ToInt32(value);
        }
    }
}
