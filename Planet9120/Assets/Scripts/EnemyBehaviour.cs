using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Transform Ship;
    public float Speed;

    public void Start()
    {
        Ship = GameObject.FindWithTag("Ship").transform;
    }
    public void Movement()
    {
        float MoveSpd = Speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Ship.position, MoveSpd);
    }
    public void Update()
    {
        Movement();
    }
}
