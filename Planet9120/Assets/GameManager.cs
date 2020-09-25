using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerController PlayerScript;
    GameObject Player;    
    float PlayerHealth;//player's health
    
    public float WinCondition = 100;//amount needed to fix ship
    public int Count;//amount of resources collected by player
    public int ShipCount;// amount of resources in ship

    Ship ShipHP;
    
    [Header("UI")]
    public Text ResourceBox;
    //public Text ShipResourceBox;
    public GameObject deathPanel;
    public GameObject WinPanel;
    public GameObject PausePanel;
    bool isPaused;
    public Slider ShipHPTracker;
    public Slider ShipRepairTracker;
    public Text Ab1Text;
    public Text Ab2Text;

    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 1f;
        ShipHP = GameObject.Find("Ship2d").GetComponent<Ship>();
        Player = GameObject.FindWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerController>();
        ShipHPTracker.maxValue = ShipHP.Health;
        ShipRepairTracker.maxValue = WinCondition;
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
       
        ResourceBox.text = Count.ToString();
        //ShipResourceBox.text = ShipCount.ToString() + " / " + WinCondition.ToString();

        ShipHPTracker.value = ShipHP.Health;
        ShipRepairTracker.value = ShipCount;

        PlayerHealth = PlayerScript.Health;
        if(PlayerHealth <= 0 || ShipHP.Health <= 0)//lose conditions
        {
            Time.timeScale = 0f;
            deathPanel.SetActive(true);
            Debug.Log("You lose");
            //death
        }
        
        if(ShipCount >= WinCondition)//win conditions 
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
