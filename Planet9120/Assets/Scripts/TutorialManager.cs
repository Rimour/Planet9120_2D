using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public Image Screen;
    int slideNumber = 0;
    public Sprite[] Slide;

    public void Start()
    {
        slideNumber = 0;
    }
    public void NextSlide()
    {
        if(slideNumber == 0)            
        {
            slideNumber++;
            Screen.sprite = Slide[0];
        }else if (slideNumber == 1)
        {
            slideNumber++;
            Screen.sprite = Slide[1];

        }
        else if (slideNumber == 2)
        {
            slideNumber++;
            Screen.sprite = Slide[2];

        }
        else if (slideNumber == 3)
        {
            slideNumber++;
            Screen.sprite = Slide[3];

        }
        else if (slideNumber == 4)
        {
            slideNumber++;
            Screen.sprite = Slide[4];

        }
        else if (slideNumber == 5)
        {
            slideNumber++;
            Screen.sprite = Slide[5];

        }
        else if (slideNumber == 6)
        {
            slideNumber++;
            Screen.sprite = Slide[6];

        }
        else if (slideNumber == 7)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
           

        }

    }
}
