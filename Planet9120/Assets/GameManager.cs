using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerController PlayerScript;
    GameObject Player;
    public Text ResourceBox;
    public Text ShipResourceBox;
    float PlayerHealth;
    public GameObject deathPanel;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerController>();
        deathPanel.SetActive(false);
        
    }
    public void StartOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        ResourceBox.text = PlayerScript.Resources.ToString();
        ShipResourceBox.text = PlayerScript.ShipResources.ToString();

        PlayerHealth = PlayerScript.Health;
        if(PlayerHealth <= 0)
        {
            deathPanel.SetActive(true);
            //death
        }
    }
}
