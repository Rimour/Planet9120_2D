using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    public float Range;
    Vector2 Direction;
    public GameObject target;
    public GameObject Gun;
    public GameObject bullet;
    private bool CanAttack = true;
    private float attackTimer;
    private float cooldown = 1;
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
        Vector2 targetPos = target.transform.position;
        Direction = targetPos - (Vector2)transform.position;
        Gun.transform.up = Direction;
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
                GameObject BulletIns = Instantiate(bullet, Shootpoint.position, Quaternion.identity);
                BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
               // SoundManager.PlaySound("TowerShoot");
               // attackTimer = 0;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
