using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private GameObject player;

    private float leftBorder;
    private float rightBorder;

    private float cameraDelayDivider = 2f;
    private float cameraFromCenterToBorderDistance;



    void Start()
    {
        player = GameObject.Find("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        cameraFromCenterToBorderDistance = Camera.main.orthographicSize * 2 - player.transform.localScale.x;


        rightBorder = GameObject.FindGameObjectWithTag("Right Border").transform.position.x - cameraFromCenterToBorderDistance;
        leftBorder = GameObject.FindGameObjectWithTag("Left Border").transform.position.x + cameraFromCenterToBorderDistance;
    }

    void LateUpdate()
    {
        SetCameraPosition();
    }

    void SetCameraPosition()
    {
        float playerPositionWithGap = player.transform.position.x - Input.GetAxis("Horizontal") / cameraDelayDivider;

        Vector3 cameraPosition = new Vector3(Mathf.Clamp(playerPositionWithGap, leftBorder, rightBorder), player.transform.position.y, transform.position.z);

        transform.position = cameraPosition;

    }
}
