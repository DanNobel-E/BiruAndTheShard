using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy_Ctrl : MonoBehaviour
{
    public float Speed;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Vector3 forward;
    float startGravityScale;
    bool walk = false;

    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        anim = transform.GetComponent<Animator>();
        startGravityScale = rigidBody.gravityScale;
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
        if (walk)
        {
            rigidBody.velocity = transform.right * Speed * Time.deltaTime;//lasciando il deltaTime devo aumentare molto la speed, ma togliendo il deltaTime non è piuà frame dependent
        }
    }
    void RaycastEnemy()
    {
        forward = (transform.right * Speed).normalized;
        Vector2 origin = new Vector2(transform.position.x + forward.x * .8f, transform.position.y * 2);
        RaycastHit2D ray = Physics2D.Raycast(origin, forward, .1f);
        Debug.DrawRay(origin, forward);

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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Spikes_Trap"))
        {
            anim.SetBool("IsDeath", true);
            //gameObject.SetActive(false);
        }
    }
    void EnableFrameWalk()
    {
        walk = true;
        rigidBody.gravityScale = 0;
    }
    void DisableFrameWalk()
    {
        walk = false;
        rigidBody.gravityScale = startGravityScale;
    }
}