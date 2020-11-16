using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public float Health = 30;
    public float Oxygen = 200;
    public Text ShipStatus;

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
        if (Health <= 0)
        {
            Debug.Log("Player Loses");
        }

        Manager.ShipCount = (int)Health;
        ShipStatus.text = Mathf.Round(Health).ToString() + " %";
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Health -= Time.deltaTime;
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
