using AAA.DataTypes;
using UnityEngine;
using AAA.GlobalVariables.Variables;
using NaughtyAttributes;

namespace AAA.GlobalVariables.UI
{
    public class ChangeTextFloatVariable : ChangeText
    {
        [SerializeField] private bool useFloatRange = false;

        [SerializeField] [HideIf("useFloatRange")]
        private FloatVariable floatVariable;

        [SerializeField] [ShowIf("useFloatRange")]
        private FloatRangeVariable floatRangeVariable;

        [SerializeField] [ShowIf("useFloatRange")]
        private VariableValue valueType = VariableValue.Value;

        private void OnEnable()
        {
            if (useFloatRange)
                floatRangeVariable.OnChanged += VariableChanged;
            else
                floatVariable.OnChanged += VariableChanged;

            VariableChanged();
        }

        private void OnDisable()
        {
            if (useFloatRange)
                floatRangeVariable.OnChanged -= VariableChanged;
            else
                floatVariable.OnChanged -= VariableChanged;
        }

        public void VariableChanged()
        {
            var variableValue = useFloatRange ? floatRangeVariable.Value.GetValue(valueType) : floatVariable.Value;

            SetText(variableValue);
        }
    }
}