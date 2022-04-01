using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGamePlayer : MonoBehaviour
{
    public Action PlayerGettingUpAfterFallingEvent;

    [Header("Components")]
    private Rigidbody rb;
    private Animator animator;

    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    private FixedJoystick joystick;
    private Vector3 movementInput;

    public enum CharacterState { Idle,Running,Rolling, Fallover}
    public CharacterState currentCharacterState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        joystick = FindObjectOfType<FixedJoystick>();
    }

    private void Start()
    {
        //EnterIdleState();
    }

    private void Update()
    {
        switch (currentCharacterState)
        {
            case CharacterState.Running:

                movementInput = new Vector3(joystick.Horizontal, 0, 1f);
                rb.velocity = movementInput * movementSpeed;
                break;

            case CharacterState.Rolling:

                movementInput = new Vector3(0, 0, 1f);
                rb.velocity = movementInput * movementSpeed;
                break;

            default:
                rb.velocity = Vector3.zero;
                break;
        }

    }

    //Collision Detection.
    private void OnCollisionEnter(Collision collision)
    {
        //Hit solid wall. Stumble.
         if(collision.gameObject.CompareTag("RealWall"))
        {
            EnterFalloverState();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FakeWall"))
        {
            EnterRollingState();
        }
    }
    //Entering States.
    private void EnterFalloverState()
    {
        currentCharacterState = CharacterState.Fallover;
        animator.SetBool("Fall", true);
        animator.SetBool("Running", false);
    }
    private void EnterRollingState()
    {
        currentCharacterState = CharacterState.Rolling;
        animator.SetBool("Roll", true);
        animator.SetBool("Running", false);
    }
    private void EnterIdleState()
    {
        currentCharacterState = CharacterState.Idle;
        animator.SetBool("Idle", true);
        animator.SetBool("Running", false);
    }

    //Animation Events.
    public void OnRollAnimationFinished()
    {
        currentCharacterState = CharacterState.Running;
        animator.SetBool("Roll", false);
        animator.SetBool("Running", true);
    }
    public void OnFallAnimationFinished()
    {
        animator.SetBool("Fall", false);
        animator.SetBool("StandUp", true);

        PlayerGettingUpAfterFallingEvent?.Invoke();
    }
    public void OnStandUpAnimationFinished()
    {
        currentCharacterState = CharacterState.Running;
        animator.SetBool("StandUp", false);
        animator.SetBool("Running", true);
    }
}