using System.Xml.Serialization;
using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;
using AAA.Utility.GlobalVariables;

public class AudioMixerFade : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private float fadeInSpeed = 0.5f;
    [SerializeField]
    private float fadeOutSpeed = 0.5f;
    [SerializeField]
    private bool fadeOnStart = false;
    [SerializeField]
    private bool useUnscaledTime = false;

    [SerializeField]
    private float startValue = 0f;
    [SerializeField]
    private float targetValue = 1f;

    [Header("References")]
    [SerializeField]
    private AudioMixer audioMixer = null;
    [SerializeField]
    private string parameterName = "";

    void Awake()
    {
        if (audioMixer == null)
            audioMixer = GetComponent<AudioMixer>();
        if (audioMixer == null)
        {
            Destroy(this);
            Debug.LogError("There is no Audiomixer on this GameObject: " + gameObject.name);
        }
    }
    void OnEnable()
    {
        if(fadeOnStart)
        {
            audioMixer.SetFloat(parameterName, startValue);
            Enter();
        }
    }
    public void Enter()
    {
        Enter(null);
    }
    public void Exit()
    {
        Exit(null);
    }

    public void Enter(Action onFadeFinished)
    {
        StartCoroutine(FadeIn(fadeInSpeed, onFadeFinished));
    }
    public void Exit(Action onFadeFinished)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(fadeOutSpeed, onFadeFinished));
    }

    #region Coroutines
    private IEnumerator FadeIn(float speed, Action onFadeFinished)
    {
        if(speed == 0)
        {
            audioMixer.SetFloat(parameterName, targetValue);
            yield return null;
        }
        else
        {
            audioMixer.GetFloat(parameterName, out float currentValue);

            float progress = Mathf.Clamp(Mathf.InverseLerp(startValue, targetValue, currentValue), 0f, 0.99f);

            while (progress < 1)
            {
                if(useUnscaledTime)
                    progress += Time.unscaledDeltaTime / fadeInSpeed;
                else
                    progress += Time.deltaTime / fadeInSpeed;

                if (progress > 1)
                {
                    progress = 1;
                }
                audioMixer.SetFloat(parameterName, Mathf.Lerp(startValue, targetValue, progress));
                yield return null;
            }
        }
        if(onFadeFinished != null)
            onFadeFinished();
    }

    private IEnumerator FadeOut(float speed, Action onFadeFinished)
    {
        if(speed == 0)
        {
            audioMixer.SetFloat(parameterName, startValue);
            yield return null;
        }
        else
        {
            audioMixer.GetFloat(parameterName, out float currentValue);

            float progress = Mathf.Clamp(Mathf.InverseLerp(startValue, targetValue, currentValue), 0.01f, 1f);

            while (progress > 0)
            {
                if(useUnscaledTime)
                    progress -= Time.unscaledDeltaTime / fadeInSpeed;
                else
                    progress -= Time.deltaTime / fadeInSpeed;

                if (progress < 0)
                {
                    progress = 0;
                }
                audioMixer.SetFloat(parameterName, Mathf.Lerp(startValue, targetValue, progress));
                yield return null;
            }
        }
        if(onFadeFinished != null)
            onFadeFinished();
    }
    #endregion
}