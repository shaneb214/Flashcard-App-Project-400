using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    //private CharacterController characterController;
    private Rigidbody rigidbody;
    private Animator animator;
    [SerializeField] private float movementSpeed;

    private void Awake()
    {
        //characterController = GetComponent<CharacterController>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Screen.orientation = ScreenOrientation.Landscape;
    }

    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1f);
        //Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1f);

        rigidbody.velocity = input * movementSpeed;

        //characterController.SimpleMove(input * movementSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("fsafsaf");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.normal.y <= 0.7f)
        {
            animator.SetBool("Fall", true);
            print(hit.gameObject.name);
        }
            
    }
}
