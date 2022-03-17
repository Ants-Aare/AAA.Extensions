using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using AAA.Utility.DataTypes;

namespace AAA.Utility.General
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomizePitch : MonoBehaviour
    {
        [SerializeField] private FloatRangeValue range;
        [SerializeField] private AudioSource audioSource = null;

        void OnValidate()
        {
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
        }
        void Start()
        {
            RandomizePitchValue();
        }
        public void RandomizePitchValue()
        {
            audioSource.pitch = range.RandomValue();
        }
    }
}