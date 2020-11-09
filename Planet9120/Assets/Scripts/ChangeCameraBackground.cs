using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraBackground : MonoBehaviour
{
    public Camera cam;
    public Color CurrentColor;
    public Color NewColor;

    public void Start()
    {
        //cam = GetComponent<Camera>();
        CurrentColor = cam.backgroundColor;
        //cam.clearFlags = CameraClearFlags.SolidColor;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            cam.backgroundColor = NewColor;        
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam.backgroundColor = CurrentColor;
        }
    }

}
