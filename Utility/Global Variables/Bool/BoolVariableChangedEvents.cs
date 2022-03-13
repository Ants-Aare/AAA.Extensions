using System;
using UnityEngine;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;
using Sirenix.OdinInspector;

namespace AAA.Utility.GlobalVariables
{
    public class BoolVariableChangedEvents : GlobalVariableChangedEvents<bool, BoolVariable>
    {
        [TabGroup("Properties")][SerializeField] private bool invertValue = false;

        [TabGroup("Events")][SerializeField] private BoolUnityEvent onChanged;
        [TabGroup("Events")][SerializeField] private UnityEvent onTrue, onFalse;

        protected override void OnChanged()
        {
            bool outputValue = variable.Value;
            if (invertValue)
                outputValue = !outputValue;

            onChanged?.Invoke(outputValue);
            if (outputValue)
            {
                onTrue?.Invoke();
            }
            else
            {
                onFalse?.Invoke();
            }
        }
    }
}