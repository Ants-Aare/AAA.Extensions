using UnityEngine;

namespace AAA.Utility.Animation
{
    public class ChangeAnimatorVariable : StateMachineBehaviour
    {
        enum ValueType
        {
            Float,
            Integer,
            Boolean,
        }
        
        [SerializeField] private ValueType valueType;
        [SerializeField] private string variableName;

        [SerializeField] private bool targetBool;
        [SerializeField] private int targetInt;
        [SerializeField] private float targetFloat;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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