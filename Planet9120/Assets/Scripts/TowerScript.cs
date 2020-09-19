using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TowerScript : MonoBehaviour
{
    public float Range;
    Transform Enemy;
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
        Enemy = GameObject.FindWithTag("Enemy").transform;
    }
    // Update is called once per frame
    void Update()
    { 
        //float EnemyDist = Vector2.Distance(Enemy.position, transform.position);//distance between enemy & player
    //    if (EnemyDist <= Range)// if enemy is within range of player
     //   {

     //       if (EnemyDist >= Range)
   //           Direction = targetPos - (Vector2)transform.position;
    //           Gun.transform.up = Direction;
     //          shoot();
               
     //   }
    }
   public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Vector2 targetPos = Enemy.position;
            Direction = targetPos - (Vector2)transform.position;
            Gun.transform.up = Direction;
            shoot();
            if (Enemy != null && !other.Alive)
            {

            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
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