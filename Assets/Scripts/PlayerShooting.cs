using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static Action shootInput;
   
    // will invoke an event/action when mouse is pressed
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
            // ? is a null operation and will avoid a null exception
        }
        
    }
}
