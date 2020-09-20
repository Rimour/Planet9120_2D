using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    GameManager Manager;
    public GameObject EnemyPrefab;
    int Amount;
    public int MaxAmount;
    public float Range;
    float Xpos;
    float Ypos;
    float MaxX, MaxY, MinX, MinY;

    public int NumberOfEnemies;
    void Start()
    {
        
        MaxX = transform.position.x + Range;
        MaxY = transform.position.y + Range;
        MinX = transform.position.x - Range;
        MinY = transform.position.y - Range;
    }

    public void Awake()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {

        while (Amount < MaxAmount)
        {
            Xpos = Random.Range(MinX, MaxX);
            Ypos = Random.Range(MinY, MaxY);
            Instantiate(EnemyPrefab, new Vector3(Xpos, Ypos, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
            Amount++;
            NumberOfEnemies++;
        }
    }

    public void Update()
    {
        if (NumberOfEnemies <= NumberOfEnemies / 2)
        {
            StartCoroutine(Spawn());
        }
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
