using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GunData gunData;
    void Shoot()
    {
        Debug.Log("shots fired");
        
    }

}
