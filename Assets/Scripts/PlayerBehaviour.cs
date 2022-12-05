using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using Unity.Netcode;
using UnityEngine;

public class PlayerBehaviour : NetworkBehaviour
{
    public playerHealth healthBar;
    public int maxHealth = 100;
    public int currrentHealth;

    
    float playerHeight = 2f;

    [Header("Movement")]
    private float speed = 1f;
    [SerializeField] float movementMult=10f;
    [SerializeField] float airMult = 0.4f;
    //rbDrag = 6f;
    

    [Header("jumping")]
    public float jumpForce=5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 2f;

    //private NetworkVariable<float> fBpos = new NetworkVariable<float>();
    //private NetworkVariable<float> lRpos = new NetworkVariable<float>();


    //private float turnSpeed = 45;
    private float hInput;
    private float fInput;
    //movementMult = 5f;
    // Update is called once per frame
    bool isGrounded;

    Vector3 moveDirection;
    Vector3 slopeMoveDirect;
    Rigidbody rb;

    RaycastHit slopeHit;
    
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down, out slopeHit, playerHeight/2 + 1f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        currrentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

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
    private void Update()
    {
        UpdateClient();
        UpdateServer();
    }
    void UpdateClient()
    {
        if (!IsOwner)
            return;
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
        //debug.log(isGrounded);

        Movement();
        ControlDrag();

        if (Input.GetKeyDown(jumpKey) && isGrounded)    
        {
            Jump();
        }
        else
        {
            rb.AddForce(Vector3.down*10f,ForceMode.Acceleration);
            //Debug.Log("going down");
        }
        slopeMoveDirect = Vector3.ProjectOnPlane(moveDirection,slopeHit.normal);
        
        
        //move forward
       
       // Debug.Log(fInput);
      //  transform.Translate(Vector3.up * Time.deltaTime * speed * fInput);
        //transform.Translate(Vector3.right * Time.deltaTime * speed * hInput );
    }
    void UpdateServer()
    {
        transform.position = new Vector3(transform.position.x+hInput, transform.position.y, transform.position.z+fInput);
    }

    private void Jump()
    {
        Debug.Log("can jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
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
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized*speed*movementMult, ForceMode.VelocityChange);
        }
        else if(isGrounded&& OnSlope())
        {
            rb.AddForce(slopeMoveDirect.normalized * speed * movementMult, ForceMode.VelocityChange);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * speed * movementMult*airMult, ForceMode.VelocityChange);
        }
        
    }
    /*public void TakeDamage(int damage)
    {
        currrentHealth -= damage;
        healthBar.setHealth(currrentHealth);
    }
     public void amp()
{
    I am currently at 4:40
}*/
}
