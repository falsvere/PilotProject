using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private GameObject player;

    private float leftBorder;
    private float rightBorder;

    void Start()
    {
        player = GameObject.Find("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        rightBorder = GameObject.FindGameObjectWithTag("Left Border").transform.position.x + 0.55f;
        leftBorder = GameObject.FindGameObjectWithTag("Left Border").transform.position.x + 0.55f;
        Debug.Log(rightBorder);
    }

    void LateUpdate()
    {
        // ToDo add small delay for camera moving to create sense of speed
        //transform.position = Vector3.Lerp(transform.position,new Vector3(player.transform.position.x, player.transform.position.y, -10f), 0.1f);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        Debug.Log(transform.position.x + Camera.main.orthographicSize * 2 - 1.117319f);
    }
}
