using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObj : MonoBehaviour
{
    Collider2D trigger;
    public int NextLevel { get; set; }

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

    }

    public void OnLevelChange(int index)
    {
        trigger.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventManager.OnLevelChange.Invoke(NextLevel);

        }
    }


}
