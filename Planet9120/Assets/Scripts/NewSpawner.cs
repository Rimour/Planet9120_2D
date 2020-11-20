using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawner : MonoBehaviour
{
    GameManager Manager;
    public GameObject[] EnemyPrefabs;
    int Amount;
    public int MaxAmount;
    public float Range;
    float Xpos;
    float Ypos;
    float MaxX, MaxY, MinX, MinY;

    public float TimeBetweenSpawn;
    public float TimeBetweenWaves;

    private int CurrentWave = 1;

    public int NumberOfEnemies;

    void Start()
    {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Awake()
    {
        StartCoroutine(SpawnNewWave());
    }

    public void Update()
    {
        //if (NumberOfEnemies <= NumberOfEnemies / 2)
        //{
        //    StartCoroutine(SpawnNewWave());
        //}

    

        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    IEnumerator LookForEnemies()
    {
        Debug.Log("Looking for enemies");
        yield return new WaitForSecondsRealtime(1);
        if (!GameObject.FindGameObjectWithTag("Enemy"))
        {
            StartCoroutine(StartNewWave());
        }

        else
        {
            StartCoroutine(LookForEnemies());
        }
    }

    IEnumerator SpawnNewWave()
    {
        yield return new WaitForSecondsRealtime(10);

        while(NumberOfEnemies < MaxAmount)
        {
            yield return new WaitForSecondsRealtime(TimeBetweenSpawn);
            Instantiate(EnemyPrefabs[Random.Range(0, 4)], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            NumberOfEnemies++;
        }

            StartCoroutine(StartNewWave());
       

    }


    IEnumerator StartNewWave()
    {
        yield return new WaitForSecondsRealtime(TimeBetweenWaves);
        CurrentWave++;
        MaxAmount += 2;
        NumberOfEnemies = 0;
        TimeBetweenWaves += 20;
        Manager.UpdateWave(CurrentWave);
        StartCoroutine(SpawnNewWave());
    }

}
