using UnityEngine;

namespace AAA.GlobalVariables.Variables
{
    [CreateAssetMenu(menuName = "Variable/Float Variable")]
    public class FloatVariable : GlobalVariable<float, FloatVariable>
    {
#if UNITTY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void DomainReload() => GenericDomainReload();
#endif
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
        
        public void Increase(float amount) => Value += amount;

        public void Decrease(float amount) => Value -= amount;

        public void Increment() => Value++;

        public void Decrement() => Value--;
    }
}