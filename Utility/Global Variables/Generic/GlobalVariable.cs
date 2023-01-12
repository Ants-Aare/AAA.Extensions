using System;
using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    public abstract class GlobalVariable<T> : ScriptableObject, ISavable
        where T : IEquatable<T>
    {
        [SerializeField] protected T value;
        [SerializeField] protected T defaultValue;
        [NonSerialized] protected bool IsInitialized = false;

        public Action OnChanged;

        public virtual T Value
        {
            get => value;
            set
            {
                if (Value.Equals(value)) return;

                this.value = value;
                OnChanged?.Invoke();
            }
        }

        // This will change the value without calling OnChanged
        public virtual void SetValueSilent(T newValue)
        {
            value = newValue;
        }

        // This will change the value with calling OnChanged even if the value is the same
        public virtual void SetValueLoud(T newValue)
        {
            value = newValue;
            OnChanged?.Invoke();
        }

        protected virtual void OnEnable()
        {
            if (IsInitialized) return;
            LoadVariable();
            IsInitialized = true;
        }

        protected virtual void OnDisable()
        {
            if (IsInitialized) return;
            LoadVariable();
            IsInitialized = true;
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            OnChanged?.Invoke();
        }
#endif

        public virtual void Save()
        {
            SaveVariable();
            PlayerPrefs.Save();
        }

        protected abstract void SaveVariable();
        protected abstract void LoadVariable();

        public static implicit operator T(GlobalVariable<T> variable) => variable.Value;
    }
}