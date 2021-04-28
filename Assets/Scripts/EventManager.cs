using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class EventManager : MonoBehaviour
{
    public static UnityEvent<int> OnLevelChange;
    public static UnityEvent OnDoorActivation;
    public static UnityEvent<int,bool> OnButtonPressed;
    public static UnityEvent OnBiruDie;
    

    void Awake()
    {
        OnLevelChange = new UnityEvent<int>();
        OnDoorActivation = new UnityEvent();
        OnButtonPressed = new UnityEvent<int, bool>();
        OnBiruDie = new UnityEvent();
       

    }

    public static void RestartLevel()
    {
        OnLevelChange.Invoke(0);

    }


}
