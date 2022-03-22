using System;
using UnityEngine;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;
using Sirenix.OdinInspector;
using AAA.Utility.DataTypes;

namespace AAA.Utility.GlobalVariables
{
    public class FloatRangeVariableChangedEvents : GlobalVariableChangedEvents<FloatRangeValue, FloatRangeVariable>
    {
        [TabGroup("Events")][SerializeField] private FloatRangeValueUnityEvent onChanged;
        [TabGroup("Events")][SerializeField] private UnityEvent onIncreased, onDecreased, onReachedMin, onReachedMax;
        private float cachedValue = 0f;

        protected override void OnEnable()
        {
            base.OnEnable();
            cachedValue = variable.Value.Value;
        }
        protected override void OnChanged()
        {
            onChanged?.Invoke(variable.Value);

            float outputValue = variable.Value.Value;

            if (outputValue > cachedValue)
            {
                onIncreased?.Invoke();
            }
            else if (outputValue < cachedValue)
            {
                onDecreased?.Invoke();
            }

            if(variable.Value.GetProgress() == 1)
            {
                onReachedMax?.Invoke();
            }
            else if(variable.Value.GetProgress() == 0)
            {
                onReachedMin?.Invoke();
            }
            cachedValue = outputValue;
        }
    }
}