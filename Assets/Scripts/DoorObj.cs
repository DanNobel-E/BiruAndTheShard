using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObj : MonoBehaviour
{
    public Color ActivationColor;
    public int NextLevel { get; set; }
    Collider2D trigger;
    public int LevelId { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<Collider2D>();
        trigger.enabled = false;
    

    }

    private void OnEnable()
    {
        EventManager.OnLevelChange.AddListener(OnLevelChange);
        EventManager.OnDoorActivation.AddListener(OnDoorActivation);
    }

    private void OnDisable()
    {
        EventManager.OnLevelChange.RemoveListener(OnLevelChange);
        EventManager.OnDoorActivation.RemoveListener(OnDoorActivation);
    }


    public void OnDoorActivation()
    {
        trigger.enabled = true;
        LightPulse lp = GetComponentInChildren<LightPulse>();
        lp.ChangeColor(ActivationColor);
        lp.PulseDuration = 1;
        lp.ChangeRadius(2,7);

    }

    public void OnLevelChange(int index)
    {
        trigger.enabled = false;
        LightPulse lp = GetComponentInChildren<LightPulse>();
        lp.ChangeColor(lp.DefaultColor);
        lp.PulseDuration = lp.DefaultDuration;
        lp.ChangeRadius(lp.DefaultInnerRadius, lp.DefaultOuterRadius);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventManager.OnLevelChange.Invoke(NextLevel);

        }
    }


}
