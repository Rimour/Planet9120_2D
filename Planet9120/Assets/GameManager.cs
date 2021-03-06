﻿using System.Collections;
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

    bool SizeCalled;
    int Direction;

    int minutes;
    int hours;
    float seconds;

    [Header("UI")]

    public Text Grenades;

    public Text CurrentWave;
    public Text ShipWarning;
    public Image BloodScreen;
    public Image OxygenScreen;

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

    public void UpdateWave(int currentWave)
    {
        CurrentWave.text = currentWave + " ";
    }

    //Handling the player warnings for oxygen, health and ship health
    public void UpdateWarnings()
    {

        if (ShipCount < 10)
        {
            ShipWarning.gameObject.SetActive(true);


            StartCoroutine(WarningSize());
        }

        else
        {
            SizeCalled = false;
            ShipWarning.gameObject.SetActive(false);
        }

        if ((PlayerScript.Health / PlayerScript.MaxHealth) >= .5f)
        {
            var tempColor = BloodScreen.color;
            tempColor.a = 0;
            BloodScreen.color = tempColor;
        }


        if ((PlayerScript.Health / PlayerScript.MaxHealth) < .5f)
        {
            var tempColor = BloodScreen.color;
            tempColor.a = ((PlayerScript.Health / PlayerScript.MaxHealth) - 1) * (-1); //Setting the alpha of the screen effect based on the ratio of current health and max health
            BloodScreen.color = tempColor;
            tempColor.a = 0;
            OxygenScreen.color = tempColor; //Making sure the oxygen screen isn't seen when the blood screen is active
        }

        else if ((PlayerScript.Oxygen / PlayerScript.MaxOxygen) < .5f)
        {
            var tempColor = OxygenScreen.color;
            tempColor.a = ((PlayerScript.Oxygen / PlayerScript.MaxOxygen) - 1) * (-1); //Setting the alpha of the screen effect based on the ratio of current oxygen and max oxygen
            OxygenScreen.color = tempColor;
        }

        else if((PlayerScript.Oxygen / PlayerScript.MaxOxygen) >= .5f)
        {
            var tempColor = OxygenScreen.color;
            tempColor.a = 0;
            OxygenScreen.color = tempColor;
        }
    }

    IEnumerator WarningSize()
    {
        //if (!SizeCalled)
        //{
        //    SizeCalled = true;
        //    ShipWarning.gameObject.SetActive(true);
        //    yield return new WaitForSecondsRealtime(5);

        //    ShipWarning.gameObject.SetActive(false);
        //}
        if (!SizeCalled)
        {
            SizeCalled = true;
            yield return new WaitForSecondsRealtime(.03f);



            if (ShipWarning.fontSize >= 15)
            {
                Direction = -1;
            }

            else if (ShipWarning.fontSize <= 1)
            {
                Direction = 1;
            }

            ShipWarning.fontSize += Direction;

            SizeCalled = false;
        }
    }

    void Update()
    {
        UpdateWarnings();

        PlayerHealth = PlayerScript.Health;
        if (PlayerHealth <= 0 || ShipHP.Health <= 0)//lose conditions
        {
            Time.timeScale = 0f;
            deathPanel.SetActive(true);
            HighScoreChecking();
            //Debug.Log("You lose");
            //death
        }

        ResourceBox.text = Count.ToString();
        //ShipResourceBox.text = ShipCount.ToString() + " / " + WinCondition.ToString();
        GoldResource.text = GoldResourceCount.ToString() + "/2";

        //ShipHPTracker.value = ShipHP.Health;
        //ShipRepairTracker.value = ShipCount;
        TimeSurvived += Time.deltaTime;
        seconds += Time.deltaTime;
        if(TimeSurvived >= 60)
        {
            minutes++;
            TimeSurvived = 0;
        }

        //hours = (int)minutes % 60;
        SurvivalTime_Lose.text = minutes + ":" + Mathf.Round(TimeSurvived).ToString("00");// + " sec";
        Enemieskilled_Lose.text = EnemiesKilled.ToString();
        Score.text = PlayerScore.ToString();
        SurvivalTime_Win.text = minutes + ":" + Mathf.Round(TimeSurvived).ToString("00");// + " sec";
        Enemieskilled_Win.text = EnemiesKilled.ToString();
        Score_Lose.text = PlayerScore.ToString();
        Score_Win.text = PlayerScore.ToString();
        Multiplier_Text.text = Multiplier.ToString();

        
        if(ShipCount >= WinCondition && GoldResourceCount>= GoldResourceWin)//win conditions 
        {
            Time.timeScale = 0f;
            bronzemedal.SetActive(true);
            if(PlayerScore >= 3000 || TimeSurvived <= 180)
                {
                   silvermedal.SetActive(true);
                }
            if (PlayerScore >= 3000 && TimeSurvived <= 180)
            {
                silvermedal.SetActive(true);
                goldmedal.SetActive(true);  
            }            
            WinPanel.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            PauseGame();

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            ResumeGame();
        }
    }

    void HighScoreChecking()
    {
        if(seconds > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", seconds);
        }

        SurvivalTime_Lose.text = PlayerPrefs.GetFloat("HighScore", 0).ToString();
    }

    
}
