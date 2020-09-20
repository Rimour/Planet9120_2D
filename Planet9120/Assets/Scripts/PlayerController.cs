using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Weapon weapon;

    public GameObject AttackTower;
    public GameObject LongTower;
    public GameObject OxygenTower;
    public GameObject HealthTower;
    public GameObject Mine;
    public Transform TowerPlacement;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    public float Health;
    
    public float Resources;
    public float Oxygen;

    public float Ability1Cooldown = 0;
    public float Ability1UseTime = 0;
    public float Ability2Cooldown = 0;

    public float ShipResources;

    public Slider HPBar;
    public Slider OxyBar;

    private const float coef = 5f;

    public void Start()
    {
       HPBar.maxValue = Health;
       OxyBar.maxValue = Oxygen;
    }

    // Update is called once per frame
    void Update()
    {
        HPBar.value = Health;
        OxyBar.value = Oxygen;

        Ability2Cooldown += Time.deltaTime;
        Ability1Cooldown += Time.deltaTime;
        Ability1UseTime += Time.deltaTime;

        // Processing Inputs
        ProcessInputs();
        OxygenSystem();
        SpawnTower();
        SpawnTower2();
        SpawnOxygenTower();
        SpawnHealthTower();
        Ability1();
        Ability2();

        if (Ability1UseTime >= 3)
        {
            moveSpeed = 5;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Resource"))
        {
            Resources += 15;
            SoundManager.PlaySound("Resource");
            Destroy(other.gameObject);
        }
    }
        private void OnTriggerStay2D(Collider2D other)
        {    
            if (other.gameObject.CompareTag("Oxygen"))
            {
                Oxygen = 100;
            }
            if (other.gameObject.CompareTag("Enemy"))
            {
                Health -= 20;
            }
            if (other.gameObject.CompareTag("Health"))
            {
                Health = 100;
            }
            if (other.gameObject.CompareTag("Ship"))
        {
                ShipResources += Resources;
                Resources = 0;
                Oxygen = 100;
               // SoundManager.PlaySound("ShipFix");

        }
    }

    public void SpawnTower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Resources >=10)
        {
            //Debug.Log("E key was pressed.");
            Resources -= 10;
            SoundManager.PlaySound("PlaceTower");
            GameObject projectile = Instantiate(AttackTower, TowerPlacement.position, TowerPlacement.rotation);
        }
    }
    public void SpawnTower2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && Resources >= 10)
        {
            //Debug.Log("E key was pressed.");
            Resources -= 10;
            SoundManager.PlaySound("PlaceTower");
            GameObject projectile = Instantiate(LongTower, TowerPlacement.position, TowerPlacement.rotation);
        }
    }

    public void SpawnOxygenTower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && Resources >= 5)
        {
            //Debug.Log("Q key was pressed.");
            Resources -= 5;
            SoundManager.PlaySound("PlaceTower");
            GameObject projectile = Instantiate(OxygenTower, TowerPlacement.position, TowerPlacement.rotation);
        }
    }

    public void SpawnHealthTower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && Resources >= 10)
        {
            //Debug.Log("Q key was pressed.");
            Resources -= 10;
            SoundManager.PlaySound("PlaceTower");
            GameObject projectile = Instantiate(HealthTower, TowerPlacement.position, TowerPlacement.rotation);
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

    void Ability1()
    {
        //run
        if (Input.GetKeyDown(KeyCode.Q) && Ability1Cooldown >= 5)
        {
                moveSpeed = 10;
                Ability1Cooldown = 0;
                Ability1UseTime = 0;
           
        }
    }
    void Ability2()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Ability2Cooldown >= 5)
            {
               // Debug.Log("Ability 2");
                GameObject projectile = Instantiate(Mine, TowerPlacement.position, TowerPlacement.rotation);
                Ability2Cooldown = 0;
            }       
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