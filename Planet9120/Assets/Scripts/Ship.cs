using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public float Health = 100;
    float HealthToDisplay;
    public float Oxygen = 200;
    public Text ShipStatus;
    public Text ShipOxygen;

    private bool bPlayerInRange;

    GameManager Manager;

    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        HealthToDisplay = Mathf.Floor((Health / Manager.WinCondition) *100);

        Manager.ShipCount = (int)Health;
        if(HealthToDisplay > 100)
        {
            HealthToDisplay = 100;
        }

        if (Health <= 0)
        {
            HealthToDisplay = 0;
        }

        ShipStatus.text = HealthToDisplay + " %";

        ShipOxygen.text = Oxygen.ToString();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
          //  Health -= Time.deltaTime;
        }
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

    IEnumerator RefillOxygen()
    {
        if (!bPlayerInRange)
        {
            Oxygen = Mathf.Clamp(Oxygen + 5, 0, 100);
        }

        yield return new WaitForSecondsRealtime(1f);

        StartCoroutine(RefillOxygen());
    }
}
