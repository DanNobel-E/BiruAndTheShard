using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    Vector3 startPos;
    int levelId;


    void Start()
    {
        startPos = transform.position;
        levelId = transform.parent.GetSiblingIndex() + 1;

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
        transform.position = startPos;

        if(index!=0)
            gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            EventManager.OnLevelChange.Invoke(0);
        }
    }

}
