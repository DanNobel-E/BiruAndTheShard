using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy_Ctrl : MonoBehaviour
{
    //Level Generator
    Vector3 startPos;
    public int LevelId { get; set; }

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
    bool collisionHit = false;
    float counter = 0;
    float counterForAnimHit = 0;
    float counterForActive = 0;
    float timeForActive; /*= Random.Range(0, 4);*/

    void Start()
    {
        startPos = transform.position;

        rigidBody = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        anim = transform.GetComponent<Animator>();
        startGravityScale = rigidBody.gravityScale;

        //Time for active enemy from start level
        //counterForActive = 0;
        timeForActive = Random.Range(0, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Animations
        if (Speed != 0)
            anim.SetBool("IsWalking", true);
        else
            anim.SetBool("IsWalking", false);


        if (walk )
        {
            //if(counterForActive >= timeForActive)
                rigidBody.velocity = transform.right * Speed * Time.deltaTime;
        }
    }
    void Update()
    {
        //counterForActive += Time.deltaTime;

        RaycastEnemy();

        if (collisionFromSpikes)
        {
            counter += TimerForDisactiveEnemy * Time.deltaTime;
            if (counter >= TimerForDisactiveEnemy)
                transform.gameObject.SetActive(false);

            counter = 0;
            collisionFromSpikes = false;
        }

        // set bool from collision player
        if (collisionHit)
        {
            AddForce();
            anim.SetBool("Hit", true);
            counterForAnimHit += TimeHitAnimations * Time.deltaTime;

            if (counterForAnimHit >= TimeHitAnimations)
            {
                anim.SetBool("Hit", false);
                counterForAnimHit = 0;
                collisionHit = false;
            }
        }
    }
    void RaycastEnemy()
    {
        forward = (transform.right * Speed).normalized;
        //Vector2 origin = new Vector2(transform.position.x + forward.x * .8f, transform.position.y * 2);
        //RaycastHit2D ray = Physics2D.Raycast(origin, forward, .1f);
        RaycastHit2D ray = Physics2D.Raycast(transform.position, forward, 1.0f);
        Debug.DrawRay(transform.position, forward);

        if (ray.collider != null)
        {
            Debug.Log("ho colliso con : " + ray.collider.transform.tag + "   Collider name = " + ray.collider.transform.name);

            if (ray.collider.CompareTag("Erasable")
                || ray.collider.CompareTag("BorderTile")
                || ray.collider.name == "tm_Grass"
                || ray.collider.name == "tm_Stone")
            {
                InvertSpeed();
            }
        }
    }
    void AddForce()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.AddForce(new Vector2(.5f, .5f) * (Speed * -1), ForceMode2D.Force);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Border") || col.CompareTag("Spikes_Trap"))
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
        if (col.collider.tag == "Enemy")
        {
            InvertSpeed();
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
    void InvertSpeed()
    {
        Speed *= -1;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void OnEnable()
    {
        EventManager.OnLevelChange.AddListener(OnLevelChange);
    }

    private void OnDisable()
    {
        EventManager.OnLevelChange.RemoveListener(OnLevelChange);

    }


    public void OnLevelChange(int index)
    {
        transform.position = startPos;

        if (index == 0)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

}