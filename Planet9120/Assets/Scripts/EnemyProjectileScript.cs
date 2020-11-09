using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;

    private Transform player;
    private Vector2 target;


    public void Start()
    {
        StartCoroutine(NoTarget());
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }
    public void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //if (transform.position.x == target.x && transform.position.y == target.y) ;
    }

    IEnumerator NoTarget()
    {
        yield return new WaitForSeconds(2f);
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
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
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
