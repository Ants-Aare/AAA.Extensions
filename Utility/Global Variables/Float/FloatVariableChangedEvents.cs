using System;
using UnityEngine;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;


namespace AAA.Utility.GlobalVariables
{
    public class FloatVariableChangedEvents : GlobalVariableChangedEvents<float, FloatVariable>
    {
        [SerializeField] private FloatUnityEvent onChanged;
        [SerializeField] private UnityEvent onIncreased, onDecreased;
        private float cachedValue = 0f;

        protected override void OnEnable()
        {
            base.OnEnable();
            cachedValue = variable.Value;
        }
        protected override void OnChanged()
        {
            float outputValue = variable.Value;

            onChanged?.Invoke(outputValue);
            if (outputValue > cachedValue)
            {
                onIncreased?.Invoke();
            }
            else if (outputValue < cachedValue)
            {
                onDecreased?.Invoke();
            }
            cachedValue = outputValue;
        }
    }
}