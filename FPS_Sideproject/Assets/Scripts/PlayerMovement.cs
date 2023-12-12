using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    private CharacterController playerController;
    private PlayerInput input;

    private Vector3 velocity;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded == true && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        Vector3 moveDir = transform.forward * input.moveInput.y + transform.right * input.moveInput.x;   
        playerController.Move(moveDir * Speed * Time.deltaTime);

        if(input.isJumping == true && isGrounded == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity*Time.deltaTime);
    }
}
