using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControll player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControll>();
    }

    private void FixedUpdate()
    {
        HorizontalInputListeners();
    }

    void Update()
    {
        SpaceListeners();
        LeftClickListeners();
    }

    private void HorizontalInputListeners()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        player.MoveFunction(horizontalInput);
    }

    private void LeftClickListeners()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            player.GetComponent<ICanShoot>().Shoot(Input.mousePosition);
        }
    }

    private void SpaceListeners()
    {
        player.ThowJumpMarkerToFU();
    }
}
