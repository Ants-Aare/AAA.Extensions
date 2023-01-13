using UnityEngine;
using System;

namespace AAA.DataTypes
{
    [System.Serializable]
    public class IntRangeValue : RangeValue, IEquatable<IntRangeValue>
    {
        #region Fields
        [SerializeField] private int value = 1;
        [SerializeField] private int minValue = 0;
        [SerializeField] private int maxValue = 1;
        #endregion

        #region Constructors
        public IntRangeValue(int minValue, int maxValue, int defaultValue)
        {
            this.value = defaultValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
        public IntRangeValue(int minValue, int maxValue)
        {
            this.value = minValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
        public IntRangeValue(IntRangeValue intRangeValue)
        {
            value = intRangeValue.value;
            minValue = intRangeValue.minValue;
            maxValue = intRangeValue.maxValue;
            OnChanged = intRangeValue.OnChanged;
        }

        #endregion

        #region Properties
        public int Value
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
        public int MinValue
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
        public int MaxValue
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
        public int RandomValue() => UnityEngine.Random.Range(minValue, maxValue);
        public int LerpMinMax(float t) => (int)Mathf.Lerp(minValue, maxValue, t);
        public override void SetValueClamped(float newValue) => Value = Mathf.Clamp((int)newValue, minValue, maxValue);
        public override void SetValueClamped(int newValue) => Value = Mathf.Clamp(newValue, minValue, maxValue);
        public override float GetProgress() => Mathf.InverseLerp(minValue, maxValue, value);
        public override void SetProgress(float t) => Value = (int)Mathf.Lerp(minValue, maxValue, t);
        public override bool IsInRange(float value) => (float)minValue >= value && value >= (float)maxValue;
        public override bool IsInRange(int value) => minValue >= value && value >= maxValue;

        public int GetValue(VariableValue valueType)
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

        public virtual bool Equals(IntRangeValue value)
        {
            return value == this;
        }
    }
}