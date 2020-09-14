using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject AttackTower;
    public GameObject OxygenTower;
    public Transform TowerPlacement;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    public float Health;
    public float Resources;
    public float Oxygen;
    public float ShipResources;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
