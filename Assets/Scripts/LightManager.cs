using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static List<LightManager> AllLights = new();

    [Header("Settings")]
    public float minIntensity = 5f;
    public float maxIntensity = 15f;
    public float minDelay = 0.05f;
    public float maxDelay = 0.5f;

    [Header("Flicker Enable")]
    public bool isFlickerEnabled;

    private Light lightSource;
    private Coroutine flickerCoroutine;

    void Awake()
    {
        lightSource = GetComponent<Light>();
    }

    void OnEnable()
    {
        if (!AllLights.Contains(this))
        {
            AllLights.Add(this);
        }

        if (isFlickerEnabled)
        {
            EnableFlicker();
        }
    }

    void OnDisable()
    {
        if (AllLights.Contains(this))
        {
            AllLights.Remove(this);
        }

        DisableFlicker();
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

    public void TurnOff()
    {
        
        isFlickerEnabled = false;

        lightSource.enabled = false;
    }

    public void TurnOn()
    {
        isFlickerEnabled = false;

        lightSource.enabled = true;
    }
}
