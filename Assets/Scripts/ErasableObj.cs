using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErasableObj : MonoBehaviour
{
    public void Erase()
    {
        gameObject.SetActive(false);
    }
}
