using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace AAA.Mobile.Input.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class DropArea : TouchInteractable
    {
        [TabGroup("Properties")] public bool disableObjectInteractability = false;
        [TabGroup("Properties")] public bool setPosition = true;
        [TabGroup("Properties")] public bool setRotation = true;
        [TabGroup("Properties")] [SerializeField] private int maxDroppedObjects = 1;
        
        [TabGroup("References")] public Transform targetTransform;

        [TabGroup("Events")] [SerializeField] private UnityEvent onObjectDropped, onObjectReleased, onDropFailed;
        
        [TabGroup("State")] [ShowInInspector, ReadOnly] private List<DraggableObject> droppedObjects = new List<DraggableObject>();

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