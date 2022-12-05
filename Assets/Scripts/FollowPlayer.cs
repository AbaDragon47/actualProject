using System.Collections;
using System.Collections.Generic;
//using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // public GameObject player;
    //private Vector3 offset = new Vector3(-2, 2, -3);
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;


    [SerializeField]Camera cam;

    float mouseX;
    float mouseY;

    float multi = .01f;
    float xRot, yRot;

    private void Start()
    {

        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // myinput gets mouse position and ig basis visiion of player based on mouse
        MyInput();
        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }
    void MyInput()
    {
      

        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * sensX * multi;
        xRot -= mouseY * sensY * multi;

        xRot = Mathf.Clamp(xRot, -90f, 90f);
    }


    /* void LateUpdate()
     {
         transform.position = player.transform.position + offset;
        // transform.rotate=(player.transform.Rotate*offset);
     }*/


}
