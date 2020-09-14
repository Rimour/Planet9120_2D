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

    public GameObject AttackTower;
    public GameObject OxygenTower;
    public Transform TowerPlacement;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    public float Health;
    public float Resources;
    public float Oxygen;
    public float ShipResources;

    private const float coef = 5f;

    // Update is called once per frame
    void Update()
    {
        // Processing Inputs
        ProcessInputs();
        OxygenSystem();
        //Detect when the E arrow key is pressed down
        if (Input.GetKeyDown(KeyCode.E))
          //  Debug.Log("E key was pressed.");
              SpawnTower();
        if (Input.GetKeyDown(KeyCode.Q))
           // Debug.Log("Q key was pressed.");
            SpawnOxygenTower();
        //PlayerWins
        if (ShipResources == 15)
        {
            Debug.Log("Player Wins!");
        }
        //Player Dies
        if (Health <= 0)
        {
            Debug.Log("Player Died!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Resource"))
        {
            Resources += 5;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Oxygen"))
        {
            Oxygen = 100;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Health -= 20;
        }
        if (other.gameObject.CompareTag("Ship"))
        {
           // if (Input.GetKeyDown(KeyCode.Q))
            //{
                //Debug.Log("Q key was pressed.");
                ShipResources += Resources;
                Resources = 0;
                //GameObject projectile = Instantiate(AttackTower, TowerPlacement.position, TowerPlacement.rotation);
           // }
        }
    }

    public void SpawnTower()
    {
        if (Input.GetKeyDown(KeyCode.E ) && Resources >=2)
        {
            //Debug.Log("E key was pressed.");
            Resources -= 5;
            GameObject projectile = Instantiate(AttackTower, TowerPlacement.position, TowerPlacement.rotation);
        }
    }

    public void SpawnOxygenTower()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Resources >= 2)
        {
            //Debug.Log("Q key was pressed.");
            Resources -= 5;
            GameObject projectile = Instantiate(OxygenTower, TowerPlacement.position, TowerPlacement.rotation);
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