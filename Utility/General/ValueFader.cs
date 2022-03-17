using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

namespace AAA.Utility.General
{
    public abstract class ValueFader : MonoBehaviour
    {
        [Tooltip("In seconds")]
        [TabGroup("Properties")][SerializeField] protected float fadeInSpeed, fadeOutSpeed = 0.5f;
        [TabGroup("Properties")][SerializeField] protected bool fadeOnStart, useUnscaledTime = false;
        [TabGroup("Properties")][SerializeField] protected float startValue, targetValue = 0f;

        [Button]
        public void FadeIn()=> FadeIn(null);
        [Button]
        public void FadeOut()=> FadeOut(null);

        public void FadeIn(Action onFadeFinished)
        {
            StopAllCoroutines();
            StartCoroutine(FadeInCoroutine(onFadeFinished));
        }
        public void FadeOut(Action onFadeFinished)
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutCoroutine(onFadeFinished));
        }
        public void FadeInOut() => FadeIn(()=>StartCoroutine(FadeOutCoroutine()));
        public void FadeOutIn() => FadeOut(()=>StartCoroutine(FadeInCoroutine()));
        public void FadeInOut(float multiplier)
        {
            StopAllCoroutines();
            StartCoroutine(FadeInCoroutine(()=>StartCoroutine(FadeOutCoroutine())));
        }

        IEnumerator FadeInCoroutine(Action onFadeFinished = null)
        {
            while (GetFadeProgress() < targetValue  - 0.001f)
            {
                float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                
                ChangeFadeProgress(deltaTime / fadeInSpeed);

                yield return null;
            }
            onFadeFinished?.Invoke();
        }

        IEnumerator FadeOutCoroutine(Action onFadeFinished = null)
        {
            while (GetFadeProgress() > startValue + 0.001f)
            {
                float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
                
                ChangeFadeProgress(deltaTime / fadeInSpeed * -1);

                yield return null;
            }
            onFadeFinished?.Invoke();
        }

        protected abstract void ChangeFadeProgress(float value);
        protected abstract float GetFadeProgress();
    }
}