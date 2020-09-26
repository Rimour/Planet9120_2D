using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Transform Ship;
    Transform Player;
    //Transform []Towers;
    public float MaxHP;
    public float CurrentHP;
    public float Speed;
    public float PlayerRange;
    public float attackRange;
    public float attackDamage;
    public bool Alive;
    public GameObject BloodSplat;
    public Transform EnemyDeath;

    public void Start()
    {
        Alive = true;
        CurrentHP = MaxHP;
        Ship = GameObject.FindWithTag("Ship").transform;
        Player = GameObject.FindWithTag("Player").transform;
        //Towers = GameObject.FindGameObjectsWithTag("Tower").transform;
    }

    public void takeDamage(float damage)
    {
        CurrentHP -= damage;
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

        if(CurrentHP <= 0)
        {
            Alive = false;
            SoundManager.PlaySound("EnemyDeath");
            GameObject projectile = Instantiate(BloodSplat, EnemyDeath.position, EnemyDeath.rotation);
            Destroy(this.gameObject);
            
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mine"))
        {
            CurrentHP -= 25;
        }
    }
}
