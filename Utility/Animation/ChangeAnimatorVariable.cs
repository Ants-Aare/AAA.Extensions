using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AAA.Utility.Animation
{
    public class ChangeAnimatorVariable : StateMachineBehaviour
    {
        [SerializeField] private ValueType valueType;
        [SerializeField] private string variableName;

        [SerializeField] private bool targetBool;
        [SerializeField] private int targetInt;
        [SerializeField] private float targetFloat;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            switch (valueType)
            {
                case ValueType.Float:
                    animator.SetFloat(variableName, targetFloat);
                    break;
                case ValueType.Boolean:
                    animator.SetBool(variableName, targetBool);
                    break;
                case ValueType.Integer:
                    animator.SetInteger(variableName, targetInt);
                    break;
            }
        }
    }
}