using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


namespace AAA.GlobalVariables.Variables
{
    public abstract class GlobalVariableChangedEvents<TValue, TVariable>  : MonoBehaviour
        where TVariable : GlobalVariable<TValue, TVariable>
        where TValue : IEquatable<TValue>
    {
        [FormerlySerializedAs("callOnChangedOnEnable")] [FormerlySerializedAs("initializeValueOnStart")] [SerializeField] protected bool callEventsOnEnable = true;
        [SerializeField] protected TVariable variable;

        protected virtual void Start()
        {
            if (variable == null)
            {
                Debug.LogWarning("Please Assign the variable for " + gameObject.name, this);
                return;
            }
            variable.OnChanged += OnChanged;
            if (callEventsOnEnable)
                OnChanged();
        }
        protected virtual void OnDestroy()
        {
            variable.OnChanged -= OnChanged;
        }

        protected abstract void OnChanged();
    }
}