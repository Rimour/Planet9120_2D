using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTower : MonoBehaviour
{
    public Text AmmoCount;
    public int AmmoUses = 4;
    bool bCanRefill = true;
    
    public void Update()
    {
        AmmoCount.text = AmmoUses.ToString();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && AmmoUses > 0 && bCanRefill)
        {
            bCanRefill = false;
            StartCoroutine(ResetRefill());
            AmmoUses--;
            if(AmmoUses == 0)
            {
                Destroy(this.gameObject);
            }
        }

    }

    IEnumerator ResetRefill()
    {
        yield return new WaitForSecondsRealtime(2);
        bCanRefill = true;
    }
}
