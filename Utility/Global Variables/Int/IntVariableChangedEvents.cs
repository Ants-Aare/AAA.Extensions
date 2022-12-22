using System;
using UnityEngine;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;


namespace AAA.Utility.GlobalVariables
{
    public class IntVariableChangedEvents : GlobalVariableChangedEvents<int, IntVariable>
    {
        [SerializeField] private IntUnityEvent onChanged;
        [SerializeField] private UnityEvent onIncreased, onDecreased;
        private int cachedValue = 0;

        protected override void OnEnable()
        {
            base.OnEnable();
            cachedValue = variable.Value;
        }
        protected override void OnChanged()
        {
            int outputValue = variable.Value;

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