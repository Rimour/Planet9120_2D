using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;

    public float fireForce;

    public int bullets = 100;
    public bool bIsShooting = false;
    public bool bisFiring = false;

    public void Fire()
    {
        if (bIsShooting && !bisFiring)
        {
            bisFiring = true;
            bullets--;
            GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            SoundManager.PlaySound("Shoot");
        }
        
    }

    public IEnumerator Firing()
    {

        if (!bisFiring && bIsShooting && bullets > 0)
        {
            bisFiring = true;
            bullets--;
            GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            SoundManager.PlaySound("Shoot");

            yield return new WaitForSecondsRealtime(.3f);

            bisFiring = false;
            if (bIsShooting)
            {
                  StartCoroutine(Firing());             
            }
        }
                  
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
