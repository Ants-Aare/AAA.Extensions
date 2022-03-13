using System;

namespace AAA.Mobile.Input.Interactions
{
    public interface iTouchInteractable
    {
        void EnableInteractability();
        void DisableInteractability();

        void StartTouchInteraction(TouchInputAction touchInputAction);

        void EndTouchInteraction(TouchInputAction touchInputAction);
    }
}
