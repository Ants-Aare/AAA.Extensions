using UnityEngine;
using System.Collections.Generic;

public class AnimatorFunctions : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private SoundList[] sounds;
    [SerializeField]
    private GameObject targetGameobject;

    void Start()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    #region Region
    public void PlayRandomSound(int soundIndex)
    {
        if (audioSource == null)
        {
            Debug.LogError("This is script is trying to play a sound, but no audiosource was found.");
            return;
        }

        AudioClip audioClip = sounds[soundIndex].GetRandom();
        if (audioClip != null)
            audioSource.PlayOneShot(audioClip);
    }
    public void ActivateGameObject(int isActive)
    {
        if (targetGameobject == null)
            return;

        targetGameobject.SetActive(isActive != 0);
    }

    #endregion
}