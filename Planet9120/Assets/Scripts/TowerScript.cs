using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TowerScript : MonoBehaviour
{
    public float Range;
    public Transform Target;
    bool Detected = false;
    Vector2 Direction;
    public GameObject Gun;
    public GameObject bullet;
    public float FireRate;
    float nextTimeToFire = 0;
    public Transform Shootpoint;
    public float Force;
    public float Health;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    { 
            Vector2 targetPos = Target.position;
            Direction = targetPos - (Vector2)transform.position;
            Gun.transform.up = Direction;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector2 targetPos = Target.position;
            Direction = targetPos - (Vector2)transform.position;
            Gun.transform.up = Direction;
            shoot();
        }
    }
 
    void shoot()
    {
        GameObject BulletIns = Instantiate(bullet, Shootpoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}