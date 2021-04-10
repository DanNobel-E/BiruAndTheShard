using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum biruAnimState { Biru_Idle, Biru_Run, Biru_Jump, Biru_Falling }

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class BiruAnimatorController : MonoBehaviour
{
    [Header("Animator State-Debugging")]
    public biruAnimState currentState;
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
        AnimationManager();

    }

    void UpdateAnimationState(biruAnimState newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;

        animator.Play(newState.ToString());

        currentState = newState;
    }

    void AnimationManager()
    {
        if(onGround)
        {
            if (runAnim) UpdateAnimationState(biruAnimState.Biru_Run);
            else UpdateAnimationState(biruAnimState.Biru_Idle);
        }
        else
        {
            if (JumpAnim) UpdateAnimationState(biruAnimState.Biru_Jump);
            else if (fallAnim) UpdateAnimationState(biruAnimState.Biru_Falling);
        }

       
    }


    


}
