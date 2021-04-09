using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class EventManager : MonoBehaviour
{
    public static UnityEvent OnLevelChange;

    void Awake()
    {
        OnLevelChange = new UnityEvent();
    }

    
}
