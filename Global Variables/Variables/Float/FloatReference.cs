namespace AAA.GlobalVariables.Variables
{
    [System.Serializable]
    public class FloatReference : GlobalVariableReference<float, FloatVariable>
    {
        public FloatReference(float value) : base()
        {
            ConstantValue = value;
        }
        
        public static bool operator <(FloatReference a, float b) => a.Value < b;
        public static bool operator >(FloatReference a, float b) => a.Value > b;
        public static bool operator <=(FloatReference a, float b) => a.Value <= b;
        public static bool operator >=(FloatReference a, float b) => a.Value >= b;

        public static FloatReference operator -(FloatReference a, float b)
        {
            a.Value -= b;
            return a;
        }
        public static FloatReference operator +(FloatReference a, float b)
        {
            a.Value += b;
            return a;
        }
        public static FloatReference operator -(FloatReference a, int b)
        {
            a.Value -= b;
            return a;
        }
        public static FloatReference operator +(FloatReference a, int b)
        {
            a.Value += b;
            return a;
        }

        public static FloatReference operator ++(FloatReference a)
        {
            a.Value++;
            return a;
        }

        public static FloatReference operator --(FloatReference a)
        {
            a.Value--;
            return a;
        }
    }
}