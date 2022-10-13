using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // public GameObject player;
    //private Vector3 offset = new Vector3(-2, 2, -3);
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;


    Camera cam;

    float mouseX;
    float mouseY;

    float multi = .01f;
    float xRot, yRot;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        
    }
    void MyInput()
    {

        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        xRot -= mouseX * sensX * multi;
        yRot += mouseY * sensY * multi;
    }


    /* void LateUpdate()
     {
         transform.position = player.transform.position + offset;
        // transform.rotate=(player.transform.Rotate*offset);
     }*/


}
