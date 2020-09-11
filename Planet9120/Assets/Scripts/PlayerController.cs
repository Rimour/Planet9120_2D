using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Weapon weapon;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    public float Health;
    public float Resources;
    public float Oxygen;

    private const float coef = 5f;

    // Update is called once per frame
    void Update()
    {
        // Processing Inputs
        ProcessInputs();
        OxygenSystem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Resource"))
        {
            Resources++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Oxygen"))
        {
            Oxygen = 100;
        }

    }

    void OxygenSystem()
    {
        if (Oxygen >= 0) 
        { 
            Oxygen -= coef * Time.deltaTime;
        }
        else if (Oxygen <= 0)
        {
            Health -= coef * Time.deltaTime;
        }
    }


    void FixedUpdate()
    {
        // Physics Calculation
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        // Rotate player to follow mouse
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}