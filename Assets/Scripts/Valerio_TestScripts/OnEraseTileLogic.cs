using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEraseTileLogic : MonoBehaviour
{
    public ParticleSystem ps;
    public Vector3 position;
    public bool explode;
    private void Start()
    {
        EventManager.OnEraseTile.AddListener(EraseExplosion);
    }
    

    private void Update()
    {
        if (explode)
        {
            explode = false;
            EraseExplosion(position);
            Debug.Log("explode!");
        }
    }

    private void EraseExplosion(Vector3 pos)
    {
        
        var newps = ps;
        newps.transform.position = transform.position + pos;
        newps.Play();
        
    }
}
