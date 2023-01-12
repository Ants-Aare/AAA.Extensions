using UnityEngine;
using AAA.Utility.DataTypes;

namespace AAA.Utility.GlobalVariables
{
    [System.Serializable]
    public class IntRangeReference : GlobalVariableReference<IntRangeValue, IntRangeVariable>
    {
        public IntRangeReference(IntRangeValue value)
        {
            ConstantValue = value;
        }
        public IntRangeReference(int minValue, int maxValue, int defaultValue)
        {
            ConstantValue = new IntRangeValue(minValue, maxValue, defaultValue);
        }
    }
}