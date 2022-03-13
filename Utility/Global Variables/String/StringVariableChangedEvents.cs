using System;
using UnityEngine;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;
using Sirenix.OdinInspector;

namespace AAA.Utility.GlobalVariables
{
    public class StringVariableChangedEvents : GlobalVariableChangedEvents<string, StringVariable>
    {
        [TabGroup("Events")][SerializeField] private StringUnityEvent onChanged;
        protected override void OnChanged()
        {
            onChanged?.Invoke(variable.Value);
        }
    }
}