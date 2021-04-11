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
    //Vector3 velocity;
    float fraction = 0;
    //float originalSpeed;
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
    void FixedUpdate()
    {
        CheckDistanceToPlayer();
        MovementEnemy();
        //rigidBody.velocity = transform.right * Speed * Time.deltaTime;
    }
    void CheckDistanceToPlayer()            // metodo per gestire
    {
        distToPlayer = Player.transform.position - transform.position;
        distLenght = distToPlayer.magnitude;
    }
    void MovementEnemy()
    {
        //counter += Time.deltaTime;
        //if (counter >= timeToGo)       // usato counter 
        //{
        // USATO INZIALMENTE COME LERP-PINGPONG, MA DA CAMBIARE USANDO RAYCAST
        //fraction += Time.deltaTime;
        //if (fraction > LerpTime)        // quando la fraction raggiunge il tempo publico LerpTime inverte la speed
        //{
        //    fraction = 0;
        //    Speed = -Speed;
        //    spriteRenderer.flipX = !spriteRenderer.flipX;
        //}

        //Vector2 dirRay = new Vector2(.5f, 0);
        ////RaycastHit2D ray = Physics2D.Raycast(dirRay, transform.position);
        //RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.TransformDirection(transform.position.x, 0, 0));
        RaycastHit2D ray = Physics2D.Raycast(transform.TransformDirection(transform.position.x, 0, 0), transform.position); //con questo funziona ma ha problemi
        ////Debug.DrawRay(transform.position, transform.TransformDirection(transform.position.x, 0, 0), Color.red);
        Debug.DrawRay(transform.TransformDirection(transform.position.x, 0, 0), transform.position, Color.red);
        ////Debug.DrawRay(transform.position, transform.right, Color.red);


        if (ray.collider != null)
        {
            //Vector2 distFromCollider = ray.transform.position - transform.position;
            float distFromCollider = ray.point.x - transform.position.x;
            Debug.Log(ray.collider.gameObject.tag);
            if (ray.collider.CompareTag("ColumnTag") /*&& distFromCollider.x <= .5f*/)
            {
                if (distFromCollider <= 1)
                {
                    Speed *= -1;
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                    Debug.Log("asjdnajdfahjf");
                }

            }

        }
        rigidBody.velocity = transform.right * Speed * Time.deltaTime;//lasciando il deltaTime devo aumentare molto la speed, ma togliendo il deltaTime non è piuà frame dependent
                                                                      // Debug.Log("ho colliso con: " + ray.collider.tag);
                                                                      //}
    }
    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.gameObject.tag == "ColumnTag")
    //    {
    //        Debug.Log("ciao");
    //        Speed *= -1;
    //        spriteRenderer.flipX = !spriteRenderer.flipX;
    //    }
    //}
}