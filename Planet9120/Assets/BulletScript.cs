using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{


    public Rigidbody2D rb;

    public void OntriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Wall":
            Destroy(gameObject);
            break;
            case "Enemy":
            //other.GameObject.GetComponent<MyEnemyScript>().TakeDamage();
            Destroy(gameObject);
            break;
        }
    }



}
