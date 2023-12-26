using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        // ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //resetting the default velocity
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //getting the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; //right - Red Axis, forward - blue Axis

        // move the player
        controller.Move((move * speed ) * Time.deltaTime);

        // check if the player can jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            // the actual jump
            velocity.y  = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }

        //falling down
        velocity.y +=gravity * Time.deltaTime;

        // execute the jump
        controller.Move(velocity * Time.deltaTime);

        if(lastPosition != gameObject.transform.position && isGrounded == true)
        {
            // use later
            isMoving = true;
        }
        else
        {
            // use later
            isMoving= false;
        }

        lastPosition = gameObject.transform.position;


    }
}
