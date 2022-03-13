using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace AAA.Utility.GlobalVariables
{
    public class GlobalVariable<T> : ScriptableObject, ISavable where T : IEquatable<T>
    {
        [SerializeField] protected T value;
        [SerializeField] protected T defaultValue;
        [System.NonSerialized] protected bool isInitialized = false;

        public Action OnChanged;
        public virtual T Value
        {
            get
            {
                return value;
            }
            set
            {
                if(!this.Value.Equals(value))
                {
                    this.value = value;
                    OnChanged?.Invoke();
                }
            }
        }
        // This will change the value without calling onupdate
        public virtual void SetValueSilent(T newValue)
        {
            value = newValue;
        }

        // This will change the value with calling onUpdate even if the value is the same
        public virtual void SetValueLoud(T newValue)
        {
            value = newValue;
            OnChanged?.Invoke();
        }

        protected virtual void OnEnable()
        {
            InitializeVariable();
        }
        protected virtual void OnDisable()
        {
            InitializeVariable();
        }

        #if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            OnChanged?.Invoke();
        }
        #endif

        public virtual void Save()
        {
            PlayerPrefs.Save();
        }
        public virtual void InitializeVariable()
        {
            if(isInitialized)
                return;
        }

    }
}