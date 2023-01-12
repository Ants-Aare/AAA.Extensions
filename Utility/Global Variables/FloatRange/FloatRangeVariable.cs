using UnityEngine;
using AAA.Utility.DataTypes;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Float Range Variable")]
    public class FloatRangeVariable : GlobalVariable<FloatRangeValue>
    {
        public override FloatRangeValue Value
        {
            get => value;
            set
            {
                if (this.value == value)
                    return;

                this.value.OnChanged -= InvokeOnChanged;

                this.value = value;
                value.OnChanged = OnChanged;

                this.value.OnChanged += InvokeOnChanged;

                OnChanged?.Invoke();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            value.OnChanged += InvokeOnChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            value.OnChanged -= InvokeOnChanged;
        }

        private void InvokeOnChanged()
        {
            OnChanged?.Invoke();
        }

        protected override void SaveVariable()
        {
            PlayerPrefs.SetFloat(name, value.Value);
            PlayerPrefs.SetFloat(name + "Min", value.MinValue);
            PlayerPrefs.SetFloat(name + "Max", value.MaxValue);
        }

        protected override void LoadVariable()
        {
            var targetValue = PlayerPrefs.GetFloat(name, defaultValue.Value);
            var minValue = PlayerPrefs.GetFloat(name + "Min", defaultValue.MinValue);
            var maxValue = PlayerPrefs.GetFloat(name + "Max", defaultValue.MaxValue);
            value = new FloatRangeValue(minValue, maxValue, targetValue);
        }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        value.OnChanged?.Invoke();
        OnChanged?.Invoke();
    }
#endif
        
        public static bool operator <(FloatRangeVariable a, float b) => a.Value.Value < b;
        public static bool operator >(FloatRangeVariable a, float b) => a.Value.Value > b;
        public static bool operator <=(FloatRangeVariable a, float b) => a.Value.Value <= b;
        public static bool operator >=(FloatRangeVariable a, float b) => a.Value.Value >= b;

        public static FloatRangeVariable operator -(FloatRangeVariable a, float b)
        {
            a.Value.Value -= b;
            return a;
        }
        public static FloatRangeVariable operator +(FloatRangeVariable a, float b)
        {
            a.Value.Value += b;
            return a;
        }
        public static FloatRangeVariable operator -(FloatRangeVariable a, int b)
        {
            a.Value.Value -= b;
            return a;
        }
        public static FloatRangeVariable operator +(FloatRangeVariable a, int b)
        {
            a.Value.Value += b;
            return a;
        }

        public static FloatRangeVariable operator ++(FloatRangeVariable a)
        {
            a.Value.Value++;
            return a;
        }

        public static FloatRangeVariable operator --(FloatRangeVariable a)
        {
            a.Value.Value--;
            return a;
        }
    }
}