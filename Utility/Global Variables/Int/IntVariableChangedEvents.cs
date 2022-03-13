using System;
using UnityEngine;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;
using Sirenix.OdinInspector;

namespace AAA.Utility.GlobalVariables
{
    public class IntVariableChangedEvents : GlobalVariableChangedEvents<int, IntVariable>
    {
        [TabGroup("Events")][SerializeField] private IntUnityEvent onChanged;
        [TabGroup("Events")][SerializeField] private UnityEvent onIncreased, onDecreased;
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