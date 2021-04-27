using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivableObj : MonoBehaviour
{
    Animator anim;
    string activationParam = "Activate";
    bool gemInside;
    bool press;

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

    public void IsGemInside(bool b){

        gemInside=b;
    }

    void Update(){

        if (gemInside)
        {

            if (Input.GetMouseButtonDown(0))
            {
                press = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                press = false;
            }
        }
    }
    public void OnLevelChange(int index)
    {
        anim.SetBool(activationParam, false);
        press = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
      
        if (collision.CompareTag("Gem") && press)
        {
            bool status = anim.GetBool(activationParam);
            SetActive(!status);

            EventManager.OnDoorActivation.Invoke();

        }
    }

}
