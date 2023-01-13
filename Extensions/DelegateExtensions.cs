namespace AAA.Extensions
{
    public static class DelegateExtensions
    {
        public static T Self<T>(T value) => value;
        public static void DoNothing() { }
        public static void DoNothing<T>(T value) { }
        public static void DoNothing<T1, T2>(T1 value1, T2 value2) { }
    }
}
