using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private PlayerControll playerConrtroll;

    private float leftBorder;
    private float rightBorder;
    
    [SerializeField]
    private float cameraDelayDivider = 1f;
    private float cameraFromCenterToBorderDistance;

    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        cameraFromCenterToBorderDistance = Camera.main.orthographicSize * Camera.main.aspect;

        //max left/right positions of camera center
        rightBorder = GameObject.FindGameObjectWithTag("Right Border").transform.position.x - cameraFromCenterToBorderDistance;
        leftBorder = GameObject.FindGameObjectWithTag("Left Border").transform.position.x + cameraFromCenterToBorderDistance;
    }

    void LateUpdate()
    {
        SetCameraPosition();
    }

    void SetCameraPosition()
    {
        float playerPositionWithGap = player.transform.position.x - Input.GetAxis("Horizontal") * cameraDelayDivider;

        Vector3 cameraPosition = new Vector3(Mathf.Clamp(playerPositionWithGap, leftBorder, rightBorder), player.transform.position.y, transform.position.z);

        transform.position = cameraPosition;
    }
}
