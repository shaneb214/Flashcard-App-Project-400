using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    [SerializeField] private float movementSpeed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1f);
        //Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1f);

        characterController.SimpleMove(input * movementSpeed);
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
