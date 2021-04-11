using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erase_Prova : MonoBehaviour
{
    Camera cam;
    //Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), cam.transform.forward);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "ColumnTag")
                {
                    Debug.Log("ho colliso con: " + hit.collider.transform.name);
                    Destroy(hit.collider.gameObject);
                }
            }
        //}
    }
}