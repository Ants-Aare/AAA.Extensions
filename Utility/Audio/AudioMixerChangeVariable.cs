using UnityEngine;
using UnityEngine.Audio;
using AAA.Utility.GlobalVariables;

public class AudioMixerChangeVariable : MonoBehaviour
{
    [SerializeField]
    private FloatRangeVariable variable;
    [SerializeField]
    private bool useProgress = false;
    [SerializeField]
    private bool setValueUsingLogarithmicScaling = false;
    [Header("References")]
    [SerializeField]
    private AudioMixer audioMixer = null;
    [SerializeField]
    private string parameterName = "";
    
    private void Start()
    {
        variable.OnChanged += UpdateAudioMixer;

        UpdateAudioMixer();
    }

    public void UpdateAudioMixer()
    {
        float value = useProgress ? variable.Value.GetProgress() : variable.Value.Value;
        if(setValueUsingLogarithmicScaling)
            value = Mathf.Log10(value) * 20;

        audioMixer.SetFloat(parameterName, value);
    }
}