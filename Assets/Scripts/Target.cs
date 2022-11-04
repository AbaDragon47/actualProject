using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamagable
{
    //hello
    public int Health;
    public playerHealth bar;
    // public playerHealth x;
    public void Start()
    {
        //Debug.Log("works?");
        Health=100;
       bar.SetMaxHealth((int)Health);
    }
    
    public void TakeDamage(float damage)
    {
        Health -= (int)damage;
        bar.setHealth(Health);
        bool dead = Health < 0? true:false;
        Debug.Log(dead);
        if (Health < 0)
        {
            Debug.Log("health is less than 0");
            Destroy(gameObject);
        }
            
    }


}
