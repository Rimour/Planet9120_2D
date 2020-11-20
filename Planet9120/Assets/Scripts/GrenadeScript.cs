using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public GameObject Explosion;
    public Transform ExplosionTransform;
    bool bExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Explode()
    {
        yield return new WaitForSecondsRealtime(3);
        bExploded = true;
        GameObject Effect = Instantiate(Explosion, ExplosionTransform.position, ExplosionTransform.rotation);
        Destroy(this.gameObject);
    }
}
