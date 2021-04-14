using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class Gem : MonoBehaviour, IPointerClickHandler
{
    public Vector3 Offset = Vector3.zero;
    public float LerpFactor = 1;
    bool draggable;
    bool movable;
    bool doorActive;
    Vector3 startPos;
    Vector3 borderPos;

    Transform screenHandlerLD, screenHandlerUR;

    private void Start()
    {
        startPos = transform.position;

        screenHandlerLD = Camera.main.transform.GetChild(0);
        screenHandlerUR = Camera.main.transform.GetChild(1);


    }

    private void OnEnable()
    {
        EventManager.OnDoorActivation.AddListener(OnDoorActivation);
        EventManager.OnLevelChange.AddListener(OnLevelChange);

    }

    private void OnDisable()
    {
        EventManager.OnDoorActivation.RemoveListener(OnDoorActivation);

    }

    public void OnLevelChange(int index)
    {
        //EventManager.OnLevelChange.RemoveListener(OnLevelChange);
        gameObject.SetActive(true);
        draggable = false;
        doorActive = false;
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
            if (!doorActive)
            {
                draggable = false;
                transform.position = startPos;

            }

        }
    }

    private void Update()
    {
        if (draggable && movable)
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 truePos = new Vector3(pos.x, pos.y, 0);
            transform.position = Vector3.Lerp(transform.position, truePos, Time.deltaTime * LerpFactor) + Offset;

        }
        else if (!movable)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.x < screenHandlerUR.position.x && mousePos.x > screenHandlerLD.position.x
                && mousePos.y < screenHandlerUR.position.y && mousePos.y > screenHandlerLD.position.y)
                movable = true;
        }
    }

    public void OnDoorActivation()
    {
        transform.position = startPos;
        gameObject.SetActive(false);

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
        } else if (collision.gameObject.CompareTag("Slot"))
        {
            doorActive = true;
            collision.GetComponent<ActivableObj>().IsGemInside(doorActive);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            transform.position = borderPos;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slot"))
        {
            doorActive = false;
            collision.GetComponent<ActivableObj>().IsGemInside(doorActive);


        }

    }

}
