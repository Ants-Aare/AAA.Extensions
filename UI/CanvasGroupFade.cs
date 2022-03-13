using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace AAA.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupFade : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private float fadeInSpeed, fadeOutSpeed = 0.5f;
        [SerializeField] private bool fadeOnStart, destroyOnExit, useUnscaledTime = false;

        [Header("References")]
        [SerializeField] private CanvasGroup canvasGroup = null;
        
        void OnValidate()
        {
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();
        }
        void OnEnable()
        {
            if(fadeOnStart)
            {
                canvasGroup.alpha = 0;
                Enter(true);
            }
        }
        public void Enter()
        {
            Enter(null);
        }
        public void Exit()
        {
            Exit(null);
        }

        public void Enter(bool fadeFromBeginning)
        {
            if(fadeFromBeginning)
                canvasGroup.alpha = 0;
            Enter(null);
        }
        public void Exit(bool fadeFromBeginning)
        {
            if(fadeFromBeginning)
                canvasGroup.alpha = 1;
            Exit(null);
        }

        public void Enter(Action onFadeFinished = null)
        {
            StartCoroutine(FadeIn(fadeInSpeed, onFadeFinished));
        }
        public void Exit(Action onFadeFinished = null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut(fadeOutSpeed, onFadeFinished));
        }

        public void Enter(float speed, Action onFadeFinished = null)
        {
            StartCoroutine(FadeIn(speed, onFadeFinished));
        }
        public void Exit(float speed, Action onFadeFinished = null)
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut(speed, onFadeFinished));
        }

        public void FadeInOut()
        {
            StopAllCoroutines();
            StartCoroutine(FadeIn(fadeInSpeed, ()=>StartCoroutine(FadeOut(fadeOutSpeed))));
        }
        public void FadeOutIn()
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut(fadeOutSpeed, ()=>StartCoroutine(FadeIn(fadeInSpeed))));
        }

        #region Coroutines
        private IEnumerator FadeIn(float speed, Action onFadeFinished = null)
        {
            if(speed == 0)
            {
                canvasGroup.alpha = 1;
                yield return null;
            }
            else
            {
                while (canvasGroup.alpha < 1)
                {
                    if(useUnscaledTime)
                    canvasGroup.alpha += Time.unscaledDeltaTime / speed;
                else
                    canvasGroup.alpha += Time.deltaTime / speed;
                    if(canvasGroup.alpha > 1)
                        canvasGroup.alpha = 1;

                    yield return null;
                }
            }
            if(onFadeFinished != null)
                onFadeFinished();
        }

        private IEnumerator FadeOut(float speed, Action onFadeFinished = null)
        {
            while (canvasGroup.alpha > 0)
            {
                if(useUnscaledTime)
                    canvasGroup.alpha -= Time.unscaledDeltaTime / speed;
                else
                    canvasGroup.alpha -= Time.deltaTime / speed;

                if(canvasGroup.alpha < 0)
                    canvasGroup.alpha = 0;

                yield return null;
            }
            if(onFadeFinished != null)
                onFadeFinished();
            if(destroyOnExit)
                Destroy(gameObject);
        }
        #endregion
    }
}