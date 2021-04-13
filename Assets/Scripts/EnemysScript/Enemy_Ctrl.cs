using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ctrl : MonoBehaviour
{
    public float Speed;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Vector3 forward;

    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Animations
        if (Speed != 0)
            anim.SetBool("IsWalking", true);
        else
            anim.SetBool("IsWalking", false);

        RaycastEnemy();
        rigidBody.velocity = transform.right * Speed * Time.deltaTime;//lasciando il deltaTime devo aumentare molto la speed, ma togliendo il deltaTime non è piuà frame dependent
    }
    void RaycastEnemy()
    {
        forward = (transform.right * Speed).normalized;
        RaycastHit2D ray = Physics2D.Raycast(transform.position + forward * .6f, forward, .1f);
        Debug.DrawRay(transform.position + forward * .5f, forward);

        if (ray.collider != null)
        {
            Debug.Log("ho colliso con : " + ray.collider.transform.tag + "   Collider name = " + ray.collider.transform.name);

            if (ray.collider.CompareTag("Erasable") || ray.collider.CompareTag("Border") /*&& distFromCollider.x <= .5f*/)
            {
                Speed *= -1;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }
}