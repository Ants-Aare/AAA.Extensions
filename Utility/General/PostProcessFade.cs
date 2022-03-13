using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class PostProcessFade : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float fadeInSpeed = 0.5f;
    [SerializeField] private float fadeOutSpeed = 0.5f;
    [SerializeField] private bool fadeInOnStart = true;
    [SerializeField] private bool useUnscaledTime = false;

    [Header("References")]
    [SerializeField] private Volume volume = null;

    void Awake()
    {
        if (volume == null)
            volume = GetComponent<Volume>();
        if (volume == null)
        {
            Destroy(this);
            Debug.LogError("There is no Post Processing Volume on this GameObject: " + gameObject.name);
        }
    }
    void OnEnable()
    {
        if (fadeInOnStart)
        {
            volume.weight = 0f;
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
        StopAllCoroutines();
        StartCoroutine(FadeIn(onFadeFinished));
    }
    public void Exit(Action onFadeFinished)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(onFadeFinished));
    }

    public void FadeInOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn(()=>StartCoroutine(FadeOut())));
    }
    public void FadeOutIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(()=>StartCoroutine(FadeIn())));
    }

    public void FadeInOut(float multiplier)
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn(()=>StartCoroutine(FadeOut()), multiplier));
    }

    IEnumerator FadeIn(Action onFadeFinished = null, float multiplier = 1f)
    {
        // Debug.Log("Startfadein");
        while (volume.weight < multiplier)
        {
            if(useUnscaledTime)
                volume.weight += Time.unscaledDeltaTime / fadeInSpeed * multiplier;
            else
                volume.weight += Time.deltaTime / fadeInSpeed * multiplier;

            if (volume.weight > 1)
            {
                volume.weight = 1;
            }
            // Debug.Log("Set weight to " + volume.weight);
            yield return null;
        }
        if(onFadeFinished != null)
            onFadeFinished();
    }
    IEnumerator FadeOut(Action onFadeFinished = null)
    {
        // Debug.Log("Startfadeout");
        while (volume.weight > 0)
        {
            if(useUnscaledTime)
                volume.weight -= Time.unscaledDeltaTime / fadeOutSpeed;
            else
                volume.weight -= Time.deltaTime / fadeOutSpeed;

            if (volume.weight < 0)
            {
                volume.weight = 0;
            }
            // Debug.Log("Set weight to " + volume.weight);
            yield return null;
        }
        if(onFadeFinished != null)
            onFadeFinished();
    }
}