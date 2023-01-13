using AAA.DataTypes;

namespace AAA.GlobalVariables.Variables
{
    [System.Serializable]
    public class FloatRangeReference : GlobalVariableReference<FloatRangeValue, FloatRangeVariable>
    {
        public FloatRangeReference(FloatRangeValue value) : base()
        {
            ConstantValue = value;
        }
        public FloatRangeReference(float minValue, float maxValue, float defaultValue)
        {
            ConstantValue = new FloatRangeValue(minValue, maxValue, defaultValue);
        }
        
        public static bool operator <(FloatRangeReference a, float b) => a.Value.Value < b;
        public static bool operator >(FloatRangeReference a, float b) => a.Value.Value > b;
        public static bool operator <=(FloatRangeReference a, float b) => a.Value.Value <= b;
        public static bool operator >=(FloatRangeReference a, float b) => a.Value.Value >= b;

        public static FloatRangeReference operator -(FloatRangeReference a, float b)
        {
            a.Value.Value -= b;
            return a;
        }
        public static FloatRangeReference operator +(FloatRangeReference a, float b)
        {
            a.Value.Value += b;
            return a;
        }
        public static FloatRangeReference operator -(FloatRangeReference a, int b)
        {
            a.Value.Value -= b;
            return a;
        }
        public static FloatRangeReference operator +(FloatRangeReference a, int b)
        {
            a.Value.Value += b;
            return a;
        }

        public static FloatRangeReference operator ++(FloatRangeReference a)
        {
            a.Value.Value++;
            return a;
        }

        public static FloatRangeReference operator --(FloatRangeReference a)
        {
            a.Value.Value--;
            return a;
        }
    }
}