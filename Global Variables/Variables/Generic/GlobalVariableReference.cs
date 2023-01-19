using System;
using UnityEngine;

namespace AAA.GlobalVariables.Variables
{
    public class GlobalVariableReference<TValue, TVariable>
        where TVariable : GlobalVariable<TValue, TVariable>
        where TValue : IEquatable<TValue>
    {
        protected GlobalVariableReference()
        {
            UseConstant = true;
        }
        
        public bool UseConstant = true;
        public TValue ConstantValue;
        public TVariable Variable;
        public Action OnChanged;

        public TValue Value
        {
            get
            {
#if UNITY_EDITOR
                if (Variable == null)
                        Debug.LogError("The Variable was not set and a Constant Value will be used.");
#endif
                return UseConstant ? ConstantValue : Variable.Value;
            }
            set
            {
#if UNITY_EDITOR
                    if (Variable == null)
                        Debug.LogError("The Variable was not set.");
#endif
                if (UseConstant)
                {
                    var hasChanged = !ConstantValue.Equals(value);
                    
                    ConstantValue = value;
                    
                    if(hasChanged)
                        OnChanged?.Invoke();
                }
                else
                {
                    //Maybe change this
                    if (OnChanged != null)
                    {
                        Variable.OnChanged -= VariableOnChanged;
                        Variable.OnChanged += VariableOnChanged;
                    }
                    Variable.Value = value;
                }
            }
        }

        void VariableOnChanged() => OnChanged?.Invoke();
        
        public static implicit operator TValue(GlobalVariableReference<TValue, TVariable> variable) => variable.Value;

    }
}