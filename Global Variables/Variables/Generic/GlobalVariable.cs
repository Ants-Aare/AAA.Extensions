using System;
using System.Collections.Generic;
using System.Linq;
using AAA.Extensions;
using UnityEngine;

namespace AAA.GlobalVariables.Variables
{
    public abstract class GlobalVariable<T, TSelf> : ScriptableObject
        , ISavable
#if UNITY_EDITOR
        , IValidatable
#endif

        where T : IEquatable<T>
        where TSelf : GlobalVariable<T, TSelf>
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

        public virtual void SetValue(T newValue)
        {
            value = newValue;
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
            
#if UNITY_EDITOR
            Variables.Add(this);
#endif
        }

        protected virtual void OnDisable()
        {
            LoadVariable();
            IsInitialized = false;
        }

#if UNITY_EDITOR
        public void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;
        public virtual void _OnValidate()
        {
            OnChanged?.Invoke();
        }

        private static readonly HashSet<GlobalVariable<T, TSelf>> Variables = new HashSet<GlobalVariable<T, TSelf>>();
        private protected static void GenericDomainReload()
        {
            Debug.Log("reloading domain");
            foreach (var variable in Variables)
            {
                Debug.Log($"resetting {variable.name}");
                variable.OnDisable();
                variable.OnEnable();
            }
        }
#endif

        public virtual void Save()
        {
            SaveVariable();
            PlayerPrefs.Save();
        }

        protected abstract void SaveVariable();
        protected abstract void LoadVariable();

        public static implicit operator T(GlobalVariable<T,TSelf> variable) => variable.Value;
    }

    public interface IValidatable
    {
        void OnValidate();
    }
}