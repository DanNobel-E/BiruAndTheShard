using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum buttonAnimState {Button_Idle,Button_Pressed }
[RequireComponent(typeof(Animator))]
public class ButtonAnimatorController : MonoBehaviour
{
    [Header("Animator State-Debugging")]
    public buttonAnimState currentState;

    [Header("Components")]
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Enemy") UpdateAnimationState(buttonAnimState.Button_Pressed);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        UpdateAnimationState(buttonAnimState.Button_Idle);
    }

    void UpdateAnimationState(buttonAnimState newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;

        animator.Play(newState.ToString());

        currentState = newState;
    }
}
