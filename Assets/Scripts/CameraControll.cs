using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
       transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }

    void Update()
    {
        // ToDo add small delay for camera moving to create sense of speed
        //transform.position = Vector3.Lerp(transform.position,new Vector3(player.transform.position.x, player.transform.position.y, -10f), 0.1f);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }
}
