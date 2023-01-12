using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Integer Variable")]
    public class IntVariable : GlobalVariable<int>
    {
        protected override void SaveVariable() => PlayerPrefs.SetInt(name, value);
        protected override void LoadVariable() => value = PlayerPrefs.GetInt(name, defaultValue);

        public bool ClampValue(int min, int max)
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

        public static bool operator <(IntVariable a, int b) => a.Value < b;
        public static bool operator >(IntVariable a, int b) => a.Value > b;
        public static bool operator <=(IntVariable a, int b) => a.Value <= b;
        public static bool operator >=(IntVariable a, int b) => a.Value >= b;

        public static IntVariable operator -(IntVariable a, int b)
        {
            a.Value = a.value - b;
            return a;
        }

        public static IntVariable operator +(IntVariable a, int b)
        {
            a.Value = a.value + b;
            return a;
        }

        public static IntVariable operator ++(IntVariable a)
        {
            a.Value++;
            return a;
        }

        public static IntVariable operator --(IntVariable a)
        {
            a.Value--;
            return a;
        }
    }
}