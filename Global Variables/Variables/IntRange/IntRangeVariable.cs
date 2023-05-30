using UnityEngine;
using AAA.DataTypes;

namespace AAA.GlobalVariables.Variables
{
    [CreateAssetMenu(menuName = "Variable/Int Range Variable")]
    public class IntRangeVariable : GlobalVariable<IntRangeValue, IntRangeVariable>
    {
#if UNITTY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void DomainReload() => GenericDomainReload();
#endif
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
            if (PlayerPrefs.HasKey(name))
            {
                var targetValue = PlayerPrefs.GetInt(name, defaultValue.Value);
                var minValue = PlayerPrefs.GetInt(name + "Min", defaultValue.MinValue);
                var maxValue = PlayerPrefs.GetInt(name + "Max", defaultValue.MaxValue);
                value = new IntRangeValue(minValue, maxValue, targetValue);
            }
            else
                value = new IntRangeValue(defaultValue);
        }

#if UNITY_EDITOR
        public override void _OnValidate()
        {
            value.OnChanged?.Invoke();
            base._OnValidate();
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


        public void Increase(int amount) => Value.Value += amount;

        public void Decrease(int amount) => Value.Value -= amount;

        public void Increment() => Value.Value++;

        public void Decrement() => Value.Value--;

        public void SetVariableValue(int newValue) => Value.Value = newValue;

        public void SetVariableMinValue(int newValue) => Value.MinValue = newValue;

        public void SetVariableMaxValue(int newValue) => Value.MaxValue = newValue;

        public void SetVariableProgress(float newValue) => Value.SetProgress(newValue);

        public void SetRandomProgress() => SetVariableProgress(Random.Range(0f, 1f));
    }
}