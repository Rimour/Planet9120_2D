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
    public GameObject AmmoTower; //Ammo Tower ref
    public Transform TowerPlacement;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    public float Health;
    
    public float Resources;
    public float Oxygen;

    private bool bInOxygenArea;
    private bool bAlreadyInOxygen;
    private bool bInHealthArea;
    private bool bAlreadyInHealth;

    public float Ability1Cooldown = 0;
    public float Ability1UseTime = 0;
    public float Ability2Cooldown = 0;

    public float ShipResources;

    public Slider HPBar;
    public Slider OxyBar;

    private const float coef = 5f;

    GameManager manager;//game manager
    int CountdownTime1 = 5;
    int CountdownTime2 = 5;

    Ship ship;

    public void Start()
    {
       HPBar.maxValue = Health;
       OxyBar.maxValue = Oxygen;
       manager = GameObject.Find("GameManager").GetComponent<GameManager>();// set game manager
       ship = GameObject.FindWithTag("Ship").GetComponent<Ship>();
    }

    IEnumerator Ability1Tracker()//starts 5 second countdown for ability 1
    {
        while (CountdownTime1 > 0)
        {
            manager.Ab1Text.text = CountdownTime1.ToString();
            yield return new WaitForSeconds(1f);
            CountdownTime1--;
        }

        manager.Ab1Text.text = " ";
    }

    IEnumerator Ability2Tracker()//starts 5 second countdown for ability 1
    {
        while (CountdownTime2 > 0)
        {
            manager.Ab2Text.text = CountdownTime2.ToString();
            yield return new WaitForSeconds(1f);
            CountdownTime2--;
        }

        manager.Ab2Text.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        HPBar.value = Health;
        OxyBar.value = Oxygen;

        manager.AmmoCount.text = weapon.bullets.ToString();

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
        SpawnAmmoTower();
        Ability1();
        Ability2();

        if (Ability1UseTime >= 3)
        {
            moveSpeed = 5;
        }

        //if (rb.IsTouching(GameObject.FindWithTag("Oxygen").GetComponent<Collider2D>()))
        //{
        //    Debug.Log("Is colliding with oxygen");
        //}

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string OtherTag = other.gameObject.tag;


        switch (OtherTag)
        {

            case "Resource":
                //if (other.gameObject.CompareTag("Resource"))

                //Resources += 15;
                SoundManager.PlaySound("Resource");
                Destroy(other.gameObject);

                break;

            case "Oxygen":

                OxygenTowerBehavior OxygenRef = other.GetComponent<OxygenTowerBehavior>();
                bInOxygenArea = true;
                if (OxygenRef.oxygenLevel > 0)
                {
                    StartCoroutine(ReceivingOxygen(OxygenRef));
                }
                
                break;

            case "Ship":
                //ShipResources += Resources;
                //manager.ShipCount += manager.Count;
                
                ship.Health += manager.Count;
                manager.Count = 0;

                //Resources = 0;
                bInOxygenArea = true;
                if (ship.Oxygen > 0)
                {
                    StartCoroutine(ReceivingOxygenFromShip(ship));
                }
                // SoundManager.PlaySound("ShipFix");
                break;

            case "Health":

                HealthTowerBehavior HealthRef = other.GetComponent<HealthTowerBehavior>();
                bInHealthArea = true;
                if (HealthRef.HealthLevel > 0)
                {
                    StartCoroutine(ReceivingHealth(HealthRef));
                }

                break;

            case "EnemyProjectile":
                Health -= 10f;
                break;

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        string OtherTag = other.gameObject.tag;
        Debug.Log(OtherTag);

        switch (OtherTag)
        {
            case "Oxygen":
                OxygenTowerBehavior OxygenRef = other.GetComponent<OxygenTowerBehavior>();
                bInOxygenArea = false;
                Debug.Log("Left the oxygen area");
                StopCoroutine(ReceivingOxygen(OxygenRef));
                break;

            case "Ship":
                bInOxygenArea = false;
                StopCoroutine(ReceivingOxygenFromShip(ship));
                break;

            case "Health":
                HealthTowerBehavior HealthRef = other.GetComponent<HealthTowerBehavior>();
                bInHealthArea = false;
                StopCoroutine(ReceivingHealth(HealthRef));  
                break;
        }
    }



    private void OnTriggerStay2D(Collider2D other)
    {
        string OtherTag = other.gameObject.tag;
        

        switch (OtherTag)
        {
            //case "Oxygen":
            //    Oxygen = 100;
            //    break;

            case "Enemy":
                Health -= 0.1f;
                moveSpeed = 0;
                break;

            case "Acid":
                Health -= 0.1f;
                break;

           // case "EnemyProjectile":
          //      Health -= 10f;
           //     break;

            //case "Health":
            //    Health = 100f;
            //    break;

            //case "Ship":
            //    //ShipResources += Resources;
            //    //manager.ShipCount += manager.Count;
            //    ship.Health += manager.Count;
            //    manager.Count = 0;

            //    //Resources = 0;
            //    Oxygen = 100;
            //    // SoundManager.PlaySound("ShipFix");
            //    break;

            case "AmmoTower":
                weapon.bullets = 99;
                break;

            default:
                //Debug.Log("No tag matched");
                break;

        }

        //if (other.gameObject.CompareTag("Oxygen"))
        //{
        //    Oxygen = 100;
        //}
        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    Health -= 0.1f;
        //}
        //if (other.gameObject.CompareTag("Acid"))
        //{
        //    Health -= 0.1f;
        //}
        //if (other.gameObject.CompareTag("EnemyProjectile"))
        //{
        //    Health -= 10f;
        //}
        //if (other.gameObject.CompareTag("Health"))
        //{
        //    Health = 100;
        //}
        //if (other.gameObject.CompareTag("Ship"))
        //{
        //    //ShipResources += Resources;
        //    //manager.ShipCount += manager.Count;
        //    ship.Health += manager.Count;
        //    manager.Count = 0;

        //    //Resources = 0;
        //    Oxygen = 100;
        //    // SoundManager.PlaySound("ShipFix");
        //}

    }

    public IEnumerator ReceivingHealth(HealthTowerBehavior TowerRef)
    {
        if (!bAlreadyInHealth)
        {
            bAlreadyInHealth = true;
            Health = Mathf.Clamp(Health + 10, 0, 100);
            TowerRef.HealthLevel = Mathf.Clamp(TowerRef.HealthLevel - 10, 0, 100);

            yield return new WaitForSecondsRealtime(1f);
            bAlreadyInHealth = false;
            if ((bInHealthArea) && TowerRef.HealthLevel > 0)
            {
                StartCoroutine(ReceivingHealth(TowerRef));
            }

            yield return new WaitForSecondsRealtime(1f);
            bAlreadyInHealth = false;

        }

    }
    public IEnumerator ReceivingOxygen(OxygenTowerBehavior TowerRef)
    {
        if (!bAlreadyInOxygen)
        {
            bAlreadyInOxygen = true;
            Oxygen = Mathf.Clamp(Oxygen + 10, 0, 100);    
            TowerRef.oxygenLevel = Mathf.Clamp(TowerRef.oxygenLevel - 10, 0, 100);

            yield return new WaitForSecondsRealtime(1f);
            bAlreadyInOxygen = false;
            if ((bInOxygenArea) && TowerRef.oxygenLevel > 0)
            {
                StartCoroutine(ReceivingOxygen(TowerRef));
            }

            yield return new WaitForSecondsRealtime(1f);
            bAlreadyInOxygen = false;

        }

    }

    public IEnumerator ReceivingOxygenFromShip(Ship ShipRef)
    {
        if (!bAlreadyInOxygen)
        {
            bAlreadyInOxygen = true;
            Oxygen = Mathf.Clamp(Oxygen + 10, 0, 100);
            ShipRef.Oxygen = Mathf.Clamp(ShipRef.Oxygen - 10, 0, 100);

            yield return new WaitForSecondsRealtime(1f);
            bAlreadyInOxygen = false;
            if ((bInOxygenArea) && ShipRef.Oxygen > 0)
            {
                StartCoroutine(ReceivingOxygenFromShip(ShipRef));
            }

            yield return new WaitForSecondsRealtime(1f);
            bAlreadyInOxygen = false;

        }

    }


    public void SpawnTower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && manager.Count >=10)
        {
            //Debug.Log("E key was pressed.");
            manager.Count -= 10;
            SoundManager.PlaySound("PlaceTower");
            GameObject projectile = Instantiate(AttackTower, TowerPlacement.position, TowerPlacement.rotation);
        }
    }
    public void SpawnTower2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && manager.Count >= 10)
        {
            //Debug.Log("E key was pressed.");
            manager.Count -= 10;
            SoundManager.PlaySound("PlaceTower");
            GameObject projectile = Instantiate(LongTower, TowerPlacement.position, TowerPlacement.rotation);
        }
    }

    public void SpawnOxygenTower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && manager.Count >= 5)
        {
            //Debug.Log("Q key was pressed.");
            manager.Count -= 5;
            SoundManager.PlaySound("PlaceTower");
            GameObject projectile = Instantiate(OxygenTower, TowerPlacement.position, TowerPlacement.rotation);
        }
    }

    public void SpawnHealthTower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && manager.Count >= 10)
        {
            //Debug.Log("Q key was pressed.");
            manager.Count -= 10;
            SoundManager.PlaySound("PlaceTower");
            GameObject projectile = Instantiate(HealthTower, TowerPlacement.position, TowerPlacement.rotation);
        }
    }

    public void SpawnAmmoTower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5) && manager.Count >= 1)
        {
            //Debug.Log("Q key was pressed.");
            manager.Count -= 1;
            SoundManager.PlaySound("PlaceTower");
            GameObject projectile = Instantiate(AmmoTower, TowerPlacement.position, TowerPlacement.rotation);
        }
    }

    void OxygenSystem()
    {

        if (Oxygen >= 0)
        {
            Oxygen -= coef * Time.deltaTime;
        }
        if (Oxygen <= 0)
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
                CountdownTime1 = 5;//sets start time for ability countdown
                StartCoroutine(Ability1Tracker());//displays countdown for ability


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
                CountdownTime2 = 5;//sets start time for ability countdown
                StartCoroutine(Ability2Tracker());//displays countdown for ability
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
            if (!weapon.bIsShooting)
            {
                weapon.bIsShooting = true;
                StartCoroutine(weapon.Firing());
               // weapon.Fire();
            }

            
        }

        if(Input.GetMouseButtonUp(0))
        {
            weapon.bIsShooting = false;
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