using UnityEngine;
using System;
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
            PlayerPrefs.SetInt(name, value.Value);
            PlayerPrefs.SetInt(name + "Min", value.MinValue);
            PlayerPrefs.SetInt(name + "Max", value.MaxValue);
            base.Save();
        }

        public override void InitializeVariable()
        {
            base.InitializeVariable();

            if (PlayerPrefs.HasKey(name))
            {
                int targetValue = PlayerPrefs.GetInt(name, value.Value);
                int minValue = PlayerPrefs.GetInt(name + "Min", value.MinValue);
                int maxValue = PlayerPrefs.GetInt(name + "Max", value.MaxValue);
                value = new IntRangeValue(minValue, maxValue, targetValue);
            }
            else
            {
                value = new IntRangeValue(defaultValue);
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