using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using AAA.Utility.DataTypes;
using AAA.Utility.GlobalVariables;

namespace AAA.UI
{
    public class ChangeSliderFloatVariable : ChangeSlider
    {
        [TabGroup("Properties")][SerializeField] private bool receiveChanges = true;
        [TabGroup("Properties")][SerializeField] private bool sendChanges = false;

        [TabGroup("References")][SerializeField] private FloatRangeVariable variable = null;
        [TabGroup("Events")][SerializeField] private UnityEvent onChanged;

        private void OnEnable()
        {
            if (variable is null)
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
        void OnDisable()
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

        public void SliderChanged()
        {
            if (sendChanges)
            {
                if(useImage)
                    variable.Value.SetProgress(image.fillAmount);
                else
                    variable.Value.SetProgress(slider.value);
            }
        }
    }
}