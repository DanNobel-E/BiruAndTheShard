using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class Gem : MonoBehaviour, IPointerClickHandler
{
    public Vector3 Offset=Vector3.zero;
    public float LerpFactor = 1;
    bool draggable;
    bool movable;
    Vector3 startPos;
    Vector3 borderPos;

    Transform screenHandlerLD, screenHandlerUR;

    private void Start()
    {
        startPos = transform.position;

        screenHandlerLD = Camera.main.transform.GetChild(0);
        screenHandlerUR = Camera.main.transform.GetChild(1);


    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!draggable)
        {
            draggable = true;
            movable = true;
        }
        else
        {

        }
    }

    private void Update()
    {
        if (draggable && movable)
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 truePos = new Vector3(pos.x, pos.y, 0);
            transform.position = Vector3.Lerp(transform.position, truePos, Time.deltaTime*LerpFactor) + Offset;

        }else if (!movable)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.x < screenHandlerUR.position.x && mousePos.x > screenHandlerLD.position.x
                && mousePos.y<screenHandlerUR.position.y && mousePos.y>screenHandlerLD.position.y)
                movable = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Erasable"))
        {
            collision.gameObject.GetComponent<ErasableObj>().Erase();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            borderPos = transform.position;
            movable = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            transform.position = borderPos;
        }
    }
}
