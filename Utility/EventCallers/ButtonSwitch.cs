using System;
using UnityEngine;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;

namespace AAA.Utility
{
    public class ButtonSwitch : MonoBehaviour
    {
        [SerializeField] private bool isOn = false;
        [SerializeField] private UnityEvent onEnabled, onDisabled;
        [SerializeField] private BoolUnityEvent onChanged;

        private void Start()
        {
            Switch(isOn);
        }

        public void Switch()
        {
            Switch(!isOn);
        }
        public void Switch(bool isOn)
        {
            this.isOn = isOn;

            onChanged.Invoke(isOn);

            if (isOn)
                onEnabled.Invoke();
            else
                onDisabled.Invoke();
        }
    }
}
