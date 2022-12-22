using System;
using UnityEngine;
using UnityEngine.Events;


namespace AAA.Utility.GlobalVariables
{
    public class GlobalVariableChangedEvents<TValue, TVariable>  : MonoBehaviour where TVariable : GlobalVariable<TValue> where TValue : IEquatable<TValue>
    {
        [SerializeField] protected bool initializeValueOnStart = true;
        [SerializeField] protected TVariable variable;

        protected virtual void OnEnable()
        {
            if(variable == null)
                Debug.LogWarning("Please Assign the variable for " + gameObject.name, gameObject);
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