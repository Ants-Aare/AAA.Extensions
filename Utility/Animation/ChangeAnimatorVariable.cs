using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
