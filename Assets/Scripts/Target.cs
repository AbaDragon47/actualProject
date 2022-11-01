using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamagable
{
    //hello
    public float Health;
    public void Start()
    {
        //Debug.Log("works?");
        Health=100f;
    }
    
    public void TakeDamage(float damage)
    {
        Debug.Log("hi");
        Health -= damage;
        Debug.Log(Health);
        if (Health <= 0)
            Destroy(gameObject);
    }


}
