using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    float timeSinceLastShot;

    [Header("References")]
    [SerializeField] GunData gunData;
    private void Start()
    {
        //calls from playerShooting class? and put Shoot method in shootInput
        // I think the reason why it was beneficial to seperate the 2 methods was so the Gun class would have the different guns but the all call shoot
        PlayerShooting.shootInput += Shoot;
    }
    // the f after the numbers makes sure the number is saved as a float
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    void Shoot()
    {
        Debug.Log("shots fired");
        // pulls from the GunData class and grabbing variables
        if (gunData.currentAmmo > 0)
        {

        }
        
    }

}
