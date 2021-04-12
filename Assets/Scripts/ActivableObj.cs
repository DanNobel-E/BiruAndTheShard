using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivableObj : MonoBehaviour
{
    Animator anim;
    string activationParam = "Activate";

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetActive(bool b)
    {
        anim.SetBool(activationParam, b);
    }

    private void OnEnable()
    {
        EventManager.OnLevelChange.AddListener(OnLevelChange);

    }

    private void OnDisable()
    {
        EventManager.OnLevelChange.RemoveListener(OnLevelChange);

    }

    public void OnLevelChange(int index)
    {
        anim.SetBool(activationParam, false);

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
      
        if (collision.CompareTag("Gem") && Input.GetMouseButtonDown(0))
        {
            bool status = anim.GetBool(activationParam);
            SetActive(!status);

            EventManager.OnDoorActivation.Invoke();

        }
    }

}
