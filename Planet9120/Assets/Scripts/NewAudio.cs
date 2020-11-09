using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class NewAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip newSound;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            source.clip = newSound;
            source.Play();
        }
    }
}
