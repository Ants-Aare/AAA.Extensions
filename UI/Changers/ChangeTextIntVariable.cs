using UnityEngine;

using AAA.Utility.GlobalVariables;
using AAA.Utility.DataTypes;
using NaughtyAttributes;

namespace AAA.UI
{
    public class ChangeTextIntVariable : ChangeText
    {
        [SerializeField] private bool useIntRange = false;
        [SerializeField][HideIf("useIntRange")] private IntVariable intVariable;
        [SerializeField][ShowIf("useIntRange")] private IntRangeVariable intRangeVariable;
        [SerializeField][ShowIf("useIntRange")] private VariableValue valueType = VariableValue.Value;

        private void OnEnable()
        {
            if (useIntRange)
            {
                intRangeVariable.OnChanged += VariableChanged;
                // intRangeVariable.Value.OnChanged += VariableChanged;
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
                // intRangeVariable.Value.OnChanged -= VariableChanged;
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