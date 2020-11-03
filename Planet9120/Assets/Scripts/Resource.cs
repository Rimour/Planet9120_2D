using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int ResourceGained;
    public float RespawnTime;
    public bool bIsGold;
    GameManager Manager;

    public void Start()
    {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

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
            if (!bIsGold)
            {
                Manager.Count += ResourceGained;
                StartCoroutine(Respawn());
            }

            else
            {
                Manager.GoldResourceCount += ResourceGained;
            }
        }

    }
   
}
