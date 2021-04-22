using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy_Ctrl : MonoBehaviour
{
    public float Speed;
    public float TimerForDisactiveEnemy;         // float publico per decidere dopo quanti secondi disattivare il gameObject
    public float TimeHitAnimations;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Vector3 forward;
    float startGravityScale;
    bool walk = false;
    bool collisionFromSpikes = false;    //attivo se collide con Spikes_Trap
    float counter = 0;
    bool collisionHit = false;
    float counterForAnimHit = 0;

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

        if (walk)
            rigidBody.velocity = transform.right * Speed * Time.deltaTime;
    }
    void Update()
    {
        RaycastEnemy();

        if (collisionFromSpikes)
        {
            counter += TimerForDisactiveEnemy * Time.deltaTime;
            if (counter >= TimerForDisactiveEnemy)
                transform.gameObject.SetActive(false);

            //counter = 0;
        }

        // set bool from collision player
        if (collisionHit)
        {
            anim.SetBool("Hit", true);

            counterForAnimHit += TimeHitAnimations * Time.deltaTime;
            if (counterForAnimHit >= TimeHitAnimations)
            {
                anim.SetBool("Hit", false);
                counterForAnimHit = 0;
                collisionHit = false;
            }
        }

        Debug.Log(counterForAnimHit);
    }
    void RaycastEnemy()
    {
        forward = (transform.right * Speed).normalized;
        Vector2 origin = new Vector2(transform.position.x + forward.x * .8f, transform.position.y * 2);
        //RaycastHit2D ray = Physics2D.Raycast(origin, forward, .1f);
        RaycastHit2D ray = Physics2D.Raycast(transform.position, forward, 1.0f);
        Debug.DrawRay(transform.position, forward);

        if (ray.collider != null)
        {
            Debug.Log("ho colliso con : " + ray.collider.transform.tag + "   Collider name = " + ray.collider.transform.name);

            //if (ray.collider.CompareTag("Erasable") || ray.collider.CompareTag("BorderTile") || ray.collider.CompareTag("BorderTile")/*&& distFromCollider.x <= .5f*/)
            //{
            //    Speed *= -1;
            //    spriteRenderer.flipX = !spriteRenderer.flipX;
            //}
            if (ray.collider.CompareTag("Erasable") || ray.collider.name == "tm_Grass" || ray.collider.CompareTag("BorderTile") || ray.collider.name == "tm_Stone"/*&& distFromCollider.x <= .5f*/)
            {
                Speed *= -1;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Spikes_Trap") /*|| col.CompareTag("Suspended_Trap")*/)
        {
            anim.SetBool("IsDeath", true);
            collisionFromSpikes = true;                  // booleano che attiva il counter per disattivare l'Enemy dopo l'animazione di death
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
        {
            collisionHit = true;
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