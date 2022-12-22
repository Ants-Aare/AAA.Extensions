using UnityEngine;
using UnityEngine.Rendering;


namespace AAA.Utility.General
{
    public class PostProcessFade : ValueFader
    {
        [SerializeField] private Volume volume = null;

#if UNITY_EDITOR
        void OnValidate()
        {
            if (volume == null)
                volume = GetComponentInChildren<Volume>();
        }
#endif
        void Start()
        {
            if (fadeOnStart)
            {
                volume.weight = startValue;
                FadeIn();
            }
        }
        protected override void ChangeFadeProgress(float value)
        {
            volume.weight += Mathf.Lerp(startValue, targetValue, value);

            volume.weight = Mathf.Clamp(volume.weight, startValue, targetValue);
        }
        protected override float GetFadeProgress()
        {
            return Mathf.InverseLerp(startValue, targetValue, volume.weight);
        }
    }
}