using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static Action shootInput;
    private void Start()
    {
        PlayerShooting.shootInput += Shoot;
    }

    // Update is called once per frame
    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }
        
    }
}
