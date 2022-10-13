using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement")]
    private float speed = 1f, rbDrag = 6f;
    
    //private float turnSpeed = 45;
    private float hInput;
    private float fInput;
    private float movementMult = 10f;
    // Update is called once per frame
    Vector3 moveDirection;
    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation=true;
    }
    void ControlDrag()
    {
        rb.drag = rbDrag;
    }
    void Update()
    {
        Movement();
        ControlDrag();
        
        //move forward
       
       // Debug.Log(fInput);
      //  transform.Translate(Vector3.up * Time.deltaTime * speed * fInput);
        //transform.Translate(Vector3.right * Time.deltaTime * speed * hInput );
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
        rb.AddForce(moveDirection.normalized*speed*movementMult, ForceMode.VelocityChange);
    }
    /* public void amp()
{
    while
}*/
}
