using System.Net.Mail;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AAA.Mobile.Input.Interactions
{
    [RequireComponent(typeof(Collider))]
	public class TouchableObject : TouchInteractable
    {
        [Tooltip("The time (in seconds) it takes for a tap to count as a hold.")]
        [SerializeField] private float holdThresholdTime = 1.0f;

		[SerializeField] private TouchInputUnityEvent onTapped, onHeld;

        public override void EndTouchInteraction(TouchInputAction touchInputAction)
        {
            if (touchInputAction.Duration < holdThresholdTime)
                OnTapped(touchInputAction);
            else
                OnHeld(touchInputAction);
        }

        protected virtual void OnTapped(TouchInputAction touchInputAction)
        {
            onTapped.Invoke(touchInputAction);
        }
        protected virtual void OnHeld(TouchInputAction touchInputAction)
        {
            onHeld.Invoke(touchInputAction);
        }
    }
}
