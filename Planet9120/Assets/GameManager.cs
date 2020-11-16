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
    public float Multiplier = 1; // How many points the player has

    Ship ShipHP;

    [Header("UI")]
    public Text ResourceBox;
    public Text GoldResource;
    public Text AmmoCount;
    public Texture2D cursorTexture;
    //public Text ShipResourceBox;
    public GameObject deathPanel;
    public GameObject WinPanel;
    public GameObject PausePanel;
    bool isPaused;
    public GameObject PauseButtons;
    public GameObject SettingsPanel;
    public GameObject ControlsPanel;
    //public Slider ShipHPTracker;
    //public Slider ShipRepairTracker;
    [Header("Player Abilities")]
    public Text Ab1Text;//ability 1 text slot
    public Text Ab2Text;//ability 2 text slot
    [Header("Score System")]
    public Text Score;
    public Text Enemieskilled_Lose;
    public Text SurvivalTime_Lose;
    public Text Score_Lose;
    public Text Enemieskilled_Win;
    public Text SurvivalTime_Win;
    public Text Score_Win;
    public Text Multiplier_Text;
    public GameObject bronzemedal;
    public GameObject silvermedal;
    public GameObject goldmedal;

    // Start is called before the first frame update
    void Start()
    {       
        Time.timeScale = 1f;
        ShipHP = GameObject.Find("Ship2d").GetComponent<Ship>();
        Player = GameObject.FindWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerController>();
        //ShipHPTracker.maxValue = ShipHP.Health;
        //ShipRepairTracker.maxValue = WinCondition;
        deathPanel.SetActive(false);
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void StartOver()//restarts game scene
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void PauseGame()//pauses game
    {
        PausePanel.SetActive(true);
        PausePanel.GetComponentInChildren<Text>().text = "Pause";
        PauseButtons.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void ResumeGame()//resumes game from pause
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
    public void CloseControls()//closes control panel
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

    void Update()
    {      
        ResourceBox.text = Count.ToString();
        //ShipResourceBox.text = ShipCount.ToString() + " / " + WinCondition.ToString();
        GoldResource.text = GoldResourceCount.ToString() + "/2";

        //ShipHPTracker.value = ShipHP.Health;
        //ShipRepairTracker.value = ShipCount;
        TimeSurvived += Time.deltaTime;
        SurvivalTime_Lose.text = Mathf.Round(TimeSurvived).ToString() + " sec";
        Enemieskilled_Lose.text = EnemiesKilled.ToString();
        Score.text = PlayerScore.ToString();
        SurvivalTime_Win.text = Mathf.Round(TimeSurvived).ToString() + " sec";
        Enemieskilled_Win.text = EnemiesKilled.ToString();
        Score_Lose.text = PlayerScore.ToString();
        Score_Win.text = PlayerScore.ToString();
        Multiplier_Text.text = Multiplier.ToString();

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
            bronzemedal.SetActive(true);
            if(PlayerScore >= 1500 && PlayerScore <= 3000)
              {
                   silvermedal.SetActive(true);
              }
            else if (PlayerScore >= 3001)
            {
                silvermedal.SetActive(true);
                goldmedal.SetActive(true);  
            }            
            WinPanel.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            PauseGame();

        }else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            ResumeGame();
        }
    }

    
}
