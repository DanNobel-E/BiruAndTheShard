using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErasableObj : MonoBehaviour
{
    int levelId;

    private void Start()
    {
        levelId = transform.parent.GetSiblingIndex() + 1;

    }

    public void Erase()
    {
        gameObject.SetActive(false);
    }
}
