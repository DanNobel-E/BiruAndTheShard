using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr : MonoBehaviour
{
    Vector3 startPos;
    public int LevelId { get; set; }
    BiruAnimatorController anim;


    void Start()
    {
        startPos = transform.position;
        anim = GetComponent<BiruAnimatorController>();
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

   

  

}
