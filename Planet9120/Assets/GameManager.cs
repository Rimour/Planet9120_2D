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
    public int GoldResourceWin = 2; //amount of gold resource needed to win
    public int GoldResourceCount = 0; //current amount of gold resource 
    public float EnemiesKilled = 0; //How many enemies killed
    public float TimeSurvived = 0; // How long the player survived
    public float PlayerScore = 0; // How many points the player has

    Ship ShipHP;
    
    [Header("UI")]
    public Text ResourceBox;
    public Text GoldResource;
    public Text AmmoCount;
    //public Text ShipResourceBox;
    public GameObject deathPanel;
    public GameObject WinPanel;
    public GameObject PausePanel;
    bool isPaused;
    public GameObject PauseButtons;
    public GameObject SettingsPanel;
    public GameObject ControlsPanel;
    public Slider ShipHPTracker;
    public Slider ShipRepairTracker;
    [Header("Player Abilities")]
    public Text Ab1Text;//ability 1 text slot
    public Text Ab2Text;//ability 2 text slot
    [Header("Score System")]
    public Text Enemieskilled;
    public Text SurvivalTime;
    public Text Score;

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
        PausePanel.GetComponentInChildren<Text>().text = "Pause";
        PauseButtons.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        SettingsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        PausePanel.SetActive(false);        
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void OpenSettings()//opens settings menu
    {
        PausePanel.GetComponentInChildren<Text>().text = "";
        PauseButtons.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    public void CloseSettings()//Closes settings menu
    {
        PausePanel.GetComponentInChildren<Text>().text = "Pause";
        PauseButtons.SetActive(true);
        SettingsPanel.SetActive(false);
    }
    public void OpenControls()//opens controls menu
    {
        PausePanel.GetComponentInChildren<Text>().text = "";
        PauseButtons.SetActive(false);
        ControlsPanel.SetActive(true);
    }

    public void CloseControls()
    {
        PausePanel.GetComponentInChildren<Text>().text = "Pause";
        PauseButtons.SetActive(true);
        ControlsPanel.SetActive(false);
    }

    public void GoToMain()//load main menu scene
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ExitGame()// exits game
    {
        Application.Quit();
    }

   
    
    // Update is called once per frame
    void Update()
    {
       
        ResourceBox.text = Count.ToString();
        //ShipResourceBox.text = ShipCount.ToString() + " / " + WinCondition.ToString();
        GoldResource.text = GoldResourceCount.ToString() + "/2";

        ShipHPTracker.value = ShipHP.Health;
        ShipRepairTracker.value = ShipCount;

        TimeSurvived += Time.deltaTime;
        SurvivalTime.text = Count.ToString();
        Enemieskilled.text = Count.ToString();
        Score.text = Count.ToString();
        //SurvivalTime.Text = "Time Survived:" + TimeSurvived;
       // Enemieskilled.Text = "Enemies Killed:" + EnemiesKilled;
       // Score.Text = "Score:" + PlayerScore;

        PlayerHealth = PlayerScript.Health;
        if(PlayerHealth <= 0 || ShipHP.Health <= 0)//lose conditions
        {
            Time.timeScale = 0f;
            deathPanel.SetActive(true);
            Debug.Log("You lose");
            //death
        }
        
        if(ShipCount >= WinCondition && GoldResourceCount>= GoldResourceWin)//win conditions 
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
