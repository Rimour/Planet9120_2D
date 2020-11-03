using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    public GameObject Resource;
    public ColorType TypeOfColor;
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
        if(TypeOfColor == ColorType.Blue)
        {
            Gizmos.color = Color.blue;
        }
        else if(TypeOfColor == ColorType.Red)
        {
            Gizmos.color = Color.red;
        }else if(TypeOfColor == ColorType.Yellow)
        {
            Gizmos.color = Color.yellow;
        }else if (TypeOfColor == ColorType.Green)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.white;
        }
        
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}

public enum ColorType
{
    Red,
    Yellow,
    Green,
    Blue, 
    Gold
}
