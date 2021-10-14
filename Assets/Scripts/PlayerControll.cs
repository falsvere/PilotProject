using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Rigidbody2D playerRB;


    private float speed = 12;
    private float rotation = 60;

    private bool isOnFloor = false;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

 

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if(horizontalInput != 0)
        {

            Vector2 moveDirection = new Vector2(speed, 0) * horizontalInput;

            playerRB.velocity  = moveDirection;
            playerRB.MoveRotation(playerRB.rotation * rotation * Time.fixedDeltaTime);
        }
        else
        {
            if (isOnFloor)
            {
                playerRB.velocity *= 0f;
                playerRB.rotation *= 0f;
            }

        }


    }

    void Update()
    {


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Floor")
        {
            isOnFloor = true;
        }
    }
}
