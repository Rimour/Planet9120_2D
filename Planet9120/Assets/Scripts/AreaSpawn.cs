using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawn : MonoBehaviour
{
    public GameObject theEnemy;
    int xPos;
    int zPos;
    int yPos;
    int EnemyCount;
    public int MaxEnemyCount = 10;
    public int minXPos;
    public int maxXPos;
    public int minYPos;
    public int maxYPos;
    public int minZPos;
    public int maxZPos;
    


    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (EnemyCount < MaxEnemyCount)
        {
            xPos = Random.Range(minXPos, maxXPos);
            zPos = Random.Range(minZPos, maxZPos);
            yPos = Random.Range(minYPos, maxYPos);
            Instantiate(theEnemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            EnemyCount += 1;
        }
    }
}
