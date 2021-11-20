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
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        player.MoveFunction(horizontalInput);
    }

    void Update()
    {
        player.ThowJumpMarkerToFU();
    }
}
