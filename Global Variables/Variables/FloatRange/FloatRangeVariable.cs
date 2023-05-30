using UnityEngine;
using AAA.DataTypes;

namespace AAA.GlobalVariables.Variables
{
    [CreateAssetMenu(menuName = "Variable/Float Range Variable")]
    public class FloatRangeVariable : GlobalVariable<FloatRangeValue, FloatRangeVariable>
    {
#if UNITTY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void DomainReload() => GenericDomainReload();
#endif
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
        public override void _OnValidate()
        {
            value.OnChanged?.Invoke();
            base._OnValidate();
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


        public void Increase(float amount) => Value.Value += amount;
        public void Decrease(float amount) => Value.Value -= amount;
        public void Increment() => Value.Value++;
        public void Decrement() => Value.Value--;
        public void SetVariableValue(float newValue) => Value.Value = newValue;
        public void SetVariableMinValue(float newValue) => Value.MinValue = newValue;
        public void SetVariableMaxValue(float newValue) => Value.MaxValue = newValue;
        public void SetVariableProgress(float newValue) => Value.SetProgress(newValue);

        public void SetRandomProgress() => SetVariableProgress(Random.Range(0f, 1f));
    }
}