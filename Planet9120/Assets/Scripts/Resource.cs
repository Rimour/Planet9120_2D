using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int ResourceGained;
    public float RespawnTime;
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
            Manager.Count += ResourceGained;
            StartCoroutine(Respawn());
        }

    }
   
}
