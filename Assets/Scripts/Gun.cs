using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IDamagable
{
    // Start is called before the first frame update
    float timeSinceLastShot;
    //public Target x;

    public reloading whatAmIAt;

    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;
    private void Start()
    {
        //calls from playerShooting class? and put Shoot method in shootInput
        // I think the reason why it was beneficial to seperate the 2 methods was so the Gun class would have the different guns but the all call shoot
        PlayerShooting.shootInput += Shoot;
        PlayerShooting.reloadInput += StartReload;
        
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    // the f after the numbers makes sure the number is saved as a float
    // the variable is made automatically true as long as argument after is true;
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);



    
    void Shoot()
    {

       // Debug.Log("shots hiiii");
        // pulls from the GunData class and grabbing variables
        if (gunData.currentAmmo > 0)
        {
           // Debug.Log(CanShoot());
            if (CanShoot())
            {
                
                //Debug.Log("can shoot works");
                // whenever we use a variable in a method, it passes by value: makes a copy of the variable
                //out makes it pass by reference: changes the acutal value in memory => not a copy
                //Raycast send out a ray that will turn true if the ray intersects with any collider
                //Debug.Log(Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDist));
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDist)&&hitInfo.transform.tag=="Target")
                {
                    Debug.Log(hitInfo.transform.name+" shots fired");
                    IDamagable damagable = hitInfo.collider.gameObject.GetComponentInParent<IDamagable>();
                    Debug.Log("Damagable= "+damagable);
                    Debug.Log("idk why this didnt work: "+hitInfo.collider.gameObject.GetComponent<IDamagable>());
                    damagable?.TakeDamage(gunData.damage);
                    gunData.currentAmmo--;
                    whatAmIAt.currentAmmo();
                }

                gunData.currentAmmo--;
                whatAmIAt.currentAmmo();
                timeSinceLastShot = 0;
                OnGunShot();
            }
            
        }
        
    }
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        //Debug.Log("doing update");
        Debug.DrawRay(cam.position, cam.forward*10000,Color.yellow);
    }

    private void OnGunShot()
    {
        throw new NotImplementedException();
        //put effects in here
        //uhm for some reason target appears over player on start() dont know why.


    }

    public void TakeDamage(float damage)
    {
        Debug.Log(gunData.damage);
    }
}
