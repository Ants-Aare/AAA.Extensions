using System;
using AAA.GlobalVariables.Variables;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace AAA.GlobalVariables.Utility
{
    public class CursorBehaviour : MonoBehaviour
    {
        [SerializeField] private BoolVariable variable;
        [BoxGroup("")][SerializeField] private CursorLockMode cursorModeOnTrue;
        [SerializeField] private bool cursorVisibleOnTrue;
        [SerializeField] private CursorLockMode cursorModeOnFalse;
        [SerializeField] private bool cursorVisibleOnFalse;

        private void Start()
        {
            OnChanged();
        }

        private void OnEnable()
        {
            variable.OnChanged += OnChanged;
        }
        private void OnDisable()
        {
            variable.OnChanged -= OnChanged;
        }

        private void OnChanged()
        {
            Cursor.visible = variable
                ? cursorVisibleOnTrue
                : cursorVisibleOnFalse;
            Cursor.lockState = variable
                ? cursorModeOnTrue
                : cursorModeOnFalse;
        }
    }
}