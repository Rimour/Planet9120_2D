using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float damageTo;
    public Rigidbody2D rb;

    public void Start()
    {
        StartCoroutine(NoTarget());
    }

    IEnumerator NoTarget()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
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
            EnemyBehaviour EnemyHealth = other.GetComponent<EnemyBehaviour>();
            EnemyHealth.takeDamage(damageTo);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (other.tag == "obstruction")
        {
            Destroy(gameObject);
        }
    }



}
