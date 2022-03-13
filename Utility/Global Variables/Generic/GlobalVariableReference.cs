using System;
using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    public class GlobalVariableReference<TValue, TVariable> where TVariable : GlobalVariable<TValue> where TValue : IEquatable<TValue>
    {
        public bool useConstant = true;
        public TValue constantValue;
        public TVariable variable;

        public TValue Value
        {
            get
            {
                if (useConstant)
                    return constantValue;
                else
                {
                    if (variable == null)
                    {
#if UNITY_EDITOR
                        Debug.LogError("The Variable was not set.");
#endif
                        useConstant = true;
                        return constantValue;
                    }

                    return variable.Value;
                }
            }
            set
            {
                if (useConstant)
                    constantValue = value;
                else
                {
                    if (variable == null)
                    {
#if UNITY_EDITOR
                        Debug.LogError("The Variable was not set.");
#endif
                        useConstant = true;
                    }
                    else
                    {
                        variable.Value = value;
                    }
                }
            }
        }
    }
}