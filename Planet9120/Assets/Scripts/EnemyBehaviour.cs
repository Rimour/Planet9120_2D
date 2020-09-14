using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Transform Ship;
    Transform Player;
    //Transform []Towers;
    public float Speed;
    public float PlayerRange;
    public float attackRange;

    public void Start()
    {
        Ship = GameObject.FindWithTag("Ship").transform;
        Player = GameObject.FindWithTag("Player").transform;
        //Towers = GameObject.FindGameObjectsWithTag("Tower").transform;
    }
   
    public void Update()
    {
        float MoveSpd = Speed * Time.deltaTime;//enemy movement speed
        float ShipDist = Vector2.Distance(Ship.position, transform.position);//distance between enemy & ship
        float PlayerDist = Vector2.Distance(Player.position, transform.position);//distance between enemy & player
        if(PlayerDist <= PlayerRange)// if enemy is within range of player
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, MoveSpd);//enemy moves towards player
            if(PlayerDist >= attackRange)
            {
                //attack player animation
                //deal damage to player
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Ship.position, MoveSpd);//enemy moves towards ship
            if(ShipDist <= attackRange)
            {
                //attack player animation
                //deal damage to ship
            }
        }
        
    }
}
