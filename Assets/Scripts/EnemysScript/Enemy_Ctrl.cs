using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ctrl : MonoBehaviour
{
    public Transform Player;
    public Vector2 WalkPos;
    public float LerpTime;      // tempo usato per andare da una parte all altra
    public float Speed;
    [Space]         // aggiunge uno spazio tra i nomi public nell'inspector
    public float AttDist, IdolDist;

    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Vector3 distToPlayer;
    Vector3 velocity;
    Vector2 startLerpPos, endLerpPos;       // startLerpPos e endLerpPos riferimento per inizio e fine del lerp dell enemy
    float fraction = 0;
    float originalSpeed;
    float distLenght;
    bool attack;
    float counter = 0;
    int timeToGo = 3;       // timer dopo quanto tempo i nemici cominciano a muoversi (problema di delay nel caricamento)

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        fraction = 0;
        attack = false;
    }

    // Update is called once per frame
    void Update()
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
        counter += Time.deltaTime;
        if (counter >= timeToGo)       // usato counter 
        {
            fraction += Time.deltaTime;

            if (fraction > LerpTime)        // quando la fraction raggiunge il tempo publico LerpTime inverte la speed
            {
                fraction = 0;
                Speed = -Speed;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
            rigidBody.velocity = transform.right * Speed * Time.deltaTime;//lasciando il deltaTime devo aumentare molto la speed, ma togliendo il deltaTime non è piuà frame dependent

            //Debug.Log(fraction);
        }
    }
}