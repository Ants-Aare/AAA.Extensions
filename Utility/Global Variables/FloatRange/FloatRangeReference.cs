using UnityEngine;
using AAA.Utility.DataTypes;

namespace AAA.Utility.GlobalVariables
{
    [System.Serializable]
    public class FloatRangeReference : GlobalVariableReference<FloatRangeValue, FloatRangeVariable>
    {
        public FloatRangeReference(FloatRangeValue value)
        {
            constantValue = value;
        }
        public FloatRangeReference(float minValue, float maxValue, float defaultValue)
        {
            constantValue = new FloatRangeValue(minValue, maxValue, defaultValue);
        }
    }
}