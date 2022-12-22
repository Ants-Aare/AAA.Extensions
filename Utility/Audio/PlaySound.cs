using System.Runtime.CompilerServices;
using UnityEngine;


namespace AAA.Utility.Audio
{
    public class PlaySound : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioSource source = null;
        [SerializeField] private SoundList randomizedSounds1, randomizedSounds2, multipleSounds, soundsList;

        public void PlaySpecificSound(int soundIndex)
        {
            AudioClip audioClip = soundsList.GetByIndex(soundIndex);
            TryPlayClip(audioClip);
        }

        public void TryPlayClip(AudioClip audioClip)
        {
            if (audioClip != null)
                source.PlayOneShot(audioClip);
            else
                Debug.LogError("No such audioclip could be found", gameObject);
        }

        public void PlayRandomizedSound1()
        {
            AudioClip audioClip = randomizedSounds1.GetRandom();
            TryPlayClip(audioClip);
        }
        public void PlayRandomizedSound2()
        {
            AudioClip audioClip = randomizedSounds1.GetRandom();
            TryPlayClip(audioClip);
        }
        public void PlayMultipleSounds()
        {
            foreach (var sound in multipleSounds.GetAll())
            {
                source.PlayOneShot(sound);
            }
        }
    }
}