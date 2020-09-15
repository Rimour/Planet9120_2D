using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform Player;
    void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
    }
}
