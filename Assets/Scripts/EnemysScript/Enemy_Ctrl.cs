using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ctrl : MonoBehaviour
{
    public Transform Player;
    public Vector2 WalkPos;
    public float LerpTime;      // tempo usato per andare da una parte all altra
    public float Speed;
    public Vector2 direction;
    [Space]         // aggiunge uno spazio tra i nomi public nell'inspector
    //public float AttDist, IdolDist;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Vector3 distToPlayer;
    Vector3 forward;
    //Vector3 velocity;
    float fraction = 0;
    //float originalSpeed;
    float distLenght;
    bool attack;
    float counter = 0;
    int timeToGo = 3;       // timer dopo quanto tempo i nemici cominciano a muoversi (problema di delay nel caricamento)

    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        fraction = 0;
        attack = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistanceToPlayer();
        MovementEnemy();
    }
    void CheckDistanceToPlayer()            // metodo per gestire
    {
        distToPlayer = Player.transform.position - transform.position;
        distLenght = distToPlayer.magnitude;
    }
    void MovementEnemy()
    {
        forward = (transform.right * Speed).normalized;
        RaycastHit2D ray = Physics2D.Raycast(transform.position + forward * 0.5f, forward, .1f); // aggiunto * 0.5f perchè altrimenti partiva da troppo lontano
                                                                                                 // se parte da troppo vicino collide sempre con il player

        if (ray.collider != null)
        {
            Debug.Log(ray.collider.gameObject.tag);
            if (ray.collider.CompareTag("ColumnTag") /*&& distFromCollider.x <= .5f*/)
            {
                Speed *= -1;
                spriteRenderer.flipX = !spriteRenderer.flipX;
                Debug.Log("asjdnajdfahjf");
            }
        }
        rigidBody.velocity = transform.right * Speed * Time.deltaTime;//lasciando il deltaTime devo aumentare molto la speed, ma togliendo il deltaTime non è piuà frame dependent
                                                                      // Debug.Log("ho colliso con: " + ray.collider.tag);
    }
}