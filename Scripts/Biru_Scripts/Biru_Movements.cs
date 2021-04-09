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

    [Header("DebugVariables")]
    public LayerMask GroundLayer;
    public bool onGround => CheckIfGrounded();
    public bool canJump => Input.GetKeyDown(KeyCode.UpArrow) && onGround;

   

    public float RaycastLenght;
    public Vector2 Raycastoffset;

    [Header("MovementVariables")]
    public float moveSpeed = 5f;

    [Header("JumpVariables")]
    public float jumpVelocity = 5f;
    public float longJumpScale = 3f;
    public float lowJumpScale = 2f;


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

    private void Update()
    {
        SpriteFlip();
        if (canJump) JumpCharacter();
       
        
    }
    void FixedUpdate()
    {
        MoveCharacter(GetInput());
        ControlGravity();
    }

    private void ControlGravity()
    {
        if (rb.velocity.y < 0) rb.gravityScale = longJumpScale;
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow)) rb.gravityScale = lowJumpScale;
        else rb.gravityScale = 1;
    }

    private Vector2 GetInput()
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
        rb.velocity += Vector2.up * jumpVelocity;
    }

    private bool CheckIfGrounded()
    {
       return Physics2D.Raycast((Vector2)transform.position+Raycastoffset, Vector2.down, RaycastLenght, GroundLayer);
    }

    #region DebugRegion
    private void OnGUI()
    {
        GUI.color = GUIColor;
        GUI.skin.label.fontSize = GUIFontSize;
        GUI.Label(new Rect(GUIPosition.x, GUIPosition.y, GUIDimensions.x, GUIDimensions.y), $"rb.velocity = {rb.velocity}");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay((Vector2)transform.position+Raycastoffset, Vector2.down*RaycastLenght);
    }

    private void SpriteFlip()
    {
        if (rb.velocity.x > 0) sr.flipX = false;
        else if (rb.velocity.x < 0) sr.flipX = true;
    }

    #endregion
}
