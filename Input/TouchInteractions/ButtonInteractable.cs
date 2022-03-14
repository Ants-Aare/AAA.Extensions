using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;

namespace AAA.Mobile.Input.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class ButtonInteractable : TouchInteractable
    {
        [TabGroup("Properties")] [SerializeField] private bool isTapButton;

        [TabGroup("Events")] [SerializeField] private UnityEvent onButtonDeactivated, onButtonActivated;
        [TabGroup("Events")] [SerializeField] private BoolUnityEvent onPressed;

        [TabGroup("State")] [ReadOnly] private bool isActivated = false;

        public override void StartTouchInteraction(TouchInputAction touchInputAction)
        {
            if(isTapButton)
            {
                touchInputAction.onEndTouch.AddListener(DeactivateButton);
                onPressed?.Invoke(true);
                ActivateButton();
                return;
            }
            
            onPressed?.Invoke(!isActivated);

            if (isActivated)
                DeactivateButton();
            else
                ActivateButton();
        }
        [Button][HideInEditorMode]
        public void DebugPress()
        {
            onPressed?.Invoke(true);
            ActivateButton();
            Invoke("DeactivateButton", 0.3f);
        }

        public override void EndTouchInteraction(TouchInputAction touchInputAction)
        {
            if (isTapButton)
            {
                DeactivateButton(touchInputAction);
            }
        }

        public void ActivateButton()
        {
            if (isActivated)
                return;

            onButtonActivated?.Invoke();
            isActivated = true;
        }
        public void DeactivateButton()
        {
            if (!isActivated)
                return;

            onButtonDeactivated?.Invoke();
            isActivated = false;
        }
        public void DeactivateButton(TouchInputAction touchInputAction)
        {
            touchInputAction.onEndTouch.RemoveListener(DeactivateButton);
            DeactivateButton();
        }
    }
}
