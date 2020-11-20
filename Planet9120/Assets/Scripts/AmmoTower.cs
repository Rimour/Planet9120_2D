using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTower : MonoBehaviour
{
    public Text AmmoCount;
    public int AmmoUses = 4;
    
    public void Update()
    {
        AmmoCount.text = AmmoUses.ToString();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && AmmoUses > 0)
        {
            AmmoUses--;
            if(AmmoUses == 0)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
