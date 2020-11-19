using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenTowerBehavior : MonoBehaviour
{
    public float oxygenLevel = 30;
    private bool bPlayerInRange = false;
    public Text OxygenCounter;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RefillOxygen());
        
    }

    // Update is called once per frame
    void Update()
    {
        OxygenCounter.text = oxygenLevel.ToString();
    }

    IEnumerator RefillOxygen()
    {
        if (!bPlayerInRange)
        {
            oxygenLevel = Mathf.Clamp(oxygenLevel + 5, 0, 100);
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
