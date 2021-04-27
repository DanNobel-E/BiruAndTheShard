using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum biruAnimState { Biru_Idle, Biru_Run, Biru_Jump, Biru_Falling, Biru_Die }

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class BiruAnimatorController : MonoBehaviour
{
    [Header("Animator State-Debugging")]
    public biruAnimState currentState;
    public bool BiruDead;
   
    [Header("Components")]
    Rigidbody2D rb;
    Animator animator;
    Biru_Movements bm;  //refactor the name and if possible functionality


    //Animator Parameters
    bool runAnim => Mathf.Abs(rb.velocity.x) > 0.1 && bm.onGround;
    bool fallAnim => rb.velocity.y <= 0.1f;
    bool JumpAnim => rb.velocity.y >= 0.1f;

    [Header("Animations")]

    public AnimationClip DeathClip;
    float deathDuration;
    bool isDying;
    float deathTimer=0;
    

  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bm = GetComponent<Biru_Movements>();
        deathDuration = DeathClip.length;
    }


    void Update()
    {
        AnimationManager();

        if (isDying)
        {
            deathTimer += Time.deltaTime;

            if (deathTimer >= deathDuration)
            {
                EventManager.RestartLevel();
                deathTimer = 0;
                isDying = false;
                bm.alive = true;
                rb.simulated = true;
            }

        }
    }   
    

    
    void UpdateAnimationState(biruAnimState newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;

        animator.Play(newState.ToString());

        currentState = newState;

        if (newState == biruAnimState.Biru_Die && !isDying)
            isDying = true;
        

    }

    void AnimationManager()
    {
        if(bm.alive)
        {
            if (bm.onGround)
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
        else
        {
            UpdateAnimationState(biruAnimState.Biru_Die);
        }
       
    }



       
    





        
       
        
       









    





}
