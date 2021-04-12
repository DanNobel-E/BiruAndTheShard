using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class Gem : MonoBehaviour, IPointerClickHandler
{
    public Vector3 Offset=Vector3.zero;
    bool draggable;
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!draggable)
            draggable = true;
        else
        {

        }
    }

    private void Update()
    {
        if (draggable)
        {
            transform.position = Input.mousePosition + Offset;

        }
    }

}
