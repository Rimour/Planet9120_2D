using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer Mixer;
    public Toggle FullScreenToggle;
    
    public void SetMasterVol(float volume)
    {
        Mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetFullScreen(bool full)
    {
        Screen.fullScreen = full;
    }
}
