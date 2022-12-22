using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using AAA.Utility.General;


namespace AAA.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupFade : ValueFader
    {
        [SerializeField] private bool destroyOnFadeOut;
        [SerializeField] private CanvasGroup canvasGroup;
        
#if UNITY_EDITOR

        void OnValidate()
        {
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();
        }
#endif
        void Start()
        {
            if (fadeOnStart)
            {
                canvasGroup.alpha = 0;
                FadeIn();
            }
        }
        protected override void ChangeFadeProgress(float value)
        {
            canvasGroup.alpha += value;

            canvasGroup.alpha = Mathf.Clamp(canvasGroup.alpha, startValue, targetValue);
        }
        protected override float GetFadeProgress()
        {
            return Mathf.InverseLerp(startValue, targetValue, canvasGroup.alpha);
        }
        protected override void OnFadeOutFinished()
        {
            base.OnFadeOutFinished();
            if(destroyOnFadeOut)
                Destroy(gameObject);
        }
    }
}