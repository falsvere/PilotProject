using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Rigidbody2D playerRB;

    private float speed = 12;
    private float speedInJump = 15;
    private float jumpForce = 10;
    private float rotation = 400;


    private bool activateJump = false;
    private bool isOnFloor = false;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveFunction();
    }
    
    private void MoveFunction()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");


        if (activateJump && horizontalInput == 0)
        {
            isOnFloor = false;
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            activateJump = false;

        } else if (horizontalInput != 0 && activateJump)
        {
            Debug.Log("asdads");
            isOnFloor = false;
            playerRB.rotation *= 0.2f;
            playerRB.velocity *= 0.2f;
            playerRB.AddForce(new Vector2(horizontalInput * speedInJump/11f, jumpForce), ForceMode2D.Impulse);
            activateJump = false;

        }else if (horizontalInput != 0 && !activateJump)
        {
            if (isOnFloor)
            {
                Vector2 moveDirection = new Vector2(speed, 0) * horizontalInput;
                float RBRotation = playerRB.rotation + rotation * Time.fixedDeltaTime * - horizontalInput;

                playerRB.velocity = moveDirection;
      
                playerRB.MoveRotation(RBRotation);
            }
            else
            {
                playerRB.AddForce(new Vector2(speedInJump * horizontalInput, 0) , ForceMode2D.Force);
            }
        }

        if (isOnFloor && horizontalInput == 0)
        {
            playerRB.velocity *= 0f;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isOnFloor)
        {
            activateJump = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Floor")
        {
            isOnFloor = true;
        }


        if (collision.collider.name == "Border")
        {
            playerRB.velocity *= 0f;
        }
    }

}
