using UnityEngine;
using System;
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

                this.value.OnChanged -= OnChanged.Invoke;

                this.value = value;

                this.value.OnChanged += OnChanged.Invoke;

                OnChanged?.Invoke();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            value.OnChanged += OnChanged.Invoke;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            value.OnChanged -= OnChanged.Invoke;
        }

        public override void Save()
        {
            PlayerPrefs.SetFloat(name, value.Value);
            PlayerPrefs.SetFloat(name + "Min", value.MinValue);
            PlayerPrefs.SetFloat(name + "Max", value.MaxValue);
            base.Save();
        }

        public override void InitializeVariable()
        {
            base.InitializeVariable();

            if (PlayerPrefs.HasKey(name))
            {
                float targetValue = PlayerPrefs.GetFloat(name, value.Value);
                float minValue = PlayerPrefs.GetFloat(name + "Min", value.MinValue);
                float maxValue = PlayerPrefs.GetFloat(name + "Max", value.MaxValue);
                value = new FloatRangeValue(minValue, maxValue, targetValue);
            }
            else
            {
                value = new FloatRangeValue(defaultValue);
            }

            isInitialized = true;
        }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        value.OnChanged?.Invoke();
        OnChanged?.Invoke();
    }
#endif
    }
}