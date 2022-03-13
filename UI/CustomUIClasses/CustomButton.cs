using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System.Collections.Generic;
using System.Collections;
using Lean.Transition;

namespace AAA.UI.CustomUI
{
    public class CustomButton : Selectable, IBeginDragHandler, IDragHandler, IEndDragHandler, ISubmitHandler
    {
        [SerializeField]
        protected float dragThreshold = 10.0f;
        [SerializeField]
        protected bool multiDown;
        [SerializeField]
        [Tooltip("This Button can only trigger the OnClicked Event once. Use ResetIsClicked() to make it fire the event again.")]
        private bool onlyAllowClickingOnce = false;
        [SerializeField]
        [Tooltip("You can't click the button this many seconds after it has been pressed.")]
        private float clickTimeOut = 0.1f;
        [SerializeField]
        [Tooltip("Which Object to Select after Clicking the button. QOL thing. Defaults to itself when unassigned.")]
        private GameObject firstSelectedWhenClicked = null;


        [Header("Transitions")]
        [SerializeField]
        protected LeanPlayer normalTransitions = new LeanPlayer();
        [SerializeField]
        protected LeanPlayer downTransitions = new LeanPlayer();
        [SerializeField]
        protected LeanPlayer clickTransitions = new LeanPlayer();
        [SerializeField]
        protected LeanPlayer selectedTransitions = new LeanPlayer();


        [Header("Button Events")]
        [SerializeField] protected UnityEvent onDown = new UnityEvent();
        [SerializeField] protected UnityEvent onClick = new UnityEvent();
        [SerializeField] protected UnityEvent onSelect = new UnityEvent();
        [SerializeField] protected UnityEvent onDeselect = new UnityEvent();


        [Header("Button State")]
        public bool isSelected = false;
        public bool isDown = false;
        public bool isClicked = false;


        [System.NonSerialized]
        protected Vector2 totalDelta;
        protected List<int> downPointers = new List<int>();
        [System.NonSerialized]
        protected ScrollRect parentScrollRect;


        #region Select
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsInteractable())
                return;

            StartSelect();
            eventData.selectedObject = gameObject;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (!IsInteractable())
                return;

            // Debug.Log($"exiting");
            if (isDown)
            {
                if (dragThreshold == 0.0f)
                {
                    downPointers.Remove(eventData.pointerId);

                    if (downPointers.Count == 0)
                    {
                        isDown = false;
                        StopSelect();
                        DoNormal();
                    }
                }
            }
            else
            {
                StopSelect();
                DoNormal();
            }
        }

        public override void OnSelect(BaseEventData eventData)
        {
            if (!IsInteractable())
                return;

            StartSelect();
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            if (!IsInteractable())
                return;

            isDown = false;
            StopSelect();
            DoNormal();
        }

        #endregion

        #region Submitting and Clicking
        public override void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInteractable() || eventData.button != PointerEventData.InputButton.Left)
                return;

            if (navigation.mode != Navigation.Mode.None && EventSystem.current != null)
                EventSystem.current.SetSelectedGameObject(gameObject, eventData);


            totalDelta = Vector2.zero;

            downPointers.Add(eventData.pointerId);

            if (multiDown == true || downPointers.Count == 1)
            {
                DoDown();
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (downPointers.Remove(eventData.pointerId) == true)
            {
                isDown = false;
                DoClick();
            }
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (!enabled || !IsInteractable())
                return;

            eventData.selectedObject = gameObject;
            DoClick();
        }

        #endregion

        #region Drag Behaviour
        public void OnBeginDrag(PointerEventData eventData)
        {
            parentScrollRect = GetComponentInParent<ScrollRect>();

            if (parentScrollRect != null)
            {
                parentScrollRect.OnBeginDrag(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!IsInteractable())
                return;

            if (downPointers.Contains(eventData.pointerId) == true)
            {
                totalDelta += eventData.delta;

                if (dragThreshold > 0.0f && totalDelta.magnitude > dragThreshold)
                {
                    downPointers.Remove(eventData.pointerId);

                    if (downPointers.Count == 0)
                    {
                        isDown = false;
                        StopSelect();
                        DoNormal();
                    }
                }
            }

            if (parentScrollRect != null)
            {
                parentScrollRect.OnDrag(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (parentScrollRect != null)
            {
                parentScrollRect.OnEndDrag(eventData);
            }
        }

        #endregion

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            return;
        }

        private void StartSelect()
        {
            if (!isSelected)
            {
                // Debug.Log("StartSelect" + gameObject.name, gameObject);
                selectedTransitions?.Begin();
                onSelect?.Invoke();
            }
            isSelected = true;
        }

        private void StopSelect()
        {
            if (isSelected)
            {
                // Debug.Log("StopSelect" + gameObject.name, gameObject);
                onDeselect?.Invoke();
                isSelected = false;
            }
        }

        protected void DoNormal()
        {
            normalTransitions?.Begin();
        }

        protected void DoDown()
        {
            if (isDown)
                return;

            onDown?.Invoke();
            downTransitions?.Begin();

            isDown = true;
        }

        protected virtual void DoClick()
        {
            if (isClicked)
                return;

            onClick?.Invoke();
            clickTransitions?.Begin();

            if (gameObject.activeInHierarchy)
            {
                isClicked = true;
                isSelected = false;
                StopAllCoroutines();
                StartCoroutine(ClickTimeout());
            }
        }


        public void ResetIsClicked()
        {
            isClicked = false;
        }

        public IEnumerator ClickTimeout()
        {
            yield return new WaitForSeconds(clickTimeOut);

            if (firstSelectedWhenClicked != null)
                EventSystem.current?.SetSelectedGameObject(firstSelectedWhenClicked);
            else
            {
                // Debug.Log("Doing selectiontransition" + gameObject.name, gameObject);
                selectedTransitions?.Begin();
                isSelected = true;
            }

            if (!onlyAllowClickingOnce)
                ResetIsClicked();
        }
    }
}