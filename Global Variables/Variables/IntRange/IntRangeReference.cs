using UnityEngine;
using AAA.DataTypes;

namespace AAA.GlobalVariables.Variables
{
    [System.Serializable]
    public class IntRangeReference : GlobalVariableReference<IntRangeValue, IntRangeVariable>
    {
        public IntRangeReference(IntRangeValue value) : base()
        {
            ConstantValue = value;
        }
        public IntRangeReference(int minValue, int maxValue, int defaultValue)
        {
            ConstantValue = new IntRangeValue(minValue, maxValue, defaultValue);
        }
    }
}