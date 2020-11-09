using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTowerBehavior : MonoBehaviour
{
    public float HealthLevel = 30;
    private bool bPlayerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RefillOxygen());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator RefillOxygen()
    {
        if (!bPlayerInRange)
        {
            HealthLevel = Mathf.Clamp(HealthLevel + 5, 0, 100);
            Debug.Log("Oxygen tower went up");
        }

        //else
        //{
        //    oxygenLevel = Mathf.Clamp(oxygenLevel - 10, 0, 100);
        //    Debug.Log("oxygen tower went down");
        //    Debug.Log(oxygenLevel);
        //}

        yield return new WaitForSecondsRealtime(1f);

        StartCoroutine(RefillOxygen());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string OtherTag = other.gameObject.tag;

        if (OtherTag == "Player")
        {
            bPlayerInRange = true;

            //StartCoroutine(EmptyOxygen());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        string OtherTag = other.gameObject.tag;
        Debug.Log(OtherTag);

        if (OtherTag == "Player")
        {
            bPlayerInRange = false;
        }
    }
}
