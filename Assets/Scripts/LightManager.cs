using System.Collections;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [Header("Settings")]
    public float minIntensity = 15f;
    public float maxIntensity = 20f;
    public float minDelay = 0.05f;
    public float maxDelay = 0.5f;

    [Header("Flicker Enable")]
    public bool isFlickerEnabled;

    private Light lightSource;
    private Coroutine flickerCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightSource = GetComponent<Light>();

        if (isFlickerEnabled)
        {
            EnableFlicker();
        }
    }

    void Update()
    {
        if (isFlickerEnabled)
        {
            EnableFlicker();
        } 
        else
        {
            DisableFlicker();
        }
    }

    private IEnumerator FlickerRoutine()
    {
        while (true)
        {
            lightSource.intensity = Random.Range(minIntensity, maxIntensity);
            lightSource.enabled = !lightSource.enabled;
            
            float randomTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomTime);
        }
    }

    public void EnableFlicker()
    {
        isFlickerEnabled = true;

        flickerCoroutine ??= StartCoroutine(FlickerRoutine());
    }

    public void DisableFlicker()
    {
        isFlickerEnabled = false;

        if (flickerCoroutine != null) 
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
        }

        lightSource.enabled = true;
        lightSource.intensity = maxIntensity;
    }
}
