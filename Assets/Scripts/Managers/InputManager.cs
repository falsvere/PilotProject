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

        if (Input.GetButtonUp("Horizontal"))
        {
            player.DropVelocityOnKeysUp();
        }
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
            mousePositionConvertedFormPx.z = 0;
            player.Shoot(mousePositionConvertedFormPx);
        }
    }

    private void SpaceListeners()
    {
        if(Input.GetButtonDown("Jump"))
        {
            player.Jump();
        }
    }
}
