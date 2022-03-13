using UnityEngine;
using AAA.Utility.DataTypes;

namespace AAA.Utility.GlobalVariables
{
    [System.Serializable]
    public class IntRangeReference : GlobalVariableReference<IntRangeValue, IntRangeVariable>
    {
        public IntRangeReference(IntRangeValue value)
        {
            constantValue = value;
        }
        public IntRangeReference(int minValue, int maxValue, int defaultValue)
        {
            constantValue = new IntRangeValue(minValue, maxValue, defaultValue);
        }
    }
}