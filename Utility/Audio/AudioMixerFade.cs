using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;
using AAA.Utility.General;

namespace AAA.Utility.Audio
{
    public class AudioMixerFade : ValueFader
    {
        [TabGroup("References")][SerializeField] private string parameterName = "";
        [TabGroup("References")][SerializeField] private AudioMixer audioMixer = null;

        private float currentValue;

        void Start()
        {
            if (fadeOnStart)
            {
                audioMixer.SetFloat(parameterName, startValue);
                currentValue = startValue;
                FadeIn();
                return;
            }
            audioMixer.GetFloat(parameterName, out currentValue);
        }

        protected override void ChangeFadeProgress(float value)
        {
            currentValue += Mathf.Lerp(startValue, targetValue, value);

            currentValue = Mathf.Clamp(currentValue, startValue, targetValue);

            audioMixer.SetFloat(parameterName, currentValue);
        }
        protected override float GetFadeProgress()
        {
            return Mathf.InverseLerp(startValue, targetValue, currentValue);
        }
    }
}