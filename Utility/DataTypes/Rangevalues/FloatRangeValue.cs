using UnityEngine;
using System;

namespace AAA.Utility.DataTypes
{
    [System.Serializable]
    public class FloatRangeValue : RangeValue, IEquatable<FloatRangeValue>
    {
        #region Fields
        [SerializeField] private float value = 1;
        [SerializeField] private float minValue = 0;
        [SerializeField] private float maxValue = 1;
        #endregion

        #region Constructors
        public FloatRangeValue(float minValue, float maxValue, float defaultValue)
        {
            this.value = defaultValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
        public FloatRangeValue(float minValue, float maxValue)
        {
            this.value = minValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
        public FloatRangeValue(FloatRangeValue floatRangeValue)
        {
            minValue = floatRangeValue.minValue;
            maxValue = floatRangeValue.maxValue;
            value = floatRangeValue.value;
            OnChanged = floatRangeValue.OnChanged;
        }
        #endregion

        #region Properties
        public float Value
        {
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnChanged?.Invoke();
                }
            }
        }
        public float MinValue
        {
            get => minValue;
            set
            {
                if (minValue != value)
                {
                    minValue = value;
                    OnChanged?.Invoke();
                }
            }
        }
        public float MaxValue
        {
            get => maxValue;
            set
            {
                if (maxValue != value)
                {
                    maxValue = value;
                    OnChanged?.Invoke();
                }
            }
        }
        #endregion

        #region Utility
        public float RandomValue() => UnityEngine.Random.Range(minValue, maxValue);
        public float LerpMinMax(float t) => Mathf.Lerp(minValue, maxValue, t);
        public override void SetValueClamped(int newValue) => Value = Mathf.Clamp((float)newValue, minValue, maxValue);
        public override void SetValueClamped(float newValue) => Value = Mathf.Clamp(newValue, minValue, maxValue);
        public override float GetProgress() => Mathf.InverseLerp(minValue, maxValue, value);
        public override void SetProgress(float t) => Value = Mathf.Lerp(minValue, maxValue, t);
        public override bool IsInRange(float value) => minValue >= value && value >= maxValue;
        public override bool IsInRange(int value) => minValue >= (float)value && (float)value >= maxValue;

        public float GetValue(VariableValue valueType)
        {
            switch (valueType)
            {
                case VariableValue.Value:
                    return value;
                case VariableValue.MinValue:
                    return minValue;
                case VariableValue.MaxValue:
                    return maxValue;
                case VariableValue.ValueMinDelta:
                    return value - minValue;
                case VariableValue.ValueMaxDelta:
                    return maxValue - value;
                case VariableValue.MinMaxDelta:
                    return maxValue - minValue;
                default:
                    return value;
            }
        }
        #endregion

        public virtual bool Equals(FloatRangeValue value)
        {
            return value == this;
        }
    }
}