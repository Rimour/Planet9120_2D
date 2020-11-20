using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip ResourceSound, ShootSound, TowerShoot, TowerPlace, Mine, EnemyDeath, ShipRepairSFX, MineSFX, DashSFX, PlayerHitSFX;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        ResourceSound = Resources.Load<AudioClip>("Resource");
        ShootSound = Resources.Load<AudioClip>("Shoot");
        Mine = Resources.Load<AudioClip>("Mine");
        TowerPlace = Resources.Load<AudioClip>("PlaceTower");
        TowerShoot = Resources.Load<AudioClip>("TowerShoot");
        EnemyDeath = Resources.Load<AudioClip>("EnemyDeath");
        ShipRepairSFX = Resources.Load<AudioClip>("ShipRepairSFX");
        MineSFX = Resources.Load<AudioClip>("MineSFX");
        DashSFX = Resources.Load<AudioClip>("DashSFX");
        PlayerHitSFX = Resources.Load<AudioClip>("PlayerHitSFX");

        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Shoot":
                audioSrc.PlayOneShot(ShootSound);
                break;
            case "Mine":
                audioSrc.PlayOneShot(Mine);
                break;
            case "Resource":
                audioSrc.PlayOneShot(ResourceSound);
                break;
            case "TowerShoot":
                audioSrc.PlayOneShot(TowerShoot);
                break;
            case "PlaceTower":
                audioSrc.PlayOneShot(TowerPlace);
                break;
            case "EnemyDeath":
                audioSrc.PlayOneShot(EnemyDeath);
                break;
            case "ShipRepairSFX":
                audioSrc.PlayOneShot(ShipRepairSFX);
                break;
            case "MineSFX":
                audioSrc.PlayOneShot(MineSFX);
                break;
            case "DashSFX":
                audioSrc.PlayOneShot(DashSFX);
                break;
            case "PlayerHitSFX":
                audioSrc.PlayOneShot(PlayerHitSFX);
                break;

        }
    }
        
}
