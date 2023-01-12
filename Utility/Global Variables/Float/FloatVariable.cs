using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Float Variable")]
    public class FloatVariable : GlobalVariable<float>
    {
        protected override void SaveVariable() => PlayerPrefs.SetFloat(name, value);
        protected override void LoadVariable() => value = PlayerPrefs.GetFloat(name, defaultValue);

        public bool ClampValue(float min, float max)
        {
            if (value > max)
            {
                value = max;
                OnChanged?.Invoke();
                return true;
            }

            if (value < min)
            {
                value = min;
                OnChanged?.Invoke();
                return true;
            }

            return false;
        }

        public static bool operator <(FloatVariable a, int b) => a.Value < b;
        public static bool operator >(FloatVariable a, int b) => a.Value > b;
        public static bool operator <=(FloatVariable a, int b) => a.Value <= b;
        public static bool operator >=(FloatVariable a, int b) => a.Value >= b;

        public static FloatVariable operator -(FloatVariable a, int b)
        {
            a.Value -= b;
            return a;
        }

        public static FloatVariable operator +(FloatVariable a, int b)
        {
            a.Value += b;
            return a;
        }

        public static FloatVariable operator -(FloatVariable a, float b)
        {
            a.Value = a.value - b;
            return a;
        }

        public static FloatVariable operator +(FloatVariable a, float b)
        {
            a.Value = a.value + b;
            return a;
        }

        public static FloatVariable operator ++(FloatVariable a)
        {
            a.Value++;
            return a;
        }

        public static FloatVariable operator --(FloatVariable a)
        {
            a.Value--;
            return a;
        }
    }
}