using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private float speed = 20;
    private float turnSpeed = 45;
    private float hInput;
    private float fInput;
    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        fInput = Input.GetAxis("Vertical");
        //move forward
        if (Input.GetAxis("Vertical")> 0)
            fInput += 1+1/10;
        else
            fInput = Input.GetAxis("Vertical");
       // Debug.Log(fInput);
        transform.Translate(Vector3.up * Time.deltaTime * speed * fInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * hInput );
    }
}
