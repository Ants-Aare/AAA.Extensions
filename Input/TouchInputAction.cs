using System.Runtime.InteropServices.WindowsRuntime;
using System;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace AAA.Mobile.Input
{
    [System.Serializable]
    public class TouchInputAction
    {
        [TabGroup("Values")][ReadOnly] public bool pressed = false;

        [TabGroup("Values")][ReadOnly] public float startTime;

        [TabGroup("Values")][ReadOnly] public Vector2 startPosition, currentPosition, deltaPosition;
        [TabGroup("Values")] [ShowInInspector, ReadOnly] private Vector2 direction;
        
        [TabGroup("Events")] public TouchInputUnityEvent onStartTouch, onEndTouch;

        public float Duration => Time.time - startTime;
        public Vector2 Direction
        {
            get
            {
                direction = currentPosition - startPosition;
                return currentPosition - startPosition;
            }
        }
        public Action<TouchInputAction> OnPositionChanged;

        public void StartTouch(Vector2 value)
        {
            startPosition = value;
            currentPosition = value;
            startTime = Time.time;
            pressed = true;
            onStartTouch.Invoke(this);
            OnPositionChanged?.Invoke(this);
        }

        public void UpdateTouch(Vector2 value)
        {
            if(startPosition == Vector2.zero)
                startPosition = value;

            deltaPosition = value - currentPosition;
            currentPosition = value;
            OnPositionChanged?.Invoke(this);
        }

        public void EndTouch(Vector2 value)
        {
            currentPosition = value;
            pressed = false;
            onEndTouch.Invoke(this);
            OnPositionChanged?.Invoke(this);
        }
    }
}

