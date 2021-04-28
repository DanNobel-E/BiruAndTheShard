using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightPulse : MonoBehaviour
{
    public float PulseDuration = 1;
    public Color DefaultColor { get; set; }
    public float DefaultDuration { get; set; }
    public float DefaultInnerRadius { get; set; }
    public float DefaultOuterRadius { get; set; }



    Light2D light;
    float pulseTimer = 0;
    float pulseIntensity;
    bool pulse;
    float startPulse;
    float endPulse;

    void Start()
    {
        light = GetComponent<Light2D>();
        pulseIntensity = light.intensity;
        DefaultColor = light.color;
        DefaultDuration = PulseDuration;
        DefaultInnerRadius = light.pointLightInnerRadius;
        DefaultOuterRadius = light.pointLightOuterRadius;

    }

    public void Pulse()
    {
        if (!pulse)
        {
            if (startPulse != pulseIntensity)
            {
                startPulse = pulseIntensity;
                endPulse = 0;
            }
        }
        else
        {
            if (startPulse != 0)
            {
                startPulse = 0;
                endPulse = pulseIntensity;
            }
        }

        pulseTimer += Time.deltaTime;
        float fraction = pulseTimer / PulseDuration;
        light.intensity = Mathf.Lerp(startPulse, endPulse, fraction);

        if (pulseTimer >= PulseDuration)
        {
            pulseTimer = 0;
            light.intensity = endPulse;
            pulse = !pulse;
        }



    }

    public void ChangeColor(Color color)
    {
        light.color = color;
    }

    public void ChangeRadius(float inner, float outer)
    {
        light.pointLightInnerRadius = inner;
        light.pointLightOuterRadius = outer;

    }

    // Update is called once per frame
    void Update()
    {
        Pulse();
    }
}
