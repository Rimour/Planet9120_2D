using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildMode : MonoBehaviour
{
    public GameObject towers;
    public bool BuildModeActive;
    public GameObject BuildModeUI;
    public Text TowerNameText;
    public Text TowerDescriptText;
    public string[] TowerInfo;
    public Image TowerImage;
    public Sprite[] TowerSprites;

    public void EnterBuildMode()
    {
        BuildModeActive = true;
        towers.SetActive(true);
        BuildModeUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ExitBuildMode()
    {
        BuildModeActive = false;
        towers.SetActive(false);
        BuildModeUI.SetActive(false);
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && BuildModeActive == false)
        {
            EnterBuildMode();
        }else if(Input.GetKeyDown(KeyCode.Space) && BuildModeActive == true)
        {
            ExitBuildMode();
        }
    }

    public void ViewTowerOne()
    {
        TowerNameText.text = " Rocket Tower ";
        TowerDescriptText.text = TowerInfo[0];
        TowerImage.sprite = TowerSprites[0];
    }

    public void ViewTowerTwo()
    {
        TowerNameText.text = " Heavy Rocket Tower ";
        TowerDescriptText.text = TowerInfo[1];
        TowerImage.sprite = TowerSprites[1];
    }

    public void ViewTowerThree()
    {
        TowerNameText.text = " Oxygen Tower ";
        TowerDescriptText.text = TowerInfo[2];
        TowerImage.sprite = TowerSprites[2];
    }

    public void ViewTowerFour()
    {
        TowerNameText.text = " Healing Tower ";
        TowerDescriptText.text = TowerInfo[3];
        TowerImage.sprite = TowerSprites[3];
    }

    public void ViewTowerFive()
    {
        TowerNameText.text = " Ammo Tower ";
        TowerDescriptText.text = TowerInfo[4];
        TowerImage.sprite = TowerSprites[4];
    }
}
