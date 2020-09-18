using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    public GameObject Resource;
    int Amount;
    public int MaxAmount;
    public float Range;
    float Xpos;
    float Ypos;
    float MaxX, MaxY, MinX, MinY;


    public void Start()
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
        
        while(Amount < MaxAmount)
        {
            Xpos = Random.Range(MinX, MaxX);
            Ypos = Random.Range(MinY, MaxY);
            Instantiate(Resource, new Vector3( Xpos, Ypos, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
            Amount++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
