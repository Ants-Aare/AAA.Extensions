using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Sirenix.OdinInspector;
using AAA.Utility.Math;

namespace AAA.Mobile.Input
{
    public class TouchInputManager : MonoBehaviour
    {
        [ShowInInspector] private PlayerControls playerControls;

        [SerializeField] private TouchInputAction primaryInputTouchAction = new TouchInputAction();
        [SerializeField] private TouchInputAction secondaryInputTouchAction = new TouchInputAction();

        public void Awake()
        {
            playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            TouchSimulation.Enable();
            playerControls.Enable();
            playerControls.CombatControls.Enable();

            playerControls.CombatControls.PrimaryContact.started += PrimaryTouchStarted;
            playerControls.CombatControls.PrimaryContact.canceled += PrimaryTouchEnded;
            playerControls.CombatControls.PrimaryPosition.performed+= UpdatePrimaryTouch;

            playerControls.CombatControls.SecondaryContact.started += SecondaryTouchStarted;
            playerControls.CombatControls.SecondaryContact.canceled += SecondaryTouchEnded;

            //Debug.Log("Enabling controls");
        }


        private void OnDisable()
        {
            playerControls.CombatControls.PrimaryContact.started -= PrimaryTouchStarted;
            playerControls.CombatControls.PrimaryContact.canceled -= PrimaryTouchEnded;

            playerControls.CombatControls.SecondaryContact.started -= SecondaryTouchStarted;
            playerControls.CombatControls.SecondaryContact.canceled -= SecondaryTouchEnded;
            playerControls.Disable();
        }

        private void PrimaryTouchStarted(InputAction.CallbackContext context)
        {
            Vector2 value = ScreenUtility.ScreenToViewPort(playerControls.CombatControls.PrimaryPosition.ReadValue<Vector2>());
            primaryInputTouchAction.StartTouch(value);
        }

        private void UpdatePrimaryTouch(InputAction.CallbackContext context)
        {
            Vector2 value = ScreenUtility.ScreenToViewPort(playerControls.CombatControls.PrimaryPosition.ReadValue<Vector2>());
            primaryInputTouchAction.UpdateTouch(value);
        }

        private void PrimaryTouchEnded(InputAction.CallbackContext context)
        {
            Vector2 value = ScreenUtility.ScreenToViewPort(playerControls.CombatControls.PrimaryPosition.ReadValue<Vector2>());
            primaryInputTouchAction.EndTouch(value);
        }

        private void SecondaryTouchStarted(InputAction.CallbackContext context)
        {
            Vector2 value = ScreenUtility.ScreenToViewPort(playerControls.CombatControls.SecondaryPosition.ReadValue<Vector2>());
            secondaryInputTouchAction.StartTouch(value);
        }

        private void UpdateSecondaryTouch(InputAction.CallbackContext context)
        {
            Vector2 value = ScreenUtility.ScreenToViewPort(playerControls.CombatControls.SecondaryPosition.ReadValue<Vector2>());
            secondaryInputTouchAction.UpdateTouch(value);
        }

        private void SecondaryTouchEnded(InputAction.CallbackContext context)
        {
            Vector2 value = ScreenUtility.ScreenToViewPort(playerControls.CombatControls.SecondaryPosition.ReadValue<Vector2>());
            secondaryInputTouchAction.EndTouch(value);
        }
    }
}
