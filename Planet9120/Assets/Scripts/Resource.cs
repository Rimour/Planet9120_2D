using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int ResourceGained;
    public int RespawnTime;

    IEnumerator Respawn()
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(RespawnTime);
        this.gameObject.SetActive(true);

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Respawn());
        }

    }
   
}
