using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float movementSpeed;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1f);

        characterController.SimpleMove(input * movementSpeed);
    }
}
