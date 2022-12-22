using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace AAA.Mobile.Input.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class DropArea : TouchInteractable
    {
         public bool disableObjectInteractability = false;
         public bool setPosition = true;
         public bool setRotation = true;
         [SerializeField] private int maxDroppedObjects = 1;
        
         public Transform targetTransform;

         [SerializeField] private UnityEvent onObjectDropped, onObjectReleased, onDropFailed;
        
         [ShowNonSerializedField, ReadOnly] private List<DraggableObject> droppedObjects = new List<DraggableObject>();

        public void OnObjectDropped(DraggableObject draggableObject)
        {
            droppedObjects.Add(draggableObject);
            onObjectDropped?.Invoke();
        }

        public void OnObjectReleased(DraggableObject draggableObject)
        {
            droppedObjects.Remove(draggableObject);
            onObjectReleased?.Invoke();
        }

        public bool CanObjectBeDropped()
        {
            if(droppedObjects.Count < maxDroppedObjects)
                return true;
            onDropFailed?.Invoke();
            return false;
        }
    }
}