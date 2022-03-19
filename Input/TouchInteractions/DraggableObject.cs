using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Events;
using AAA.Utility.CustomUnityEvents;

namespace AAA.Mobile.Input.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class DraggableObject : TouchInteractable
    {
        [TabGroup("Properties")] [SerializeField] protected float smoothDragFactor = 0.1f;
        [TabGroup("Properties")] [SerializeField] protected LayerMask dropLayerMask;
        [TabGroup("Properties")] [SerializeField] protected LayerMask collisionLayerMask;


        [TabGroup("References")] [SerializeField] protected Transform transformToMove;
        [TabGroup("References")] [SerializeField] protected Transform dropOffsetTransform;
        [TabGroup("References")] [SerializeField] private bool returnToStartPosition = false;
        [TabGroup("References")] [SerializeField] [ShowIf("returnToStartPosition")] protected Transform startPositionTransform;

        [TabGroup("Events")] [SerializeField] private TouchInputUnityEvent onStartDrag, onEndDrag;
        [TabGroup("Events")] [SerializeField] private UnityEvent onDropAreaLeft;
        [TabGroup("Events")] [SerializeField] private TransformUnityEvent onDropped;

        protected Camera activeCamera;
        protected TouchInputAction inputTouchAction;
        protected Vector3 offset, currentVelocity = Vector3.zero;
        protected DropArea activeDropArea;

        private void Awake()
        {
            activeCamera = Camera.main;
            if(returnToStartPosition && startPositionTransform == null)
            {
                startPositionTransform = new GameObject().transform;
                startPositionTransform.position = transform.position;
                startPositionTransform.rotation = transform.rotation;
            }
        }


        // TODO
        protected virtual bool CanDrag()
        {
            return true;
        }

        public void Reset()
        {
            if(returnToStartPosition)
            {
                transformToMove.position = startPositionTransform.position;
                transformToMove.rotation = startPositionTransform.rotation;
            }
            if(enableOnStart)
                EnableInteractability();
            else
                DisableInteractability();
            
            if(activeDropArea != null)
            {
                activeDropArea = null;
                onDropAreaLeft?.Invoke();
            }
            currentVelocity = Vector3.zero;
        }


        #region Draggable Methods

        public override void StartTouchInteraction(TouchInputAction touchInputAction)
        {
            StopAllCoroutines();
            this.inputTouchAction = touchInputAction;
            touchInputAction.onEndTouch.AddListener(OnTouchReleased);

            offset = activeCamera.WorldToViewportPoint(transformToMove.position) - (Vector3)touchInputAction.currentPosition;
            DisableInteractability();
            StartCoroutine(DragUpdate());
            onStartDrag.Invoke(touchInputAction);
        }

        public virtual void OnTouchReleased(TouchInputAction inputTouchAction)
        {
            StopAllCoroutines();
            EnableInteractability();
            this.inputTouchAction = null;
            inputTouchAction.onEndTouch.RemoveListener(OnTouchReleased);

            if(activeDropArea != null)
            {
                activeDropArea.OnObjectReleased(this);
                activeDropArea = null;
                onDropAreaLeft?.Invoke();
            }

            if(!TryDropObject(inputTouchAction.currentPosition))
            {
                if(returnToStartPosition && startPositionTransform != null)
                {
                    StartCoroutine(MoveToNewTransform(startPositionTransform));
                }
            }

            onEndDrag.Invoke(inputTouchAction);
        }

        private bool TryDropObject(Vector2 position)
        {
            Ray ray = activeCamera.ViewportPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, dropLayerMask))
            {
                DropArea dropArea = hit.collider.GetComponent<DropArea>();

                if(dropArea == null)
                    return false;

                Debug.Log("Dropped onto " + dropArea.gameObject.name);
                
                if(!dropArea.CanObjectBeDropped())
                {
                    Debug.Log("Could not be released, droparea is full.");
                    return false;
                }

                if(dropArea.disableObjectInteractability)
                    DisableInteractability();

                if(dropArea.setPosition)
                {
                    Vector3 targetPosition = dropArea.targetTransform.position;
                    if(dropOffsetTransform != null)
                        targetPosition = dropArea.targetTransform.position - transformToMove.TransformVector(dropOffsetTransform.localPosition);

                    StartCoroutine(MoveToPosition(targetPosition, ()=> OnFinishedDropping(dropArea)));
                }
                else
                    OnFinishedDropping(dropArea);

                if(dropArea.setRotation)
                {
                    StartCoroutine(TurnToRotation(dropArea.targetTransform.rotation));
                }
                return true;
            }

            return false;
        }

        private void OnFinishedDropping(DropArea dropArea)
        {
            dropArea.OnObjectDropped(this);
            onDropped?.Invoke(dropArea.transform);
            activeDropArea = dropArea;
        }

        private IEnumerator MoveToNewTransform(Transform newTransform, bool setRotation = true)
        {
            bool hasArrived = false;
            while(!hasArrived)
            {
                yield return new WaitForFixedUpdate();
                transformToMove.position = Vector3.Lerp(transformToMove.position, newTransform.position, 0.2f);
                transformToMove.rotation = Quaternion.Slerp(transformToMove.rotation, newTransform.rotation, 0.2f);
                float distance = (transformToMove.position - newTransform.position).sqrMagnitude;
                float angle = Quaternion.Angle(transformToMove.rotation, newTransform.rotation);

                if (distance < 0.1f * 0.1f && angle < 2f)
                {
                    transformToMove.position = newTransform.position;
                    transformToMove.rotation = newTransform.rotation;
                    hasArrived = true;
                }
            }
        }
        private IEnumerator MoveToPosition(Vector3 newPosition, Action callback = null)
        {
            bool hasArrived = false;
            while(!hasArrived)
            {
                yield return new WaitForFixedUpdate();
                transformToMove.position = Vector3.Lerp(transformToMove.position, newPosition, 0.2f);
                float distance = (transformToMove.position - newPosition).sqrMagnitude;

                if (distance < 0.1f * 0.1f)
                {
                    transformToMove.position = newPosition;
                    hasArrived = true;
                }
            }
            callback?.Invoke();
        }
        private IEnumerator TurnToRotation(Quaternion newRotation)
        {
            bool hasArrived = false;
            while(!hasArrived)
            {
                yield return new WaitForFixedUpdate();
                transformToMove.rotation = Quaternion.Slerp(transformToMove.rotation, newRotation, 0.2f);
                float angle = Quaternion.Angle(transformToMove.rotation, newRotation);

                if (angle < 2f)
                {
                    transformToMove.rotation = newRotation;
                    hasArrived = true;
                }
            }
        }

        private IEnumerator DragUpdate()
        {
            while(true)
            {
                if (CanDrag())
                {
                    PerformDragUpdate();
                }
                yield return null;
            }
        }

        protected virtual void PerformDragUpdate()
        {
            Vector3 viewportPosition = (Vector3)inputTouchAction.currentPosition + offset;

            Vector3 targetPosition = activeCamera.ViewportToWorldPoint(viewportPosition);

            if (Physics.Raycast(activeCamera.transform.position, targetPosition, out RaycastHit hit, 100f, collisionLayerMask))
            {
                // if the raycast hit point is closer to the camera than the target Position, then we must have collided with something
                if((hit.point - activeCamera.transform.position).sqrMagnitude < (targetPosition - activeCamera.transform.position).sqrMagnitude)
                    targetPosition = hit.point;
            }
            transformToMove.position = Vector3.SmoothDamp(transformToMove.position, targetPosition, ref currentVelocity, smoothDragFactor);
        }
        #endregion
    }
}