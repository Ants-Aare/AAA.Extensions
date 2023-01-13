using System;
using UnityEngine;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;


namespace AAA.GlobalVariables.Variables
{
    public class BoolVariableChangedEvents : GlobalVariableChangedEvents<bool, BoolVariable>
    {
        [SerializeField] private bool invertValue = false;

        [SerializeField] private BoolUnityEvent onChanged;
        [SerializeField] private UnityEvent onTrue, onFalse;

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