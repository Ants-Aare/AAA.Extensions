using UnityEngine;
using Sirenix.OdinInspector;
using AAA.Utility.GlobalVariables;
using AAA.Utility.DataTypes;

namespace AAA.UI
{
    public class ChangeTextFloatVariable : ChangeText
    {
        [TabGroup("References")][SerializeField] private bool useFloatRange = false;
        [TabGroup("References")][SerializeField][HideIf("useFloatRange")] private FloatVariable floatVariable;
        [TabGroup("References")][SerializeField][ShowIf("useFloatRange")] private FloatRangeVariable floatRangeVariable;
        [TabGroup("References")][SerializeField][ShowIf("useFloatRange")] private VariableValue valueType = VariableValue.Value;

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