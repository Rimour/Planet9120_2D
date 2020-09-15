using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float damageTo;
    public Rigidbody2D rb;

    GameObject EnemyTarget;
    EnemyBehaviour EnemyHealth;
    

    public void Start()
    {
        EnemyTarget = GameObject.FindWithTag("Enemy");
        EnemyHealth = EnemyTarget.GetComponent<EnemyBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Resource"))
        {  
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Oxygen"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth.takeDamage(damageTo);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Ship"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }



}
