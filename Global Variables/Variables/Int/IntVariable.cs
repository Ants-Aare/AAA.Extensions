using UnityEngine;

namespace AAA.GlobalVariables.Variables
{
    [CreateAssetMenu(menuName = "Variable/Integer Variable")]
    public class IntVariable : GlobalVariable<int, IntVariable>
    {
#if UNITTY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void DomainReload() => GenericDomainReload();
#endif

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

        public void Increase(int amount) => Value += amount;
        public void Decrease(int amount) => Value -= amount;

        public void Increment() => Value++;
        public void Decrement() => Value--;
    }
}