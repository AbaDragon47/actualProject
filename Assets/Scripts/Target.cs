using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamagable 
{
    private float Health=100f;
    public void Damage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Destroy(gameObject);
    }
    void Start()
    {

    }


}
