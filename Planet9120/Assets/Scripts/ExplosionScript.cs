using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
            if (collision.CompareTag("Enemy"))
            {
                Debug.Log("it did collide tho");
                EnemyBehaviour EnemyRef = collision.gameObject.GetComponent<EnemyBehaviour>();
                EnemyRef.CurrentHP -= .3f;
            }
        
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSecondsRealtime(2);
        Destroy(this.gameObject);
    }
}
