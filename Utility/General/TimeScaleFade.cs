using UnityEngine;
using Sirenix.OdinInspector;

namespace AAA.Utility.General
{
    public class TimeScaleFade : ValueFader
    {
        public float currentValue = 1;
        void Start()
        {
            currentValue = Time.timeScale;
            if (fadeOnStart)
            {
                Time.timeScale = startValue;
                FadeIn();
            }
        }
        [Button]
        public void SetTimeScaleToStartValue()
        {
            currentValue = startValue;
            Time.timeScale = startValue;
        }
        [Button]
        public void SetTimeScaleToTargetValue()
        {
            currentValue = targetValue;
            Time.timeScale = targetValue;
        }
        protected override void ChangeFadeProgress(float value)
        {
            // Mathf.Lerp gave me an outpor of 0 when t was negative no clue why but this will do for now
            float valueDelta = targetValue * value;
            currentValue = Mathf.Clamp(currentValue + valueDelta, startValue, targetValue);

            Time.timeScale = currentValue;
        }
        protected override float GetFadeProgress()
        {
            return Mathf.InverseLerp(startValue, targetValue, Time.timeScale);
        }
    }
}