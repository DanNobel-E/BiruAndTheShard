using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ctrl : MonoBehaviour
{
    public Transform Player;
    public float Speed;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Vector3 forward;

    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementEnemy();
    }  
    void MovementEnemy()
    {
        forward = (transform.right * Speed).normalized;
        RaycastHit2D ray = Physics2D.Raycast(transform.position + forward * 0.5f, forward, .1f); // aggiunto * 0.5f perchè altrimenti partiva da troppo lontano
                                                                                                 // se parte da troppo vicino collide sempre con il player

        if (ray.collider != null)
        {
            if (ray.collider.CompareTag("ColumnTag") /*&& distFromCollider.x <= .5f*/)
            {
                Speed *= -1;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
        rigidBody.velocity = transform.right * Speed * Time.deltaTime;//lasciando il deltaTime devo aumentare molto la speed, ma togliendo il deltaTime non è piuà frame dependent
    }
}