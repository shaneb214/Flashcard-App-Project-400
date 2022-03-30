using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    private Animator animator;

    [SerializeField] private float movementSpeed;


    public enum CharacterState { Moving, Fallover}
    public CharacterState currentCharacterState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    private void Update()
    {
        if(currentCharacterState == CharacterState.Moving)
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1f);
            //Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1f);

            rb.velocity = input * movementSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Hit solid wall. Stumble.
        //Hit fake wall. Roll through.
        //if(collision.contacts[0].normal.y <= 0.7f)
        //{
        //    print("fsafsaf");
        //    animator.SetBool("Roll", true);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FakeWall"))
        {
            animator.SetBool("Roll", true);
            animator.SetBool("Running", false);
        }
    }

    public void OnRollAnimationFinished()
    {
        animator.SetBool("Roll", false);
        animator.SetBool("Running", true);
    }
}
