using System;
using UnityEngine;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;


namespace AAA.GlobalVariables.Variables
{
    public class StringVariableChangedEvents : GlobalVariableChangedEvents<string, StringVariable>
    {
        [SerializeField] private StringUnityEvent onChanged;
        protected override void OnChanged()
        {
            onChanged?.Invoke(variable.Value);
        }
    }
}