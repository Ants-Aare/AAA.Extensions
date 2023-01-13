using UnityEngine;
using System;

namespace AAA.DataTypes
{
    public abstract class RangeValue : IEquatable<RangeValue>
    {
        public Action OnChanged;
        public abstract void SetValueClamped(float value);
        public abstract void SetValueClamped(int value);
        public abstract float GetProgress();
        public abstract void SetProgress(float progress);
        public abstract bool IsInRange(float value);
        public abstract bool IsInRange(int value);

        public virtual bool Equals(RangeValue value)
        {
            return value == this;
        }
    }
}