using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;
using AAA.Utility.GlobalVariables;
using AAA.Utility.DataTypes;

namespace AAA.Utility.Audio
{
    public class AudioMixerChangeVariable : MonoBehaviour
    {
        [TabGroup("Properties")][SerializeField] private string parameterName = "";
        [TabGroup("Properties")][SerializeField] private bool useProgress = false;
        [TabGroup("Properties")][SerializeField] private bool setValueUsingLogarithmicScaling = false;
        [TabGroup("References")][SerializeField] private FloatRangeVariable variable;
        [TabGroup("References")][SerializeField] private AudioMixer audioMixer = null;

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