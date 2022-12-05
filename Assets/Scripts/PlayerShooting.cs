using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // so Im thinking that Action creates and event? Maybe similar to how scratch had events you send around;
    public static Action shootInput;
    public static Action reloadInput;
    //public reloading whatAmIAt;
    // will invoke an event/action when mouse is pressed
    // Update is called once per frame

    [SerializeField] private KeyCode reloadKey;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
            //Debug.Log("should start shooting");
            // ? is a null operation and will avoid a null exception
            
        }
        if (Input.GetKeyDown(reloadKey))
        {
            reloadInput?.Invoke();
        }
        
    }
}
