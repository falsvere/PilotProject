using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Rigidbody2D playerRB;

    private float speed = 12f;
    private float speedInJump = 20f;
    private float jumpForce = 10f;
    private float rotation = 400f;
    private float jumpXSpeedInMov = 1.35f;



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

        //if player press jump in static
        if (activateJump && horizontalInput == 0)
        {
            isOnFloor = false;
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            activateJump = false;

        } 
        // if player press jump in movement on floor
        else if (horizontalInput != 0 && activateJump)
        {
            isOnFloor = false;
            playerRB.rotation *= 0.2f;
            playerRB.velocity *= 0.2f;
            playerRB.AddForce(new Vector2(horizontalInput * jumpXSpeedInMov, jumpForce), ForceMode2D.Impulse);
            activateJump = false;

        }
        //if player move without jumping on floor or in air
        else if (horizontalInput != 0 && !activateJump)
        {
            if (isOnFloor)
            {
                Vector2 velocity = new Vector2(speed, 0) * horizontalInput;
                float RBRotation = playerRB.rotation + rotation * Time.fixedDeltaTime * - horizontalInput;

                playerRB.velocity = velocity;
      
                playerRB.MoveRotation(RBRotation);
            }
            else
            {
                playerRB.AddForce(new Vector2(speedInJump * horizontalInput, 0) , ForceMode2D.Force);
            }
        }


        //dpop speed when control buttons do not pressed to avoid inertia
        if (isOnFloor && horizontalInput == 0)
        {
            playerRB.velocity *= 0f;
        }
    }

    private void Update()
    {
        //throw to fixedUpdate marker that space was pressed
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

        if (collision.collider.CompareTag("Platform"))
        {
            isOnFloor = true;
            playerRB.velocity *= 0f;
        }

        if (collision.collider.name == "Border")
        {
            playerRB.velocity *= 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "Floor" || collision.collider.CompareTag("Platform"))
        {
            isOnFloor = false;
        }
    }

}
