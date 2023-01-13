using UnityEngine;
using UnityEngine.Events;
using AAA.GlobalVariables.Variables;

namespace AAA.GlobalVariables.UI
{
    public class ChangeSliderIntVariable : ChangeSlider
    {
        [SerializeField] private bool receiveChanges = true;
        [SerializeField] private bool sendChanges = false;

        [SerializeField] private IntRangeVariable variable = null;
        [SerializeField] private UnityEvent onChanged;

        private void OnEnable()
        {
            if (variable == null)
            {
                Destroy(this);
                Debug.Log("There are missing references for " + gameObject.name, gameObject);
                return;
            }

            if (receiveChanges)
            {
                variable.OnChanged += VariableChanged;
                SetSliderProgress(variable.Value);
            }
        }

        private void OnDisable()
        {
            if (receiveChanges)
            {
                variable.OnChanged -= VariableChanged;
            }
        }

        public void VariableChanged()
        {
            SetSliderProgress(variable.Value);
            onChanged?.Invoke();
        }

        public void SendSliderChanges()
        {
            if (sendChanges)
            {
                variable.Value.SetProgress(useImage ? image.fillAmount : slider.value);
            }
        }
    }
}