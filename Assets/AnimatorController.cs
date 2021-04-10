using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
   
    [Header("Components")]
    Rigidbody2D rb;
    Animator animator;
    Biru_Movements bm;
    

    //Animator Parameters
    bool runAnim => Mathf.Abs(rb.velocity.x) > 0.1 && rb.velocity.y == 0.0f;
    bool fallAnim => rb.velocity.y <= 0.1f;
    bool JumpAnim => rb.velocity.y >= 0.1f;
    bool onGround => bm.CheckIfGrounded();
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bm = GetComponent<Biru_Movements>();
       
    }

   
    void Update()
    {
        animator.SetBool("isRunning", runAnim);
        animator.SetBool("isJumping", JumpAnim);
        animator.SetBool("isFalling", fallAnim);
        animator.SetBool("onGround", onGround);

    }


    


}
