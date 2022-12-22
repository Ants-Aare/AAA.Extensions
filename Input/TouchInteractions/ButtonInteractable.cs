using UnityEngine;

using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;
using NaughtyAttributes;

namespace AAA.Mobile.Input.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class ButtonInteractable : TouchInteractable
    {
         [SerializeField] private bool isTapButton;

         [SerializeField] private UnityEvent onButtonDeactivated, onButtonActivated;
         [SerializeField] private BoolUnityEvent onPressed;

         [ReadOnly] private bool isActivated = false;

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
        [Button]
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
