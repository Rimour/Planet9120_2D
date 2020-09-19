using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeTower : MonoBehaviour
{
    public float Range;
    Transform Enemy;
    bool Detected = false;
    Vector2 Direction;
    public GameObject Gun;
    public GameObject bullet;
    public float FireRate;
    float nextTimeToFire = 0;
    public Transform Shootpoint;
    public float Force;

    // Start is called before the first frame update
    void Start()
    {
       // if (Enemy == null GameObject.FindWithTag("Enemies"))
      //  Enemy = GameObject.FindWithTag("Enemy").transform;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector2 targetPos = Enemy.position;
            Direction = targetPos - (Vector2)transform.position;
            Gun.transform.up = Direction;
            shoot();
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy = null;
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
