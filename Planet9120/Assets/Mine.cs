using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    GameObject EnemyTarget;
    EnemyBehaviour EnemyHealth;
    public float damageTo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
          //  EnemyHealth.takeDamage(damageTo);
            Destroy(gameObject);
        }
    }

}
