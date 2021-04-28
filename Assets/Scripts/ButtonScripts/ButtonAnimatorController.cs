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
        if (other.CompareTag("Player") || other.CompareTag("Enemy")) 
            UpdateAnimationState(buttonAnimState.Button_Pressed);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            UpdateAnimationState(buttonAnimState.Button_Idle);
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
        animator.Play(buttonAnimState.Button_Idle.ToString());

        currentState = buttonAnimState.Button_Idle;
    }
    void UpdateAnimationState(buttonAnimState newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;

        animator.Play(newState.ToString());

        currentState = newState;
    }
}
