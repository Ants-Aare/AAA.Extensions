using UnityEngine;
using UnityEngine.Audio;

using AAA.GlobalVariables.Variables;
using AAA.DataTypes;

namespace AAA.Utility.Audio
{
    public class AudioMixerChangeVariable : MonoBehaviour
    {
        [SerializeField] private string parameterName = "";
        [SerializeField] private bool useProgress = false;
        [SerializeField] private bool setValueUsingLogarithmicScaling = false;
        [SerializeField] private FloatRangeVariable variable;
        [SerializeField] private AudioMixer audioMixer = null;

        private void OnEnable()
        {
            variable.OnChanged += UpdateAudioMixer;

            UpdateAudioMixer();
        }

        private void OnDisable()
        {
            variable.OnChanged -= UpdateAudioMixer;
        }

        public void UpdateAudioMixer()
        {
            float value = useProgress ? variable.Value.GetProgress() : variable.Value.Value;
            SetAudioMixerValue(value);
        }

        public void SetAudioMixerValue(float value)
        {
            if (setValueUsingLogarithmicScaling)
                value = Mathf.Log10(value) * 20;

            audioMixer.SetFloat(parameterName, value);
        }
    }
}