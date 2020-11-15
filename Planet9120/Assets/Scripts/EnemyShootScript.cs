using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    public float Range;
    Vector2 Direction;
    public Transform target;
    bool Detected = false;
    public GameObject Gun;
    public GameObject Bullet;
    public float FireRate;
    float nextTimeToFire = 0;
    public Transform Shootpoint;
    public float Force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = target.position;
        Direction = targetPos - (Vector2)transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);

        if(rayInfo)
        {
            if(rayInfo.collider.gameObject.tag == "Player")
            {
                if(Detected == false)
                {
                    Detected = true;
                }
            }

            else
            {
                {
                    if (Detected == true)
                    {
                        Detected = false;
                    }
                }
            }

            if (Detected)
            {
                Gun.transform.up = Direction;

                if(Time.time > nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / FireRate;
                    shoot();
                }
            }

        }
        //shoot();
        //attackTimer += Time.deltaTime;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shoot();
        }
    }

    void shoot()
    {
                GameObject BulletIns = Instantiate(Bullet, Shootpoint.position, Quaternion.identity);
               // BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
               // SoundManager.PlaySound("TowerShoot");
               // attackTimer = 0;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
