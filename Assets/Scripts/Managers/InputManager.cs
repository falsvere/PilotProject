using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private PlayerControll player;

    void Update()
    {
        SpaceListeners();
        LeftClickListeners();
    }

    private void FixedUpdate()
    {
        HorizontalInputListeners();
    }

    //custom methods
    private void HorizontalInputListeners()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        player.MoveFunction(horizontalInput);
    }

    private void LeftClickListeners()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePositionConvertedFormPx = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            player.Shoot(mousePositionConvertedFormPx);
        }
    }

    private void SpaceListeners()
    {
        //TODO remove ThrowMarker
        player.ThowJumpMarkerToFU();
        player.DropVelocityOnKeysUp();
    }
}
