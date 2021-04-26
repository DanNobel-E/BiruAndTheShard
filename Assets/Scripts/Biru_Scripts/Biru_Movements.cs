using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider))]
public class Biru_Movements : MonoBehaviour
{
    [Header("Components")]
    Rigidbody2D rb;
    SpriteRenderer sr;
    public ParticleSystem ps;

    [Header("DebugVariables")]
    public LayerMask GroundLayer;
    public float deathAnimSpeed;
    public bool alive = true;
    public bool oneTime = true;
    public bool facingRight = true;

    public bool onGround => CheckIfGrounded();
    public bool canJump => Input.GetKeyDown(KeyCode.UpArrow) && onGround;
    

    public float boxAngle;
    public Vector2 boxOffset;
    public Vector2 boxDimensions;



    [Header("MovementVariables")]
    public float moveSpeed = 5f;

    [Header("JumpVariables")]
    [Range(0, 10)]
    public float jumpForce = 5f;
    [Range(0,10)]
    public float fallGravityScale = 3f;
    [Range(0, 10)]
    public float ascendGravityScale = 3f;



    [Header("GUIVariables")]
    public Vector2 GUIDimensions=new Vector2(200,150);   //x=Rect.width, y=Rect.height for the GUILabel rect
    public Vector2 GUIPosition= new Vector2(20,20);
    public int GUIFontSize;
    public Color GUIColor;
   

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
      
    }

    private void OnCollisionEnter2D(Collision2D other) //idk if is better to handle collsion with trigger or with collider (non ne sono molto contento, della logica in generale)
    {
        if (other.gameObject.name == "Enemy")
        {
            Debug.Log("ho colpito un nemico");
            alive = false;
            
            rb.simulated = false;
        }
    
    }
    

    private void Update()
    {
        SpriteFlip();
        if (canJump) JumpCharacter();
        if (!alive && sr.enabled) DeathAnimMovement();
        

    }
    void FixedUpdate()
    {
        MoveCharacter(GetInput());
        ControlGravity();
    }

    private void ControlGravity()
    {
        if (rb.velocity.y > 0) rb.gravityScale = ascendGravityScale;
        else if (rb.velocity.y < 0) rb.gravityScale = fallGravityScale;
        else rb.gravityScale = 1;
    }

    private Vector2 GetInput() //refactor
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        return new Vector2(x, y);
    }

    private void MoveCharacter(Vector2 input)
    {
       rb.velocity = new Vector2(input.x * moveSpeed, rb.velocity.y);
       
       
    }

    private void JumpCharacter()
    {
        rb.velocity += Vector2.up * jumpForce;
        ps.Play();
    }

    public bool CheckIfGrounded()
    {
       return Physics2D.OverlapBox((Vector2)transform.position+boxOffset, boxDimensions, boxAngle, GroundLayer);
    }

    public void DeathAnimMovement() //Chiedere a seb perchè se lo faccio nell'animator non funziona...
    {
        transform.position += new Vector3(0,1,0)*Time.deltaTime * deathAnimSpeed;
    }
   

    #region DebugRegion
    private void OnGUI()
    {
        GUI.color = GUIColor;
        GUI.skin.label.fontSize = GUIFontSize;
        GUI.Label(new Rect(GUIPosition.x, GUIPosition.y, GUIDimensions.x, GUIDimensions.y), $"velocity: {rb.velocity}\nAlive: {alive}");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube((Vector2)transform.position+boxOffset, boxDimensions);
    }

    private void SpriteFlip()
    {
        if (GetInput().x>0.1f && !facingRight)
        {
            FlipCharacter();
        }
        else if (GetInput().x < -0.1f && facingRight)
        {
            FlipCharacter();
        }
       
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up,180f);
        ps.Play();
    }

  
    #endregion
}
