using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private AudioSource source = null;
    [SerializeField]
    private AudioClip[] randomizedSounds1;
    [SerializeField]
    private AudioClip[] randomizedSounds2;
    [SerializeField]
    private AudioClip[] multipleSounds;
    [SerializeField]
    private AudioClip[] soundsList;

    public void PlaySpecificSound(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < soundsList.Length)
            source.PlayOneShot(soundsList[soundIndex]);
        else
            Debug.LogError("index is too big", gameObject);
    }
    public void PlayRandomizedSound1()
    {
        if (randomizedSounds1.Length > 0)
            source.PlayOneShot(randomizedSounds1[Random.Range(0, randomizedSounds2.Length)]);
    }
    public void PlayRandomizedSound2()
    {
        if (randomizedSounds2.Length > 0)
            source.PlayOneShot(randomizedSounds2[Random.Range(0, randomizedSounds2.Length)]);
    }
    public void PlayMultipleSounds()
    {
        for (int i = 0; i < multipleSounds.Length; i++)
        {
            source.PlayOneShot(multipleSounds[i]);
        }
    }
}