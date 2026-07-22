using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [Header("Settings")]
    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 1.0f;

    private CanvasGroup canvasGroup;
    private Slider slider;
    private Coroutine activeFadeRoutine;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetMaxStat(float maxStat)
    {
        slider.maxValue = maxStat;
        slider.value = maxStat;
    }

    public void UpdateStatValue(float currentStat)
    {
        slider.value = currentStat;
    }

    public void FadeOut()
    {
        if (activeFadeRoutine != null) StopCoroutine(activeFadeRoutine);
        activeFadeRoutine = StartCoroutine(FadeRoutine(1f, 0f, fadeOutDuration));
    }

    public void FadeIn()
    {
        if (activeFadeRoutine != null) StopCoroutine(activeFadeRoutine);
        activeFadeRoutine = StartCoroutine(FadeRoutine(0f, 1f, fadeInDuration));
    }

    private IEnumerator FadeRoutine(float startAlpha, float targetAlpha, float duration)
    {
        if (duration <= 0)
        {
            canvasGroup.alpha = targetAlpha;
            yield break; 
        }
        
        float time = 0;
        canvasGroup.alpha = startAlpha;

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
