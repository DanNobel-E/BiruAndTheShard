using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileExplosion : MonoBehaviour
{
    ParticleSystem ps;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        EventManager.OnEraseTile.AddListener(ExplodeTile);
    }

    private void ExplodeTile(Vector3 pos)
    {
        ps.transform.position = pos;
        ps.Play();
    }
}
