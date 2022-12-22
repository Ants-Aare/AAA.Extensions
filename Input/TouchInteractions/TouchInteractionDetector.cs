using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using AAA.Utility.Math;
using AAA.Utility.GlobalVariables;

namespace AAA.Mobile.Input.Interactions
{
    public class TouchInteractionDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private LayerMask UILayerMask;
        [SerializeField] private BoolReference allowInteractions;
        private Camera activeCamera;

        private void Awake()
        {
            activeCamera = Camera.main;
        }

        public void TryStartTouchInteraction(TouchInputAction inputTouchAction)
        {
            if (!CanInteract(inputTouchAction))
                return;

            iTouchInteractable touchInteractable = GetInteractableAtPosition(inputTouchAction.currentPosition);
            touchInteractable?.StartTouchInteraction(inputTouchAction);
        }

        

        public void EndTouchInteraction(TouchInputAction inputTouchAction)
        {
            if (!CanInteract(inputTouchAction))
                return;

            iTouchInteractable touchInteractable = GetInteractableAtPosition(inputTouchAction.currentPosition);
            touchInteractable?.EndTouchInteraction(inputTouchAction);
        }

        private bool CanInteract(TouchInputAction inputTouchAction)
        {
            if(!allowInteractions.Value)
                return false;
            if (EventSystem.current.IsPointerOverGameObject())
                return false;
            if(IsPositionOverUIElement(ScreenUtility.ViewportToScreen(inputTouchAction.currentPosition)))
                return false;
            return true;
        }

        private iTouchInteractable GetInteractableAtPosition(Vector2 position)
        {
            Ray ray = activeCamera.ViewportPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
                return hit.collider.GetComponent<iTouchInteractable>();
            return null;
        }


        // TODO Maybe put all of this into a separate class?
        #region Checking UI Overlap
        public bool IsPositionOverUIElement(Vector2 position)
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults(position));
        }

        private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults)
        {
            for (int index = 0; index < eventSystemRaycastResults.Count; index++)
            {
                RaycastResult raycastResult = eventSystemRaycastResults[index];
                if (raycastResult.gameObject.layer == UILayerMask)
                    return true;
            }
            return false;
        }
    
    
        //Gets all event system raycast results of current mouse or touch position.
        static List<RaycastResult> GetEventSystemRaycastResults(Vector2 position)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = position;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
        #endregion
    }
}