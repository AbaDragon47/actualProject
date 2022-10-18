using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    float playerHeight = 2f;

    [Header("Movement")]
    private float speed = 1f;
    float movementMult=10f;
    [SerializeField] float airMult = 0.4f;
    //rbDrag = 6f;
    

    [Header("jumping")]
    public float jumpForce=5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 2f;
    
    //private float turnSpeed = 45;
    private float hInput;
    private float fInput;
    //movementMult = 5f;
    // Update is called once per frame
    bool isGrounded;

    Vector3 moveDirection;
    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation=true;
    }
    void ControlDrag()
    {
        // drag prevents the sliding on the ground even when not moving
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
        //print(isGrounded);

        Movement();
        ControlDrag();

        if(Input.GetKeyDown(jumpKey)&& isGrounded)
        {
            Jump();
        }
        
        //move forward
       
       // Debug.Log(fInput);
      //  transform.Translate(Vector3.up * Time.deltaTime * speed * fInput);
        //transform.Translate(Vector3.right * Time.deltaTime * speed * hInput );
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void Movement()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        fInput = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * fInput + transform.right * hInput;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized*speed*movementMult, ForceMode.VelocityChange);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * speed * movementMult*airMult, ForceMode.VelocityChange);
        }
        
    }
    /* public void amp()
{
    I am currently at 4:40
}*/
}
