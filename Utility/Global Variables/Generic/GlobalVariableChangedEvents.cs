using System;
using UnityEngine;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;
using Sirenix.OdinInspector;

namespace AAA.Utility.GlobalVariables
{
    public class GlobalVariableChangedEvents<TValue, TVariable>  : MonoBehaviour where TVariable : GlobalVariable<TValue> where TValue : IEquatable<TValue>
    {
        [TabGroup("Properties")][SerializeField] protected bool initializeValueOnStart = true;
        [TabGroup("Properties")][SerializeField] protected TVariable variable;

        protected virtual void OnEnable()
        {
            variable.OnChanged += OnChanged;
            if (initializeValueOnStart)
                OnChanged();
        }
        protected virtual void OnDisable()
        {
            variable.OnChanged -= OnChanged;
        }

        protected virtual void OnChanged()
        {
            
        }
    }
}