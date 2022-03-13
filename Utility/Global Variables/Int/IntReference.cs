using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    [System.Serializable]
    public class IntReference : GlobalVariableReference<int, IntVariable>
    {
        public IntReference(int value)
        {
            constantValue = value;
        }
    }
}