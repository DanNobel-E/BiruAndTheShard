using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonState { Idle, Pressed}
public class ButtonLogic : MonoBehaviour
{
    
    
    public int buttonIndex = 0; //Use that to differentiate between buttons

    private void Awake()
    {
        EventManager.OnButtonPressed.AddListener(ButtonEventDebug);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.OnButtonPressed.Invoke();
    }

    void ButtonEventDebug()
    {
        Debug.Log("i'm pressed");

       
    }

}
