using UnityEngine;
using AAA.Utility.DataTypes;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Int Range Variable")]
    public class IntRangeVariable : GlobalVariable<IntRangeValue>
    {
        public override IntRangeValue Value
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
            PlayerPrefs.SetInt(name, value.Value);
            PlayerPrefs.SetInt(name + "Min", value.MinValue);
            PlayerPrefs.SetInt(name + "Max", value.MaxValue);
        }

        protected override void LoadVariable()
        {
            var targetValue = PlayerPrefs.GetInt(name, defaultValue.Value);
            var minValue = PlayerPrefs.GetInt(name + "Min", defaultValue.MinValue);
            var maxValue = PlayerPrefs.GetInt(name + "Max", defaultValue.MaxValue);
            value = new IntRangeValue(minValue, maxValue, targetValue);
        }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        value.OnChanged?.Invoke();
        OnChanged?.Invoke();
    }
#endif

        public static bool operator <(IntRangeVariable a, int b) => a.Value.Value < b;
        public static bool operator >(IntRangeVariable a, int b) => a.Value.Value > b;
        public static bool operator <=(IntRangeVariable a, int b) => a.Value.Value <= b;
        public static bool operator >=(IntRangeVariable a, int b) => a.Value.Value >= b;

        public static IntRangeVariable operator -(IntRangeVariable a, int b)
        {
            a.Value.Value -= b;
            return a;
        }

        public static IntRangeVariable operator +(IntRangeVariable a, int b)
        {
            a.Value.Value += b;
            return a;
        }

        public static IntRangeVariable operator ++(IntRangeVariable a)
        {
            a.Value.Value++;
            return a;
        }

        public static IntRangeVariable operator --(IntRangeVariable a)
        {
            a.Value.Value--;
            return a;
        }
    }
}