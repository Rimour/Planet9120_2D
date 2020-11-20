using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeTower : MonoBehaviour
{
    public float Range;
    private EnemyBehaviour target;
    private Queue<EnemyBehaviour> enemy = new Queue<EnemyBehaviour>();
    Vector2 Direction;
    public GameObject Gun;
    public GameObject bullet;
    private bool CanAttack = true;
    private float attackTimer;
    private float cooldown = 1;
    //public float FireRate;
    float nextTimeToFire = 0;
    public Transform Shootpoint;
    public float Force;
    public float Health;
    public int Ammo;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        shoot();
       // Debug.Log(target);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemy.Enqueue(other.GetComponent<EnemyBehaviour>());
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            target = null;
            CanAttack = false;
        }
    }

    void shoot()
    {

        if (!CanAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= cooldown)
            {
                CanAttack = true;
                attackTimer = 0;
            }
        }
        if (target == null && enemy.Count > 0)
        {
            target = enemy.Dequeue();

        }
        if (target != null && target.Alive)
        {
            //Debug.Log("fire");
            if (CanAttack)
            {
                Vector2 targetPos = target.transform.position;
                Direction = targetPos - (Vector2)transform.position;
                Gun.transform.up = Direction;
                GameObject BulletIns = Instantiate(bullet, Shootpoint.position, Quaternion.identity);
                BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
                Ammo--;
                CanAttack = false;
            }

        }

        if (Ammo < 0)
        {
            Destroy(this.gameObject);
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
