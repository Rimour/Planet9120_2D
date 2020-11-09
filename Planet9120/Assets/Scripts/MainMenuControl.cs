using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public GameObject background;
    public GameObject PressSpaceText;//press space text
    public GameObject CreditsPanel;//credits menu
    public GameObject SettingsPanel;//settings menu
    bool spaceWasPressed;

    public void PlayGameButton()//load game scene
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void CreditsButton()//open credits menu
    {
        background.SetActive(false);
        CreditsPanel.SetActive(true);
    }
    public void SettingsButton()//open settigns menu
    {
        SettingsPanel.SetActive(true);
        background.SetActive(false);
    }
    public void QuitGameButton()//exit game
    {
        Application.Quit();
    }
    public void CloseMenus()//close game menus
    {
        spaceWasPressed = true;
        CreditsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        background.SetActive(true);
        PressSpaceText.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { if (spaceWasPressed == false)
            {
                spaceWasPressed = true;
                background.SetActive(true);
                PressSpaceText.SetActive(false);
            }            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenus();
        }
    }
}
