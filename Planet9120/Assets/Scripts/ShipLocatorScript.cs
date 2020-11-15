using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLocatorScript : MonoBehaviour
{
    public float moveSpeed;
    public Transform Target;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 Direction;
    public GameObject Arrow;

    void Start()
    {
    }
    void Update()
    {
        Vector2 targetPos = Target.position;
        Direction = targetPos - (Vector2)transform.position;
        Arrow.transform.up = Direction;
        Move();
        ProcessInputs();
    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
