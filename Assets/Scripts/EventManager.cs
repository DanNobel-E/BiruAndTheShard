using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class EventManager : MonoBehaviour
{
    public static UnityEvent<int> OnLevelChange;
    public static UnityEvent OnDoorActivation;
    public static UnityEvent OnButtonPressed;
    public static UnityEvent OnBiruDie;
    

    void Awake()
    {
        OnLevelChange = new UnityEvent<int>();
        OnDoorActivation = new UnityEvent();
        OnButtonPressed = new UnityEvent();
        OnBiruDie = new UnityEvent();
       

    }

    
}
