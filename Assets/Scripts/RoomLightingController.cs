using UnityEngine;

public class RoomLightingController : MonoBehaviour
{
    public void SetAllLightsFlicker(bool enable)
    {
        for (int i = LightManager.AllLights.Count - 1; i >= 0; i--)
        {
            if (LightManager.AllLights[i] != null)
            {
                if (enable)
                {
                    LightManager.AllLights[i].EnableFlicker();
                }
                else
                {
                    LightManager.AllLights[i].DisableFlicker();
                }
            }
        }
    }

    public void SetRandomLightsFlicker(float chanceToFlicker = 0.5f)
    {
        foreach (var lightManager in LightManager.AllLights)
        {
            if (lightManager != null)
            {
                if (Random.value < chanceToFlicker)
                {
                    lightManager.EnableFlicker();
                }
                else
                {
                    lightManager.DisableFlicker();
                }
            }
        }
    }
}