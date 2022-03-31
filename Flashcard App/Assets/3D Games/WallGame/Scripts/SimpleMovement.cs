using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    private Animator animator;

    [SerializeField] private float movementSpeed;

    Vector3 movementInput;
    public enum CharacterState { Running,Rolling, Fallover}
    public CharacterState currentCharacterState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (currentCharacterState)
        {
            case CharacterState.Running:

                movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1f);
                rb.velocity = movementInput * movementSpeed;
                break;

            case CharacterState.Rolling:

                movementInput = new Vector3(0, 0, 1f);
                rb.velocity = movementInput * movementSpeed;
                break;

            case CharacterState.Fallover:

                break;
            default:
                break;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Hit solid wall. Stumble.
         if(collision.gameObject.CompareTag("RealWall"))
         {
            currentCharacterState = CharacterState.Fallover;
            animator.SetBool("Fall", true);
            animator.SetBool("Running", false);
         }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FakeWall"))
        {
            currentCharacterState = CharacterState.Rolling;
            animator.SetBool("Roll", true);
            animator.SetBool("Running", false);
        }
    }

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
    }
    public void OnStandUpAnimationFinished()
    {
        currentCharacterState = CharacterState.Running;
        animator.SetBool("StandUp", false);
        animator.SetBool("Running", true);
    }
}
