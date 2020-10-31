using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public GameObject background;
    public GameObject PressSpaceText;
    public GameObject CreditsPanel;
    public GameObject SettingsPanel;
    bool spaceWasPressed;

    public void PlayGameButton()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void CreditsButton()
    {
        background.SetActive(false);
        CreditsPanel.SetActive(true);
    }
    public void SettingsButton()
    {
        SettingsPanel.SetActive(true);
        background.SetActive(false);
    }
    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void CloseMenus()
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
