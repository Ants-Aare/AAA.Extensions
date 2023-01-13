using UnityEngine;
using AAA.GlobalVariables.Variables;

namespace AAA.GlobalVariables.UI
{
    public class ChangeTextStringVariable : ChangeText
    {
        [SerializeField] private StringVariable stringVariable;

        private void OnEnable()
        {
            stringVariable.OnChanged += VariableChanged;
            VariableChanged();
        }

        private void OnDisable()
        {
            stringVariable.OnChanged -= VariableChanged;
        }

        public void VariableChanged()
        {
            SetText(stringVariable.Value);
        }
    }
}