using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonState { Idle, Pressed}
public class ButtonLogic : MonoBehaviour
{
    public int LevelId { get; set; }
    bool pressed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        pressed = true;
        EventManager.OnButtonPressed.Invoke(LevelId, pressed);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        pressed = false;
        EventManager.OnButtonPressed.Invoke(LevelId, pressed);
    }

}
