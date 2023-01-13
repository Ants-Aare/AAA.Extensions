using UnityEngine;

using AAA.GlobalVariables.Variables;
using AAA.DataTypes;
using NaughtyAttributes;

namespace AAA.GlobalVariables.UI
{
    public class ChangeTextIntVariable : ChangeText
    {
        [SerializeField] private bool useIntRange;
        [SerializeField][HideIf("useIntRange")] private IntVariable intVariable;
        [SerializeField][ShowIf("useIntRange")] private IntRangeVariable intRangeVariable;
        [SerializeField][ShowIf("useIntRange")] private VariableValue valueType = VariableValue.Value;

        private void OnEnable()
        {
            if (useIntRange)
                intRangeVariable.OnChanged += VariableChanged;
            else
                intVariable.OnChanged += VariableChanged;
            
            VariableChanged();
        }
        private void OnDisable()
        {
            if (useIntRange)
                intRangeVariable.OnChanged -= VariableChanged;
            else
                intVariable.OnChanged -= VariableChanged;
        }

        public void VariableChanged()
        {
            var variableValue = useIntRange ? intRangeVariable.Value.GetValue(valueType) : intVariable.Value;

            SetText(variableValue);
        }
    }
}