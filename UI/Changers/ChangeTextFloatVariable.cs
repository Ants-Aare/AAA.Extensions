using UnityEngine;

using AAA.Utility.GlobalVariables;
using AAA.Utility.DataTypes;
using NaughtyAttributes;

namespace AAA.UI
{
    public class ChangeTextFloatVariable : ChangeText
    {
        [SerializeField] private bool useFloatRange = false;
        [SerializeField][HideIf("useFloatRange")] private FloatVariable floatVariable;
        [SerializeField][ShowIf("useFloatRange")] private FloatRangeVariable floatRangeVariable;
        [SerializeField][ShowIf("useFloatRange")] private VariableValue valueType = VariableValue.Value;

        private void OnEnable()
        {
            if (useFloatRange)
            {
                floatRangeVariable.OnChanged += VariableChanged;
                // floatRangeVariable.Value.OnChanged += VariableChanged;
            }
            else
            {
                floatVariable.OnChanged += VariableChanged;
            }
            VariableChanged();
        }
        private void OnDisable()
        {
            if (useFloatRange)
            {
                floatRangeVariable.OnChanged -= VariableChanged;
                // floatRangeVariable.Value.OnChanged -= VariableChanged;
            }
            else
                floatVariable.OnChanged -= VariableChanged;
        }

        public void VariableChanged()
        {
            float variableValue = 0f;
            if (useFloatRange)
                variableValue = floatRangeVariable.Value.GetValue(valueType);
            else
                variableValue = floatVariable.Value;

            SetText(variableValue);
        }
    }
}