using UnityEngine;
using Sirenix.OdinInspector;
using AAA.Utility.GlobalVariables;
using AAA.Utility.DataTypes;

namespace AAA.UI
{
    public class ChangeTextIntVariable : ChangeText
    {
        [TabGroup("References")][SerializeField] private bool useIntRange = false;
        [TabGroup("References")][SerializeField][HideIf("useIntRange")] private IntVariable intVariable;
        [TabGroup("References")][SerializeField][ShowIf("useIntRange")] private IntRangeVariable intRangeVariable;
        [TabGroup("References")][SerializeField][ShowIf("useIntRange")] private VariableValue valueType = VariableValue.Value;

        private void OnEnable()
        {
            if (useIntRange)
            {
                intRangeVariable.OnChanged += VariableChanged;
                // floatRangeVariable.Value.OnChanged += VariableChanged;
            }
            else
            {
                intVariable.OnChanged += VariableChanged;
            }
            VariableChanged();
        }
        private void OnDisable()
        {
            if (useIntRange)
            {
                intRangeVariable.OnChanged -= VariableChanged;
                // floatRangeVariable.Value.OnChanged -= VariableChanged;
            }
            else
                intVariable.OnChanged -= VariableChanged;
        }

        public void VariableChanged()
        {
            int variableValue = 0;
            if (useIntRange)
                variableValue = intRangeVariable.Value.GetValue(valueType);
            else
                variableValue = intVariable.Value;

            SetText(variableValue);
        }
    }
}