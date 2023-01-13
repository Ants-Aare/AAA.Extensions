using System;
using UnityEngine;

namespace AAA.GlobalVariables.Variables
{
    public class SetGlobalVariable<TValue, TVariable> : MonoBehaviour
        where TVariable : GlobalVariable<TValue, TVariable>
        where TValue : IEquatable<TValue>
    {
        [SerializeField] protected bool saveVariable = false;
        [SerializeField] protected TVariable variable;
        public void SetVariableTo(TValue value)
        {
            variable.Value = value;
            if(saveVariable)
                variable.Save();
        }
    }
}