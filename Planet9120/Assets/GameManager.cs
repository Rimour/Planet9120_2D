using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerController PlayerScript;
    GameObject Player;    
    float PlayerHealth;
    float ShipFixed;
    public float WinCondition = 100;
    [Header("UI")]
    public Text ResourceBox;
    public Text ShipResourceBox;
    public GameObject deathPanel;
    public GameObject WinPanel;
    public GameObject PausePanel;
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerController>();
        deathPanel.SetActive(false);
        
    }
    public void StartOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        PausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
    
    // Update is called once per frame
    void Update()
    {
        ResourceBox.text = PlayerScript.Resources.ToString();
        ShipResourceBox.text = PlayerScript.ShipResources.ToString() + " / " + WinCondition.ToString();

        PlayerHealth = PlayerScript.Health;
        if(PlayerHealth <= 0)
        {
            Time.timeScale = 0f;
            deathPanel.SetActive(true);
            //death
        }
        ShipFixed = PlayerScript.ShipResources;
        if(ShipFixed >= WinCondition)
        {
            Time.timeScale = 0f;
            WinPanel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isPaused == false)
        {
            PauseGame();

        }else if (Input.GetKeyDown(KeyCode.Space) && isPaused == true)
        {
            ResumeGame();
        }
    }
}
