using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErasableObj : MonoBehaviour
{
    public void Erase()
    {
        gameObject.SetActive(false);
    }

   
}
